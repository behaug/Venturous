﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdPanel.aspx.cs" Inherits="Venturous.TestWeb.UpdPanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id='panelDiv'>
                    <asp:Label runat=server ID="lblText" Text="Not clicked yet"></asp:Label>
                    <asp:Button runat=server ID="btnTest" OnClick="btnTest_Click" Text="Click on me" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
