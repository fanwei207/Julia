<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UMP_finishList.aspx.cs" Inherits="wo2_UMP_finishList" %>

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
                <td align="left" style="width: 377px">
                    申请单号
                    <asp:TextBox ID="txtProjectcode" runat="server" Width="132px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox><asp:Label ID="lblmid" runat="server" Text=" " Visible="false"></asp:Label>
                      成本中心 <asp:TextBox ID="txtdepCode" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                </td>
                <td style="width: 217px">
                   域 <asp:DropDownList ID="ddldomain" runat="server">
                        <asp:ListItem Selected="True">--</asp:ListItem>
                        <asp:ListItem>SZX</asp:ListItem>
                        <asp:ListItem>ZQL</asp:ListItem>
                        <asp:ListItem>HQL</asp:ListItem>
                        <asp:ListItem>YQL</asp:ListItem>
                    </asp:DropDownList>
                     状态 <asp:DropDownList ID="ddlstatus" runat="server">
                        <asp:ListItem Selected="True" Value="10">--</asp:ListItem>
                        <asp:ListItem Value="1">已入账</asp:ListItem>
                        <asp:ListItem Value="0">未入账</asp:ListItem>
                      
                    </asp:DropDownList>

                </td>



                <td align="left" class="style1" style="width: 380px;">
                    &nbsp;创建时间:<asp:TextBox ID="txtApplyDate" runat="server" Width="75px" CssClass="SmallTextBox Date"></asp:TextBox>
                     -<asp:TextBox ID="txtApplyDate1" runat="server" Width="75px" CssClass="SmallTextBox Date"></asp:TextBox>
                    <asp:CheckBox ID="chkb_displayToApprove" runat="server" Text="只看自己审批"
                        Checked="true" Width="158px"  Visible="false"
                         />
                    &nbsp;事务号<asp:TextBox ID="txttrnbr" runat="server" Width="75px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td align="left" style="width: 280px; height: 26px;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="50px"
                        OnClick="btnSearch_Click" />
                    &nbsp; &nbsp;
                    &nbsp; &nbsp;
                    <asp:Button ID="EXCEL" runat="server" CssClass="SmallButton2" 
                        Text="Export" Width="50px" onclick="BtnExport_Click"></asp:Button>
                   
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowPaging="True" AutoGenerateColumns="False" PageSize="25"
            CssClass="GridViewStyle" runat="server" Width="1200px" DataKeyNames="id,UMP_appby,UMP_createby,UMP_status"
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
                <asp:BoundField HeaderText="申请单号" DataField="UMP_code" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="类型" DataField="UMP_typename" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="总账" DataField="UMP_accountname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" Width="55px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="明细账" DataField="UMP_accountdetname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" Width="55px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="域" DataField="UMP_domain" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" Width="55px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="地点" DataField="UMP_site" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" Width="55px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="成本中心" DataField="UMP_Departmentscode" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" Width="55px" />
                </asp:BoundField>
                  <asp:BoundField HeaderText="创建人" DataField="UMP_createname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" Width="55px" />
                </asp:BoundField>



               
                <asp:BoundField HeaderText="创建时间" DataField="UMP_createdate" ReadOnly="True"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
               
                <asp:BoundField HeaderText="物料号" DataField="UMP_qad" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="物料描述" DataField="UMP_desc" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="申请量" DataField="UMP_qty1" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="实发量" DataField="UMP_qty2" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="单位" DataField="UMP_UM" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="备注" DataField="UMP_remark" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkApproveDetail" Text="View" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandName="look" CommandArgument='<%#  ((GridViewRow) Container).RowIndex %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:TemplateField>
               
             
            </Columns>
        </asp:GridView>
        </form>
    </div>
</body>
</html>