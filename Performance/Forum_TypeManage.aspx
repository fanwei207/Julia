<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forum_TypeManage.aspx.cs" Inherits="Performance_Forum_TypeManage" %>

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
        <div>
            <asp:CheckBox ID="chkIsAll" runat="server" Text="显示全部" AutoPostBack="true" 
                oncheckedchanged="chkIsAll_CheckedChanged" />
            &nbsp;&nbsp;
            <asp:Button  ID="btnAdd" runat="server" Text="新增类型" CssClass=" SmallButton2" 
                onclick="btnAdd_Click"/>
            &nbsp;&nbsp;
            <asp:Button id="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" CssClass=" SmallButton2"/>
        </div>
        <div>
        </div>
        <div>
            <asp:GridView ID="gvInfo" runat="server" AllowPaging="true" PageSize="15" 
                AutoGenerateColumns="false" DataKeyNames="fst_typeId"
             CssClass=" GridViewStyle" EmptyDataText="No Data" 
                onpageindexchanging="gvInfo_PageIndexChanging" 
                onrowcommand="gvInfo_RowCommand" onrowdatabound="gvInfo_RowDataBound">
             <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
               <asp:BoundField DataField="fst_typeName" HeaderText="类型名称">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                  <asp:TemplateField HeaderText="删除">
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnDelete" runat="server" CommandName="lkbtnDelete" CommandArgument='<%# Eval("fst_typeId") %>'><u>删除</u></asp:LinkButton>
                        &nbsp;
                    </ItemTemplate>
                    <HeaderStyle Width="50px"  />
                    <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="是否取消" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbIsCancel" runat="server" Text='<%# Eval("fst_isTypeCancel") %>'></asp:Label>
                        &nbsp;
                    </ItemTemplate>
                    <HeaderStyle Width="50px"  />
                    <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:TemplateField>
            </Columns>
            
            </asp:GridView>
        
        
        </div>
    </div>
    </form>
     <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
