<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCF_MadeInquiryDet.aspx.cs" Inherits="price_PCF_madeInquiryDet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <base target="_self">
       <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">
        $(function () {
            $("#chkAll").click(function () {
                $("#gvNotInquiryList input[type='checkbox'][id$='chk']").prop("checked", $(this).prop("checked"))
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div align="left">
        <table>
            <tr >
                <td> 
                    供应商：&nbsp;&nbsp;<asp:Label ID="lbVender"  runat="server"></asp:Label>&nbsp;&nbsp;
                </td>
                <td> 
                    供应商名：&nbsp;&nbsp;<asp:Label ID="lbVenberName"  runat="server"></asp:Label>&nbsp;&nbsp;
                </td>
                <td>
                    成单限制：&nbsp;&nbsp;<asp:TextBox runat="server" ID ="txtQty" Width ="60px" OnTextChanged="txtQty_TextChanged" CssClass="number">200</asp:TextBox>条&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                     <asp:Button id="btnSelect" runat="server" Text="刷新" CssClass="SmallButton2" 
                        Width="100px" OnClick="btnSelect_Click"/>

                </td>
                <td>
                    
                    <asp:Button id="btnAddIM" runat="server" Text="生成询价单" CssClass="SmallButton2" 
                        onclick="btnAddIM_Click" Width="100px"/>
                    
                </td>
                <td> 
                     <asp:Button id="btnReturn" runat="server" Text="返回" CssClass="SmallButton2" 
                         onclick="btnReturn_Click" Width="100px"/>
                </td>
            </tr>

        
        </table>
        </div>
        <asp:GridView ID="gvNotInquiryList" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="false"
                 DataKeyNames="PCF_ID,PCF_sourceID" OnRowCommand="gvNotInquiryList_RowCommand"  >
                 <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:TemplateField>
                                <HeaderTemplate>
                                     <input id="chkAll" type="checkbox">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="QAD" DataField="PCF_part">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
<%--                            <asp:BoundField HeaderText="部件号" DataField="ItemCode">
                                <HeaderStyle Width="67px" />
                            </asp:BoundField>--%>
                             <asp:BoundField HeaderText="基本单位" DataField="PCF_ptum">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                           
                            <asp:BoundField HeaderText="采购单位" DataField="PCF_um">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="转换因子" DataField="PCF_changeFor">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                          <asp:TemplateField HeaderText="申请单">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbApply" runat="server" CommandName="lkbApply" CommandArgument='<%# Eval("PCF_sourceID") %>'
                                        Text="查看"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="规格" DataField="PCF_format">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="描述" DataField="PCF_desc">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                           <asp:BoundField HeaderText="用途" DataField="PCF_purpose">
                                    <HeaderStyle Width="300px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbDelete" runat="server" CommandName="lkbDelete" CommandArgument='<%# Eval("PCF_ID") %>'
                                        Text="删除"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="描述1" DataField="PCF_desc1">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="描述2" DataField="PCF_desc2">
                                <HeaderStyle Width="300" />
                            </asp:BoundField>
                            
                          

                
                </Columns>
                
                </asp:GridView>
    </div>
    </form>
     <script type="text/javascript">
         <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
