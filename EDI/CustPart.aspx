<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustPart.aspx.cs" Inherits="CustPart" %>

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
            <table cellspacing="0" cellpadding="0" bgcolor="white" border="0" style="width: 988px;">
                <tr class="main_top">
                    <td class="main_left"></td>
                    <td style="height: 1px">
                        
                          <asp:DropDownList ID="dropDomain" runat="server" CssClass="Param" Visible="False">
                              <asp:ListItem Selected="True">--</asp:ListItem>
                        <asp:ListItem>SZX</asp:ListItem>
                        <asp:ListItem>ZQL</asp:ListItem>
                        <asp:ListItem>YQL</asp:ListItem>
                        <asp:ListItem>HQL</asp:ListItem>
                        <asp:ListItem>ATL</asp:ListItem>
                        <asp:ListItem>ZQZ</asp:ListItem>
                        <asp:ListItem>TCP</asp:ListItem>
                    </asp:DropDownList>
                        客户/货物发往：
                    <asp:TextBox ID="txtCust" runat="server" CssClass="smalltextbox Param" Width="104px"></asp:TextBox>
                        客户物料：
                        <asp:TextBox
                            ID="txtPart" runat="server" CssClass="smalltextbox Param" Width="122px"></asp:TextBox>
                        物料号：
                        <asp:TextBox
                            ID="txtQad" runat="server" CssClass="smalltextbox Param" Width="115px"></asp:TextBox>
                        有效时间：<asp:TextBox
                            ID="txtStdDate" runat="server" CssClass="smalltextbox Date Param" Width="81px"></asp:TextBox><asp:TextBox
                                ID="txtEndDate" runat="server" CssClass="smalltextbox Date Param" Width="82px" Visible="False"></asp:TextBox>
                        <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" OnClick="btnQuery_Click"
                            Text="查询" Width="50px" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton2" OnClick="btnExcel_Click"
                            Text="Excel" Width="50px" />
                    </td>
                    <td class="main_right"></td>
                </tr>
            </table>
            <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
                DataKeyNames="cp_id,cp_part" OnRowDeleting="gvlist_RowDeleting" PageSize="30"
                CssClass="GridViewStyle GridViewRebuild" OnRowCommand="gvlist_RowCommand">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                   
                    <asp:BoundField HeaderText="客户/货物发往" DataField="cp_cust">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="客户物料" DataField="cp_cust_part">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="物料号" DataField="cp_part">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="生效日期" DataField="cp_start_date" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="截止日期" DataField="cp_end_date" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="说明" DataField="cp_comment">
                        <HeaderStyle Width="300px" />
                        <ItemStyle Width="300px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="显示客户物料" DataField="cp_cust_partd">
                        <HeaderStyle Width="250px" />
                        <ItemStyle Width="250px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="SKU" DataField="Xord_Cust_Item">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="ltnEdit" Text="<u>修改</u>" ForeColor="Blue" Font-Size="12px" runat="server"
                                CommandName="myEdit" />
                        </ItemTemplate>
                        <HeaderStyle Width="40px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" DeleteText="&lt;u&gt;删除&lt;/u&gt;">
                        <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
