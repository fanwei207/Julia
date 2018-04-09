<%@ Page Language="C#" AutoEventWireup="true" CodeFile="soque_list.aspx.cs" Inherits="soque_list" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" 
            style="width: 978px;">
            <tr>
                <td>
                    Order：<asp:TextBox ID="txtNbr" runat="server" CssClass="smalltextbox" Width="83px"
                        MaxLength="20"></asp:TextBox>&nbsp; Product：<asp:TextBox ID="txtPart" runat="server"
                            CssClass="smalltextbox" Width="87px" MaxLength="30"></asp:TextBox>&nbsp;
                    Cust：<asp:TextBox ID="txtCust" runat="server" CssClass="smalltextbox" Width="83px"
                        MaxLength="15"></asp:TextBox>&nbsp; Submitter：<asp:TextBox ID="txtCreatedBy" runat="server"
                            CssClass="smalltextbox" Width="83px" MaxLength="6"></asp:TextBox>&nbsp;
                    Status：<asp:DropDownList ID="ddlStatus" runat="server" Width="80px" 
                        AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">Unfinished</asp:ListItem>
                        <asp:ListItem Value="1">Finished</asp:ListItem>
                    </asp:DropDownList>
                    Close Date:<asp:TextBox ID="txtCloseDate" runat="server" CssClass="smalltextbox Date" Width="83px"
                        MaxLength="15"></asp:TextBox>

                    &nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" OnClick="btnQuery_Click"
                        Text="Query" Width="50px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton2" OnClick="btnExcel_Click"
                        Text="Excel" Width="50px" />
                </td>
                <td align="right">
                    <asp:Button ID="btnClose" runat="server" CssClass="SmallButton2" OnClick="btnClose_Click"
                        Text="Close" Width="50px" />&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False" Width="940px"
            PageSize="23" OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
            OnRowCommand="gvlist_RowCommand" DataKeyNames="soque_id,soque_status" CssClass="GridViewStyle AutoPageSize">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="Order" DataField="soque_nbr">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Line" DataField="soque_line">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Product" DataField="soque_cus_part" HtmlEncode="False">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Qty" DataField="soque_qty_ord" HtmlEncode="False">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Customer" DataField="soque_cus">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Ord Date" DataField="soque_date_ord" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Ship Date" DataField="soque_date_ship" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="QAD" DataField="soque_part" HtmlEncode="False">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Status" DataField="soque_status" HtmlEncode="False">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Submitter" DataField="soque_createdName">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Degree" DataField="soque_degreeName">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkHist" runat="server" CommandName="myHist" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black" Text="<u>History</u>" CommandArgument='<%# Bind("soque_id") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px"  />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" runat="server" CommandName="myEdit" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black" Text="<u>Edit</u>" CommandArgument='<%# Bind("soque_id") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
