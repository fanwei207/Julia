<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.NWF_FlowNodePerson" CodeFile="NWF_FlowNodePerson.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" runat="server">
    <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
        <tr>
            <td align="right">
                <asp:Button ID="btnBack" TabIndex="0" runat="server" Text="返回" CssClass="SmallButton3"
                    Width="60"></asp:Button>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                选择审批人员：
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txb_docid" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txb_chooseid" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txb_choose" runat="server" Width="300px"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="保存" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="450" colspan="2" style="height: 20px">
                公司<asp:DropDownList ID="ddl_Company" runat="server" Width="200px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="450" colspan="2">
                部门<asp:DropDownList ID="ddl_department" runat="server" Width="180px" AutoPostBack="True">
                </asp:DropDownList>
                人员<asp:TextBox ID="txb_user" CssClass="SmallTextBox" Width="80px" runat="server"></asp:TextBox>
                <asp:Button ID="btn_search" CssClass="SmallButton3" runat="server" Text="查询"></asp:Button>
            </td>
        </tr>
        <tr>
            <td>
                <p style="color: #ff0000">
                    注:部门和人员至少存在一个条件,"人员"查询是模糊匹配!</p>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="300px" Height="300px"
                    BorderWidth="1">
                    <asp:CheckBoxList ID="cbl_user" runat="server" AutoPostBack="True">
                    </asp:CheckBoxList>
                </asp:Panel>
            </td>
        </tr>
<%--        <tr>
            <td align="right">
                <asp:Button ID="Button1" TabIndex="0" runat="server" Text="关闭窗口" CssClass="SmallButton3"
                    Width="60"></asp:Button>&nbsp;
            </td>
        </tr>--%>
    </table>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
