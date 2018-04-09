<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mold_Son.aspx.cs" Inherits="Purchase_Mold_Son" %>

<!DOCTYPE html>

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
            <table id="tb1" >
                <tr>
                    <td width="80px" align="left">模具类编号</td>
                    <td width="150px" align="left">
                        <asp:TextBox runat="server" ID="txt_Nbr" Width="150px"></asp:TextBox></td>
                    <td width="50px" align="left">供应商</td>
                    <td width="150px" align="left">
                        <asp:TextBox runat="server" ID="txt_vend" Width="150px" CssClas="Supplier"></asp:TextBox></td>
                     <td width="30px" align="left">父子比例</td>
                    <td width="150px" align="left">
                        <asp:TextBox runat="server" ID="txt_weighting" Width="150px" ></asp:TextBox></td>
                    <td width="200px">
                        <asp:Button runat="server" ID="Button1" Text="添加" Width="70px" CssClass="SmallButton2" OnClick="btn_save_Click" />
                        <asp:Button runat="server" ID="Button2" Text="返回" Width="70px" Visible="false" CssClass="SmallButton2" />
                    </td>
                </tr>
                <tr>
                    
                    <td colspan="7" align="center">
                        <asp:GridView ID="gvInfo" runat="server"  AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                            PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDeleting="gv_RowDeleting"
                            DataKeyNames="id">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                               
                                <asp:BoundField HeaderText="子模具类编号" DataField="Mold_Nbr">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                 <asp:BoundField HeaderText="父子比例" DataField="Mold_Weighting">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="供应商" DataField="Mold_Vend">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="供应商名称" DataField="Mold_VendName">
                                    <HeaderStyle HorizontalAlign="Center" Width="220px" />
                                    <ItemStyle HorizontalAlign="Center" Width="220px" />
                                </asp:BoundField>
                                 <%--<asp:BoundField HeaderText="备注" DataField="code">
                                    <HeaderStyle HorizontalAlign="Center" Width="220px" />
                                    <ItemStyle HorizontalAlign="Center" Width="220px" />
                                </asp:BoundField>--%>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>

        </div>
    </form>
      <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
