<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_WorkFlowTemplateList.aspx.cs"
    Inherits="WF_WorkFlowTemplate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
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
        <table class="main_top" border="0" cellpadding="0" cellspacing="0" width="1020px">
            <tr>
                <td class="main_left">
                </td>
                <td align="center" style="height: 27px; display: inline;">
                    流程模板名称:<asp:TextBox 
                        ID="txtFlowName" runat="server" Height="20px" Width="120px" 
                        CssClass="SmallTextBox Param"></asp:TextBox>
                    &nbsp; 创建者:<asp:TextBox ID="txtCreatedBy" runat="server" Height="20px" Width="120px"
                        CssClass="SmallTextBox Param"></asp:TextBox>
                    &nbsp; 创建日期:
                    <asp:TextBox ID="txtCreatedDate1" runat="server" Height="20px" MaxLength="10" Width="76px"
                        CssClass="SmallTextBox Date Param"></asp:TextBox>-
                    <asp:TextBox ID="txtCreatedDate2" runat="server" Height="20px" MaxLength="10" Width="76px"
                        CssClass="SmallTextBox Date Param"></asp:TextBox>
                    &nbsp;
                    <input id="chkall" name="chkall" type="checkbox" value="true" runat="server" 
                        class="Param" />所有
                    &nbsp;<asp:Button ID="BtnSearch" runat="server" CssClass="SmallButton3" Text="查询"
                        OnClick="BtnSearch_Click" Width="50px" />
                    &nbsp;
                    <asp:Button ID="BtnAdd" runat="server" CssClass="SmallButton3" Text="增加" OnClick="BtnAdd_Click"
                        Width="50px" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWF" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="22" DataKeyNames="Flow_ID" Width="1020px"
            OnRowDeleting="gvWF_RowDeleting" OnRowCommand="gvWF_RowCommand" OnRowDataBound="gvWF_RowDataBound"
            OnPageIndexChanging="gvWF_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="Flow_Name" HeaderText="流程模板名称">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Flow_Req_Pre" HeaderText="流程模板代码">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Flow_Description" HeaderText="流程模板描述">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="表单名称">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkform" ForeColor="Blue" Font-Underline="true" Text='<%# Bind("Flow_FormTemplateName") %>'
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("Flow_ID") %>' CommandName="view">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="150px" />
                    <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Flow_Status" HeaderText="状态">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Flow_CreatedBy" HeaderText="创建者">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Flow_CreatedDate" HeaderText="创建日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkedit" Text="<u>编辑</u>" ForeColor="Blue" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("Flow_ID") %>' CommandName="myEdit" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkdelete" Text="<u>删除</u>" ForeColor="Blue" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("Flow_ID") %>' CommandName="Delete" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkstep" Text="步骤设置" ForeColor="Blue" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("Flow_ID") %>' CommandName="step" />
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkFormDesign" Text="表单设置" ForeColor="Blue" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("Flow_ID") %>' CommandName="formDesign" />
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
