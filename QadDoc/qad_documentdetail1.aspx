<%@ Page Language="vb" AutoEventWireup="true" Inherits="tcpc.qad_documentdetail1"
    CodeFile="qad_documentdetail1.aspx.vb" %>

<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload"
    TagPrefix="Upload" %>
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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <input type="hidden" id="hidOldFileName" name="OldFileName" runat="server" style="width: 63px" />
        <input type="hidden" id="hidOldAccFileName" name="OldAccFileName" runat="server"
            style="width: 63px" />
        <table cellspacing="0" cellpadding="0" width="1040">
            <tr>
                <td align="left" colspan="2">
                    &nbsp;<asp:Label ID="Label1" runat="server" Width="900"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 20px">
                    &nbsp;DocName:<asp:TextBox ID="txbname" runat="server" CssClass="smalltextbox" Width="200px"></asp:TextBox>
                    &nbsp; Doc Ver:<asp:TextBox ID="txbversion" runat="server" CssClass="smalltextbox"
                        Width="40px">1</asp:TextBox>&nbsp;&nbsp; 图号:
                    <asp:TextBox ID="txtPictureNo" runat="server" CssClass="smalltextbox" Width="200px"></asp:TextBox>&nbsp;
                    Doc Level:<asp:DropDownList ID="ddlLevel" Width="40px" runat="server">
                        <asp:ListItem Value="0" Text="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:CheckBox ID="chkIsPublic" runat="server" Text="IsPublic" TextAlign="Left" />
                </td>
                <td align="left" style="height: 20px">
                    <asp:CheckBox ID="chk_noApprove" runat="server" Text="仅未审批的" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="Search" Width="70px">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 20px">
                    Doc Desc:
                    <asp:TextBox ID="txbdesc" Width="694px" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td align="left" style="height: 20px">
                    <asp:CheckBox ID="chkAccFileName" runat="server" Text="修改关联文档" Visible="False" />
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="1040">
            <tr>
                <td align="left" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Doc:<Upload:InputFile ID="fileAttachFileDoc" runat="server" Width="400px" />
                    
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    AccFile:<Upload:InputFile ID="fileAccFileDoc" runat="server" Width="400px" />
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="chkall" runat="server" Text="For All Items" TextAlign="Left"></asp:CheckBox>
                    <asp:Button ID="btnadd" runat="server" CssClass="SmallButton3" Text="Add"></asp:Button>
                    <asp:Button ID="Btnedit" runat="server" CssClass="SmallButton3" Text="Modify" Width="70">
                    </asp:Button>
                    <asp:Button ID="Butcancel" runat="server" CssClass="SmallButton3" Text="Cancel" Width="70">
                    </asp:Button>
                    <asp:Button ID="btnback" runat="server" CssClass="SmallButton3" Text="Back"></asp:Button>
                    <asp:Button ID="BtnExport" runat="server" CssClass="SmallButton3" Text="Export" Width="60px">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="datagrid1" runat="server" Width="1810px" AutoGenerateColumns="False"
            HeaderStyle-Font-Bold="false" PagerStyle-HorizontalAlign="Center" AllowPaging="True"
            PagerStyle-Mode="NumericPages" PagerStyle-BackColor="#99ffff" PagerStyle-Font-Size="12pt"
            PagerStyle-ForeColor="#0033ff" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="DocName">
                    <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="filename" HeaderText="FileName">
                    <HeaderStyle HorizontalAlign="Center" Width="350px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="350px"></ItemStyle>
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
                    <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="isall" HeaderText="All">
                    <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="isPublic" HeaderText="Public">
                    <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;Edit&lt;/u&gt;" CommandName="Select">
                    <HeaderStyle HorizontalAlign="Center" Font-Size="8pt" Font-Names="Tahoma,Arial" Width="25px">
                    </HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="True" HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;Del&lt;/u&gt;" CommandName="DeleteClick">
                    <HeaderStyle HorizontalAlign="Center" Font-Size="8pt" Font-Names="Tahoma,Arial" Width="25px">
                    </HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="True" HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="preview" HeaderText="View">
                    <HeaderStyle HorizontalAlign="Center" Width="25px" Font-Bold="False"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="oldview" HeaderText="P.Ver">
                    <%--第11列--%>
                    <HeaderStyle HorizontalAlign="Center" Width="20px" Font-Bold="False"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="assText" HeaderText="Assoc" CommandName="associated_item">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn DataTextField="vendText" HeaderText="vend" CommandName="associated_vend">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;Submit&lt;/u&gt;" CommandName="checkedBy" HeaderText="Submit">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" Font-Bold="False"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="pictureNo" HeaderText="图号">
                    <%--第15列--%>
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" HeaderText="Description">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="verifycnt"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="creator"></asp:BoundColumn>
                <%--第18列----%>
                <asp:BoundColumn Visible="False" DataField="filename1"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="accFileName"></asp:BoundColumn>
                <%--第20列----%>
                <asp:BoundColumn DataField="createdname" HeaderText="上载者">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createdDate" HeaderText="上载日期">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="modifiedby"></asp:BoundColumn>
                <%--第23列----%>
                <asp:BoundColumn DataField="modifiedname" HeaderText="修改者">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="modifiedDate" HeaderText="修改日期">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
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
