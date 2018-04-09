<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_priceList.aspx.cs" Inherits="price_pc_priceList" %>

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
        .SmallButton2
        {}
        .auto-style1 {
            width: 712px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table align="left">
            <tr align="left">
                <td>QAD：&nbsp;&nbsp;<asp:TextBox ID="txtQAD" runat="server" 
                CssClass="SmallTextBox" Width="115px" Height="20px" ></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>               
                <td>供应商：&nbsp;&nbsp;<asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox" 
                Width="115px" Height="20px" ></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 
                </td>               
                <td>供应商名称：&nbsp;&nbsp;<asp:TextBox ID="txtVenderName" runat="server" CssClass="SmallTextBox" 
                Width="115px" Height="20px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>                
                <td colspan="2">
                    <asp:Button id="btnSelect" runat="server" CssClass="SmallButton2"  Text="查询" 
                onclick="btnSelect_Click" Width="89px" Height="26px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>                
                <td>
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" Text="导出" Width="89px" Height="26px" OnClick="btnExport_Click"/>
                </td>
            </tr>
            <tr align="left">
                <td> 
                单位：&nbsp;&nbsp;<asp:DropDownList ID="ddlUmType" runat="server" 
                 DataTextField ="pc_um" DataValueField="pc_um">
                 </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                域：&nbsp;&nbsp;<asp:DropDownList ID="ddlDomain" runat="server">
                <asp:ListItem Text="all" Selected="True"></asp:ListItem>
                <asp:ListItem Text="SZX"></asp:ListItem>
                <asp:ListItem Text="ZQL"></asp:ListItem>
                <asp:ListItem Text="ZQZ"></asp:ListItem>
                <asp:ListItem Text="YQL"></asp:ListItem>
                <asp:ListItem Text="HQL"></asp:ListItem>
                </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td colspan="2">
    币种：&nbsp;&nbsp;<asp:DropDownList ID="curr" runat="server">
                <asp:ListItem Text="all" Selected="True"></asp:ListItem>
                <asp:ListItem Text="RMB"></asp:ListItem>
                <asp:ListItem Text="USD"></asp:ListItem>              
                </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:CheckBox runat ="server" Text="未终止价格" ID ="chkIsNotEnd" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:CheckBox runat ="server" Text="目前最后一条价格" ID ="chkIsNotEffect" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
       <tr>  
           <td rowspan="">

           </td>   
        <asp:GridView ID="gvPriceMstr" runat="server" AllowPaging="true" PageSize="25" AutoGenerateColumns ="false"
             CssClass="GridViewStyle"  OnPageIndexChanging="gvPriceMstr_PageIndexChanging" OnRowCommand="gvPriceMstr_RowCommand" >
          <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                 <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="720px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="QAD" Width="100px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="150px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="单位" Width="30px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="币种" Width="30px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="无税价格" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="最小值" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="含税价格" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="最大值" Width="80px"></asp:TableCell>                                   
                                    <asp:TableCell HorizontalAlign="center" Text="税率" Width="80px"></asp:TableCell>
                                    <%--<asp:TableCell HorizontalAlign="center" Text="amt" Width="80px"></asp:TableCell>--%>
                                     <asp:TableCell HorizontalAlign="center" Text="起始时间" Width="100px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="终止时间" Width="100px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="域" Width="50px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="3"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                <Columns>
                  <asp:TemplateField HeaderText="QAD">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnQadDet" runat="server" CommandName="lkbtnQadDet"   CommandArgument='<%# Eval("pc_part") %>'
                            Text='<%# Eval("pc_part") %>'></asp:LinkButton>&nbsp;&nbsp;&nbsp;                   
                    </ItemTemplate>
                  </asp:TemplateField>
                  <%--<asp:BoundField HeaderText="QAD" DataField="pc_part" >
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />                    
                        </asp:BoundField>--%>
                 <asp:TemplateField HeaderText="供应商">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnSupplierDet" runat="server" CommandName="lkbtnSupplierDet"   CommandArgument='<%# Eval("pc_list") %>'
                            Text='<%# Eval("pc_list") %>'></asp:LinkButton>&nbsp;&nbsp;&nbsp;                   
                    </ItemTemplate>
                  </asp:TemplateField>

                      <%--<asp:BoundField HeaderText="供应商" DataField="pc_list" >
                        <HeaderStyle Width="60px"/>

                      </asp:BoundField> --%>
                      <asp:BoundField HeaderText="供应商名称" DataField="ad_name" >
                        <HeaderStyle Width="150px" />
                    </asp:BoundField>
                      <asp:BoundField HeaderText="单位" DataField="pc_um" >
                        <HeaderStyle Width="30px" />
                    </asp:BoundField>
                      <asp:BoundField HeaderText="币种" DataField="pc_curr" >
                        <HeaderStyle Width="30px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="无税价格" DataField="pc_price" >
                    <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="最小值" DataField="pc_min" >
                        <HeaderStyle Width="80px" />
                    </asp:BoundField> 
                    <asp:BoundField HeaderText="含税价格" DataField="pc_price1" >
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>               
                    <asp:BoundField HeaderText="最大值" DataField="pc_max" >
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                     
                    <asp:BoundField HeaderText="税率" DataField="pc_taxes" >
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <%--<asp:BoundField HeaderText="amt" DataField="pc_amt" >
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>--%>
                      <asp:BoundField HeaderText="起始时间" DataField="pc_start" >
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                      <asp:BoundField HeaderText="终止时间" DataField="pc_expire" >
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                      <asp:BoundField HeaderText="域" DataField="pc_domain" >
                        <HeaderStyle Width="30px" />
                        <ItemStyle HorizontalAlign="Center" Width="30px" />                  
                          </asp:BoundField>
<%--                  <asp:TemplateField HeaderText="操作">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnView" runat="server" CommandName="lkbtnView" Visible="false"  CommandArgument='<%#  ((GridViewRow) Container).RowIndex %>'
                            Text="view"></asp:LinkButton>&nbsp;&nbsp;&nbsp;                   
                    </ItemTemplate>
                  </asp:TemplateField> --%>            
                
                </Columns>
        </asp:GridView>
            
        </table>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        $window()
    </script>
</body>
</html>
