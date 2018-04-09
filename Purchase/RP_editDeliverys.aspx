<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RP_editDeliverys.aspx.cs" Inherits="Purchase_RP_editDeliverys" %>

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
       <div>
        <table id="table1" border="0" style="text-align: center; height: 450px; width: 1000px;
            height: 20px;" cellpadding="0" cellspacing="0">
            <tr align="left" style="font-size: 8pt">
                <td style="height: 10px;" colspan="2">
                    <font style="color: Red; font-size: 11px;"><span style="color: #000000">采购单:<asp:Label
                        ID="lblPo" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>
                        &nbsp; &nbsp; &nbsp; 供应商:<asp:Label ID="lblVend" runat="server" CssClass="LabelCenter"
                            Font-Bold="False"></asp:Label></span></font> &nbsp; &nbsp; &nbsp; 地点:<asp:Label ID="lblSite"
                                runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>
                    &nbsp; &nbsp; &nbsp; 域:<asp:Label ID="lblDomain" runat="server" CssClass="LabelCenter"
                        Font-Bold="False"></asp:Label>
                    &nbsp; &nbsp; &nbsp;订货日期:<asp:Label ID="lblOrderDate" runat="server" CssClass="LabelCenter"
                        Font-Bold="False"></asp:Label>
                    &nbsp; &nbsp; &nbsp; 截止日期:<asp:Label ID="lblDueDate" runat="server" CssClass="LabelCenter"
                        Font-Bold="False"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="width: 600px; height: 10px;">
                  <asp:CheckBox ID="chkAll" runat="server" Text="全选" Width="60px"
                            AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                      送货单:<asp:Label ID="lblDelivery" runat="server" CssClass="LabelCenter"></asp:Label>
                    &nbsp; &nbsp;&nbsp;总行数:<asp:Label ID="lblPodCount" runat="server" CssClass="LabelCenter"
                        Font-Bold="False"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 送货单每页显示记录数:<asp:TextBox ID="txtPageSize" runat="server"
                        CssClass="TextRight" Height="20px" Width="38px">10</asp:TextBox>
                </td>
                <td style="width: 600px; height: 20px;" align="right">
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btn_Save" runat="server" CausesValidation="true" CssClass="SmallButton2"
                        OnClick="btn_Save_Click" Text="保存" ValidationGroup="validator" Width="50px" />
                    &nbsp;&nbsp; &nbsp;<asp:Button ID="btnPrint" runat="server" CausesValidation="true"
                        CssClass="SmallButton2" OnClick="btnPrint_Click" Text="打印" ValidationGroup="validator"
                        Width="50px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CausesValidation="true" CssClass="SmallButton2"
                        OnClick="btnBack_Click" Text="返回" ValidationGroup="validator" Width="50px" />
                </td>
            </tr>
            <tr align="left" style="vertical-align: top; height: 20px;">
                <td style="height: 20px" colspan="2">
                    <asp:GridView ID="dtgList" runat="server" Style="vertical-align: top" CssClass="GridViewStyle"
                        AllowSorting="True" AllowPaging="True" PageSize="20" 
                        AutoGenerateColumns="False" OnRowDataBound="dtgList_RowDataBound"
                        DataKeyNames="isExists,prd_factory,pt_part,prd_part,prd_qty_sum,prd_qty_ord,prd_line,prd_qty_dev" 
                        OnRowCommand="dtgList_RowCommand" 
                        onpageindexchanging="dtgList_PageIndexChanging" Width="1000px">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSinger" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                <HeaderTemplate>
                                   <%-- <asp:CheckBox ID="chkAll" runat="server" ForeColor="Black" AutoPostBack="False" onclick="doSelect()" />--%>
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="prd_line" HeaderText="行号">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_part" HeaderText="物料编码">
                                <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                <ItemStyle Width="120px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_xpart" HeaderText="品名/规格/型号">
                                <HeaderStyle Width="270px" HorizontalAlign="Center" />
                                <ItemStyle Width="270px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_um" HeaderText="单位">
                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_qty_ord" HeaderText="订单数">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_qty_short" HeaderText="欠交数">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_qty_sum" HeaderText="已送出">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="送货数">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_prd_qty_dev" Text='<%# Bind("prd_qty_dev") %>' runat="server"
                                        CssClass="TextRight" Width="92%" Style="ime-mode: disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57) && event.keyCode!=46) event.returnValue=false;"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="60px" Font-Bold="True" />
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                            </asp:TemplateField>
                           
                            <asp:BoundField DataField="prd_qty_dev" HeaderText="本次送货数量">
                                <HeaderStyle Width="0px" HorizontalAlign="Center" />
                                <ItemStyle Width="0px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_appv" HeaderText="验收状态">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <font class="Remark" style="color: Red; font-size: 11px; text-align: left">注意:1、勾选表示该物料将参与到送货单发运;如要删除某一行，则取消勾选即可
                        2、参与送货的订单行必须上传质量检验报告 3、当"上传"两字的背景色为绿色时表示该行已经上传了质量检验报告<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 4、当订单行显示灰色时，则表明此行被质检停止送货</font>
                </td>
            </tr>
        </table>
    </div>
          </form>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
