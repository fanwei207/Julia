<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_aql.aspx.cs" Inherits="QC_qc_aql" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="1002" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td style="width: 945px">
                    样本量字码:<asp:DropDownList ID="dropCode" runat="server" Width="91px" DataTextField="aqlCode"
                        DataValueField="aqlCode">
                    </asp:DropDownList>
                    <asp:Label ID="lblCode" runat="server" ForeColor="Red" Text="请选择一项" Visible="False"
                        Width="69px"></asp:Label>
                    AQL:<asp:DropDownList ID="dropAqlLevel" runat="server" Width="91px" DataTextField="aql"
                        DataValueField="aql">
                    </asp:DropDownList>
                    <asp:Label ID="lblAql" runat="server" ForeColor="Red" Text="请选择一项" Visible="False"
                        Width="69px"></asp:Label>方向:
                    <asp:DropDownList ID="dropWay" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropWay_SelectedIndexChanged">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem Value="0">UP</asp:ListItem>
                        <asp:ListItem Value="1">DOWN</asp:ListItem>
                        <asp:ListItem Value="2">NONE</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblWay" runat="server" ForeColor="Red" Text="请选择一项" Visible="False"
                        Width="69px"></asp:Label><asp:Label ID="lblAc" runat="server" Text="AC:" Width="21px"
                            Visible="False"></asp:Label>
                    <asp:TextBox ID="txtAc" runat="server" Width="30px" Visible="False">0</asp:TextBox><asp:Label
                        ID="lblRe" runat="server" Text="RE:" Width="11px" Visible="False"></asp:Label><asp:TextBox
                            ID="txtRe" runat="server" Width="30px" Visible="False">1</asp:TextBox>
                    <asp:Label ID="lblCompare" runat="server" ForeColor="Red" Text="AC的值必须小于RE的值" Visible="False"
                        Width="127px"></asp:Label>&nbsp;
                    <asp:Button ID="btnSave" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="btnSave_Click" Text="保存" Width="80px" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvAql" runat="server" AutoGenerateColumns="False" DataKeyNames="aqlCode"
            OnRowDataBound="gvAql_RowDataBound" Width="1440px" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
