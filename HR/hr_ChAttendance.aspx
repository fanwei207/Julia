<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_ChAttendance.aspx.cs"
    Inherits="HR_hr_ChAttendance" %>

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
    <div align="center">
        <form id="form1" runat="server">
        <table width="980px" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    ����:<asp:TextBox ID="txtSDate" runat="server" Width="70px" CssClass="smalltextbox Date"></asp:TextBox>--<asp:TextBox
                        ID="txtEDate" runat="server" Width="70px" CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td>
                    ����:<asp:TextBox ID="txtUserNo" runat="server" Width="60px"></asp:TextBox>
                </td>
                <td>
                    ����:<asp:TextBox ID="txtUserName" runat="server" Width="60px"></asp:TextBox>
                </td>
                <td>
                    ����:<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    Ա������:<asp:DropDownList ID="dropType" runat="server" Width="80px">
                    </asp:DropDownList>
                </td>
                <td>
                    �½�Ա��:<asp:CheckBox ID="chkUser" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        Width="60px" />
                    &nbsp;<asp:Button ID="btnUpdate" runat="server" Text="����" CssClass="SmallButton3"
                        OnClick="btnUpdate_Click" Width="60px" Enabled="False" />
                    &nbsp;<asp:Button ID="btnExport" runat="server" Text="����" CssClass="SmallButton3"
                        OnClick="btnExport_Click" Width="60px" />
                    &nbsp;<asp:Button ID="Button1" runat="server" Text="����" CssClass="SmallButton3" OnClick="Button1_Click"
                        Width="60px" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvAttendance" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            runat="server" PageSize="26" Width="980px" DataSourceID="obdsAtt">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="dname" HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:BoundField>
                <asp:BoundField DataField="WorkGroup" HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="userNO" HeaderText="���� ">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="username" HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="workdate" HeaderText="����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="starttime" HeaderText="�ϰ�ʱ��" DataFormatString="{0:HH:mm:ss}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="endtime" HeaderText="�°�ʱ��" DataFormatString="{0:HH:mm:ss}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="starttime1" HeaderText="�ϰ�ʱ��1" DataFormatString="{0:HH:mm:ss}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="endtime1" HeaderText="�°�ʱ��1" DataFormatString="{0:HH:mm:ss}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="atypename" HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="940px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ϰ�ʱ��" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�°�ʱ��" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ϰ�ʱ��1" Width="80px" HorizontalAlign="right"></asp:TableCell>
                        <asp:TableCell Text="�°�ʱ��1" Width="80px" HorizontalAlign="right"></asp:TableCell>
                        <asp:TableCell Text="����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsAtt" runat="server" SelectMethod="CheckAttendance"
            TypeName="WOrder.WorkOrder">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtSDate" Name="strStart" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtEDate" Name="strEnd" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtUserNo" Name="strUserNo" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtUserName" Name="strUserName" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="dropDept" DefaultValue="0" Name="intDepart" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropType" Name="intUserType" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="chkUser" DefaultValue="0" Name="intflag" PropertyName="Checked"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
