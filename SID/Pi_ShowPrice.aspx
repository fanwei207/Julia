<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pi_ShowPrice.aspx.cs" Inherits="IT_Pi_ShowPrice" %>

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
     <table style="width: 1330px">
            <tr>
                <td align="left">
                    客户：<asp:TextBox 
                        ID="txtcust" runat="server" Width="60px" CssClass="Param" MaxLength="8"></asp:TextBox>
                    &nbsp; 物料号：<asp:TextBox ID="txtQAD" runat="server" Width="120px" 
                        CssClass="Param" MaxLength="14"></asp:TextBox>
                    &nbsp; 最终客户：<asp:TextBox ID="txtshipto" runat="server" Width="60px" 
                        CssClass="Param" MaxLength="8"></asp:TextBox>
                    &nbsp;开始时间：
                    <asp:TextBox ID="txtCrtDate1" runat="server" Width="70px" CssClass="Date Param"></asp:TextBox>
                    --<asp:TextBox ID="txtCrtDate2" runat="server" Width="70px" CssClass="Date Param"></asp:TextBox>
                     &nbsp;截止时间：
                    <asp:TextBox ID="txtCrtDate3" runat="server" Width="70px" CssClass="Date Param"></asp:TextBox>
                    --<asp:TextBox ID="txtCrtDate4" runat="server" Width="70px" CssClass="Date Param"></asp:TextBox>
                    
                     &nbsp;零件状态：
                     <asp:DropDownList ID="ddl_status" runat="server" Width="60px">
                        <asp:ListItem Value = "0" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value = "ACTIVE">ACTIVE</asp:ListItem>
                        <asp:ListItem Value = "NEW">NEW</asp:ListItem>
                        <asp:ListItem Value = "NOCOST">NOCOST</asp:ListItem>
                        <asp:ListItem Value = "NOCOST_A">NOCOST_A</asp:ListItem>
                        <asp:ListItem Value = "NOCOST_b">NOCOST_b</asp:ListItem>
                        <asp:ListItem Value = "NOCOST_N">NOCOST_N</asp:ListItem>
                        <asp:ListItem Value = "NOPO">NOPO</asp:ListItem>
                        <asp:ListItem Value = "stop">STOP</asp:ListItem>
                        <asp:ListItem Value = "STOPL">STOPL</asp:ListItem>
                        <asp:ListItem Value = "update">UPDATE</asp:ListItem>
                     </asp:DropDownList>
                    &nbsp;域：
                     <asp:DropDownList ID="ddl_Domain" runat="server" Width="40px">
                        <asp:ListItem Value = "0" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value = "SZX">SZX</asp:ListItem>
                        <asp:ListItem Value = "ZQL">ZQL</asp:ListItem>
                        <asp:ListItem Value = "ZQZ">ZQZ</asp:ListItem>
                        <asp:ListItem Value = "YQL">YQL</asp:ListItem>
                        <asp:ListItem Value = "HQL">HQL</asp:ListItem>
                        <asp:ListItem Value = "ATL">ATL</asp:ListItem>
                     </asp:DropDownList>
                     &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" 
                        onclick="btnSearch_Click"  />

                         <asp:Button ID="btnExcel" runat="server" Text="Excel" 
                        CssClass="SmallButton3" onclick="btnExcel_Click" 
                          />
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
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
                            <asp:BoundField DataField="Pi_DoMain" HeaderText="域" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="货币">
                                <EditItemTemplate>
                                    <asp:Label ID="lblCurrency" Text='<%# Bind("Pi_Currency") %>' runat="server" Visible="false"></asp:Label>
                                   <asp:DropDownList ID="ddlCurrency" runat="server">
                                  
                                   <asp:ListItem >USD</asp:ListItem>
                                    <asp:ListItem >RMB</asp:ListItem>
                                   <asp:ListItem >EUR</asp:ListItem>
                                   <asp:ListItem>HKD</asp:ListItem>
                                </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemTemplate>
                                    <%#Eval("Pi_Currency")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位">
                                <EditItemTemplate>
                                    <asp:Label ID="lblUM" Text='<%# Bind("Pi_UM") %>' runat="server" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="drpUM" runat="server">
                                       
                                        <asp:ListItem>SETS</asp:ListItem>
                                        <asp:ListItem>PCS</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemTemplate>
                                    <%#Eval("Pi_UM")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:BoundField DataField="pt_um" HeaderText="系统单位" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Pi_StartDate" HeaderText="开始时间" ReadOnly="True">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="截止时间" >
                                 <EditItemTemplate>
                                  <asp:TextBox ID="txtEndDate" runat="server" CssClass="SmallTextBox Date" Text='<%# Bind("Pi_EndDate") %>'
                                        Width="70px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemTemplate>
                                    <%#Eval("Pi_EndDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="价格1">
                                 <EditItemTemplate>
                                  <asp:TextBox ID="txtprice1" runat="server" CssClass="SmallTextBox" Text='<%# Bind("Pi_price1") %>'
                                        Width="60px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemTemplate>
                                    <%#Eval("Pi_price1")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="价格2">
                                 <EditItemTemplate>
                                  <asp:TextBox ID="txtprice2" runat="server" CssClass="SmallTextBox" Text='<%# Bind("Pi_price2") %>'
                                        Width="60px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemTemplate>
                                    <%#Eval("Pi_price2")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="价格3">
                                 <EditItemTemplate>
                                  <asp:TextBox ID="txtprice3" runat="server" CssClass="SmallTextBox" Text='<%# Bind("Pi_price3") %>'
                                        Width="60px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemTemplate>
                                    <%#Eval("Pi_price3")%>
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
                                <ItemStyle Width="180px" HorizontalAlign="left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="QADdesc" HeaderText="物料描述" ReadOnly="True">
                                <HeaderStyle Width="180px" HorizontalAlign="Center" />
                                <ItemStyle Width="180px" HorizontalAlign="left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="shipto" HeaderText="最终客户名称" ReadOnly="True">
                                <HeaderStyle Width="180px" HorizontalAlign="Center" />
                                <ItemStyle Width="180px" HorizontalAlign="left" />
                            </asp:BoundField>
                             
                              <asp:TemplateField HeaderText="备注">
                                 <EditItemTemplate>
                                  <asp:TextBox ID="txtRemark" runat="server" CssClass="SmallTextBox" Text='<%# Bind("Pi_Remark") %>'
                                        Width="180px" MaxLength="50"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                <ItemTemplate>
                                    <%#Eval("Pi_Remark")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
    </div>
     <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
    </form>
</body>
</html>
