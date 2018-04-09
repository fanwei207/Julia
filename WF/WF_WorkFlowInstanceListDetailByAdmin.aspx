<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_WorkFlowInstanceListDetailByAdmin.aspx.cs"
    Inherits="WF_WorkFlowTemplateEdit" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.workflow.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <iewc:TabStrip runat="server" AutoPostBack="True" CssClass="TabStrip" TargetID="TeamMultiTab"
            TabDefaultStyle="color:white;font-size:12px;font-family:Verdana, Trebuchet MS, Arial, Helvetica; background-image:url(../images/button16.gif); float:leftheight:20px;width:80px;"
            TabHoverStyle="color: white; font-size: 12px; font-family: Verdana, Trebuchet MS, Arial, Helvetica; background-image:url(../images/button16.gif);  float: left; height: 20px;width:80px; "
            TabSelectedStyle="color: black; font-size: 12px; font-family: Verdana, Trebuchet MS, Arial, Helvetica;  background-image:url(../images/button15.gif); float: left;height: 20px ;width:80px;"
            Style="left: 100px; position: absolute; top: 10px; height: 23px;" ID="TabStrip1"
            Font-Bold="True" Font-Size="12px" OnSelectedIndexChange="TabStrip1_SelectedIndexChange">
            <iewc:Tab Text="tab1" ID="Setup1"></iewc:Tab>
        </iewc:TabStrip>
        <iewc:MultiPage ID="TeamMultiTab" runat="server" Style="border-right: #0c65dd 2px solid;
            border-top: #0c65dd 2px solid; z-index: -100; left: 100px; overflow: auto; border-left: #0c65dd 2px solid;
            border-bottom: #0c65dd 2px solid; position: absolute; top: 33px; background-color: #ffffff"
            Height="450" Width="800">
            <iewc:PageView>
                <asp:Table ID="Table1" runat="server" BorderWidth="0" Width="790px" GridLines="Both"
                    Height="410px">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3" VerticalAlign="Top" HorizontalAlign="Center">
                            <table border="0" cellpadding="1" cellspacing="1" width="780">
                                <tr style="background: white;">
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">
                                        申请号:
                                    </td>
                                    <td style="width: 235px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtReqNbr" runat="server" CssClass="SmallTextBox" Enabled="False"
                                            Height="20px" Width="130px"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 100px; height: 20px" valign="middle">
                                        操作序号:
                                    </td>
                                    <td style="width: 213px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtSortOrder" runat="server" CssClass="SmallTextBox" Enabled="False"
                                            Height="20px" Width="130px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px; height: 20px" valign="middle" align="right">
                                        事项类型:
                                    </td>
                                    <td style="width: 213px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtWorkFlowPre" runat="server" CssClass="SmallTextBox" Enabled="False"
                                            Height="20px" Width="130px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="background: white;">
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">
                                        申请日期:
                                    </td>
                                    <td style="width: 235px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtReqDate" runat="server" CssClass="SmallTextBox Date" Height="20px"
                                            Width="130px"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 100px; height: 20px" valign="middle">
                                        截止日期:
                                    </td>
                                    <td style="width: 213px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtDueDate" runat="server" CssClass="SmallTextBox Date" Height="20px"
                                            Width="130px"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px; height: 20px" valign="middle">
                                        表单:
                                    </td>
                                    <td style="width: 213px; height: 20px" valign="middle">
                                        <asp:HyperLink ID="hlForm" runat="server" Font-Underline="True" Target="_blank" Width="130px">HyperLink</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:HiddenField ID="hidSql" runat="server" />
                                        <div id="divForm" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr style="background: white;">
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">
                                        备注:
                                    </td>
                                    <td colspan="5" style="height: 20px" valign="middle">
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="SmallTextBox" Height="90px"
                                            MaxLength="1000" TextMode="MultiLine" Width="655px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="background: white;">
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">
                                        附件:
                                    </td>
                                    <td colspan="5" style="height: 20px" valign="middle">
                                        <input id="fileAttach" runat="server" name="filename1" style="width: 549px; height: 20px;
                                            border-left-color: white; border-bottom-color: white; border-top-style: inset;
                                            border-top-color: white; border-right-style: inset; border-left-style: inset;
                                            border-right-color: white; border-bottom-style: inset;" type="file" />
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                        <asp:Button ID="btn_upload" runat="server" CausesValidation="true" CssClass="SmallButton3"
                                            OnClick="btn_upload_Click" Text="上传" />
                                    </td>
                                </tr>
                                <tr style="background: white;">
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">
                                        操作人:
                                    </td>
                                    <td style="width: 235px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="SmallTextBox" Enabled="False"
                                            Height="20px" Width="130px"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px; height: 20px" valign="middle">
                                        岗位:
                                    </td>
                                    <td style="width: 213px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtCreatedByRole" runat="server" CssClass="SmallTextBox" Enabled="False"
                                            Height="20px" Width="130px"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 100px; height: 20px" valign="middle">
                                        操作时间:
                                    </td>
                                    <td style="width: 213px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtCreatedDate" runat="server" CssClass="SmallTextBox" Enabled="False"
                                            Height="20px" Width="130px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">步骤描述:</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtNoteDesc" runat="server" CssClass="SmallTextBox" Height="50px"
                                            MaxLength="1000" TextMode="MultiLine" Width="655px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table id="tblReview" runat="server" border="0" cellpadding="1" cellspacing="1" style="width: 780px;">
                                <tr style="background: white;">
                                    <td align="right" style="width: 67px; height: 20px" valign="middle">
                                        审批:
                                    </td>
                                    <td style="height: 20px" valign="middle">
                                        <asp:RadioButton ID="radFinish" runat="server" Checked="true" GroupName="radGroup1"
                                            TabIndex="4" Text="通过" Width="60px" />
                                        &nbsp; &nbsp;<asp:RadioButton ID="radStop" runat="server" GroupName="radGroup1" TabIndex="4"
                                            Text="不通过" Width="60px" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table id="tblButton" runat="server" border="0" cellpadding="2" cellspacing="2" style="width: 260px">
                                <tr>
                                    <td align="left">
                                        <asp:Button ID="btn_save" runat="server" CausesValidation="true" CssClass="SmallButton3"
                                            OnClick="btn_save_Click" Width="53px" Text="保存" />
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btn_return" runat="server" CausesValidation="true" CssClass="SmallButton3"
                                            OnClick="btn_return_Click" Width="53px" Text="返回" />
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvFNIA" runat="server" AutoGenerateColumns="False" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="1" DataKeyNames="Attach_ID"
                                GridLines="Vertical" Height="10px" OnRowCommand="gvFNIA_RowCommand" OnRowDataBound="gvFNIA_RowDataBound"
                                OnRowDeleting="gvFNIA_RowDeleting" PageSize="15" Width="600px" CssClass="GridViewStyle">
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="Attach_Name" HeaderText="名称">
                                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Attach_CreatedBy" HeaderText="上传者">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Attach_CreatedDate" HeaderText="上传时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                        HtmlEncode="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="查看">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkstep" runat="server" CommandArgument='<%# Eval("Attach_ID") %>'
                                                CommandName="download" Font-Size="12px" Font-Underline="true" ForeColor="Blue"
                                                Text="查看"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkdelete" runat="server" CommandArgument='<%# Eval("Attach_ID") %>'
                                                CommandName="Delete" Font-Size="12px" Font-Underline="true" ForeColor="Blue"
                                                Text="<u>删除</u>"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </iewc:PageView>
        </iewc:MultiPage>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
