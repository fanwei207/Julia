<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_FormEdit.aspx.cs" Inherits="WF_ExcelEdit"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ScreenSpliter(header, footer, middle) {
            this._header = document.getElementById(header);
            this._footer = document.getElementById(footer);
            this._middle = document.getElementById(middle);

            document.body.style.margin = "0px";
            document.body.style.overflow = "hidden";
            this._middle.style.overflow = "auto";
            this.resize(null);
            registerEventHandler(window, 'resize', getInstanceDelegate(this, "resize"));
        }
        function load_XmlDocumentFromElement(Id) {
            var hf = document.getElementById(Id);
            if (hf != null) {
                var xmldoc = new ActiveXObject("Microsoft.XMLDOM");
                xmldoc.loadXML(hf.value);
                return xmldoc;
            }
            return null;
        }

        function get_NodeAttributeText(root, node_name, attr_name) {
            var node = root.selectSingleNode(node_name);
            if (node != null)
                return node.getAttribute(attr_name);
            else
                return null;
        }

        function set_ExcelDisplayMode(sheet) {
            sheet.AllowPropertyToolbox = false;
            sheet.DisplayToolbar = true;
            sheet.DisplayOfficeLogo = false;
            sheet.DisplayWorkbookTabs = false;
            sheet.DisplayTitleBar = false;
        }

        function set_ProtectModeForEdit(sheet) {
            var protection = sheet.ActiveSheet.Protection;
            protection.AllowInsertingRows = true;
            protection.AllowDeletingRows = true;
            protection.AllowFormattingColumns = true;
            protection.AllowSorting = true;
            protection.Enabled = true;

            sheet.activeWindow.enableResize = false;
        }

        function set_ProtectModeForBrowse(sheet) {
            var protection = sheet.ActiveSheet.Protection;
            protection.AllowFormattingRows = true;
            protection.AllowFormattingColumns = true;
            protection.AllowDeletingRows = false;
            protection.AllowInsertingRows = false;
            protection.AllowInsertingColumns = false;
            protection.AllowSorting = false;
            protection.Enabled = true;

            sheet.activeWindow.enableResize = false;
        }

        function get_SheetXmlData() {
            var sheet = document.getElementById("_obj_Excel");
            sheet.ActiveSheet.Unprotect(); //去除保护

            var xmldoc = load_XmlDocumentFromElement("<%= _hf_ExcelSetting.ClientID%>");
            if (xmldoc != null) {
                var temp = get_NodeAttributeText(xmldoc.lastChild, "ClearContents", "cols");
                if (temp != null) {
                    var range = sheet.ActiveSheet.Columns(temp);
                    if (range != null)
                        range.ClearContents();
                }
            }

            var hf = document.getElementById("<%= _hf_ExcelXmlData.ClientID%>");
            hf.value = sheet.XMLData;
        }

        function set_SheetXmlData() {
            var sheet = document.getElementById("_obj_Excel");

            var hf = document.getElementById("<%= _hf_ExcelXmlData.ClientID%>");
            if (hf.value.length < 10) {
                sheet.style.display = "none";
                var button = document.getElementById("BtnSave");
                button.disabled = true;
            }
            else {
                sheet.XMLData = hf.value;
                hf.value = "";
            }

            var xmldoc = load_XmlDocumentFromElement("<%= _hf_ExcelSetting.ClientID%>");
            if (xmldoc != null) {
                var temp = get_NodeAttributeText(xmldoc.lastChild, "Viewable", "cols");
                if (temp != null)
                    sheet.ViewableRange = temp;
            }


            set_ProtectModeForEdit(sheet);
            set_ExcelDisplayMode(sheet);
        }

        window.onload = function () {
            set_SheetXmlData();
        }
    </script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <div style="width: 900px; height: 500px">
            <table border="0" cellpadding="2" cellspacing="2" style="width: 900px">
                <tr>
                    <td align="left" style="width: 550px">
                        <span style="color: black">如果页面无法显示，请安装</span><asp:HyperLink ID="hlOWC11" runat="server"
                            NavigateUrl="~/docs/owc11.rar" Font-Underline="True" ForeColor="#0000C0">OWC11</asp:HyperLink>，安装后如果还显示不了，请联系管理员!
                    </td>
                    <td align="right">
                        <asp:Button ID="BtnSave" runat="server" CssClass="SmallButton3" OnClientClick="get_SheetXmlData();"
                            OnClick="BtnSave_Click" Text="保存" Width="50px"/>
                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<asp:Button ID="BtnClose" runat="server" CausesValidation="true"
                            CssClass="SmallButton3" OnClick="BtnClose_Click" Text="关闭" Width="50px"/>
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <object id="_obj_Excel" classid="clsid:0002E559-0000-0000-C000-000000000046" width="100%"
                height="500px" standby="Loading">
            </object>
        </div>
        <asp:HiddenField ID="_hf_ExcelSetting" runat="server" />
        <asp:HiddenField ID="_hf_ExcelXmlData" runat="server" />
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
