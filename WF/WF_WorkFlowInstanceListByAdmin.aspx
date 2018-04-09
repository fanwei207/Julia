<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_WorkFlowInstanceListByAdmin.aspx.cs"
    Inherits="PM_HeaderList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body runat="server">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="1000px" class="main_top" border="0">
            <tr>
                <td>
                    <asp:Label ID="lblDomain" runat="server" Width="20px" CssClass="LabelRight" Text="��:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDomain" runat="server">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="3">YQL</asp:ListItem>
                        <asp:ListItem Value="4">HQL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblNbr" runat="server" Width="48px" CssClass="LabelRight" Text="�����:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNbr" runat="server" Width="100px" TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblFlowName" runat="server" Width="60px" CssClass="LabelRight" Text="����ģ��:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkFlow" runat="server" DataTextField="Flow_Name" DataValueField="Flow_ID"
                        Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblCreateBy" runat="server" Width="50px" CssClass="LabelRight" Text="������:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreateBy" runat="server" Width="80px" TabIndex="3" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblReqDate" runat="server" Width="60px" CssClass="LabelRight" Text="��������:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReqDate" runat="server" Width="80px" TabIndex="3" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td style="width: 66px">
                    <asp:Label ID="lblStatus" runat="server" Width="60px" CssClass="LabelRight" Text="����״̬:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="4">--</asp:ListItem>
                        <asp:ListItem Value="1">������</asp:ListItem>
                        <asp:ListItem Value="2">�����</asp:ListItem>
                        <asp:ListItem Value="3">���ܾ�</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" TabIndex="7" Text="��ѯ"
                        Width="50px" OnClick="btnQuery_Click" />
                </td>
                <td align="right">
                    <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton3" TabIndex="7" Text="Excel"
                        Width="50px" OnClick="btnExcel_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWFI" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" PageSize="23" OnPreRender="gvWFI_PreRender"
            DataKeyNames="WFN_Nbr" OnRowDataBound="gvWFI_RowDataBound" OnPageIndexChanging="gvWFI_PageIndexChanging"
            Width="1150px" OnRowCommand="gvWFI_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1150px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����ģ��" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��ֹ����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��" Width="170px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="״̬" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��ϸ" Width="50px" HorizontalAlign="center"></asp:TableCell>
                         <asp:TableCell Text="��ע" Width="250px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="WFN_Domain" HeaderText="��">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="WFN_Nbr" HeaderText="�����">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Flow_Name" HeaderText="����ģ��">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="WFN_ReqDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="WFN_DueDate" HeaderText="��ֹ����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="��">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkform" ForeColor="Blue" Font-Underline="true" Text='<%# Bind("WFN_FormName") %>'
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("WFN_Nbr") %>' CommandName="View">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="150px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:BoundField DataField="WFN_CreatedBy" HeaderText="������">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="WFN_CreatedDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="WFN_Status" HeaderText="״̬">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkdetail" Text="<u>��ϸ</u>" ForeColor="Blue" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("WFN_Nbr") %>' CommandName="Detail" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="FNI_Remark" HeaderText="��ע">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
