<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_accfound_mstr.aspx.cs"
    Inherits="HR_hr_accfound_mstr" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"  >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $("#btnInput").click(function () {
                $("#tbInput").show();
                $("#isInputShow").val("block");
            })

            $("#btnBack").click(function () {
                $("#tbInput").hide();
                $("#isInputShow").val("none");
            })
            //end
        })

    </script>
</head>
<body>
    <div align="center">
        <form id="form1" method="post" runat="server">
        <table width="980px" cellspacing="0" cellpadding="0" id="tbSearch" runat="server">
            <tr>
                <td valign="bottom">
                    年月&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="50px" MaxLength="4" AutoPostBack="true"></asp:TextBox>
                    &nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" CssClass="server" Width="40px">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="bottom" class="style1">
                    部门&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="100px" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td valign="bottom" class="style2">
                    工号&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="60px" TabIndex="2"></asp:TextBox>
                </td>
                <td valign="bottom">
                    姓名&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="60px" TabIndex="3"></asp:TextBox>
                </td>
                <td valign="bottom">
                    <asp:DropDownList ID="dropStatus" runat="server" Width="50px" TabIndex="3">
                    </asp:DropDownList>
                </td>
                <td valign="bottom">
                    社保类型&nbsp;<asp:DropDownList ID="dropIns" runat="server" Width="80px" TabIndex="4">
                    </asp:DropDownList>
                </td>
                <td align="center" valign="bottom">
                    <asp:Button ID="btnSearch" runat="server" Width="60px" Text="查询" CssClass="SmallButton2"
                        OnClick="btnSearch_Click" CausesValidation="false" />
                        <asp:Button ID="ButExcel" runat="server" CssClass="SmallButton2" 
                        Text="Excel" Width="60px" onclick="ButExcel_Click"></asp:Button>
                    <input id="btnInput" class="SmallButton2" type="button" value="录入" Width="40px" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvInsurance" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            runat="server" PageSize="20" Width="980px" DataSourceID="obdsInsurance" OnRowDataBound="MyRowDataBound"
            OnRowUpdating="MyRowUpdating" OnRowEditing="MyRowEditing" DataKeyNames="hr_accfound_id"
            OnRowDeleting="MyRowDeleting" OnSelectedIndexChanged="gvInsurance_SelectedIndexChanged">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="工号" DataField="userNo" ReadOnly="True">
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="userName" ReadOnly="True">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="dname" ReadOnly="True">
                    <ItemStyle Width="140px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="养老保险金" DataField="hr_accfound_rFound">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="医疗保险金" DataField="hr_accfound_mFound">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="失业保险金" DataField="hr_accfound_eFound">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="住房公积金" DataField="hr_accfound_hFound">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工伤" DataField="hr_accfound_Injury">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="离职日期" DataField="leavedate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" ReadOnly="True">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="70px" />
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelfound" runat="server" Text="<u>删除</u>" CommandName="Delete"
                            CausesValidation="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" Width="40px" />
                    <ControlStyle ForeColor="Black" />
                </asp:TemplateField>
                <asp:BoundField DataField="houseFund" HeaderText="住房" ReadOnly="True" Visible="False">
                    <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <FooterStyle CssClass="hidden" />
                </asp:BoundField>
                <asp:BoundField DataField="medicalFund" HeaderText="医疗" ReadOnly="True" Visible="False">
                    <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <FooterStyle CssClass="hidden" />
                </asp:BoundField>
                <asp:BoundField DataField="unemployFund" HeaderText="失业" ReadOnly="True" Visible="False">
                    <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <FooterStyle CssClass="hidden" />
                </asp:BoundField>
                <asp:BoundField DataField="retiredFund" HeaderText="养老" ReadOnly="True" Visible="False">
                    <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <FooterStyle CssClass="hidden" />
                </asp:BoundField>
                <asp:BoundField DataField="sretiredFund" HeaderText="工伤" ReadOnly="True" Visible="False">
                    <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <FooterStyle CssClass="hidden" />
                </asp:BoundField>
                <asp:BoundField HeaderText="合计" DataField="total" ReadOnly="True" >
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="序号" DataField="rowIndex" ReadOnly="True">
                    <ItemStyle CssClass="hidden" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="hidden" />
                    <FooterStyle CssClass="hidden" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="0" CellSpacing="0" CssClass="GridViewHeaderStyle">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="部门" Width="100px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="养老保险金" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="医疗保险金" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="失业保险金" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="住房公积金" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="工伤" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="离职日期" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Width="60px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsInsurance" runat="server" SelectMethod="gvInsDataBind"
            TypeName="Wage.HR" DeleteMethod="DeleteIns" UpdateMethod="UpdateIns">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" Name="strUserno" PropertyName="Text"
                    Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="txtUserName" Name="strUsername" PropertyName="Text"
                    Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="dropDept" DefaultValue="0" Name="intDept" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropStatus" DefaultValue="" Name="intSatus" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropIns" Name="intIns" PropertyName="SelectedValue"
                    Type="Int32" DefaultValue="0" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="hr_accfound_id" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="hr_accfound_hFound" Type="Decimal" />
                <asp:Parameter Name="hr_accfound_mFound" Type="Decimal" />
                <asp:Parameter Name="hr_accfound_eFound" Type="Decimal" />
                <asp:Parameter Name="hr_accfound_rFound" Type="Decimal" />
                <asp:Parameter Name="hr_accfound_Injury" Type="Decimal" />
                <asp:SessionParameter Name="intUser" SessionField="Uid" Type="Int32" />
            </UpdateParameters>
        </asp:ObjectDataSource>
        <%-- Start Input--%>
        <div id="tbInput" style='position: absolute; width: 100%; height: 100%; text-align: center;
            margin-left: 0px; left: 0px; margin-top: 0px; top: 0px; background-color: White;
            display: <% Response.Write(isInputShow.Value); %>'>
            <table style="width: 800px; text-align: center;" cellpadding="0" cellspacing="0">
                <tr style="white-space: nowrap;">
                    
                    <td>部门</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddl_Department"  runat="server" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Department_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                    <td>工段/工序</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddl_workshop" runat="server" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddl_workshop_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                    <td>职务/岗位</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddl_RoleType" runat="server" AutoPostBack="True" Width="60px" OnSelectedIndexChanged="ddl_RoleType_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="-1" >--</asp:ListItem>
                        <asp:ListItem Value="0">管理层</asp:ListItem>
                        <asp:ListItem Value="1">部门级</asp:ListItem>
                        <asp:ListItem Value="2">职工</asp:ListItem>
                        <asp:ListItem Value="3">工人</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddl_role" TabIndex="22" runat="server" Width="100px" AutoPostBack="True">
                    </asp:DropDownList>
                    </td>                    
                </tr>
                <tr>
                    <td>班组</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddl_workgroup" TabIndex="15" runat="server" Width="160px" AutoPostBack="True">
                    </asp:DropDownList>
                    </td>
                    <td style="text-align: right;">
                        工号:
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtInputUser" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtInputUser_TextChanged"
                            TabIndex="6"></asp:TextBox>
                        <asp:Label ID="lblInputName" runat="server" Width="70px"></asp:Label>
                        <asp:Label ID="lblUid" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 18px;">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr style="white-space: nowrap;">
                    <td style="text-align: right;">
                        养老保险金:
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtRfound" runat="server" Width="100px" TabIndex="10"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">
                        医疗保险金:
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtMfound" runat="server" Width="100px" TabIndex="8"></asp:TextBox>
                    </td>
                </tr>
                <tr style="white-space: nowrap;">
                    
                </tr>
                <tr style="white-space: nowrap;">
                    <td style="text-align: right;">
                        失业保险金:
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtEfound" runat="server" Width="100px" TabIndex="9" Height="20px"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">
                        住房公积金:
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtHfound" runat="server" Width="100px" TabIndex="7"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">
                        工伤:
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtInjure" runat="server" Width="100px" TabIndex="11"></asp:TextBox>
                        <input id="isInputShow" type="hidden" name="isInputShow" value="none" runat="server" />
                    </td>
                </tr> 
                
                <tr>
                    <td style="height: 18px;">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="white-space: nowrap;">
                    <td style="white-space: nowrap;" align="center">
                        <asp:Button ID="btnSave" runat="server" Width="80px" Text="保存" CssClass="SmallButton2"
                            OnClick="btnSave_Click" TabIndex="12" />
                    </td>
                    <td style="white-space: nowrap;" align="center">
                        <input id="btnBack" class="SmallButton2" type="button" value="返回" style="width: 80px;" />&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <%-- End--%>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        //设置录入调出层的高度
        $("#tbInput").height($("#ifrmHolder", parent.document).height()).css({ "top": "0px", "margin-top": "0px" });
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
