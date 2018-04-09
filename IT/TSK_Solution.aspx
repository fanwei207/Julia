<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_Solution.aspx.cs" Inherits="TSK_Solution" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $(".TaskShowTestDetails").click(function () {

                var _tskNbr = $("#hidNbr").val();
                var _detID = $("#hidTskdID").val();
                var _src = "../IT/TSK_TestingView.aspx?tskNbr=" + _tskNbr + "&detID=" + _detID;

                $.window("任务明细", 1000, 700, _src);
            });




        })
    
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 800px">
            <tr>
                <td align="left">
                    开发日志：(*不可留空,50字以内。每条日志的开头，必须是“完成”、“正在”、“请问”三个关键词 )
                </td>
                <td align="right">
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="SmallButton3" OnClick="btnSave_Click"
                        TabIndex="2" />
                    &nbsp;
                    <asp:Button ID="txtBack" runat="server" Text="BACK" CssClass="SmallButton3" OnClick="txtBack_Click"
                        TabIndex="4" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtDemand" runat="server" Width="800px" MaxLength="50" TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        Width="800px" DataKeyNames="tskv_id" OnRowCommand="gv_RowCommand" TabIndex="3">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table3" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="NULL" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("tskv_status") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripion">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("tskv_desc") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="600px" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblcreateDate" runat="server" Text='<%# Bind("tskv_createDate") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="finish" runat="server" Font-Bold="False" Font-Size="11px" CommandName="finish"
                                        Font-Underline="True" Text='<%# Bind("tskv_statusing") %>' Style="padding-left: 5px;"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2">
                    &nbsp;
                    <asp:Button ID="btnDone" runat="server" Text="TO TEST" CssClass="SmallButton3" Width="100px"
                        OnClick="btnDone_Click" TabIndex="5" />
                    <input id="hidNbr" type="hidden" runat="server" />
                    <input id="hidDesc" type="hidden" runat="server" />
                    <input id="hidType" type="hidden" runat="server" />
                    <input id="hidTrackEmail" type="hidden" runat="server" />
                    <input id="hidTestEmail" type="hidden" runat="server" />
                    <input id="hidTestSecondEmail" type="hidden" runat="server" />
                    <input id="hidTskdID" type="hidden" value="0" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
