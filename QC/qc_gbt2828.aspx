<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_gbt2828.aspx.cs" Inherits="QC_qc_gbt2828" %>

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
        <asp:GridView ID="gvGbt2828" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="gvGbt2828_RowCancelingEdit"
            OnRowEditing="gvGbt2828_RowEditing" OnRowUpdating="gvGbt2828_RowUpdating" DataKeyNames="gbtMin,gbtMax"
            OnRowDeleting="gvGbt2828_RowDeleting" OnRowDataBound="gvGbt2828_RowDataBound" CssClass="GridViewStyle">
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
