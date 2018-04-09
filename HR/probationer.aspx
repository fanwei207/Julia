<%@ Page Language="C#" AutoEventWireup="true" CodeFile="probationer.aspx.cs" Inherits="Wage.hr_probationer" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <form id="Form1" method="post" runat="server">
        <table id="Table1" runat="server" cellspacing="0" width="1040px">
            <tr>
                <td>
                    ����
                </td>
                <td style="width: 116px">
                    &nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="102px" AutoPostBack="True"
                        OnTextChanged="txtUserNo_TextChanged"></asp:TextBox>
                </td>
                <td style="width: 41px">
                    ����
                </td>
                <td style="width: 84px">
                    &nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td style="width: 63px">
                    �빫˾����
                </td>
                <td style="width: 100px">
                    &nbsp;<asp:TextBox ID="txtEnterDate" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td style="width: 87px" align="right">
                    ����
                </td>
                <td>
                    <asp:DropDownList ID="dropDept" runat="server" Width="100px" DataTextField="Name"
                        DataValueField="departmentID">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" CssClass="SmallButton2" runat="server" Text="��ѯ" Width="80px"
                        OnClick="btnSearch_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    ����
                </td>
                <td style="width: 116px">
                    <asp:TextBox ID="txtYear" runat="server" Width="50px" AutoPostBack="True"></asp:TextBox>&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" Width="50px"
                        Font-Size="10pt" CssClass="smallbutton2">
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
                <td style="width: 41px">
                    Ӧ����
                </td>
                <td style="width: 84px">
                    &nbsp;<asp:TextBox ID="txtAttendence" CssClass="SmallTextBox Numeric" runat="server"
                        Width="80px"></asp:TextBox>
                </td>
                <td style="width: 13px">
                </td>
                <td style="width: 100px">
                    &nbsp;
                </td>
                <td columnspan="3" style="width: 87px">
                    <asp:Button ID="btnSave" runat="server" Width="80px" Text="����" CssClass="SmallButton2"
                        OnClick="btnSave_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgProb" runat="server" Width="1040px" PageSize="16" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="True" OnDeleteCommand="dgProb_DeleteCommand"
            OnPageIndexChanged="dgProb_PageIndexChanged" OnItemDataBound="dgProb_ItemDataBound">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn SortExpression="gsort" HeaderText="���" ReadOnly="True">
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" HeaderText="����" ReadOnly="True">
                    <ItemStyle Width="200px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workshop" HeaderText="����" ReadOnly="True">
                    <ItemStyle Width="200px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" SortExpression="userID" HeaderText="����" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" HeaderText="����" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Attendence" SortExpression="kinds" HeaderText="Ӧ����">
                    <ItemStyle Width="60px" HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="EnterDate" SortExpression="enter" HeaderText="�빫˾����"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="leavedate" SortExpression="leave" HeaderText="��ְ����" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cleave" SortExpression="cleave" HeaderText="����">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>ɾ��</u>" CommandName="Delete">
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
