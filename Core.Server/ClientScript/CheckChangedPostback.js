/*
此脚本用于使CheckBox控件能够在勾选状态更改时自动回发数据到服务器
通常将其应用于TreeView等控件，使其子控件中的复选框能够具有回发功能
示例：
TreeView1.Attributes.Add("onclick", "postBackByObject()");
*/
function postBackByObject() {
    var o = window.event.srcElement;
    if (o.tagName == "INPUT" && o.type == "checkbox") {
        __doPostBack("", "");
    }
} 