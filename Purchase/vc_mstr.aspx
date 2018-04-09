<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_mstr.aspx.cs" Inherits="Purchase_vc_maintenance" %>

<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload"
    TagPrefix="Upload" %>
<!DOCTYPE html>
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
    <form id="form1" runat="server">
    <div align="center">
        <table id="tb1" >
            <tr>
                <td align="right">
                    工厂:
                </td>
                <td style=" text-align:left;">
                    <asp:DropDownList ID="dropPlant" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="dropPlant_SelectedIndexChanged">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                    <input id="hidID" type="hidden" runat="server" />
                </td>
                <td align="right">
                    车间:</td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtFactory" Width="150px"></asp:TextBox>
                </td>
                <td align="right">
                    供应商:
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropVender" runat="server" Width="200px" DataTextField="allname"
                        DataValueField="usr_loginName" AutoPostBack="True" OnSelectedIndexChanged="dropVender_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    地点:
                </td>
                <td style=" text-align:left;">
                    <asp:DropDownList ID="dropSite" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    联系人:</td>
                    <td align="left">
                    <asp:TextBox runat="server" ID="txtAtten" Width="100px"></asp:TextBox>
                </td>
                <td align="right">
                    邮箱:
                </td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtEmail" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    提交人:
                </td>
                <td style=" text-align:left;">
                    <asp:TextBox ID="txtSubmiter" runat="server" Width="100px" ReadOnly="true" BackColor="LightGray"></asp:TextBox>
                </td>
                <td>
                    提交日期:</td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtDate" Width="150px" ReadOnly="true" BackColor="LightGray"></asp:TextBox>
                </td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    科目:
                </td>
                <td style=" text-align:left;">
                    <asp:DropDownList ID="dropCate" runat="server" Width="150px" DataTextField="vcc_name"
                        DataValueField="vcc_id">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    金额:</td>
                <td align="left">
                    <asp:TextBox ID="txtAmount" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td align="right">
                    比率:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtRate" runat="server" Width="100px"></asp:TextBox>%
                    <asp:CheckBox ID="chkIsModify" runat="server" Visible="False" />
                </td>
            </tr>
            
            <tr>
                <td valign="top" style="height: 55px">
                    备注:
                </td>
                <td colspan="5" valign="top" style="height: 55px">
                    <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Height="136px" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height:10px;">
                <td></td>
            </tr>
            <tr id="tr_reason" runat="server" visible="false">
                <td >
                    原因：
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txt_reason" runat="server" TextMode="MultiLine" Width="100%" Height="50px"></asp:TextBox>
                </td>
                <td align="Center">
                    <asp:Button ID="btn_approve" runat="server"  CssClass="SmallButton3" Width="80px" Text="同意" OnClick="btn_approve_Click"/>
                    <br/>
                    <br />
                    <asp:Button ID="btn_reject" runat="server"  CssClass="SmallButton3" Width="80px" Text="拒绝" OnClick="btn_reject_Click" />
                </td>
            </tr>
            <tr style="height:10px;">
                <td></td>
            </tr>s
            <tr>
                <td valign="top" style="height: 25px">
                    &nbsp;
                </td>
                <td colspan="5" valign="top" style="text-align: center;">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Width="80px" Text="保存"
                        OnClick="btnSave_Click" />

                     <asp:Button ID="btn_delete" runat="server" CssClass="SmallButton3" Width="80px" Text="确认取消" OnClick="btn_delete_Click" Visible ="false"
                         />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_cancle" runat="server" CssClass="SmallButton3" Width="70px" Text="取消" OnClick="btn_cancle_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton3" Width="70px" Text="继续录入"
                        OnClick="btnBack_Click" />
                    
                </td>
            </tr>
            <tr id="tr_upload" runat="server">
                <td valign="top" style="height: 25px">
                    依据:
                </td>
                <td colspan="5" valign="top" style="text-align: left;">
                    <input id="FileUpload2" runat="server" style="width: 400px;" name="resumename" type="file" />&nbsp;
                    <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton3" Width="70px" Text="上传"
                        OnClick="btnUpload_Click" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td valign="top" style="height: 25px" colspan="5">
                    <asp:GridView runat="server" ID="gv" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        Width="100%" PageSize="20" AllowPaging="True" OnRowDeleting="gv_RowDeleting"
                        DataKeyNames="vc_id,vc_AllPath" OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand"
                        OnPageIndexChanging="gv_PageIndexChanging">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="tb2" Width="700px" runat="server" CellPadding="-1" CellSpacing="0"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="序号" HorizontalAlign="Center" Width="40px"></asp:TableCell>
                                    <asp:TableCell Text="文件名" HorizontalAlign="Center" Width="600px"></asp:TableCell>
                                    <asp:TableCell Text="操作" HorizontalAlign="Center" Width="60px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="序号" DataField="rowid" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="文件名" DataField="vc_docname" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="600px" />
                                <ItemStyle HorizontalAlign="left" Width="600px" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:CommandField>
                            <asp:TemplateField>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtndown" runat="server" Text="DownLoad" ForeColor="Black" CommandName="download"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="false"></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
