<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_documentsearch"
    CodeFile="qad_documentsearch.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0">
            <tr class="main_top">
                <td>
                    Schema
                    <asp:DropDownList ID="SelectSchemaDropDown" runat="server" Width="180px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    Category<asp:DropDownList ID="SelectTypeDropDown" runat="server" Width="200px" AutoPostBack="True">
                    </asp:DropDownList>
                    Type<asp:DropDownList ID="SelectCateDropDown" runat="server" Width="200px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_search" runat="server" CssClass="SmallButton3" Text="Search"
                        Width="100px"></asp:Button>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="Install DWG"
                        Width="87px"></asp:Button>
                </td>
            </tr>
            <tr class="main_top">
                <td>
                    Key Word<asp:TextBox ID="txb_search" runat="server" CssClass="SmallTextBox" Width="200px"></asp:TextBox>

                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <span>通知：我们在“<b style="color:Red;">预览</b>”列新增了一个"<b style="color:Red;">在线预览</b>"的功能。如果你仅是查看Word、Excel、PPT文件，而不需要下载文件到本地，可以点击这个“<b style="color:Red;">预览</b>”按钮</span>
        <asp:DataGrid ID="DgDoc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CssClass="GridViewStyle GridViewRebuild" Width="1775px" PageSize="25">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:ButtonColumn Visible="False" Text="&gt;" CommandName="select">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="docid"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="typeid"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="cateid"></asp:BoundColumn>
                <asp:BoundColumn DataField="typename" HeaderText="Category">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="catename" HeaderText="Type">
                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="DocName">
                    <HeaderStyle HorizontalAlign="Center" Width="160px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="160px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="filename" HeaderText="FileName">
                    <HeaderStyle HorizontalAlign="Center" Width="400px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="400px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Level" HeaderText="Lv">
                    <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="version" HeaderText="Ver">
                    <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="isAppr" HeaderText="Appr">
                    <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="isall" HeaderText="All">
                    <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="preview" HeaderText="View">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" Font-Bold="False"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="preview1" HeaderText="预览">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" Font-Bold="False"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="oldview" HeaderText="P.Ver">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" Font-Bold="False"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="assText" HeaderText="Assoc" CommandName="associated_item">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="pictureNo" HeaderText="图号">
                    <HeaderStyle HorizontalAlign="Center" Width="300px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" HeaderText="Description">
                    <HeaderStyle HorizontalAlign="Center" Width="400px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="400px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
