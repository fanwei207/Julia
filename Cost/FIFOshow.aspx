<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FIFOshow.aspx.cs" Inherits="EDI_FIFOshow" %>

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
                        
                         SOPO：
                    <asp:TextBox ID="txtSOPO" runat="server" CssClass="smalltextbox Param" Width="104px"></asp:TextBox>
                        SO：
                        <asp:TextBox
                            ID="txtSO" runat="server" CssClass="smalltextbox Param" Width="122px"></asp:TextBox>
                        INV NO：
                        <asp:TextBox
                            ID="txtINV" runat="server" CssClass="smalltextbox Param" Width="115px"></asp:TextBox>
                       </td>
                    <td align="right">
                        <asp:Button ID="btnquery" runat="server" CssClass="SmallButton2" 
                            Text="查询" Width="50px" OnClick="btnquery_Click"  />
                        <asp:Button ID="btnOK" runat="server" CssClass="SmallButton2" 
                            Text="确定" Width="50px" OnClick="btnOK_Click" />
                    </td>
                    <td class="main_right"></td>
                </tr>
            </table>
            <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
               
                DataKeyNames=""  PageSize="30"
                CssClass="GridViewStyle GridViewRebuild" OnPageIndexChanged="gvlist_PageIndexChanged" OnPageIndexChanging="gvlist_PageIndexChanging" >
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                   
                    <asp:BoundField HeaderText="SOPO" DataField="SOPO">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CUST" DataField="Customer">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="SO" DataField="QADSalesOrder">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="LINE" DataField="QADSalesOrderLine">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="WO" DataField="QADWO">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="QAD" DataField="QADItem">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="CUSTITEM" DataField="CustomerItem">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="QTY" DataField="Quantity">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="SHIP__DATE" DataField="ShipDate"  DataFormatString="{0:yyyy-MM-dd}"  HtmlEncode="False">
                        <HeaderStyle Width="250px" />
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="OVERHEAD(RMB)" DataField="OverheadCostRMB" 
                       >
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="InvPrice" DataField="InvoicePriceUSD">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="INV NO" DataField="InvoiceNumber">
                        <HeaderStyle Width="250px" />
                        <ItemStyle Width="250px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DateTimeUpdated" DataField="DateTimeUpdated" DataFormatString="{0:yyyy-MM-dd}"  HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="AluminumPCBCostRMB" DataField="AluminumPCBCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ConnectorCostRMB" DataField="ConnectorCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DriverCostRMB" DataField="DriverCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="HeatSinkCostRMB" DataField="HeatSinkCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="LampBaseCostRMB" DataField="LampBaseCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="LampShadeCostRMB" DataField="LampShadeCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="LEDChipCostRMB" DataField="LEDChipCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="OtherCostRMB" DataField="OtherCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PackageCostRMB" DataField="PackageCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="PlasticCostRMB" DataField="PlasticCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="MaterialCostRMB" DataField="MaterialCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="LaborCostRMB" DataField="LaborCostRMB">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    
                    
                </Columns>
            </asp:GridView>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
