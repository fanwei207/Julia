<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mold_RelateQad.aspx.cs" Inherits="Purchase_Mold_RelateQad" %>

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
            <div>
         <div align="center">
            <table id="tb1" width="450px">
                <tr>
                    <td width="50px" align="left">QAD</td>
                    <td width="250px" align="left">
                        <asp:TextBox runat="server" ID="txt_Qad" Width="200px" CssClass="Part"></asp:TextBox></td>
                    <td width="150px">
                        <asp:Button runat="server" ID="btn_save" Text="添加" Width="70px" CssClass="SmallButton2" OnClick="btn_save_Click" />
                        <asp:Button runat="server" ID="btn_back" Text="返回" Width="70px" Visible="false" CssClass="SmallButton2"  />
                    </td>
                </tr>
                <tr>
                    <td width="50px" align="left"></td>
                    <td colspan="2" align="left">
                        <asp:GridView ID="gv" runat="server" Width="450px" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                            PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDeleting="gv_RowDeleting"
                            DataKeyNames="id">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="序号" DataField="rowNbr">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="QAD" DataField="Mold_qad">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="部件号" DataField="code">
                                    <HeaderStyle HorizontalAlign="Center" Width="220px" />
                                    <ItemStyle HorizontalAlign="Center" Width="220px" />
                                </asp:BoundField>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>

        </div>
    </div>
    </form>
</body>
</html>
