using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Popups;

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

        public static async Task 显示基本提示对话框(string 内容, string 标题 = "提示", string 按钮文字 = "确定")
        {
            MessageDialog messageDialog = new MessageDialog(内容, 标题);
            messageDialog.Commands.Add(new UICommand(按钮文字, cmd => { }, 按钮文字));
            messageDialog.DefaultCommandIndex = 0;
            await messageDialog.ShowAsync();
        }

        public static async Task<bool> 显示基本确认对话框(string 内容, string 标题 = "提示", string 确认按钮文字 = "确定",string 取消按钮文字= "取消")
        {
            MessageDialog messageDialog = new MessageDialog(内容, 标题);
            messageDialog.Commands.Add(new UICommand(确认按钮文字, cmd => { }, 确认按钮文字));
            messageDialog.Commands.Add(new UICommand(取消按钮文字, cmd => { }, 取消按钮文字));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            IUICommand result = await messageDialog.ShowAsync();
            return result.Id as string == 确认按钮文字;
        }
    }
}
