<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bom_CheckDetails.aspx.cs" Inherits="Bom_CheckDetails" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="890px">
            <br />
            <br />
            <tr>
                <td width="210">
                    �������Ϻ�:<asp:TextBox ID="txt_ps_par" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                </td>
                <td style="height: 20px" width="150">
                    ��������<asp:TextBox ID="txt_BeginDate" runat="server" CssClass="SmallTextBox Date" Width="80px"
                        onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                <td width="100">
                    ��<asp:TextBox
                        ID="txt_EndDate" runat="server" CssClass="SmallTextBox Date" Width="80px" onkeydown="event.returnValue=false;"
                        onpaste="return false;"></asp:TextBox>
                </td>
                <td style="height: 20px" width="320">
                    <asp:RadioButtonList ID="rbt_ChangBom" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" AutoPostBack="true" OnSelectedIndexChanged="rbt_ChangBom_CheckedChanged">
                    <asp:ListItem Text="������" Value="S"></asp:ListItem> 
                    <asp:ListItem Text="��ǩ��" Value="W" Selected="True"></asp:ListItem>   
                    <asp:ListItem Text="��ǩ��" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="�Ѿܾ�" Value="F"></asp:ListItem> 
                    <asp:ListItem Text="����" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                 <%--   <asp:RadioButton ID="rad1" runat="server" Text="��ǩ��" AutoPostBack="True" Checked="True"
                        GroupName="RadGroup" OnCheckedChanged="rad1_CheckedChanged"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="rad2" runat="server" Text="��ǩ��" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" OnCheckedChanged="rad2_CheckedChanged"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="rad3" runat="server" Text="δǩ��" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" OnCheckedChanged="rad3_CheckedChanged"></asp:RadioButton>--%>
                </td>

                <td style="height: 20px">
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="��ѯ" Width="40"
                        OnClick="btnSearch_Click" />
                    <asp:Button ID="btn_exrort" runat="server" CssClass="SmallButton3" Text="����" Width="40"
                        OnClick="btn_export_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv_CheckInfo" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
            DataKeyNames="ps_id" OnRowCommand="gvShip_RowCommand" OnPageIndexChanging="gvShip_PageIndexChanging"
            OnRowDataBound="gvShip_RowDataBound" Width="910px" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox id="chkImport" type="checkbox" name="chkImport" runat="server" value='<%#Eval("ps_id") %>' />
                    </ItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                    </HeaderTemplate>
                </asp:TemplateField>

 <%--               <asp:TemplateField>
                     <ItemTemplate>
                        <asp:CheckBox ID="chk" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="ps_id" HeaderText="ID">
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                </asp:BoundField>

                <asp:BoundField DataField="ps_types" HeaderText="">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_par" HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_comp" HeaderText="�Ӽ�">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_start" HeaderText="��Ч����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_end" HeaderText="��ֹ����">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_qty_per" HeaderText="ÿ������">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_scrp_pct" HeaderText="��Ʒ��">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_to_comp" HeaderText="���Ӽ�">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>

<%--                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit1"
                            Enabled='<%# Eval("bom_id") %>' Text="<u>����</u>" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>--%>
                <asp:ButtonField CommandName="Detail1" Text="<u>����</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:ButtonField CommandName="Check" Text="<u>����</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
              <%--  <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Del1"
                            Enabled='<%# Eval("SID_org_con") %>' Text="<u>ɾ��</u>" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>--%>
                <asp:BoundField DataField="ps_remark" HeaderText="ԭ��">
                    <ItemStyle HorizontalAlign="Left" Width="230px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="PlantCode" HeaderText="��">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_types" HeaderText="״̬" Visible="false">
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
