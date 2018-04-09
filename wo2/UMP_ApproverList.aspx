<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UMP_ApproverList.aspx.cs" Inherits="wo2_UMP_ApproverList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 318px;
        }
    </style>
</head>
<body>
    <div style="text-align: left;">
        <form id="form1" runat="server">
            <table>

                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" style="width: 377px">申请单号
                    <asp:TextBox ID="txtProjectcode" runat="server" Width="132px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox><asp:Label ID="lblmid" runat="server" Text=" " Visible="false"></asp:Label>
                                    成本中心
                                    <asp:TextBox ID="txtdepCode" runat="server" Width="90px" CssClass="SmallTextBox"
                                        TabIndex="1"></asp:TextBox>
                                </td>
                                <td style="width: 217px">域
                                    <asp:DropDownList ID="ddldomain" runat="server">
                                        <asp:ListItem Selected="True">--</asp:ListItem>
                                        <asp:ListItem>SZX</asp:ListItem>
                                        <asp:ListItem>ZQL</asp:ListItem>
                                        <asp:ListItem>HQL</asp:ListItem>
                                        <asp:ListItem>YQL</asp:ListItem>
                                    </asp:DropDownList>
                                    状态
                                    <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="10">--</asp:ListItem>
                                        <asp:ListItem Value="0">新增</asp:ListItem>
                                        <asp:ListItem Value="1">审批中</asp:ListItem>
                                        <asp:ListItem Value="2">完成</asp:ListItem>
                                        <asp:ListItem Value="-1">拒绝</asp:ListItem>
                                    </asp:DropDownList>

                                </td>



                                <td align="left" class="style1" style="width: 380px;">&nbsp;创建时间:<asp:TextBox ID="txtApplyDate" runat="server" Width="75px" CssClass="SmallTextBox Date"></asp:TextBox>

                                    -<asp:TextBox ID="txtApplyDate1" runat="server" Width="75px" CssClass="SmallTextBox Date"></asp:TextBox>
                                    <asp:CheckBox ID="chkb_displayToApprove" runat="server" Text="只看自己审批"
                                        Checked="true" Width="158px" AutoPostBack="True" OnCheckedChanged="chkb_displayToApprove_CheckedChanged" />
                                    &nbsp;
                                </td>
                                <td align="left" style="width: 280px; height: 26px;">
                                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="50px"
                                        OnClick="btnSearch_Click" />
                                    &nbsp; &nbsp;
                    <asp:Button ID="btnApply" runat="server" Text="新增" OnClick="btnApply_Click"
                        Width="72px" CssClass="SmallButton2" Visible="true" />
                                    &nbsp; &nbsp;
                    <asp:Button ID="EXCEL" runat="server" CssClass="SmallButton2"
                        Text="Export" Width="50px" OnClick="BtnExport_Click"></asp:Button>   &nbsp; &nbsp;

                                      <asp:Button ID="btnappv" runat="server" CssClass="SmallButton2"
                        Text="批量审批" Width="50px" OnClick="btnappv_Click" Visible="false"></asp:Button>

                                    <asp:Label ID="lblcheck" runat="server" Text="0" Visible="false"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                             <table style="width: 800px;" cellpadding="1" cellspacing="1" id="appv" runat="server" visible="false">
                <tr align="right">
                    <td align="right" style="width: 50px;">
                        <asp:Label ID="Label3" runat="server" Text="提交给：" Width="76px" CssClass="LabelRight"></asp:Label>
                    </td>
                    <td align="left" class="style7">&nbsp;<asp:TextBox
                        ID="txtApproveName" runat="server"
                        CssClass="SmallButton2" Width="78px"
                        Height="21px"></asp:TextBox>
                        &nbsp;
                    <asp:TextBox ID="txt_ApproveEmail" runat="server" CssClass="SmallButton2" Width="161px"
                        Height="21px"></asp:TextBox>
                        <asp:TextBox ID="txt_approveID" runat="server" Width="0px" BorderWidth="0"></asp:TextBox>
                        <asp:Button ID="btn_Approver" runat="server" Text="选择提交人" OnClick="btn_Approver_Click"
                            CssClass="SmallButton2" Width="93px" Height="21px" />
                        &nbsp;
                    <asp:CheckBox ID="chkEmail" runat="server" Text="发送邮件" Checked="true" />
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label5" runat="server" Text="理由:" Width="100px" CssClass="LabelRight"></asp:Label>
                    </td>
                    <td align="left" class="style7">
                        <asp:TextBox ID="txtApplyReason" runat="server" CssClass="SmallTextBox" Width="520px"
                            MaxLength="500" TextMode="MultiLine" Height="28px"></asp:TextBox>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblApplyDate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                      
                    </td>
                    <td class="style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_submit" runat="server" Text="提交" CssClass="SmallButton2"
                        OnClick="btn_submit_Click" />
                        &nbsp; &nbsp;
                    <asp:Button ID="btn_approve" runat="server" Text="通过" CssClass="SmallButton2"
                        OnClick="btn_approve_Click" />&nbsp; &nbsp; &nbsp;<asp:Button ID="btn_diaApp" runat="server"
                            CssClass="SmallButton2" Text="拒绝" Width="70px" OnClick="btn_diaApp_Click" Visible="false"/>&nbsp;
                    &nbsp;
                    
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
                    </td>
                </tr>
            </table>




       
            <asp:GridView ID="gv" AllowPaging="True" AutoGenerateColumns="False" PageSize="25"
                CssClass="GridViewStyle" runat="server" Width="1200px" DataKeyNames="id,UMP_appby,UMP_createby,UMP_status"
                OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowDeleting="gv_RowDeleting">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="1200px"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="center" Text="没有数据" Width="90px"></asp:TableCell>
                           
                        </asp:TableRow>
                        <asp:TableFooterRow BackColor="white" ForeColor="Black">
                            <asp:TableCell HorizontalAlign="Center" Text="No apply information" ColumnSpan="8"></asp:TableCell>
                        </asp:TableFooterRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Select" runat="server" Width="20px" />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" />
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                        <ControlStyle Font-Size="12px" Font-Underline="True" />
                          <HeaderTemplate>
                                 <asp:CheckBox ID="chkAll" runat="server" Text="" Width="60px"
                            AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                                </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="申请单号" DataField="UMP_code" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="类型" DataField="UMP_typename" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="总账" DataField="UMP_accountname" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="55px" />
                        <ItemStyle HorizontalAlign="Center" Width="55px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="明细账" DataField="UMP_accountdetname" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="55px" />
                        <ItemStyle HorizontalAlign="Center" Width="55px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="域" DataField="UMP_domain" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="55px" />
                        <ItemStyle HorizontalAlign="Center" Width="55px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="地点" DataField="UMP_site" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="55px" />
                        <ItemStyle HorizontalAlign="Center" Width="55px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="成本中心" DataField="UMP_Departmentscode" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="55px" />
                        <ItemStyle HorizontalAlign="Center" Width="55px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="创建人" DataField="UMP_createname" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="55px" />
                        <ItemStyle HorizontalAlign="Center" Width="55px" />
                    </asp:BoundField>




                    <asp:BoundField HeaderText="创建时间" DataField="UMP_createdate" ReadOnly="True"
                        DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="状态" DataField="UMP_status" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="当前审批人" DataField="UMP_appname" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Approve" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkFirstApp" Text="Approve" ForeColor="black" Font-Underline="true"
                                Enabled="false" Font-Size="12px" runat="server" CommandArgument='<%#  ((GridViewRow) Container).RowIndex %>'
                                CommandName="Approve" />
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                        <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkApproveDetail" Text="View" ForeColor="black" Font-Underline="true"
                                Font-Size="12px" runat="server" CommandName="look" CommandArgument='<%#  ((GridViewRow) Container).RowIndex %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" Text="delete" ForeColor="black" Font-Underline="true"
                                Font-Size="12px" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="delete"
                                Enabled="false" />
                        </ItemTemplate>
                        <HeaderStyle Width="80px" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <ControlStyle Font-Size="12px" Font-Underline="True" />
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
