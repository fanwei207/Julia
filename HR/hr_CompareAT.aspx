<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_CompareAT.aspx.cs" Inherits="HR_hr_CompareAT" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"  >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" method="post" runat="server">
        <table width="900px" cellspacing="0" cellpadding="0" id="tbSearch" runat="server">
            <tr>
                <td valign="bottom">
                    ����&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="50px" MaxLength="4" AutoPostBack="true"></asp:TextBox>
                    &nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" CssClass="server" Width="40px">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="bottom">
                    ����&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="100px" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td valign="bottom"  width="90px">
                    ����&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="60px" TabIndex="2"></asp:TextBox>
                </td>
                <td valign="bottom" width="90px">
                    ����&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="60px" TabIndex="3"></asp:TextBox>
                </td>
                <td valign="bottom">
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        CausesValidation="false" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExport0" runat="server" Text="����" CssClass="SmallButton2" 
                        OnClick="btnExport0_Click" />
                &nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="�Ͳ���ϸ" CssClass="SmallButton2" 
                        OnClick="btnExport_Click" />
                &nbsp;
                    <asp:Button ID="btnExportAll" runat="server" Text="�Ͳ���ϸȫ��" Width="80px" CssClass="SmallButton2" 
                        OnClick="btnExportAll_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCompare" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            runat="server" PageSize="20" Width="900px" DataSourceID="obdsDinerCompare" OnRowDataBound="gvCompare_DataBound"
            DataKeyNames="userID" OnRowCommand="gvCompare_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="����" DataField="userNo" ReadOnly="True">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="userName" ReadOnly="True">
                    <ItemStyle Width="80px" />
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
                <asp:BoundField HeaderText="����" DataField="wname">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ܳ�����" DataField="totalattday">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������" DataField="attday">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="dinner">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="���" DataField="reduce">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:CommandField />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDetail" runat="server" CausesValidation="false" CommandName="Detail"
                            Text="&lt;u&gt;��ϸ&lt;/u&gt;"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:ObjectDataSource ID="obdsDinerCompare" runat="server" SelectMethod="AttAndDinerCompare"
            TypeName="Wage.HR">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="strYear" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropDept" DefaultValue="0" Name="intDepartment"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" DefaultValue="" Name="strUser" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtUserName" Name="strUserName" PropertyName="Text"
                    Type="String" />
                <asp:SessionParameter Name="intPlant" SessionField="PlantCode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
</body>
</html>
