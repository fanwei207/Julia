<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_PartCheckPriceList.aspx.cs" Inherits="price_pcm_PartCheckPriceList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
                $(function () {
                    $("#chkAll").click(function () {
                        $("#gv input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked", $(this).prop("checked"))
                    })
                })
    </script>
</head>
<body>
    <div>
        <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 150px">
                    部件号<asp:TextBox ID="txtItemCode" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>(*)
                </td>
                <td style="width: 150px">
                    QAD号<asp:TextBox ID="txtPart" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="2"></asp:TextBox>(*)
                </td>
                <td>
                    供应商<asp:TextBox id="txtVender" runat="server" CssClass = "SmallTextBox5" Width="100" 
                        TabIndex="3"></asp:TextBox>(*)&nbsp;&nbsp;
                </td>
                <td style="width: 170px">
                    询价单号<asp:TextBox ID="txtInquiry" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="4"></asp:TextBox>(*)&nbsp;&nbsp;

                </td>
                <td  style="width: 200px">
                   供应商名称<asp:TextBox id="txtVenderName" runat="server" CssClass = "SmallTextBox5" Width="100" 
                        TabIndex="3"></asp:TextBox>(*)&nbsp;&nbsp;
                    &nbsp;
                </td>
                <td align="left" style="width: 150px; height: 26px;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" 
                        OnClick="btnSearch_Click" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnApply" runat="server" Text="提交" OnClick="btnApply_Click"  CssClass="SmallButton2"/>
<%--                    <asp:Button ID="BtnExport" runat="server" CssClass="SmallButton2" 
                        Text="Export" Width="50px" onclick="BtnExport_Click"></asp:Button>--%>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowPaging="false" AutoGenerateColumns="False" PageSize="25"
            CssClass="GridViewStyle" runat="server" Width="1700px" DataKeyNames="DetId,IMID,isout"
            OnRowCommand="gv_RowCommand" 
            onrowdatabound="gv_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                          <asp:TemplateField>
                    <HeaderTemplate>
                            <input id="chkAll" type="checkbox">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk" runat="server"/>
                    </ItemTemplate>
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                  <asp:BoundField HeaderText="询价单号" DataField="IMID" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="QAD号" DataField="Part" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部件号" DataField="ItemCode" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
             
                <asp:BoundField HeaderText="供应商" DataField="Vender" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="供应商名称" DataField="VenderName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="核价" DataField="CheckPrice" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="期望价格" DataField="applyPrice" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="期望价格开始时间" DataField="applyPriceStarDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="期望价格截止时间" DataField="applyPriceEndDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                  <asp:BoundField HeaderText="是否驳回" DataField="isFinCheckPriceReject" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="驳回信息" DataField="FinCheckPriceRejectMsg" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                </asp:BoundField>
                   <asp:BoundField HeaderText="详细描述" DataField="ItemDescription" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="描述1" DataField="ItemDesc1" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="描述2" DataField="ItemDesc2" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="技术规格" DataField="Formate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
