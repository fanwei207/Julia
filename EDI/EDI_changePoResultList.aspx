<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI_changePoResultList.aspx.cs" Inherits="EDI_EDI_changePoResultList" %>

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
        <table>
            <tr>
             <td>
                    <label>NO.<asp:TextBox ID="txtPocCode" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label>
                </td>
               <td>
                    <label>PO number<asp:TextBox ID="txtPoNbr" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label>
                </td>
              <td>
                    <label>resultDate</label>
                    <asp:TextBox ID="txtDateBegin" runat="server" CssClass="SmallTextBox5 Date"  Width="80px"></asp:TextBox>
                    ---
                    <asp:TextBox ID="txtDateEnd" runat="server" CssClass="SmallTextBox5 Date"  Width="80px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="Query" CssClass="SmallButton3" OnClick="btnQuery_Click"  Width="80px"/>

        </table>
                                      <asp:GridView ID="gvlist" runat="server"  AllowPaging="true"  AutoGenerateColumns="False" Width="1500px"
                                DataKeyNames="id,poLine,partNbr,sku,qadPart,ordQty,um,price,dueDate,reqDate,remark,addflag,isdelete" OnPageIndexChanging="gvlist_PageIndexChanging"
                                OnRowDataBound="gvlist_RowDataBound"  PageSize="20"
                                CssClass="GridViewStyle AutoPageSize GridViewRebuild">
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <EmptyDataTemplate>
                                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                        GridLines="Vertical">
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="center" Text="Line" Width="30px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Cust Part" Width="150px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="SKU" Width="50px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="QAD" Width="150px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Qty" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="UM" Width="30px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Price" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Req Date" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Due Date" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Remarks" Width="150px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Ord Date" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Ord By" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="" Width="100px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="" Width="30px"></asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField HeaderText="NO." DataField="poc_Code" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="PoNbr" DataField="po_nbr" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="reason" DataField="poc_reason" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Line" DataField="poLine" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:BoundField>


                                     <asp:TemplateField HeaderText="Change type" >
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbChangeType" runat="server"></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cust Part" SortExpression="partNbr">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblpartNbr" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkpartNbr" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="SKU" SortExpression="sku">
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSKU" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkSKU" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="QAD" SortExpression="qadPart">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblqadPart" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkqadPart" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty" SortExpression="ordQty">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblordQty" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkordQty" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UM" SortExpression="um">
                                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblum" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkum" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Price" SortExpression="price">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblprice" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkprice" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Req Date" SortExpression="reqDate">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblreqDate" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkreqDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Due Date" SortExpression="dueDate">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldueDate" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkdueDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="CreatedBy" DataField="createdName" ReadOnly="True" >
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="CreatedDate" DataField="createdDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="ResultDate" DataField="poc_resuleDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Remarks" SortExpression="remark">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblremark" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkremark" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    

                                  
                                </Columns>
                            </asp:GridView>
    </div>
    </form>
</body>
</html>
