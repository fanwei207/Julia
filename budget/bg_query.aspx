<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_query.aspx.cs" Inherits="BudgetProcess.budget_bg_query" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .fixTitle
        {
            position: relative;
            top: expression(this.offsetParent.scrollTop-1);
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table style="width: 850px;">
            <tr>
                <td style="height: 23px">
                    ����:<asp:TextBox ID="txtMstr" runat="server" CssClass="SmallTextBox" Width="110px"></asp:TextBox>����:<asp:TextBox
                        ID="txtDep" runat="server" CssClass="SmallTextBox" Width="110px"></asp:TextBox>
                    �˻�:<asp:TextBox ID="txtAcc" runat="server" CssClass="SmallTextBox" Width="110px"></asp:TextBox>�ɱ�����:<asp:TextBox
                        ID="txtCC" runat="server" CssClass="SmallTextBox" Width="110px"></asp:TextBox>��Ŀ:<asp:TextBox
                            ID="txtPro" runat="server" CssClass="SmallTextBox" Width="90px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        Text="��ѯ" Width="40" />
                    <asp:Button ID="btnImport" runat="server" CssClass="SmallButton3" OnClick="btnImport_Click"
                        Text="������excel" Width="74px" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMstr" runat="server" AutoGenerateColumns="False" OnSorting="gvMstr_Sorting"
            OnRowDataBound="gvMstr_RowDataBound" DataKeyNames="bg_master,bg_dept,bg_acc,bg_sub,bg_project,bg_cc,bg_year,bg_per"
            OnRowCreated="gvMstr_RowCreated" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle fixTitle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="bg_master" HeaderText="����" Visible="False">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="bg_master" HeaderText="ID" Visible="False">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="bg_masterC">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="bg_dept" HeaderText="����">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�˻�" DataField="bg_acc">
                    <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="���˻�" DataField="bg_sub">
                    <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="bg_desc">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle Width="130px" />
                </asp:BoundField>
                <asp:BoundField DataField="bg_cc" HeaderText="�ɱ�����">
                    <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��Ŀ" DataField="bg_project">
                    <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ڼ�" DataField="period">
                    <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:HyperLinkField DataTextField="bg_ecurr_amt" HeaderText="ʵ��">
                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" />
                    <HeaderStyle Width="90px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:HyperLinkField>
                <asp:BoundField HeaderText="Ԥ��" DataField="bg_budget">
                    <HeaderStyle Width="90px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="diff">
                    <HeaderStyle Width="90px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/vbscript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
