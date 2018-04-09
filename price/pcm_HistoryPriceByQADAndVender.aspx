<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_HistoryPriceByQADAndVender.aspx.cs" Inherits="price_pcm_HistoryPriceByQADAndVender" %>

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
           
            <div>
                <asp:GridView ID="gvPcMstr" runat="server" Width="800px" AllowSorting="True"
                    AutoGenerateColumns="False" AllowPaging="true" PageSize="8"
                    CssClass="GridViewStyle GridViewRebuild" DataKeyNames="pc_list"
                    EmptyDataText="No data">
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                       <asp:BoundField HeaderText="QAD" DataField="pc_part">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="供应商" DataField="pc_list">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="供应商名称" DataField="ad_name">
                            <HeaderStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="单位" DataField="pc_um">
                            <HeaderStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="价格" DataField="pc_price">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="币种" DataField="pc_curr">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="域" DataField="pc_domain">
                            <HeaderStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="开始时间" DataField="pc_start">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="结束时间" DataField="pc_expire">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                    </Columns>


                </asp:GridView>
            </div>

        </div>
    </form>

    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
     
    </script>
</body>
</html>
