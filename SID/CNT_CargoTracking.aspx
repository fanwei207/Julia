<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CNT_CargoTracking.aspx.cs" Inherits="SID_CNT_CargoTracking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 228px;
        }
        .auto-style2 {
            width: 247px;
        }
        .auto-style3 {
            width: 248px;
        }
        .auto-style4 {
            width: 185px;
        }
    </style>
</head>
<body>
    <div>
        <form id="form1" runat="server">
    
        <table id="tb1" cellspacing="0" cellpadding="0" border="0" width="1000px" >
            <tr style="height:40px">
                <td class="auto-style3">进厂日期&nbsp<asp:TextBox runat="server" ID="txt_entry" ReadOnly="true" BackColor="LightGray" Width="150px"></asp:TextBox></td>
                <td class="auto-style2">出厂日期&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_leave" ReadOnly="true" BackColor="LightGray" Width="150px"></asp:TextBox></td>
                <td class="auto-style1">车队电话&nbsp<asp:TextBox runat="server" ID="txt_MotorcadePhone" ReadOnly="true" BackColor="LightGray" Width="150px"></asp:TextBox></td>
                <td class="auto-style4"></td>
                <td><asp:Button runat="server" ID="btn_back" Width="70px" CssClass="SmallButton2" Text="返回" OnClick="btn_back_Click" /></td>
                
            </tr>
            <tr style="height:40px">
                <td class="auto-style3">车牌号&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_PlateNbr" ReadOnly="true" BackColor="LightGray" Width="150px"></asp:TextBox></td>
                <td class="auto-style2">驾驶员姓名<asp:TextBox runat="server" ID="txt_Driver" ReadOnly="true" BackColor="LightGray" Width="150px"></asp:TextBox></td>
                <td class="auto-style1">联系电话&nbsp<asp:TextBox runat="server" ID="txt_DriverPhone" ReadOnly="true" BackColor="LightGray" Width="150px"></asp:TextBox></td>
                <td class="auto-style4">箱号<asp:TextBox runat="server" ID="txt_cntID" ReadOnly="true" BackColor="LightGray" Width="100px"></asp:TextBox></td>
                
            </tr>
        </table>
        <table id="tb2" cellspacing="0" cellpadding="0" border="0" width="1000px" >
            <tr style="height:30px">
                <td width="200px"><asp:TextBox ID="txt_TrackTime" Width="190px" runat="server" CssClass="SmallTextBox EnglishDate"></asp:TextBox></td>&nbsp;&nbsp;&nbsp
                <td style="width:500px"><asp:TextBox ID="txt_location" Width="480px" runat="server"></asp:TextBox></td>
                <td><asp:DropDownList ID="ddl_normal" Width="80px" runat="server">
                    <asp:ListItem Text="--" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="正常" Value="1"></asp:ListItem>
                    <asp:ListItem Text="异常" Value="0"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:DropDownList ID="ddl_ontime" Width="80px" runat="server">
                    <asp:ListItem Text="--" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:DropDownList></td>
                
                <td><asp:Button runat="server" ID="btn_save" Width="70px" CssClass="SmallButton2" Text="保存" OnClick="btn_save_Click" /></td>
            </tr>
            <tr><td colspan="5"><asp:TextBox runat="server" ID="txt_remark" Width="900px" TextMode="MultiLine" Height="50px"></asp:TextBox></td></tr>
        </table>
        
        <asp:GridView runat="server" AutoGenerateColumns="false" PageSize="20" AllowPaging="true" AllowSorting="true"
            CssClass="GridViewStyle GridViewRebuild" Width="1000px" ID="gv_track">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table runat="server" ID="tb_gv" CellPadding="-1" CellSpacing="0" Width="1000px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="联系时间" HorizontalAlign="Center" Width="200px"></asp:TableCell>
                            <asp:TableCell Text="车辆位置" HorizontalAlign="Center" Width="500px"></asp:TableCell>
                            <asp:TableCell Text="是否异常" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                            <asp:TableCell Text="是否按时到达" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                            <asp:TableCell Text="备注" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="联系时间" DataField="cargo_tracktime" >
                        <HeaderStyle Width="200px" HorizontalAlign="Center"/>
                        <ItemStyle Width="200px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="车辆位置" DataField="cargo_location" >
                        <HeaderStyle Width="500px" HorizontalAlign="Center"/>
                        <ItemStyle Width="500px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="是否异常" DataField="cargo_IsNormal" >
                        <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                        <ItemStyle Width="100px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="是否按时到达" DataField="cargo_IsOnSchedule" >
                        <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                        <ItemStyle Width="100px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="备注" DataField="cargo_remark">
                        <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                        <ItemStyle Width="100px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
       </form>        
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
