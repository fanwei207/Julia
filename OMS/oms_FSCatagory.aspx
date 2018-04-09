<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oms_FSCatagory.aspx.cs" Inherits="Factory_Status_Factory_Status_Type_Maintainance" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Factory_Status类型维护</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table  style="background-color: White;
            " border="0">
    <tr>
    <td>
    Catagory&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name:&nbsp;&nbsp;
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
&nbsp;</td>
    <td>
        <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="Add" 
            onclick="btnAdd_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" 
            CssClass="SmallButton3" Text="Search" onclick="btnSearch_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton3" 
            Text="Cancel" onclick="btnCancel_Click" />
    </td>
    </tr>
    <tr>
    <td>
        Category&nbsp; Description:<asp:TextBox ID="txtDesc" 
            runat="server" Width="300px"></asp:TextBox>
        <asp:Label ID="lblCaId" runat="server" Text="0" Visible="False"></asp:Label>
    </td>
    </tr>
    </table>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
            CssClass="GridViewStyle" DataKeyNames="id" onrowcommand="gv_RowCommand" 
            AllowPaging="True" onpageindexchanging="gv_PageIndexChanging">
    <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1"  CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Category Name" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Category Description" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Edit" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Delete" Width="200px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>

            <asp:BoundField DataField="tp" HeaderText="Category Name">
             <HeaderStyle Width="200px" HorizontalAlign="Center" />
             <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:BoundField>

             <asp:BoundField DataField="tpDesc" HeaderText="Category Description">
             <HeaderStyle Width="200px" HorizontalAlign="Center" />
             <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:BoundField>


            <asp:TemplateField HeaderText="Edit">
            <HeaderStyle Width="150px" HorizontalAlign="Center" />
             <ItemStyle Width="150px" HorizontalAlign="Center" />
                <ItemTemplate >
                <asp:LinkButton ID="Edit1" runat="server" Font-Bold="False" Font-Size="12px"
                  CommandName="Edit1" Font-Underline="True" Text="Edit"  ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
            <HeaderStyle Width="150px" HorizontalAlign="Center" />
             <ItemStyle Width="150px" HorizontalAlign="Center" />
                <ItemTemplate >
                <asp:LinkButton ID="Delete1" runat="server" Font-Bold="False" Font-Size="12px"
                            CommandName="Delete1" Font-Underline="True" Text="Delete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            </Columns>
    </asp:GridView>
    </div>    
    </form>
</body>
</html>
