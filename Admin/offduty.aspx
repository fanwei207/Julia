<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.offduty" CodeFile="offduty.aspx.vb" %>

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
        <table id="Table1" runat="server" cellspacing="0" bordercolor="Black" width="860px">
            <tr>
                <td>
                    工号<asp:TextBox runat="server" Width="60px" ID="txtNo"></asp:TextBox>
                </td>
                <td>
                    姓名<asp:TextBox runat="server" ID="txtName" Width="60px"></asp:TextBox>
                </td>
                <td>
                    部门<asp:DropDownList ID="department" runat="server" Width="120px">
                    </asp:DropDownList>
                </td>
                <td>
                    日期<asp:TextBox ID="txtStartDate" runat="server" Width="80px" onpaste="return false;"
                        CssClass="smalltextbox Date"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtEndDate" runat="server" Width="80px" onpaste="return false;"
                        CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" Width="60px"
                        CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnExport" runat="server" Visible="False" CssClass="SmallButton2"
                        Text="导出Excel"></asp:Button>
                    &nbsp;
                    <input class="SmallButton2" id="uploadBtn" style="width: 70px" type="button" value="旷工导出"
                        name="uploadBtn" runat="server">&nbsp;
                    <input class="SmallButton2" id="Bexcel" style="width: 80px" type="button" value="病事假导出"
                        name="Bexcel" runat="server">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgoffduty" runat="server" Width="860px" PageSize="21" AutoGenerateColumns="False"
            AllowPaging="True" CssClass="GridViewStyle AutoPageSize" 
            AllowSorting="True">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" Visible="False" HeaderText="序号">
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" HeaderText="工号">
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" HeaderText="员工姓名">
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="dept" HeaderText="所属部门">
                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workshop" HeaderText="所属工段">
                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group" HeaderText="所属班组">
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="type" HeaderText="性质">
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="restday" HeaderText="请假天数" 
                    CommandName="detail" DataTextFormatString="&lt;u&gt;{0}&lt;/u&gt;">
                    <ItemStyle HorizontalAlign="Right" Width="80px" Font-Bold="False" 
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="True"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="sickday" HeaderText="病假天数">
                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="holiday" HeaderText="年休">
                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
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
