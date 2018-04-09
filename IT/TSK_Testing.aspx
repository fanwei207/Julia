<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_Testing.aspx.cs" Inherits="TSK_Testing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 940px">
            <tr>
                <td align="left">
                    测试方案：<asp:CheckBox 
                        ID="chkNotTest" runat="server" Checked="True" Text="仅未测试" AutoPostBack="True"
                        OnCheckedChanged="chkNotUpdate_CheckedChanged" />
                    （第一测试员设立，并跟踪测试方案；第二测试员确认测试结果）
                </td>
                <td align="right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        PageSize="20" Width="940px" OnRowCommand="gv_RowCommand" DataKeyNames="tskr_id,tskr_result,tskr_testing,tskr_testDate,tskr_chargeBy,tskr_table,tskr_isCompleted,tskr_type,tskr_chargeEmail,tskr_detID"
                        OnRowDataBound="gv_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="940px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="序号" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="测试方案" Width="500px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="创建时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="待决" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="结果" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="tskr_index" HeaderText="序号" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskr_type" HeaderText="类别" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskr_testSolution" HeaderText="测试方案">
                                <HeaderStyle Width="500px" HorizontalAlign="Center" />
                                <ItemStyle Width="500px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskr_createDate" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                                HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskr_resultText" HeaderText="测试结果">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkNotPass" Text="&lt;u&gt;否决&lt;/u&gt;" ForeColor="Blue" Font-Size="12px"
                                        runat="server" CommandName="NotPass" />&nbsp;&nbsp;
                                    <asp:LinkButton ID="linkPass" Text="&lt;u&gt;通过&lt;/u&gt;" ForeColor="Blue" Font-Size="12px"
                                        runat="server" CommandName="Pass" />
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkNew" Text="&lt;u&gt;新增&lt;/u&gt;" ForeColor="Blue" Font-Size="12px"
                                        runat="server" CommandName="New" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkReturn" Text="&lt;u&gt;退回&lt;/u&gt;" ForeColor="Blue" Font-Size="12px"
                                        runat="server" CommandName="Return" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="2">
                    <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="SmallButton3"
                        OnClick="btnBack_Click" Width="80px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnDone" runat="server" Text="DONE" CssClass="SmallButton3"
                        OnClick="btnDone_Click" Width="80px" />
                    <asp:Label ID="lbtskNbr" runat="server" Visible="False">0</asp:Label>
                    <input id="hidTrackEmail" type="hidden" runat="server" />
                    <input id="hidTestBy" type="hidden" runat="server" />
                    <input id="hidTestSecondBy" type="hidden" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
