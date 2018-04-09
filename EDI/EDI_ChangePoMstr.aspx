<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI_ChangePoMstr.aspx.cs" Inherits="EDI_EDI_ChangePoMstr" %>

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
    <div>
        <table style="width:1000px">
            <tr>
                <td>
                    <label>NO.<asp:TextBox ID="txtPocCode" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label>
                </td>
               <td>
                    <label>PO number<asp:TextBox ID="txtPoNbr" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label>
                </td>
                <td><label>Status
                <asp:DropDownList ID="ddlStatu" runat="server" Width="85px" CssClass="Param">
                            <asp:ListItem Value="100">--</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">In Process</asp:ListItem>
                            <asp:ListItem Value="-10">Reject</asp:ListItem>
                            <asp:ListItem Value="10">Complete</asp:ListItem>
                            <asp:ListItem Value="-20">Cancel</asp:ListItem>
                        </asp:DropDownList></label></td>
                <td>
                    <label>Waiting For
                         <asp:DropDownList ID="ddlType" runat="server" Width="80px" CssClass="Param">

                            <asp:ListItem Value="0">--All--</asp:ListItem>
                             <asp:ListItem Value ="1">Manager</asp:ListItem>
                            <asp:ListItem Value="2">Review</asp:ListItem>
                            <asp:ListItem Value="3">Notice</asp:ListItem>
                             <asp:ListItem Value="4">Result</asp:ListItem>
                        </asp:DropDownList>
                    </label>
                </td>
                <td>
                    <label>CreatedDate</label>
                    <asp:TextBox ID="txtDateBegin" runat="server" CssClass="SmallTextBox5 Date"  Width="80px"></asp:TextBox>
                    ---
                    <asp:TextBox ID="txtDateEnd" runat="server" CssClass="SmallTextBox5 Date"  Width="80px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="Query" CssClass="SmallButton3" Width="60px" OnClick="btnQuery_Click" />

                </td>
                <td>
                    <asp:Button ID="btnToNew" runat="server" Text="New" CssClass="SmallButton3" Width="60px" OnClick="btnToNew_Click" />
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="SmallButton3" Width="60px" OnClick="btnExport_Click" />
                </td>
            </tr>
           
        </table>
        <asp:GridView ID="gvInfo" runat="server" CssClass="GridViewStyle GridViewRebuild"  Width="1100px"
            DataKeyNames="poc_id,poc_Code,po_nbr,poc_noticeIsAgree,poc_managerIsAgree,poc_status,poc_commit" AutoGenerateColumns="False" 
            PageSize="10" AllowPaging="true" OnPageIndexChanging="gvInfo_PageIndexChanging" OnRowDataBound="gvInfo_RowDataBound" OnRowCommand="gvInfo_RowCommand"  >
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="1200px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="No." Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="PoNbr" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Notice" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Applicant" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Application Date" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Reason" Width="200px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Detail" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Close" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Manager Agree By" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Manager Agree Date" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Notice By" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Notice Date" Width="80px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="12"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                  <asp:TemplateField HeaderText="No.">
                   <ControlStyle Font-Bold="False" Font-Size="12px"  Font-Underline="True"/>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnUpdate" runat="server" CommandName="lkbtnUpdate" CommandArgument='<%# Eval("poc_id") %>' 
                            Text='<%# Eval("poc_Code") %>' ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:BoundField HeaderText="PoNbr" DataField="po_nbr">
                    <HeaderStyle Width="80px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
               
                 <%--<asp:TemplateField HeaderText="Financial Controller">
                   <ControlStyle Font-Bold="False" Font-Size="12px"  />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID ="lbNoticeAgree" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Status">
                   <ControlStyle Font-Bold="False" Font-Size="12px"  />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID ="lbStatus" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField HeaderText="Applicant" DataField="poc_commitName">
                    <HeaderStyle Width="80px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="Application Date" DataField="poc_commitDate" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Reason" DataField="poc_reason">
                    <HeaderStyle Width="200px" />
                </asp:BoundField>
                  <asp:TemplateField HeaderText="Detail">
                   <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnDet" runat="server" CommandName="lkbtnDet" CommandArgument='<%# Eval("poc_id") %>' 
                            Text="Detail"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Close">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnClose" runat="server" CommandName="lkbtnClose" CommandArgument='<%# Eval("poc_id") %>'
                            Text="Close"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Manager Agree By" DataField="poc_managerName">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Manager Agree Date" DataField="poc_managerDate" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
               <%-- <asp:BoundField HeaderText="Financial Controller By" DataField="poc_noticeName">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Financial Controller Date" DataField="poc_noticeDate" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>--%>
               
                
            </Columns>
        </asp:GridView>
    </div>
    </form>
 <script type="text/javascript">
     <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body> 
</html>
