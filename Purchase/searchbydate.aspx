<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.SearchbyDate" CodeFile="SearchbyDate.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>
         
    </title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:ValidationSummary ID="cMsg" runat="server" HeaderText="��ע������������ !" ShowSummary="False"
            ShowMessageBox="True"></asp:ValidationSummary> 
        <asp:Table ID="Table2" runat="server" Width="800px">
            <asp:TableRow>
                <asp:TableCell>
                    ����
                    <asp:TextBox ID="txtStartDate" runat="server" Width="75px" TabIndex="0" CssClass="smalltextbox Date"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtStartDate"
                        ErrorMessage="��ʼ���ڲ���Ϊ��" ID="Requiredfieldvalidator1"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" Display="None" Operator="DataTypeCheck" Type="Date"
                        ControlToValidate="txtStartDate" ErrorMessage="��������ȷ�����ڸ�ʽ����-��-�գ�" ID="Comparevalidator1"></asp:CompareValidator>
                    --
                    <asp:TextBox ID="txtEndDate" runat="server" Width="75px" TabIndex="0" CssClass="smalltextbox Date"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtEndDate"
                        ErrorMessage="�������ڲ���Ϊ��" ID="Requiredfieldvalidator2"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" Display="None" Operator="DataTypeCheck" Type="Date"
                        ControlToValidate="txtStartDate" ErrorMessage="��������ȷ�����ڸ�ʽ����-��-�գ�" ID="Comparevalidator2"></asp:CompareValidator>
                    ���<asp:TextBox ID="txtCode" runat="server" TabIndex="0" Width="160px"></asp:TextBox>
                    ����<asp:DropDownList ID="typeDDL" runat="server" Width="60px">
                    </asp:DropDownList>
                    ����<asp:TextBox ID="txtCat" runat="server" Width="50px" TabIndex="0"></asp:TextBox>
                    �ֿ�<asp:DropDownList ID="ddlWhplace" Width="80px" runat="server">
                    </asp:DropDownList>
                    ״̬<asp:DropDownList ID="ddlStatus" Width="80px" runat="server">
                    </asp:DropDownList>
                    <asp:Button Text="��ѯ" runat="server" OnClick="Report_click" Width="30px" CssClass="SmallButton3"
                        ID="BtnReport" TabIndex="0"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" BorderColor="#CCCCCC" Width="800px" GridLines="Vertical"
            PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Center" PageSize="23"
            AllowPaging="True" CellPadding="1" BackColor="White" AutoGenerateColumns="False"
            ShowHeader="true" BorderWidth="1px" BorderStyle="None" AllowSorting="True">
           <ItemStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="partcode"   HeaderText="<b>���ϴ���</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="300px"></HeaderStyle>
                    <ItemStyle Width="300px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="category"   HeaderText="<b>����</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle Width="100" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="type"  HeaderText="<b>����</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle Width="100" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="total"  HeaderText="<b>�������</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle Width="100" HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="itype"  HeaderText="<b>����</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle Width="100" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" HeaderText="<b>״̬</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="unit"   HeaderText="<b>��λ</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                    <ItemStyle Width="50px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
            </Columns> 
        </asp:DataGrid>
       </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
