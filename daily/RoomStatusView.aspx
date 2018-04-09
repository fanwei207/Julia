<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoomStatusView.aspx.cs" Inherits="RoomStatusView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function datelink() 
        {
            document.getElementById("tb_EndDate").value = document.getElementById("tb_OrderDate").value;
        }
    </script>
</head>
<body>
    <div align="center">
    <form id="form1" runat="server">
    <input type="hidden" id="hd_loginCompanyCode" runat="server" />
    <div class="MainDiv">
        <br />
        <div class="MainTable">
            <table align="center">
                <tr>
                    <td width="60">
                        <asp:Label ID="Label2" runat="server" Text="������"></asp:Label>��
                    </td>
                    <td width="100">
                        <asp:DropDownList ID="ddl_meetingRooms" runat="server" OnSelectedIndexChanged="ddl_meetingRooms_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td width="80">
                        <asp:Label ID="lb_Date" runat="server" Text="��ѯ���ڣ�"></asp:Label>
                        <asp:Label ID="lb_Month" runat="server" Text="��ѯ�·ݣ�"></asp:Label>
                    </td>
                    <td width="150">
                        <asp:TextBox ID="tb_ViewDate" runat="server" CssClass="smalltextbox Date"></asp:TextBox>
                        <asp:DropDownList ID="ddl_ViewYear" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddl_ViewMonth" runat="server">
                        </asp:DropDownList>
                    </td>

                    <td width="60">
                        <asp:Button ID="btn_View" Text="�鿴" runat="server" OnClick="btn_View_Click"/>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="9">
                         <asp:GridView ID="gdv_MROrderList" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
                               OnRowCommand="gdv_MROrderList_RowCommand" OnRowDataBound="gdv_MROrderList_RowDataBound" DataKeyNames="MR_FormId,MR_AppEno,mr_begintime,mr_endtime"
                               OnPageIndexChanging="gdv_MROrderList_PageIndexChanging" Width="100%" CssClass="GridViewStyle">
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <Columns>
                                <asp:BoundField DataField="MR_FormId" HeaderText="���뵥��">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="mr_name" HeaderText="������" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="35px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_Deptname" HeaderText="����"  ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_AppName" HeaderText="������"  ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_AppExtNo" HeaderText="�ֻ���" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="mr_begintime" HeaderText="��ʼʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                     ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle Font-Size="0.8em" />
                                </asp:BoundField>
                                <asp:BoundField DataField="mr_endtime" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                     ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle Font-Size="0.8em" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_Reason" HeaderText="����"  ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle Font-Size="0.8em" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_BorrowThings" HeaderText="������Ʒ"  ItemStyle-HorizontalAlign="Left" >
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Font-Size="0.8em" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_otherDes" HeaderText="��������"  ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Font-Size="0.8em" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_AgentDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"  ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle Font-Size="0.8em" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="40px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_Cancel" CommandName="CancelOrder"  CancelText="<u>ȡ��</u>" Text="ȡ��" ToolTip="ȡ���˵�������������䣬����ȡ��"
                                            CommandArgument='<%# Bind("MR_FormId") %>' runat="server"  ItemStyle-HorizontalAlign="Center"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle Font-Underline="True" />
                                </asp:TemplateField>
                            </Columns>
                         </asp:GridView>


                       <%-- <asp:GridView ID="gdv_MROrderList" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowCommand="gdv_MROrderList_RowCommand" OnRowDataBound="gdv_MROrderList_RowDataBound" DataKeyNames="MR_FormId,MR_AppEno,mr_begintime"
                            OnPageIndexChanging="gdv_MROrderList_PageIndexChanging" AllowPaging="True" PageSize="20">
                            <Columns>
                                <asp:BoundField DataField="MR_FormId" HeaderText="���뵥��">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_RoomID" HeaderText="���">
                                    <HeaderStyle Width="35px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_Deptname" HeaderText="����">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_AppName" HeaderText="������">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_AppExtNo" HeaderText="�ֻ���">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="mr_begintime" HeaderText="��ʼʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                    meta:resourcekey="BoundFieldResource6">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle Font-Size="0.8em" />
                                </asp:BoundField>
                                <asp:BoundField DataField="mr_endtime" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                    meta:resourcekey="BoundFieldResource7">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle Font-Size="0.8em" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_Reason" HeaderText="����" meta:resourcekey="BoundFieldResource8" />
                                <asp:BoundField DataField="MR_BorrowThings" HeaderText="������Ʒ" meta:resourcekey="BoundFieldResource9" />
                                <asp:BoundField DataField="MR_otherDes" HeaderText="��������" meta:resourcekey="BoundFieldResource10" />
                                <asp:BoundField DataField="MR_AgentDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle Font-Size="0.8em" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="40px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_Cancel" CommandName="CancelOrder"  CancelText="<u>ȡ��</u>" Text="ȡ��" ToolTip="ȡ���˵�������������䣬����ȡ��"
                                            CommandArgument='<%# Bind("MR_FormId") %>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <EmptyDataRowStyle CssClass="gridviewrowstyle" />
                            <EditRowStyle CssClass="gridviewrowstyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <PagerTemplate>
                                <asp:Label ID="Label4" runat="server" Text="��" meta:resourcekey="Label4Resource1"></asp:Label><asp:Label
                                    ID="lblPageIndex" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>"
                                    meta:resourcekey="lblPageIndexResource1"></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text="ҳ ��" meta:resourcekey="Label5Resource1"></asp:Label><asp:Label
                                    ID="lblPageCount" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageCount %>"
                                    meta:resourcekey="lblPageCountResource1"></asp:Label>
                                <asp:Label ID="Label6" runat="server" Text="ҳ" meta:resourcekey="Label6Resource1"></asp:Label>
                                <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First"
                                    CommandName="Page" Text="��ҳ" meta:resourcekey="btnFirstResource1"></asp:LinkButton>
                                <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev"
                                    CommandName="Page" Text="��һҳ" meta:resourcekey="btnPrevResource1"></asp:LinkButton>
                                <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next"
                                    CommandName="Page" Text="��һҳ" meta:resourcekey="btnNextResource1"></asp:LinkButton>
                                <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last"
                                    CommandName="Page" Text="β�" meta:resourcekey="btnLastResource1"></asp:LinkButton>
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>"
                                    Width="20px" meta:resourcekey="txtNewPageIndexResource1"></asp:TextBox>
                                <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-1"
                                    CommandName="Page" Text="GO" meta:resourcekey="btnGoResource1"></asp:LinkButton>
                            </PagerTemplate>
                        </asp:GridView>--%>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
    </div>
    <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
