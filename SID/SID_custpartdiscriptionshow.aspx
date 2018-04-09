<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_custpartdiscriptionshow.aspx.cs" Inherits="SID_SID_custpartdiscriptionshow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
 <div align="center">
    <form id="form1" runat="server" >
   
     <table style="width: 700px">
            <tr>
                <td align="center">
                    客户物料号：<asp:TextBox 
                        ID="txtpart" runat="server" Width="100px" CssClass="Param" ></asp:TextBox>
                    &nbsp; 
                    客户：<asp:TextBox ID="txtcust" runat="server" Width="120px" 
                        CssClass="Param" MaxLength="8"></asp:TextBox>
                    &nbsp; 
                    HST：<asp:TextBox ID="txtHST" runat="server" Width="100px" 
                        CssClass="Param" ></asp:TextBox>
                   
                    
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" onclick="btnSearch_Click" 
                        />

                         <asp:Button ID="btnExcel" runat="server" Text="Excel" 
                        CssClass="SmallButton3"  onclick="btnExcel_Click"
                          />
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild AutoPageSize"
                        PageSize="20" DataKeyNames="SID_id"
                        Width="800px" AllowPaging="True" 
         onpageindexchanging="gv_PageIndexChanging" onrowcommand="gv_RowCommand" 
         onrowdeleting="gv_RowDeleting">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="600px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="客户物料号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="客户号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="HST" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="描述" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                 
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="SID_partID" HeaderText="客户物料号" HtmlEncode="False" ReadOnly="True">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SID_cust" HeaderText="客户号" HtmlEncode="False" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SID_HST" HeaderText="HST" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                                <asp:BoundField DataField="SID_description" HeaderText="描述" ReadOnly="True">
                                <HeaderStyle Width="160px" HorizontalAlign="Center" />
                                <ItemStyle Width="160px" HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:CommandField ShowDeleteButton="True"  DeleteText="<u>删除</u>">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" ForeColor="Black" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:CommandField>

                             <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ltnEdit" Text="<u>修改</u>" ForeColor="Blue" Font-Size="12px" runat="server"
                                                CommandName="myEdit" />
                                    </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
    
     <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
    </form>
    </div>
</body>
</html>
