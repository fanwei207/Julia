<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_StandardStep.aspx.cs" Inherits="RDW_RDW_StandardStep" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
            <table cellspacing="2" cellpadding="2" width="850" bgcolor="white" border="0">
                <tr>                 
                    <td style="width: 700px">
                        <asp:DropDownList ID="ddlMstr" runat="server" AutoPostBack="true" DataValueField="RDW_Id" DataTextField="RDW_Name" OnSelectedIndexChanged="ddlMstr_SelectedIndexChanged"></asp:DropDownList>                       
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Width="60px" CssClass="SmallButton2" Text="New Step"
                            Visible="True" OnClick="btnAdd_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvRDW" runat="server" Width="980" AllowPaging="false" AllowSorting="True"
                AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild" DataKeyNames="RDW_Code,RDW_StepName"
                OnPageIndexChanging="gvRDW_PageIndexChanging" OnRowCommand="gvRDW_RowCommand"
                OnRowDataBound="gvRDW_RowDataBound" OnRowDeleting="gvRDW_RowDeleting" PageSize="18">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <Columns>
                    <asp:BoundField DataField="RDW_TaskID" HeaderText="No.">
                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkProName" runat="server" CommandArgument='<%# Bind("RDW_Code") %>'
                                CommandName="Desc" Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black"
                                Text='<%# Bind("RDW_StepName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                   
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDelete" runat="server" CommandName="Delete" Font-Bold="False"
                                Font-Size="8pt" Font-Underline="True" ForeColor="Black" Text="Del"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="50px" />
                        <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="linkUp" runat="server" CommandName="UP" Font-Bold="False" Font-Size="8pt"
                                CommandArgument='<%# Bind("RDW_Code") %>' Font-Underline="False" ForeColor="Black">↑</asp:LinkButton>
                            &nbsp; &nbsp;<asp:LinkButton ID="linkDown" runat="server" CommandName="DOWN" Font-Bold="False"
                                CommandArgument='<%# Bind("RDW_Code") %>' Font-Size="8pt" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Black">↓</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                        GridLines="Vertical" Width="980px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="center" Text="No." Width="60px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="Description" Width="300px"></asp:TableCell>
                           
                            <asp:TableCell HorizontalAlign="center" Text="Del" Width="50px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="↑↓" Width="50px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            </asp:GridView>

        </form>
        <script language="javascript" type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
</body>
</html>

