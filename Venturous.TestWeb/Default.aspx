<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Venturous.TestWeb._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <asp:LinkButton runat=server ID="btnServerError" OnClick="btnServerError_Click" Text="Server Error" />
    <p />
    <a id="open_window" href="#" onclick="window.open('framecontent.htm','Inside','toolbar=no,resizable=yes,fullscreen=no,scrollbars=yes,statusbar=no,menubar=no')">Open Window</a>
    <p />

    <select id="dropdown" onchange="document.getElementById('dropdown_value').innerHTML = document.getElementById('dropdown').value">
        <option value="item1">Item 1</option>
        <option value="item2">Item 2</option>
        <option value="item3">Item 3</option>
    </select>
    <span id="dropdown_value"></span>
    <p />

    <select id="listbox" size="5" onchange="document.getElementById('listbox_value').innerHTML = document.getElementById('listbox').value">
        <option value="item1">Item 1</option>
        <option value="item2">Item 2</option>
        <option value="item3">Item 3</option>
    </select>
    <span id="listbox_value"></span>
    <p />

    <input type="text" id="txtInput" />
    <p />

    <script type="text/javascript">
        function showAllSelected() {
            var val = document.getElementById('multiselect_value');
            var sel = document.getElementById('multiselect');
            var vals = [];
            for (var i = 0; i < sel.options.length; i++) {
                var opt = sel.options[i];
                if (opt.selected) vals.push(opt.value);
            }
            val.innerHTML = vals.join();
        }
    </script>
    <select id="multiselect" size="5" multiple="multiple" onchange="javscript:showAllSelected();">
        <option value="item1">Item 1</option>
        <option value="item2">Item 2</option>
        <option value="item3">Item 3</option>
    </select>
    <span id="multiselect_value"></span>
    <p />

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
