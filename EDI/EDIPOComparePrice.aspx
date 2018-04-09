<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDIPOComparePrice.aspx.cs" Inherits="EDIPOComparePrice" %>

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
    <div align="left">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" bgcolor="white" border="0" style="width: 1000px;
            margin-top: 4px;">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; font-family: 微软雅黑;">
                <td Width="30px" >
                    <asp:Label ID="lb_cust" Text="Cust" runat="server" Width="30px" ></asp:Label>
                </td>
                <td Width="82px" >
                    <asp:TextBox ID="txtCust" runat="server" Width="82px" 
                        MaxLength="8"></asp:TextBox>
                </td>
                <td Width="60px" >
                    <asp:Label ID="Label1" Text="Cust Po" runat="server" Width="60px" ></asp:Label>
                </td>
                <td Width="385px">
                    <asp:TextBox ID="txtPoNbr" runat="server" Width="165px"  MaxLength="24"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddl_hist" runat="server" Width="60px">
                        <asp:ListItem Text=0>--</asp:ListItem>
                        <asp:ListItem Text=1>历史</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click"
                        Text="Query" Width="50px"/>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click"
                        Text="Submit" Width="60px"/>
                </td>
                <td width="60%">&nbsp;</td>
                <td width="50%">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="5" style="height: 4px;">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="18" OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
                        
                        DataKeyNames="poNbr,poline,price,Pi_price3,Pi_StartDate,Pi_EndDate" Width="1000px"
                        OnRowDeleting="gvlist_RowDeleting" OnRowCommand="gvlist_RowCommand" 
                        CssClass="GridViewStyle AutoPageSize">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <input id="chkImport" type="checkbox" name="chkImport" runat="server" value='<%#Eval("poNbr") %>' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Cust" DataField="cusCode">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cust Po" DataField="poNbr">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Po Line" DataField="poLine">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ship To" DataField="shipto">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Qad Part" DataField="qadPart">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Req Date" DataField="reqDate" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle Width="70px" />
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Due Date" DataField="dueDate" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle Width="70px" />
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Porec Date" DataField="det_porecdate" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ord Qty" DataField="ordqty">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Po Price" DataField="Price">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fin Price" DataField="Pi_price3">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fin Start" DataField="Pi_StartDate" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fin End" DataField="Pi_EndDate" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
