<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_DocList.aspx.cs" Inherits="RDW_RDW_DocList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
               <table cellspacing="2" cellpadding="2" width="1000px" bgcolor="white" border="0">
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblProject" runat="server" Width="100px" CssClass="LabelRight" Text="Project Name:" Font-Bold="false"></asp:Label>
                        <asp:Label ID="lblProjectData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblProdCode" runat="server" Width="100px" CssClass="LabelRight" Text="Product Code:" Font-Bold="false"></asp:Label>
                        <asp:Label ID="lblProdCodeData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblProdDesc" runat="server" Width="100px" CssClass="LabelRight" Text="Description:" Font-Bold="false"></asp:Label>
                        <asp:Label ID="lblProdDescData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblStartDate" runat="server" Width="100px" CssClass="LabelRight" Text="Start Date:" Font-Bold="false"></asp:Label>
                             <asp:Label ID="lblStartDateData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                        <asp:Label ID="lblEndDate" runat="server" Width="100px" CssClass="LabelRight" Text="End Date:" Font-Bold="false"></asp:Label>
                        <asp:Label ID="lblEndDateData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                    
                    </td>
                    <td align="right">
                        <asp:Button ID="btnClose" runat="server" CssClass="SmallButton3" Text="Close" Width="50px" OnClick="btnClose_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7" valign="top" align="center">
                        <asp:GridView ID="gvUpload" runat="server" AllowPaging="False" Visible="false"
                            AllowSorting="True" AutoGenerateColumns="False" Width="1000px"
                            CssClass="GridViewStyle" PageSize="20"
                             OnRowCommand="gvUpload_RowCommand" 
                            DataKeyNames="RDW_DetID,RDW_PhysicalName,RDW_DocsID, RDW_UploaderID,RDW_Path" 
                            onrowdatabound="gvUpload_RowDataBound">
                            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <EmptyDataTemplate>
                                <asp:Table ID="Table2" Width="1000px" CellPadding="-1" CellSpacing="0" runat="server" CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                    <asp:TableRow>
                                        <asp:TableCell Text="Step Name" Width="350px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="Message List" Width="220px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="Attach File Name" Width="220px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="Upload User" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="Upload Date" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="View" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
                            <Columns>
                              
                                <asp:TemplateField HeaderText="Step">
                                    <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                    <ItemStyle Width="350px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton runat="Server"  Text='<%#Eval("RDW_StepName") %>'
                                        Font-Underline="true" ID="lk_StepName" CommandName="StepName"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RDW_FileName" HeaderText="Attach File Name">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RDW_Uploader" HeaderText="Upload User">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RDW_UploadDate" HeaderText="Upload Date">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField Text="View" CommandName="View" ControlStyle-Font-Underline="true">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="7" valign="top" align="center">
                        <asp:GridView ID="gvMessage" runat="server" AllowPaging="False" AllowSorting="True"
                            AutoGenerateColumns="False" Width="1000px" CssClass="GridViewStyle" Visible="false"
                            onrowdatabound="gvMessage_RowDataBound" 
                            onrowcommand="gvMessage_RowCommand"
                             DataKeyNames="RDW_DetID,RDW_MstrID">
                            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <EmptyDataTemplate>
                                <asp:Table ID="Table1" Width="1000px" CellPadding="-1" CellSpacing="0" runat="server"
                                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                    <asp:TableRow>
                                        <asp:TableCell Text="Step Name" Width="350px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="Message List"  HorizontalAlign="center"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Step" >
                                    <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                    <ItemStyle Width="350px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton  Font-Underline="true"  Text='<%#Eval("RDW_StepName") %>'
                                         runat="Server" ID="lk_StepName" CommandName="StepName" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>                            
                                <asp:BoundField DataField="RDW_Message" HeaderText="Message List" HtmlEncode="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="7" valign="top" align="center">
                        <asp:GridView ID="gv_all" runat="server" AllowPaging="False" 
                            AllowSorting="True" AutoGenerateColumns="False" Width="1000px" 
                            CssClass="GridViewStyle" PageSize="20"
                            DataKeyNames="RDW_DetID,RDW_PhysicalName,RDW_DocsID, RDW_UploaderID,RDW_Path,RDW_MstrID" OnRowCommand="gv_all_RowCommand" OnRowDataBound="gv_all_RowDataBound">
                            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <EmptyDataTemplate>
                                <asp:Table ID="Table2" Width="1000px" CellPadding="-1" CellSpacing="0" runat="server" CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                    <asp:TableRow>
                                        <asp:TableCell Text="Step Name" Width="220px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="Message List" Width="350px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="Attach File Name" Width="220px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="Upload User" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="Upload Date" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                        <asp:TableCell Text="View" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
                            <Columns>
                              
                                <asp:TemplateField HeaderText="Step">
                                    <HeaderStyle Width="220px" HorizontalAlign="Center" />
                                    <ItemStyle Width="220px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton runat="Server"  Text='<%#Eval("RDW_StepName") %>'
                                        Font-Underline="true" ID="lk_StepName" CommandName="StepName"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Message List">
                                    <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                    <ItemStyle Width="350px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label runat="Server" Text='<%#Eval("RDW_Message") %>'  ID="lbl_message"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RDW_FileName" HeaderText="Attach File Name">
                                    <HeaderStyle Width="220px" HorizontalAlign="Center" />
                                    <ItemStyle Width="220px"  HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RDW_Uploader" HeaderText="Upload User">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RDW_UploadDate" HeaderText="Upload Date">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField Text="View" CommandName="View" ControlStyle-Font-Underline="true">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </form>
    </div>

    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>

</body>
</html>
