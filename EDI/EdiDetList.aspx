<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiDetList.aspx.cs" Inherits="EDI_EdiDetList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function SelectAll(chkbox) {
            for (i = 1; i < document.all.gvlist.rows.length; i++) {
                gvlist.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = chkbox.checked;
            }
        }
    </script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table cellspacing="2" cellpadding="2" width="960" bgcolor="white" border="0" style="margin-top: 4px;">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; margin: 2px auto; font-family: 微软雅黑;">
                <td style="width: 3px; background-image: url(../images/bg_tb1.jpg); background-repeat: no-repeat;
                    background-position: left top;">
                </td>
                <td align="right">
                    <asp:Button ID="btnBack" runat="server" Text="Close Window" OnClientClick="window.close();"
                        CssClass="SmallButton3" Width="120" OnClick="btnBack_Click" />
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;
                    background-position: right top;">
                </td>
            </tr>
<%--            <tr>
                <td colspan="3">
                    <asp:Button ID="btnToPlan" runat="server" CssClass="SmallButton2" Text="ToPlan" 
                        onclick="btnToPlan_Click"></asp:Button>
                    <asp:Button ID="btnReject" runat="server" CssClass="SmallButton2" Text="Reject" onclick="btnReject_Click" 
                       ></asp:Button>
                    <asp:Button ID="btnImport" runat="server" CssClass="SmallButton2" Text="Import" onclick="btnImport_Click" 
                       ></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton2" Text="Cancel" onclick="btnCancel_Click" 
                       ></asp:Button>
                </td>
            </tr>--%>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="20" Width="1100px" OnRowDataBound="gvlist_RowDataBound"
                        OnPageIndexChanging="gvlist_PageIndexChanging" DataKeyNames="isrejected,det_inBigOrder,notNeeded,id"
                        CssClass="GridViewStyle">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
     <%--                       <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server"/>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Line">
                                <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("poLine")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Part #">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("partNbr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QAD #">
                                <HeaderStyle Width="00px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQadPart" runat="server" Text='<%# Bind("qadPart")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SKU #">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                <ItemStyle Width="70px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("sku")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Site">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" />
                                <ItemTemplate>
                                    <asp:Label ID="Labe22" runat="server" Text='<%# Bind("site")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Qty">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ordQty")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UM">
                                <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPoNbr" runat="server" Text='<%# Bind("um")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("price")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Require Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("reqDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("dueDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Input Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_dateInfo" runat="server" Text='<%# Bind("dateInfo","{0:yyyy-MM-dd}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="To Plan">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="btnToPlan" runat="server" Text="ToPlan" Width='50' CommandArgument='<%# Eval("id") %>'
                                        CssClass="SmallButton2" CommandName="ToPlan" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnReject" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Reject"
                                        CssClass="SmallButton2" Text="Reject" Width="47px" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="大订单" Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="btnBigOrder" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="BigOrder"
                                        CssClass="SmallButton2" Text="显示" Width="47px" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Import">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkImport" runat="server" CommandName="need">需要</asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cancel">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkCancel" runat="server" CommandName="CancelDEI">取消</asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Error Message">
                                <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_err" runat="server" Text='<%# Bind("errMsg")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
