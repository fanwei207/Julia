<%@ Page Language="C#" AutoEventWireup="true" CodeFile="productCheckSizeImport.aspx.cs"
    Inherits="product_productCheckSizeImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="780" bgcolor="white" border="0" style="margin: 0 auto;">
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table style="width: 700px; height: 140px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>�����ļ��ĸ�ʽ���£�</b><br>
                                    <br>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="20%" style="height: 19px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>��Ʒ�ͺ�</b></font>
                                            </td>
                                            <td style="width: 100px; height: 19px;" width="184">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>����(kg)</b></font>
                                            </td>
                                            <td style="width: 100px; height: 19px;" width="189">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>���(m3)</b></font>
                                            </td>
                                            <td style="width: 100px; height: 19px;" width="189">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>��(cm)</b></font>
                                            </td>
                                            <td style="width: 100px; height: 19px;" width="189">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>��(cm)</b></font>
                                            </td>
                                            <td style="width: 100px; height: 19px;" width="189">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>��(cm)</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td style="height: 17px">
                                                <font style="background-color: #eceef7" color="#000000">DXTXD-4-5</font>
                                            </td>
                                            <td style="height: 17px">
                                                <font style="background-color: #eceef7" color="#000000">2</font>
                                            </td>
                                            <td style="height: 17px">
                                                <font style="background-color: #eceef7" color="#000000">0.0064</font>
                                            </td>
                                            <td style="height: 17px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>10</b></font>
                                            </td>
                                            <td style="height: 17px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>20</b></font>
                                            </td>
                                            <td style="height: 17px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>32</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td style="height: 17px">
                                                <font style="background-color: #eceef7" color="#000000"><font style="background-color: #eceef7"
                                                    color="#000000">DXTXD-4-6</font></font>
                                            </td>
                                            <td style="height: 17px">
                                                <font style="background-color: #eceef7" color="#000000">3</font>
                                            </td>
                                            <td style="height: 17px">
                                                <font style="background-color: #eceef7" color="#000000">0.014</font>
                                            </td>
                                            <td style="height: 17px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>20</b></font>
                                            </td>
                                            <td style="height: 17px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>20</b></font>
                                            </td>
                                            <td style="height: 17px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>35</b></font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9">
                                                <b>ע�����������Ҳ��������ߡ���ͬʱ����ʱ��ȡ�����</b>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </font>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="2" cellpadding="2" width="780" bgcolor="white" border="0">
                        <tr>
                            <td align="right" width="90">
                                �ļ�����: &nbsp;
                            </td>
                            <td valign="top" width="500" colspan="2">
                                <asp:DropDownList ID="dropFileType" runat="server" AutoPostBack="True" Width="300px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td height="5">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="90">
                                �����ļ�: &nbsp;
                            </td>
                            <td valign="top" width="500" colspan="2">
                                <input id="excelFile" style="width: 487px; height: 22px" type="file" size="45" name="filename"
                                    runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td height="5">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="90">
                                <font size="3">���أ�</font>
                            </td>
                            <td align="left" width="135">
                                <label id="here" onclick="submit();">
                                    <a href="/docs/productCheckSizeTemplate.xls" target="blank"><font color="blue">��Ʒ����ߴ絼���ģ��</font></a></label>
                            </td>
                            <td align="center">
                                <asp:Button ID="btnImport" Text="��Ʒ����ߴ絼��" runat="server" CssClass="SmallButton2"
                                    Width="120px" OnClick="btnImport_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
