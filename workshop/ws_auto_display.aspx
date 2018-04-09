<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ws_auto_display" CodeFile="ws_auto_display.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript"> 
		<!--
        var limit = "0:30" /* min:sec */
        if (document.images) {
            var parselimit = limit.split(":")
            parselimit = parselimit[0] * 60 + parselimit[1] * 1
        }

        function beginrefresh() {
            if (!document.images)
                return
            if (parselimit == 1)
                window.location.href = "/workshop/ws_auto_daily.aspx?dd=" + document.getElementById("txb_date").value + "&site=" + document.getElementById("ddl_site").value + "&cc=" + document.getElementById("ddl_cc").value + "&line=" + document.getElementById("ddl_line").value + "&part=" + document.getElementById("txb_part").value;
            else {
                parselimit -= 1
                /*
                curmin=Math.floor(parselimit/60) 
                cursec=parselimit%60 
                if (curmin!=0) 
                curtime=curmin+"分"+cursec+"秒后重刷本页！" 
                else 
                curtime=cursec+"秒后重刷本页！" 
                window.status=curtime */
                setTimeout("beginrefresh()", 1000)
            }
        }

        window.onload = beginrefresh 
		//--> 
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:Panel ID="Panel2" runat="server" Width="980px" HorizontalAlign="Left" BorderWidth="1px"
            BorderColor="Black" Height="40px">
            <table id="table1" cellspacing="0" cellpadding="0" width="980">
                <tr>
                    <td>
                        &nbsp;<asp:DropDownList ID="ddl_site" runat="server" Width="100px" Enabled="false">
                            <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                            <asp:ListItem Selected="false" Value="2">镇江强凌 ZQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="5">扬州强凌 YQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="1">上海振欣 SZX</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; 成本中心
                        <asp:DropDownList ID="ddl_cc" runat="server" Width="90px" AutoPostBack="True">
                        </asp:DropDownList>
                        工段线
                        <asp:DropDownList ID="ddl_line" runat="server" Width="150px">
                        </asp:DropDownList>
                        零件号<asp:TextBox ID="txb_part" runat="server" Width="150" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                        日期<asp:TextBox ID="txb_date" runat="server" Width="75" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btn_list" runat="server" Width="40" CssClass="SmallButton3" Text="刷新"
                            TabIndex="4"></asp:Button>&nbsp;
                        <asp:Button ID="btn_export" runat="server" Width="40" CssClass="SmallButton3" Text="导出"
                            TabIndex="24" Visible="false"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        &nbsp;<asp:Label ID="lbl_qty" runat="server"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="960px" AutoGenerateColumns="False"
            GridLines="Vertical" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="group_pid" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="group_lid" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="group_site" HeaderText="公司">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_cc" HeaderText="成本中心">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_line" HeaderText="工段线">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_total" HeaderText="流量">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_bad" HeaderText="次品">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_pass" HeaderText="一次合格率" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;明细&lt;/u&gt;" CommandName="proc_detail">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;原因&lt;/u&gt;" CommandName="proc_fail">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;走势图&lt;/u&gt;" CommandName="proc_statistics">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
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
