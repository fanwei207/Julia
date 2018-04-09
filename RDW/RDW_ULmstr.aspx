<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_ULmstr.aspx.cs" Inherits="RDW_RDW_ULmstr" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 163px;
        }
    </style>
</head>
<body>
     <div>
          <form id="Form1" method="post" runat="server">
                <table>
                    <tr>
                        <td>
                            产品规格:<asp:TextBox ID="txtProject" runat="server" Width="300px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                        </td>
                        <td>
                            完成时间:<asp:TextBox ID="txtDate1" runat="server" Width="100px" CssClass="SmallTextBox Date"
                        TabIndex="1"></asp:TextBox>-
                            <asp:TextBox ID="txtDate2" runat="server" Width="100px" CssClass="SmallTextBox Date"
                        TabIndex="1"></asp:TextBox>
                        </td>
                        <td>
                            UL Model:<asp:TextBox ID="txtModel" runat="server" Width="100px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                            <asp:Label ID="lblmid" runat="server" Text="" Visible ="false"></asp:Label>
                        </td>
                         <td>
                           &nbsp;&nbsp; <asp:Button ID="btnSelect" runat="server" CssClass="SmallButton2" TabIndex="9" Text="Query"
                        Width="50px" CausesValidation="false" OnClick="btnSelect_Click"  />

                              &nbsp;&nbsp; <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" TabIndex="9" Text="New"
                        Width="50px" CausesValidation="false" OnClick="btnNew_Click"  />
                              &nbsp;&nbsp; <asp:Button ID="btnback" runat="server" CssClass="SmallButton2" TabIndex="9" Text="Back"
                        Width="50px" CausesValidation="false" OnClick="btnback_Click"   />
                              &nbsp;&nbsp; <asp:Button ID="btnEXCEL" runat="server" CssClass="SmallButton2" TabIndex="9" Text="EXCEL"
                        Width="50px" CausesValidation="false" OnClick="btnEXCEL_Click"  />
                        </td>
                    </tr>

                </table>

    <asp:GridView ID="gvRDW" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="GridViewStyle" PageSize="20" 
                Width="1480px" DataKeyNames="UL_msrtID,UL_bsdnbr,UL_bsddate,UL_id" OnRowCommand="gvRDW_RowCommand" AllowPaging="True" OnPageIndexChanging="gvRDW_PageIndexChanging" 
               >
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="No." Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Description" Width="370px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="StartDate" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="EndDate" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Complete" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Duration" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <%--  <asp:TableCell Text="Pre" Width="40px" HorizontalAlign="center"></asp:TableCell>--%>
                            <asp:TableCell Text="Member" Width="130px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Approver" Width="130px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                     <asp:BoundField DataField="UL_Project" HeaderText="产品规格" HtmlEncode="False">
                        <HeaderStyle Width="400px" HorizontalAlign="Center" />
                        <ItemStyle Width="400px" HorizontalAlign="Left" />
                    </asp:BoundField>

                      <asp:BoundField DataField="UL_bsddate" HeaderText="样品寄出日期" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="出运单号">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSample" runat="server" 
                                CommandName="mySample" Font-Bold="False" Font-Size="8pt" Font-Underline="True"
                                ForeColor="Black" Text='<%# Bind("UL_bsdnbr") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>

                  

                     <asp:TemplateField HeaderText="文件发出日期">
                        <ItemTemplate>
                            <asp:LinkButton ID="Lbtn_Sample" runat="server" 
                                CommandName="myDoc" Font-Bold="False" Font-Size="8pt" Font-Underline="True"
                                ForeColor="Black" Text='<%# Bind("UL_docDate") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="UL Model">
                        <ItemTemplate>
                            <asp:LinkButton ID="Lbtn_Model" runat="server" 
                                CommandName="myModel" Font-Bold="False" Font-Size="8pt" Font-Underline="True"
                                ForeColor="Black" Text='<%# Bind("UL_model") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                     <asp:BoundField DataField="UL_DriverJXL" HeaderText="DriverJXL" HtmlEncode="False">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField DataField="UL_LEDJXL" HeaderText="LEDJXL" HtmlEncode="False">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField DataField="UL_DriverLv" HeaderText="DriverLv" HtmlEncode="False">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField DataField="UL_LEDLv" HeaderText="LEDLv" HtmlEncode="False">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UL_FinDate" HeaderText="UL完成时间" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="关联QAD">
                        <ItemTemplate>
                            <asp:LinkButton ID="Lbtn_QAD" runat="server" 
                                CommandName="myQAD" Font-Bold="False" Font-Size="8pt" Font-Underline="True"
                                ForeColor="Black" Text="维护/查看"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    
        &nbsp;&nbsp;&nbsp;
    
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
