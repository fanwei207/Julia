<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.docitemimport" CodeFile="qad_docitemimport.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="6" width="900" class="main_import">
            <tr>
                <td colspan="3" class="main_import_img">
                    <b>文件导入</b>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90; border-top: 2px solid #e5e5e5;">
                    File Type: &nbsp;
                </td>
                <td valign="top" colspan="2" style="width: 500px; border-top: 2px solid #e5e5e5;">
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
                    <input id="filename1" style="width: 400px; height: 22px" type="file" size="45" name="filename1"
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
                <td align="left" width="150">
                    <label id="here" onclick="submit();">
                        <a href="/docs/docitemimport.xls" target="blank"><font color="blue">Template</font></a></label>
                </td>
                <td align="left">
                    <input class="SmallButton2" id="uploadPartBtn" style="width: 120px" type="button"
                        value="Import" name="uploadPartBtn" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table style="width: 900px; height: 100px" cellpadding="0" class="main_import_fieldset">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend><b>Data Format:</b></legend>
                                    <br>
                                    <table height="60" cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="4%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>No.</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>Category</b></font>
                                            </td>
                                            <td width="15%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>Type</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>DocName</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>QAD Item</b></font>
                                            </td>
                                        </tr>
                                        <tr class="fieldset_exp">
                                            <td width="4%">
                                                1<font color="#cc0000"></font>
                                            </td>
                                            <td width="10%">
                                                产品技术标准<font color="#cc0000"></font>
                                            </td>
                                            <td width="15%">
                                                外销120V产品技术标准<font color="#cc0000"></font>
                                            </td>
                                            <td width="10%">
                                                SZX-JB-N-001<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                10000000000000<font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                    </table>
                                    </font>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
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
