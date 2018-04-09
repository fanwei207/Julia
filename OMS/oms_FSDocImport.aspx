<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oms_FSDocImport.aspx.cs"
    Inherits="oms_FSDocImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
        <table cellpadding="0" cellspacing="0" width="900" style="background-color: White;
            text-align: center;" border="0">

            <tr>
                <td style="height: 5">
                    &nbsp;</td>
            </tr>
             <tr>
                <td style="width:200px" align="left">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                     Customer :
                </td>
                <td align="left"style="width:500px" >
                    <asp:Label ID="lblCustCode" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCust" runat="server" Text=""></asp:Label>
                    </td>
            </tr>
             <tr>
                <td style="height: 5">
                </td>
            </tr>
             <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td valign="top" style=" text-align:left;" colspan="3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Importance:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <asp:DropDownList ID="ddlImportance" runat="server" Width="120px">
                        <asp:ListItem Value="-1">--All--</asp:ListItem>
                        <asp:ListItem Value="0">Normal</asp:ListItem>
                        <asp:ListItem Value="1">Emergency</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Catagory:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="ddlCatagory" runat="server" Height="20px" Width="120px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td align="left" style="">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Upload File: &nbsp;
                </td>
                <td valign="top" style="width: 600; text-align:left;" colspan="2">
                    <input id="filename1" style="width: 450px; height: 23px" type="file" size="45" name="filename1"
                        runat="server" />
                    <asp:Button ID="btnUpLoad" runat="server" CssClass="SmallButton2" Text="Upload" Width="68px"
                        OnClick="btnUpLoad_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <br>
                </td>
            </tr>
            <tr>
                <td style="height: 10">
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Filename:
                </td>
                <td valign="top" style="width: 600; text-align:left;" colspan="2">
                <asp:TextBox ID="txtFilename" runat="server" Height="20px" Width="300px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" 
                        onclick="btnSearch_Click" Text="Search" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton3" 
                        onclick="btnBack_Click" Text="Back" />
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50; height: 21px;">
                    &nbsp;
                </td>
                <td align="left" style="width: 135; height: 21px;">
                    &nbsp;
                </td>
                <td align="center">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvFactoryStatus" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="800px" DataKeyNames="fsd_id" OnRowCommand="gvFactoryStatus_RowCommand"
            OnPageIndexChanging="gvFactoryStatus_PageIndexChanging" PageSize="15">
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
                        <asp:TableCell Text="Category" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="File Name" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Owner" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Date Created" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Factory" Width="150px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
            <asp:BoundField DataField="fsd_impt" HeaderText="Importance">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fsc_type" HeaderText="Category">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fsd_filename" HeaderText="File Name">
                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="fsd_domain" HeaderText="Factory">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fsd_uploadName" HeaderText="Owner">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fsd_uploadDate" HeaderText="Date Created">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="" Visible="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDownload" runat="server" Font-Bold="False" Font-Size="12px"
                            CommandName="Download" Font-Underline="True" Text="Download"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" runat="server" Font-Bold="False" Font-Size="12px"
                            CommandName="DeleteFile" Font-Underline="True" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
