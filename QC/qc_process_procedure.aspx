<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_process_procedure.aspx.cs"
    Inherits="QC_qc_process_procedure" %>

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
        <table cellspacing="0" cellpadding="0" width="600px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td class="style1">
                    名称:<asp:TextBox ID="txtName" runat="server" CssClass="SmallTextBox" Width="116px"></asp:TextBox>
                    类别:<asp:DropDownList ID="dropType" runat="server" DataTextField="typeName" DataValueField="typeID"
                        Width="104px">
                    </asp:DropDownList>
                </td>
                <td style="height: 28px" align="right">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="查询" />
                    &nbsp;
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" TabIndex="0" Text="增加"
                        OnClick="btnAdd_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvProcedure" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            DataKeyNames="prcdID,typeID,prcdName" OnRowCancelingEdit="gvProcedure_RowCancelingEdit"
            OnRowDataBound="gvProcedure_RowDataBound" OnRowDeleting="gvProcedure_RowDeleting"
            OnRowUpdating="gvProcedure_RowUpdating" OnRowEditing="gvProcedure_RowEditing"
            AllowPaging="True" OnPageIndexChanging="gvProcedure_PageIndexChanging" PageSize="20"
            Width="600px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工序名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtProcedure" runat="server" Text='<%# Bind("prcdName") %>' Width="102px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemTemplate>
                        <%#Eval("prcdName")%>
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
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="prcdName,prcdID,pID" DataNavigateUrlFormatString="qc_defect_item.aspx?def={0}&amp;ID={1}&amp;pID={2}"
                    HeaderText="缺陷维护" Text="添加/查看">
                    <ControlStyle Font-Bold="False" Font-Names="8px" Font-Underline="True" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:TemplateField HeaderText="排序">
                    <ItemTemplate>
                        <%#Eval("orderBy")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox" Width="47px" Text='<%# Bind("orderBy") %>'></asp:TextBox>
                    </EditItemTemplate>
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
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
