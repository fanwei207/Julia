<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_ProjectArgue.aspx.cs" Inherits="RDW_RDW_ProjectArgue" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
      <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
      <form id="form1" runat="server">
    <div>
        <div>
            <label>Project Code:</label><asp:Label ID="lbProjectCode" runat="server"></asp:Label>
        &nbsp; &nbsp;   <label>Keywords:</label><asp:TextBox ID="txtKeywords" runat="server" Width="100px" CssClass="SmallTextBox5"></asp:TextBox>
            &nbsp; &nbsp; &nbsp; &nbsp; <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton2" Text="Search"  Width="60px" OnClick="btnSearch_Click"/>
            &nbsp; &nbsp; &nbsp; &nbsp; <asp:Button ID="btnReply" runat="server" CssClass="SmallButton2" Text="Reply"  Width="60px" OnClick="btnReply_Click"/>
            &nbsp; &nbsp; &nbsp; &nbsp; <asp:Button ID="btnReturn" runat="server" CssClass="SmallButton2" Text="Return"  Width="60px" OnClick="btnReturn_Click"/>
        </div>

    <asp:GridView ID="gvMessagereply" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
            AllowPaging="True" Width="900px" DataKeyNames="RDW_mstrID,RDW_argueID,RDW_URL" 
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
                        <asp:Label ID="Labelname" runat="server" Text='<%# Bind("createdEName") %>' Style="margin-top: 10px;
                            padding-top: 10px;"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        Post At:
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("createdDate") %>' Style="margin-top: 10px;
                            padding-top: 10px;"></asp:Label>
                        <hr align="left" style="width: 100%; border-top: 1px dashed #000; border-bottom: 0px dashed #000;
                            height: 0px">
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("RDW_msg") %>' Style="word-break: break-all"
                            Width="600px" Font-Size="Medium"></asp:Label>
                        <br />
                        <br />
                        <asp:LinkButton ID="Download1" runat="server" Font-Bold="False" Font-Size="12px"
                            CommandName="DownloadFile" Font-Underline="True" Text='<%# Bind("RDW_fileName") %>' 
                            CommandArgument ='<%# Bind("RDW_URL") %>'>
                        </asp:LinkButton>
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
  <script language="javascript" type="text/javascript">
      <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
