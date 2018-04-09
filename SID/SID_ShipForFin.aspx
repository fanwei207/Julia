<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_ShipForFin.aspx.cs" Inherits="SID_ShipForFin" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1180px">
            <tr>
                <td Width="160px">
                    系统单号<asp:TextBox ID="txtPK" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td Width="160px">
                    出运单号<asp:TextBox ID="txtnbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td Width="160px">
                    装箱地点<asp:TextBox ID="txtsite" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td Width="160px">
                    出运日期<asp:TextBox ID="txtShipDate" runat="server" CssClass="SmallTextBox Date" Width="100px"
                        onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                <td Width="160px">
                    至<asp:TextBox
                        ID="txtShipDate1"  runat="server" CssClass="SmallTextBox Date" Width="100px"
                        onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                <td>
                    类型:
                    <asp:DropDownList ID="ddl_type" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddl_type_SelectedIndexChanged" >
                        <asp:ListItem Value ="0">内销-未确认</asp:ListItem>
                        <asp:ListItem Value ="1">内销-已确认</asp:ListItem>
                        <asp:ListItem Value ="2">非内销</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="height: 20px">
                    创建日期<asp:TextBox ID="txtcreated" runat="server" CssClass="SmallTextBox Date" Width="100px"
                        onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                <td style="height: 20px">
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;至<asp:TextBox
                        ID="txtcreated1" runat="server" CssClass="SmallTextBox Date" Width="100px"
                        onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>  
                </td>
                <td style="height: 20px">
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" Width="40"
                        OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_exportexcelByTemp" runat="server" CssClass="SmallButton3" 
                        Text="导出出运单" Width="70px"
                        OnClick="btn_exportexcelByTemp_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvShip" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
            DataKeyNames="ID" OnRowCommand="gvShip_RowCommand" OnPageIndexChanging="gvShip_PageIndexChanging"
            OnRowDataBound="gvShip_RowDataBound" Width="1310px" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="PK" HeaderText="系统单号">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="PKref" HeaderText="参考">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="nbr" HeaderText="出运单号">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="via" HeaderText="运输方式">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ctype" HeaderText="集装箱型">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="site" HeaderText="装箱地点">
                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="shipdate" HeaderText="出运日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="outdate" HeaderText="出厂日期">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_date" HeaderText="验货日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_site" HeaderText="验货地点">
                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_matchdate" HeaderText="预配日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="mj" HeaderText="免检">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                 <asp:BoundField DataField="IsCabin" HeaderText="配舱">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:ButtonField CommandName="Detail1" Text="<u>详细</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:TemplateField HeaderText="确认">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org2_con") %>' Text='<%# Eval("SID_org2_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="domain" HeaderText="域">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="状态">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="finisheddate" HeaderText="完工日期">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="checkeddate" HeaderText="抵达日期">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="shipto" HeaderText="运往">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
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
