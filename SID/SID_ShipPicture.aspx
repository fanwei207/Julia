<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_ShipPicture.aspx.cs" Inherits="SID_SID_ShipPicture" %>

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
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="1100px" border="0" class="">
            <tr>
                <td class="">
                </td>
                <td align="left">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" Width="60px" 
                        AutoPostBack="True" oncheckedchanged="chkAll_CheckedChanged"
                         />
                </td>
                <td align="right">
                    <asp:Label ID="lblSysPKNo" runat="server" Width="100px" CssClass="LabelRight" Text="系统货运单号(PK):"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSysPKNo" runat="server" Width="80px" TabIndex="1" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblSysPKRef" runat="server" Width="55px" CssClass="LabelRight" Text="图片日期:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                         <asp:TextBox ID="txtpicDate1" runat="server" CssClass="smalltextbox Date" TabIndex="3"
                        Width="70px"></asp:TextBox>-<asp:TextBox ID="txtpicDate2" runat="server" CssClass="smalltextbox Date"
                            TabIndex="3" Width="70px"></asp:TextBox>

                    <asp:TextBox ID="txtSysPKRef" runat="server" Width="80px" TabIndex="2" CssClass="smalltextbox" Visible="False"></asp:TextBox>
                </td>
                <td align="right">
                    出运日期:
                </td>
                <td align="right">
                    <asp:TextBox ID="txtShipDate1" runat="server" CssClass="smalltextbox Date" TabIndex="3"
                        Width="70px"></asp:TextBox>-<asp:TextBox ID="txtShipDate2" runat="server" CssClass="smalltextbox Date"
                            TabIndex="3" Width="70px"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" CssClass="LabelRight" Text="出运单号:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtShipNo" runat="server" Width="80px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right">
                   
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDomain" runat="server" Width="60px" TabIndex="3" 
                        CssClass="smalltextbox" Wrap="False" Visible="False"></asp:TextBox>
                </td>
                 <td align="right">
                    <asp:Label ID="lblstatus" runat="server" Width="40px" CssClass="LabelRight" Text="状态:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:DropDownList ID="ddlstatus" runat="server">
                        <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">未上传照片</asp:ListItem>
                        <asp:ListItem Value="2">已上传照片</asp:ListItem>
                    </asp:DropDownList>
                   
                </td>
                <td align="Left">
                    &nbsp;<asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="4"
                        Text="查询" Width="50px" OnClick="btnQuery_Click" Height="22px" />
                </td>
                <td align="Left">
                    &nbsp;<asp:Button 
                        ID="btnAddNew" runat="server" CssClass="SmallButton2" TabIndex="5"
                        Text="上传" Width="50px" onclick="btnAddNew_Click" />
                </td>
                <td class="">
                </td>
            </tr>


           <tr>
                <td class="">
                </td>
                <td align="left">
                  
                </td>
                <td align="right">
                    <asp:Label ID="lblsite" runat="server" Width="100px" CssClass="LabelRight" Text="装箱地点:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtsite" runat="server" Width="80px" TabIndex="1" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lbloutdate" runat="server" Width="55px" CssClass="LabelRight" Text="出厂时间:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                         <asp:TextBox ID="txtoutdate" runat="server" CssClass="smalltextbox" TabIndex="3"
                        Width="70px"></asp:TextBox>(*)

                    <asp:TextBox ID="TextBox4" runat="server" Width="80px" TabIndex="2" CssClass="smalltextbox" Visible="False"></asp:TextBox>
                </td>
                <td align="right">
                   
                </td>
                <td align="right">
                    
                </td>
                <td align="right">
                    
                </td>
                <td align="center">
                  
                </td>
                <td align="right">
                   
                </td>
                <td align="center">
                    
                </td>
                 <td align="right">
                 
                </td>
                <td align="center">
                 
                   
                </td>
                <td align="center">
                </td>
                <td align="center">
                  
                </td>
                <td class="">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSID" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="20" 
            DataKeyNames="SID,SID_Status" Width="980px" 
            onpageindexchanging="gvSID_PageIndexChanging" 
            onprerender="gvSID_PreRender" onrowdatabound="gvSID_RowDataBound" onrowcommand="gvSID_RowCommand"
           >
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="√" Width="20px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="系统货运单号" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="参考" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运单号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="装箱地点" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运日期" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出厂日期" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="运输方式" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="运往" Width="400px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="域" Width="30px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Select" runat="server" Width="20px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PK" HeaderText="系统货运单号">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PKRef" HeaderText="参考">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Nbr" HeaderText="出运单号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Site" HeaderText="装箱地点">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="出运日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OutDate" HeaderText="出厂日期">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Via" HeaderText="运输方式">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Shipto" HeaderText="运往">
                    <HeaderStyle Width="400px" HorizontalAlign="Center" />
                    <ItemStyle Width="400px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="域">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="图片">
                        <ItemTemplate>
                          <asp:LinkButton ID="lnkstatus" runat="server" Font-Bold="False" Font-Size="12px"
                                CommandName="show" Font-Underline="True" Text=""></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Top" />
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
