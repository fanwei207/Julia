<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forum_mstr.aspx.cs" Inherits="Supplier_Forum_mstr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

//        $(function () {

//            var _index = $("#hidTabIndex").val();

//            var $tabs = $("#divTabs").tabs({ active: _index });

//            //$("#gvMessagereply").find(".GridViewHeaderStyle TH:eq(1)").css({ 'text-align': 'left', 'word-break': 'break-all', 'word-wrap': 'break-word' });
//            //$("#gvMessagereply").find(".GridViewHeaderStyle").hide();

//        })
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 900px; height: 5px;">
            <tr>
            <td>
                <asp:DropDownList  ID="ddlType" runat="server" DataTextField="fst_typeName" DataValueField="fst_typeId" Width="100px">
                </asp:DropDownList>&nbsp;
            </td>
                <td>
                    Keywords:
                    <asp:TextBox ID="txtkeywords" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="250px" />
                    &nbsp;
                    <asp:RadioButtonList ID="ralStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">Open</asp:ListItem>
                        <asp:ListItem Value="2">Closed</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_messageselect" runat="server" Text="Query" CssClass="SmallButton2"
                        OnClick="btn_messageselect_Click" />
                </td>
                <td>
                    <asp:Button ID="btnTypeManage" runat="server"  CssClass="SmallButton2" 
                        Text="TypeManger"  Width="80px" onclick="btnTypeManage_Click"/>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="btn_back" runat="server" Text="BACK" CssClass="SmallButton2" Visible="False"
                        OnClick="btn_back_Click" />
                    <asp:Button ID="btn_reply" runat="server" Text="REPLY" CssClass="SmallButton2" Visible="False"
                        OnClick="btn_reply_Click" />
                    <asp:Button ID="btn_new" runat="server" Text="NEW" CssClass="SmallButton2" OnClick="btn_new_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMessage" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
            AllowPaging="True" Width="920px" OnPageIndexChanging="gv_PageIndexChanging" DataKeyNames="fst_id,fst_filepath,fst_IsClosed,fst_createBy,fst_istop"
            OnRowCommand="gvMessage_RowCommand" PageSize="22" OnRowDataBound="gvMessage_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" Height="30px" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table3" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Owner" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Message" Width="610px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="NO.">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject">
                    <ItemTemplate>
                        <asp:Label ID="lb_istopname" runat="server" Text='<%# Bind("fst_istopname") %>' Style="margin-top: 10px;
                            padding-top: 10px;"></asp:Label>
                        <asp:LinkButton ID="reply" runat="server" Font-Bold="False" Font-Size="12px" CommandName="reply"
                            Font-Underline="True" Text='<%# Bind("fst_desc") %>' Style="padding-left: 5px;"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="600px" />
                    <ItemStyle HorizontalAlign="Left" Width="600px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Author">
                    <ItemTemplate>
                        <asp:Label ID="Labelname" runat="server" Text='<%# Bind("fst_createName") %>' Style="margin-top: 10px;
                            padding-top: 10px;"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="fst_createDate" HeaderText="CreateDate">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fst_newDate" HeaderText="Last Post">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:LinkButton ID="close" runat="server" Font-Bold="False" Font-Size="12px" CommandName="close"
                            Font-Underline="True" Text='<%# Bind("fst_close") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gvMessagereply" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
            AllowPaging="True" Width="900px" DataKeyNames="fst_id,fst_filepath" Visible="False"
            OnRowCommand="gvMessagereply_RowCommand" OnPageIndexChanging="gvMessagereply_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table3" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Owner" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Message" Width="610px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="Author">
                    <ItemTemplate>
                        <asp:Label ID="Labelname" runat="server" Text='<%# Bind("fst_createName") %>' Style="margin-top: 10px;
                            padding-top: 10px;"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        Post At:
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("fst_createDate") %>' Style="margin-top: 10px;
                            padding-top: 10px;"></asp:Label>
                        <hr align="left" style="width: 100%; border-top: 1px dashed #000; border-bottom: 0px dashed #000;
                            height: 0px">
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("fst_desc") %>' Style="word-break: break-all"
                            Width="600px" Font-Size="Medium"></asp:Label>
                        <br />
                        <br />
                        <asp:LinkButton ID="Download1" runat="server" Font-Bold="False" Font-Size="12px"
                            CommandName="DownloadFile" Font-Underline="True" Text='<%# Bind("fst_filename") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Left" Width="700px" />
                    <ItemStyle HorizontalAlign="Left" Width="700px" Height="100px" VerticalAlign="Top"
                        Font-Bold="False" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
