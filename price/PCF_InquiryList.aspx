<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCF_InquiryList.aspx.cs" Inherits="price_PCF_InquiryList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
         <base target="_self">
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
               <td>&nbsp;&nbsp;&nbsp;&nbsp;询价单号：&nbsp;&nbsp;<asp:TextBox ID="txtIMID" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>&nbsp;&nbsp;</td> 
                <td >供应商：&nbsp;&nbsp;<asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>&nbsp;&nbsp;</td>
                  <td>供应商名称：&nbsp;&nbsp;<asp:TextBox ID="txtVenderName" runat="server" CssClass="SmallTextBox "  Width="100px"></asp:TextBox>&nbsp;&nbsp;</td>
                 <td >&nbsp;&nbsp;&nbsp;QAD：&nbsp;&nbsp;<asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox " Width="100px"></asp:TextBox>&nbsp;&nbsp; </td>
                  
                 <td rowspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="0" Text="全部" Selected="True"></asp:ListItem>
                    <asp:ListItem  Value="-10"  Text="被驳回" ></asp:ListItem>
                    <asp:ListItem  Value="10"  Text="未核价" ></asp:ListItem>
                    <asp:ListItem  Value="20" Text="已核价"></asp:ListItem>
                    <asp:ListItem  Value="30" Text="已提交"></asp:ListItem>
                     <asp:ListItem  Value="40" Text="已完成"></asp:ListItem>
                 </asp:DropDownList>&nbsp;&nbsp;&nbsp;</td>
                
                    <td  class="style2"><asp:Button  ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2" 
                            onclick="btnSelect_Click" Width="80px"/></td>
                             <td  class="style2"><asp:Button  ID="btnMakeInquiry" runat="server" 
                                     Text="生成询价单" Width="80px" CssClass="SmallButton2" onclick="btnMakeInquiry_Click" 
                            /></td>

              
                
            </tr>
        
        </table>
                       <asp:GridView id="gvInquiryInfo" runat="server" AllowPaging="true" PageSize="25" 
                       AutoGenerateColumns="false" DataKeyNames="PCF_inquiryID,PCF_IMID,PCF_vender,PCF_venderName,createdName,createdDate"
                             CssClass="GridViewStyle" 
                       onpageindexchanging="gvInquiryInfo_PageIndexChanging" 
                       
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
                         <asp:BoundField HeaderText="询价单号" DataField="PCF_IMID">
                                <HeaderStyle Width="67px" />
                                <ItemStyle  HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="供应商" DataField="PCF_vender">
                                <HeaderStyle Width="67px" />
                                  <ItemStyle  HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="供应商名" DataField="PCF_venderName">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="状态">
                                <ControlStyle Font-Bold="False" Font-Size="12px"  />
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                <asp:Label ID="lbStatus"  runat="server" Text='<%# Eval("PCF_states") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnOperation" runat="server" CommandName="lkbtnOperation" CommandArgument='<%# Eval("PCF_inquiryID") %>'
                                        Text="操作"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="创建人" DataField="createdName">
                                <HeaderStyle Width="100px" />
                                  <ItemStyle  HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="创建时间" DataField="createdDate" DataFormatString="{0:yyyy-MM-dd}">
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
