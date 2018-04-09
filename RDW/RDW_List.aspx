<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_List.aspx.cs" Inherits="RDW_List" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table width="1100px" bgcolor="white" border="0">
            <tr>
                <td>
                    Project Category<asp:DropDownList ID="dropCatetory" runat="server" DataTextField="cate_name"
                        DataValueField="cate_id" Width="80px">
                    </asp:DropDownList>
                </td>
                <td>
                    Project Name<asp:TextBox ID="txtProject" runat="server" Width="80px" TabIndex="1"
                        CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    Project Code<asp:TextBox ID="txtProjectCode" runat="server" Width="50px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    Qad No.<asp:TextBox ID="txtQad" runat="server" Width="60px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    Status<asp:DropDownList ID="ddlStatus" runat="server" Width="100px" TabIndex="4"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        <asp:ListItem Value="0">Show expired step</asp:ListItem>
                        <asp:ListItem Value="4">Show no expired & no completed steps</asp:ListItem>
                        <asp:ListItem Value="2">Show all steps</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Start Date<asp:TextBox ID="txtStartDate" runat="server" Width="60px" TabIndex="3"
                        CssClass="SmallTextBox EnglishDate"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="dropSKU" runat="server" DataTextField="SKU" DataValueField="SKU"
                        Width="100px" Visible="false" >
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                        Width="40px" OnClick="btnQuery_Click" />
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" TabIndex="7" Text="Export"
                        ToolTip="project summary" Width="40px" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvRDW" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" PageSize="20" OnPreRender="gvRDW_PreRender" DataKeyNames="RDW_MstrID,RDW_DetID"
            OnRowDataBound="gvRDW_RowDataBound" OnPageIndexChanging="gvRDW_PageIndexChanging"
            Width="1500px" OnRowCommand="gvRDW_RowCommand">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1200px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Project" Width="13%" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Project Code" Width="12%" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Step No." Width="3%" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Current Step" Width="10%" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Member" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Approver" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Start Date" Width="7.5%" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="End Date" Width="7.5%" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="SKU#" Width="12.5%" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Description" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Detail" Width="4%" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="RDW_Project" HeaderText="Project">
                    <HeaderStyle Width="160px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="160px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_ProdCode" HeaderText="Project Code">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_Category" HeaderText="Project Categpry">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_TaskID" HeaderText="Step No.">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_CurrStep" HeaderText="Current Step">
                    <HeaderStyle Width="180px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_StartDate" HeaderText="Start Date" Visible="false">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_EndDate" HeaderText="End Date">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_FinishDate" HeaderText="Finish Date">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField Text="View" HeaderText="Doc" CommandName="gobom" ControlStyle-Font-Underline="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:ButtonField Text="View" HeaderText="Detail" CommandName="Detail" ControlStyle-Font-Underline="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:BoundField DataField="RDW_Ptr" HeaderText="Member">
                    <HeaderStyle Width="180px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_Mbr" HeaderText="Approver" Visible="false">
                    <HeaderStyle Width="180px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_Creater" HeaderText="Creator" Visible="false">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_CreatedDate" HeaderText="Finish Date" Visible="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_ProdSku" HeaderText="SKU#">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_ProdDesc" HeaderText="Description">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
