<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UL_QAD.aspx.cs" Inherits="RDW_UL_QAD" %>

<!DOCTYPE html>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 163px;
        }
    </style>
</head>
<body>
     <div align="center">
          <form id="Form1" method="post" runat="server">
                <table width="600">
                    <tr>
                        <td>
                            Project Name:<asp:TextBox ID="txtProject" runat="server" Width="300px" CssClass="SmallTextBox"
                        TabIndex="1" Enabled="False"></asp:TextBox>
                            <asp:Label ID="lblId" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                        

                         </tr>
                    <%--  <tr>
                         <td>
                            QAD:<asp:TextBox ID="txtDate1" runat="server" Width="300px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                        </td>
                         
                        </tr>--%>
                 
                  
                    <tr>
                         <td colspan="2">
                           &nbsp;&nbsp; <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="9" Text="save"
                        Width="50px" CausesValidation="false" OnClick="btnSave_Click"   Visible="False" />
                                &nbsp;&nbsp; <asp:Button ID="btnback" runat="server" CssClass="SmallButton2" TabIndex="9" Text="back"
                        Width="50px" CausesValidation="false" OnClick="btnback_Click"   />

                              &nbsp;&nbsp; <asp:Button ID="btnImport" runat="server" CssClass="SmallButton2" TabIndex="9" Text="Import"
                        Width="50px" CausesValidation="false" OnClick="btnImport_Click"   />
                        </td>

                        
                    </tr>

                </table>

    <asp:GridView ID="gvRDW" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="GridViewStyle" PageSize="20" 
                Width="800px" DataKeyNames="code" 
               >
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="产品名称" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="QAD" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="描述" Width="60px" HorizontalAlign="center"></asp:TableCell>
                           
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="UL_model" HeaderText="model NO." HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="left" />
                    </asp:BoundField>

                     <asp:BoundField DataField="UL_Project" HeaderText="Project Name" HtmlEncode="False">
                        <HeaderStyle Width="500px" HorizontalAlign="Center" />
                        <ItemStyle Width="500px" HorizontalAlign="left" />
                    </asp:BoundField>

                     <asp:BoundField DataField="item_qad" HeaderText="QAD" HtmlEncode="False">
                        <HeaderStyle Width="250px" HorizontalAlign="Center" />
                        <ItemStyle Width="250px" HorizontalAlign="left" />
                    </asp:BoundField>

                   <asp:BoundField DataField="description" HeaderText="description" HtmlEncode="False">
                        <HeaderStyle Width="800px" HorizontalAlign="Center" />
                        <ItemStyle Width="800px" HorizontalAlign="left" />
                    </asp:BoundField>

                     
                </Columns>
            </asp:GridView>
    
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
