<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_calendar.aspx.cs" Inherits="wo2_calendar" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="HEAD1" runat="server">
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
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 334px;">
            <tr>
                <td style="width: 769px;">
                    <asp:DropDownList ID="dropYear" runat="server" Width="68px" DataTextField="ft_name"
                        DataValueField="ft_id" AutoPostBack="True" OnSelectedIndexChanged="dropYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;<asp:DropDownList ID="dropMonth" runat="server" Width="66px" AutoPostBack="True"
                        OnSelectedIndexChanged="dropMonth_SelectedIndexChanged">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">01月</asp:ListItem>
                        <asp:ListItem Value="2">02月</asp:ListItem>
                        <asp:ListItem Value="3">03月</asp:ListItem>
                        <asp:ListItem Value="4">04月</asp:ListItem>
                        <asp:ListItem Value="5">05月</asp:ListItem>
                        <asp:ListItem Value="6">06月</asp:ListItem>
                        <asp:ListItem Value="7">07月</asp:ListItem>
                        <asp:ListItem Value="8">08月</asp:ListItem>
                        <asp:ListItem Value="9">09月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                        <asp:ListItem Value="12">12月</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="btnAdd_Click" Text="保存" Width="56px" />
                    <asp:CheckBox ID="chkView" runat="server" Visible="False" />
                </td>
            </tr>
            <tr>
                <td style="width: 769px">
                    <asp:DataList ID="DataList1" runat="server" BorderColor="#999999" GridLines="Both"
                        RepeatColumns="5" OnItemDataBound="DataList1_ItemDataBound" RepeatDirection="Horizontal">
                        <ItemStyle BackColor="#EEEEEE" />
                        <ItemTemplate>
                            <asp:Label ID="lbDate" runat="server" Text='<%# Bind("cal_date", "{0:yyyy-MM-dd}") %>'
                                Width="70px" Font-Bold="True"></asp:Label><br />
                            &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;<asp:CheckBox ID="chkDate" runat="server"
                                Checked='<%# Bind("cal_active") %>' />
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
             <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
