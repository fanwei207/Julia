<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_ApplyQADStatus.aspx.cs" Inherits="price_pcm_ApplyQADStatus" %>

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
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" Width="70px">
                <asp:ListItem Value="7" Text="全部"></asp:ListItem>
                <asp:ListItem Value="1"  Text="已提交"></asp:ListItem>
                <asp:ListItem Value="2"  Text="正在询价"></asp:ListItem>
                <asp:ListItem  Value="3" Text="已报价"></asp:ListItem>
                <asp:ListItem Value="4"  Text="已核价"></asp:ListItem>
                <asp:ListItem Value="5"  Text="已提交财务审批"></asp:ListItem>
                <asp:ListItem Value="6"  Text="审批通过"></asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;</td>
                <td><asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2" 
                        onclick="btnSelect_Click" /></td>
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
         <asp:BoundField HeaderText="QAD" DataField="Part">
                    <HeaderStyle Width="100px" />
                    <ItemStyle  HorizontalAlign="Center"/>
         </asp:BoundField>
           <asp:BoundField HeaderText="部件号" DataField="ItemCode">
                    <HeaderStyle Width="150px" />
         </asp:BoundField>
           <asp:BoundField HeaderText="供应商" DataField="Vender">
                    <HeaderStyle Width="80px" />
                    <ItemStyle  HorizontalAlign="Center"/>
         </asp:BoundField>
          <asp:BoundField HeaderText="供应商名称" DataField="VenderName">
                    <HeaderStyle Width="250px" />
         </asp:BoundField>
           <asp:TemplateField HeaderText="技术部指定">
                    <ControlStyle Font-Bold="False" Font-Size="12px"/>
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbIsAppoint" runat="server" Text='<%# Eval("InfoFrom") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="状态">
                    <ControlStyle Font-Bold="False" Font-Size="12px"/>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbStatue" runat="server" Text='<%# Eval("status") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField HeaderText="报价处理结果">
                    <ControlStyle Font-Bold="False" Font-Size="12px"/>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbQuoted" runat="server" Text='<%# Eval("PriceCancel") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField HeaderText="核价处理结果">
                    <ControlStyle Font-Bold="False" Font-Size="12px"/>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbCheck" runat="server" Text='<%# Eval("CheckPriceCancel") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        </asp:GridView>
    </div>
    </form>
      <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
