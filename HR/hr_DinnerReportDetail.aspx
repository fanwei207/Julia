<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_DinnerReportDetail.aspx.cs"
    Inherits="hr_DinnerReportDetail" %>

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
    <form id="form1" runat="server">
    <div align="center">
        <table runat="server" id="table1" cellspacing="0" cellpadding="1" width="600" align="center">
            <tr style="height: 20px">
                <td style="width: 140px" align="center" valign="middle">
                    �Ͳ�����:&nbsp;<asp:Label ID="lblDate" runat="server" Width="80px" CssClass="LabelLeft"
                        Font-Bold="false"></asp:Label>
                </td>
                <td style="width: 260px" align="left" valign="middle">
                    Ա������:&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px" CssClass="TextLeft"></asp:TextBox>
                    <asp:CheckBox ID="chkAll" runat="server" Width="70px" Text="��ʾ����" />
                    <asp:Button ID="btnQuery" runat="server" Width="40px" Text="��ѯ" CssClass="SmallButton3"
                        OnClick="btnQuery_Click" />
                </td>
                <td align="center">
                    <asp:Button ID="btnExcel" runat="server" Text="����Excel" CssClass="SmallButton3" Width="50px"
                        TabIndex="3" OnClick="btnExcel_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnReturn" runat="server" Text="����" CssClass="SmallButton3" Width="40px"
                        TabIndex="4" OnClick="btnReturn_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvDinnerDetail" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="20" OnPreRender="gvDinnerDetail_PreRender"
            Width="600px" OnPageIndexChanging="gvDinnerDetail_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="DinnerUserNo" HeaderText="���ڱ��">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DinnerUserCode" HeaderText="Ա������">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DinnerUserName" HeaderText="Ա������">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DinnerTime" HeaderText="�Ͳ�ʱ��">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="DinnerSensor" HeaderText="�豸���">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Deptment" HeaderText="����">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="GongDuan" HeaderText="����">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
