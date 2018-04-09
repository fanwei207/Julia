<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_mstr_edit.aspx.cs" Inherits="budget_bg_mstr_edit" %>

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
        <form id="Form1" runat="server">
        <table style="width: 396px;">
            <tr>
                <td style="width: 662px; text-align: center;">
                    主管: &nbsp; &nbsp;<asp:TextBox ID="txtMstr" runat="server" CssClass="SmallTextBox"
                        Width="76px" ReadOnly="True"></asp:TextBox>
                    部门: &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:TextBox ID="txtDep" runat="server" CssClass="SmallTextBox" Width="76px" ReadOnly="True"></asp:TextBox>
                    账户:<asp:TextBox ID="txtAcc" runat="server" CssClass="SmallTextBox" Width="76px" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 662px; height: 22px; text-align: center;">
                    分账户:<asp:TextBox ID="txtSub" runat="server" CssClass="SmallTextBox" ReadOnly="True"
                        Width="76px"></asp:TextBox>分账户描述:<asp:TextBox ID="txtDes" runat="server" CssClass="SmallTextBox"
                            ReadOnly="True" Width="178px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 662px; height: 22px; text-align: left">
                    &nbsp; &nbsp;&nbsp; 成本中心:<asp:TextBox ID="txtCC" runat="server" CssClass="SmallTextBox"
                        Width="76px" ReadOnly="True"></asp:TextBox>
                    项目:<asp:TextBox ID="txtPro" runat="server" CssClass="SmallTextBox" Width="76px" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 662px; height: 143px; text-align: center;">
                    <asp:GridView ID="gvBudget" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        OnRowDataBound="gvBudget_RowDataBound" Width="366px" 
                        DataKeyNames="bg_ecurr_amt,bg_budget">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="期间" DataField="period">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="实际金额">
                                <ItemTemplate>
                                    <%#Eval("bg_ecurr_amt")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="预测金额">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBudget" runat="server" CssClass="SmallTextBox Numeric" Width="129px"
                                        Text='<%#Bind("bg_budget") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="差额">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="保存" Width="50px"
            OnClick="btnSave_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="关闭" Width="60px"
            OnClientClick="window.close();" />
        <asp:TextBox ID="txtYear" runat="server" CssClass="SmallTextBox" Width="76px" Visible="False"></asp:TextBox></form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
