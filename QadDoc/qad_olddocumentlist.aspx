<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_olddocumentlist"
    CodeFile="qad_olddocumentlist.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .GridViewStyle
        {}
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="600px" align="center" style="margin-top: 2px;"
            border="0">
            <tr class="main_top">
                <td class="main_left">
                </td>
                <td style="height: 6px" width="420">
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
                <td style="height: 6px" align="right" width="60">
                    <input class="smallButton3" id="btnback" onclick="window.close();" type="button"
                        value="Close" name="btnback" runat="server">
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
            Width="600px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="version" HeaderText="Version">
                    <HeaderStyle HorizontalAlign="Center"  Width="30px"></HeaderStyle>
                     <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="filepath"></asp:BoundColumn>
                <asp:BoundColumn DataField="filename" HeaderText="FileName">
                    <HeaderStyle HorizontalAlign="Center" Width="270px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="270px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="View" HeaderText="View">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                  <asp:BoundColumn DataField="createdname" HeaderText="创建人">
                    <HeaderStyle HorizontalAlign="Center"  Width="50px" ></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"  Width="50px" ></ItemStyle>
                </asp:BoundColumn>
                  <asp:BoundColumn DataField="createdDate" HeaderText="创建日期" >
                    <HeaderStyle HorizontalAlign="Center"  Width="70px" ></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"  Width="70px"></ItemStyle>
                </asp:BoundColumn>
                  <asp:BoundColumn DataField="upgradeName" HeaderText="升级人">
                    <HeaderStyle HorizontalAlign="Center"  Width="50px" ></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px" ></ItemStyle>
                </asp:BoundColumn>
                  <asp:BoundColumn DataField="upgradeDate" HeaderText="升级日期">
                    <HeaderStyle HorizontalAlign="Center"  Width="70px" ></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px" ></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
