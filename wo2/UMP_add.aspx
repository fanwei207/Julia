<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UMP_add.aspx.cs" Inherits="wo2_UMP_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 100px;
        }
        .SmallTextBox
        {
        }
        .style3
        {
        }
        .style4
        {
            width: 120px;
        }
        .style6
        {
            width: 281px;
        }
        .style7
        {
            width: 547px;
        }
        .style8
        {
            width: 100px;
            height: 43px;
        }
        .style9
        {
            width: 547px;
            height: 43px;
        }
        .style10
        {
            height: 43px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="ProjectHeader" runat="server" cellspacing="1" cellpadding="1" width="800px" bgcolor="white" border="0">
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblcode" runat="server" Width="103px" CssClass="LabelRight" Text="申请单号:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left" class="style6">
                    <!--<asp:DropDownList ID="dropCatetory" runat="server" DataTextField="cate_name" DataValueField="cate_id"
                        Width="190px" Enabled="false" Height="16px">
                    </asp:DropDownList>-->
                   
                    <asp:TextBox ID="txtUMPcode" runat="server" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lbl_id" runat="server" Text="" Visible="false" ></asp:Label>
                      <asp:Label ID="lblApplyId" runat="server" Text="" Visible="false"></asp:Label>
                </td>
                     <td align="right" class="style1">
                    <asp:Label ID="lblname" runat="server" Width="100px" CssClass="LabelRight" Text="类型:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" class="style6">
                   
                    
                    <asp:DropDownList ID="ddltype" runat="server" DataTextField="UMP_typename"
                        DataValueField="UMP_typeid" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="right" class="style4">
                    <asp:Label ID="lblremark" runat="server" Width="100px" CssClass="LabelRight" Text="账户:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" class="style4">
                  
                    <asp:DropDownList ID="ddlaccount" runat="server" DataTextField="UMP_accountname"
                        DataValueField="UMP_accountid" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblPM1" runat="server" Width="80px" CssClass="LabelLeft" Font-Bold="false"
                        Text="明细账:"></asp:Label>
                </td>
                <td align="left" class="style6">
                    
                   
                    
                    <asp:DropDownList ID="ddlaccountdet" runat="server" DataTextField="UMP_accountdetname"
                        DataValueField="UMP_accountdetid">
                    </asp:DropDownList>
                </td>
                <td align="right" class="style1">
                    <asp:Label ID="lblStartDate" runat="server" Width="100px" CssClass="LabelRight" Text="地点:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" class="style6">
                     <asp:DropDownList ID="ddlsite" runat="server" DataTextField="qad_site"
                        DataValueField="qad_site" AutoPostBack="True" OnSelectedIndexChanged="ddlsite_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="right" class="style4">
                    <asp:Label ID="lblEndDate" runat="server" Width="100px" CssClass="LabelRight" Text="域:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" class="style4">
                    <asp:TextBox ID="txtdomain" runat="server" Enabled="false"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblProdDesc" runat="server" Width="100px" CssClass="LabelRight" Text="成本中心:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" class="style3" colspan="3">
                    <asp:TextBox ID="txtcode" runat="server" AutoPostBack="True" OnTextChanged="txtcode_TextChanged"></asp:TextBox>

                    <asp:Label ID="lbldeptcode" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" class="style4">
                    <asp:Button ID="btnDoc" runat="server" CssClass="SmallButton2" TabIndex="11" Text="保存"
                        Width="50px" CausesValidation="false" OnClick="btnDoc_Click" />
                </td>
                <td align="left" colspan="2">
                    &nbsp;
                    <asp:Label ID="lblCurrentApprover" runat="server" CssClass="LabelLeft" Font-Bold="false" Width="0" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <br />

        <table cellspacing="0" cellpadding="0" width="800ox" bgcolor="white" border="0" id="det" runat="server">
            <tr align="left">
                <td align="left" colspan="2">
                    物料号<asp:TextBox ID="txtQad" CssClass="Part" runat="server" Width="203px"></asp:TextBox>
                    &nbsp; &nbsp;

                    申请量<asp:TextBox ID="txtnum" CssClass="smalltextbox" runat="server"></asp:TextBox>
                     &nbsp; &nbsp;

                    备注<asp:TextBox ID="txtremark" CssClass="smalltextbox" runat="server" Width="203px"></asp:TextBox>
                    <asp:Button ID="BtnAdd" runat="server" CssClass="SmallButton2" Text="Add" Width="50"
                        OnClick="BtnAdd_Click"></asp:Button>

                         &nbsp; &nbsp;
                    </td>
            </tr>
           
           <%--  <tr>
                <td align="left">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" Width="60px" 
                        AutoPostBack="True" oncheckedchanged="chkAll_CheckedChanged"
                         />
                    
                </td>
            </tr>--%>
            <tr>
                <td align="left" colspan="2">
                    <asp:GridView ID="gvRWDQad" runat="server" Width="800px" AllowPaging="True" PageSize="20"
                        AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames="UMP_id" OnRowDeleting="gvRWDQad_RowDeleting"
                        OnPageIndexChanging="gvRWDQad_PageIndexChanging" OnRowCommand="gvRWDQad_RowCommand"
                        OnRowDataBound="gvRWDQad_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="没有数据" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                   
                                </asp:TableRow>
                                <asp:TableRow BackColor="White">
                                    <asp:TableCell Text="No data" Width="100px" HorizontalAlign="center" ColumnSpan="8"></asp:TableCell>                                  
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <%--<asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                     <asp:CheckBox ID="chk_Select" runat="server" Width="20px" />
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="UMP_qad" HeaderText="物料">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="UMP_desc" HeaderText="描述">
                                <HeaderStyle Width="200px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="UMP_UM" HeaderText="单位">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>

                                <asp:BoundField DataField="UMP_cst_tot" HeaderText="成本">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
<%--                            <asp:BoundField DataField="log" HeaderText="Verify">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="UMP_qty1" HeaderText="申请量">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="UMP_qty2" HeaderText="实发量" >
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="UMP_remark" HeaderText="备注">
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                           
                             <asp:CommandField ShowDeleteButton="True" DeleteText="Del">
                                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:CommandField>
                         <%--   <asp:TemplateField HeaderText="Delete">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Del" ForeColor="Black" CommandName="Delete"
                                        CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
                <table style="width: 800px;" cellpadding="1" cellspacing="1" id="appv" runat="server">
            <tr align="right">
                <td align="right" class="style1">
                    <asp:Label ID="Label3" runat="server" Text="提交给：" Width="76px" CssClass="LabelRight"></asp:Label>
                </td>
                <td align="left" class="style7">
                    &nbsp;<asp:TextBox 
                        ID="txtApproveName" runat="server" 
                        CssClass="SmallButton2" Width="78px"
                        Height="21px"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txt_ApproveEmail" runat="server" CssClass="SmallButton2" Width="161px"
                        Height="21px"></asp:TextBox>
                    <asp:TextBox ID="txt_approveID" runat="server" Width="0px" BorderWidth="0"></asp:TextBox>
                    <asp:Button ID="btn_Approver" runat="server" Text="选择提交人" OnClick="btn_Approver_Click"
                        CssClass="SmallButton2" Width="93px" Height="21px" />
                    &nbsp;
                    <asp:CheckBox ID="chkEmail" runat="server" Text="发送邮件" Checked="true" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="Label5" runat="server" Text="理由:" Width="100px" CssClass="LabelRight"></asp:Label>
                </td>
                <td align="left" class="style7">
                    <asp:TextBox ID="txtApplyReason" runat="server" CssClass="SmallTextBox" Width="520px"
                        MaxLength="500" TextMode="MultiLine" Height="28px"></asp:TextBox>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblApplyDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="approveopinion" visible="false">
                <td align="right" class="style1" >
                    <asp:Label ID="Label6" runat="server" Text="Approve Opinion:"></asp:Label>
                </td>
                <td align="left" class="style7">
                    <asp:TextBox ID="txtApprOpin" runat="server" CssClass="SmallTextBox" Width="520px"
                        MaxLength="500" TextMode="MultiLine" Height="28px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblAproveDate" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="approvalresult" visible="false">
                <td align="right" class="style8">
                    <asp:Label ID="Label7" runat="server" Text="Approve Results:"></asp:Label>
                </td>
                <td align="left" class="style9" >
                    <asp:RadioButtonList ID="rdb_Result" runat="server" TextAlign="Right" RepeatDirection="Horizontal"
                        Height="16px" Enabled="False">
                        <asp:ListItem Value="False"> No Pass</asp:ListItem>
                        <asp:ListItem Value="True"> Pass</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="style10">
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    &nbsp;
                </td>
                <td class="style7">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_submit" runat="server" Text="提交" CssClass="SmallButton2"
                        OnClick="btn_submit_Click" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btn_approve" runat="server" Text="通过" CssClass="SmallButton2"
                        OnClick="btn_approve_Click" />&nbsp; &nbsp; &nbsp;<asp:Button ID="btn_diaApp" runat="server"
                            CssClass="SmallButton2" Text="拒绝" Width="70px" OnClick="btn_diaApp_Click" />&nbsp;
                    &nbsp;
                    
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
            <asp:Button ID="btnBack0" runat="server" CssClass="SmallButton2" Text="返回" Width="50px"
                        OnClick="btnBack_Click" />
        <br />
       
        <table cellspacing="0" cellpadding="0" width="800" bgcolor="white" border="0">
            <tr>
                <td align="left">
                    Approval List
                </td>
            </tr>
            <tr>
                <td align="left" >
                    <asp:GridView ID="gvApprove" runat="server" Width="800px" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" PageSize="20"  DataKeyNames="" EmptyDataText="No data">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                GridLines="Vertical"  CssClass="GridViewHeaderStyle">
                                <asp:TableRow>
                                     <asp:TableCell Text="没有数据" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow BackColor="White">
                                    <asp:TableCell Text="No data" Width="100px" HorizontalAlign="center" ColumnSpan="4"></asp:TableCell>
                                    
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="UMP_applyName" HeaderText="审批人" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UMP_applyReason" HeaderText="审批说明" HtmlEncode="False">
                                <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="UMP_applyDate" HeaderText="审批日期">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ump_status" HeaderText="状态">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
