<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.partlist" CodeFile="partlist.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function(){
            $("#txtPartCode").focus(function(){
                if($("#txtPartCode").val()=="�����벿����")
                {
                    $("#txtPartCode").val("");
                }
                return true;
            });
        })
    </script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="1100px" bgcolor="white" border="0">
            <tr>
                <td align="left">
                    ������
                    <asp:TextBox ID="txtPartCode" runat="server" Width="153px" MaxLength="50" 
                        CssClass="SmallTextBox">�����벿����</asp:TextBox>&nbsp;
                    QAD��<asp:TextBox ID="txtQad" runat="server" Width="95px" MaxLength="50" 
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;����
                    <asp:TextBox ID="txtCategory" runat="server" Width="100px" MaxLength="30" CssClass="SmallTextBox"></asp:TextBox>&nbsp;����
                    <asp:TextBox ID="txtDesc" TabIndex="0" runat="server" Width="100px" MaxLength="255"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp; 
                    <asp:RadioButton ID="radNormal" runat="server" Text="ʹ��" AutoPostBack="True" Checked="True"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="radTry" runat="server" Text="����" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="radStop" runat="server" Text="ͣ��" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                         <asp:Button ID="BtnQuery" runat="server" CssClass="SmallButton3" Text="��ѯ" 
                        Width="48px"></asp:Button>
                </td>
                <td align="right">
                    <asp:Label ID="lblCount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnReplace" runat="server" CssClass="SmallButton3" Text="�滻����" Width="60">
                    </asp:Button>&nbsp;<asp:Button ID="BtnAdd" runat="server" CssClass="SmallButton3" Text="����"></asp:Button>
                    <asp:Button ID="ButExcel" runat="server" CssClass="SmallButton3" 
                        Text="Excel" Width="60px" onclick="ButExcel_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="2040px" AutoGenerateColumns="False"
            HeaderStyle-Font-Bold="false" PagerStyle-HorizontalAlign="Center" AllowPaging="True"
            PageSize="25" PagerStyle-Mode="NumericPages" PagerStyle-BackColor="#99ffff" PagerStyle-Font-Size="12pt"
            PagerStyle-ForeColor="#0033ff" CssClass="GridViewStyle GridViewRebuild">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="partid" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" ReadOnly="True" Visible="False"
                    HeaderText="<b>���</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" SortExpression="gsort" HeaderText="<b>������</b>">
                    <HeaderStyle Width="220px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="220px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="item_qad" SortExpression="gsort" HeaderText="<b>����QAD��</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="category" SortExpression="category" HeaderText="<b>����</b>">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="minQty" SortExpression="qty" HeaderText="<b>��С�����</b>">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="unitName" SortExpression="unitName" HeaderText="<b>��λ</b>">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TranUnit" SortExpression="TranUnit" HeaderText="<b>ת��ǰ��λ</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Tran_Rate" SortExpression="TranRate" HeaderText="<b>ת��ϵ��</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>�༭</u>" HeaderText="<b>�༭</b>" CommandName="EditBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>����</u>" HeaderText="<b>����</b>" CommandName="UsedByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>����</u>" HeaderText="<b>����</b>" CommandName="SupplyBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                    <asp:ButtonColumn Text="<u>�ߴ�</u>" HeaderText="�ߴ�" CommandName="SizeByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>�ĵ�</u>" HeaderText="<b>�ĵ�</b>" CommandName="DocByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>ת��Ʒ</u>" HeaderText="<b>ת��Ʒ</b>" CommandName="ToProdBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>��ʷ</u>" HeaderText="<b>��ʷ</b>" CommandName="HisBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="description" SortExpression="description" HeaderText="<b>��������</b>">
                    <HeaderStyle Width="1200px" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="1200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createddate1" SortExpression="createddate1" HeaderText="<b>��������</b>">
                    <HeaderStyle HorizontalAlign="center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createdby1" SortExpression="createdby1" HeaderText="<b>������</b>">
                    <HeaderStyle HorizontalAlign="center" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="true" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
