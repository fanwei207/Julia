<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_Qad.aspx.cs" Inherits="RDW_RDW_Qad" %>

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
            <table cellspacing="0" cellpadding="0" width="650" bgcolor="white" border="0">
                <tr>
                    <td align="left" colspan="2">
                        Part<asp:TextBox ID="txtQad" CssClass="smalltextbox" runat="server" Width="301px"></asp:TextBox>
                        <asp:Button ID="BtnAdd" runat="server" CssClass="SmallButton2" Text="Add" Width="50"
                            OnClick="BtnAdd_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                    Import File
                        <input id="filename1" style="width: 252px; height: 22px" type="file" size="27" name="filename1"
                            runat="server" />
                        &nbsp; &nbsp;
                        <asp:Button ID="BtnImport" runat="server" CssClass="SmallButton2" 
                            Text="Import QAD list" Width="93px" onclick="BtnImport_Click">
                        </asp:Button>
                    </td>
                    <td>
                        <label id="here" onclick="submit();">
                        <font size="3">下载：</font>
                        <a href="/docs/RDW_QadApplyImport.xls" target="blank"><font color="blue">导入模版</font></a>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Associated Part
                    </td>
                    <td align="right">
                        <input class="SmallButton2" id="Button1" onclick="window.close();" type="button"
                            value="Close" name="Button1" runat="server">
                        </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" width="650" align="center" bgcolor="white"
                border="0">
                <tr width="100%">
                    <td>
                        <asp:GridView ID="gvRWDQad" runat="server" Width="650px" AllowPaging="True" PageSize="20"
                            AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames="id,qad" OnRowDeleting="gvRWDQad_RowDeleting"
                            OnPageIndexChanging="gvRWDQad_PageIndexChanging" OnRowCommand="gvRWDQad_RowCommand" OnRowDataBound="gvRWDQad_RowDataBound">
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblViewNo" runat="server" Text='<%# (Container.DataItemIndex + 1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="qad" HeaderText="Part">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="description" HeaderText="Description">
                                    <HeaderStyle Width="430px"></HeaderStyle>   
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="log" HeaderText="Verify">
                                    <HeaderStyle Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="pkgcode" HeaderText="Change Part">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="reason" HeaderText="Reason">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Bom">
                                    <HeaderStyle Width="40px" HorizontalAlign="Center"/>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnBom" runat="server" Text="Bom" ForeColor="Black" CommandName="gobom"
                                             CommandArgument='<%# Eval("qad") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <HeaderStyle Width="40px" HorizontalAlign="Center"/>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Del" ForeColor="Black" CommandName="Delete" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </form>
    </div>

    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>

</body>
</html>
