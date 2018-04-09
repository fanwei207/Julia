<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Page_Det.aspx.cs" Inherits="Page_Det" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        function isInt(str) {
            var reg1 = /^\d+$/;
            return reg1.test(str);
        }
        //行
        $(function () {
            $("#txtRowSpan").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("行合并必须为正数！");
                    $(this).val("");
                }
            });
        })
        //列
        $(function () {
            $("#txtColSpan").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("列合并必须为正数！");
                    $(this).val("");
                }
            });
        })
        //字段长度
        $(function () {
            $("#txtDataLength").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("字段长度必须为正数！");
                    $(this).val("");
                }
            });
        })
        //行索引
        $(function () {
            $("#txtRowIndex").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("列合并必须为正数！");
                    $(this).val("");
                }
            });
        })
        //列索引
        $(function () {
            $("#txtColIndex").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("索引必须为正数！");
                    $(this).val("");
                }
            });
        })
        //行高度
        $(function () {
            $("#txtRowHeight").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("行高度必须为正数！");
                    $(this).val("");
                }
            });
        })
        //列宽度
        $(function () {
            $("#txtColWidth").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("列合并必须为正数！");
                    $(this).val("");
                }
            });
        })
        //查询条件顺序
        $(function () {
            $("#txtQueryIndex").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("查询条件顺序必须为正数！");
                    $(this).val("");
                }
            });
        })
        //查询框宽度
        $(function () {
            $("#txtQueryWidht").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("查询框宽度必须为正数！");
                    $(this).val("");
                }
            });
        })
        //GridView
        $(function () {
            $("#txtGridWidth").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("GridView必须为正数！");
                    $(this).val("");
                }
            });
        })
        //导入索引
        $(function () {
            $("#txtImportIndex").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("导入索引必须为正数！");
                    $(this).val("");
                }
            });
        })
        //Excel宽度
        $(function () {
            $("#txtExportWidth").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("Excel宽度必须为正数！");
                    $(this).val("");
                }
            });
        })
        //导出顺序
        $(function () {
            $("#txtExportIndex").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("导出顺序必须为正数！");
                    $(this).val("");
                }
            });
        })
        //排序
        $(function () {
            $("#txtOrderIndex").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("排序顺序必须为正数！");
                    $(this).val("");
                }
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style=" width:800px;">
            <tr>
                <td style="text-align: left;" colspan="8">
                    <asp:Label ID="lbDb" runat="server" Font-Bold="True" Text="WorkFlow"></asp:Label>
                    .
                    <asp:Label ID="lbTable" runat="server" Font-Bold="True" Text="Page_Det"></asp:Label>
                    .
                    <asp:Label ID="lbColunm" runat="server" Font-Bold="True" Text="pd_pageID"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 50px;">
                    &nbsp;</td>
                <td style="text-align: left; " colspan="7">
                    <u>Infomation</u>：
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left; width:50px;">
                </td>
                <td style="text-align: left;">
                    Label:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtLabel" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Data Type:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDataType" runat="server" Width="100px" Enabled="False" BackColor="#F3F3F3"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Data Length:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDataLength" runat="server" Width="100px" Enabled="False" BackColor="#F3F3F3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Primary Key:
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkPKey" runat="server" Enabled="False" />
                </td>
                <td style="text-align: left;">
                    Foreign Key:
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkFKey" runat="server" Enabled="False" />
                </td>
                <td style="text-align: left;"></td>
                <td style="text-align: left;"></td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
                <td style="text-align: left;">
                    Is Null:</td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkIsNull" runat="server" Enabled="False" />
                </td>
                <td style="text-align: left;">
                    Identify:</td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkIsIdentify" runat="server" Enabled="False" />
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
                <td style="text-align: left;">
                    Control:
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="dropControl" runat="server" Width="100px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>TextBox</asp:ListItem>
                        <asp:ListItem>CheckBox</asp:ListItem>
                        <asp:ListItem>TextArea</asp:ListItem>
                        <asp:ListItem>RadioBox</asp:ListItem>
                        <asp:ListItem>DropDownList</asp:ListItem>
                        <asp:ListItem>FileUpload</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left;">
                    Ctr Value:
                </td>
                <td style="text-align: left;" colspan="3">
                    <asp:TextBox ID="txtControlValue" runat="server" Width="97%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;"></td>
                <td style="text-align: left;">
                    Default Value:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDefValue" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">Format String:</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtFormatStr" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">CSS Class:</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtCssClass" runat="server"  Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;" colspan="7">
                    <u>Newing</u>：
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Can Add:
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkAdd" runat="server" />
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Row Index:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtRowIndex" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Row Span:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtRowSpan" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Row Height:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtRowHeight" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Col Index:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtColIndex" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Col Span:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtColSpan" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Col Width:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtColWidht" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;" colspan="6">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;" colspan="7">
                    <u>Editting</u>：
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Can Edit:
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkEdit" runat="server" />
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;" colspan="7">
                    <u>Grid Search Condition</u>：
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Show In:
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkQuery" runat="server" />
                </td>
                <td style="text-align: left;">
                    Location:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtQueryIndex" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Width:</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtQueryWidht" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;"></td>
                <td style="text-align: left;"></td>
                <td style="text-align: left;">
                    Visible:
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkVisible" runat="server" />
                </td>
                <td style="text-align: left;"></td>
                <td style="text-align: left;"></td>
                <td style="text-align: left;"></td>
                <td style="text-align: left;"></td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;" colspan="7">
                    <u>Grid View</u>：
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Show In:
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkGrid" runat="server" />
                </td>
                <td style="text-align: left;">
                    Width(<em>px</em>):
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtGridWidth" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Align:</td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="dropGridAlign" runat="server" Width="100px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>Left</asp:ListItem>
                        <asp:ListItem>Center</asp:ListItem>
                        <asp:ListItem>Right</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">Format String:</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtFormatValue" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Has Link:</td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkLink" runat="server" />
                </td>
                <td style="text-align: left;">
                    Custom:</td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkIsCustom" runat="server" />
                </td>
                <td style="text-align: left;">
                    Title:</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtLinkTitle" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Page/Url:</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtLinkPage" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Params:</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtLinkParams" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Target:</td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="dropLinkTarget" runat="server" Width="100px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>_self</asp:ListItem>
                        <asp:ListItem>_blank</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;" colspan="7">
                    <u>Importing</u>：
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Can Import:
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkImport" runat="server" />
                </td>
                <td style="text-align: left;">
                    Cell Index:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtImportIndex" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;" colspan="7">
                    <u>Exporting</u>：
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Can Export:
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkExport" runat="server" />
                </td>
                <td style="text-align: left;">
                    Width(<em>px</em>):
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtExportWidth" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Cell Index:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtExportIndex" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;" colspan="7">
                    <u>Order By</u>：
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                    Show In:
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="chkOrderBy" runat="server" />
                </td>
                <td style="text-align: left;">
                    Order Index:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtOrderIndex" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;</td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;" colspan="2">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="DONE" Width="100px" OnClick="btnSave_Click" />
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
