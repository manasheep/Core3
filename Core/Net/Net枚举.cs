using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Net
{
    /// <summary>
    /// 文件类型枚举，可以通过GetRemark扩展方法获取其对应的MIME类型。如存在多个MIME类型，则以逗号分隔。
    /// </summary>
    public enum 文件类型
    {

        /// <summary>
        /// 对应ContentType(MIME)类型：video/3gpp
        /// </summary>
        [Remark("video/3gpp")]
        _3gp,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-authoware-bin
        /// </summary>
        [Remark("application/x-authoware-bin")]
        _aab,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-authoware-map
        /// </summary>
        [Remark("application/x-authoware-map")]
        _aam,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-authoware-seg
        /// </summary>
        [Remark("application/x-authoware-seg")]
        _aas,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/postscript
        /// </summary>
        [Remark("application/postscript")]
        _ai,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-aiff
        /// </summary>
        [Remark("audio/x-aiff")]
        _aif,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-aiff
        /// </summary>
        [Remark("audio/x-aiff")]
        _aifc,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-aiff
        /// </summary>
        [Remark("audio/x-aiff")]
        _aiff,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/X-Alpha5
        /// </summary>
        [Remark("audio/X-Alpha5")]
        _als,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mpeg
        /// </summary>
        [Remark("application/x-mpeg")]
        _amc,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _ani,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Remark("text/plain")]
        _asc,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/astound
        /// </summary>
        [Remark("application/astound")]
        _asd,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-ms-asf
        /// </summary>
        [Remark("video/x-ms-asf")]
        _asf,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/astound
        /// </summary>
        [Remark("application/astound")]
        _asn,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-asap
        /// </summary>
        [Remark("application/x-asap")]
        _asp,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-ms-asf
        /// </summary>
        [Remark("video/x-ms-asf")]
        _asx,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/basic
        /// </summary>
        [Remark("audio/basic")]
        _au,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _avb,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-msvideo
        /// </summary>
        [Remark("video/x-msvideo")]
        _avi,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/amr-wb
        /// </summary>
        [Remark("audio/amr-wb")]
        _awb,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-bcpio
        /// </summary>
        [Remark("application/x-bcpio")]
        _bcpio,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _bin,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/bld
        /// </summary>
        [Remark("application/bld")]
        _bld,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/bld2
        /// </summary>
        [Remark("application/bld2")]
        _bld2,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-MS-bmp
        /// </summary>
        [Remark("application/x-MS-bmp")]
        _bmp,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _bpk,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-bzip2
        /// </summary>
        [Remark("application/x-bzip2")]
        _bz2,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-cals
        /// </summary>
        [Remark("image/x-cals")]
        _cal,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-cnc
        /// </summary>
        [Remark("application/x-cnc")]
        _ccn,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-cocoa
        /// </summary>
        [Remark("application/x-cocoa")]
        _cco,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-netcdf
        /// </summary>
        [Remark("application/x-netcdf")]
        _cdf,

        /// <summary>
        /// 对应ContentType(MIME)类型：magnus-internal/cgi
        /// </summary>
        [Remark("magnus-internal/cgi")]
        _cgi,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-chat
        /// </summary>
        [Remark("application/x-chat")]
        _chat,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _class,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-msclip
        /// </summary>
        [Remark("application/x-msclip")]
        _clp,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-cmx
        /// </summary>
        [Remark("application/x-cmx")]
        _cmx,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-cult3d-object
        /// </summary>
        [Remark("application/x-cult3d-object")]
        _co,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/cis-cod
        /// </summary>
        [Remark("image/cis-cod")]
        _cod,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-cpio
        /// </summary>
        [Remark("application/x-cpio")]
        _cpio,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/mac-compactpro
        /// </summary>
        [Remark("application/mac-compactpro")]
        _cpt,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mscardfile
        /// </summary>
        [Remark("application/x-mscardfile")]
        _crd,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-csh
        /// </summary>
        [Remark("application/x-csh")]
        _csh,

        /// <summary>
        /// 对应ContentType(MIME)类型：chemical/x-csml
        /// </summary>
        [Remark("chemical/x-csml")]
        _csm,

        /// <summary>
        /// 对应ContentType(MIME)类型：chemical/x-csml
        /// </summary>
        [Remark("chemical/x-csml")]
        _csml,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/css
        /// </summary>
        [Remark("text/css")]
        _css,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _cur,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-lml/x-evm
        /// </summary>
        [Remark("x-lml/x-evm")]
        _dcm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-director
        /// </summary>
        [Remark("application/x-director")]
        _dcr,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-dcx
        /// </summary>
        [Remark("image/x-dcx")]
        _dcx,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/html
        /// </summary>
        [Remark("text/html")]
        _dhtml,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-director
        /// </summary>
        [Remark("application/x-director")]
        _dir,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _dll,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _dmg,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _dms,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/msword
        /// </summary>
        [Remark("application/msword")]
        _doc,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-dot
        /// </summary>
        [Remark("application/x-dot")]
        _dot,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-dvi
        /// </summary>
        [Remark("application/x-dvi")]
        _dvi,

        /// <summary>
        /// 对应ContentType(MIME)类型：drawing/x-dwf
        /// </summary>
        [Remark("drawing/x-dwf")]
        _dwf,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-autocad
        /// </summary>
        [Remark("application/x-autocad")]
        _dwg,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-autocad
        /// </summary>
        [Remark("application/x-autocad")]
        _dxf,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-director
        /// </summary>
        [Remark("application/x-director")]
        _dxr,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-expandedbook
        /// </summary>
        [Remark("application/x-expandedbook")]
        _ebk,

        /// <summary>
        /// 对应ContentType(MIME)类型：chemical/x-embl-dl-nucleotide
        /// </summary>
        [Remark("chemical/x-embl-dl-nucleotide")]
        _emb,

        /// <summary>
        /// 对应ContentType(MIME)类型：chemical/x-embl-dl-nucleotide
        /// </summary>
        [Remark("chemical/x-embl-dl-nucleotide")]
        _embl,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/postscript
        /// </summary>
        [Remark("application/postscript")]
        _eps,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-eri
        /// </summary>
        [Remark("image/x-eri")]
        _eri,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/echospeech
        /// </summary>
        [Remark("audio/echospeech")]
        _es,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/echospeech
        /// </summary>
        [Remark("audio/echospeech")]
        _esl,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-earthtime
        /// </summary>
        [Remark("application/x-earthtime")]
        _etc,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/x-setext
        /// </summary>
        [Remark("text/x-setext")]
        _etx,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-lml/x-evm
        /// </summary>
        [Remark("x-lml/x-evm")]
        _evm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-envoy
        /// </summary>
        [Remark("application/x-envoy")]
        _evy,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _exe,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-freehand
        /// </summary>
        [Remark("image/x-freehand")]
        _fh4,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-freehand
        /// </summary>
        [Remark("image/x-freehand")]
        _fh5,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-freehand
        /// </summary>
        [Remark("image/x-freehand")]
        _fhc,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/fif
        /// </summary>
        [Remark("image/fif")]
        _fif,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-maker
        /// </summary>
        [Remark("application/x-maker")]
        _fm,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-fpx
        /// </summary>
        [Remark("image/x-fpx")]
        _fpx,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/isivideo
        /// </summary>
        [Remark("video/isivideo")]
        _fvi,

        /// <summary>
        /// 对应ContentType(MIME)类型：chemical/x-gaussian-input
        /// </summary>
        [Remark("chemical/x-gaussian-input")]
        _gau,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-gca-compressed
        /// </summary>
        [Remark("application/x-gca-compressed")]
        _gca,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-lml/x-gdb
        /// </summary>
        [Remark("x-lml/x-gdb")]
        _gdb,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/gif
        /// </summary>
        [Remark("image/gif")]
        _gif,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-gps
        /// </summary>
        [Remark("application/x-gps")]
        _gps,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-gtar
        /// </summary>
        [Remark("application/x-gtar")]
        _gtar,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-gzip
        /// </summary>
        [Remark("application/x-gzip")]
        _gz,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-hdf
        /// </summary>
        [Remark("application/x-hdf")]
        _hdf,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/x-hdml
        /// </summary>
        [Remark("text/x-hdml")]
        _hdm,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/x-hdml
        /// </summary>
        [Remark("text/x-hdml")]
        _hdml,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/winhlp
        /// </summary>
        [Remark("application/winhlp")]
        _hlp,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/mac-binhex40
        /// </summary>
        [Remark("application/mac-binhex40")]
        _hqx,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/html
        /// </summary>
        [Remark("text/html")]
        _htm,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/html
        /// </summary>
        [Remark("text/html")]
        _html,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/html
        /// </summary>
        [Remark("text/html")]
        _hts,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-conference/x-cooltalk
        /// </summary>
        [Remark("x-conference/x-cooltalk")]
        _ice,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _ico,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/ief
        /// </summary>
        [Remark("image/ief")]
        _ief,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/gif
        /// </summary>
        [Remark("image/gif")]
        _ifm,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/ifs
        /// </summary>
        [Remark("image/ifs")]
        _ifs,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/melody
        /// </summary>
        [Remark("audio/melody")]
        _imy,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-NET-Install
        /// </summary>
        [Remark("application/x-NET-Install")]
        _ins,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-ipscript
        /// </summary>
        [Remark("application/x-ipscript")]
        _ips,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-ipix
        /// </summary>
        [Remark("application/x-ipix")]
        _ipx,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _it,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _itz,

        /// <summary>
        /// 对应ContentType(MIME)类型：i-world/i-vrml
        /// </summary>
        [Remark("i-world/i-vrml")]
        _ivr,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/j2k
        /// </summary>
        [Remark("image/j2k")]
        _j2k,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/vnd.sun.j2me.app-descriptor
        /// </summary>
        [Remark("text/vnd.sun.j2me.app-descriptor")]
        _jad,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-jam
        /// </summary>
        [Remark("application/x-jam")]
        _jam,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/java-archive
        /// </summary>
        [Remark("application/java-archive")]
        _jar,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-java-jnlp-file
        /// </summary>
        [Remark("application/x-java-jnlp-file")]
        _jnlp,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/jpeg
        /// </summary>
        [Remark("image/jpeg")]
        _jpe,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/jpeg
        /// </summary>
        [Remark("image/jpeg")]
        _jpeg,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/jpeg
        /// </summary>
        [Remark("image/jpeg")]
        _jpg,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/jpeg
        /// </summary>
        [Remark("image/jpeg")]
        _jpz,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-javascript
        /// </summary>
        [Remark("application/x-javascript")]
        _js,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/jwc
        /// </summary>
        [Remark("application/jwc")]
        _jwc,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-kjx
        /// </summary>
        [Remark("application/x-kjx")]
        _kjx,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-lml/x-lak
        /// </summary>
        [Remark("x-lml/x-lak")]
        _lak,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-latex
        /// </summary>
        [Remark("application/x-latex")]
        _latex,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/fastman
        /// </summary>
        [Remark("application/fastman")]
        _lcc,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-digitalloca
        /// </summary>
        [Remark("application/x-digitalloca")]
        _lcl,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-digitalloca
        /// </summary>
        [Remark("application/x-digitalloca")]
        _lcr,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/lgh
        /// </summary>
        [Remark("application/lgh")]
        _lgh,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _lha,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-lml/x-lml
        /// </summary>
        [Remark("x-lml/x-lml")]
        _lml,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-lml/x-lmlpack
        /// </summary>
        [Remark("x-lml/x-lmlpack")]
        _lmlpack,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-ms-asf
        /// </summary>
        [Remark("video/x-ms-asf")]
        _lsf,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-ms-asf
        /// </summary>
        [Remark("video/x-ms-asf")]
        _lsx,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-lzh
        /// </summary>
        [Remark("application/x-lzh")]
        _lzh,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-msmediaview
        /// </summary>
        [Remark("application/x-msmediaview")]
        _m13,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-msmediaview
        /// </summary>
        [Remark("application/x-msmediaview")]
        _m14,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _m15,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mpegurl
        /// </summary>
        [Remark("audio/x-mpegurl")]
        _m3u,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mpegurl
        /// </summary>
        [Remark("audio/x-mpegurl")]
        _m3url,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/ma1
        /// </summary>
        [Remark("audio/ma1")]
        _ma1,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/ma2
        /// </summary>
        [Remark("audio/ma2")]
        _ma2,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/ma3
        /// </summary>
        [Remark("audio/ma3")]
        _ma3,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/ma5
        /// </summary>
        [Remark("audio/ma5")]
        _ma5,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-troff-man
        /// </summary>
        [Remark("application/x-troff-man")]
        _man,

        /// <summary>
        /// 对应ContentType(MIME)类型：magnus-internal/imagemap
        /// </summary>
        [Remark("magnus-internal/imagemap")]
        _map,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/mbedlet
        /// </summary>
        [Remark("application/mbedlet")]
        _mbd,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mascot
        /// </summary>
        [Remark("application/x-mascot")]
        _mct,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-msaccess
        /// </summary>
        [Remark("application/x-msaccess")]
        _mdb,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _mdz,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-troff-me
        /// </summary>
        [Remark("application/x-troff-me")]
        _me,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/x-vmel
        /// </summary>
        [Remark("text/x-vmel")]
        _mel,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mif
        /// </summary>
        [Remark("application/x-mif")]
        _mi,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/midi
        /// </summary>
        [Remark("audio/midi")]
        _mid,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/midi
        /// </summary>
        [Remark("audio/midi")]
        _midi,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mif
        /// </summary>
        [Remark("application/x-mif")]
        _mif,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-cals
        /// </summary>
        [Remark("image/x-cals")]
        _mil,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mio
        /// </summary>
        [Remark("audio/x-mio")]
        _mio,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-skt-lbs
        /// </summary>
        [Remark("application/x-skt-lbs")]
        _mmf,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-mng
        /// </summary>
        [Remark("video/x-mng")]
        _mng,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-msmoney
        /// </summary>
        [Remark("application/x-msmoney")]
        _mny,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mocha
        /// </summary>
        [Remark("application/x-mocha")]
        _moc,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mocha
        /// </summary>
        [Remark("application/x-mocha")]
        _mocha,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _mod,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-yumekara
        /// </summary>
        [Remark("application/x-yumekara")]
        _mof,

        /// <summary>
        /// 对应ContentType(MIME)类型：chemical/x-mdl-molfile
        /// </summary>
        [Remark("chemical/x-mdl-molfile")]
        _mol,

        /// <summary>
        /// 对应ContentType(MIME)类型：chemical/x-mopac-input
        /// </summary>
        [Remark("chemical/x-mopac-input")]
        _mop,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/quicktime
        /// </summary>
        [Remark("video/quicktime")]
        _mov,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-sgi-movie
        /// </summary>
        [Remark("video/x-sgi-movie")]
        _movie,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mpeg
        /// </summary>
        [Remark("audio/x-mpeg")]
        _mp2,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mpeg
        /// </summary>
        [Remark("audio/x-mpeg")]
        _mp3,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/mp4
        /// </summary>
        [Remark("video/mp4")]
        _mp4,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.mpohun.certificate
        /// </summary>
        [Remark("application/vnd.mpohun.certificate")]
        _mpc,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/mpeg
        /// </summary>
        [Remark("video/mpeg")]
        _mpe,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/mpeg
        /// </summary>
        [Remark("video/mpeg")]
        _mpeg,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/mpeg
        /// </summary>
        [Remark("video/mpeg")]
        _mpg,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/mp4
        /// </summary>
        [Remark("video/mp4")]
        _mpg4,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/mpeg
        /// </summary>
        [Remark("audio/mpeg")]
        _mpga,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.mophun.application
        /// </summary>
        [Remark("application/vnd.mophun.application")]
        _mpn,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.ms-project
        /// </summary>
        [Remark("application/vnd.ms-project")]
        _mpp,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mapserver
        /// </summary>
        [Remark("application/x-mapserver")]
        _mps,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/x-mrml
        /// </summary>
        [Remark("text/x-mrml")]
        _mrl,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mrm
        /// </summary>
        [Remark("application/x-mrm")]
        _mrm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-troff-ms
        /// </summary>
        [Remark("application/x-troff-ms")]
        _ms,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/metastream
        /// </summary>
        [Remark("application/metastream")]
        _mts,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/metastream
        /// </summary>
        [Remark("application/metastream")]
        _mtx,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/metastream
        /// </summary>
        [Remark("application/metastream")]
        _mtz,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/metastream
        /// </summary>
        [Remark("application/metastream")]
        _mzv,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/zip
        /// </summary>
        [Remark("application/zip")]
        _nar,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/nbmp
        /// </summary>
        [Remark("image/nbmp")]
        _nbmp,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-netcdf
        /// </summary>
        [Remark("application/x-netcdf")]
        _nc,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-lml/x-ndb
        /// </summary>
        [Remark("x-lml/x-ndb")]
        _ndb,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/ndwn
        /// </summary>
        [Remark("application/ndwn")]
        _ndwn,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-nif
        /// </summary>
        [Remark("application/x-nif")]
        _nif,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-scream
        /// </summary>
        [Remark("application/x-scream")]
        _nmz,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-netfpx
        /// </summary>
        [Remark("application/x-netfpx")]
        _npx,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/nsnd
        /// </summary>
        [Remark("audio/nsnd")]
        _nsnd,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-neva1
        /// </summary>
        [Remark("application/x-neva1")]
        _nva,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/oda
        /// </summary>
        [Remark("application/oda")]
        _oda,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-AtlasMate-Plugin
        /// </summary>
        [Remark("application/x-AtlasMate-Plugin")]
        _oom,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-pac
        /// </summary>
        [Remark("audio/x-pac")]
        _pac,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-epac
        /// </summary>
        [Remark("audio/x-epac")]
        _pae,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-pan
        /// </summary>
        [Remark("application/x-pan")]
        _pan,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-portable-bitmap
        /// </summary>
        [Remark("image/x-portable-bitmap")]
        _pbm,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-pcx
        /// </summary>
        [Remark("image/x-pcx")]
        _pcx,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-pda
        /// </summary>
        [Remark("image/x-pda")]
        _pda,

        /// <summary>
        /// 对应ContentType(MIME)类型：chemical/x-pdb
        /// </summary>
        [Remark("chemical/x-pdb")]
        _pdb,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/pdf
        /// </summary>
        [Remark("application/pdf")]
        _pdf,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/font-tdpfr
        /// </summary>
        [Remark("application/font-tdpfr")]
        _pfr,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-portable-graymap
        /// </summary>
        [Remark("image/x-portable-graymap")]
        _pgm,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-pict
        /// </summary>
        [Remark("image/x-pict")]
        _pict,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-perl
        /// </summary>
        [Remark("application/x-perl")]
        _pm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-pmd
        /// </summary>
        [Remark("application/x-pmd")]
        _pmd,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/png
        /// </summary>
        [Remark("image/png")]
        _png,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-portable-anymap
        /// </summary>
        [Remark("image/x-portable-anymap")]
        _pnm,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/png
        /// </summary>
        [Remark("image/png")]
        _pnz,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.ms-powerpoint
        /// </summary>
        [Remark("application/vnd.ms-powerpoint")]
        _pot,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-portable-pixmap
        /// </summary>
        [Remark("image/x-portable-pixmap")]
        _ppm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.ms-powerpoint
        /// </summary>
        [Remark("application/vnd.ms-powerpoint")]
        _pps,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.ms-powerpoint
        /// </summary>
        [Remark("application/vnd.ms-powerpoint")]
        _ppt,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-cprplayer
        /// </summary>
        [Remark("application/x-cprplayer")]
        _pqf,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/cprplayer
        /// </summary>
        [Remark("application/cprplayer")]
        _pqi,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-prc
        /// </summary>
        [Remark("application/x-prc")]
        _prc,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-ns-proxy-autoconfig
        /// </summary>
        [Remark("application/x-ns-proxy-autoconfig")]
        _proxy,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/postscript
        /// </summary>
        [Remark("application/postscript")]
        _ps,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/listenup
        /// </summary>
        [Remark("application/listenup")]
        _ptlk,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mspublisher
        /// </summary>
        [Remark("application/x-mspublisher")]
        _pub,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-pv-pvx
        /// </summary>
        [Remark("video/x-pv-pvx")]
        _pvx,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/vnd.qcelp
        /// </summary>
        [Remark("audio/vnd.qcelp")]
        _qcp,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/quicktime
        /// </summary>
        [Remark("video/quicktime")]
        _qt,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-quicktime
        /// </summary>
        [Remark("image/x-quicktime")]
        _qti,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-quicktime
        /// </summary>
        [Remark("image/x-quicktime")]
        _qtif,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/vnd.rn-realtext3d
        /// </summary>
        [Remark("text/vnd.rn-realtext3d")]
        _r3t,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-pn-realaudio
        /// </summary>
        [Remark("audio/x-pn-realaudio")]
        _ra,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-pn-realaudio
        /// </summary>
        [Remark("audio/x-pn-realaudio")]
        _ram,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-rar-compressed
        /// </summary>
        [Remark("application/x-rar-compressed")]
        _rar,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-cmu-raster
        /// </summary>
        [Remark("image/x-cmu-raster")]
        _ras,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/rdf+xml
        /// </summary>
        [Remark("application/rdf+xml")]
        _rdf,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/vnd.rn-realflash
        /// </summary>
        [Remark("image/vnd.rn-realflash")]
        _rf,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-rgb
        /// </summary>
        [Remark("image/x-rgb")]
        _rgb,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-richlink
        /// </summary>
        [Remark("application/x-richlink")]
        _rlf,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-pn-realaudio
        /// </summary>
        [Remark("audio/x-pn-realaudio")]
        _rm,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-rmf
        /// </summary>
        [Remark("audio/x-rmf")]
        _rmf,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-pn-realaudio
        /// </summary>
        [Remark("audio/x-pn-realaudio")]
        _rmm,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-pn-realaudio
        /// </summary>
        [Remark("audio/x-pn-realaudio")]
        _rmvb,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.rn-realplayer
        /// </summary>
        [Remark("application/vnd.rn-realplayer")]
        _rnx,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-troff
        /// </summary>
        [Remark("application/x-troff")]
        _roff,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/vnd.rn-realpix
        /// </summary>
        [Remark("image/vnd.rn-realpix")]
        _rp,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-pn-realaudio-plugin
        /// </summary>
        [Remark("audio/x-pn-realaudio-plugin")]
        _rpm,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/vnd.rn-realtext
        /// </summary>
        [Remark("text/vnd.rn-realtext")]
        _rt,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-lml/x-gps
        /// </summary>
        [Remark("x-lml/x-gps")]
        _rte,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/rtf
        /// </summary>
        [Remark("application/rtf")]
        _rtf,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/metastream
        /// </summary>
        [Remark("application/metastream")]
        _rtg,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/richtext
        /// </summary>
        [Remark("text/richtext")]
        _rtx,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/vnd.rn-realvideo
        /// </summary>
        [Remark("video/vnd.rn-realvideo")]
        _rv,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-rogerwilco
        /// </summary>
        [Remark("application/x-rogerwilco")]
        _rwc,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _s3m,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _s3z,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-supercard
        /// </summary>
        [Remark("application/x-supercard")]
        _sca,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-msschedule
        /// </summary>
        [Remark("application/x-msschedule")]
        _scd,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/e-score
        /// </summary>
        [Remark("application/e-score")]
        _sdf,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-stuffit
        /// </summary>
        [Remark("application/x-stuffit")]
        _sea,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/x-sgml
        /// </summary>
        [Remark("text/x-sgml")]
        _sgm,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/x-sgml
        /// </summary>
        [Remark("text/x-sgml")]
        _sgml,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-sh
        /// </summary>
        [Remark("application/x-sh")]
        _sh,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-shar
        /// </summary>
        [Remark("application/x-shar")]
        _shar,

        /// <summary>
        /// 对应ContentType(MIME)类型：magnus-internal/parsed-html
        /// </summary>
        [Remark("magnus-internal/parsed-html")]
        _shtml,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/presentations
        /// </summary>
        [Remark("application/presentations")]
        _shw,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/si6
        /// </summary>
        [Remark("image/si6")]
        _si6,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/vnd.stiwap.sis
        /// </summary>
        [Remark("image/vnd.stiwap.sis")]
        _si7,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/vnd.lgtwap.sis
        /// </summary>
        [Remark("image/vnd.lgtwap.sis")]
        _si9,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.symbian.install
        /// </summary>
        [Remark("application/vnd.symbian.install")]
        _sis,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-stuffit
        /// </summary>
        [Remark("application/x-stuffit")]
        _sit,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-Koan
        /// </summary>
        [Remark("application/x-Koan")]
        _skd,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-Koan
        /// </summary>
        [Remark("application/x-Koan")]
        _skm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-Koan
        /// </summary>
        [Remark("application/x-Koan")]
        _skp,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-Koan
        /// </summary>
        [Remark("application/x-Koan")]
        _skt,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-salsa
        /// </summary>
        [Remark("application/x-salsa")]
        _slc,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-smd
        /// </summary>
        [Remark("audio/x-smd")]
        _smd,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/smil
        /// </summary>
        [Remark("application/smil")]
        _smi,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/smil
        /// </summary>
        [Remark("application/smil")]
        _smil,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/studiom
        /// </summary>
        [Remark("application/studiom")]
        _smp,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-smd
        /// </summary>
        [Remark("audio/x-smd")]
        _smz,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/basic
        /// </summary>
        [Remark("audio/basic")]
        _snd,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/x-speech
        /// </summary>
        [Remark("text/x-speech")]
        _spc,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/futuresplash
        /// </summary>
        [Remark("application/futuresplash")]
        _spl,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-sprite
        /// </summary>
        [Remark("application/x-sprite")]
        _spr,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-sprite
        /// </summary>
        [Remark("application/x-sprite")]
        _sprite,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-spt
        /// </summary>
        [Remark("application/x-spt")]
        _spt,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-wais-source
        /// </summary>
        [Remark("application/x-wais-source")]
        _src,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/hyperstudio
        /// </summary>
        [Remark("application/hyperstudio")]
        _stk,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _stm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-sv4cpio
        /// </summary>
        [Remark("application/x-sv4cpio")]
        _sv4cpio,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-sv4crc
        /// </summary>
        [Remark("application/x-sv4crc")]
        _sv4crc,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/vnd
        /// </summary>
        [Remark("image/vnd")]
        _svf,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/svg-xml
        /// </summary>
        [Remark("image/svg-xml")]
        _svg,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/svh
        /// </summary>
        [Remark("image/svh")]
        _svh,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-world/x-svr
        /// </summary>
        [Remark("x-world/x-svr")]
        _svr,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-shockwave-flash
        /// </summary>
        [Remark("application/x-shockwave-flash")]
        _swf,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-shockwave-flash
        /// </summary>
        [Remark("application/x-shockwave-flash")]
        _swfl,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-troff
        /// </summary>
        [Remark("application/x-troff")]
        _t,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _tad,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/x-speech
        /// </summary>
        [Remark("text/x-speech")]
        _talk,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-tar
        /// </summary>
        [Remark("application/x-tar")]
        _tar,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-tar
        /// </summary>
        [Remark("application/x-tar")]
        _taz,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-timbuktu
        /// </summary>
        [Remark("application/x-timbuktu")]
        _tbp,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-timbuktu
        /// </summary>
        [Remark("application/x-timbuktu")]
        _tbt,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-tcl
        /// </summary>
        [Remark("application/x-tcl")]
        _tcl,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-tex
        /// </summary>
        [Remark("application/x-tex")]
        _tex,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-texinfo
        /// </summary>
        [Remark("application/x-texinfo")]
        _texi,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-texinfo
        /// </summary>
        [Remark("application/x-texinfo")]
        _texinfo,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-tar
        /// </summary>
        [Remark("application/x-tar")]
        _tgz,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.eri.thm
        /// </summary>
        [Remark("application/vnd.eri.thm")]
        _thm,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/tiff
        /// </summary>
        [Remark("image/tiff")]
        _tif,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/tiff
        /// </summary>
        [Remark("image/tiff")]
        _tiff,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-tkined
        /// </summary>
        [Remark("application/x-tkined")]
        _tki,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-tkined
        /// </summary>
        [Remark("application/x-tkined")]
        _tkined,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/toc
        /// </summary>
        [Remark("application/toc")]
        _toc,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/toy
        /// </summary>
        [Remark("image/toy")]
        _toy,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-troff
        /// </summary>
        [Remark("application/x-troff")]
        _tr,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-lml/x-gps
        /// </summary>
        [Remark("x-lml/x-gps")]
        _trk,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-msterminal
        /// </summary>
        [Remark("application/x-msterminal")]
        _trm,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/tsplayer
        /// </summary>
        [Remark("audio/tsplayer")]
        _tsi,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/dsptype
        /// </summary>
        [Remark("application/dsptype")]
        _tsp,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/tab-separated-values
        /// </summary>
        [Remark("text/tab-separated-values")]
        _tsv,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Remark("application/octet-stream")]
        _ttf,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/t-time
        /// </summary>
        [Remark("application/t-time")]
        _ttz,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Remark("text/plain")]
        _txt,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _ult,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-ustar
        /// </summary>
        [Remark("application/x-ustar")]
        _ustar,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-uuencode
        /// </summary>
        [Remark("application/x-uuencode")]
        _uu,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-uuencode
        /// </summary>
        [Remark("application/x-uuencode")]
        _uue,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-cdlink
        /// </summary>
        [Remark("application/x-cdlink")]
        _vcd,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/x-vcard
        /// </summary>
        [Remark("text/x-vcard")]
        _vcf,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/vdo
        /// </summary>
        [Remark("video/vdo")]
        _vdo,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/vib
        /// </summary>
        [Remark("audio/vib")]
        _vib,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/vivo
        /// </summary>
        [Remark("video/vivo")]
        _viv,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/vivo
        /// </summary>
        [Remark("video/vivo")]
        _vivo,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vocaltec-media-desc
        /// </summary>
        [Remark("application/vocaltec-media-desc")]
        _vmd,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vocaltec-media-file
        /// </summary>
        [Remark("application/vocaltec-media-file")]
        _vmf,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-dreamcast-vms-info
        /// </summary>
        [Remark("application/x-dreamcast-vms-info")]
        _vmi,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-dreamcast-vms
        /// </summary>
        [Remark("application/x-dreamcast-vms")]
        _vms,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/voxware
        /// </summary>
        [Remark("audio/voxware")]
        _vox,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-twinvq-plugin
        /// </summary>
        [Remark("audio/x-twinvq-plugin")]
        _vqe,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-twinvq
        /// </summary>
        [Remark("audio/x-twinvq")]
        _vqf,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-twinvq
        /// </summary>
        [Remark("audio/x-twinvq")]
        _vql,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-world/x-vream
        /// </summary>
        [Remark("x-world/x-vream")]
        _vre,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-world/x-vrml
        /// </summary>
        [Remark("x-world/x-vrml")]
        _vrml,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-world/x-vrt
        /// </summary>
        [Remark("x-world/x-vrt")]
        _vrt,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-world/x-vream
        /// </summary>
        [Remark("x-world/x-vream")]
        _vrw,

        /// <summary>
        /// 对应ContentType(MIME)类型：workbook/formulaone
        /// </summary>
        [Remark("workbook/formulaone")]
        _vts,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-wav
        /// </summary>
        [Remark("audio/x-wav")]
        _wav,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-ms-wax
        /// </summary>
        [Remark("audio/x-ms-wax")]
        _wax,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/vnd.wap.wbmp
        /// </summary>
        [Remark("image/vnd.wap.wbmp")]
        _wbmp,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.xara
        /// </summary>
        [Remark("application/vnd.xara")]
        _web,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/wavelet
        /// </summary>
        [Remark("image/wavelet")]
        _wi,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-InstallShield
        /// </summary>
        [Remark("application/x-InstallShield")]
        _wis,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-ms-wm
        /// </summary>
        [Remark("video/x-ms-wm")]
        _wm,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-ms-wma
        /// </summary>
        [Remark("audio/x-ms-wma")]
        _wma,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-ms-wmd
        /// </summary>
        [Remark("application/x-ms-wmd")]
        _wmd,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-msmetafile
        /// </summary>
        [Remark("application/x-msmetafile")]
        _wmf,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/vnd.wap.wml
        /// </summary>
        [Remark("text/vnd.wap.wml")]
        _wml,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.wap.wmlc
        /// </summary>
        [Remark("application/vnd.wap.wmlc")]
        _wmlc,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/vnd.wap.wmlscript
        /// </summary>
        [Remark("text/vnd.wap.wmlscript")]
        _wmls,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.wap.wmlscriptc
        /// </summary>
        [Remark("application/vnd.wap.wmlscriptc")]
        _wmlsc,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/vnd.wap.wmlscript
        /// </summary>
        [Remark("text/vnd.wap.wmlscript")]
        _wmlscript,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-ms-wmv
        /// </summary>
        [Remark("audio/x-ms-wmv")]
        _wmv,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-ms-wmx
        /// </summary>
        [Remark("video/x-ms-wmx")]
        _wmx,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-ms-wmz
        /// </summary>
        [Remark("application/x-ms-wmz")]
        _wmz,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-up-wpng
        /// </summary>
        [Remark("image/x-up-wpng")]
        _wpng,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-lml/x-gps
        /// </summary>
        [Remark("x-lml/x-gps")]
        _wpt,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-mswrite
        /// </summary>
        [Remark("application/x-mswrite")]
        _wri,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-world/x-vrml
        /// </summary>
        [Remark("x-world/x-vrml")]
        _wrl,

        /// <summary>
        /// 对应ContentType(MIME)类型：x-world/x-vrml
        /// </summary>
        [Remark("x-world/x-vrml")]
        _wrz,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/vnd.wap.wmlscript
        /// </summary>
        [Remark("text/vnd.wap.wmlscript")]
        _ws,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.wap.wmlscriptc
        /// </summary>
        [Remark("application/vnd.wap.wmlscriptc")]
        _wsc,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/wavelet
        /// </summary>
        [Remark("video/wavelet")]
        _wv,

        /// <summary>
        /// 对应ContentType(MIME)类型：video/x-ms-wvx
        /// </summary>
        [Remark("video/x-ms-wvx")]
        _wvx,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-wxl
        /// </summary>
        [Remark("application/x-wxl")]
        _wxl,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.xara
        /// </summary>
        [Remark("application/vnd.xara")]
        _xar,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-xbitmap
        /// </summary>
        [Remark("image/x-xbitmap")]
        _xbm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-xdma
        /// </summary>
        [Remark("application/x-xdma")]
        _xdm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-xdma
        /// </summary>
        [Remark("application/x-xdma")]
        _xdma,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.fujixerox.docuworks
        /// </summary>
        [Remark("application/vnd.fujixerox.docuworks")]
        _xdw,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/xhtml+xml
        /// </summary>
        [Remark("application/xhtml+xml")]
        _xht,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/xhtml+xml
        /// </summary>
        [Remark("application/xhtml+xml")]
        _xhtm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/xhtml+xml
        /// </summary>
        [Remark("application/xhtml+xml")]
        _xhtml,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Remark("application/vnd.ms-excel")]
        _xla,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Remark("application/vnd.ms-excel")]
        _xlc,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-excel
        /// </summary>
        [Remark("application/x-excel")]
        _xll,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Remark("application/vnd.ms-excel")]
        _xlm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Remark("application/vnd.ms-excel")]
        _xls,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Remark("application/vnd.ms-excel")]
        _xlt,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Remark("application/vnd.ms-excel")]
        _xlw,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _xm,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/xml,text/xml
        /// </summary>
        [Remark("application/xml,text/xml")]
        _xml,

        /// <summary>
        /// 对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Remark("audio/x-mod")]
        _xmz,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-xpinstall
        /// </summary>
        [Remark("application/x-xpinstall")]
        _xpi,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-xpixmap
        /// </summary>
        [Remark("image/x-xpixmap")]
        _xpm,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/xml
        /// </summary>
        [Remark("text/xml")]
        _xsit,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/xml
        /// </summary>
        [Remark("text/xml")]
        _xsl,

        /// <summary>
        /// 对应ContentType(MIME)类型：text/xul
        /// </summary>
        [Remark("text/xul")]
        _xul,

        /// <summary>
        /// 对应ContentType(MIME)类型：image/x-xwindowdump
        /// </summary>
        [Remark("image/x-xwindowdump")]
        _xwd,

        /// <summary>
        /// 对应ContentType(MIME)类型：chemical/x-pdb
        /// </summary>
        [Remark("chemical/x-pdb")]
        _xyz,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-yz1
        /// </summary>
        [Remark("application/x-yz1")]
        _yz1,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-compress
        /// </summary>
        [Remark("application/x-compress")]
        _z,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/x-zaurus-zac
        /// </summary>
        [Remark("application/x-zaurus-zac")]
        _zac,

        /// <summary>
        /// 对应ContentType(MIME)类型：application/zip
        /// </summary>
        [Remark("application/zip")]
        _zip,

    }
}
