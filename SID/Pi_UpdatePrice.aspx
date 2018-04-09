<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pi_UpdatePrice.aspx.cs" Inherits="IT_Pi_ShowPrice" %>

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
    <form id="form1" runat="server">
    <div>
     <table style="width: 1230px">
            <tr>
                <td align="left">
                    客户号：<asp:TextBox ID="txtcust" runat="server" Width="100px" CssClass="Param"></asp:TextBox>
                    &nbsp; 物料号：<asp:TextBox ID="txtQAD" runat="server" Width="120px" CssClass="Param"></asp:TextBox>
                    &nbsp; 最终客户：<asp:TextBox ID="txtshipto" runat="server" Width="100px" CssClass="Param"></asp:TextBox>
                    &nbsp;创建时间：
                    <asp:TextBox ID="txtCrtDate1" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                    --<asp:TextBox ID="txtCrtDate2" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                    
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" 
                        onclick="btnSearch_Click"  />
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        PageSize="20" DataKeyNames="pi_id"
                        Width="1600px" AllowPaging="True" 
            onpageindexchanging="gv_PageIndexChanging" 
            onrowcancelingedit="gv_RowCancelingEdit" onrowediting="gv_RowEditing" 
            onrowupdating="gv_RowUpdating" onrowdeleting="gv_RowDeleting" >
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="1200px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="客户" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="物料号" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="货币" Width="600px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="单位" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="开始时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="到期时间" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                     <asp:TableCell Text="价格1" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                      <asp:TableCell Text="价格2" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                       <asp:TableCell Text="价格3" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="Pi_Cust" HeaderText="客户" HtmlEncode="False" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Pi_QAD" HeaderText="物料号" HtmlEncode="False" ReadOnly="True">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Pi_ShipTo" HeaderText="最终客户" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                           
                             <asp:BoundField DataField="Pi_Currency" HeaderText="货币" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                         <asp:BoundField DataField="Pi_UM" HeaderText="单位" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                               <asp:BoundField DataField="pt_um" HeaderText="系统单位" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                           <asp:BoundField DataField="Pi_StartDate" HeaderText="开始时间" ReadOnly="True">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                              <asp:BoundField DataField="Pi_EndDate" HeaderText="截止时间" ReadOnly="True">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                           <asp:BoundField DataField="Pi_price1" HeaderText="价格1" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Pi_price2" HeaderText="价格2" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Pi_price3" HeaderText="价格3" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>

                            

                               <asp:TemplateField HeaderText="可修改">
                                <EditItemTemplate>
                                    <asp:Label ID="lblHis" Text='<%# Bind("Pi_IsHis") %>' runat="server" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlHis" runat="server">
                                       
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemTemplate>
                                    <%#Eval("Pi_IsHis")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                                </asp:CommandField>
                           
                             <asp:BoundField DataField="cust" HeaderText="客户名称" ReadOnly="True">
                                <HeaderStyle Width="180px" HorizontalAlign="Center" />
                                <ItemStyle Width="180px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="QADdesc" HeaderText="物料描述" ReadOnly="True">
                                <HeaderStyle Width="180px" HorizontalAlign="Center" />
                                <ItemStyle Width="180px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="shipto" HeaderText="最终客户名称" ReadOnly="True">
                                <HeaderStyle Width="180px" HorizontalAlign="Center" />
                                <ItemStyle Width="180px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Pi_Remark" HeaderText="备注" ReadOnly="True">
                                <HeaderStyle Width="180px" HorizontalAlign="Center" />
                                <ItemStyle Width="180px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             
                           
                                        </Columns>
                                    </asp:GridView>
    </div>
     <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
    </form>
</body>
</html>
