<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NPart_QADDocHistByDocID.aspx.cs" Inherits="part_NPart_QADDocHistByDocID" %>

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
            <table cellspacing="0" cellpadding="0" width="600px" align="center" style="margin-top: 2px;"
                border="0">
                <tr class="main_top">
                    <td class="main_left"></td>
                    <td style="height: 6px" width="420">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </td>
                    <td style="height: 6px" align="right" width="60">
                        <asp:Button ID="btnReturn" runat="server" Text="返回" OnClick="btnReturn_Click" />
                    </td>
                    <td class="main_right"></td>
                </tr>
            </table>
            <asp:GridView ID="gvDet" runat="server" Width="800px"
                AutoGenerateColumns="False" 
                CssClass="GridViewStyle " DataKeyNames="ID,filepath,filename,Path" OnRowCommand="gvDet_RowCommand" OnRowDataBound="gvDet_RowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="Version" DataField="version" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="FileName" DataField="filename" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <HeaderTemplate>preview</HeaderTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30px" Font-Underline="true"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbview" runat="server" Text="view" CommandName="view"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="创建人" DataField="createdname" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="创建日期" DataField="createdDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="升级人" DataField="upgradeName" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="升级日期" DataField="upgradeDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
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
