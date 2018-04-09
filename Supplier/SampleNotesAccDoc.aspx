<%@ Page Language="C#" AutoEventWireup="true"   CodeFile="SampleNotesAccDoc.aspx.cs" Inherits="supplier_SampleReceiveNotesAccDoc" %>

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
        <table style="text-align: center; width: 950px;" cellspacing="0">
            <tr style="background-color: Aqua;">
                <td align="left">
                    &nbsp; 单据号：<asp:TextBox ID="txt_bosnbr" runat="server" Width="103px" ReadOnly="true"></asp:TextBox>
                    行号：<asp:Label ID="lbl_line" runat="server" Text="" Width="30px"></asp:Label>
                    部件号：<asp:Label ID="lbl_bosCode" runat="server" Text="Label" Width="192px"></asp:Label>
                    <asp:Label ID="lbl_bosQad" runat="server" Text=" "></asp:Label>
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:Button ID="btn_Back" runat="server" Text="返回" CssClass="SmallButton2" Width="52px"
                        OnClick="btn_Back_Click" Height="22px" />
                    <asp:CheckBox ID="chk_EditPermisson" runat="server" Visible="false" />
                    <asp:CheckBox ID="chk_isVendConfirm" runat="server" Visible="false" />
                </td>
            </tr>
            <tr>
                <td align="left" valign="bottom" style="height: 28px;" colspan="2">
                    <hr />
                    <asp:Label ID="Label2" runat="server" Text="已与部件QAD关联的文档" ForeColor="#0000ff"></asp:Label>
                </td>
            </tr>
            <tr id="tr_gv_bos_Qaddoc" runat="server" visible="false">
                <td colspan="2" align="left">
                    <asp:GridView ID="gv_bos_Qaddoc" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" 
                        OnPaginxChanging="gv_bos_Qaddoc_PageIndexChanging" OnRowCommand="gv_bos_Qaddoc_RowCommand "
                        OnRowDataBound="gv_bos_Qaddoc_RowDataBound" PageSize="5" 
                        DataKeyNames="id,cateid,typeid,filePath,fileName,isNewMechanism,docLevel,accFileName" 
                        Width="950px">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="typeid" HeaderText="cateid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="cateid" HeaderText="typeid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="filepath" HeaderText="docid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="typename" HeaderText="Category">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="catename" HeaderText="Type">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="name" HeaderText="DocName">
                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                <ItemStyle HorizontalAlign="Center" Width="250px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="File Name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkFileName" runat="server" CommandName="OpenPdfFile" Font-Underline="True"
                                        CommandArgument='<%# (Container.DataItemIndex )%>' Text='<%# Bind("filename") %>'
                                        ForeColor="Black">View</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="version" HeaderText="Ver">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Design File">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkAccFileName" runat="server" CommandName="myView" Font-Underline="True"
                                        CommandArgument='<%# (Container.DataItemIndex )%>'
                                        ForeColor="Black">View</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table id="tbID_DocAdd" runat="server" style="text-align: center; width: 950px;">
            <tr style="list-style-type: none; border-style: solid none none none; border-top-width: thin;
                border-top-color: #000000">
                <td align="left">
                    <hr />
                    <asp:Label ID="Label3" runat="server" Text="请选择建立关档关联" ForeColor="#0000ff"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    Category<asp:DropDownList ID="ddl_Type" runat="server" Width="180px" AutoPostBack="True"
                        DataTextField="typename" DataValueField="typeid" OnSelectedIndexChanged="ddl_Type_SelectedIndexChanged">
                    </asp:DropDownList>
                    Type<asp:DropDownList ID="ddl_Category" runat="server" Width="180px" AutoPostBack="True"
                        DataTextField="catename" DataValueField="cateid" OnSelectedIndexChanged="ddl_Category_SelectedIndexChanged">
                    </asp:DropDownList>
                    Key Word<asp:TextBox ID="txt_KeyWordSearch" runat="server" CssClass="SmallTextBox"
                        Width="200px"></asp:TextBox>(*)&nbsp;&nbsp;
                    <asp:Button ID="btn_DocSearch" runat="server" CssClass="SmallButton2" Text="查询" Width="40px"
                        OnClick="btn_DocSearch_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:GridView ID="gv_allDoc" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_allDoc_PageIndexChanging" OnRowCommand="gv_allDoc_RowCommand"
                        OnRowDataBound="gv_allDoc_RowDataBound" PageSize="4" DataKeyNames="id,accFileName,typeid,cateid"
                        Width="872px">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="typeid" HeaderText="cateid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="cateid" HeaderText="typeid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="id" HeaderText="docid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="filepath" HeaderText="FilePath" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="typename" HeaderText="Category">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="catename" HeaderText="Type">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="name" HeaderText="DocName">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fileName" HeaderText="FileName">
                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="version" HeaderText="Ver">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:ButtonField Text="增加关联" HeaderText="" CommandName="myAddlink" ControlStyle-Font-Underline="true">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="false" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table style="text-align: center; width: 950px;">
            <tr>
                <td align="left" valign="bottom" style="height: 28px;">
                    <hr />
                    <asp:Label ID="Label1" runat="server" Text="已建关联的文档" ForeColor="#0000ff"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:GridView ID="gv_relateDoc" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_relateDoc_PageIndexChanging"
                        OnRowDeleting="gv_relateDoc_RowDeleting" OnRowDataBound="gv_relateDoc_RowDataBound"
                        OnRowCommand="gv_relateDoc_RowCommand" PageSize="15" DataKeyNames="ID,bos_docId,bos_typeID,bos_cateID,bos_filePath,bos_fileName,bos_docIsNewMechanism,bos_accFileName"
                        Width="950px" EnableModelValidation="True">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="950px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="Category" Width="150px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="Type" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="DocName" Width="190px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="FileName" Width="160px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" " Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" " Width="40px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有关联文档的记录" ColumnSpan="6"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="bos_typeID" HeaderText="cateid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="bos_cateID" HeaderText="typeid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="bos_docId" HeaderText="docid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="bos_catename" HeaderText="Category">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_typename" HeaderText="Type">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_docName" HeaderText="DocName">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="File Name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkFileName" runat="server" CommandName="OpenPdfFile" Font-Underline="True"
                                        CommandArgument='<%# (Container.DataItemIndex )%>' Text='<%# Bind("bos_fileName") %>'
                                        ForeColor="Black">View</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="bos_docVersion" HeaderText="Ver">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Design File">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkView" runat="server" CommandName="myView" Font-Underline="True"
                                        CommandArgument='<%# Container.DataItemIndex %>' ForeColor="Black">View</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="删除">
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                <ControlStyle ForeColor="Black" Font-Underline="True" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="text-align: center; width: 950px;">
            <tr>
                <td align="left" valign="bottom" style="height: 28px;">
                    <hr />
                    <asp:Label ID="Label4" runat="server" Text="供应商上传的文档" ForeColor="#0000ff"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        CssClass="GridViewStyle" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                        CellPadding="1" DataKeyNames="id, bos_det_isReceipt" GridLines="Vertical" OnPageIndexChanging="gvlist_PageIndexChanging"
                        Height="20px" PageSize="5" Width="950px" OnRowCommand="gvlist_RowCommand" 
                        onrowdatabound="gvlist_RowDataBound">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="bos_docTypeName" HeaderText="文件类型">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_docfileName" HeaderText="文件名称">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_docUploadby" HeaderText="上传者">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_docUploadDate" HeaderText="上传时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkstep" runat="server" CommandArgument='<%# Eval("bos_vend") + "," + Eval("bos_docId")+ "," + Eval("bos_path") + "," + Eval("bos_transferStatus") %>'
                                        CommandName="download" Font-Size="12px" Font-Underline="true" ForeColor="Black"
                                        Text="View"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="bos_transferStatus" HeaderText="转移状态">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkTransfer" runat="server" CommandName="Transfer" Font-Underline="true"
                                        CommandArgument='<%# Eval("Id") + "," + Eval("bos_transferStatus") %>' ForeColor="Black"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="bos_docfileDescs" HeaderText="文件描述">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
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