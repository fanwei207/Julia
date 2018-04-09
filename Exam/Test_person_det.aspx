<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_person_det.aspx.cs" Inherits="Test_Test_person_det" %>

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
        <div align="center">
            <table cellspacing="0" cellpadding="0" style="width: 800px;">
                
                <tr>
                    
                    <td style="text-align: right; width: 50px;">公司</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="dropPlant" runat="server"  AutoPostBack="True"
                            DataTextField="description" DataValueField="plantID" TabIndex="1"
                            Style="text-align: center;">
                            <asp:ListItem Value="0">----</asp:ListItem>
                            <asp:ListItem Value="1">上海强凌电子有限公司 SZX</asp:ListItem>
                            <asp:ListItem Value="2">镇江强凌电子有限公司 ZQL</asp:ListItem>
                            <asp:ListItem Value="5">扬州强凌有限公司 YQL</asp:ListItem>
                            <asp:ListItem Value="8">淮安强陵照明有限公司  HQL</asp:ListItem>
                             
                        </asp:DropDownList>
                    <td style="width: 80px;">工号</td>
                    <td>
                        <asp:TextBox ID="txtstartdate" runat="server" Width="120px" CssClass="Param"></asp:TextBox>
                    </td>
                  

                  
                    <td rowspan="2" style="text-align:left;width:200px;" >
                          <asp:Button ID="btnsearch" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnsearch_Click"/>
                        <asp:Button ID="btnnew" runat="server" Text="新增" CssClass="SmallButton3" OnClick="btnnew_Click"/>
                          <asp:Button ID="btnback" runat="server" Text="返回" CssClass="SmallButton3" OnClick="btnback_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowCommand="gv_RowCommand" DataKeyNames="Person_UserId" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" Width="1000px">
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
                      <asp:BoundField HeaderText="工号" DataField="loginName" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    
                 
                    <asp:BoundField HeaderText="名称" DataField="userName" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    
                    <asp:BoundField HeaderText="状态" DataField="status" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="删除"><%--17--%>
                        <ItemTemplate>
                            <asp:LinkButton ID="linkClose" CssClass="close" runat="server" CommandName="close" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>Cancel</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>

                   
                   

                    <asp:BoundField HeaderText="创建人" DataField="Person_CreatedBy" ReadOnly="True"><%--9--%>
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="创建时间" DataField="Person_CreatedDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>