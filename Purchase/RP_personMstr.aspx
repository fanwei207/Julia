<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RP_personMstr.aspx.cs" Inherits="Purchase_RP_personMstr" %>

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
                    
                    <td style="text-align: right; width: 50px;">工厂</td>
                    <td style="text-align: left;">
                      
                        <asp:DropDownList ID="dropPlant" runat="server" Width="250px" AutoPostBack="True"
                             TabIndex="1" 
                            Style="text-align: center;" OnSelectedIndexChanged="dropPlant_SelectedIndexChanged" >
                            <asp:ListItem Value="0">--请选择一个公司--</asp:ListItem>
                            <asp:ListItem Value="1">上海强凌电子有限公司 SZX</asp:ListItem>
                            <asp:ListItem Value="2">镇江强凌电子有限公司 ZQL</asp:ListItem>
                            <asp:ListItem Value="5">扬州强凌有限公司 YQL</asp:ListItem>
                            <asp:ListItem Value="8">淮安强陵照明有限公司  HQL</asp:ListItem>
                              
                        </asp:DropDownList>
                   
                    <td>部门

                    </td>
                    <td>
                            <asp:DropDownList ID="ddlStatu" runat="server" Width="108px" CssClass="Param" DataTextField="name" DataValueField ="departmentID">
                            
                        </asp:DropDownList>
                       
                    </td>   
                      <td>

                    </td>
                    <td>
                             
                    </td>   
                    <td rowspan="2" style="text-align:left;width:200px;" >
                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnQuery_Click" />
                        <asp:Button ID="btnnew" runat="server" Text="新增" CssClass="SmallButton2" OnClick="btnnew_Click"/>   
                    </td>   
                </tr>
            </table>
        
             <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowCommand="gv_RowCommand" DataKeyNames="id" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" Width="800px">
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
                   
                    <asp:BoundField HeaderText="域" DataField="plantcode" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="部门" DataField="departmentname" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>
                   

                
                    <asp:TemplateField HeaderText="人员明细">
                        <ItemStyle HorizontalAlign="Center" Width="40" Font-Underline="True" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDetail" runat="server" Text='Detail'
                                CommandName="Detail"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="40"></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="删除"><%--17--%>
                        <ItemTemplate>
                            <asp:LinkButton ID="linkClose" CssClass="close" runat="server" CommandName="close" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>Cancel</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>


                  

                   
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>