<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_project.aspx.cs" Inherits="QC_qc_project" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        <table cellspacing="0" cellpadding="0" style="width: 872px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td style="width: 811px">
                    项目名称: &nbsp; &nbsp;
                    <asp:TextBox ID="txtPro" runat="server" Width="147px" CssClass="smalltextbox"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        Text="查询" />
                </td>
                <td style="width: 45px">
                    <asp:Button ID="btnAddNew" runat="server" CssClass="SmallButton3" OnClick="btnAddNew_Click"
                        Text="增加" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="False" DataKeyNames="proID,isGB,tID"
            OnRowDataBound="gvProject_RowDataBound" OnRowDeleting="gvProject_RowDeleting"
            Width="872px" AllowPaging="True" OnPageIndexChanging="gvProject_PageIndexChanging"
            OnRowCancelingEdit="gvProject_RowCancelingEdit" OnRowEditing="gvProject_RowEditing"
            OnRowUpdating="gvProject_RowUpdating" PageSize="17" OnRowCommand="gvProject_RowCommand"
            CssClass="GridViewStyle AutoPageSize">
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle Width="15px" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="项目名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPro" runat="server" Text='<%# Bind("proName") %>' CssClass="smalltextbox"
                            Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle Width="250px" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("proName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类别">
                    <EditItemTemplate>
                        <asp:DropDownList ID="dType" runat="server" Width="100%" DataTextField="typeName"
                            DataValueField="typeID">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="dType" runat="server" Width="90px" DataTextField="typeName"
                            DataValueField="typeID">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle Width="90px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="样本量(Min)">
                    <ItemTemplate>
                        <%#Eval("proMin")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMin" runat="server" Text='<%# Bind("proMin") %>' Width="100%"
                            CssClass="smalltextbox Numberic"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="样本量(Max)">
                    <ItemTemplate>
                        <%#Eval("proMax")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMax" runat="server" Text='<%# Bind("proMax") %>' Width="100%"
                            CssClass="smalltextbox Numberic"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="检验数量">
                    <ItemTemplate>
                        <%#Eval("proNum")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNum" runat="server" Text='<%# Bind("proNum") %>' Width="100%"
                            CssClass="smalltextbox Numberic"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ac">
                    <ItemTemplate>
                        <%#Eval("proAc")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAc" runat="server" Text='<%# Bind("proAc") %>' Width="100%" CssClass="smalltextbox Numberic"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Re">
                    <ItemTemplate>
                        <%#Eval("proRe")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRe" runat="server" Text='<%# Bind("proRe") %>' Width="100%" CssClass="smalltextbox Numberic"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="myEdit">编辑子项目</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="取消" DeleteText="删除" EditText="编辑"
                    UpdateText="更新">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField DeleteText="<u>删除</u>" ShowDeleteButton="True">
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
