<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_report_undo.aspx.cs" Inherits="QC_qc_report_undo" %>

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
            font-size: 14px;
            color: #000000;
            text-decoration: underline;
            font-weight: normal;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" width="800" bgcolor="white" border="0">
            <tr>
                <td align="left" style="height: 9px; width: 858px;">
                    工单号:<asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox" Width="90px"
                        TabIndex="1"></asp:TextBox>&nbsp; ID号:<asp:TextBox ID="txtReceiver" runat="server" CssClass="SmallTextBox"
                            TabIndex="2" Width="90px"></asp:TextBox><asp:TextBox ID="txtLine" runat="server"
                                CssClass="SmallTextBox" TabIndex="2" Width="90px" Text="0" Visible="false"></asp:TextBox>物料号:<asp:TextBox
                                    ID="txtPart" runat="server" CssClass="SmallTextBox Part" Width="90px" TabIndex="3"></asp:TextBox>产线:<asp:TextBox
                                        ID="txtCus" runat="server" CssClass="SmallTextBox Supplier" TabIndex="3" Width="90px"></asp:TextBox><br />
                    评审日期:<asp:TextBox ID="txtStd" runat="server" CssClass="smalltextbox Date" Width="100px"
                        onpaste="return false"></asp:TextBox>
                    —<asp:TextBox ID="txtEnd" runat="server" CssClass="smalltextbox Date" Width="100px"
                        onpaste="return false"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton" OnClick="btnSearch_Click"
                        TabIndex="4" Text="查询" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Width="787px"
            CssClass="GridViewStyle AutoPageSize" OnRowCommand="gvReport_RowCommand" OnRowDataBound="gvReport_RowDataBound"
            ShowFooter="True">
            <Columns>
                <asp:TemplateField Visible="true">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />&nbsp;
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="工单号" DataField="prh_nbr">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ID号" DataField="prh_receiver">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" /> 
                </asp:BoundField>
                <asp:BoundField HeaderText="行号" DataField="prh_line" visible="false">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" />
                    <ItemStyle Font-Bold="False" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="物料号" DataField="prh_part">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="产线" DataField="prh_vend">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_rcvd" HeaderText="工单数量" DataFormatString="{0:N0}">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="评审日期" DataField="prh_rcp_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField HeaderText="关单日期" DataField="close_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="检验项目">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="link">添加项目</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    <ItemStyle Font-Bold="False" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="光色检验" Visible="False">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="link">导入</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" Visible ="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CommandName="compete"
                            Text="免检"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ControlStyle Font-Underline="True" />
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        <asp:TextBox ID="txtPageIndex" runat="server" Visible="False" Width="49px"></asp:TextBox>
        <asp:TextBox ID="txtPageCount" runat="server" Visible="False" Width="49px"></asp:TextBox>
        <asp:TextBox ID="txtIndex" runat="server" Visible="False" Width="49px"></asp:TextBox></form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
