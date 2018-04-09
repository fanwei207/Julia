<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo_actualPart.aspx.cs" Inherits="plan_wo_actualPart" %>

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
         <asp:Button ID="btn_back" runat="server" Text="Back" CssClass="smallbutton2" OnClick="btn_back_Click"/>
     <asp:GridView ID="gvlist" runat="server"  AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize"
             Width="1000px" DataKeyNames="wod_part" OnRowCommand="gvlist_RowCommand"  >
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="1500px">
                    <asp:TableRow>          
                        <asp:TableCell HorizontalAlign="center" Text="加工单" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ID" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="QAD" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="下达日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="实际日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="变更日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建人" Width="60px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="wod_nbr" HeaderText="工单">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wod_lot" HeaderText="ID号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
               
                 <asp:TemplateField HeaderText="发放零件">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkQad" runat="server" CommandName="Link" Font-Underline="True"
                            Text='<%# Bind("wod_part") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="pt_desc1" HeaderText="零件描述" >
                    <HeaderStyle Width="280px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="left" />
                </asp:BoundField>
                  <asp:BoundField DataField="wod_qty_req" HeaderText="需求数量" >
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="right" />
                </asp:BoundField>
                  <asp:BoundField DataField="wod_qty_iss" HeaderText="发放数量" >
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="right" />
                </asp:BoundField>
                  <asp:BoundField DataField="wod_iss_date" HeaderText="发放日期" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                
                  <asp:BoundField DataField="wod_loc" HeaderText="批序号" >
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="right" />
                </asp:BoundField>
               
                
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>

