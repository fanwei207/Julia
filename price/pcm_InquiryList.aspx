<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_InquiryList.aspx.cs" Inherits="price_pcm_InquiryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">

        .style2
        {
            width: 100px;
        }
        .style3
        {
            width: 223px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
               <td>&nbsp;&nbsp;&nbsp;&nbsp;询价单号：&nbsp;&nbsp;<asp:TextBox ID="txtIMID" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>&nbsp;&nbsp;</td> 
                <td >供应商：&nbsp;&nbsp;<asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>&nbsp;&nbsp;</td>
                  <td >部件号：&nbsp;&nbsp;<asp:TextBox ID="txtItemCode" runat="server" CssClass="SmallTextBox "  Width="100px"></asp:TextBox>&nbsp;&nbsp; </td>
                  
                 <td rowspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="4" Text="全部" Selected="True"></asp:ListItem>
                    <asp:ListItem  Value="0"  Text="未报价" ></asp:ListItem>
                    <asp:ListItem  Value="1" Text="已报价"></asp:ListItem>
                    <asp:ListItem  Value="2" Text="已核价"></asp:ListItem>
                    <asp:ListItem  Value="3" Text="已完成"></asp:ListItem>
                 </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                
                    <td  rowspan="2" class="style2"><asp:Button  ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2" 
                            onclick="btnSelect_Click"/></td>
                             <td  rowspan="2" class="style2"><asp:Button  ID="btnMakeInquiry" runat="server" 
                                     Text="生成询价单" CssClass="SmallButton2" onclick="btnMakeInquiry_Click" 
                            /></td>
            </tr>
        
            <tr>
               <td>供应商名称：&nbsp;&nbsp;<asp:TextBox ID="txtVenderName" runat="server" CssClass="SmallTextBox "  Width="100px"></asp:TextBox>&nbsp;&nbsp;</td>
                 <td >&nbsp;&nbsp;&nbsp;QAD：&nbsp;&nbsp;<asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox " Width="100px"></asp:TextBox>&nbsp;&nbsp; </td>
                  <td >
                 <%--   <asp:CheckBox id="chkSelf" runat="server" Text="供应商未报价"/>--%>
                </td>
            </tr>
        
        </table>
                       <asp:GridView id="gvInquiryInfo" runat="server" AllowPaging="true" PageSize="25" 
                       AutoGenerateColumns="false" DataKeyNames="IMID"
                             CssClass="GridViewStyle" 
                       onpageindexchanging="gvInquiryInfo_PageIndexChanging" 
                       onrowdatabound="gvInquiryInfo_RowDataBound" 
                       onrowcommand="gvInquiryInfo_RowCommand" >
                         <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="664px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="询价单号" Width="67px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商号" Width="67px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商名" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="状态" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="操作" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="创建人" Width="100px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="创建时间" Width="100px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="3"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>

                        <Columns>
                         <asp:BoundField HeaderText="询价单号" DataField="IMID">
                                <HeaderStyle Width="67px" />
                                <ItemStyle  HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="供应商" DataField="vender">
                                <HeaderStyle Width="67px" />
                                  <ItemStyle  HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="供应商名" DataField="venderName">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="状态">
                                <ControlStyle Font-Bold="False" Font-Size="12px"  />
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                <asp:Label ID="lbStatus"  runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnOperation" runat="server" CommandName="lkbtnOperation" CommandArgument='<%# Eval("IMID") %>'
                                        Text=""></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="创建人" DataField="userName">
                                <HeaderStyle Width="100px" />
                                  <ItemStyle  HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="创建时间" DataField="createdDate">
                                <HeaderStyle Width="100px" />
                                  <ItemStyle  HorizontalAlign="Center" />
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
