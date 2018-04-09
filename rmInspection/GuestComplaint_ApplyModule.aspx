<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestComplaint_ApplyModule.aspx.cs" Inherits="rmInspection_GuestComplaint_ApplyModule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>客诉-模块申请</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .LabelRight
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellspacing="2" cellpadding="2" width="600px" bgcolor="white" border="0">
            <tr> 
                <td>
                    <asp:Label ID="lblApplyModule" runat="server" Width="55px" CssClass="LabelRight" Text="申请模块:"
                        Font-Bold="False"></asp:Label>
                     <asp:DropDownList ID="ddlModule" runat="server" Width="200px" 
                        DataValueField="moduleID" DataTextField="moduleName" AutoPostBack="true" >
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="查询"
                        Width="40px" OnClick="btnQuery_Click" />&nbsp;
                    <asp:Button ID="btnApply" runat="server" CssClass="SmallButton2" TabIndex="5" Text="申请"
                        Width="40px" OnClick="btnApply_Click" />
                    <asp:Button ID="btnAgree" runat="server" CssClass="SmallButton2" TabIndex="5" Text="同意"
                        Width="40px" OnClick="btnAgree_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle" PageSize="20" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" 
            onitemcommand="gv_ItemCommand" Width="620px" DataKeyNames="ID,ModuleId,CreatedBy,CreatedTime">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="620px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="申请模块" Width="50px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="申请人" Width="300px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="申请时间" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell> 
                        <asp:TableCell Text="Del" Width="40px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns> 
                <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApprove" runat="server"/>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                    </asp:TemplateField>
                <asp:BoundField DataField="ModuledName" HeaderText="申请模块">
                    <HeaderStyle Width="340px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="340px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CreatedName" HeaderText="申请者" ReadOnly="true">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>  
                <asp:BoundField DataField="Createdtime" HeaderText="申请时间" ReadOnly="true">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>            
                <asp:CommandField ShowDeleteButton="True" DeleteText="del">
                    <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
    </div>
     <script type="text/javascript">
         <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
