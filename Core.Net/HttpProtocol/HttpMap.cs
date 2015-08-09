using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.IO;

namespace Core.Net.HttpProtocol
{
    /// <summary>
    /// Http协议解析类
    /// </summary>
    public class HttpMap
    {
        #region PROPERTIES
        private string[] m_StrHttpField = new string[52];
        private byte[] m_byteData = null;

        private static Regex _charsetRegex = new Regex(@"charset=([\w\-]+)");
        private static Regex _httpResponseTextTypeRegex = new Regex(@"(text|application)/(html|xml|json)");

        public string[] HttpField
        {
            get { return m_StrHttpField; }
            set { m_StrHttpField = value; }
        }
        public byte[] Data
        {
            get { return m_byteData; }
            set { m_byteData = value; }
        }

        /// <summary>
        /// 获取Http字段
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>字段值</returns>
        public string GetHttpField(HttpHeaderField fieldName)
        {
            return HttpField[(int)fieldName];
        }

        /// <summary>
        /// 获取内容的字符集编码信息，如果没有则返回UTF8编码
        /// </summary>
        public Encoding CharsetEncoding
        {
            get
            {
                var m = _charsetRegex.Match(GetHttpField(HttpHeaderField.Content_Type));
                if (!m.Groups[1].Value.IsNullOrEmpty())
                {
                    return Encoding.GetEncoding(m.Groups[1].Value);
                }
                else
                {
                    return Encoding.UTF8;
                }
            }
        }

        /// <summary>
        /// 获取内容是否为文本类型，通常html或xml或json类型的内容会被视为文本类型
        /// </summary>
        public bool IsTextContent => _httpResponseTextTypeRegex.IsMatch(GetHttpField(HttpHeaderField.Content_Type));

        /// <summary>
        /// 解码数据，通常用于解gzip压缩，如果数据未经压缩则直接返回
        /// </summary>
        /// <returns>解码后的数据</returns>
        public byte[] DecodeData()
        {
            if (GetHttpField(HttpHeaderField.Content_Encoding).Trim().ToLower() == "gzip")
            {
                return GZip.解压缩(Data);
            }
            else return Data;
        }

        /// <summary>
        /// 转换数据为字符串，将数据解码并按字符集编码后返回字符串。
        /// 注意：如果数据为空或不是文本类型的数据，则会返回空字符串。
        /// </summary>
        /// <returns>字符串数据</returns>
        public string ConversionDataToString()
        {
            if (Data.IsNullOrEmpty()) return null;
            return IsTextContent ? CharsetEncoding.GetString(DecodeData()) : null;
        }

        #endregion
        // convertion
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();


        #region CONSTRUCTEUR
        /// <summary>
        /// Constructeur par défaut - non utilisé
        /// </summary>
        private HttpMap()
        { }

        public HttpMap(byte[] ByteHttpRequest)
        {
            string HttpRequest = encoding.GetString(ByteHttpRequest);
            try
            {
                int IndexHeaderEnd;
                string Header;

                // Si la taille de requête est supérieur ou égale à 1460, alors toutes la chaine est l'entête Http
                if (HttpRequest.Length <= 1460)
                    Header = HttpRequest;
                else
                {
                    IndexHeaderEnd = HttpRequest.IndexOf("\r\n\r\n");
                    Header = HttpRequest.Substring(0, IndexHeaderEnd);
                    Data = ByteHttpRequest.Skip(IndexHeaderEnd + 4).ToArray();
                }

                HttpHeaderParse(Header);
            }
            catch (Exception)
            { }
        }
        #endregion

        #region METHODES
        private void HttpHeaderParse(string Header)
        {
            #region Http HEADER REQUEST & RESPONSE

            HttpHeaderField HHField;
            string Httpfield, buffer;
            int Index;
            foreach (int IndexHttpfield in Enum.GetValues(typeof(HttpHeaderField)))
            {
                HHField = (HttpHeaderField)IndexHttpfield;
                Httpfield = "\n" + HHField.ToString().Replace('_', '-') + ": "; //Ajout de \n devant pour éviter les doublons entre cookie et set_cookie
                // Si le champ n'est pas présent dans la requête, on passe au champ suivant
                Index = Header.IndexOf(Httpfield);
                if (Index == -1)
                    continue;

                buffer = Header.Substring(Index + Httpfield.Length);
                Index = buffer.IndexOf("\r\n");
                if (Index == -1)
                    m_StrHttpField[IndexHttpfield] = buffer.Trim();
                else
                    m_StrHttpField[IndexHttpfield] = buffer.Substring(0, Index).Trim();

                //Console.WriteLine("Index = " + IndexHttpfield + " | champ = " + Httpfield.Substring(1) + " " + m_StrHttpField[IndexHttpfield]);
            }

            // Affichage de tout les champs
            /*for (int j = 0; j < m_StrHttpField.Length; j++)
            {
                HHField = (HttpHeaderField)j;
                Console.WriteLine("m_StrHttpField[" + j + "]; " + HHField + " = " + m_StrHttpField[j]);
            }
            */
            #endregion

        }
        #endregion
    }
}
