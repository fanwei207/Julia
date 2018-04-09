<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Npart_ShowPartMasterList.aspx.cs" Inherits="part_Npart_ShowPartMasterList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="complain.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
<%--     <script language="javascript" type="text/javascript">
         $(function () {
             $("input[name='gvDet$ctl01$chkAll']:eq(1)").remove();
             $("#gvDet_ctl01_chkAll").click(function () {
                 $("#gvDet input[type='checkbox'][id$='chk']").prop("checked", $(this).prop("checked"));
                 $("#gvDet input[type='checkbox'][id$='chk']").each(GetSelectedIndex);

                 event.stopPropagation();
             })
             $("#gvDet input[type='checkbox'][id$='chk']").change(GetSelectedIndex)
         })
    </script>--%>
</head>
<body>
   <form id="Form1" method="post" runat="server">
    <div>
            <div align="left">
                <table cellspacing="0" cellpadding="0" width="800px" bgcolor="white" border="0">
                <tr>
                    <td>模板</td>
                    <td>
                        <asp:DropDownList ID="ddlModule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" DataTextField="partTypeName" DataValueField="partTypeID">
                        </asp:DropDownList>
                    </td>
                    <td>申请号</td>
                    <td>
                        <asp:TextBox ID="txtApplyNo" runat="server" Width="100px" CssClass="SmallTextBox5"></asp:TextBox>
                    </td>
                    <td>QAD</td>
                    <td>
                        <asp:TextBox ID="txtQad" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>状态</td>
                    <td>
                        <asp:DropDownList ID="ddlSatus" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="----" Value="1"></asp:ListItem>
                            <asp:ListItem Text="驳回" Value="-10"></asp:ListItem>
                            <asp:ListItem Text="待提交" Value="0"></asp:ListItem>
                            <asp:ListItem Text="待电子工程师确认" Value="2"></asp:ListItem>
                            <asp:ListItem Text="待结构工程师确认" Value="3"></asp:ListItem>
                            <asp:ListItem Text="待包装工程师确认" Value="4"></asp:ListItem>
                            <asp:ListItem Text="待DCC确认" Value="5"></asp:ListItem>
                            <asp:ListItem Text="待供应商开发部确认" Value="6"></asp:ListItem>
                            <asp:ListItem Text="已完成" Value="7"></asp:ListItem>
                            <asp:ListItem Text="已通过" Value="20"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Width="80px" CssClass="SmallButton3" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnExport" runat="server" Width="80px" CssClass="SmallButton3" Text="导出" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
            <asp:DataGrid ID="gvDet" runat="server"
                Width="1500px" CssClass="GridViewStyle AutoPageSize" AllowPaging="True"
                PageSize="25"                 
                AutoGenerateColumns="False" 
                OnPageIndexChanged="gvDet_PageIndexChanged">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <ItemStyle CssClass="GridViewRowStyle" />
                <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <Columns>
                    <asp:BoundColumn DataField="partApplyCode" ReadOnly="True" HeaderText="<b>申请号</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="createdName" ReadOnly="True" HeaderText="<b>申请者</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="createdDate" HeaderText="<b>申请日期</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="descript" ReadOnly="True" HeaderText="<b>描述</b>">
                        <HeaderStyle Width="300px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="appvResult" HeaderText="<b>是否同意</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="appvResultReason" HeaderText="<b>拒绝原因</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="part" ReadOnly="True" HeaderText="<b>QAD</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="um" ReadOnly="True" HeaderText="<b>单位</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="addReason" ReadOnly="True" HeaderText="<b>新增原因</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="oldPart" HeaderText="<b>原QAD</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Manufacturer1" ReadOnly="True" HeaderText="<b>原厂1</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Model1" HeaderText="<b>型号1</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn> 
                    <asp:BoundColumn DataField="Manufacturer2" HeaderText="<b>原厂2</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Model2" ReadOnly="True" HeaderText="<b>型号2</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Manufacturer3" HeaderText="<b>原厂3</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn> 
                    <asp:BoundColumn DataField="Model3" HeaderText="<b>型号3</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="MPQ" ReadOnly="True" HeaderText="<b>最小包装量</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="MOQ" HeaderText="<b>最小订单量</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="vend" ReadOnly="True" HeaderText="<b>供应商</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="leadtime" HeaderText="<b>采购周期</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>                   
                </Columns>
            </asp:DataGrid>
        </div>

    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    <asp:HiddenField id="hidFlowID" runat="server"/>
    <asp:HiddenField ID ="hidNodeID" runat="server" />
    </div>
    </form>
</body>
</html>
