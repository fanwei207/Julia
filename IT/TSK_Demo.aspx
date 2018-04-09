<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_Demo.aspx.cs" Inherits="IT_TSK_Demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.obj.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $(".j-demo-portlet").draggable({
                helper: "clone"
            });
            //初始化:先清除WaitSaving标记
            $(".j-demo-waitsaving").removeClass("j-demo-waitsaving");
            $(".SelectedCell").removeClass("SelectedCell");

            $(".j-demo-textbox").each(function () {
                $(this).TextBox({ containment: "#demoHolder" });
            })
            $(".j-demo-button").each(function () {
                $(this).Button({ containment: "#demoHolder" });
            })
            $(".j-demo-dropdownlist").each(function () {
                $(this).DropDownList({ containment: "#demoHolder" });
            })
            $(".j-demo-checkbox").each(function () {
                $(this).CheckBox({ containment: "#demoHolder" });
            })
            $(".j-demo-radiobox").each(function () {
                $(this).RadioBox({ containment: "#demoHolder" });
            })
            $(".j-demo-gridview").each(function () {
                $(this).GridView({ containment: "#demoHolder" });
            })
            $(".j-demo-fileupload").each(function () {
                $(this).FileUpload({ containment: "#demoHolder" });
            })
            $(".j-demo-tabs").each(function () {
                $(this).Tabs({ containment: "#demoHolder" });
            })
            $(".j-demo-textArea").each(function () {
                $(this).TextArea({ containment: "#demoHolder" });
            })
            //拖拽
            $("#demoHolder").droppable({
                accept: ".j-demo-portlet",
                drop: function (event, ui) {
                    var _text = ui.draggable.text().replace(/^\s+|\s+$/g, "");
                    _text = _text.replace("\n", "");
                    if (_text == "TextBox") {
                        objDemo.TextBox().TextBox({ containment: "#demoHolder" }).appendTo($(this));
                    } else if (_text == "Button") {
                        objDemo.Button().Button({ containment: "#demoHolder" }).appendTo($(this));
                    } else if (_text == "DropDownList") {
                        objDemo.DropDownList().DropDownList({ containment: "#demoHolder" }).appendTo($(this));
                    } else if (_text == "GridView") {
                        objDemo.GridView().GridView({ containment: "#demoHolder" }).appendTo($(this));
                    } else if (_text == "CheckBox") {
                        objDemo.CheckBox().CheckBox({ containment: "#demoHolder" }).appendTo($(this));
                    } else if (_text == "RadioBox") {
                        objDemo.RadioBox().RadioBox({ containment: "#demoHolder" }).appendTo($(this));
                    } else if (_text == "FileUpload") {
                        objDemo.FileUpload().FileUpload({ containment: "#demoHolder" }).appendTo($(this));
                    } else if (_text == "Tabs") {
                        objDemo.Tabs().Tabs({ containment: "#demoHolder" }).appendTo($(this));
                    } else if (_text == "TextArea") {
                        objDemo.TextArea().TextArea({ containment: "#demoHolder" }).appendTo($(this));
                    }
                }
            });
            //end droppable

            //end 属性编辑txtConfigWidth

            $("#btnSave").click(function () {
                var _html = $("#demoHolder").html();
                _html = _html.replace(/</g, "&lt;");
                _html = _html.replace(/>/g, "&gt;");
                $("#hidDemoHtml").val(_html);
            });
            //end click

        })
    </script>
    <style type="text/css">
        .j-demo-portlet
        {
            cursor: pointer;
        }
        .j-demo-waitsaving
        {
            border: 2px dotted red;
        }
        .j-demo-tabitem
        {
            background-color: silver;
        }
        .j-demo-tabactive
        {
            background-color: #fff;
        }
        #tblConfig TD
        {
            border-top: 1px solid #000;
            border-left: 1px solid #000;
            border-right: 1px solid #000;
        }
        #demoList
        {
            list-style-type: none;
            margin: 0;
            padding: 0;
        }
        #demoList li
        {
            margin: 0 3px 3px 3px;
            padding: 0.4em;
            font-size: 1.4em;
            border: 1px solid #d3d3d3;
            background-color: #e6e6e6;
            font-weight: normal;
            color: #555555;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 550px;">
        <tr>
            <td style="width: 150px; vertical-align: top; text-align: center; border-right: 1px solid #000;">
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /><br />
                <br />
                <ul id="demoList">
                    <li><span class="j-demo-portlet">TextBox</span></li>
                    <li><span class="j-demo-portlet">Button</span></li>
                    <li><span class="j-demo-portlet">GridView</span></li>
                    <li><span class="j-demo-portlet">DropDownList</span></li>
                    <li><span class="j-demo-portlet">CheckBox</span></li>
                    <li><span class="j-demo-portlet">RadioBox</span></li>
                    <li><span class="j-demo-portlet">Tabs</span></li>
                    <li><span class="j-demo-portlet">FileUpload</span></li>
                    <li><span class="j-demo-portlet">TextArea</span></li>
                </ul>
                <input id="hidDemoHtml" type="hidden" runat="server" />
            </td>
            <td id="demoHolder" style="vertical-align: top; border: 1px solid #000" runat="server">
            </td>
            <td id="demoConfig" style="width: 200px; vertical-align: top; border-left: 1px solid #000;">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
