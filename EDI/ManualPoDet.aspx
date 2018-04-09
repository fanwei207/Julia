<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManualPoDet.aspx.cs" Inherits="ManualPoDet" %>

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
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 970px">
            <tr>
                <td align="right" style="width: 956px; height: 17px">
                </td>
            </tr>
            <tr>
                <td style="width: 956px">
                    <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        PageSize="20" OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
                        DataKeyNames="mpod_id,mpod_hrd_id,mpod_submittedBy" OnRowDeleting="gvlist_RowDeleting">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="mpod_line" HeaderText="Line">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_cust_part" HeaderText="Cust Part">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_qad" HeaderText="QAD">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_ord_qty" HeaderText="Qty">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_um" HeaderText="UM">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_price" HeaderText="Price">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_req_date" HeaderText="Req Date" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_due_date" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_rmks" HeaderText="Remarks">
                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_createdName" HeaderText="Ord By">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_createdDate" HeaderText="Ord Date" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mpod_sod_site" HeaderText="Site">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True">
                                <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:CommandField>
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
