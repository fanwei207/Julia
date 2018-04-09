<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_venderListView.aspx.cs" Inherits="price_pcm_venderListView" %>

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
            <div>请选择供应商：</div>
            <div>
                <asp:GridView ID="gvVenderList" runat="server" Width="400px" AllowSorting="True"
                    AutoGenerateColumns="False" AllowPaging="true" PageSize="8"
                    CssClass="GridViewStyle GridViewRebuild" DataKeyNames="pc_list,pc_price,pc_um,pc_curr,ad_name"
                    EmptyDataText="No data">
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkVender" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="供应商" DataField="pc_list">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="供应商名称" DataField="ad_name">
                            <HeaderStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="单位" DataField="pc_um">
                            <HeaderStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="价格" DataField="pc_price">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="币种" DataField="pc_curr">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="修改价">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="changePrice" runat="server" CssClass="SmallTextBox Numeric"  Width="80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>


                </asp:GridView>
            </div>
            <div>
                <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="保存" OnClick="btnSave_Click" />
                &nbsp; &nbsp; &nbsp; &nbsp; 
            <asp:Button ID="btnReturn" runat="server" CssClass="SmallButton2" Text="返回" OnClick="btnReturn_Click" />


            </div>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
