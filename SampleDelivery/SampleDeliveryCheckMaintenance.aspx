<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleDeliveryCheckMaintenance.aspx.cs" Inherits="SampleDelivery_SampleDeliveryCheckMaintenance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
            width: 51px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table cellpadding="0" cellspacing="0" style="text-align: left">
            <tr>
                <td class="style1">
                    &nbsp;送样单：
                </td>
                <td class="style1">
                    <asp:TextBox ID="txt_nbr" runat="server" Width="110px" ReadOnly="true"></asp:TextBox>
                    <asp:Label ID="lbl_id" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbl_detId" runat="server" Visible="false"></asp:Label>
                </td>
                <td align="right">
                    生成日期：
                </td>
                <td class="style3">
                   <asp:TextBox ID="txt_createdDate" runat="server" Width="149px" ReadOnly="true" CssClass="Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;接收方：
                </td>
                <td>
                     <asp:TextBox ID="txt_receiver" runat="server" Width="149px"></asp:TextBox>
                </td>
                <td align="right">
                    送&nbsp;&nbsp;达&nbsp;&nbsp;地：
                </td>
                <td>
                     <asp:TextBox ID="txt_shipto" runat="server" Width="149px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;备&nbsp;&nbsp;&nbsp;&nbsp;注：
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtRmks" runat="server" Width="598px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:Button ID="btn_Back" runat="server" Text="返回" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_Back_Click" />
                </td>
            </tr>

        </table>
        <br/>
        <table id="tb_det" runat="server" style="text-align: center; width: 900px;">
            
            <tr>
                <td colspan="2" class="style2">
                    <asp:GridView ID="gv_det" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_det_PageIndexChanging" DataKeyNames="Id"
                        OnRowDataBound="gv_det_RowDataBound" OnRowCancelingEdit="gv_det_RowCancelingEdit"
                        OnRowEditing="gv_det_RowEditing" OnRowUpdating="gv_det_RowUpdating" OnRowCommand="gv_det_RowCommand"
                        PageSize="8" Width="1100px" EnableModelValidation="True">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="900px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="部件号" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="QAD号" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="数量" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="备注"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="关联文档" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" " Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="检测" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" " Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="检测日期" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="原因" Width="200px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="单据无明细记录" ColumnSpan="7"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PartCode" HeaderText="部件号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="QadNo" HeaderText="QAD号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="数量" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remarks" HeaderText="备注" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                <ItemStyle HorizontalAlign="Center" Width="250px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="关联文档">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_Doc" runat="server" CommandName="EditDoc" Font-Underline="True"
                                        CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' ForeColor="Black"> 查看</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="检测结果">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCheck" runat="server" Text='<%# Eval("CheckResult")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_CheckResult" runat="server">
                                            <asp:ListItem Value="2">--</asp:ListItem>
                                            <asp:ListItem Value="0">不通过</asp:ListItem>
                                            <asp:ListItem Value="1">通过</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField EditText="检测" ShowEditButton="True" CancelText="取消" UpdateText="确定">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                    <ControlStyle ForeColor="Black" Font-Underline="True" />
                                </asp:CommandField>
                                <asp:BoundField DataField="CheckedDate" HeaderText="检测日期" ReadOnly="true"
                                    DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="原因">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReason" runat="server" Text='<%# Eval("CheckRemarks")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtReason" runat="server" Width="300px" MaxLength="200"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="300px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
    </form>
</body>
</html>
