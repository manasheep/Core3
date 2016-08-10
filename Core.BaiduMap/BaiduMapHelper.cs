using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BaiduMap
{
    /// <summary>
    /// 百度地图助手
    /// </summary>
    public class BaiduMapHelper
    {
        public BaiduMapHelper(string ak)
        {
            Ak = ak;
        }

        /// <summary>
        /// 访问应用（AK），注意服务端应用和浏览器端应用不通用，具体查询帮助。
        /// 注册申请：http://lbsyun.baidu.com/index.php?title=%E9%A6%96%E9%A1%B5
        /// </summary>
        public string Ak { get; set; }

        /// <summary>
        /// 获取静态地图图像的网址。
        /// 百度地图静态图API，可实现将百度地图以图片形式嵌入到您的网页中。您只需发送HTTP请求访问百度地图静态图服务，便可在网页上以图片形式显示您的地图。静态图API较之JavaScript API载入的动态网站，既能满足基本的地图信息浏览，又能加快网页访问速度。
        /// 静态图API是百度地图Web服务API中的一种，它根据所设定的参数，通过标准HTTP协议，返回PNG格式的地图图片。通过给img标签设置src属性即可将地图图片显示在网页中。用户可以指定图片的尺寸、地图的显示范围（包含中心点和缩放级别），还可以放置一些覆盖物在地图上，以生成符合需求的地图图片。
        /// 说明页：http://lbsyun.baidu.com/index.php?title=static
        /// </summary>
        /// <param name="width">图片宽度。取值范围：(0, 1024]。Scale=2,取值范围：(0, 512]。默认为400</param>
        /// <param name="height">图片高度。取值范围：(0, 1024]。Scale=2,取值范围：(0, 512]。默认为300</param>
        /// <param name="center">地图中心点位置，参数可以为经纬度坐标或名称。坐标格式：lng(经度)，lat(纬度)，例如116.43213,38.76623。默认为"北京"</param>
        /// <param name="zoom">地图级别。高清图范围[3, 18]；低清图范围[3,19]。默认为11</param>
        /// <param name="copyright">静态图版权样式，0表示log+文字描述样式，1表示纯文字描述样式，默认为0。</param>
        /// <param name="scale">返回图片大小会根据此标志调整。取值范围为1或2：1表示返回的图片大小为size= width * height;2表示返回图片为(width*2)*(height *2)，且zoom加1注：如果zoom为最大级别，则返回图片为（width*2）*（height*2），zoom不变。默认为null</param>
        /// <param name="bbox">地图视野范围。格式：minX,minY;maxX,maxY。</param>
        /// <param name="markers">标注，可通过经纬度或地址/地名描述；多个标注之间用竖线分隔。</param>
        /// <param name="markerStyles">与markers有对应关系。markerStyles可设置默认图标样式和自定义图标样式。其中设置默认图标样式时，可指定的属性包括size,label和color；设置自定义图标时，可指定的属性包括url，注意，设置自定义图标时需要先传-1以此区分默认图标。</param>
        /// <param name="labels">标签，可通过经纬度或地址/地名描述；多个标签之间用竖线分隔。坐标格式：lng&lt;经度&gt;，lat&lt;纬度&gt;，例如116.43213,38.76623。</param>
        /// <param name="labelStyles">标签样式 content, fontWeight,fontSize,fontColor,bgColor, border。与labels一一对应。</param>
        /// <param name="path">折线，可通过经纬度或地址/地名描述；多个折线用竖线"|"分隔；每条折线的点用分号";"分隔；点坐标用逗号","分隔。坐标格式：lng&lt;经度&gt;，lat&lt;纬度&gt;，例如116.43213,38.76623。</param>
        /// <param name="pathStyles">折线样式 color,weight,opacity[,fillColor]。</param>
        /// <returns>静态地图图像的网址</returns>
        public string CreateStaticImageUrl(
            int? width = null, int? height = null, string center = null, byte? zoom = null,
            byte? copyright = null, byte? scale = null, string bbox = null,
            string markers = null, string markerStyles = null, string labels = null, string labelStyles = null,
            string path = null, string pathStyles = null
            )
        {
            var str = new StringBuilder("http://api.map.baidu.com/staticimage/v2?ak=");

            str.Append(Ak);
            if (width != null)
            {
                str.Append("&width=");
                str.Append(width.Value);
            }
            if (height != null)
            {
                str.Append("&height=");
                str.Append(height.Value);
            }
            if (center != null)
            {
                str.Append("&center=");
                str.Append(center);
            }
            if (zoom != null)
            {
                str.Append("&zoom=");
                str.Append(zoom.Value);
            }
            if (copyright != null)
            {
                str.Append("&copyright=");
                str.Append(copyright.Value);
            }
            if (scale != null)
            {
                str.Append("&scale=");
                str.Append(scale.Value);
            }
            if (bbox != null)
            {
                str.Append("&bbox=");
                str.Append(bbox);
            }
            if (markers != null)
            {
                str.Append("&markers=");
                str.Append(markers);
            }
            if (markerStyles != null)
            {
                str.Append("&markerStyles=");
                str.Append(markerStyles);
            }
            if (labels != null)
            {
                str.Append("&labels=");
                str.Append(labels);
            }
            if (path != null)
            {
                str.Append("&path=");
                str.Append(path);
            }
            if (labelStyles != null)
            {
                str.Append("&labelStyles=");
                str.Append(labelStyles);
            }
            if (pathStyles != null)
            {
                str.Append("&pathStyles=");
                str.Append(pathStyles);
            }

            return str.ToString();
        }
    }
}
