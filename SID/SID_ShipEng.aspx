<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_ShipEng.aspx.cs" Inherits="SID_SID_ShipEng" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1100px" runat="server">
            <tr>
                <td>
                    System No.<asp:TextBox ID="txtPK" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td class="style1">
                    Reference:<asp:TextBox ID="txtPKref" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    Waybill No.<asp:TextBox ID="txtnbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    Transport:<asp:TextBox ID="txtVia" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    Container Type:<asp:TextBox ID="txtCtype" runat="server" CssClass="SmallTextBox"
                        Width="100px"></asp:TextBox>
                </td>
                <td>
                    Company:<asp:TextBox ID="txtdomain" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Packing Place:<asp:TextBox ID="txtsite" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    Shipment Date:<asp:TextBox ID="txtShipDate" runat="server" CssClass="SmallTextBox Date"
                        Width="100px" onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                <td>
                    Manufacture Date:<asp:TextBox ID="txtOutDate" runat="server" CssClass="SmallTextBox"
                        Width="100px"></asp:TextBox>
                </td>
                <td colspan="2">
                    Shipped<asp:TextBox ID="txtshipto" runat="server" CssClass="SmallTextBox" Width="280px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblSID" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="保存" Width="40"
                        OnClick="btnSave_Click" Visible="False" />
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="新增" Width="40"
                        OnClick="btnAdd_Click" Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                    SO Nbr:<asp:TextBox ID="txt_SID_nbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    SO Line:<asp:TextBox ID="txt_SID_line" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Exemption<asp:CheckBox ID="chkMianJian" runat="server" Enabled="false" OnCheckedChanged="chkMianJian_CheckedChanged"
                        AutoPostBack="True" />
                </td>
                <td class="style1">
                    Inspection Date<asp:TextBox ID="txtInspectDate" runat="server" CssClass="SmallTextBox Date"
                        Enabled="False" onkeydown="event.returnValue=false;" onpaste="return false;"
                        Width="100px"></asp:TextBox>
                </td>
                <td>
                    Inspection Locations<asp:TextBox ID="txtInspectSite" runat="server" CssClass="SmallTextBox"
                        Enabled="False" Width="100px"></asp:TextBox>
                </td>
                <td>
                    Pre-distribution Date<asp:TextBox ID="txt_InspMatchDate" runat="server" CssClass="SmallTextBox Date"
                        Enabled="False" onkeydown="event.returnValue=false;" onpaste="return false;"
                        Width="100px"></asp:TextBox>
                </td>
                <td>
                    With cabin<asp:CheckBox ID="chkIsCabin" runat="server" Enabled="false" AutoPostBack="True" />
                    <asp:TextBox ID="txtOldShipDate" runat="server" CssClass="SmallTextBox" onkeydown="event.returnValue=false;"
                        onpaste="return false;" Visible="False" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSaveInsp" runat="server" CssClass="SmallButton3" Text="保存" Width="40px"
                        OnClick="btnSaveInsp_Click" Visible="False" Height="26px" />
                    <asp:Button ID="btnClearInsp" runat="server" CssClass="SmallButton3" Text="清除" Width="40"
                        OnClick="btnClearInsp_Click" Visible="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 20px">
                    <asp:RadioButton ID="rad1" runat="server" Text="No Customs Clearance" AutoPostBack="True"
                        Checked="True" GroupName="RadGroup" OnCheckedChanged="rad1_CheckedChanged"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="rad2" runat="server" Text="Customs Clearance" AutoPostBack="True"
                        Checked="false" GroupName="RadGroup" OnCheckedChanged="rad2_CheckedChanged">
                    </asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="rad3" runat="server" Text="New Changes" AutoPostBack="True"
                        Checked="false" GroupName="RadGroup" OnCheckedChanged="rad3_CheckedChanged">
                    </asp:RadioButton>
                </td>
                <td style="height: 20px">
                    Creation Date<asp:TextBox ID="txtcreated" runat="server" CssClass="SmallTextBox Date"
                        Width="100px" onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 20px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To<asp:TextBox
                        ID="txtcreated1" runat="server" CssClass="SmallTextBox" Width="100px" onkeydown="event.returnValue=false;"
                        onpaste="return false;"></asp:TextBox>&nbsp;
                    <asp:CheckBox ID="chkInspectDate" runat="server" Text="是否拥有社设置验货的权限" Visible="False" />
                </td>
                <td style="height: 20px">
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="Query" Width="40"
                        OnClick="btnSearch_Click" />
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
                <asp:BoundField DataField="PK" HeaderText="System No.">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="PKref" HeaderText="Reference">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="nbr" HeaderText="Waybill No.">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="via" HeaderText="Transport">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ctype" HeaderText="Containers Type">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="site" HeaderText="Packing Place">
                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="shipdate" HeaderText="Shipment Date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="outdate" HeaderText="Date of Manufacture">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_date" HeaderText="Inspection Date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_site" HeaderText="Inspection Locations">
                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_matchdate" HeaderText="Pre-distribution Date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="mj" HeaderText="Exemption">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="IsCabin" HeaderText="With cabin">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit1"
                            Enabled='<%# Eval("SID_org_con") %>' Text="<u>编辑</u>" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:ButtonField CommandName="Detail1" Text="<u>Details</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Del1"
                            Enabled='<%# Eval("SID_org_con") %>' Text="<u>删除</u>" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="domain" HeaderText="Domain">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="State">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="shipto" HeaderText="Shipped">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Documents">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org1_con") %>' Text='<%# Eval("SID_org1_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm1" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customs clearance">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org2_con") %>' Text='<%# Eval("SID_org2_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm2" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="IsQC" HeaderText="QC">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
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
