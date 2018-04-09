<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCD_210171View.aspx.cs" Inherits="IT_PCD_210171View" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
     <asp:GridView ID="gvlist" name="gvlist" runat="server"  AutoGenerateColumns="False"
                
                CssClass="GridViewStyle GridViewRebuild"   >
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="订单号" DataField="poNbr">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="行号" DataField="poLine">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="物料号" DataField="part">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="芯片板QAD" DataField="wodPart">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="需求数量" DataField="needQty">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="分配数量" DataField="issQty">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                       <asp:BoundField HeaderText="缺料量" DataField="lackQty">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="加工单" DataField="nbr">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                       <asp:BoundField HeaderText="ID" DataField="lot">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                      
                       <asp:BoundField HeaderText="类型" DataField="type">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="芯片板PCD" DataField="planDate" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                   
                    <asp:BoundField HeaderText="芯片" DataField="StrCol11">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="铝基板" DataField="StrCol12">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="3019物料" DataField="StrCol14">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Left" />
                    </asp:BoundField>
                   
                </Columns>
            </asp:GridView>
    </div>
    </form>
</body>
</html>