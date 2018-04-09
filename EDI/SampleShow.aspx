﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleShow.aspx.cs" Inherits="EDI_SampleShow" %>

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
                    <asp:Label ID="lblSysPKNo" runat="server" Width="100px" CssClass="LabelRight" Text="订单号(*):"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtNbr" runat="server" Width="80px" TabIndex="1" CssClass="smalltextbox"></asp:TextBox>
                    </td>
                <td align="right">
                    <asp:Label ID="lblSysPKRef" runat="server" Width="55px" CssClass="LabelRight" Text="接收日期:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="center">
                         <asp:TextBox ID="txtdate" runat="server" CssClass="smalltextbox Date" TabIndex="3"
                        Width="70px"></asp:TextBox>
                </td>
              
           
                <td align="right">
                   状态：<asp:DropDownList ID="ddlstatus" runat="server">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="1">已确认</asp:ListItem>
                        <asp:ListItem Selected="True" Value="2">未确认</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="center">
                    &nbsp;</td>
                 <td align="right">
                     &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
                <td align="Left">
                    &nbsp;<asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="4"
                        Text="查询" Width="50px" OnClick="btnQuery_Click" Height="22px" />
                </td>
                <td align="Left">
                    &nbsp;<asp:Button 
                        ID="btnAddNew" runat="server" CssClass="SmallButton2" TabIndex="5"
                        Text="确认" Width="50px" onclick="btnAddNew_Click" />
                   
                </td>

                <td class="">
                     <asp:Button ID="btnExportExcel" runat="server" Text="导出" OnClick="btnExportExcel_Click"
                        CssClass="SmallButton3" Width="50px" />
                </td>
            </tr>
           

        </table>
        <asp:GridView ID="gvSID" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="20" 
            DataKeyNames="id" Width="980px" OnPageIndexChanging="gvSID_PageIndexChanging" OnRowDataBound="gvSID_RowDataBound" 
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
                        <asp:CheckBox ID="chk_Select" runat="server" Width="20px" Enabled='<%# Bind("finished") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="poNbr" HeaderText="PO Number" ReadOnly="true">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                
               <asp:BoundField DataField="cusCode" HeaderText="cusCode" ReadOnly="true">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="rmk" HeaderText="Ship To" ReadOnly="true">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PoRecDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="接收日期"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
              
                <asp:TemplateField HeaderText="Line">
                                <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("poLine")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Part #">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("partNbr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QAD #">
                                <HeaderStyle Width="00px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQadPart" runat="server" Text='<%# Bind("qadPart")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="domain">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                <ItemStyle Width="70px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("domain")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Site">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" />
                                <ItemTemplate>
                                    <asp:Label ID="Labe22" runat="server" Text='<%# Bind("site")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Qty">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ordQty")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UM">
                                <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPoNbr" runat="server" Text='<%# Bind("um")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="Require Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("reqDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("dueDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Approve Status">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_appvResult" runat="server" Text='<%# Bind("appvResult")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                          <asp:TemplateField HeaderText="IsSample">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                <ItemStyle Width="70px" HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblsample" runat="server" Text='<%# Bind("isSample")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                 <asp:BoundField DataField="mpod_createdName" HeaderText="CreateBy" ReadOnly="true">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
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