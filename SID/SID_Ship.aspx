<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_Ship.aspx.cs" Inherits="SID_SID_Ship" %>

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
                <td>
                    系统单号<asp:TextBox ID="txtPK" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td class="style1">
                    参&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 考<asp:TextBox ID="txtPKref" runat="server"
                        CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    出运单号<asp:TextBox ID="txtnbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    运输方式<asp:TextBox ID="txtVia" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    集装箱型<asp:TextBox ID="txtCtype" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    所在公司<asp:TextBox ID="txtdomain" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    装箱地点<asp:TextBox ID="txtsite" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    出运日期<asp:TextBox ID="txtShipDate" runat="server" CssClass="SmallTextBox Date" Width="100px"
                        onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                <td>
                    出厂日期<asp:TextBox ID="txtOutDate" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td colspan="2">
                    运&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;往<asp:TextBox ID="txtshipto" runat="server"
                        CssClass="SmallTextBox" Width="280px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblSID" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="保存" Width="40"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="新增" Width="40"
                        OnClick="btnAdd_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    免&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检&nbsp;<asp:CheckBox ID="chkMianJian"
                        runat="server" Enabled="false" OnCheckedChanged="chkMianJian_CheckedChanged"
                        AutoPostBack="True" />
                </td>
                <td class="style1">
                    验货日期<asp:TextBox ID="txtInspectDate" runat="server" CssClass="SmallTextBox Date"
                        Enabled="False" onkeydown="event.returnValue=false;" onpaste="return false;"
                        Width="100px"></asp:TextBox>
                </td>
                <td>
                    验货地点<asp:TextBox ID="txtInspectSite" runat="server" CssClass="SmallTextBox" Enabled="False"
                        Width="100px"></asp:TextBox>
                </td>
                <td>
                    预配日期<asp:TextBox ID="txt_InspMatchDate" runat="server" CssClass="SmallTextBox Date"
                        Enabled="False" onkeydown="event.returnValue=false;" onpaste="return false;" Width="100px"></asp:TextBox>
                </td>
                <td>
                        配&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;舱&nbsp;<asp:CheckBox ID="chkIsCabin"
                        runat="server" Enabled="false"
                        AutoPostBack="True" />

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
                    <asp:RadioButton ID="rad1" runat="server" Text="未报关" AutoPostBack="True" Checked="True"
                        GroupName="RadGroup" OnCheckedChanged="rad1_CheckedChanged"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="rad2" runat="server" Text="已报关" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" OnCheckedChanged="rad2_CheckedChanged"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="rad3" runat="server" Text="新修改" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" OnCheckedChanged="rad3_CheckedChanged"></asp:RadioButton>
                </td>
                <td style="height: 20px">
                    创建日期<asp:TextBox ID="txtcreated" runat="server" CssClass="SmallTextBox Date" Width="100px"
                        onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 20px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;至<asp:TextBox
                        ID="txtcreated1" runat="server" CssClass="SmallTextBox" Width="100px" onkeydown="event.returnValue=false;"
                        onpaste="return false;"></asp:TextBox>&nbsp;
                    <asp:CheckBox ID="chkInspectDate" runat="server" Text="是否拥有社设置验货的权限" Visible="False" />
                </td>
                <td style="height: 20px">
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" Width="40"
                        OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_exportexcel" runat="server" CssClass="SmallButton3" 
                        Text="导出" Width="40px"
                        OnClick="btn_exportexcel_Click" />
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
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit1"
                            Enabled='<%# Eval("SID_org_con") %>' Text="<u>编辑</u>" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:ButtonField CommandName="Detail1" Text="<u>详细</u>">
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
                <asp:BoundField DataField="domain" HeaderText="域">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="状态">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>

                <asp:TemplateField ShowHeader="False" HeaderText="通知">
                    <ItemTemplate>
                        <asp:CheckBoxList ID="cklist_domain" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value = "1">SZX</asp:ListItem>
                            <asp:ListItem Value = "2">ZQL</asp:ListItem>
                            <asp:ListItem Value = "5">YQL</asp:ListItem>
                            <asp:ListItem Value = "8">HQL</asp:ListItem>
                        </asp:CheckBoxList>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="确认">
                    <ItemTemplate>
                        <asp:Button ID="btn_check" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_isCK") %>' Text='<%# Eval("SID_isCK_createdBy") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="ConfirmC" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="修改">
                    <ItemTemplate>
                        <asp:Button ID="btn_update" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_isUP") %>' Text='<%# Eval("SID_isUP_createdBy") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="ConfirmU" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
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
                <asp:TemplateField HeaderText="单证">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org1_con") %>' Text='<%# Eval("SID_org1_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm1" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="报关">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org2_con") %>' Text='<%# Eval("SID_org2_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm2" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
<%--               <asp:BoundField DataField="IsQC" HeaderText="质检">
                    <ItemStyle HorizontalAlign="Left" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="放行">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_IsQC") %>' Text='<%# Eval("IsQCCheck") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm3" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
