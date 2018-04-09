<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mr_RoomStatusView.aspx.cs" Inherits="mr_RoomStatusView" %>

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

       $(function () {
           $("input[name$='chkAll']:eq(1)").remove();
           $("#chkAll").click(function () {
               $("#gdv_MROrderList input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked", $(this).prop("checked"))
           })
       })
    </script>
</head>
<body>
    <div align="center">
    <form id="form1" runat="server">
    <div class="MainDiv">
        <br />
        <div class="MainTable">
            <table align="center" cellspacing="0" cellpadding="0" width="1150px">
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
                        
                    </td>
                    <td width="180">
                        <asp:TextBox ID="tb_ViewStartDate" runat="server" CssClass="smalltextbox Date" Width="80px"></asp:TextBox>
                        -
                        <asp:TextBox ID="tb_ViewEndDate" runat="server" CssClass="smalltextbox Date" Width="80px"></asp:TextBox>
                    </td>
                    <td width="20">
                        <asp:Label ID="Label1" runat="server" Text="��"></asp:Label>��
                    </td>
                    <td Width="80px">
                        <asp:DropDownList ID="ddl_Domain" runat="server" Width="80px" AutoPostBack="true"
                            onselectedindexchanged="ddl_Domain_SelectedIndexChanged">
                            <asp:ListItem Value="SZX" >SZX</asp:ListItem>
                            <asp:ListItem Value="ZQL">ZQL</asp:ListItem>
                            <asp:ListItem Value="YQL">YQL</asp:ListItem>
                            <asp:ListItem Value="HQL">HQL</asp:ListItem>
                            <asp:ListItem Value="TCB">TCB</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="40px">
                        <asp:Label ID="Label3" runat="server" Text="����"></asp:Label>��
                    </td>
                    <td Width="80px">
                        <asp:DropDownList ID="ddl_Department" runat="server" Width="80px" AutoPostBack="true"
                            onselectedindexchanged="ddl_Department_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td width="50px">
                        <asp:CheckBox ID="cb_effective" Text="��Ч" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="cb_effective_CheckedChanged"/>
                    </td>
                    <td width="60px">
                        <asp:CheckBox ID="isCheck" Text="������" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="isCheck_CheckedChanged"/>
                    </td>
                    <td width="60">
                        <asp:Button ID="btn_View" Text="�鿴" runat="server" OnClick="btn_View_Click"/>
                    </td>
                    <td width="60">
                        <asp:Button ID="btn_Approve" Text="ͬ��" runat="server" OnClick="btn_Approve_Click"/>
                    </td>
                    <td width="300px">
                    </td>
                    <td>
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="12">
                         <asp:GridView ID="gdv_MROrderList" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
                               OnRowCommand="gdv_MROrderList_RowCommand" OnRowDataBound="gdv_MROrderList_RowDataBound" DataKeyNames="MR_FormId,MR_AppEno,mr_begintime,mr_endtime,mr_id,MR_AppName,MR_Deptname"
                               OnPageIndexChanging="gdv_MROrderList_PageIndexChanging" Width="1280px" CssClass="GridViewStyle">

                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <Columns>
                                <asp:TemplateField>
                                <ItemTemplate>                  
                                    <asp:CheckBox id="chk" runat="server"/>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="MR_FormId" HeaderText="���뵥��" ReadOnly="True">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <%--<asp:TemplateField HeaderText="���뵥��" Visible="true">
                                    <ItemStyle HorizontalAlign="Center" width="60" Font-Underline="true"/>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="mr_reqNo" CssClass="no" runat="server" Text='<%# Eval("MR_FormId") %>'
                                        CommandName="editReq" CommandArgument='<%# Eval("MR_FormId") %>'>

                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle Font-Underline="true"/>
                                    <HeaderStyle HorizontalAlign="Center" Width="110"/>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="mr_name" HeaderText="������" ItemStyle-HorizontalAlign="Center" ReadOnly="True">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_Deptname" HeaderText="����"  ItemStyle-HorizontalAlign="Center" ReadOnly="True">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_AppName" HeaderText="������"  ItemStyle-HorizontalAlign="Center" ReadOnly="True">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_AppExtNo" HeaderText="�ֻ���" ItemStyle-HorizontalAlign="Left" ReadOnly="True">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="begintime" HeaderText="��ʼʱ��" ItemStyle-HorizontalAlign="Left" ReadOnly="True">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="endtime" HeaderText="����ʱ��" ItemStyle-HorizontalAlign="Left" ReadOnly="True">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
<%--                                <asp:TemplateField HeaderText="��ʼʱ��">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_begintime" runat="server" CssClass="SmallTextBox" Text='<%# Bind("begintime") %>'
                                            Width="100px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemTemplate>
                                        <%#Eval("begintime")%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
<%--                                <asp:TemplateField HeaderText="����ʱ��">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_endtime" runat="server" CssClass="SmallTextBox" Text='<%# Bind("endtime") %>'
                                            Width="100px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemTemplate>
                                        <%#Eval("endtime")%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="MR_Reason" HeaderText="����"  ItemStyle-HorizontalAlign="Left" ReadOnly="True">
                                    <HeaderStyle Width="190px" />
                                    <ItemStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_BorrowThings" HeaderText="������Ʒ"  ItemStyle-HorizontalAlign="Left"  ReadOnly="True">
                                    <HeaderStyle Width="160px" />
                                    <ItemStyle HorizontalAlign="Left" Width="160px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AgentDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"  ItemStyle-HorizontalAlign="Center" ReadOnly="True">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle />
                                </asp:BoundField>
<%--                                <asp:CommandField ShowEditButton="True" CancelText="<u>ȡ��</u>" DeleteText="<u>ɾ��</u>"
                                    EditText="<u>�༭</u>" UpdateText="<u>����</u>">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                                </asp:CommandField>--%>
                                <asp:TemplateField>
                                    <HeaderStyle Width="50px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_Cancel" CommandName="CancelOrder"  CancelText="<u>�ܾ�</u>" Text="�ܾ�" ToolTip="ȡ���˵�������������䣬����ȡ��"
                                            CommandArgument='<%# Bind("MR_FormId") %>' runat="server"  ItemStyle-HorizontalAlign="Center"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="true"/>
                                     <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="MR_otherDes" HeaderText="��������"  ItemStyle-HorizontalAlign="Left" ReadOnly="True">
                                    <HeaderStyle Width="140px" />
                                    <ItemStyle />
                                </asp:BoundField>
                            </Columns>
                         </asp:GridView>
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
