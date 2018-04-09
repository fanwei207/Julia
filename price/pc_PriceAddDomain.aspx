<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_PriceAddDomain.aspx.cs" Inherits="price_pc_PriceAddDomain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <div style="width:1200px;">
                <!--查询框-->
                <span>部件号:</span>(*)&nbsp;
            <asp:TextBox ID="txtCode" runat="server" Width="80px" CssClass="SmallTextBox5" TabIndex ="1"></asp:TextBox>&nbsp;
            <span>QAD:</span>(*)&nbsp;
            <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox5" Width="100px" TabIndex ="2">  </asp:TextBox>&nbsp;
           <span>供应商：</span>(*)&nbsp;
            <asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox5" Width="100px" TabIndex ="3"></asp:TextBox>&nbsp;
            <span>供应商名称：</span>(*)&nbsp;
            <asp:TextBox ID="txtVenderName" runat="server" CssClass="SmallTextBox5" Width="100px" TabIndex ="4"></asp:TextBox>&nbsp;
            &nbsp;
            <%--<span>有新增域：</span> &nbsp;--%>
            <asp:CheckBox ID="chkIsAddDomai" runat="server" Checked="true" AutoPostBack="true"  Visible="false" TabIndex ="5" Text="有新增的域" OnCheckedChanged="chkIsAddDomai_CheckedChanged"/>&nbsp;&nbsp;
            <asp:Button ID="btnSelect" runat="server" CssClass="SmallButton2" Width="60px" Text="查询" OnClick="btnSelect_Click" TabIndex ="6" />&nbsp;
            <asp:Button ID="btnCimload" runat="server" CssClass=" SmallButton2" Width="80px" Text="导出cimload"  TabIndex ="7" OnClick="btnCimload_Click"/>&nbsp;
             <asp:Button ID="btnHandle" runat="server" CssClass=" SmallButton2" Width="80px" Text="处理"  TabIndex ="8" OnClick="btnHandle_Click" />&nbsp;
            </div>
            <div>
                <!--gv-->
                <asp:GridView ID="gvDet" runat="server" Width="1200px" AllowSorting="True"
                    AutoGenerateColumns="False" DataKeyNames="pc_part,pc_list"
                    CssClass="GridViewStyle " 
                    EmptyDataText="No data" OnRowDataBound="gvDet_RowDataBound">
                    <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <EmptyDataTemplate>
                        <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="1800px"
                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                            <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                <asp:TableCell HorizontalAlign="Center" Text="没有找到数据"></asp:TableCell>
                            </asp:TableFooterRow>
                        </asp:Table>

                    </EmptyDataTemplate>
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
             <asp:BoundField HeaderText="QAD号" DataField="pc_part" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="部件号" DataField="code" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                <ItemStyle HorizontalAlign="Center" Width="120px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商" DataField="pc_list" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商名称" DataField="ad_name" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                <ItemStyle HorizontalAlign="Center" Width="200px" />
            </asp:BoundField>
              <asp:BoundField HeaderText="单位" DataField="pc_um" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="币种" DataField="pc_curr" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
              <asp:BoundField HeaderText="价格" DataField="pc_price" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
             <asp:BoundField HeaderText="折扣表" DataField="pc_amt" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
               <asp:BoundField HeaderText="原价" DataField="pc_price1" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
                 <asp:BoundField HeaderText="起始时间" DataField="pc_start" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="终止时间" DataField="pc_expire" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
                </Columns>
                </asp:GridView>

            </div>
        </div>
    </form>
                     <script type="text/javascript">
                         <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>

</body>
</html>
