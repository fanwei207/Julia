<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="SampleNotesSupplierDocsImport.aspx.cs" Inherits="supplier_SampleNotesSupplierDocsImport" %>

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
        <table bgcolor="white" border="0" cellpadding="0" cellspacing="0" width="900px">
            <tr>
                <td align="left" height="25px" colspan="2" valign="middle" style="font-weight: bold;">
                    <font face="宋体">供应商:<asp:Label ID="lblVend" runat="server" CssClass="LabelCenter"
                        Font-Bold="False"></asp:Label>
                        &nbsp;&nbsp;打样单:<asp:Label ID="lblBosNbr" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>
                        &nbsp; 行号:<asp:Label ID="lblLine" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;&nbsp;
                        部件号:<asp:Label ID="lblPart" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;&nbsp;
                        &nbsp; <span style="font-family: 宋体">QAD:</span>
                        <asp:Label ID="lblQad" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;
                    </font>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" height="20" valign="middle">
                    <font style="color: Red; font-size: 11px;">备注：上传文件必须包含以下2种类型，确少任一类型文件都会导致无法打印送货单！</font><br />
                    1、<font style="color: Red; font-size: 11px;">生产工艺和原材料组成表</font>&nbsp; 2、<font style="color: Red;
                        font-size: 11px;">质量检验报告</font><br />
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
                    文件类型:
                </td>
                <td align="left" colspan="2">
                    <asp:DropDownList ID="dropType" runat="server" Width="150px">
                        <asp:ListItem Value="N">----</asp:ListItem>
                        <asp:ListItem Value="M">工艺、原材料</asp:ListItem>
                        <asp:ListItem Value="Q">质量检验报告</asp:ListItem>
                    </asp:DropDownList>
                    (*最大100M)</td>
            </tr>
            <tr>
                <td align="left">
                    导入文件:
                </td>
                <td align="left">
                    <Upload:InputFile ID="fileAttachFile" runat="server" Width="400px" />
                </td>
                <td style="width: 420px;">
                    <Upload:ProgressBar ID="pbProgressBar" runat='server' Inline="true" Width="400px"
                        Height="40px">
                    </Upload:ProgressBar>
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
                        <Columns>
                            <asp:BoundField DataField="bos_docTypeName" HeaderText="文件类型">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_docfileName" HeaderText="文件名称">
                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
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
                                    <asp:LinkButton ID="linkstep" runat="server" CommandArgument='<%# Eval("bos_vend") + "," + Eval("bos_docId") %>'
                                        CommandName="download" Font-Size="12px" Font-Underline="true" ForeColor="Black"
                                        Text="查看"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkdelete" runat="server" CommandArgument='<%# Eval("id") + "," + Eval("bos_vend") + "," + Eval("bos_docId") %>'
                                        CommandName="del" Font-Size="12px" Font-Underline="true" ForeColor="Black" Text="<u>删除</u>"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="bos_docfileDescs" HeaderText="文件描述">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
