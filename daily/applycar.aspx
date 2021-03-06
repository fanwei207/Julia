<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.applycar" CodeFile="applycar.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:Table ID="Table2" runat="server" Width="680px" BorderStyle="Solid" CellSpacing="0"
            BorderColor="Black" CellPadding="0" GridLines="Both" Visible="False">
            <asp:TableRow BorderWidth="1px" BorderStyle="Solid">
                <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                    Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;TO：" ID="cell1"></asp:TableCell>
                <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                    Wrap="False">
                    <asp:TextBox runat="server" BorderStyle="None" Height="19px" TabIndex="1" Width="280px"
                        Wrap="False" ID="objtto" MaxLength="50"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                    Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;FROM：" ID="customerID1"></asp:TableCell>
                <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                    Wrap="False">
                    <asp:TextBox runat="server" BorderStyle="None" Height="19px" TabIndex="2" Width="280px"
                        Wrap="False" ID="objtfrom" MaxLength="50"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BorderWidth="1px" BorderStyle="Solid">
                <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                    Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;TEL：" ID="objttotelaa"></asp:TableCell>
                <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                    Wrap="False">
                    <asp:TextBox runat="server" BorderStyle="None" Height="19px" TabIndex="3" Width="280px"
                        Wrap="False" ID="objttotel" MaxLength="15" CssClass="smallTextbox Numeric"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                    Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;TEL：" ID="adeaf"></asp:TableCell>
                <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                    Wrap="False">
                    <asp:TextBox runat="server" BorderStyle="None" Height="19px" TabIndex="4" Width="280px"
                        Wrap="False" ID="objtfromtel" MaxLength="15" CssClass="smallTextbox Numeric"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BorderWidth="1px" BorderStyle="Solid">
                <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                    Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;FAX：" ID="line31"></asp:TableCell>
                <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                    Wrap="False">
                    <asp:TextBox runat="server" BorderStyle="None" Height="19px" TabIndex="5" Width="280px"
                        Wrap="False" ID="objttofax" MaxLength="15"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                    Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;FAX：" ID="line32"></asp:TableCell>
                <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                    Wrap="False">
                    <asp:TextBox runat="server" BorderStyle="None" Height="19px" TabIndex="6" Width="280px"
                        Wrap="False" ID="objtfromfax" MaxLength="15"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BorderWidth="1px" BorderStyle="Solid">
                <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                    Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;CC：" ID="line41"></asp:TableCell>
                <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                    Wrap="False">
                    <asp:TextBox runat="server" BorderStyle="None" Height="19px" TabIndex="7" Width="280px"
                        Wrap="False" ID="objtCC" MaxLength="30"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                    Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;DATE：" ID="line42"></asp:TableCell>
                <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                    Wrap="False">
                    <asp:TextBox runat="server" BorderStyle="None" Height="19px" TabIndex="8" Width="200px"
                        Wrap="False" ID="objtDate" MaxLength="10" AutoPostBack="True" CssClass="smallTextbox Date" ></asp:TextBox>
                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BorderWidth="1px" BorderStyle="Solid">
                <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                    Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;SB：" ID="line51"></asp:TableCell>
                <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                    Wrap="False">
                    <asp:TextBox runat="server" BorderStyle="None" Height="19px" TabIndex="9" Width="280px"
                        Wrap="False" ID="objtSB" MaxLength="30"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                    Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;OTHER：" ID="line52"></asp:TableCell>
                <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                    Wrap="False">
                    <asp:TextBox runat="server" BorderStyle="None" Height="19px" TabIndex="10" Width="280px"
                        Wrap="False" ID="objtOTHER" MaxLength="100"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <table cellspacing="0" cellpadding="0" width="550" align="center" bgcolor="white"
            border="0">
            <tr>
                <td style="height: 24px" align="right" width="30%">
                    <asp:Button ID="Button1" TabIndex="15" runat="server" Width="80px" Visible="False"
                        CssClass="SmallButton1" Text="添加" CausesValidation="False"></asp:Button><asp:Button
                            ID="BtOk" TabIndex="15" runat="server" Width="80px" Visible="False" CssClass="SmallButton1"
                            Text="保存" CausesValidation="False" ForeColor="Red"></asp:Button><asp:Button ID="btprint"
                                TabIndex="15" runat="server" Width="100px" Visible="False" CssClass="SmallButton1"
                                Text="打印用车申请" CausesValidation="False"></asp:Button><asp:Button ID="btCancel" TabIndex="15"
                                    runat="server" Width="80px" Visible="False" CssClass="SmallButton1" Text="返回"
                                    CausesValidation="False"></asp:Button>
                </td>
            </tr>
        </table>
        <table id="table3" cellspacing="0" cellpadding="0" width="680" align="center" bgcolor="white"
            border="0">
            <tr width="100%">
                <td>
                    <asp:Panel ID="panel1" runat="server">
                        <asp:DataGrid ID="dgOrderDetail" runat="server" BorderColor="#999999" BorderStyle="None"
                            Width="780px" PageSize="25" CssClass="GridViewStyle AutoPageSize">
                            <ItemStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                <asp:BoundColumn DataField="gCarid" ReadOnly="True" HeaderText="序号">
                                    <HeaderStyle Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="gtime" HeaderText="用车时间">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="gstartplace" HeaderText="起始地点">
                                    <HeaderStyle Width="140px"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="gendplace" HeaderText="到达地点">
                                    <HeaderStyle Width="140px"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="gqty" HeaderText="载货数">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="gperson" HeaderText="载人数">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="comment" HeaderText="备注">
                                    <HeaderStyle Width="200px"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="id" Visible="False" HeaderText="" ReadOnly="True"></asp:BoundColumn>
                                <asp:ButtonColumn Text="删除" HeaderText="删除" CommandName="DeleteBtn">
                                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                                     <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:Label ID="Labelmemory" runat="server" Width="121px" BorderColor="Yellow" Visible="False"
            BackColor="Yellow">0</asp:Label><asp:Label ID="carid" Visible="False" runat="server"></asp:Label>
        <table height="10" width="780">
            <tr align="left" height="100%" width="100%">
                <td align="right">
                    <asp:Button ID="btAppend" TabIndex="15" runat="server" CssClass="SmallButton1" Text="增加用车申请"
                        CausesValidation="False"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="780px" AutoGenerateColumns="False"
            PageSize="50" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="number" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gUsedDate" HeaderText="用车日期">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gUsedtime" HeaderText="用车时间">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gstartplace" HeaderText="起始地点">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gendplace" HeaderText="到达地点">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gqty" HeaderText="载货数">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gperson" HeaderText="载人数">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="打印" HeaderText="打印" CommandName="printBtn">
                    <HeaderStyle Width="50px"></HeaderStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="gcarid" HeaderText="车号">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="编辑" HeaderText="编辑" CommandName="EditBtn">
                    <HeaderStyle Width="50px"></HeaderStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
