using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace Core.UWP
{
    // ReSharper disable once InconsistentNaming
    public static class UWP处理函数
    {
        public static void 复制到剪切板(string 内容)
        {
            DataPackage dataPackage = new DataPackage { RequestedOperation = DataPackageOperation.Copy };
            dataPackage.SetText(内容);
            Clipboard.SetContent(dataPackage);
        }
    }
}
