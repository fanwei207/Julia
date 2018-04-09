<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new_DedAss.aspx.cs" Inherits="new_DedAss" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
    <form id="form1" runat="server">
        <table  id="table1" cellspacing="0" cellpadding="0">
            <tr>
                <td align="left" >
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    扣分类型：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>                    
                    <asp:Label ID="showName" runat="server" Text=" "></asp:Label>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" 
                        Width="50px" onclick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 534px">
                    备注:<asp:TextBox ID="txtNote" runat="server" Width="465px" CssClass="SmallTextBox" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="showMessage" runat="server" Visible="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:Button ID="btnAdd" runat="server" Text="增加" CssClass="SmallButton3" 
                        Width="50px" onclick="btnAdd_Click" />                        
                    <asp:Button ID="btnupdate" runat="server" Text="保存" CssClass="SmallButton3" Visible="false"
                        Width="50px" onclick="btnUpdate_Click" />
                    &nbsp;<asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="SmallButton3" 
                        Width="50px" onclick="btnCancel_Click"/>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="showType" runat="server" CssClass="GridViewStyle" 
            AutoGenerateColumns="False" DataKeyNames="perfd_id" 
            OnRowcommand="gv_RowCommand" OnRowdatabound="gv_RowDataBound"  AllowPaging="True"
            OnRowdeleting="showType_RowDeleting" PageSize="5" 
            OnPageindexchanging="showType_PageIndexChanging">
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
            <asp:BoundField HeaderText="扣分类型" DataField="perfd_type" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="300px" />
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="备注" DataField="perfd_desc" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="300px" />
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" Text="修改" ForeColor="black" Font-Underline="true"
                        Font-Size="12px" runat="server" CommandArgument='<%# Eval("perfd_id") %>' CommandName="ModifyDesc" />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lnkDelete" Text="删除" ForeColor="black" Font-Underline="true"
                        Font-Size="12px" runat="server" CommandArgument='<%# Eval("perfd_id") %>' CommandName="Delete" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
    </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>

</body>
</html>