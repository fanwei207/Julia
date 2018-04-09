<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_defect.aspx.cs" Inherits="QC_qc_defect" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 522px">
            <tr>
                <td>
                    检验模块:<asp:DropDownList ID="dropModule" runat="server" DataTextField="pName" DataValueField="pID"
                        Width="110px" AutoPostBack="True" OnSelectedIndexChanged="dropModule_SelectedIndexChanged"
                        Enabled="False">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    名称:<asp:TextBox ID="txtName" runat="server" CssClass="SmallTextBox" Width="116px"></asp:TextBox>类别:<asp:DropDownList
                        ID="dropType" runat="server" Width="90px" DataTextField="typeName" DataValueField="typeID"
                        AutoPostBack="True" OnSelectedIndexChanged="dropType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" TabIndex="0" Text="查询"
                        OnClick="btnQuery_Click" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" TabIndex="0" Text="增加"
                        OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvDefect" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            OnRowCancelingEdit="gvDefect_RowCancelingEdit" OnRowDeleting="gvDefect_RowDeleting"
            OnRowUpdating="gvDefect_RowUpdating" OnRowDataBound="gvDefect_RowDataBound" DataKeyNames="defID,tID,defName"
            OnRowEditing="gvDefect_RowEditing" AllowPaging="True" OnPageIndexChanging="gvDefect_PageIndexChanging"
            PageSize="20">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt" runat="server" Text='<%# Bind("defName") %>' Width="130px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemTemplate>
                        <%#Eval("defName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类别">
                    <EditItemTemplate>
                        <asp:DropDownList ID="dType" runat="server" Width="90px" DataTextField="typeName"
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
                <asp:HyperLinkField HeaderText="添加子项目" Text="添加/查看" DataNavigateUrlFields="defName,defID,pID"
                    DataNavigateUrlFormatString="qc_defect_item.aspx?def={0}&amp;ID={1}&amp;pID={2}">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:TemplateField HeaderText="排序">
                    <ItemTemplate>
                        <%#Eval("orderBy")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox" Width="45px" Text='<%# Bind("orderBy") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
