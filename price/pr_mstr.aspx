<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pr_mstr.aspx.cs" Inherits="TCP.Price.price_pr_mstr" %>

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
        <table cellspacing="2" cellpadding="2" width="970px" bgcolor="white" border="0">
            <tr>
                <td>
                    �����:<asp:TextBox 
                        ID="txtPart" runat="server" CssClass="smalltextbox Part" Width="150px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPart"
                        ErrorMessage="QAD��:8-14λ����" ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator>
                    &nbsp;����:<asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date" Width="150px"
                        onkeydown="event.returnValue=false;" onpaste="event.returnValue=false;"></asp:TextBox>
                </td>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="btnQuery" runat="server" Text="��ѯ" CssClass="SmallButton3" Width="58px"
                        OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPrice" name="gvAtl" runat="server" 
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            PageSize="15" OnRowDataBound="gvPrice_RowDataBound" AllowPaging="True" 
            OnPageIndexChanging="gvPrice_PageIndexChanging" Width="970px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="pc_domain" HeaderText="�ص�">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��Ʒ��">
                    <HeaderStyle Width="90px" />
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="pc_part" HeaderText="�����">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����">
                    <HeaderStyle Width="160px" />
                    <ItemStyle Width="160px" />
                </asp:BoundField>
                <asp:BoundField DataField="pc_um" HeaderText="��λ">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ɹ�Ա">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="pc_list" HeaderText="��Ӧ��">
                    <HeaderStyle Width="210px" />
                    <ItemStyle Width="210px" />
                </asp:BoundField>
                <asp:BoundField DataField="pc_amt" HeaderText="�ɹ��۸�(����˰)">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="pc_curr" HeaderText="����">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="pc_start" DataFormatString="{0:yyyy.MM.dd}" HeaderText="��ʼ����"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="pc_expire" DataFormatString="{0:yyyy.MM.dd}" HeaderText="��������">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
