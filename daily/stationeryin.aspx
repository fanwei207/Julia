<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.stationeryin" CodeFile="stationeryin.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body  >
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <br>
        <br>
         
        <table style="width: 360px; height: 277px" cellspacing="2" cellpadding="2" border="0">
            <tr>
                <td style="width: 84px; height: 16px" align="right">
                    名称:
                </td>
                <td style="height: 16px" colspan="3">
                    <asp:DropDownList ID="stationery" runat="server" Width="250px" AutoPostBack="true"
                        OnSelectedIndexChanged="stationerychange">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 84px; height: 10px" align="right">
                    数量:
                </td>
                <td style="width: 140px; height: 10px">
                    <asp:TextBox ID="PDQty" runat="server" Width="250px" CssClass="SmallTextBox Numeric" MaxLength="8"
                        TabIndex="1" AutoPostBack="True" OnTextChanged="pricechang"></asp:TextBox>
                </td> 
                <tr>
                    <td style="width: 84px; height: 10px" align="right">
                        单价:
                    </td>
                    <td style="width: 140px; height: 10px">
                        <asp:Label ID="price" runat="server" Width="101px"></asp:Label>
                    </td>
                    <tr>
                        <td style="width: 84px; height: 10px" align="right">
                            总价:
                        </td>
                        <td style="width: 140px; height: 10px">
                            <asp:Label ID="totalprice" runat="server" Width="101px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px; height: 14px" align="right">
                            日期:
                        </td>
                        <td style="height: 14px" colspan="3">
                            <asp:TextBox ID="InDate" runat="server" Width="101px"  MaxLength="20"
                                ReadOnly="True" CssClass="smalltextbox Date"></asp:TextBox> 
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px; height: 80px" valign="top" align="right">
                            备注:
                        </td>
                        <td style="height: 80px" valign="top" colspan="3">
                            <asp:TextBox ID="note" runat="server" TextMode="MultiLine" Height="80px" Width="250px"
                                Wrap="True" TabIndex="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 35px" align="right" colspan="2">
                            <asp:Button ID="Button1" runat="server" Width="50px" CssClass="SmallButton2" Text="进库"
                                TabIndex="5"></asp:Button>
                            <asp:Button ID="Button2" runat="server" Width="50px" CssClass="SmallButton2" Text="返回"
                                ></asp:Button>
                        </td>
                    </tr>
        </table>
        </form> 
    </div>
    <script type="text/javascript">
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</body>
</html>
