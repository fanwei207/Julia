<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_step.aspx.cs" Inherits="RD_WorkFlow.RDW_rdw_step" %>

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
                        <asp:Label ID="lblProject" runat="server" CssClass="LabelRight" Text="" Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Width="60px" CssClass="SmallButton2" Text="New Step"
                            Visible="True" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="Back" Width="40px"
                            OnClick="btnBack_Click" />
                        <asp:Label ID="lbMID" runat="server" Text="0" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvRDW" runat="server" Width="980" AllowPaging="false" AllowSorting="True"
                AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild" DataKeyNames="StepID"
                OnPageIndexChanging="gvRDW_PageIndexChanging" OnRowCommand="gvRDW_RowCommand"
                OnRowDataBound="gvRDW_RowDataBound" OnRowDeleting="gvRDW_RowDeleting" PageSize="18">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Step" HeaderText="No.">
                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkProName" runat="server" CommandArgument='<%# Bind("StepId") %>'
                                CommandName="Desc" Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black"
                                Text='<%# Bind("StepName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Duration" HeaderText="Duration" >
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="False" Width="50px" />
                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Member">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkPartner" runat="server" CommandName="PARTNER" Font-Bold="False"
                                Font-Size="8pt" Font-Underline="False" ForeColor="Black" Text='<%# Bind("StepPartner") %>'
                                CommandArgument='<%# Bind("StepId") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approver" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkViewer" runat="server" CommandName="AUDIT_BY" Font-Bold="False"
                                Font-Size="8pt" Font-Underline="False" ForeColor="Black" Text='<%# Bind("StepMember") %>'
                                CommandArgument='<%# Bind("StepId") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Step" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkTask" runat="server" CausesValidation="False" CommandArgument='<%# Bind("StepId") %>'
                                CommandName="Task" Font-Bold="False" Font-Size="8pt" ForeColor="Black" Text="SubStep"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" />
                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="50px" />
                        <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
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
                                CommandArgument='<%# Bind("StepId") %>' Font-Underline="False" ForeColor="Black">↑</asp:LinkButton>
                            &nbsp; &nbsp;<asp:LinkButton ID="linkDown" runat="server" CommandName="DOWN" Font-Bold="False"
                                CommandArgument='<%# Bind("StepId") %>' Font-Size="8pt" Font-Strikeout="False"
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
                            <asp:TableCell Text="Duration" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="Step" Width="50px"></asp:TableCell>
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
