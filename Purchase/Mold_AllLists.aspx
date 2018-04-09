<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mold_AllLists.aspx.cs" Inherits="Purchase_Mold_AllLists" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
            <table width="1200px">
                <tr>
                    <td>供应商<asp:TextBox runat="server" ID="txt_vend"  CssClass="Supplier" Width="90px" Maxlength="8"></asp:TextBox></td>
                    <td>模具类编号<asp:TextBox runat="server" ID="txt_MoldNbr" Width="90px" Maxlength="10"></asp:TextBox></td>
                    <td>模具编号<asp:TextBox runat="server" ID="txt_DetailNbr" Width="90px" Maxlength="15"></asp:TextBox></td>
                    <td>QAD<asp:TextBox runat="server" ID="txt_QAD" Width="90px" Maxlength="20"></asp:TextBox></td>
                    <td>状态
                <asp:DropDownList runat="server" ID="ddl_detailsStatus" Width="70px">
                    <asp:ListItem Value="1">正常</asp:ListItem>
                    <asp:ListItem Value="0">停用</asp:ListItem>
                    <asp:ListItem Value="-1">--</asp:ListItem>
                </asp:DropDownList>
                    </td>
                    <td>启用时间
                <asp:TextBox Width="80px" ID="txt_startDate" runat="server" CssClass="EnglishDate"></asp:TextBox>--<asp:TextBox Width="80px" runat="server" ID="txt_endDate" CssClass="EnglishDate"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button Text="查询" runat="server" ID="btn_search" Width="70" CssClass="SmallButton2" OnClick="btn_search_Click" />
                        <asp:Button Text="导出" runat="server" ID="Button1" Width="70" CssClass="SmallButton2" OnClick="Button1_Click"/>
                    </td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="gv" Width="1190px" PageSize="20" AutoGenerateColumns="false" AllowPaging="true" OnRowCommand="gv_RowCommand"
                DataKeyNames="detID,IntStatus" CssClass="GridViewStyle" OnPageIndexChanging="gv_PageIndexChanging" >
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="供应商" DataField="Mold_Vend">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="模具类编号" DataField="moldNbr">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="模具编号" DataField="detailNbr">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="供应商模具编号" DataField="Mold_venderMoldNbr">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="开模周期" DataField="Mold_TimeCost">
                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="状态">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"/>
                    <ItemStyle Width="50px" HorizontalAlign="Center" Font-Underline="true"/>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkStatus" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                        CommandName="Change" Font-Underline="True" Text='<%# Bind("Mold_Status") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                    <asp:BoundField HeaderText="产能" DataField="Mold_Capacity">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Right" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="生产寿命" DataField="Mold_WorkingLife">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Right" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="生产类型" DataField="Mold_Type">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="材料类型" DataField="Mold_MaterialType">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="材料品牌" DataField="Mold_MaterialBrand">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="启用时间" DataField="Mold_StartDate">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="数量" DataField="Qty">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="一模几穴" DataField="Mold_Cavity">
                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="穴编号" DataField="Mold_CavityDetails">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:BoundField>
                
                    <asp:BoundField HeaderText="备注" DataField="remark">
                        <HeaderStyle Width="220px" HorizontalAlign="Center" />
                        <ItemStyle Width="220px" HorizontalAlign="Left" ForeColor="Black" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
