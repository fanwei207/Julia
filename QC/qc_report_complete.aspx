<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_report_complete.aspx.cs"
    Inherits="QC_qc_report_complete" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .Curent
        {
            font-size: 10pt;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table runat="server" id="tbHeader" cellspacing="2" cellpadding="2" bgcolor="white" border="0" width="800px">
            <tr>
                <td align="left">
                    �ջ�����:<asp:TextBox ID="txtReceiver" runat="server" CssClass="SmallTextBox" TabIndex="1"
                        Width="90px"></asp:TextBox>
                </td>
                <td>
                    �ɹ���:<asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox" TabIndex="2"
                        Width="90px"></asp:TextBox>
                </td>
                <td>
                    �к�:<asp:TextBox ID="txtLine" runat="server" CssClass="SmallTextBox" TabIndex="2"
                        Width="90px"></asp:TextBox>
                </td>
                <td>
                    ���Ϻ�:<asp:TextBox ID="txtPart" runat="server" CssClass="SmallTextBox" TabIndex="3"
                        Width="90px"></asp:TextBox>
                </td>
                <td>
                    ��Ӧ��:<asp:TextBox ID="txtCus" runat="server" CssClass="SmallTextBox" TabIndex="3"
                        Width="90px"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    �ջ�����:<asp:TextBox ID="txtStd" runat="server" CssClass="SmallTextBox Date" Width="118px"
                        onpaste="return false"></asp:TextBox>
                    ��<asp:TextBox ID="txtEnd" runat="server" CssClass="SmallTextBox Date" Width="118px"
                        onpaste="return false"></asp:TextBox>
                </td>
                <td>
                    ������:<asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">���</asp:ListItem>
                        <asp:ListItem Value="2">���</asp:ListItem>
                        <asp:ListItem Value="3">�ز�</asp:ListItem>
                        <asp:ListItem Value="4">�˻�</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        TabIndex="4" Text="��ѯ" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Width="800px"
            CssClass="GridViewStyle" PageSize="19" OnRowDataBound="gvReport_RowDataBound"
            DataKeyNames="ok,no,prh_group,flag,Identity" OnRowCommand="gvReport_RowCommand">
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Block.gif" CommandName="expand" />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="�ջ���" DataField="prh_receiver">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ɹ�����" DataField="prh_nbr">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�к�" DataField="prh_line">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="���Ϻ�" DataField="prh_part">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��Ӧ��" DataField="prh_vend">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_rcvd" HeaderText="��������" DataFormatString="{0:N0}">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ջ�����" DataField="prh_rcp_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="������Ŀ">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="link">�鿴��Ŀ</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    <ItemStyle Font-Bold="False" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��ɫ����">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="link">�鿴</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="�ж�">
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������" DataField="prh_state">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="�ؼ�">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>  
                        <asp:LinkButton ID="btnReCheck" runat="server" CommandName="reCheck"  Text="�ؼ�"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        <asp:TextBox ID="txtPageIndex" runat="server" Visible="False" Width="49px"></asp:TextBox><asp:TextBox
            ID="txtPageCount" runat="server" Visible="False" Width="49px"></asp:TextBox><asp:TextBox
                ID="txtIndex" runat="server" Visible="False" Width="49px"></asp:TextBox>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
