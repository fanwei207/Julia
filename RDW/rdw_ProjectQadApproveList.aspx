<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rdw_ProjectQadApproveList.aspx.cs"
    Inherits="RDW_rdw_ProjectQadApproveList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 318px;
        }
    </style>
</head>
<body>
    <div>
        <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td align="left" style="width: 277px">
                    Project Name
                    <asp:TextBox ID="txtProject" runat="server" Width="132px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox><asp:Label ID="lblmid" runat="server" Text=" " Visible="false"></asp:Label>
                </td>
                <td style="width: 217px">
                    Project Code<asp:TextBox ID="txtProjectCode" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                </td>
                <td align="left" class="style1">
                    &nbsp;ApplyDate:<asp:TextBox ID="txtApplyDate" runat="server" Width="75px" CssClass="SmallTextBox Date"></asp:TextBox>
                    <asp:CheckBox ID="chkb_displayToApprove" runat="server" Text="Only Pending Approval"
                        Checked="true" AutoPostBack="true" Width="158px" 
                        oncheckedchanged="chkb_displayToApprove_CheckedChanged" />
                    &nbsp;
                </td>
                <td align="left" style="width: 280px; height: 26px;">
                    <asp:Button ID="btnSearch" runat="server" Text="Query" CssClass="SmallButton2" Width="50px"
                        OnClick="btnSearch_Click" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnApply" runat="server" Text="New Apply" OnClick="btnApply_Click"
                        Width="72px" CssClass="SmallButton2" Visible="true" />
                    &nbsp; &nbsp;
                    <asp:Button ID="BtnExport" runat="server" CssClass="SmallButton2" 
                        Text="Export" Width="50px" onclick="BtnExport_Click"></asp:Button>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" Text="Back" CssClass="SmallButton2"
                        Width="50px" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowPaging="True" AutoGenerateColumns="False" PageSize="25"
            CssClass="GridViewStyle" runat="server" Width="1200px" DataKeyNames="rdw_pqid,rdw_MstrID,rdw_applyby,rdw_approverId"
            OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand" OnPageIndexChanging="gv_PageIndexChanging"
            OnRowDeleting="gv_RowDeleting">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="1200px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="applyNo" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="applyName" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ApplyReason"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Apply Date" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Approver" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Approve Date" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Approve Reasult" Width="90px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="No apply information" ColumnSpan="8"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Project Name" DataField="RDW_Project" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Project Code" DataField="RDW_ProdCode" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" Width="55px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Applyer Name" DataField="rdw_applyName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Apply Date" DataField="rdw_applyDate" ReadOnly="True"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Approver" DataField="rdw_approverId" ReadOnly="True"
                    Visible="False">
                    <HeaderStyle HorizontalAlign="Center" Width="0px" />
                    <ItemStyle HorizontalAlign="Center" Width="0px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Approver Name" DataField="rdw_approverName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Approve" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkFirstApp" Text="Approve" ForeColor="black" Font-Underline="true"
                            Enabled="false" Font-Size="12px" runat="server" CommandArgument='<%#  ((GridViewRow) Container).RowIndex %>'
                            CommandName="Approve" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkApproveDetail" Text="View" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandName="look" CommandArgument='<%#  ((GridViewRow) Container).RowIndex %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" Text="delete" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="delete"
                            Enabled="false" />
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                    <ControlStyle Font-Size="12px" Font-Underline="True" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="ApproveDate" DataField="rdw_approveDate" ReadOnly="True"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Approve Reasult" DataField="rdw_approveResult" ReadOnly="True"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Apply Reason" DataField="rdw_applyReason" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="330px" />
                    <ItemStyle HorizontalAlign="Left" Width="330px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
</body>
</html>
