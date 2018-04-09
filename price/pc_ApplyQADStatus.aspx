<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_ApplyQADStatus.aspx.cs" Inherits="price_pc_ApplyQADStatus" %>

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
        <table>
            <tr>
                <td>QAD号:&nbsp;&nbsp;<asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox5"></asp:TextBox>&nbsp;&nbsp;</td>
                <td>供应商:&nbsp;&nbsp;<asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox5"></asp:TextBox>&nbsp;&nbsp;</td>
                <td>供应商名称:&nbsp;&nbsp;<asp:TextBox ID="txtVenderName" runat="server" CssClass="SmallTextBox5"></asp:TextBox>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" Width="70px" Height="16px">
                <asp:ListItem Value="7" Text="全部"></asp:ListItem>
                <asp:ListItem Value="8" Text="未完成"></asp:ListItem>
                <asp:ListItem Value="0"  Text="未提交"></asp:ListItem>
                <asp:ListItem Value="1"  Text="已提交"></asp:ListItem>
                <asp:ListItem Value="2"  Text="正在询价"></asp:ListItem>
                <asp:ListItem  Value="3" Text="已报价"></asp:ListItem>
                <asp:ListItem Value="4"  Text="已核价"></asp:ListItem>
                <asp:ListItem Value="5"  Text="已提交财务审批"></asp:ListItem>
                <asp:ListItem Value="6"  Text="审批通过"></asp:ListItem>
                <asp:ListItem Value="-1"  Text="驳回"></asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;</td>
                <td><asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2" 
                        onclick="btnSelect_Click" /></td>
                 <td><asp:Button ID="btnExport" runat="server" Text="导出" CssClass="SmallButton2" OnClick="btnExport_Click" 
                        /></td>
            </tr>
        </table>
        <asp:GridView ID="gvInfo" runat="server" CssClass="GridViewStyle" PageSize="20" 
            AllowPaging="true" AutoGenerateColumns="false" 
            onpageindexchanging="gvInfo_PageIndexChanging" onrowdatabound="gvInfo_RowDataBound"
            >
           <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
         <asp:BoundField HeaderText="QAD" DataField="QAD">
                    <HeaderStyle Width="100px" />
                    <ItemStyle  HorizontalAlign="Center"/>
         </asp:BoundField>
           <asp:BoundField HeaderText="部件号" DataField="ItemCode">
                    <HeaderStyle Width="150px" />
         </asp:BoundField>
           <asp:BoundField HeaderText="供应商" DataField="vender">
                    <HeaderStyle Width="80px" />
                    <ItemStyle  HorizontalAlign="Center"/>
         </asp:BoundField>
          <asp:BoundField HeaderText="供应商名称" DataField="venderName">
                    <HeaderStyle Width="250px" />
         </asp:BoundField>
            <asp:BoundField HeaderText="零件审批状态" DataField="appvResult">
                    <HeaderStyle Width="80px" />
         </asp:BoundField>
            <asp:BoundField HeaderText="询价状态" DataField="status">
                    <HeaderStyle Width="150px" />
         </asp:BoundField>
            <asp:BoundField HeaderText="报价处理结果" DataField="PriceCancel">
                    <HeaderStyle Width="200px" />
         </asp:BoundField>
            <asp:BoundField HeaderText="核价处理结果" DataField="CheckPriceCancel">
                    <HeaderStyle Width="200px" />
         </asp:BoundField>
          
        </Columns>
        </asp:GridView>
    </div>
    </form>
      <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
