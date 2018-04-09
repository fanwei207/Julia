<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_FinCheckDayList.aspx.cs" Inherits="price_pc_FinCheckDayList" %>

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
            供应商：&nbsp;&nbsp;<asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox5" ></asp:TextBox>&nbsp;&nbsp;
            供应商名称：&nbsp;&nbsp;<asp:TextBox ID="txtVenderName" runat="server" CssClass="SmallTextBox5"></asp:TextBox>&nbsp;&nbsp;
            日期：&nbsp;&nbsp;<asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date"></asp:TextBox>&nbsp;&nbsp;
            <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2" 
                onclick="btnSelect_Click" />&nbsp;&nbsp;
        </div>
        <div>
        <asp:GridView ID="gvInfo" CssClass="GridViewStyle" AutoGenerateColumns="false" 
                runat="server" DataKeyNames="vender,CheckPriceLoadDate,num" 
                onrowcommand="gvInfo_RowCommand" onrowdatabound="gvInfo_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />


             <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="290px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                   
                                    <asp:TableCell HorizontalAlign="center" Text="供应商" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="处理数量" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="处理日期" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="view" Width="50px"></asp:TableCell>
                                
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="5"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                <Columns>
                      
                         <asp:BoundField HeaderText="供应商" DataField="Vender" Visible="true">
                          <HeaderStyle Width="80px" />
                              <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField HeaderText="供应商名称" DataField="VenderName" Visible="true">
                          <HeaderStyle Width="300px" />
                       </asp:BoundField>
                           <asp:BoundField HeaderText="处理数量" DataField="num" Visible="true">
                          <HeaderStyle Width="80px" />
                       </asp:BoundField>
                       <asp:BoundField HeaderText="处理日期" DataField="CheckPriceLoadDate" Visible="true">
                          <HeaderStyle Width="80px" />
                              <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:TemplateField HeaderText="view" Visible="true">
                                <ControlStyle Font-Bold="False" Font-Size="14px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px"  Font-Size="14px"/>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnList" CommandName="lkbtnList" CommandArgument=' <%# Eval("VenderName") %>'
                                        Text="view" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
        
        </Columns> 
        </asp:GridView>
        </div>
    </div>
    </form>
     <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
