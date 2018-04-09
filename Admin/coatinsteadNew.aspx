<%@ Page Language="C#" AutoEventWireup="true" CodeFile="coatinsteadNew.aspx.cs" Inherits="coatinsteadNew" %>

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
        <table width="860">
            <tr>
                <td>
                    <asp:Label ID="lb_num" Text="工号:" runat="server"></asp:Label>
                    <asp:TextBox runat="server" Width="100px" ID="txt_userno"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label1" Text="姓名:" runat="server" Width="40px"></asp:Label>
                    <asp:TextBox runat="server" Width="120px" ID="txt_name" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label2" Text="部门:" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddl_department" runat="server" Width="120px"  DataTextField="name" DataValueField="departmentID" >
                        <asp:ListItem Value="0" Selected="True">--请选--</asp:ListItem>
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label3" Text="服装类型:" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddl_coattype" runat="server" Width="90px" DataTextField="name" DataValueField="userUniformTypeID" >
                        <asp:ListItem Value="0" Selected="True">--请选--</asp:ListItem>
                        <asp:ListItem Value="1">长夹克</asp:ListItem>
                        <asp:ListItem Value="2">短夹克</asp:ListItem>
                        <asp:ListItem Value="3">白大褂</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                        <asp:CheckBox ID="ck_leaveperson" runat="server" Text="包括离职员工" />
                </td>
                <td>
                    <asp:Button ID="btn_Query" runat="server" Text="查询" Width="50px" CssClass="SmallButton3" 
                        onclick="btn_search_Click" />
                </td>
                <td>
                    <asp:Button ID="btn_export" runat="server" Text="导出" Width="50px" CssClass="SmallButton3" 
                        onclick="btn_export_Click" />
                </td>
            </tr>
        </table>
        <table  border="1" width="860" >
             <tr cellpadding="6" cellspacing="1" border="1" width="860">
            <td>
                <asp:Label ID="Label4" Text="工号:" runat="server"></asp:Label>
                <asp:TextBox runat="server" Width="100px" ID="txt_appuserno" 
                    ontextchanged="txt_numadd_TextChanged" AutoPostBack="true"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label8" Text="姓名:" runat="server" Width="40px"></asp:Label>
                <asp:TextBox runat="server" Width="120px" ID="txt_appname" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label5" Text="申领日期:" runat="server"></asp:Label>
                <asp:TextBox runat="server" Width="100px" ID="txt_Appdate" CssClass="smalltextbox Date Date"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label6" Text="服装类型:" runat="server"></asp:Label>
                <asp:DropDownList ID="ddl_coattypeadd" runat="server" Width="90px" DataTextField="name" DataValueField="userUniformTypeID">
                    <asp:ListItem Value="0" Selected="True">--请选--</asp:ListItem>
                    <asp:ListItem Value="1">长夹克</asp:ListItem>
                    <asp:ListItem Value="2">短夹克</asp:ListItem>
                    <asp:ListItem Value="3">白大褂</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label7" Text="数量:" runat="server"></asp:Label>
                <asp:TextBox ID="txt_count" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td align="left">
                <asp:Button ID="btn_add" runat="server" Text="增加" CssClass="SmallButton3"  Width="50px"
                    onclick="btn_add_Click"></asp:Button>
            </td>
        </tr>
        </table>
        <tr>
        <td></td>
        </tr>
        <asp:GridView ID="gv_coatdetail" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="860px" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting"
            OnRowEditing="gv_RowEditing" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="userUniformDetailID" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="工号" DataField="loginname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="username" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="deparmentName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="申领日期" DataField="uniformDate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="服装类型" DataField="typename" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="件数" DataField="num" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="True" HeaderText="Delete"><%--16--%>
                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:CommandField>
                <asp:BoundField HeaderText="录入人" DataField="createname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="录入时间" DataField="createdDate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
