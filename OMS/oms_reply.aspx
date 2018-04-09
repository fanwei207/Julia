<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oms_reply.aspx.cs" Inherits="OMS_oms_reply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <%-- <style type="text/css">
        #table2
        {
            width: 400px;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbCustCode" runat="server" Text="" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbCustName" runat="server" Text="" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtCustomer" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style=" text-align:left;">
                    <asp:Label ID="lblmatr" runat="server" Text="SUBJECT(Less than 150 Words):"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtmstr" runat="server" Height="60px" TextMode="MultiLine" 
                        Width="100%" MaxLength="80"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style=" text-align:left;">
                    REPLY:</td>
                </tr>
                <tr>
                    <td style="width: 700px; text-align:left;">
                        <input id="file1" style="width: 100%; height: 23px" type="file" size="45" name="filename1"
                            runat="server" />
                    </td>
            </tr>
            <tr>
                <td style="width: 700px; text-align:left;">
                    &nbsp;Message:<asp:TextBox ID="txt_tracking" runat="server" Height="200px" TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle; text-align: center;">
                    <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="SmallButton2"
                        OnClick="btn_submit_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
