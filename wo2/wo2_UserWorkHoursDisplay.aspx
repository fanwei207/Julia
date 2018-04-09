<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_UserWorkHoursDisplay.aspx.cs" Inherits="wo2_wo2_UserWorkHoursDisplay" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <div align="center">
        <form id="form1" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1000px">
            <tr>
                <td align="left" style="width: 250px">
                    日期<asp:TextBox ID="txtStart" 
                        runat="server" MaxLength="10" Width="80px" CssClass="SmallTextBox Date"></asp:TextBox>
                    --<asp:TextBox ID="txtEnd" 
                        runat="server" MaxLength="10" Width="80px" CssClass="SmallTextBox Date"></asp:TextBox>
                    </td>
                <td align="left" style="width: 150px">
                    工号<asp:TextBox 
                        ID="txtUserNo" runat="server" MaxLength="5" Width="80px" 
                        CssClass="SmallTextBox "></asp:TextBox>
                    </td>
                <td align="left" style="width: 150px">
                    加工单<asp:TextBox ID="txtWoNbr" runat="server" 
                        MaxLength="10" Width="70px" 
                        CssClass="SmallTextBox "></asp:TextBox>
                </td>
                <td align="left" style="width: 150px">
                    ID<asp:TextBox ID="txtWoLot" 
                        runat="server" MaxLength="10" Width="70px" 
                        CssClass="SmallTextBox "></asp:TextBox>
                    </td>
                <td align="left" style="width: 100px">
                    <asp:CheckBox ID="chkClose" runat="server" Text="结算" />
                    </td>
                <td align="right" style="width: 200px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Width="60px" Text="查询"
                        OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton3" Width="60px" Text="Excel"
                        OnClick="btnExcel_Click" />
                </td>
            </tr>
            </table>
        <asp:GridView ID="gvUsers" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            runat="server" PageSize="25" Width="1000px" 
            OnPageIndexChanging="gvUsers_PageIndexChanging" DataKeyNames="wo2_userID">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="工号" DataField="wo2_userNo">
                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="wo2_userName">
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="加工单号" DataField="wo2_nbr">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="加工单ID" DataField="wo2_wID">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="流水线" DataField="wo2_line">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工序" DataField="wo2_procName">
                    <ItemStyle Width="145px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工位" DataField="wo2_postName">
                    <ItemStyle Width="145px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工位系数" DataField="wo2_postProportion">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工序标准" DataField="wo2_ro_tool">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="汇报数量" DataField="wo2_line_comp">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblSum" runat="server" Text="Label" Visible="False"></asp:Label>
        </form>
        <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
        </script>
    </div>
</body>
</html>
