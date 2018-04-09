<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mold_DocList.aspx.cs" Inherits="Purchase_Mold_DocList" %>

<!DOCTYPE html>

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
           <asp:Button  ID ="btnReturn" runat="server"  Text="return" CssClass="SmallButton2" Width="80px" OnClick="btnReturn_Click"/>
         <asp:GridView ID="gvDoc" runat="server"  PageSize="13" AutoGenerateColumns="False"
                CssClass="GridViewStyle" DataKeyNames="id" OnRowCommand="DgDoc_RowCommand">
                <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                     <asp:BoundField HeaderText="filename" DataField="filename">
                        <HeaderStyle Width="300px" HorizontalAlign="Center" />
                        <ItemStyle Width="300px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="typeName" DataField="typename">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="cateidName" DataField="catename">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField> 
                    <asp:BoundField HeaderText="updateType" DataField="updateDocType">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="updateDate" DataField="updateDocDate"  DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                  
                    <asp:TemplateField HeaderText="view">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"/>
                    <ItemStyle Width="50px" HorizontalAlign="Center" Font-Underline="true"/>
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbview" runat="server" CommandArgument='<%# Bind("id") %>'
                        CommandName="lkbview" Font-Underline="True" Text="view"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                </Columns>
             </asp:GridView>
     
    </div>
    </form>
     <script type="text/javascript">
         <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
