<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.employeetrain" CodeFile="employeetrain.aspx.vb"
    EnableViewState="true" EnableSessionState="True" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        <table id="table1" width="1000" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    培训类型
                </td>
                <td>
                    <asp:DropDownList ID="SelectTypeDropDown" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
                <td>
                    完成情况
                </td>
                <td>
                    <asp:DropDownList ID="selectfinishdropdown" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">
                    &nbsp;
                </td>
                <td colspan="2" style="text-align: right;">
                    <asp:TextBox ID="txbid" Visible="False" runat="server" ReadOnly="True" Width="36px"></asp:TextBox>
                    年<asp:TextBox ID="txbyear" runat="server" Width="50"></asp:TextBox>
                    月<asp:TextBox ID="txbmonth" runat="server" Width="50"></asp:TextBox>
                    <asp:Button ID="Btnprint" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="导出培训计划" Width="100" />
                </td>
            </tr>
            <tr>
                <td>
                    培训课题
                </td>
                <td>
                    <asp:TextBox ID="txbsubject" runat="server" Width="200"></asp:TextBox>
                </td>
                <td>
                    参培部门<asp:TextBox ID="txbdepid" runat="server" Style="display: none"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txbdep" runat="server" Width="150" onkeydown="event.returnValue=false;"
                        onpaste="return false"></asp:TextBox><asp:Button ID="btnselect" Text="选择" CssClass="SmallButton2"
                            runat="server" Width="50"></asp:Button>
                </td>
                <td>
                    参培对象
                </td>
                <td>
                    <asp:TextBox ID="txbperson" runat="server" Width="200"></asp:TextBox>
                </td>
                <td>
                    完成<asp:CheckBox ID="chkstatus" runat="server"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    主讲人
                </td>
                <td>
                    <asp:TextBox ID="Txbhost" runat="server" Width="200"></asp:TextBox>
                </td>
                <td>
                    负责部门
                </td>
                <td>
                    <asp:TextBox ID="Txbresid" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="Txbres" runat="server" Width="150" onkeydown="event.returnValue=false;"
                        onpaste="return false"></asp:TextBox><asp:Button ID="Btnselect1" Text="选择" CssClass="SmallButton2"
                            runat="server" Width="50"></asp:Button>
                </td>
                <td>
                    培训日期
                </td>
                <td>
                    <asp:TextBox ID="txbdate" runat="server" Width="160" onkeydown="event.returnValue=false;"
                        onpaste="return false" CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    培训时间
                </td>
                <td>
                    <asp:TextBox ID="txbtime" runat="server" Width="200"></asp:TextBox>
                </td>
                <td>
                    培训地点
                </td>
                <td>
                    <asp:TextBox ID="Txbsite" runat="server" Width="200"></asp:TextBox>
                </td>
                <td>
                    备注
                </td>
                <td>
                    <asp:TextBox ID="Txbmemo" runat="server" Width="200"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Button ID="Btnadd" CssClass="SmallButton2" Text="增加" runat="server" Width="50">
                    </asp:Button>
                    &nbsp;<asp:Button ID="Btnmodify" Text="修改" runat="server" CssClass="SmallButton2"
                        Width="50"></asp:Button>
                    &nbsp;<asp:Button ID="Btncancel" Text="取消" runat="server" CssClass="SmallButton2"
                        Width="50" CausesValidation="False"></asp:Button>
                    &nbsp;<asp:Button ID="btnser" Text="查询" runat="server" CssClass="SmallButton2" Width="50"
                        CausesValidation="False"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="datagrid1" runat="server" Width="1510px" PageSize="15" AllowPaging="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" Visible="False" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="subject" SortExpression="subject" HeaderText="培训课题">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="depid" SortExpression="depid" Visible="False" HeaderText="参培部门ID">
                    <HeaderStyle Width="0px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="0px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="involvedep" SortExpression="involvedep" HeaderText="参培部门">
                    <HeaderStyle Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="involveemp" SortExpression="involveemp" HeaderText="参培对象">
                    <HeaderStyle Width="160px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="160px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="traindate" SortExpression="traindate" HeaderText="培训日期">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="traintime" SortExpression="traintime" HeaderText="培训时间">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="respid" SortExpression="respid" Visible="False" HeaderText="负责部门ID">
                    <HeaderStyle Width="0px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="0px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="respdep" SortExpression="respdep" HeaderText="负责部门">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="host" SortExpression="host" HeaderText="主讲人">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="site" SortExpression="site" HeaderText="培训地点">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="finish" SortExpression="finish" HeaderText="完成情况">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="memo" SortExpression="memo" HeaderText="备注">
                    <HeaderStyle Width="300px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="typeid" Visible="False" ReadOnly="True"></asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" CommandName="Select">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="DeleteClick">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
