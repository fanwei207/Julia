<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m5_valid_config.aspx.cs" Inherits="product_m5_valid_config" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="验证项目："></asp:Label>
                </td>
                <td colspan="5">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>公司：</td>
                <td>
                    <asp:DropDownList ID="ddlCompany" runat="server"  Width="270" 
                    DataTextField="description" DataValueField="plantID" 
                    onselectedindexchanged="ddlCompany_SelectedIndexChanged" 
                    AutoPostBack="True"></asp:DropDownList>
                </td>
                <td>部门：</td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server"  Width="140px" 
                    DataTextField="name" DataValueField="departmentID"></asp:DropDownList>
                </td>
                <td>工号：</td>
                <td>
                    <asp:TextBox ID="txtUser" runat="server" Style="width:100px;" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton2" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>

        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" 
        DataKeyNames="m5vu_validid,m5vu_userid" onrowdeleting="gv_RowDeleting">
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
            <asp:BoundField HeaderText="公司" DataField="company">
                <HeaderStyle Width="80px" />
                <ItemStyle Width="80px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="部门" DataField="deptName">
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="工号" DataField="m5vu_userNo">
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="姓名" DataField="m5vu_userName">
                <HeaderStyle Width="90px" />
                <ItemStyle Width="90px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ControlStyle Font-Bold="False" Font-Size="12px" />
            </asp:CommandField> 
        </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="true"></asp:Literal>
    </script>
</body>
</html>
