<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CNT_EntryLeaveList.aspx.cs" Inherits="SID_CNT_EntryLeaveList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
   <div>
    <form id="form1" runat="server">
        <table id="table1" cellpadding="0" cellspacing="0" border="0" width="900px">
            <tr style="height:50px">
                <td>进厂日期&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_enrtyTime1" Width="150px" CssClass="SmallTextBox EnglishDate Param"></asp:TextBox>-<asp:TextBox runat="server" ID="txt_enrtyTime2" Width="150px" CssClass="SmallTextBox EnglishDate Param"></asp:TextBox></td>
                <td>仅未检查<asp:CheckBox runat="server" ID="chk_check" Text="" CssClass="Param" /></td>
                <td>仅未出厂<asp:CheckBox runat="server" ID="chk_leave" Text="" CssClass="Param" /></td>
                <td>仅未封条<asp:CheckBox runat="server" ID="chk_seal" Text="" CssClass="Param" /></td>
                <td><asp:Button ID="btn_search" Text="查询" CssClass="SmallButton2" Width="70px" runat="server" OnClick="btn_search_Click" /></td>
                <td><asp:Button ID="btn_Add" Visible="false" Text="添加" CssClass="SmallButton2" Width="70px" runat="server" OnClick="btn_Add_Click" /></td>
            </tr>
        </table>
    <asp:GridView ID="gv_cnt" runat="server" AllowPaging="true" AutoGenerateColumns="false"
        CssClass="GridViewStyle GridViewRebuild" PageSize="20" Width="1130px" 
            OnSelectedIndexChanged="gv_cnt_SelectedIndexChanged" OnRowCommand="gv_cnt_RowCommand"
        DataKeyNames="cnt_id,cnt_entrydate,motorcade_phone,driver_phone" 
            OnRowDataBound="gv_cnt_RowDataBound" 
            onpageindexchanging="gv_cnt_PageIndexChanging">
        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />
        <EmptyDataTemplate>
            <asp:Table ID="tb1" Width="1200px" runat="server" CellPadding="-1" CellSpacing="0"
            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                <asp:TableRow>
                    <asp:TableCell Text="车牌号" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                    <asp:TableCell Text="箱号" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                    <asp:TableCell Text="进厂日期" HorizontalAlign="Center" Width="15%"></asp:TableCell>
                    <asp:TableCell Text="驾驶员" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                    <asp:TableCell Text="封条号" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                    <asp:TableCell Text="出厂时间" HorizontalAlign="Center" Width="15%"></asp:TableCell>
                    <asp:TableCell Text="集装箱检查" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                    <asp:TableCell Text="封条检查" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                    <asp:TableCell Text="中途联系" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="车牌号" DataField="plate_number">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:BoundField HeaderText="箱号" DataField="cnt_id">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:BoundField HeaderText="进厂日期" DataField="cnt_entrydate">
                <HeaderStyle Width="150px" HorizontalAlign="Center"/>
                <ItemStyle Width="150px" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:BoundField HeaderText="驾驶员" DataField="driver_name">
                <HeaderStyle Width="80px" HorizontalAlign="Center"/>
                <ItemStyle Width="80px" HorizontalAlign="Left"/>
            </asp:BoundField>
            <asp:BoundField HeaderText="封条号" DataField="seal_ID">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:TemplateField HeaderText="出厂日期">
                <HeaderStyle Width="150px" HorizontalAlign="Center"/>
                <ItemStyle Width="150px" HorizontalAlign="Center" Font-Underline="true"/>
                <ItemTemplate>
                    <asp:LinkButton ID="linkLeave" runat="server" CommandArgument='<%# Bind("cnt_leavedate") %>'
                    CommandName="Leave" Font-Underline="True" Text='<%# Bind("cnt_leavedate") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="集装箱检查">
                <HeaderStyle Width="150px" HorizontalAlign="Center"/>
                <ItemStyle Width="150px" HorizontalAlign="Center" Font-Underline="true"/>
                <ItemTemplate>
                    <asp:LinkButton ID="linkCntChk" runat="server" CommandArgument='<%# Bind("cnt_checkdate") %>'
                    CommandName="CntChk" Font-Underline="True" Text='<%# Bind("cnt_checkdate") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="封条检查">
                <HeaderStyle Width="150px" HorizontalAlign="Center"/>
                <ItemStyle Width="150px" HorizontalAlign="Center" Font-Underline="true"/>
                <ItemTemplate>
                    <asp:LinkButton ID="linkSealChk" runat="server" CommandArgument='<%# Bind("seal_checkdate") %>'
                    CommandName="SealChk" Font-Underline="True" Text='<%# Bind("seal_checkdate") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="中途联系">
                <HeaderStyle Width="150px" HorizontalAlign="Center"/>
                <ItemStyle Width="150px" HorizontalAlign="Center" Font-Underline="true"/>
                <ItemTemplate>
                    <asp:LinkButton ID="linkTrackChk" runat="server" CommandArgument='<%# Bind("tracking_date") %>'
                    CommandName="TrackChk" Font-Underline="True" Text='<%# Bind("tracking_date") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    </form>
   </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
