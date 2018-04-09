<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stocking_Submit.aspx.cs" Inherits="plan_Stocking_Submit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <table cellspacing="0" cellpadding="0" width="980px" border="0" class="">
            <tr>
                <td class="">
                </td>
                <td align="left">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" Width="60px" 
                        AutoPostBack="True" oncheckedchanged="chkAll_CheckedChanged"
                         />
                </td>
                <td align="right">
                    <asp:Label ID="lblSysPKNo" runat="server" Width="100px" CssClass="LabelRight" Text="备货单号(*):"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtNbr" runat="server" Width="80px" TabIndex="1" CssClass="smalltextbox"></asp:TextBox>
                    </td>
                <td align="right">
                    <asp:Label ID="lblSysPKRef" runat="server" Width="55px" CssClass="LabelRight" Text="客户:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="center">
                         <asp:TextBox ID="txtvent" runat="server" CssClass="smalltextbox" TabIndex="3"
                        Width="70px"></asp:TextBox>
                </td>
                <td align="right">
                    客户物料(*):
                </td>
                <td align="right">
                    <asp:TextBox ID="txtpart" runat="server" CssClass="smalltextbox" TabIndex="3"
                        Width="70px"></asp:TextBox>
                    </td>
                <td align="right">
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" CssClass="LabelRight" Text="QAD:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtQAD" runat="server" Width="80px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right">
                   状态：<asp:DropDownList ID="ddlstatus" runat="server">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="1">已确认</asp:ListItem>
                        <asp:ListItem Selected="True" Value="2">未确认</asp:ListItem>
                       <asp:ListItem  Value="-1">已驳回</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="center">
                    &nbsp;</td>
                 <td align="right">
                     &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
                <td align="Left">
                   <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="4"
                        Text="查询" Width="50px" OnClick="btnQuery_Click" Height="22px" />
                </td>
                <td align="Left">
                    &nbsp;<asp:Button 
                        ID="btnAddNew" runat="server" CssClass="SmallButton2" TabIndex="5"
                        Text="确认" Width="50px" onclick="btnAddNew_Click" />
                </td>
                <td class="">
                </td>
            </tr>
            <tr>
                <td class="style1" align="Left" colspan ="14">
                    驳回原因：  <asp:TextBox ID="txtcancel" runat="server" CssClass="smalltextbox" TabIndex="3"
                        Width="400px"></asp:TextBox>
                </td>
                <td align="Left">
                      <asp:Button ID="btncancel" runat="server" CausesValidation="False" CssClass="SmallButton3" Width="50px"
                       Text="驳回" OnClick="btncancel_Click"  />
                </td>
            </tr>
            <tr>
                 <td class="style1"  align="Left" colspan ="9">
                 上传订单：<input id="filename1" runat="server" name="filename" 
                     style=" width:400px; height: 24px;" type="file" />
                      <asp:Button ID="btnimport" runat="server" CausesValidation="False" CssClass="SmallButton3"
                       Text="上传" OnClick="btnimport_Click" />
                     <asp:LinkButton ID="linkDownload" runat="server" onclick="linkDownload_Click">下载源数据</asp:LinkButton>
                 </td>
            </tr>

        </table>
        <asp:GridView ID="gvSID" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="30" 
            DataKeyNames="sk_nbr" Width="980px" OnRowCancelingEdit="gvSID_RowCancelingEdit" OnRowEditing="gvSID_RowEditing" OnRowUpdating="gvSID_RowUpdating" OnPageIndexChanging="gvSID_PageIndexChanging" 
           >
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="√" Width="20px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="备货单号" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="客户" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="客户物料" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="QAD" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="价格" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="数量" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="单位" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="备注" Width="400px" HorizontalAlign="center"></asp:TableCell>
                     
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Select" runat="server" Width="20px" Enabled='<%# Bind("sk_submitdate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sk_nbr" HeaderText="备货单号" ReadOnly="true">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="sk_vent" HeaderText="客户" ReadOnly="true">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="ad_name" HeaderText="客户名称" ReadOnly="true">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sk_part" HeaderText="客户物料" ReadOnly="true">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sk_QAD" HeaderText="QAD" ReadOnly="true">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="sk_price" HeaderText="价格"  ReadOnly="true">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sk_num" HeaderText="数量" ReadOnly="true">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sk_EA" HeaderText="单位" ReadOnly="true">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="status" HeaderText="状态" ReadOnly="true">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="订单号">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtponbr" runat="server" CssClass="smalltextbox" MaxLength="50"
                                            Text='<%# Bind("po_Nbr") %>' Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblponbr" runat="server" Text='<%# Bind("po_Nbr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                                </asp:TemplateField>
                 <asp:TemplateField HeaderText="订单行">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtpolinr" runat="server" CssClass="smalltextbox" MaxLength="50"
                                            Text='<%# Bind("po_line") %>' Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblpoline" runat="server" Text='<%# Bind("po_line") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
              
                <asp:CommandField ShowEditButton="True">
                                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:CommandField>

                <asp:BoundField DataField="sk_remark" HeaderText="备注" ReadOnly="true">
                    <HeaderStyle Width="400px" HorizontalAlign="Center" />
                    <ItemStyle Width="400px" HorizontalAlign="Left" />
                </asp:BoundField>
              
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
