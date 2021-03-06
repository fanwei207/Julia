<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.personnellist" CodeFile="personnellist.aspx.vb" %>

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
        var w12;
        function openWin(url) {
            top.window.location.href = url;
        }
        function openHR(url) {
            w12 = window.open(url, 'HR', 'toolbar=0,location=0,directories=0,status=0,menubar=0,resizable=1,scrollbars=1,width=600,height=300');
            w12.focus();
        }   
    </script>
</head>
<body onunload="javascript: if(w12) w12.window.close();">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="750" bgcolor="white" border="0">
            <tr>
                <td align="left" width="470">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td align="right" width="280">
                    <input type="submit" id="BtnSearch" class="SmallButton3" value="返回" 
                        runat="server"><asp:Button
                        ID="BtnAll" runat="server" Width="90" CssClass="SmallButton3" Text="全部在职员工">
                    </asp:Button>
                    <asp:Button ID="BtnExport" runat="server" CssClass="SmallButton3" Text="Excel"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" PageSize="20"
            AutoGenerateColumns="False" AllowPaging="True" CssClass="GridViewStyle GridViewRebuild">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="userNo" ReadOnly="True" HeaderText="&lt;b&gt;工号&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="name" HeaderText="&lt;b&gt;姓名&lt;/b&gt;" 
                    CommandName="editUser" DataTextFormatString="&lt;u&gt;{0}&lt;/u&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="rolename" HeaderText="&lt;b&gt;职务&lt;/b&gt;">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="departmentName" HeaderText="&lt;b&gt;部门&lt;/b&gt;">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workshop" HeaderText="&lt;b&gt;工段&lt;/b&gt;">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="90px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sex" HeaderText="&lt;b&gt;性别&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="age" HeaderText="&lt;b&gt;年龄&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="education" HeaderText="&lt;b&gt;学历&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="type" HeaderText="&lt;b&gt;性质&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="leave" HeaderText="&lt;b&gt;离职&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enterDate" HeaderText="&lt;b&gt;进入公司时间&lt;/b&gt;">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="leavedate" HeaderText="&lt;b&gt;离职时间&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workyear" HeaderText="&lt;b&gt;工龄&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="usertype" HeaderText="&lt;b&gt;用工性质&lt;/b&gt;">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ICard" HeaderText="&lt;b&gt;身份证&lt;/b&gt;">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="provinceID" HeaderText="&lt;b&gt;户口所在地&lt;/b&gt;">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="insuranceType" HeaderText="&lt;b&gt;保险类型&lt;/b&gt;">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="uType" HeaderText="&lt;b&gt;员工类型&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" CommandName="editBt">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="userID" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
