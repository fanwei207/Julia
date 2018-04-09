<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_product_input.aspx.cs"
    Inherits="QC_qc_product_input" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .fixTitle
        {
            position: relative;
            top: expression(this.offsetParent.scrollTop-1);
        }
    </style>
</head>
<body style="top: 0; bottom: 0; left: 0; right: 0;">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <div style="width: 1002px;" class="MainContent_outer">
            <table cellspacing="0" cellpadding="0" class="MainContent_inner" style="width: 1000px;
                border: none;">
                <tr class="MainContent_top">
                    <td align="left" style="width: 405px;">
                        ID号:
                        <asp:Label ID="lblLine" runat="server" Width="62px">2950608511</asp:Label>
                    </td>
                    <td style="width: 319px;">
                        加工单号:
                        <asp:Label ID="lblOrder" runat="server"></asp:Label>
                    </td>
                    <td style="width: 443px;">
                        产品名称:
                        <asp:Label ID="lblPart" runat="server" Text="301640040076001"></asp:Label>
                    </td>
                    <td style="width: 329px;">
                        生产批号:
                        <asp:Label ID="lblSerial" runat="server" Text="PH0001"></asp:Label>
                    </td>
                    <td style="width: 306px;">
                        检验批量:
                        <asp:Label ID="lblRcvd" runat="server" Text="0" Width="62px"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 50px;">
                    <td colspan="3">
                        缺陷种类:<asp:DropDownList ID="dropType" runat="server" Width="78px" DataTextField="typeName"
                            DataValueField="typeID" AutoPostBack="True" OnSelectedIndexChanged="dropType_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;抽样基数为100时的次品数:<asp:TextBox ID="txtHNum" runat="server"
                            CssClass="SmallTextBox" EnableViewState="False" Width="58px" onfocus="document.getElementById('lbError').style.display = 'none';">0</asp:TextBox>/100<asp:RegularExpressionValidator
                                ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtHNum" Display="Dynamic"
                                ErrorMessage="必须是数字" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                        <asp:Label ID="lbError" runat="server" Width="100px" ForeColor="Red" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 329px">
                        <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="保存" Visible="True"
                            OnClick="btnSave_Click" /> &nbsp; &nbsp; &nbsp; &nbsp;
                         <asp:Button ID="btnSize" runat="server" CssClass="SmallButton3" Text="尺寸" Visible="True" OnClick="btnSize_Click"  
                             />
                    </td>
                    <td style="width: 306px">
                        &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="SmallButton3" Text="关闭" Visible="True"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="vertical-align: top;">
                        <table>
                            <tr>
                                <td valign="top">
                                    <asp:DataGrid ID="dgProcedure" runat="server" AutoGenerateColumns="False" DataKeyField="prdItemID"
                                        OnItemCommand="dgProcedure_ItemCommand" OnItemDataBound="dgProcedure_ItemDataBound"
                                        CssClass="GridViewStyle" PagerStyle-Mode="NumericPages" PageSize="19">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                                        <ItemStyle CssClass="GridViewRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:BoundColumn HeaderText="编号">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="defName" HeaderText="缺陷名称">
                                                <HeaderStyle Width="90px" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="检验水平">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="dropLevel" runat="server" DataTextField="gbtLevel" DataValueField="gbtLevel"
                                                        Width="70px">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle Width="70px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="AQL值">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="dropAql" runat="server" DataTextField="AQL" DataValueField="AQL"
                                                        Width="70px">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle Width="70px" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="prdItemID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Level" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Aql" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="orig" HeaderText="检验数量">
                                                <HeaderStyle Width="60px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ac" HeaderText="AC">
                                                <HeaderStyle Width="30px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="re" HeaderText="RE">
                                                <HeaderStyle Width="30px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="result" HeaderText="检验结果">
                                                <HeaderStyle Width="60px" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAdd" runat="server" CommandName="Add" Text="编辑" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="defID" Visible="False"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                                <td valign="top" style="width: 400px">
                                    <asp:Panel ID="Panel1" runat="server" Height="413px" ScrollBars="Auto" Style="overflow: auto"
                                        Width="100%">
                                        <asp:DataGrid ID="dgDefect" runat="server" AutoGenerateColumns="False" OnItemDataBound="dgDefect_ItemDataBound"
                                            CssClass="GridViewStyle" PagerStyle-Mode="NumericPages" PageSize="19" ShowFooter="True">
                                            <FooterStyle CssClass="GridViewFooterStyle" />
                                            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                                            <PagerStyle CssClass="GridViewPagerStyle" />
                                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                                            <ItemStyle CssClass="GridViewRowStyle" />
                                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                                            <Columns>
                                                <asp:BoundColumn HeaderText="编号">
                                                    <HeaderStyle Width="40px" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="dItemName" HeaderText="缺陷内容">
                                                    <HeaderStyle Width="340px" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="数量">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNum" runat="server" CssClass="SmallTextBox" Width="66px" EnableViewState="False">0</asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="60px" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="dItemID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="defNum" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="dItemTcp" HeaderText="dItemTcp" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblID" runat="server" Text="0" Width="28px" Visible="False"></asp:Label>&nbsp;
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hidIsTimeUp" runat="server" />
        </div>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
