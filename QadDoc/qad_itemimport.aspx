<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_itemimport" CodeFile="qad_itemimport.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="780" bgcolor="white" border="0">
            <tr>
                <td align="center">
                    <table style="width: 800px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>Data format：</b><br>
                                    <table height="60" cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="7%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>QAD Item</b></font>
                                            </td>
                                            <td width="12%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>Status</b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>Description</b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>QAD Desc1</b></font>
                                            </td>
                                            <td width="20%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>QAD Desc2</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="7%">
                                                11751765000990<font color="#cc0000"></font>
                                            </td>
                                            <td width="7%">
                                                Normal<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                T8 17W 6500K...<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                TCP-31017765 <font color="#cc0000"></font>
                                            </td>
                                            <td width="20%">
                                                <font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                    </table>
                                </font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2" width="800px" bgcolor="white" border="0">
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
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="right" width="90">
                    File: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <input id="filename1" style="width: 487px; height: 22px" type="file" size="45" name="filename1"
                        runat="server"><br>
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
                        <a href="/docs/qad_itemimport.xls" target="blank"><font color="blue">Template</font></a></label>
                </td>
                <td align="center">
                <asp:Button ID="btnImport" runat="server" Text="Import" CssClass="SmallButton2" />
                 <%--   <input class="SmallButton2" id="uploadPartBtn" style="width: 120px" type="button"
                        value="Import" name="uploadPartBtn" runat="server" />--%>
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
