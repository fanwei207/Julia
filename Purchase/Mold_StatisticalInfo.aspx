<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mold_StatisticalInfo.aspx.cs" Inherits="Purchase_Mold_StatisticalInfo" %>

<!DOCTYPE html>

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
    <table width="700px">
        <tr>
            <td align="left" >产品型号<asp:TextBox runat="server" Width="100px" ID="txt_itemCode"></asp:TextBox></td>
            <td align="left" >供应商<asp:TextBox runat="server" Width="120px" ID="txt_vend" CssClass="Supplier"></asp:TextBox></td>
            <td align="left"><asp:CheckBox runat ="server" ID="chb_status" Text="停用" /></td>
            <td align="left"><asp:Button runat ="server" Width ="70px" CssClass="SmallButton2" Text="查询" ID="btn_search" OnClick="btn_search_Click" /></td>
        </tr>
        <tr>            
            <td colspan="4" align="left">模具数量<asp:Label runat="server" Width ="70px" ID="lbl_Qty"></asp:Label></td>
        </tr>
    </table>
        <asp:GridView runat="server" ID="gv" Width="600px" PageSize="20" AutoGenerateColumns="false" AllowPaging="true" 
                 CssClass="GridViewStyle" OnPageIndexChanging="gv_PageIndexChanging">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="供应商" DataField="Mold_Vend">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="模具编号" DataField="Mold_Nbr">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="状态" DataField="Mold_Status">
                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="产能" DataField="Mold_Capacity">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Right" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="生产寿命" DataField="Mold_WorkingLife">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Right" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="已生产数" DataField="Mold_Qty">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>                    
                    <asp:BoundField HeaderText="备注" DataField="remark">
                        <HeaderStyle Width="220px" HorizontalAlign="Center" />
                        <ItemStyle Width="220px" HorizontalAlign="Left" ForeColor="Black" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
    </div>
    </form>
</body>
</html>
