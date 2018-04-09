<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_alterablefee.aspx.cs"
    Inherits="wo2_alterablefee" %>

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
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 584px;">
            <tr>
                <td style="width: 769px">
                    <asp:DropDownList ID="dropGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 769px">
                    <asp:DropDownList ID="dropType" runat="server" Width="202px" DataTextField="ft_name"
                        DataValueField="ft_id" AutoPostBack="True" OnSelectedIndexChanged="dropType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtAmt" runat="server" CssClass="smalltextbox Numeric" Width="104px">
                    </asp:TextBox>
                    <asp:TextBox ID="txtEffdate" runat="server" CssClass="smalltextbox Date" Width="102px">
                    </asp:TextBox>
                    <asp:Button ID="btnAdd" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="新增" Width="56px" OnClick="btnAdd_Click" />&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="查询" Width="56px" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            AllowPaging="True" PageSize="15" OnPageIndexChanging="gv_PageIndexChanging" OnRowDeleting="gv_RowDeleting"
            DataKeyNames="af_id,af_type_id" OnRowCommand="gv_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="af_type_name" HeaderText="费用类别">
                    <HeaderStyle Width="200px" />
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="af_amt" HeaderText="金额">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="af_eff_date" HeaderText="生效日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" runat="server" CausesValidation="False" CommandName="myEdit"
                            Text="<u>编辑</u>"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:CommandField DeleteText="<u>删除</u>" ShowDeleteButton="True">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
