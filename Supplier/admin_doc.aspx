<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_doc.aspx.cs" Inherits="admin_doc" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <table id="table1" border="0" style="height: 10px; width: 928px;" cellpadding="0"
            cellspacing="0">
            <tr runat="server" id="trCmd" align="left" style="vertical-align: top; height: 10px;">
                <td align="left" style="width: 80%; text-align: left; height: 10px">
                    <input id="File1" runat="server" style="width: 100%" type="file" />
                </td>
                <td style="width: 20%" align="left">
                    &nbsp;
                    <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton2" Text="上传" Width="48px"
                        OnClick="btnUpload_Click" /><asp:CheckBox ID="chk" runat="server" Visible="False" />
                </td>
            </tr>
             <tr>
                <td colspan="2" style="font-size: 12px; text-align: left;">
                   
                    <asp:Label ID="lblformat" runat="server" Text=""></asp:Label>
                </td>
                
            </tr>
           
            <tr style="vertical-align: top; height: 20px;">
                <td colspan="2" style="width: 928px; text-align: center;">
                    <asp:GridView ID="dg" runat="server" Style="vertical-align: top" CssClass="GridViewStyle"
                        AllowPaging="True" AutoGenerateColumns="False" PageSize="14" OnRowDataBound="dg_RowDataBound"
                        OnPageIndexChanging="dg_PageIndexChanging" 
                        DataKeyNames="doc_id,doc_name,doc_path" OnRowDeleting="dg_RowDeleting"
                        OnRowCommand="dg_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="文件名" DataField="doc_name">
                                <HeaderStyle Width="500px" />
                                <ItemStyle HorizontalAlign="Left" Width="400px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="上传人" DataField="doc_crt_user">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataFormatString="{0:yyyy-MM-dd}" HeaderText="上传日期" HtmlEncode="False"
                                DataField="doc_crt_date">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="删除">
                                <HeaderStyle Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                <ControlStyle ForeColor="Black" Font-Underline="True" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="下载">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="DownLoad" Font-Underline="True"
                                        ForeColor="Black">下载</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
