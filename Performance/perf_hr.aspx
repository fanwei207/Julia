<%@ Page Language="C#" AutoEventWireup="true" CodeFile="perf_hr.aspx.cs" Inherits="Performance_perf_hr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
 
</head>
<body>
    <form id="form1" runat="server">
        <div align="left">
            <table cellspacing="0" cellpadding="0" style="width: 1100px;">
                
                <tr>
                    
                     <td style="text-align: right; width: 50px;">公司</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="dropPlant" runat="server"  AutoPostBack="True"
                            DataTextField="description" DataValueField="plantID" TabIndex="1"
                            Style="text-align: center;">
                           
                            <asp:ListItem Value="1">上海强凌电子有限公司 SZX</asp:ListItem>
                            <asp:ListItem Value="2">镇江强凌电子有限公司 ZQL</asp:ListItem>
                            <asp:ListItem Value="5">扬州强凌有限公司 YQL</asp:ListItem>
                            <asp:ListItem Value="8">淮安强陵照明有限公司  HQL</asp:ListItem>
                             
                        </asp:DropDownList>
                        </td>
                 
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddldept" runat="server"  AutoPostBack="True"
                            DataTextField="description" DataValueField="plantID" TabIndex="1"
                            Style="text-align: center;">
                            <asp:ListItem Value="0">车间线长</asp:ListItem>
                            <asp:ListItem Value="1">科室中层</asp:ListItem>
                            <asp:ListItem Value="2">车间中层</asp:ListItem>
                             
                        </asp:DropDownList>
                        </td>
                    <td>开始时间

                    </td>
                    <td>
                        <asp:TextBox ID="txtstart" runat="server" CssClass="Date Param"></asp:TextBox>
                          
                       
                    </td>   
                      <td>结束时间

                    </td>
                    <td>
                              <asp:TextBox ID="txtend" runat="server" CssClass="Date Param"></asp:TextBox>
                    </td>   
                    <td rowspan="2" style="text-align:left;width:200px;" >
                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnQuery_Click" />
                        <asp:Button ID="btnnew" runat="server" Text="Excel" CssClass="SmallButton2" OnClick="btnnew_Click"/>   
                    </td>   
                </tr>
            </table>
        
             <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowCommand="gv_RowCommand" DataKeyNames="" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" Width="1100px">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />    
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="100%" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Width="100%" HorizontalAlign="center">无数据</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                   
                    <asp:BoundField HeaderText="部门/产线" DataField="name" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="姓名" DataField="userName" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="总扣分" DataField="MARKs" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="部门人数" DataField="num" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="处罚" DataField="punish" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                 
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>