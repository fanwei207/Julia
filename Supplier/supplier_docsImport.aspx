<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="supplier_docsImport.aspx.cs" Inherits="supplier_supplier_poImport" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload"
    TagPrefix="Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">

    $(function () {

        $("#MainContent_uploadPartBtn").click(function () {
            if ($("#MainContent_dropType").val() == "N") {
                alert("��ѡ��һ�� �ļ����ͣ�");
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
                    <font face="����">��Ӧ��:<asp:Label ID="lblVend" runat="server" CssClass="LabelCenter"
                        Font-Bold="False"></asp:Label>
                        &nbsp;&nbsp;�ɹ���:<asp:Label ID="lblPo" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>
                        &nbsp; �к�:<asp:Label ID="lblLine" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;&nbsp; ��:<asp:Label ID="lbDomain" runat ="server"></asp:Label>&nbsp;&nbsp;
                        ����:<asp:Label ID="lblPart" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;&nbsp;
                        &nbsp; <span style="font-family: ����">����:</span>
                        <asp:Label ID="lblDesc" runat="server" CssClass="LabelCenter" Font-Bold="False"></asp:Label>&nbsp;
                    </font>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" height="20" valign="middle">
                    <font style="color: Red; font-size: 11px;">��ע���ϴ��ļ������������2������</font><br />
                    1��<font style="color: Red; font-size: 11px;">�������պ�ԭ������ɱ�</font>&nbsp; 2��<font style="color: Red;
                        font-size: 11px;">�������鱨��</font><br />
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
                    �ļ�����:
                </td>
                <td align="left" style="width: 80px">
                    <asp:DropDownList ID="dropType" runat="server" Width="150px">
                        <asp:ListItem Value="N">----</asp:ListItem>
                        <asp:ListItem Value="M">���ա�ԭ����</asp:ListItem>
                        <asp:ListItem Value="Q">�������鱨��</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="left">
                    �ϴ��ļ�:
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
                    �ļ�����:
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txt_docsDecs" runat="server" Width="436px" MaxLength="200"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="uploadPartBtn" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="�ϴ�" Width="50px" OnClick="uploadPartBtn_ServerClick" />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="btnBack_Click" Text="����" Width="50px" />
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
                            <asp:BoundField DataField="prd_nbr" HeaderText="�ͻ���">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_doc_typeName" HeaderText="�ĵ�����">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_doc_filename" HeaderText="����">
                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_createName" HeaderText="�ϴ���">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_createDate" HeaderText="�ϴ�ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                <ItemStyle HorizontalAlign="Center" Width="110px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkstep" runat="server" CommandArgument='<%# Eval("prd_po_vend") + "," + Eval("prd_doc_id") %>'
                                        CommandName="download" Font-Size="12px" Font-Underline="true" ForeColor="Black"
                                        Text="�鿴"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkdelete" runat="server" CommandArgument='<%# Eval("id") + "," + Eval("prd_po_vend") + "," + Eval("prd_doc_id") %>'
                                        CommandName="del" Font-Size="12px" Font-Underline="true" ForeColor="Black" Text="<u>ɾ��</u>"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="prd_doc_desc" HeaderText="�ļ�����">
                                <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                <ItemStyle HorizontalAlign="Left" Width="130px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_transferStatus" HeaderText="ת��״̬">
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
