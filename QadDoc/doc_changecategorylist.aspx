<%@ Page Language="C#" AutoEventWireup="true" CodeFile="doc_changecategorylist.aspx.cs" Inherits="QadDoc_doc_changecategorylist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
                    Category<asp:DropDownList 
                        ID="SelectTypeDropDown" runat="server" Width="200px" AutoPostBack="True" 
                        onselectedindexchanged="SelectTypeDropDown_SelectedIndexChanged">
                    </asp:DropDownList>
                    Type<asp:DropDownList ID="SelectCateDropDown" runat="server" Width="200px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="SelectCateDropDown_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    Key Word<asp:TextBox ID="txb_search" runat="server" CssClass="SmallTextBox" Width="200px"></asp:TextBox>
                    <asp:Button ID="btn_search" runat="server" CssClass="SmallButton3" Width="60px" 
                        Text="Search" onclick="btn_search_Click">
                    </asp:Button>
                    &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="SmallButton3" 
                        Text="Cancel" Width="60"
                        Visible="false" onclick="btncancel_Click"></asp:Button>
                </td>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="Install DWG"
                        Width="87px" onclick="Button1_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DgDoc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CssClass="GridViewStyle GridViewRebuild" Width="1745px" PageSize="25"
            onitemcommand="DgDoc_ItemCommand" onpageindexchanged="DgDoc_PageIndexChanged">
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
                <asp:TemplateColumn HeaderText="Edit">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="myEdit"
                            Text="&lt;u&gt;Edit&lt;/u&gt;"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                </asp:TemplateColumn>
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
                <asp:BoundColumn Visible="False" DataField="accFileName"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
