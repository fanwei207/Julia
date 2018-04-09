<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_WorkFlowInstanceInsert.aspx.cs"
    Inherits="WF_WorkFlowTemplateEdit" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.dev.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.workflow.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="800px;" style="height: 35px;
            font-family: 微软雅黑; background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;">
            <tr>
                <td style="width: 3px; background-image: url(../images/bg_tb1.jpg); background-repeat: no-repeat;">
                </td>
                <td align="left" colspan="6" style="height: 25px; width: 800px;">
                    <b>&nbsp;流程模板:</b>
                    <asp:DropDownList ID="ddlWorkFlow" runat="server" DataTextField="Flow_Name" DataValueField="Flow_ID"
                        Width="200px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnAdd" runat="server" CausesValidation="true" CssClass="SmallButton3"
                        Text="新建" Width="50px" OnClick="BtnAdd_Click" />
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;">
                </td>
            </tr>
        </table>
        <br />
        <iewc:TabStrip ID="TabStrip1" runat="server" AutoPostBack="True" CssClass="TabStrip"
            TabDefaultStyle="color:white;font-size:12px; font-family:Verdana, Trebuchet MS, Arial, Helvetica; background-image:url(../images/button16.gif); float:leftheight:20px;width:80px;"
            TabHoverStyle="color: white; font-size: 12px; font-family: Verdana, Trebuchet MS, Arial, Helvetica; background-image:url(../images/button16.gif);  float: left; height: 20px;width:80px; "
            TabSelectedStyle="color: black; font-size: 12px; font-family: Verdana, Trebuchet MS, Arial, Helvetica;  background-image:url(../images/button15.gif); float: left;height: 20px ;width:80px;"
            Font-Bold="True" Font-Size="12px" Style="left: 135px; position: absolute; top: 50px;
            bottom: 302px;">
            <iewc:Tab ID="Setup1" Text="tab1"></iewc:Tab>
        </iewc:TabStrip>
        <iewc:MultiPage ID="TeamMultiTab" runat="server" Style="z-index: -100; left: 135px;
            border: #0c65dd 2px solid; overflow: auto; position: absolute; top: 73px; background-color: #ffffff"
            Height="450" Width="800">
            <iewc:PageView>
                <asp:Table ID="tblSupply" runat="server" BorderWidth="0" Width="790px" GridLines="Both"
                    Height="375px">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3" VerticalAlign="Top" HorizontalAlign="Center">
                            <table style="border: 1px solid #cfd2d7;" cellpadding="3" cellspacing="4" width="798px">
                                <tr>
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">
                                        申请号:
                                    </td>
                                    <td style="width: 235px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtReqNbr" runat="server" CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled SmallTextBox"
                                            Height="20px" Width="130px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 100px; height: 20px" valign="middle">
                                        操作序号:
                                    </td>
                                    <td style="width: 213px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtSortOrder" runat="server" CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled SmallTextBox"
                                            Height="20px" Width="130px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 80px; height: 20px" valign="middle" align="right">
                                        事项类型:
                                    </td>
                                    <td style="width: 213px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtWorkFlowPre" runat="server" CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled SmallTextBox"
                                            Height="20px" Width="130px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">
                                        申请日期:
                                    </td>
                                    <td style="width: 235px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtReqDate" runat="server" CssClass="SmallTextBox Date" Height="20px"
                                            Width="130px"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px; height: 20px" valign="middle">
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
                                        <asp:HyperLink ID="hlForm" runat="server" Width="130px" Font-Underline="True" Target="_blank">HyperLink</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:HiddenField ID="hidSql" runat="server" />
                                        <div id="divForm" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">
                                        备注:
                                    </td>
                                    <td colspan="5" style="height: 20px" valign="middle">
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="SmallTextBox" Height="50px"
                                            MaxLength="1000" TextMode="MultiLine" Width="655px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">
                                        附件:
                                    </td>
                                    <td colspan="5" style="height: 20px" valign="middle">
                                        <input id="fileAttach" runat="server" cssclass="" name="filename1" style="width: 549px;
                                            height: 20px; border-left-color: white; border-bottom-color: white; border-top-style: inset;
                                            border-top-color: white; border-right-style: inset; border-left-style: inset;
                                            border-right-color: white; border-bottom-style: inset;" type="file" />
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btn_upload" runat="server" CausesValidation="true" CssClass="SmallButton3"
                                            OnClick="btn_upload_Click" Text="上传" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">
                                        操作人:
                                    </td>
                                    <td style="width: 235px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled SmallTextBox"
                                            Height="20px" Width="130px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px; height: 20px" valign="middle">
                                        岗位:
                                    </td>
                                    <td style="width: 213px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtCreatedByRole" runat="server" CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled SmallTextBox"
                                            Height="20px" Width="130px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px; height: 20px" valign="middle">
                                        操作时间:
                                    </td>
                                    <td style="width: 213px; height: 20px" valign="middle">
                                        <asp:TextBox ID="txtCreatedDate" runat="server" CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled SmallTextBox"
                                            Height="20px" Width="130px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 157px; height: 20px" valign="middle">步骤描述:</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtNoteDesc" runat="server" CssClass="SmallTextBox" Height="50px"
                                            MaxLength="1000" TextMode="MultiLine" Width="655px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="padding-left: 20px">
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table id="tblButton" runat="server" border="0" cellpadding="2" cellspacing="2" style="width: 300px">
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Button ID="btn_save" runat="server" CausesValidation="true" CssClass="SmallButton3"
                                            OnClick="btn_save_Click" Width="53px" Text="提交" OnClientClick="getSaveFormDataSqlStr()"/>
                                        <asp:CheckBox ID="chkEmail" runat="server" Text="Email" Checked="true" />
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btn_return" runat="server" CausesValidation="true" CssClass="SmallButton3"
                                            OnClick="btn_return_Click" Text="返回" Width="53px" />
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvFNIA" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                DataKeyNames="Attach_ID" Visible="false" GridLines="Vertical" Height="10px" OnRowCommand="gvFNIA_RowCommand"
                                OnRowDataBound="gvFNIA_RowDataBound" OnRowDeleting="gvFNIA_RowDeleting" PageSize="15"
                                Width="600px">
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
                                        <HeaderStyle Width="50px" />
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
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
