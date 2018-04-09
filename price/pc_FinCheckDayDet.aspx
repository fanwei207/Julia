<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_FinCheckDayDet.aspx.cs" Inherits="price_pc_FinCheckDayDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
        <div>
            供应商：&nbsp;&nbsp;<asp:Label ID="lbVender" runat="server"></asp:Label>&nbsp;&nbsp;
            供应商名称：&nbsp;&nbsp;<asp:Label ID="lbVenderName" runat="server"></asp:Label>&nbsp;&nbsp;
            日期：&nbsp;&nbsp;<asp:Label ID="lbDate" runat="server"></asp:Label>&nbsp;&nbsp;
            <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" Text="导出" 
                onclick="btnExport_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnReturn" runat="server" CssClass="SmallButton2" Text="返回" 
                onclick="btnReturn_Click" />
        </div>
        <div>
            <asp:GridView ID="gvInfo" EmptyDataText="No Date"  runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" Width="1750px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns >
            <asp:BoundField HeaderText="QAD" DataField="Part" Visible="true">
               <HeaderStyle Width="100px" />
                   <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="部件号" DataField="ItemCode" Visible="true">
               <HeaderStyle Width="200px" />
            </asp:BoundField>
             <asp:BoundField HeaderText="单位" DataField="UM" Visible="true">
               <HeaderStyle Width="50px" />
                   <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
              <asp:BoundField HeaderText="币种" DataField="Curr" Visible="true">
               <HeaderStyle Width="50px" />
                   <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
               <asp:BoundField HeaderText="核价价格" DataField="FinCheckPrice" Visible="true">
               <HeaderStyle Width="50px" />
            </asp:BoundField>
              <asp:BoundField HeaderText="生效时间" DataField="CheckPriceStartDate" Visible="true">
               <HeaderStyle Width="100px" />
                   <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
              <asp:BoundField HeaderText="需求规格" DataField="Formate" Visible="true">
               <HeaderStyle Width="300px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="详细描述" DataField="ItemDescription" Visible="true">
               <HeaderStyle Width="300px" />
            </asp:BoundField>
             <asp:BoundField HeaderText="描述1" DataField="ItemDesc1" Visible="true">
               <HeaderStyle Width="300px" />
            </asp:BoundField>
             <asp:BoundField HeaderText="描述2" DataField="ItemDesc2" Visible="true">
               <HeaderStyle Width="300px" />
            </asp:BoundField>


            </Columns>
            </asp:GridView>
        
        </div>
    </div>
    </form>
     <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
