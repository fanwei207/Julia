<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_FinPrice.aspx.cs" Inherits="price_pcm_FinPrice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("input[name$='chkAll']:eq(1)").remove();
            $("#chkAll").click(function () {
                $("#gvDet input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked", $(this).prop("checked"))
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px">
                    <asp:RadioButton ID="radioPrice" runat="server" Text="报价" GroupName="PRICE"  AutoPostBack="true"
                          Visible="false"/>
                    <asp:RadioButton ID="radioCheckPrice" Text="核价" runat="server" GroupName="PRICE" AutoPostBack="true" Checked="true"
                         />
                </td>
                <td style="width: 200px">
                    状态:<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                    <asp:ListItem Text="未处理" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="已处理" Value="1"></asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td style="width: 200px">
                    部件号<asp:TextBox ID="txtItemCode" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>(*)
                </td>
                <td style="width: 200px">
                    QAD号<asp:TextBox ID="txtPart" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="2"></asp:TextBox>
                </td>
                <td align="left" style="width: 200px">
                    供应商:<asp:TextBox ID="txtVender" runat="server" Width="75px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td align="left" style="width: 500px; height: 26px;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="50px"
                        OnClick="btnSearch_Click" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnHandle" runat="server" Text="处理" OnClick="btnHandle_Click"
                        Width="50px" CssClass="SmallButton2" Visible="true" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton2" 
                        Text="忽略" Width="50px" onclick="btnCancel_Click"></asp:Button>
                    &nbsp; &nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" 
                        Text="价格导出" Width="50px" onclick="btnExport_Click"></asp:Button>
                    &nbsp; &nbsp;

                </td>
            </tr>
        </table>
        <asp:GridView ID="gvDet" runat="server" Width="1800px" AllowSorting="True" 
            AutoGenerateColumns="False" AllowPaging="true" PageSize="100"
        CssClass="GridViewStyle GridViewRebuild"  DataKeyNames="isout,part,checkPriceToQAD,Vender,DetId,Price,PriceSelf,PriceDiscount,CheckPrice,statusName,InfoFrom" 
        EmptyDataText="No data"  
            onrowdatabound="gvDet_RowDataBound" onrowcommand="gvDet_RowCommand" 
            onpageindexchanging="gvDet_PageIndexChanging">
        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
        <RowStyle CssClass="GridViewRowStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />
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
            <asp:BoundField HeaderText="QAD号" DataField="part" ReadOnly="True">
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
                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                <ItemStyle HorizontalAlign="Center" Width="200px" />
            </asp:BoundField>
             <asp:TemplateField HeaderText="供应商指定">
                <ItemTemplate>
                    <asp:Label ID="lbInfoFrom" runat="server" Text='<%# Eval("InfoFrom")%>'></asp:Label> 
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
                <HeaderStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="状态" DataField="statusName" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
                                    <asp:TemplateField HeaderText="历史价格">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnHist" runat="server" CommandName="lkbtnHist" CommandArgument='<%#  ((GridViewRow) Container).RowIndex %>'
                            Text="历史"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField HeaderText="处理日期" DataField="LoadDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="单位" DataField="UM" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="币种" DataField="Curr" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="折扣" DataField="PriceDiscount" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                <ItemStyle HorizontalAlign="Center" Width="200px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="核价" DataField="CheckPrice" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="财务核价" DataField="FinCheckPrice" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="生效日期">
                <ItemTemplate>
                    <asp:Textbox ID="txtStartDate" runat="server" Text='<%# Eval("startDate")%>'  CssClass="Date" Width="80px"></asp:Textbox>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
                <HeaderStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="失效日期">
                <ItemTemplate>
                    <asp:Textbox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate")%>'  CssClass="Date" Width="80px"></asp:Textbox>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
                <HeaderStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
    </form>
</body>
</html>
