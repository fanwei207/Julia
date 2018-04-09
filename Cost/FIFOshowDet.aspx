<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FIFOshowDet.aspx.cs" Inherits="EDI_FIFOshowDet" %>

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
                CssClass="GridViewStyle GridViewRebuild" OnPageIndexChanged="gvlist_PageIndexChanged" OnPageIndexChanging="gvlist_PageIndexChanging" Width="2500px" >
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                   
                     <asp:BoundField HeaderText="产品" DataField="Product">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="零件" DataField="Part">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="描述" DataField="Description">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" HorizontalAlign="left" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="单位" DataField="EA">
                        <HeaderStyle Width="40px" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="用量" DataField="Consumption">
                        <HeaderStyle Width="70px" />
                        <ItemStyle Width="70px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="损耗率" DataField="attrition">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="实用量" DataField="Practical">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="标准本层" DataField="layerStandard">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="标准低层" DataField="StandardLower" >
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="GL单价" DataField="GLPrice">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="GL金额" DataField="GLAmount" 
                        >
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="当前本层" DataField="CurrentLayer" 
                       >
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="当前低层" DataField="CurrentLowLevel">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CU单价" DataField="CUPrice">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CU金额" DataField="CUAmount" >
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="差异" DataField="Difference">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="层次" DataField="gradation">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="采购价日期" DataField="PurchasePriceDate" DataFormatString="{0:yyyy-MM-dd}"  HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="center" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="供应商" DataField="Supplier">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" HorizontalAlign="left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="域" DataField="domain">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="发放原则" DataField="GrantingPrinciples">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="组件类型" DataField="ComponentType">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="汇率" DataField="ExchangeRate">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="工时" DataField="Hours">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="工费(委外)" DataField="LaborCosts">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="工单" DataField="SOPO">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="下达日期" DataField="ReleaseDate" DataFormatString="{0:yyyy-MM-dd}"  HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="center" />
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