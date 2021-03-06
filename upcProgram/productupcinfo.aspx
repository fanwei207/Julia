<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.productUPCInfo" CodeFile="productUPCInfo.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    </head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="700" bgcolor="white" border="0">
            <tr>
                <td>
                    <asp:Label ID="lblTxt1" runat="server" Height="20px" Text="产品类型名称："></asp:Label>
&nbsp;<asp:TextBox ID="txtType" runat="server" Height="20px" Width="300px"></asp:TextBox>
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="SmallButton3" />                   
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton3" Text="返回" />
                    </td>
                
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="700" align="center" bgcolor="white"
            border="0">
            <tr width="100%">
                <td>
                    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnCancelCommand="Edit_cancel" OnUpdateCommand="Edit_update" 
                        CssClass="GridViewStyle" AllowPaging="True" PageSize ="10">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn DataField="gsort" SortExpression="gsort" ReadOnly="True" HeaderText="编号">
                                <HeaderStyle Width="50px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="name" SortExpression="name" HeaderText="产品类型名称">
                                <HeaderStyle Width="530px"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            
                            <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" HeaderText="编辑"
                                CancelText="<u>取消</u>" EditText="<u>编辑</u>">
                                <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="75px"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            </asp:EditCommandColumn>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                            </asp:BoundColumn>
                         <asp:ButtonColumn Text="删除" CommandName="DeleteClick" HeaderText="删除">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="75px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                            
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr> 
               <tr>
                <td height="20">
                    
                    包装类型名称： 
                     <asp:TextBox ID="txtPack" runat="server" Height="20px" Width="300px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAddPackType" runat="server" CssClass="SmallButton3" 
                        Text="添加" />
                </td>
            </tr>
            <tr width="100%">
                <td>
                    <asp:DataGrid ID="Datagrid2" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnCancelCommand="Edit_cancel2" OnUpdateCommand="Edit_update2" 
                        CssClass="GridViewStyle " AllowPaging="True" PageSize="10">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn DataField="gsort" SortExpression="gsort" ReadOnly="True" HeaderText="编号">
                                <HeaderStyle Width="50px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="name" SortExpression="name" HeaderText="包装类型名称">
                                <HeaderStyle Width="530px"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="保存" HeaderText="编辑" CancelText="取消"
                                EditText="编辑">
                                <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="75px"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            </asp:EditCommandColumn>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="删除" CommandName="DeleteClick" HeaderText="删除">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="75px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
