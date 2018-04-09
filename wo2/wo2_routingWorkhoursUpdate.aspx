<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_routingWorkhoursUpdate.aspx.cs"
    Inherits="wo2_routingWorkhoursUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="">
            <tr>
                <td>
                    结算期间：
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtYear" runat="server" Width="38px" MaxLength="4"></asp:TextBox>
                    年<asp:TextBox ID="txtMonth" runat="server" Width="30px" MaxLength="2"></asp:TextBox>
                    月</td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    公司：
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="DropDomain" runat="server" Width="100px">
                        <asp:ListItem Text="--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="SZX" Value="1"></asp:ListItem>
                        <asp:ListItem Text="ZQL" Value="2"></asp:ListItem>
                        <asp:ListItem Text="YQL" Value="5"></asp:ListItem>
                        <asp:ListItem Text="HQL" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    工单号：
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtNbr" runat="server" Width="115px"></asp:TextBox>
                </td>
                <td>
                    ID号：
                </td>
                <td>
                    <asp:TextBox ID="txtLot" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    工艺代码：
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtRouting" runat="server" Width="115px" MaxLength="15"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td align="left" colspan="2">
                    <asp:Button Width="100px" ID="btnUpdate" Text="更新" runat="server" 
                        OnClick="btnUpdate_Click1" CssClass="SmallButton3" />
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    结果反馈：
                </td>
                <td colspan="3" style="text-align: left">
                    本次更新共影响<asp:Label ID="Labcount" runat="server" Text="0"></asp:Label>条
                </td>
            </tr>
            <tr>
               <td colspan="4" style="text-align: left; vertical-align: bottom; color: Red;">
                注意：一旦财务结算，则无法更新
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
