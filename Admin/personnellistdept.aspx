<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.personnellistDept"
    CodeFile="personnellistDept.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var w;
        function openWin(url) {
            top.window.location.href = url;
        }
        function openHR(url) {
            w = window.open(url, 'HR', 'toolbar=0,location=0,directories=0,status=0,menubar=0,resizable=1,scrollbars=1,width=600,height=300');
            w.focus();
        }   
    </script>
</head>
<body onunload="javascript: if(w) w.window.close();">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="750" bgcolor="white" border="0">
            <tr>
                <td align="left">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td align="left" width="80">
                </td>
                <td align="left" width="290">
                    <b>部门 </b>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" AutoPostBack="True"
                        Enabled="False">
                    </asp:DropDownList>
                </td>
                <td align="right" width="80">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="750px" ShowFooter="false" AllowPaging="True"
            AutoGenerateColumns="False" PageSize="20" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="userNo" ReadOnly="True" HeaderText="&lt;b&gt;工号&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="center"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="&lt;b&gt;姓名&lt;/b&gt;">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="rolename" HeaderText="&lt;b&gt;职务&lt;/b&gt;">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="departmentName" HeaderText="&lt;b&gt;部门&lt;/b&gt;">
                    <HeaderStyle Width="130px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="130px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="depart1" HeaderText="&lt;b&gt;工段&lt;/b&gt;">
                    <HeaderStyle Width="85px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="85px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="depart2" HeaderText="&lt;b&gt;班组&lt;/b&gt;">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sex" HeaderText="&lt;b&gt;性别&lt;/b&gt;">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="age" HeaderText="&lt;b&gt;年龄&lt;/b&gt;">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="education" HeaderText="&lt;b&gt;学历&lt;/b&gt;">
                    <HeaderStyle Width="35px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enterDate" HeaderText="&lt;b&gt;进入公司&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userID" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
