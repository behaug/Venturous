<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextAndValue.aspx.cs" Inherits="Venturous.TestWeb.TextAndValue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span id="span1">text</span><br />
        <input id="text1" type="text" value="value" /><br />
        <input id="text2" value="value" /><br />
        <input id="check1" type="checkbox" checked="checked" value="value" /><br />
        <input id="radio1"  type="radio" value="value" /><br />
        <input id="button1" type="button" value="value" /><br />
        <textarea id="textarea1">text</textarea><br />

        <input id="text_disabled" type="text" value="value" disabled="disabled" /><br />
        <input id="button_disabled" type="submit" value="value" disabled="disabled" /><br />
    </div>
    </form>
</body>
</html>
