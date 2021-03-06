<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_mstr.aspx.cs" Inherits="BudgetProcess.budget_bg_mstr" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/superTables.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/superTables.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/superTable.js" type="text/javascript"></script>
    <style type="text/css">
        .altRow
        {
            background-color: #ddddff;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#gvMstr").toSuperTable({ width: "980px", height: "460px", fixedCols: 8, headerRows: 2 })
            .find("tr:even").addClass("altRow");
        });
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table style="width: 1000px;">
        <tr align="center">
            <td style="width: 956px;">
                主管:<asp:TextBox ID="txtMstr" runat="server" CssClass="SmallTextBox" Width="76px"></asp:TextBox>部门:<asp:TextBox
                    ID="txtDep" runat="server" CssClass="SmallTextBox" Width="76px"></asp:TextBox>
                账户:<asp:TextBox ID="txtAcc" runat="server" CssClass="SmallTextBox" Width="76px"></asp:TextBox>成本中心:<asp:TextBox
                    ID="txtCC" runat="server" CssClass="SmallTextBox" Width="76px"></asp:TextBox>项目:<asp:TextBox
                        ID="txtPro" runat="server" CssClass="SmallTextBox" Width="76px"></asp:TextBox>当前期间:<asp:DropDownList
                            ID="dropYear" runat="server">
                        </asp:DropDownList>
                年
                <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" OnClick="btnSearch_Click"
                    Text="查询" Width="40" />
                <asp:Button ID="btnImport" runat="server" CssClass="SmallButton3" OnClick="btnImport_Click"
                    Text="导出到excel" Width="74px" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvMstr" runat="server" AutoGenerateColumns="False"  CssClass="GridViewStyle"
        DataKeyNames="bg_master,bg_dept,bg_acc,bg_sub,bg_project,bg_cc" PageSize="17"
        OnPageIndexChanging="gvMstr_PageIndexChanging" Width="1002px" OnRowCreated="gvMstr_RowCreated"
        OnRowDataBound="gvMstr_RowDataBound">
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
            <asp:BoundField HeaderText="主管" DataField="bg_masterC">
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField HeaderText="部门" DataField="bg_dept">
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField HeaderText="账户" DataField="bg_acc">
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="分账户" DataField="bg_sub">
                <HeaderStyle Width="55px" />
                <ItemStyle Width="55px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="描述" DataField="bg_desc">
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField HeaderText="成本中心" DataField="bg_cc">
                <HeaderStyle Width="60px" />
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="项目" DataField="bg_project">
                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="编辑">
                <HeaderStyle Width="50px" />
                <ItemStyle CssClass="edit" Font-Bold="False" Font-Size="12px" Font-Underline="True" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="A1">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="A2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="A3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="B1">
                <HeaderStyle Width="70px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="B2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="B3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="C1">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="C2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="C3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="D1">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="D2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="D3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="E1">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="E2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="E3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="F1">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="F2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="F3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="G1">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="G2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="G3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="H1">
                <HeaderStyle Width="70px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="H2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="H3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="I1">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="I2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="I3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="J1">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="J2">
                <HeaderStyle Width="70px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="J3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="K1">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="K2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="K3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" DataField="L1">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="预测" DataField="L2">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="差异" DataField="L3">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField HeaderText="实际" />
            <asp:BoundField HeaderText="预测" />
        </Columns>
    </asp:GridView>
    </form>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
