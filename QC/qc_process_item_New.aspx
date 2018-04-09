<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_process_item_New.aspx.cs"
    Inherits="qc_process_item_New" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function SetTxtValue(obj) {
            var index;
            var parent = obj.parentNode.parentNode.parentNode;

            var txt = new Array();
            var chk = new Array();

            var txtArray = document.getElementsByTagName("input");
            var chkArray = parent.getElementsByTagName("input");
            if (txtArray && txtArray.length > 0 && chkArray && chkArray.length > 0) {
                for (i = 0; i < txtArray.length; i++) {
                    if (txtArray[i].type == "text") {
                        txt.push(txtArray[i]);
                    }
                }

                for (i = 0; i < chkArray.length; i++) {
                    if (chkArray[i].type == "checkbox") {
                        chk.push(chkArray[i]);
                    }
                }

                for (j = 0; j < chk.length; j++) {
                    if (chk[j] == obj) {
                        index = j;
                    }
                }
                index = parseInt(index) + 1;
                txt[index].value = obj.checked;
            }

        }
    </script>
    <style type="text/css">
        .fixTitle
        {
            position: relative;
            top: expression(this.offsetParent.scrollTop-1);
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <div style="width: 738px;" class="MainContent_outer">
            <table cellspacing="0" cellpadding="0" style="width: 736px; border: none;" class="MainContent_inner">
                <tr class="MainContent_top">
                    <td align="left">
                        加工单号:<asp:Label ID="lblOrder" runat="server" Text="Label" Width="129px"></asp:Label>
                    </td>
                    <td align="left">
                        ID号:
                        <asp:Label ID="lblLine" runat="server" Text="Label" Width="129px"></asp:Label>
                    </td>
                    <td align="left">
                        损坏数:<asp:Label ID="lblQty" runat="server" Text="Label" Width="107px"></asp:Label>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr style="height: 40px;">
                    <td align="left" colspan="5">
                        工&nbsp; 序:&nbsp;<asp:DropDownList ID="dropType" runat="server" AutoPostBack="True"
                            DataTextField="typeName" DataValueField="typeID" OnSelectedIndexChanged="dropType_SelectedIndexChanged"
                            Width="80px">
                        </asp:DropDownList>
                        &nbsp; &nbsp; 检验员:<asp:TextBox ID="txtInspector" runat="server" CssClass="SmallTextBox"
                            Width="76px"></asp:TextBox>
                        &nbsp; &nbsp; 日期:<asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date"
                            Width="98px" onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox>&nbsp;&nbsp;
                        <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="保存" Visible="True"
                            OnClick="btnSave_Click" />&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="SmallButton3" Text="关闭" Visible="True" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="vertical-align: top;">
                        <table style="height: 400px">
                            <tr align="left">
                                <td style="height: 148px" valign="top">
                                    <asp:DataGrid ID="dgProcedure" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                        OnItemDataBound="dgProcedure_ItemDataBound" PageSize="19" DataKeyField="prcItemID"
                                        OnItemCommand="dgProcedure_ItemCommand" ShowFooter="True">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <ItemStyle CssClass="GridViewRowStyle" />
                                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:BoundColumn HeaderText="编号">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="prcdName" HeaderText="工序名称">
                                                <HeaderStyle Width="110px" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="合计">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTotal" runat="server" CssClass="SmallTextBox" Width="55px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Width="60px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAdd" runat="server" CommandName="Add" Text="编辑" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="prcItemID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="prcdID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="prcNum" Visible="False"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                                <td style="height: 148px; width: 463px; overflow: scroll;" valign="top">
                                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" ScrollBars="Auto" Height="421px"
                                        HorizontalAlign="Left" Width="100%">
                                        <asp:DataGrid ID="dgDefect" runat="server" AutoGenerateColumns="False" PageSize="19"
                                            OnItemDataBound="dgDefect_ItemDataBound" ShowFooter="True" CssClass="GridViewStyle">
                                            <FooterStyle CssClass="GridViewFooterStyle" />
                                            <ItemStyle CssClass="GridViewRowStyle" />
                                            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                                            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
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
                                                        <asp:TextBox ID="txtNum" runat="server" CssClass="SmallTextBox" Width="60px">0</asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="70px" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="dItemID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="pd_num" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Label ID="lblID" runat="server" Text="0" Visible="False" Width="107px"></asp:Label>
        <asp:CheckBox ID="chkFlag" runat="server" Visible="False" /></form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
