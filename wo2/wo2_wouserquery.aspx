<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_wouserquery.aspx.cs"
    Inherits="wo2_wouserquery" %>

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
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 750px;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 796px;">
                    <asp:TextBox ID="txt_domain" runat="server" Width="65px" CssClass="SmallTextbox Company"></asp:TextBox><asp:TextBox
                        ID="txt_wo_nbr" runat="server" Width="95px" CssClass="SmallTextbox"></asp:TextBox><asp:TextBox
                            ID="txt_wo_lot" runat="server" Width="79px" CssClass="SmallTextbox"></asp:TextBox><asp:TextBox
                                ID="txt_wo_process" runat="server" Width="85px" CssClass="SmallTextbox"></asp:TextBox><asp:TextBox
                                    ID="txt_Fingerprint" runat="server" Width="81px" CssClass="SmallTextbox"></asp:TextBox><asp:TextBox
                                        ID="txt_userNo" runat="server" Width="85px" CssClass="SmallTextbox"></asp:TextBox><asp:TextBox
                                            ID="txt_userName" runat="server" CssClass="SmallTextbox" Width="85px"></asp:TextBox><asp:TextBox
                                                ID="txt_CreatedDate" runat="server" CssClass="SmallTextbox Date" Width="100px"></asp:TextBox>
                    <asp:Button ID="btn_query" runat="server" Width="60px" Text="查询" CssClass="SmallButton2"
                        OnClick="btn_queryClick" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWorkOrder" runat="server" Width="750px" AutoGenerateColumns="False"
            AllowPaging="True" CssClass="GridViewStyle AutoPageSize" PageSize="20" DataKeyNames="id"
            OnPageIndexChanging="myPageIndexChanging" OnPreRender="gvWorkOrder_PreRender">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="Domain" HeaderText="域">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_nbr" HeaderText="工单号">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_lot" HeaderText="ID号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_process" HeaderText="工序代码">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Fingerprint" HeaderText="卡号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="userNo" HeaderText="工号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="userName" HeaderText="姓名">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CreatedDate" HeaderText="创建日期" DataFormatString="{0:yyyy-MM-dd HH:mm:ss:fff}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
