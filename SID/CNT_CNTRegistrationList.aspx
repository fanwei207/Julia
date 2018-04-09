<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CNT_CNTRegistrationList.aspx.cs" Inherits="SID_CNT_CNTRegistrationList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .GridViewRebuild {
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" border="0" width="900px">
        <tr>
            <td>进厂日期&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_enrtyTime1" Width="150px" CssClass="SmallTextBox EnglishDate Param"></asp:TextBox>-<asp:TextBox runat="server" ID="txt_enrtyTime2" Width="150px" CssClass="SmallTextBox EnglishDate Param"></asp:TextBox></td>
            
            <td>集装箱号<asp:TextBox ID="txt_cntID" runat="server" Width="100px"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="btn_search" Text="查询" CssClass="SmallButton2" Width="70px" OnClick="btn_search_Click"/></td>
        </tr>
    </table>
     <asp:GridView runat="server" ID="gv_cnt" AllowPaging="true" AutoGenerateColumns="false" DataKeyNames="id"
        CssClass="GridViewStyle GridViewRebuild" PageSize="20" Width="1130px" OnRowEditing="gv_cnt_RowEditing" OnRowUpdating="gv_cnt_RowUpdating" OnRowCancelingEdit="gv_cnt_RowCancelingEdit" OnPageIndexChanging="gv_cnt_PageIndexChanging">
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
                    <asp:TableCell Text="车牌号" HorizontalAlign="Center" Width="8%"></asp:TableCell>
                    <asp:TableCell Text="集装箱号" HorizontalAlign="Center" Width="8%"></asp:TableCell>
                    <asp:TableCell Text="进厂日期" HorizontalAlign="Center" Width="13%"></asp:TableCell>
                    <asp:TableCell Text="驾驶员" HorizontalAlign="Center" Width="9%"></asp:TableCell>
                    <asp:TableCell Text="身份证" HorizontalAlign="Center" Width="9%"></asp:TableCell>
                    <asp:TableCell Text="司机电话" HorizontalAlign="Center" Width="13%"></asp:TableCell>
                    <asp:TableCell Text="车队电话" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                    <asp:TableCell Text="临时证号" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                    <asp:TableCell Text="备注" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
         </EmptyDataTemplate>
         <Columns>
             <asp:TemplateField HeaderText="车牌号">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_plateNum" runat="server" Text='<%# Bind("plate_number") %>'
                    CssClass="smalltextbox" Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblplateNum" runat="server" Text='<%# Bind("plate_number") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="集装箱号">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_cntID" runat="server" Text='<%# Bind("cnt_id") %>'
                    CssClass="smalltextbox" Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblcntID" runat="server" Text='<%# Bind("cnt_id") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="进厂日期">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                <ItemTemplate>
                    <asp:Label ID="lblentryDate" runat="server" Text='<%# Bind("cnt_entrydate") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="驾驶员">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_driver" runat="server" Text='<%# Bind("driver_name") %>'
                    CssClass="smalltextbox" Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbldriver" runat="server" Text='<%# Bind("driver_name") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="身份证">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_driverIDCard" runat="server" Text='<%# Bind("driver_IDCard") %>'
                    CssClass="smalltextbox" Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbldriverIDCard" runat="server" Text='<%# Bind("driver_IDCard") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="司机电话">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_driverPhone" runat="server" Text='<%# Bind("driver_phone") %>'
                    CssClass="smalltextbox" Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbldriverPhone" runat="server" Text='<%# Bind("driver_phone") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="车队电话">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_motorcadephone" runat="server" Text='<%# Bind("motorcade_phone") %>'
                    CssClass="smalltextbox" Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblmotorcadephone" runat="server" Text='<%# Bind("motorcade_phone") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="临时证号">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_temporaryID" runat="server" Text='<%# Bind("temporary_ID") %>'
                    CssClass="smalltextbox" Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbltemporaryID" runat="server" Text='<%# Bind("temporary_ID") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="备注">
                <HeaderStyle Width="100px" HorizontalAlign="Center"/>
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_remark" runat="server" Text='<%# Bind("remark") %>'
                    CssClass="smalltextbox" Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblremark" runat="server" Text='<%# Bind("remark") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>
             <asp:CommandField EditText="编辑" UpdateText="更新" ShowEditButton="true"  CancelText="取消">
                <HeaderStyle Width="60px" HorizontalAlign="Center"/>
                <ItemStyle Width="60px" HorizontalAlign="Center"/>
             </asp:CommandField>
         </Columns>
     </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
