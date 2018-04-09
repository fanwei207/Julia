<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiPoNbrImport.aspx.cs" Inherits="EDI_EdiPoNbrImport" %>

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
        
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <div style="width: 820px; margin: 20px auto; padding-top: 2px;
            padding-bottom: 2px;">
            <fieldset style="width: 790px;">
                <legend style="padding-left: 2px;"><b>File Import</b></legend>
                <table cellpadding="6" cellspacing="0" style="width: 778px;" border="0">
                    <tr>
                        <td style="height: 5; width: 219px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 219px">
                            File:
                        </td>
                        <td valign="top" colspan="2" style="width: 826px">
                            <input id="filename1" style="width: 563px; height: 22px" type="file" name="filename1"
                                runat="server" />
                            &nbsp;&nbsp;<asp:Button ID="btnImport" runat="server" 
                            Text="Import" CssClass="SmallButton2" onclick="btnImport_Click" />
&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 20px;">
                            Download:
                        </td>
                        <td style="height: 20px; text-align: left;">
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="11px"
                                Font-Underline="True" NavigateUrl="~/docs/POReplace.xls"  
                                Target="_blank">Replace Template</asp:HyperLink>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="False" Font-Size="11px"
                                Font-Underline="True" NavigateUrl="~/docs/POCancel.xls"  
                                Target="_blank">Cancel Template</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            Import Type:</td>
                        <td align="left">
                            <asp:RadioButtonList ID="radioType" runat="server" RepeatDirection="Horizontal" 
                                CellSpacing="10">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="width: 790px;">
            <legend style="padding-left: 2px;"><b>File Info Show</b></legend>
            <table>
            <tr>
            <td>
            <asp:Label ID="lblTips" runat="server" Text="Tips:Make sure details and click Confirm button !"></asp:Label>
           
            </td>
            <td align="right" style="width:330px">
            <asp:Button ID="btnSure" runat="server" 
                            Text="Confirm" CssClass="SmallButton2" onclick="btnSure_Click" />
            </td>
            </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="600px"   PageSize="15" AllowPaging="True" 
                    onpageindexchanging="gv_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table3" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="PoNbr" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="PoNbr1" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Date Created" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="CreateBy" Width="150px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
            <asp:BoundField DataField="po_Nbr" HeaderText="PoNbr">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="po_Nbr1" HeaderText="PoNbr1">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="po_createDate" HeaderText="Date Created">
                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                    <ItemStyle Width="250px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="crByName" HeaderText="Ownner">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
            </fieldset>         
        </div>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
