<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.applycarPrint" CodeFile="applycarPrint.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <table cellspacing="0" cellpadding="0" width="570" align="center" bgcolor="white"
            border="0">
            <tr width="100%" align="left">
                <td style="width: 450px" align="center">
                    <asp:Label ID="headline2" runat="server" Width="48px" Font-Size="10"></asp:Label><asp:Label
                        ID="headline1" TabIndex="19" runat="server" Width="450px" Font-Size="10" Height="10px"></asp:Label>
                    <hr style="width: 198.28%; height: 2px" width="198.28%" size="1">
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="570" align="center" bgcolor="white"
            border="0">
            <tr width="100%" align="center">
                <td>
                    <p>
                    </p>
                    <asp:Table ID="Table1" runat="server" Width="680px" BorderStyle="Solid" CellSpacing="0"
                        BorderColor="Black" CellPadding="0" GridLines="Both" Enabled="False">
                        <asp:TableRow BorderWidth="1px" BorderStyle="Solid">
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                                Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;TO：" ID="cell1"></asp:TableCell>
                            <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                                Wrap="False">
                                <asp:TextBox runat="server" BorderStyle="None" Height="19px" AutoPostBack="True"
                                    TabIndex="9" Width="280px" Wrap="False" ID="objtto" MaxLength="10"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                                Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;FROM：" ID="customerID1"></asp:TableCell>
                            <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                                Wrap="False">
                                <asp:TextBox runat="server" BorderStyle="None" Height="19px" AutoPostBack="True"
                                    TabIndex="9" Width="280px" Wrap="False" ID="objtfrom" MaxLength="10"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow BorderWidth="1px" BorderStyle="Solid">
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                                Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;TEL：" ID="objttotelaa"></asp:TableCell>
                            <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                                Wrap="False">
                                <asp:TextBox runat="server" BorderStyle="None" Height="19px" AutoPostBack="True"
                                    TabIndex="9" Width="280px" Wrap="False" ID="objttotel" MaxLength="10"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                                Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;TEL：" ID="adeaf"></asp:TableCell>
                            <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                                Wrap="False">
                                <asp:TextBox runat="server" BorderStyle="None" Height="19px" AutoPostBack="True"
                                    TabIndex="9" Width="280px" Wrap="False" ID="objtfromtel" MaxLength="10"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow BorderWidth="1px" BorderStyle="Solid">
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                                Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;FAX：" ID="line31"></asp:TableCell>
                            <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                                Wrap="False">
                                <asp:TextBox runat="server" BorderStyle="None" Height="19px" AutoPostBack="True"
                                    TabIndex="9" Width="280px" Wrap="False" ID="objttofax" MaxLength="10"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                                Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;FAX：" ID="line32"></asp:TableCell>
                            <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                                Wrap="False">
                                <asp:TextBox runat="server" BorderStyle="None" Height="19px" AutoPostBack="True"
                                    TabIndex="9" Width="280px" Wrap="False" ID="objtfromfax" MaxLength="10"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow BorderWidth="1px" BorderStyle="Solid">
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                                Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;CC：" ID="line41"></asp:TableCell>
                            <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                                Wrap="False">
                                <asp:TextBox runat="server" BorderStyle="None" Height="19px" AutoPostBack="True"
                                    TabIndex="9" Width="280px" Wrap="False" ID="objtCC" MaxLength="10"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                                Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;DATE：" ID="line42"></asp:TableCell>
                            <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                                Wrap="False">
                                <asp:TextBox runat="server" BorderStyle="None" Height="19px" AutoPostBack="True"
                                    TabIndex="9" Width="200px" Wrap="False" ID="objtDate" MaxLength="10"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow BorderWidth="1px" BorderStyle="Solid">
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                                Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;SB：" ID="line51"></asp:TableCell>
                            <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                                Wrap="False">
                                <asp:TextBox runat="server" BorderStyle="None" Height="19px" AutoPostBack="True"
                                    TabIndex="9" Width="280px" Wrap="False" ID="objtSB" MaxLength="10"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" Width="60px"
                                Font-Bold="True" HorizontalAlign="Left" Wrap="False" Text="&#160;OTHER：" ID="line52"></asp:TableCell>
                            <asp:TableCell BorderWidth="1px" VerticalAlign="Middle" Width="280px" HorizontalAlign="Left"
                                Wrap="False">
                                <asp:TextBox runat="server" BorderStyle="None" Height="19px" AutoPostBack="True"
                                    TabIndex="9" Width="280px" Wrap="False" ID="objtOTHER" MaxLength="10"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <br>
                    <table cellspacing="0" cellpadding="0" width="570" align="center" bgcolor="white"
                        border="0">
                        <tr>
                            <td style="width: 570px; height: 40px" align="center">
                                <b><font size="5">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    用车申请</font></b>
                                <td width="20%">
                                    <font face="宋体"></font>
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 570px; height: 18px" align="left">
                                <asp:Table ID="Table3" runat="server" Width="451px" BorderStyle="None">
                                    <asp:TableRow Width="570px" BorderStyle="None">
                                        <asp:TableCell Width="600px" HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="labelrequest" Font-Size="8">因业务需要，特向你部门提出用车申请，具体用车内容如下：</asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="dgOrderDetail" runat="server" Width="770px" AllowPaging="False"
                        CssClass="GridViewStyle AutoPageSize">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn DataField="gcarid" ReadOnly="True" HeaderText="序号">
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="gUsedDate"   HeaderText="用车时间">
                                <HeaderStyle Width="60px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="gstartplace"   HeaderText="起始地点">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="gendplace"   HeaderText="到达地点">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="gqty"  HeaderText="载货数">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="gperson"  HeaderText="载人数">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="comments"  HeaderText="备注">
                                <HeaderStyle Width="200px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns> 
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
         <br />
        <table width="680" height="200">
            <tr align="left" width="100%" height="100%">
                <asp:Label ID="Label1" runat="server" Font-Size="10" Visible="true">
					此致！</asp:Label>
            </tr>
            <tr align="left">
                <td style="width: 400px">
                </td>
                <td>
                    <p>
                        <asp:Label ID="tailline1" runat="server" Font-Size="10" Width="248px">Label</asp:Label></p>
                    <p>
                        <asp:Label ID="tailline2" runat="server" Font-Size="10" Width="112px">Label</asp:Label></p>
                    <p>
                        <asp:Label ID="tailline3" runat="server" Font-Size="10" Width="120px">Label</asp:Label></p>
                    <p>
                        <asp:Label ID="tailline4" runat="server" Font-Size="10" Width="128px">Label</asp:Label></p>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
