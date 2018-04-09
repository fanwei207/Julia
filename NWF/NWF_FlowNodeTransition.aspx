<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NWF_FlowNodeTransition.aspx.cs"
    Inherits="NWF_FlowNodeTransition" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <input id="hidNbr" type="hidden" runat="server" />
    <div align="center">
        <table style="width: 400px">
            <tr>
                <td align="left">
                    模板：<asp:Label ID="lblWorkFlowName" runat="server"></asp:Label>
                    <asp:Label ID="lblWorkFlowId" runat="server" Visible="False"></asp:Label>
                </td>
                <td align="right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left">
                    前置：<asp:DropDownList 
                        ID="dropPreNode" runat="server" Width="100px" DataTextField="Node_Name" 
                        DataValueField="Node_Id">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; 后置：<asp:DropDownList ID="dropNextNode" runat="server" 
                        Width="100px" DataTextField="Node_Name" DataValueField="Node_Id">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="保存" 
                        onclick="btnSave_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        PageSize="20" DataKeyNames="PrevNode,NextNode" 
                        OnRowDataBound="gv_RowDataBound" Width="400px" onrowdeleting="gv_RowDeleting">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="770px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="前置步骤" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="后置步骤" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PrevNodeName" HeaderText="前置步骤">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NextNodeName" HeaderText="后置步骤">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True">
                            <ControlStyle Font-Bold="False" Font-Underline="True" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
