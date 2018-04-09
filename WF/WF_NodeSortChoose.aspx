<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_NodeSortChoose.aspx.cs"
    Inherits="WF_NodeSort" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function post() {
            window.opener.document.getElementById("txtNodeSort").value = document.getElementById("txtChooseValue").value;
            window.opener.document.getElementById("txtNodeSortValue").value = document.getElementById("txtChooseValue").value;
            window.close();
        }		 	
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 282px">
            <tr>
                <td colspan="2" style="height: 23px" align="left">
                    请选择序号:(每个流程模板第一步骤必须为发起人) &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSort" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="280px" PageSize="15" DataKeyNames="Sort_ID" OnPageIndexChanging="gvSort_PageIndexChanging"
            OnRowDataBound="gvSort_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <asp:RadioButton ID="rbChoose" runat="server" GroupName="NodeSort" OnCheckedChanged="rbChoose_CheckedChanged"
                            AutoPostBack="True" />
                    </ItemTemplate>
                    <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="50px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Sort_Order" HeaderText="序号">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Sort_Name" HeaderText="序号名称">
                    <HeaderStyle Width="180px" HorizontalAlign="Center" />
                    <ItemStyle Width="180px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <br />
        <table cellspacing="2" cellpadding="2" width="325" bgcolor="white" border="0">
            <tr>
                <td colspan="2" style="height: 23px" align="center">
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="增加" OnClientClick="post()" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton3" TabIndex="0" Text="取消"
                        OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
        <asp:TextBox ID="txtChooseValue" runat="server" Visible="True" Style="display: none"></asp:TextBox>
    </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
