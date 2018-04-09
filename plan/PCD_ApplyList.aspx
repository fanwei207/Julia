<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCD_ApplyList.aspx.cs" Inherits="plan_PCD_ApplyList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                <td align="left" style="width: 217px">
                    订单号
                    <asp:TextBox ID="txtPoNbr" runat="server" Width="132px" CssClass="SmallTextBox"
                        TabIndex="0"></asp:TextBox><asp:Label ID="lblmid" runat="server" Text=" " Visible="false"></asp:Label>(*)
                </td>
                <td style="width: 217px">
                    客户零件号<asp:TextBox ID="txtCustPart" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>(*)
                </td>
                <td style="width: 217px">
                    QAD号<asp:TextBox ID="txtPart" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="2"></asp:TextBox>
                </td>
                <td align="left" class="style1">
                    &nbsp;申请日期:<asp:TextBox ID="txtApplyDate" runat="server" Width="75px" CssClass="SmallTextBox Date"></asp:TextBox>
                    <asp:CheckBox ID="chkb_displayToApprove" runat="server" Text="待审批"
                        Checked="true" AutoPostBack="true" Width="158px" 
                        oncheckedchanged="chkb_displayToApprove_CheckedChanged" />
                    &nbsp;
                </td>
                <td align="left" style="width: 150px; height: 26px;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="50px"
                        OnClick="btnSearch_Click" />
                    &nbsp; &nbsp;
<%--                    <asp:Button ID="btnApply" runat="server" Text="New Apply" OnClick="btnApply_Click"
                        Width="72px" CssClass="SmallButton2" Visible="true" />
                    &nbsp; &nbsp;--%>
<%--                    <asp:Button ID="BtnExport" runat="server" CssClass="SmallButton2" 
                        Text="Export" Width="50px" onclick="BtnExport_Click"></asp:Button>--%>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowPaging="True" AutoGenerateColumns="False" PageSize="25"
            CssClass="GridViewStyle" runat="server" Width="1200px" DataKeyNames="ApplyId,poNbr,ApplyBy"
            OnRowCommand="gv_RowCommand" OnPageIndexChanging="gv_PageIndexChanging" 
            onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting">
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
                        <asp:TableCell HorizontalAlign="center" Text="订单号" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请人" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="当前审批人" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="明细" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="审批日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="审批结果" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请原因" Width="300px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="No apply information" ColumnSpan="11"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="订单号" DataField="poNbr" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="申请人" DataField="Applyer" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="申请日期" DataField="ApplyDate" ReadOnly="True"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="当前审批人" DataField="Approver" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="明细">
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
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                    <ControlStyle Font-Size="12px" Font-Underline="True" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="审批日期" DataField="ApproveDate" ReadOnly="True"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="审批结果" DataField="ApproveResult" ReadOnly="True"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="申请原因" DataField="ApplyReason" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
