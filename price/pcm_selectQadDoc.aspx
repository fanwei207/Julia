<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pcm_selectQadDoc.aspx.vb"
    Inherits="price_pcm_selectQadDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <base target="_self">
        <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div><div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <%--<asp:Button ID="btnReturn" Text="Return" runat="server" CssClass="SmallButton2"  />--%>
    <asp:Label ID="lbPQID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbQAD" runat="server" Visible="false"></asp:Label> 
     <asp:Label ID="lbIMID" runat="server" Visible="false"></asp:Label>
     <asp:Label ID="lbComeFrom" runat="server" Visible="false"></asp:Label>
     </div>
  <asp:DataGrid ID="DgDoc" runat="server" Width="1600px" PageSize="13" AutoGenerateColumns="False"
                CssClass="GridViewStyle">
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
                        <HeaderStyle Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="catename" HeaderText="Type">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="name" HeaderText="DocName">
                        <HeaderStyle Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="filename" HeaderText="FileName">
                        <HeaderStyle Width="220px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="220px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="version" HeaderText="Ver">
                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="level" HeaderText="Lv">
                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="isall" HeaderText="All">
                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="preview" HeaderText="View">
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn Text="&lt;u&gt;List&lt;/u&gt;" DataTextField="old" HeaderText="P.Ver"
                        CommandName="oldview">
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle Width="30px"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:BoundColumn DataField="pictureNo" HeaderText="pictureNo">
                        <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="description" HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn ReadOnly="True"></asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="gsort"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
    </div>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
