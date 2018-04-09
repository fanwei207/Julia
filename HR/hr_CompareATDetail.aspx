<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_CompareATDetail.aspx.cs"
    Inherits="HR_hr_CompareATDetail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"  >
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
        <form id="form2" method="post" runat="server">
        <table width="860px">
            <tr>
                <td align="right">
                    <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton3" Text="Excel" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnClose" runat="server" CssClass="SmallButton3" Text="����" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCompare" AutoGenerateColumns="False" CssClass="GridViewStyle"
            runat="server" Width="1030px" DataSourceID="obdsDetail">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField HeaderText="����" DataField="userNo" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="userName" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�빫˾����" DataField="uenter" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��ְ����" DataField="uleave" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="dname" ReadOnly="True">
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�Ͳ�����" DataField="DinerDate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�Ͳ�ʱ��" DataField="DinerDate" DataFormatString="{0:HH:mm:ss}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ϰ�ʱ��" DataField="startTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    HtmlEncode="False">
                    <ItemStyle Width="150px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�°�ʱ��" DataField="EndTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    HtmlEncode="False">
                    <ItemStyle Width="150px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����Сʱ" DataField="totalhr">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�Ϳ��豸���" DataField="cardnum">
                    <ItemStyle Width="150px" HorizontalAlign="center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <asp:ObjectDataSource ID="obdsDetail" runat="server" SelectMethod="AttDinerDetail"
        TypeName="Wage.HR">
        <SelectParameters>
            <asp:QueryStringParameter Name="intYear" QueryStringField="yr" Type="Int32" />
            <asp:QueryStringParameter Name="intMonth" QueryStringField="mh" Type="Int32" />
            <asp:SessionParameter Name="intDepartment" SessionField="userID" Type="Int32" />
            <asp:QueryStringParameter Name="intUserID" QueryStringField="uid" Type="Int32" />
            <asp:SessionParameter Name="intPlant" SessionField="plantcode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
