<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_ShipSumit.aspx.cs" Inherits="SID_ShipSumit" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
         <table id="table2" cellspacing="0" cellpadding="0" width="980px">
            <tr>
                
               <td align="left" width="150px">
                 单&nbsp;&nbsp;号:<asp:TextBox ID="txt_nbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
               </td>
               <td align="left" width="150px">
                 批&nbsp;&nbsp;号:<asp:TextBox ID="TextBox1" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
               </td>
                <td  width="250px">
                    &nbsp;日期:<asp:TextBox ID="txt_startdate" runat="server" CssClass="SmallTextBox Date"
                    onkeydown="event.returnValue=false;" onpaste="return false;" Width="100px"></asp:TextBox>
                    -<asp:TextBox ID="txt_endate" runat="server" CssClass="SmallTextBox Date" Width="100px"></asp:TextBox>
                </td>
                <td width="150px">
                    &nbsp;所在公司:<asp:TextBox ID="txt_domain" runat="server" CssClass="SmallTextBox" Width="80px"></asp:TextBox>
                </td>
                <td width="150px">
                    &nbsp;事务类型:
                    <asp:DropDownList ID="ddl_type" runat="server" width="80px">
                        <asp:ListItem Value=ISS-SO>销售出运</asp:ListItem>
                        <asp:ListItem Value=ISS-WO>工单领料</asp:ListItem>
                        <asp:ListItem Value=RCT-PO>采购收货</asp:ListItem>
                        <asp:ListItem Value=RCT-WO>成品入库</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" >
                  <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" Width="40"
                        OnClick="btnSearch_Click" />
                                    &nbsp;&nbsp;&nbsp;
                   <%-- <asp:Button ID="btn_submit" runat="server" CssClass="SmallButton3" 
                        Text="提交" Width="40px"
                        OnClick="btn_submit_Click" />--%>
                    &nbsp; &nbsp;&nbsp;&nbsp;                                           
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" Width="60px" AutoPostBack="True"
                        OnCheckedChanged="chkAll_CheckedChanged" />
                </td>
                 <td width="130px" colspan="2">
                    &nbsp;状态:
                <%-- </td>
                 <td width="130px" colspan="6">--%>

                           <asp:RadioButton ID="rad1" runat="server" Text="未入账" AutoPostBack="True" Checked="True"
                        GroupName="RadGroup" oncheckedchanged="rad1_CheckedChanged" ></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="rad2" runat="server" Text="已确认" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" oncheckedchanged="rad2_CheckedChanged" ></asp:RadioButton>&nbsp;
                     <asp:RadioButton ID="RadioButton1" runat="server" Text="已入账" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" oncheckedchanged="rad2_CheckedChanged" ></asp:RadioButton>&nbsp;                                 
                </td>
                <td colspan="3" align="left">
                    <asp:Button ID="btn_back" Text="退回" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_submit" Text="确认" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_cimload" Text="入账" runat="server" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" Text="导出" runat="server" />
                </td>

            </tr>
         
        </table>
        <asp:GridView ID="gvorder" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
            DataKeyNames="wh_id,wh_nbr" OnRowCommand="gvorder_RowCommand" OnPageIndexChanging="gvorder_PageIndexChanging"
            OnRowDataBound="gvorder_RowDataBound" Width="980px" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Select" runat="server" Width="20px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="wh_domain" HeaderText="域">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="wh_site" HeaderText="地点">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="wh_nbr" HeaderText="单号">
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="wh_lot" HeaderText="批号">
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="wh_line" HeaderText="行号">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="wh_loc" HeaderText="库位">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="wh_part" HeaderText="物料号">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="wh_qty_loc" HeaderText="数量"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="wh_serial" HeaderText="批序号">
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="wh_price" HeaderText="价格"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="wh_type" HeaderText="类型">
                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
<%--                <asp:BoundField DataField="insp_matchdate" HeaderText="预配日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>--%>
<%--                 <asp:ButtonField CommandName="Detail1" Text="<u>详细</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>--%>
<%--
                <asp:TemplateField HeaderText="送货确认">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org3_con") %>' Text='<%# Eval("SID_org3_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm3" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>--%>

            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
