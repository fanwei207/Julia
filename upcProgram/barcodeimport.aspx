<%@ Page Language="C#" AutoEventWireup="true" CodeFile="barcodeimport.aspx.cs" Inherits="upcProgram_barcodeimport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table border="0" cellpadding="2" cellspacing="0" width="882">
            <tr>
                <td width="10%" style="height: 18px">
                    <span style="color: #cc0000; font-family: Verdana">导入文件的格式如下：</span>
                </td>
                <td style="width: 14%; height: 18px">
                    <span style="color: #0000ff"></span>
                </td>
                <td style="width: 11%; height: 18px">
                    <span style="color: #0000ff"></span>
                </td>
                <td style="width: 12%; height: 18px;">
                </td>
                <td width="9%" style="height: 18px">
                </td>
            </tr>
            <tr>
                <td width="10%" style="text-align: center;">
                    <font face="Verdana, Arial, Helvetica, sans-serif">Item Number</font>
                </td>
                <td style="text-align: center; width: 14%;">
                    <font face="Verdana, Arial, Helvetica, sans-serif">Item Description</font>
                </td>
                <td style="text-align: center; width: 11%;">
                    <font face="Verdana, Arial, Helvetica, sans-serif">UPC</font>
                </td>
                <td style="width: 12%; text-align: center;">
                    <font face="Verdana, Arial, Helvetica, sans-serif">InnerPackI2of5</font>
                </td>
                <td width="9%" style="text-align: center;">
                    <font face="Verdana, Arial, Helvetica, sans-serif">MasterPackI2of5</font>
                </td>
            </tr>
            <tr bgcolor="#eceef7">
                <td style="height: 18px; text-align: center;" width="10%">
                    00STKRUI2
                </td>
                <td style="height: 18px; text-align: center; width: 14%;">
                    UNITED ILLUMINATING STICKR DWO
                </td>
                <td style="height: 18px; text-align: center; width: 11%;">
                    762148011286
                </td>
                <td style="width: 12%; height: 18px; text-align: center;">
                    <font color="#cc0000"><span style="color: #000000">10762148011283</span></font>
                </td>
                <td style="height: 18px; text-align: center;" width="9%">
                    <font color="#cc0000"><span style="color: #000000">40762148011280</span></font>
                </td>
            </tr>
            <tr>
                <td width="10%">
                </td>
                <td style="width: 14%">
                </td>
                <td style="width: 11%">
                </td>
                <td style="width: 12%">
                </td>
                <td width="9%">
                </td>
            </tr>
            <tr>
                <td style="height: 17px" width="10%">
                    文件类型:
                </td>
                <td style="height: 17px; width: 14%;">
                    <asp:DropDownList ID="filetypeDDL" runat="server" AutoPostBack="True" Width="218px">
                        <asp:ListItem>Excel(.xls) File</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 17px; width: 11%;">
                    <span style="color: #0000ff">
                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="12px"
                            NavigateUrl="~/docs/barcodeipmort.xls">下载模板</asp:HyperLink></span>
                </td>
                <td style="width: 12%; height: 17px">
                </td>
                <td style="height: 17px" width="9%">
                </td>
            </tr>
            <tr>
                <td width="10%">
                    导入文件:
                </td>
                <td colspan="4">
                    <input id="filename" runat="server" style="width: 461px; height: 22px" class="smallbutton2"
                        name="filename" type="file">
                    &nbsp;&nbsp;
                    <asp:Button ID="btnImport" runat="server" CssClass="SmallButton2" OnClick="btnImport_Click"
                        Text="导入" Width="90px" />
                </td>
            </tr>
            <tr>
                <td width="10%">
                </td>
                <td style="width: 14%">
                </td>
                <td style="width: 11%">
                </td>
                <td style="width: 12%">
                </td>
                <td width="9%">
                </td>
            </tr>
            <tr>
                <td width="10%">
                    数据预览:
                </td>
                <td style="width: 14%">
                </td>
                <td style="width: 11%">
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton2" Text="确认" Width="90px"
                        OnClick="Button1_Click" />
                </td>
                <td style="width: 12%">
                </td>
                <td width="9%">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgCodeTemp" runat="server" Width="882px" AllowPaging="true" PageSize="23"
            AutoGenerateColumns="False" OnCancelCommand="dgCodeTemp_CancelCommand" OnDeleteCommand="dgCodeTemp_DeleteCommand"
            OnEditCommand="dgCodeTemp_EditCommand" OnUpdateCommand="dgCodeTemp_UpdateCommand"
            OnPageIndexChanged="dgCodeTemp_PageIndexChanged" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="bc_item" HeaderText="Item Number">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_desc" HeaderText="Item Description">
                    <HeaderStyle Width="300px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="300px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_upc" HeaderText="UPC" ReadOnly="True">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_ipi" HeaderText="InnerPackI2of5" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_mpi" HeaderText="MasterPackI2of5" ReadOnly="True">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn CancelText="<u>取消</u>" EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn CommandName="Delete" Text="<u>删除</u>">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
