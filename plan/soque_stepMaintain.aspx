<%@ Page Language="C#" AutoEventWireup="true" CodeFile="soque_stepMaintain.aspx.cs"
    Inherits="plan_soque_stepMaintain" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
        <div style="width: 1000px; margin: 0 auto;">
            <p style="text-align: left; margin: 0; padding: 5px; background-image: url(../images/banner01.jpg);">
                Detail<asp:TextBox ID="txbStep" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" Text="Company"></asp:Label>
                <asp:DropDownList ID="dropPlants" runat="server" Width="250px">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="User No"></asp:Label>
                <asp:TextBox ID="txbUserNo" runat="server" Width="70px" CssClass="SmallTextBox"></asp:TextBox>
                &nbsp;Region<asp:DropDownList ID="dropRegion" runat="server" Width="120px" 
                    DataTextField="code_cmmt" DataValueField="code_value">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Query" CssClass="SmallButton2" Width="70px"
                    OnClick="btnSearch_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text="New" CssClass="SmallButton2" Width="70px"
                    OnClick="btnUpdate_Click" />
            </p>
            <asp:GridView ID="gvSteps" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
                Width="1040px" AllowPaging="True" AllowSorting="True" PageSize="25" OnRowDataBound="gvSteps_RowDataBound"
                OnRowCommand="gvSteps_RowCommand" OnPageIndexChanging="gvSteps_PageIndexChanging">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="1000px" CellPadding="0" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="Detail" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="User No" Width="80px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="User Name" Width="90px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="Email" Width="160px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="Company" Width="170px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="Creater" Width="60px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="Create Date" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="Modify" Width="60px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="Modify Date" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="Edit" Width="30px" HorizontalAlign="Center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="stepID" Visible="False" HeaderText="ID"></asp:BoundField>
                    <asp:BoundField DataField="stepName" HeaderText="Detail">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="userNo" HeaderText="User No">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="userName" HeaderText="User Name">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="email" HeaderText="Email">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="plantCode" Visible="False" HeaderText="¹«Ë¾ID"></asp:BoundField>
                    <asp:BoundField DataField="plantName" HeaderText="Company">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="170px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="170px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="createdName" HeaderText="Creater">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="createdDate" HeaderText="Create Date">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="region" HeaderText="Region">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="linkedit" Text="<u>Edit</u>" ForeColor="Blue" Font-Underline="true"
                                Font-Size="12px" runat="server" CommandArgument='<%# Eval("stepID") %>' CommandName="myEdit" />
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="false" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:Label ID="labStepID" runat="server" Text="0" Visible="false"></asp:Label>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
