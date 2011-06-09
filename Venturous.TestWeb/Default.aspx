<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Venturous.TestWeb._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:LinkButton runat=server ID="btnServerError" OnClick="btnServerError_Click" Text="Server Error" />
    <div>
        <div id="myList">
            <ul>
                <li resKey="mnu1"><a href="#">My list 1</a></li>
                <ul>
                    <li resKey="mnuItem1"><a href="http://www.gulesider.no/">Item1</a></li>
                    <li resKey="mnuItem2"><a href="#">Item2</a></li>
                </ul>
            </ul>
        </div>

        <div id="topMenu">
            <ul>
                <li resKey="mnu1"><a href="#">Searching</a></li>
                <ul>
                    <li resKey="mnuItem1"><a href="http://www.google.no/">Google</a></li>
                    <li resKey="mnuItem2"><span onclick="javascript:document.location = 'http://www.bing.com'">Bing</span></li>
                </ul>
                <li resKey="mnu2"><a href="#">Menu2</a></li>
                <ul>
                    <li resKey="mnuItem3"><a href="#">Item3</a></li>
                    <li resKey="mnuItem4"><a href="#">Item4</a></li>
                </ul>
                <li resKey="mnu3"><a href="#">Menu3</a></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
