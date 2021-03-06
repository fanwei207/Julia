<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_documentimport"
    CodeFile="qad_documentimport.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        #table_temp td
        {
            border: 0.5px solid grey;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table>
            <tr>
                <td align="left">
                    <div style="font-size: 12px; background-color: #edeef7; font-weight: bold;">
                        Data Format(Example)：</div>
                    <table id="table_temp" height="60" cellspacing="0" cellpadding="2" width="100%" border="1">
                        <tr>
                            <td>
                                No.
                            </td>
                            <td>
                                Category
                            </td>
                            <td>
                                Type
                            </td>
                            <td>
                                DocName
                            </td>
                            <td style="width: 53px">
                                Version
                            </td>
                            <td style="width: 76px">
                                Description
                            </td>
                            <td style="width: 6px">
                                DocLevel
                            </td>
                            <td>
                                Stop
                            </td>
                            <td>
                                isPublic
                            </td>
                            <td style="width: 90px">
                                For all items
                            </td>
                            <td style="width: 131px">
                                FileName
                            </td>
                            <td>
                                图号
                            </td>
                            <td>
                                accFileName
                            </td>
                        </tr>
                        <tr style="background-color: #edeef7; color: #0000ff;">
                            <td style="height: 43px">
                                1
                            </td>
                            <td style="height: 43px">
                                包装图纸
                            </td>
                            <td style="height: 43px">
                                彩盒
                            </td>
                            <td style="height: 43px">
                                CH-1049
                            </td>
                            <td style="width: 53px; height: 43px;">
                                1
                            </td>
                            <td style="width: 76px; height: 43px;">
                                41681 33109SP彩盒400G
                            </td>
                            <td style="width: 6px; height: 43px;">
                                3
                            </td>
                            <td style="height: 43px">
                                0
                            </td>
                            <td style="height: 43px">
                                0
                            </td>
                            <td style="width: 90px; height: 43px;">
                                0
                            </td>
                            <td style="width: 131px; height: 43px;">
                                CH-1049-V001.pdf
                            </td>
                            <td style="height: 43px">
                                33109SP
                            </td>
                            <td style="height: 43px">
                                CH-1049-V001.ai
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2" width="900" bgcolor="white" border="0" id="TABLE1"
            onclick="return TABLE1_onclick()">
            <tr>
                <td align="right" width="90">
                    File Type: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <asp:DropDownList ID="FileTypeDropDownList1" runat="server" AutoPostBack="True" Width="300px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right"  height="5">Type:
                </td>
                <td>
                    <asp:CheckBox ID="updata" runat="server" Text="版本升级" />
                </td>
            </tr>
            <tr>
                <td align="right" width="90">
                    File: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <input id="filename1" style="width: 487px; height: 22px" type="file" size="45" name="filename1"
                        runat="server" /><br />
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="right" width="90">
                    Download:
                </td>
                <td align="left" width="135">
                    <label id="here" onclick="submit();">
                        <a href="/docs/qaddocimport.xls" target="blank"><font color="blue">Template</font></a></label>
                </td>
                <td align="center">
                    &nbsp;<asp:Button ID="btnImport" runat="server" CssClass="SmallButton2" Text="Import" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
	 <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
