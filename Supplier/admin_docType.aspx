<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_docType.aspx.cs" Inherits="Supplier_admin_docType" %>

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
    <div align="center">
    
    <table style="width: 730px" cellpadding="0" cellspacing="0">
            <tr valign="middle">
             
                <td align="left" style="width: 534px">
                 格式代码：
                   <asp:TextBox ID="txbExt" runat="server" Width="154px" CssClass="SmallTextBox"
                        MaxLength="30"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" onclick="btnSearch_Click" 
                       
                        />
                    &nbsp;
                </td>
             </tr>
             <tr>
              <td align="left" style="width: 534px">
                 格式名称：
                   <asp:TextBox ID="txbName" runat="server" Width="154px" CssClass="SmallTextBox"
                        MaxLength="30"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Button ID="btnAdd" runat="server" Text="增加" CssClass="SmallButton3" 
                        Width="50px" onclick="btnAdd_Click" 
                         />
                         <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton3" Visible="false"
                        Width="50px" onclick="btnSave_Click"   />
                    <asp:Button ID="btnback" runat="server" Text="取消" CssClass="SmallButton3"
                        Width="50px" onclick="btnback_Click"   />
                </td>
             </tr>
         </table>
         <asp:GridView ID="gv" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="25" CssClass="GridViewStyle" runat="server" Width="730px" 
            onpageindexchanging="gv_PageIndexChanging" onrowcommand="gv_RowCommand" 
           
            
            >
           <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                        <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />

            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="730px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="格式代码" Width="80px"></asp:TableCell>
                     
                        <asp:TableCell HorizontalAlign="center" Text="格式后缀" Width="300px"></asp:TableCell>
                       
                       
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="没有记录" ColumnSpan="5"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>

                 <asp:BoundField HeaderText="格式代码" DataField="doc_ext" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>

                <asp:BoundField HeaderText="格式后缀" DataField="doc_name" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
               
                

               <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="ltnshowdetupde" Text="<u>修改</u>"  Font-Size="12px" runat="server"
                            CommandArgument='<%# Eval("doc_id") %>' CommandName="myupdate" />
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="ltnshowdetdele" Text="<u>删除</u>"  Font-Size="12px" runat="server"
                            CommandArgument='<%# Eval("doc_id") %>' CommandName="mydelete" />
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
               
               
            </Columns>
        </asp:GridView>
    </div>
      <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </form>
</body>
</html>

