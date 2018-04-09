<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="supplier_docsImport.aspx.cs" Inherits="supplier_supplier_poImport" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload"
    TagPrefix="Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">

    $(function () {

        $("#MainContent_uploadPartBtn").click(function () {
            if ($("#MainContent_dropType").val() == "N") {
                alert("请选择一项 文件类型！");
                event.returnValue = false;
            }
        })
    })

    </script>
    <div>
        <table bgcolor="white" border="0" cellpadding="0" cellspacing="0" width="880" runat="server"
            id="Table1">
            <tr>
                <td align="left" height="20px" colspan="2" valign="middle">
                    <font face="宋体">供应商:<asp:Label ID="lblVend" runat="server" CssClass="LabelCenter"
                        Font-Bold="False"></asp:Label>
                        &nbsp;&nbsp;采购单:<asp:Label ID="lblPo" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>
                        &nbsp; 行号:<asp:Label ID="lblLine" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;&nbsp; 域:<asp:Label ID="lbDomain" runat ="server"></asp:Label>&nbsp;&nbsp;
                        物料:<asp:Label ID="lblPart" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;&nbsp;
                        &nbsp; <span style="font-family: 宋体">描述:</span>
                        <asp:Label ID="lblDesc" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;
                    </font>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" height="20" valign="middle">
                    <font style="color: Red; font-size: 11px;">备注：上传文件必须包含以下2种类型</font><br />
                    1、<font style="color: Red; font-size: 11px;">生产工艺和原材料组成表</font>&nbsp; 2、<font style="color: Red;
                        font-size: 11px;">质量检验报告</font><br />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" height="20" valign="middle">
                </td>
            </tr>
        </table>
         <table bgcolor="white" border="0" cellpadding="0" cellspacing="0" width="880px">
            <tr>
                <td align="left" style="width: 80px">
                    文件类型:
                </td>
                <td align="left" style="width: 80px">
                    <asp:DropDownList ID="dropType" runat="server" Width="150px">
                        <asp:ListItem Value="N">----</asp:ListItem>
                        <asp:ListItem Value="M">工艺、原材料</asp:ListItem>
                        <asp:ListItem Value="Q">质量检验报告</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="left">
                    上传文件:
                </td>
                <td align="left">
                    <upload:inputfile ID="fileAttachFile" runat="server" Width="400px" />
                </td>
                <td style="width: 320px;">
                    <upload:progressbar ID="pbProgressBar" runat='server' Inline="true" Width="300px"
                        Height="40px">
                    </upload:progressbar>
                </td>
            </tr>
            <tr>
                <td align="left">
                    文件描述:
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txt_docsDecs" runat="server" Width="436px" MaxLength="200"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="uploadPartBtn" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="上传" Width="50px" OnClick="uploadPartBtn_ServerClick" />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="btnBack_Click" Text="返回" Width="50px" />
                </td>
            </tr>
        </table>
        <br />
        <table bgcolor="white" border="0" cellpadding="0" cellspacing="0" width="880" style="height: 200px"
            runat="server" id="Table2">
            <tr>
                <td align="left" valign="top" style="height: 30px">
                    <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" DataKeyNames="id"
                        GridLines="Vertical" OnPageIndexChanging="PageChange" Height="20px" PageSize="5"
                        Width="880px" OnRowCommand="gvlist_RowCommand" 
                        onrowdatabound="gvlist_RowDataBound">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="prd_nbr" HeaderText="送货单">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_doc_typeName" HeaderText="文档类型">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_doc_filename" HeaderText="名称">
                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_createName" HeaderText="上传者">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_createDate" HeaderText="上传时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                <ItemStyle HorizontalAlign="Center" Width="110px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkstep" runat="server" CommandArgument='<%# Eval("prd_po_vend") + "," + Eval("prd_doc_id") %>'
                                        CommandName="download" Font-Size="12px" Font-Underline="true" ForeColor="Black"
                                        Text="查看"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkdelete" runat="server" CommandArgument='<%# Eval("id") + "," + Eval("prd_po_vend") + "," + Eval("prd_doc_id") %>'
                                        CommandName="del" Font-Size="12px" Font-Underline="true" ForeColor="Black" Text="<u>删除</u>"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="prd_doc_desc" HeaderText="文件描述">
                                <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                <ItemStyle HorizontalAlign="Left" Width="130px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_transferStatus" HeaderText="转移状态">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkTransfer" runat="server" CommandName="Transfer" Font-Underline="true"
                                        CommandArgument='<%# Eval("Id") + "," + Eval("prd_transferStatus") %>' ForeColor="Black"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</asp:Content>
