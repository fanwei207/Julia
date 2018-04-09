<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Po_det.aspx.cs" Inherits="plan_Po_det" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table style="width: 1000px;" cellspacing="0" cellpadding="4" class="table05">
            <tr>
                <td>
                    采购单:<asp:Label ID="lblpov_nbr" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    供应商:<asp:Label ID="lblpov_vend" runat="server"
                        Width="200px"></asp:Label>
                </td>
                <td>
                    地点:<asp:Label ID="lblpov_ship" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    域:<asp:Label ID="lblpov_domain" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    订货日期:<asp:Label ID="lblpov_ord_date" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    截止日期:<asp:Label ID="lblpov_due_date" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    合计价格:<asp:Label ID="lblpov_TotleCost" runat="server" Width="100px" ></asp:Label>
                </td>
                <td>
                   
                        
                </td>
            </tr>
            <tr>
                <td colspan ="3">
                    审批意见：<asp:TextBox ID="txtremark" runat="server" CssClass="smalltextbox" Width="500px" ></asp:TextBox>
                </td>
               
                <td>
                     &nbsp;&nbsp;
                    <asp:Button ID="btnPass" runat="server" Text="通过" Width="60px" CssClass="SmallButton3"
                        OnClick="btnPass_Click" />
                     <asp:Button ID="btnFail" runat="server" Text="拒绝" Width="60px" CssClass="SmallButton3" OnClick="btnFail_Click" />

                </td>

            </tr>
            <tr>
                <td colspan="4" class="Remark" style="text-align: left; vertical-align: bottom; color: Red;">
                    说明:"!" 供应商改变;"C" 订单项截止日期不一致;"#" 订单项行数不一致;"X" 订单项已被取消;"Q" 订单项数量不一致;"P"QAD号发生变化
                </td>

            </tr>
            
          
        </table>
        <asp:GridView ID="gvShipdetail" runat="server" AutoGenerateColumns="False" 
            Width="1000px" 
           
            CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                    <asp:BoundField DataField="povd_stat" HeaderText="Flag">
                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="povd_nbr" HeaderText="采购单">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="povd_line" HeaderText="行">
                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="povd_part" HeaderText="零件号">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>

                     <asp:BoundField DataField="description" HeaderText="零件描述">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:BoundField>


                    <asp:BoundField DataField="povd_due_date" HeaderText="截止日期" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="fullDocs" HeaderText="文档齐全">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:BoundField>
                  <asp:BoundField DataField="isprice" HeaderText="是否有价格">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:BoundField>
                 <asp:BoundField DataField="Mold_lock" HeaderText="模具未确定">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="povd_qty_ord" HeaderText="订货数量" DataFormatString="{0:#,##0.0}"
                        HtmlEncode="False">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                    </asp:BoundField>

                  
                    <asp:TemplateField HeaderText="单价">
                    <ItemTemplate>
                     <asp:Label ID="lblvisitName" runat="server" Text='<%# Bind("povd_std_cost","{0:#,##0.00000}") %>' Visible='<%# Eval("isShow") %>'  ></asp:Label>
                       
                    </ItemTemplate>
                    <ControlStyle Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="总价">
                    <ItemTemplate>
                     <asp:Label ID="lblvisitName" runat="server" Text='<%# Bind("povd_cost","{0:#,##0.00000}") %>' Visible='<%# Eval("isShow") %>' ></asp:Label>
                       
                    </ItemTemplate>
                    <ControlStyle Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
        <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>

