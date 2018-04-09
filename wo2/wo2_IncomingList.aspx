<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_IncomingList.aspx.cs" Inherits="wo2_wo2_IncomingList" %>

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
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>
                    <label>工单号：<asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label>
                </td>
                <td>
                    <label>ID号：<asp:TextBox ID="txtLot" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label>
                </td>
                 <td>
                    <label>物料号：<asp:TextBox ID="txtPart" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label>
                </td>
                 <td>
                    <label>供应商号：<asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label>
                </td>
                 <td>
                    <label>供应商名称：<asp:TextBox ID="txtVenderName" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label>
                </td>
                 <td style="width:250px">
                    <label >上线日期：<asp:TextBox ID="txtActDateFrom" runat="server" 
                        CssClass="SmallTextBox Date Param" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtActDateTo" runat="server" CssClass="SmallTextBox Date Param"
                            Height="20px" Width="80px"></asp:TextBox></label>
                </td>
                  <td style="width:250px">
                    <label >下线日期：<asp:TextBox ID="txtOffBegin" runat="server" 
                        CssClass="SmallTextBox Date Param" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtOffEnd" runat="server" CssClass="SmallTextBox Date Param"
                            Height="20px" Width="80px"></asp:TextBox></label>
                </td>
                <td>
                    <asp:Button  ID="btnSelect" runat="server" CssClass="SmallButton2" Text="查询" Width ="80px" OnClick="btnSelect_Click"/>
                </td>
                 <td>
                    <asp:Button  ID="benExcel" runat="server" CssClass="SmallButton2" Text="Excel" Width ="80px" OnClick="benExcel_Click"/>
                </td>

            </tr>
        </table>
            <asp:GridView ID="gvDet" runat="server"  AllowSorting="True" 
            AutoGenerateColumns="False" AllowPaging="true" PageSize="20"
        CssClass="GridViewStyle GridViewRebuild" 
         DataKeyNames ="wo_IncomingID,wo_nbr,wo_lot,wo_QAD,ad_name,wo_checkNum,wo_vender"
        EmptyDataText="No data"  
             onrowcommand="gvDet_RowCommand" 
            onpageindexchanging="gvDet_PageIndexChanging">
        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
        <RowStyle CssClass="GridViewRowStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />
        <Columns>

            <asp:BoundField HeaderText="工单号" DataField="wo_nbr" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ID号" DataField="wo_lot" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="QAD号" DataField="wo_QAD" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商" DataField="wo_vender" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
           
            <asp:BoundField HeaderText="供应商名称" DataField="ad_name" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                <ItemStyle HorizontalAlign="Center" Width="200px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="部门" DataField="deName" ReadOnly="True" >
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="产线" DataField="wo_line" ReadOnly="True" >
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="检验数量" DataField="wo_checkNum" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="不合格数量" DataField="wo_num" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
                        <asp:BoundField HeaderText="缺陷率（%）" DataField="rate" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="详细">
                <ItemTemplate>
                    <asp:LinkButton ID ="lkbDet" runat="server" CommandName="det"  Text ="详情" CommandArgument='<%# Container.DataItemIndex  %>' ></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" Font-Underline="true" />
                <HeaderStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
           
            <asp:BoundField HeaderText="上线日期" DataField="wo_online" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="下线日期" DataField="wo_offline" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    </div>
    </form>
        <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
