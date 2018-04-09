<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleDeliveryDocImport.aspx.cs" Inherits="SampleDelivery_SampleDeliveryDocImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
</head>
<body>
<div align="center">
     <form id="Form1" method="post" runat="server">
        <table bgcolor="white" border="0" cellpadding="0" cellspacing="0" width="900px">
            <tr>
                <td align="left" colspan="2" valign="middle" style="font-weight: bold;" 
                    class="style1">
                   送样单:<asp:Label ID="lblNbr" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>
                        &nbsp;&nbsp;
                        部件号:<asp:Label ID="lblPart" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;&nbsp;
                        &nbsp; <span style="font-family: 宋体">QAD:</span>
                        <asp:Label ID="lblQad" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;
                        <asp:Label ID="lblMstrId" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblDetId" runat="server" Visible="false"></asp:Label>
                    </font>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" height="20" valign="middle">
                </td>
            </tr>
        </table>
        <table bgcolor="white" border="0" cellpadding="0" cellspacing="0" width="900px">
            <tr>
                <td align="left" style="width: 80px">
                    导入文件:
                </td>
                <td align="left" style="width: 80px">
                    <input id="filename" runat="server" name="filename1" style="width: 468px; height: 22px"  type="file" />(*最大10M)
                </td>
                <td style="width: 420px;">

                </td>
            </tr>
            <tr>
                <td align="left">
                    文件描述:
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txt_docsDecs" runat="server" Width="436px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="uploadPartBtn" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="上传" Width="50px" OnClick="uploadPartBtn_Click" />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="btnBack_Click" Text="返回" Width="50px" />
                </td>
            </tr>
        </table>
        <br />
        <table bgcolor="white" border="0" cellpadding="0" cellspacing="0" width="900px" style="height: 200px"
            runat="server" id="Table2">
            <tr>
                <td align="left" valign="top" style="height: 30px">
                    <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        CellPadding="0" DataKeyNames="id" GridLines="None" OnPageIndexChanging="PageChange"
                        Height="20px" PageSize="5" Width="890px" OnRowCommand="gvlist_RowCommand" CssClass="GridViewStyle"
                        OnRowDataBound="gvlist_RowDataBound">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="900px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="文件名称" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传者" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传时间" Width="60px"></asp:TableCell>                                   
                                    <asp:TableCell HorizontalAlign="center" Text=" " Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" " Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="文件描述"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="无记录" ColumnSpan="6"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="FileName" HeaderText="文件名称">
                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Uploader" HeaderText="上传者">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UploadDate" HeaderText="上传时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkstep" runat="server" CommandArgument='<%#  Eval("VirtualFileName") + "," + Eval("FilePath") %>'
                                        CommandName="download" Font-Size="12px" Font-Underline="true" ForeColor="Black"
                                        Text="查看"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkdelete" runat="server" CommandArgument='<%# Eval("Id") + "," +  Eval("VirtualFileName") %>'
                                        CommandName="del" Font-Size="12px" Font-Underline="true" ForeColor="Black" Text="<u>删除</u>"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="FileDescription" HeaderText="文件描述">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
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
