<%@ Page Language="VB" AutoEventWireup="false" CodeFile="computerhis.aspx.vb" Inherits="computermanage_computerhis" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="580px" align="center" bgcolor="white"
            border="0">
            <tr>
                <td>
                    <asp:Label ID="lbltype" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblassetno" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <input class="smallButton3" id="btnback" onclick="window.close();" type="button"
                        value="关闭" name="btnback" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3">
                    领用日期<asp:TextBox ID="txbbegindate" 
                        runat="server" Width="65px" CssClass="SmallTextBox Date"></asp:TextBox>
                    归还日期<asp:TextBox ID="txbenddate" runat="server" Width="65px" 
                        CssClass="SmallTextBox Date"></asp:TextBox>
                    部门<asp:DropDownList ID="drpdepartment" runat="server" Width="100px" AutoPostBack="True">
                    </asp:DropDownList>
                    用户<asp:DropDownList ID="drpusername" runat="server" Width="80px">
                    </asp:DropDownList>
                    状态<asp:DropDownList ID="drpstatus" runat="server" Width="80px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3">
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注<asp:TextBox ID="txbdemo" runat="server"
                        Width="420px" CssClass="SmallTextBox"></asp:TextBox>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="新增" />
                    <asp:Button ID="btnMod" runat="server" CssClass="SmallButton3" Text="修改" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton3" Text="取消" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="datagrid1" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="False"
            CssClass="GridViewStyle" Width="580px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" HeaderText="ID" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="begindate" HeaderText="领用日期" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enddate" HeaderText="归还日期" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="departmentname" HeaderText="部门">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="username" HeaderText="姓名">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" HeaderText="状态">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="demo" HeaderText="备注">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" CommandName="Select">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="DeleteClick">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
