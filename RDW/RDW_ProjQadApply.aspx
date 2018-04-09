<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_ProjQadApply.aspx.cs"
    Inherits="RDW_RDW_ProjQadApply" %>

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
                    <asp:Label ID="lblProject0" runat="server" Width="103px" CssClass="LabelRight" Text="Project Category:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left" class="style6">
                    <!--<asp:DropDownList ID="dropCatetory" runat="server" DataTextField="cate_name" DataValueField="cate_id"
                        Width="190px" Enabled="false" Height="16px">
                    </asp:DropDownList>-->
                    <asp:Label ID="lblCatetory" runat="server" Text="" Visible="false"></asp:Label>
                </td>
                     <td align="right" class="style1">
                    <asp:Label ID="lblProject" runat="server" Width="100px" CssClass="LabelRight" Text="Project Name:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" class="style6">
                    <asp:Label ID="lblProjectData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
                <td align="right" class="style4">
                    <asp:Label ID="lblProdCode" runat="server" Width="100px" CssClass="LabelRight" Text="Product Code:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" class="style4">
                    <asp:Label ID="lblProdCodeData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                    <asp:Label ID="lblprojStatus" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblApplyId" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblPM1" runat="server" Width="80px" CssClass="LabelLeft" Font-Bold="false"
                        Text="Project Leader:"></asp:Label>
                </td>
                <td align="left" class="style6">
                    <asp:Label ID="lblPM" runat="server" Width="80px" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
                <td align="right" class="style1">
                    <asp:Label ID="lblStartDate" runat="server" Width="100px" CssClass="LabelRight" Text="Project Start Date:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" class="style6">
                    <asp:Label ID="lblStartDateData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
                <td align="right" class="style4">
                    <asp:Label ID="lblEndDate" runat="server" Width="100px" CssClass="LabelRight" Text="Project End Date:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" class="style4">
                    <asp:Label ID="lblEndDateData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblProdDesc" runat="server" Width="100px" CssClass="LabelRight" Text="Description:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" class="style3" colspan="3">
                    <asp:Label ID="lblProdDescData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
                <td align="right" class="style4">
                    <asp:Button ID="btnDoc" runat="server" CssClass="SmallButton2" TabIndex="11" Text="Doc"
                        Width="50px" CausesValidation="false" OnClick="btnDoc_Click" />
                </td>
                <td align="left" colspan="2">
                    &nbsp;
                    <asp:Label ID="lblCurrentApprover" runat="server" CssClass="LabelLeft" Font-Bold="false" Width="0" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <br />

        <table cellspacing="0" cellpadding="0" width="800ox" bgcolor="white" border="0">
            <tr align="left">
                <td align="left" colspan="2">
                    QAD Part<asp:TextBox ID="txtQad" CssClass="smalltextbox" runat="server" Width="203px"></asp:TextBox>
                    &nbsp; &nbsp;
                    <asp:Button ID="BtnAdd" runat="server" CssClass="SmallButton2" Text="Add" Width="50"
                        OnClick="BtnAdd_Click"></asp:Button>

                         &nbsp; &nbsp;
                    <asp:Button ID="BtnExport" runat="server" CssClass="SmallButton2" 
                        Text="Export QAD list" Width="93px" onclick="BtnExport_Click"
                        ></asp:Button>
                </td>
            </tr>
            <tr  id="Import" runat="server">
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
                    <font size="3">下载：</font>
                    <label id="here" onclick="submit();">
                    <a href="/docs/RDW_QadApplyImport.xls" target="blank"><font color="blue">导入模版</font></a>
                    </label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnSelectAll" runat="server" CssClass="SmallButton2" Text="Select All" Width="50"
                    OnClick="btnSelectAll_Click"></asp:Button>
                    <asp:Button ID="btnClear" runat="server" CssClass="SmallButton2" Text="Clear" Width="50"
                    OnClick="btnClear_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:GridView ID="gvRWDQad" runat="server" Width="800px" AllowPaging="True" PageSize="20"
                        AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames="id,qad" OnRowDeleting="gvRWDQad_RowDeleting"
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
                                    <asp:TableCell Text="Select" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="No." Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Part" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Description" Width="430px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Verify" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Date" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="ChangePart" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Reason" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow BackColor="White">
                                    <asp:TableCell Text="No data" Width="100px" HorizontalAlign="center" ColumnSpan="8"></asp:TableCell>                                  
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Checked='<%# check(Eval("selected")) %>' OnCheckedChanged="ItemCheckBox_CheckedChanged" AutoPostBack="true"/>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
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
<%--                            <asp:BoundField DataField="log" HeaderText="Verify">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Verify">
                                <HeaderStyle Width="50px" />
                                <ItemTemplate>
                                    <%# Eval("log")%>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                <table style="width: 800px;" cellpadding="1" cellspacing="1">
            <tr align="right">
                <td align="right" class="style1">
                    <asp:Label ID="Label3" runat="server" Text="Submit to：" Width="76px" CssClass="LabelRight"></asp:Label>
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
                    <asp:Button ID="btn_Approver" runat="server" Text="Select Approver" OnClick="btn_Approver_Click"
                        CssClass="SmallButton2" Width="93px" Height="21px" />
                    &nbsp;
                    <asp:CheckBox ID="chkEmail" runat="server" Text="Send Email" Checked="true" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="Label5" runat="server" Text="Apply Reason:" Width="100px" CssClass="LabelRight"></asp:Label>
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
                    <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="SmallButton2"
                        OnClick="btn_submit_Click" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btn_approve" runat="server" Text="Approve" CssClass="SmallButton2"
                        OnClick="btn_approve_Click" />&nbsp; &nbsp; &nbsp;<asp:Button ID="btn_diaApp" runat="server"
                            CssClass="SmallButton2" Text="DisApprove" Width="70px" OnClick="btn_diaApp_Click" />&nbsp;
                    &nbsp;
                    <asp:Button ID="btnBack0" runat="server" CssClass="SmallButton2" Text="Back" Width="50px"
                        OnClick="btnBack_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table id="ProjectStep" runat="server" cellspacing="0" cellpadding="0" width="800" bgcolor="white" border="0">
            <tr>
                <td align="left">
                    Project Step Information
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvRDW" runat="server"  AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" PageSize="20" OnPreRender="gvRDW_PreRender" OnRowDataBound="gvRDW_RowDataBound"
                        Width="800px" DataKeyNames="RDW_DetID,RDW_Sort,RDW_isActive,RDW_isTemp" OnRowCommand="gvRDW_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="No." Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Description" Width="370px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="StartDate" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="EndDate" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Complete" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Duration" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Member" Width="130px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Approver" Width="130px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="RDW_StepNo" HeaderText="No." HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RDW_StepName" HeaderText="Step Name" HtmlEncode="False">
                                <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RDW_StepStartDate" HeaderText="StartDate">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RDW_StepEndDate" HeaderText="EndDate">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RDW_StepFinishDate" HeaderText="Complete">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RDW_Duration" HeaderText="Duration">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Member">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkMember" runat="server" CommandArgument='<%# Bind("RDW_DetID") %>'
                                        CommandName="Member" Font-Bold="False" Font-Size="8pt" Font-Underline="True"
                                        ForeColor="Black" Text='<%# Bind("RDW_PartnerName") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                <ItemStyle HorizontalAlign="Center" Width="130px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approver">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkApprover" runat="server" CommandArgument='<%# Bind("RDW_DetID") %>'
                                        CommandName="Approver" Font-Bold="False" Font-Size="8pt" Font-Underline="True"
                                        ForeColor="Black" Text='<%# Bind("RDW_Evaluater") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                <ItemStyle HorizontalAlign="Center" Width="130px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
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
                        CssClass="GridViewStyle" PageSize="20"  DataKeyNames="rdw_pqapproveid" EmptyDataText="No data">
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
                                    <asp:TableCell Text="Approver" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Opinion" Width="350px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Result" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="ApprovalDate" Width="200px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow BackColor="White">
                                    <asp:TableCell Text="No data" Width="100px" HorizontalAlign="center" ColumnSpan="4"></asp:TableCell>
                                    
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="rdw_approvename" HeaderText="Approver" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rdw_approvenote" HeaderText="Opinion" HtmlEncode="False">
                                <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rdw_approveresult" HeaderText="Result">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rdw_approvedate" HeaderText="ApprovalDate">
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
