<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_ApplyPersonList.aspx.cs" Inherits="price_pc_ApplyPersonList" %>

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
    <form id="form1" runat="server">
    <div align="center">
    <asp:Button  ID="btnAdd" runat="server" CssClass="SmallButton2" Text="添加报价申请负责人" 
            onclick="btnAdd_Click" Width="120px"/>
        <asp:GridView ID="gvInfo" runat="server"  AutoGenerateColumns="false" 
            CssClass="GridViewStyle" onrowcommand="gvInfo_RowCommand" 
            onrowdatabound="gvInfo_RowDataBound">
              <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                 <asp:BoundField HeaderText="工号" DataField="loginName">
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="姓名" DataField="userName">
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="Email" DataField="Email">
                    <HeaderStyle Width="300px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="删除">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnDelete" runat="server" CommandName="lkbtnDelete" CommandArgument='<%# Eval("userID") %>'
                            Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
         </asp:GridView>
    </div>
    </form>
     <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
