<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestComplaint_NewSheet.aspx.cs" Inherits="rmInspection_GuestComplaint_NewSheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户投诉-新增投诉单</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="complain.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center" style="margin-top: 20px;">
            <table>
                <tr>
                    <td style="width: 60px;">投诉单号</td>
                    <td colspan="2">
                        <asp:Label ID="labGuestComplaintNo" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="width: 60px;">客户代码</td>
                    <td colspan="2" style="text-align: left;">
                        <asp:TextBox ID="txtGuestNo" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="10">解决方式</td>
                </tr>
                <tr>
                    <td style="width: 60px;">客户名称</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtGuestName" runat="server" CssClass="SmallTextBox5" Width="425px"></asp:TextBox>
                    </td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <td style="width: 60px;">客户等级</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlGuestLevel" runat="server" Width="40px"
                            DataTextField="LevelName" DataValueField="LevelName">
                        </asp:DropDownList>
                    </td>
                    <td colspan="9" rowspan="5">
                        <asp:CheckBoxList ID="radApproach" runat="server" RepeatDirection="Vertical" DataTextField="ApproachName" DataValueField="ApproachID" Font-Bold="False" Enabled="true" RepeatLayout="Flow">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <%--<td style="width:60px;">客户等级</td>
                <td style="text-align:left;">
                    <asp:DropDownList ID="ddlGuestLevel" runat="server"  Width="40px" 
                                    DataTextField="LevelName" DataValueField="LevelName">
                    </asp:DropDownList> 
                </td>   --%>
                    <td style="width: 60px;">严重等级</td>
                    <td>
                        <asp:DropDownList ID="ddlSeverity" runat="server" RepeatDirection="Horizontal" DataTextField="SeverityName" DataValueField="SeverityName" CellPadding="10" CellSpacing="1" Font-Bold="False" Enabled="true" RepeatLayout="Flow">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 80px;">客诉接收日期</td>
                    <td>
                        <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="SmallTextBox Date"></asp:TextBox>
                    </td>
                </tr>
                <%--            <tr>                
                <td style="width:80px;">客诉接收日期</td>
                <td>
                    <asp:TextBox ID="txtReceivedDate" runat="server"></asp:TextBox>
                </td>
            </tr>--%>
                <tr>
                    <td style="width: 60px;">导入文件 &nbsp;
                    </td>
                    <td colspan="4" valign="top" style="height: 28px">
                        <input id="file1" style="width: 468px; height: 22px" type="file" name="filename"
                            runat="server" />
                    </td>
                    <td style="height: 28px; text-align: center;">
                        <asp:Button ID="btnImport" runat="server" CausesValidation="False" CssClass="SmallButton2"
                            Text="导入" Width="80px" OnClick="btnImport_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px;">下载模板
                    </td>
                    <td colspan="3" align="left">
                        <asp:LinkButton ID="lkbModle" runat="server" Text="导入模板" Font-Underline="true" CommandName="down" OnClick="lkbModle_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="FixedGridHeight"></td>
                </tr>
                <tr>
                    <td colspan="16" class="FixedGridHeight">
                        <asp:GridView ID="gvImport" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                            DataKeyNames="FileID,CustPo,PoLine,CustPart,UM,Qad,Price,Currency,Location,Qty,DateCode,FOB,CreateBy,CreateName,CreateDate"
                            OnRowDeleting="gvImport_RowDeleting" AllowPaging="False" PageSize="20" OnPageIndexChanging="gvImport_PageIndexChanging" OnRowCancelingEdit="gvImport_RowCancelingEdit" OnRowEditing="gvImport_RowEditing" OnRowUpdating="gvImport_RowUpdating">
                            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <EmptyDataTemplate>
                                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                    GridLines="Vertical">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="center" Text="订单号" Width="110px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="行号" Width="30px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="物料号" Width="110px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="单位" Width="30px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="QAD" Width="110px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="价格" Width="30px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="币种" Width="30px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="单位" Width="30px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="位置" Width="30px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="数量" Width="30px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="周期章" Width="110px"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="CustPo" HeaderText="订单号" ReadOnly="true">
                                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PoLine" HeaderText="行号" ReadOnly="true">
                                    <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CustPart" HeaderText="物料号" ReadOnly="true">
                                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UM" HeaderText="单位" ReadOnly="true">
                                    <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Qad" HeaderText="QAD" ReadOnly="true">
                                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Price" HeaderText="价格" ReadOnly="true">
                                    <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Currency" HeaderText="币种" ReadOnly="true">
                                    <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Location" HeaderText="位置" ReadOnly="true">
                                    <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Qty" HeaderText="数量" ReadOnly="true">
                                    <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DateCode" HeaderText="周期章" ReadOnly="true">
                                    <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FOB" HeaderText="FOB价">
                                    <HeaderStyle Width="40px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreateDate" HeaderText="上传日期" ReadOnly="true">
                                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreateName" HeaderText="创建人" ReadOnly ="true">
                                    <HeaderStyle Width="60px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CommandField ShowEditButton="True">
                                    <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="Del">
                                    <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>问题描述</td>
                    <td colspan="16">
                        <asp:TextBox ID="txtProblemContent" runat="server" CssClass="SmallTextBox5" TextMode="MultiLine" Width="650px" Height="150px" MaxLength="2000"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>上传</td>
                    <td colspan="16">
                        <input id="filename" runat="server" style="width: 580px;" name="resumename" cssclass="SmallTextBox" type="file" />
                        <asp:Button ID="btnUpload" runat="server" Text="上传" CssClass="SmallButton3" OnClick="btnUpload_Click" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="16">
                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                            DataKeyNames="ID,GuestComp_No,GuestComp_FileName,GuestComp_FilePath,createBy,createName,createDate"
                            OnRowCommand="gv_RowCommand" OnRowDeleting="gv_RowDeleting"
                            AllowPaging="False" PageSize="20">
                            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <EmptyDataTemplate>
                                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                    GridLines="Vertical">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="center" Text="文件名" Width="300px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="上传日期" Width="200px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="创建人" Width="120px"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="GuestComp_FileName" HeaderText="文件名" ReadOnly="true">
                                    <HeaderStyle Width="240px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="240px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="createDate" HeaderText="上传日期" ReadOnly="true">
                                    <HeaderStyle Width="170px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="170px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="createName" HeaderText="创建人" ReadOnly="true">
                                    <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:ButtonField Text="View" HeaderText="查看" CommandName="View">
                                    <ControlStyle Font-Bold="False" Font-Underline="True" />
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:ButtonField>
                                <asp:TemplateField HeaderText="删除">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                                            CommandName="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;"></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="SmallButton3" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="SmallButton3" Visible="false" OnClick="btnEdit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" OnClick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidGuestCompNo" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidType" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
