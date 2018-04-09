<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PieceWorkPro.aspx.cs" Inherits="PieceWorkPro" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 28px;
            width: 867px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="960" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="left" class="style1">
                    标准单价:<asp:TextBox ID="txtSdprice" runat="server" CssClass="SmallTextBox" Width="99px"></asp:TextBox>系数:<asp:TextBox
                        ID="txtCoeff" runat="server" CssClass="SmallTextBox" Width="89px"></asp:TextBox>
                    支付单价:<asp:TextBox ID="txtPrice" runat="server" CssClass="SmallTextBox" Width="81px"></asp:TextBox>工种:&nbsp;<asp:DropDownList
                        ID="dropWorkKinds" runat="server" Width="162px" DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList>
                    类型：<asp:DropDownList ID="dropType" runat="server" DataTextField="systemCodeName"
                        DataValueField="systemCodeID" Width="152px">
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" OnClick="btnSearch_Click">
                    </asp:Button>&nbsp;
                </td>
                <td align="center" style="height: 28px">
                    <asp:Button ID="btnAddNew" runat="server" CssClass="SmallButton3" Text="增加" OnClick="btnAddNew_Click">
                    </asp:Button>
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="dvPieceWorkPro" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Width="956px" DataKeyNames="id" OnRowCommand="dvPieceWorkPro_RowCommand" PageSize="15"
            CssClass="GridViewStyle AutoPageSize" OnPageIndexChanging="dvPieceWorkPro_PageIndexChanging"
            OnRowDeleting="dvPieceWorkPro_RowDeleting" OnSorting="dvPieceWorkPro_Sorting">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" Visible="False" />
                <asp:BoundField DataField="sdprice" HeaderText="标准单价">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="coefficient" HeaderText="系数">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="price" HeaderText="支付单价">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="wkinds" HeaderText="工种">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="wtype" HeaderText="类型" />
                <asp:BoundField DataField="comment" HeaderText="备注" />
                <asp:ButtonField Text="<u>编辑</u>" CommandName="edit">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="50px" />
                </asp:ButtonField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
