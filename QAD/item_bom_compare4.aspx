<%@ Page Language="C#" AutoEventWireup="true" CodeFile="item_bom_compare4.aspx.cs"
    Inherits="item_bom_compare4" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $("#gvCompare_ctl01_chkSelect").click(function () {

                if ($(this).prop('checked')) {
                    $(".bom_campare4_normal").hide();
                }
                else {
                    $(".bom_campare4_normal").show();
                }
            })
            //end click


        })
    
    
    
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" width="800px" bgcolor="white" border="0">
            <tr>
                <td width="800px">
                    <asp:Label ID="lblProject" runat="server" Width="60px" CssClass="LabelRight" Text="Qad号"
                        Font-Bold="False"></asp:Label><asp:TextBox ID="txtQad" runat="server" Width="110px"
                            TabIndex="1" MaxLength="18"></asp:TextBox><font style="color: Red; size: 12px;">*必填</font>
                    &nbsp;
                    <asp:Label ID="Label2" runat="server" CssClass="LabelRight" Font-Bold="False" Text="部件号"
                        Width="50px"></asp:Label><asp:TextBox ID="txtCode" runat="server" MaxLength="50"
                            TabIndex="1" Width="120px"></asp:TextBox><span style="color: #ff0000">*必填</span>
                    &nbsp;
                    <asp:Label ID="Label1" runat="server" CssClass="LabelRight" Font-Bold="False" Text="域"
                        Width="30px"></asp:Label>
                    <asp:TextBox ID="txtDomain" runat="server" TabIndex="1" Width="40px" MaxLength="5"
                        CssClass="Company">SZX</asp:TextBox><span style="color: #ff0000">*必填</span>
                    &nbsp;
                    <asp:Label ID="lblLevel" runat="server" CssClass="LabelRight" Font-Bold="False" Text="层级"
                        Width="31px"></asp:Label><asp:TextBox ID="txtLevel" runat="server" TabIndex="1" Width="40px"
                            Style="ime-mode: disabled" onkeypress="if (event.keyCode<49 || event.keyCode>57) event.returnValue=false;"
                            MaxLength="1" onpaste="return false">5</asp:TextBox>
                    &nbsp; <span style="color: #ff0000">*选填(1-9) &nbsp;
                        <asp:Button ID="btnCompare" runat="server" CssClass="SmallButton2" OnClick="btnCompare_Click"
                            Text="比较" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnExport" runat="server" Text="导出" class="SmallButton2" OnClick="btnExport_Click"
                            Width="48px" Enabled="false" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCompare" runat="server" AllowPaging="False" AutoGenerateColumns="False"
            Width="800px" OnRowDataBound="gvCompare_RowDataBound" OnRowCreated="gvCompare_RowCreated">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" BackColor="#000099" ForeColor="White" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" 
                BackColor="#F3F3F3" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="bom_par" HeaderText="父级">
                    <HeaderStyle Width="15%" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="15%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="item_bom_comp" HeaderText="子Qad号">
                    <HeaderStyle Width="20%" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="item_bom_qty" HeaderText="子Qad数量">
                    <HeaderStyle Width="10%" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="10%" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="item_bom_lel" HeaderText="子Qad层级">
                    <HeaderStyle Width="10%" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="item_bom_crud" HeaderText="修改类型">
                    <HeaderStyle Width="10%" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="product_bom_comp" HeaderText="子Qad号">
                    <HeaderStyle Width="15%" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="15%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="product_bom_qty" HeaderText="子Qad数量">
                    <HeaderStyle Width="10%" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="10%" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="product_bom_lel" HeaderText="子Qad层级">
                    <HeaderStyle Width="10%" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
