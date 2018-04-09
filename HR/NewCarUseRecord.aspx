<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewCarUseRecord.aspx.cs" Inherits="HR_NewCarUseRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin-top:10px;">
            <tr>
                <td style="text-align:right;">日期</td>
                <td>
                    <asp:TextBox ID="txtUsrDate" runat="server" CssClass="Date"></asp:TextBox>
                </td>
                <td style="text-align:right;">出发时间</td>
                <td>
                    <asp:TextBox ID="txtDepartureTime" runat="server"></asp:TextBox>
                </td>
                <td style="text-align:right;">出发地</td>
                <td>
                    <asp:TextBox ID="txtPlaceOfDeparture" runat="server"></asp:TextBox>
                </td>
                <td style="text-align:right;">装载情况</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlTruckLoading">
                        <asp:ListItem Text="全部" Value="4">全部</asp:ListItem>
                        <asp:ListItem Text="空车" Value="0">空车</asp:ListItem>
                        <asp:ListItem Text="满载" Value="1">满载</asp:ListItem>
                        <asp:ListItem Text="没有满载" Value="2">没有满载</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align:right;">公司</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlPlant">
                        <asp:ListItem Text="全部" Value="0">全部</asp:ListItem>
                        <asp:ListItem Text="上海强凌" Value="1">上海强凌</asp:ListItem>
                        <asp:ListItem Text="镇江强凌" Value="2">镇江强凌</asp:ListItem>
                        <asp:ListItem Text="扬州强凌" Value="5">扬州强凌</asp:ListItem>
                        <asp:ListItem Text="淮安强凌" Value="8">淮安强凌</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnSearch_Click" />
                </td>
            </tr>


            <tr>
                <td style="text-align:right;">车号</td>
                <td>
                    <asp:TextBox ID="txtCarNumner" runat="server"></asp:TextBox>
                </td>
                <td style="text-align:right;">到达时间</td>
                <td>
                    <asp:TextBox ID="txtArrivalTime" runat="server"></asp:TextBox>
                </td>
                <td style="text-align:right;">到达地</td>
                <td>
                    <asp:TextBox ID="txtdestination" runat="server"></asp:TextBox>
                </td>
                <td style="text-align:right;">用途</td>
                <td colspan="3">
                    <asp:TextBox ID="txtUses" runat="server" Width="360px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnNew" runat="server" Visible="false" Text="新建" CssClass="SmallButton2" OnClick="btnNew_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;"><span style="display:none;">附件</span></td>
                <td colspan="5">
                    <input id="UpLoadFile" runat="server" visible="false" style="width: 550px;" name="resumename" type="file" />
                </td>
                <td colspan="4" style="text-align:center;">
                    <label id="here" onclick="submit();">
                        <a href="/docs/TCP-CHINACarUsesRecord.xlsx" target="blank"><font color="blue">用车记录模版下载</font></a></label>
                </td>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="导入" Visible="false" CssClass="SmallButton2" OnClick="btnUpload_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCarUseReacord" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" 
        DataKeyNames="id,TruckLoading" OnRowDeleting="gvCarUseReacord_RowDeleting"
            OnRowDataBound="gvCarUseReacord_RowDataBound" OnRowCommand="gvCarUseReacord_RowCommand" AllowPaging="True" OnPageIndexChanging="gvCarUseReacord_PageIndexChanging" PageSize="13">
        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
        <RowStyle CssClass="GridViewRowStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />
        <EmptyDataTemplate>
            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                GridLines="Vertical">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="center" Text="公司" Width="50px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="日期" Width="50px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="车号" Width="70px"></asp:TableCell>
			        <asp:TableCell HorizontalAlign="center" Text="出发时间" Width="150px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="出发地" Width="80px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="到达时间" Width="150px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="到达地" Width="50px"></asp:TableCell>                        
                    <asp:TableCell HorizontalAlign="center" Text="用途" Width="300px"></asp:TableCell>                       
                    <asp:TableCell HorizontalAlign="center" Text="货车装载情况" Width="100px"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Plant" HeaderText="公司">
                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="40px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="UsesDate" HeaderText="日期">
                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CarNumber" HeaderText="车号">
                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="DepartureTime" HeaderText="出发时间">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="PlaceOfDeparture" HeaderText="出发地">
                <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ArrivalTime" HeaderText="到达时间">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Destination" HeaderText="到达地">
                <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Kilometers" HeaderText="公里数(Km)">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Uses" HeaderText="用途">
                <HeaderStyle Width="250px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="250px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TruckLoading" HeaderText="货车装载情况">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="createName" HeaderText="使用人">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Delete">
                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>删除</u>" ForeColor="Black"
                        CommandName="Delete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>        
        </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
