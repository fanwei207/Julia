<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_MadeInquiry.aspx.cs" Inherits="price_pcm_MadeInquiry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                    
                    <asp:Button id="btnAddIM" runat="server" Text="生成询价单" CssClass="SmallButton2" 
                        onclick="btnAddIM_Click"/>
                    
                </td>
                <td> 
                     <asp:Button id="btnReturn" runat="server" Text="返回" CssClass="SmallButton2" 
                         onclick="btnReturn_Click"/>
                </td>
            </tr>

        
        </table>
        </div>
        <asp:GridView ID="gvNotInquiryList" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="false"
                 DataKeyNames="DetId,isout" OnRowDataBound="gvNotInquiryList_RowDataBound" >
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
                             <asp:BoundField HeaderText="QAD" DataField="Part">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="部件号" DataField="ItemCode">
                                <HeaderStyle Width="67px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="需求规格" DataField="Formate">
                                <HeaderStyle Width="300px" />
                                
                            </asp:BoundField>
                            <asp:BoundField HeaderText="期望价格" DataField="applyPrice">
                                <HeaderStyle Width="100"  />
                                  <ItemStyle  HorizontalAlign="right" />
                            </asp:BoundField>
                           <asp:BoundField HeaderText="货币" DataField="Curr">
                                <HeaderStyle Width="50px" />
                            </asp:BoundField>
                              <asp:BoundField HeaderText="单位" DataField="UM">
                                <HeaderStyle Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="详细描述" DataField="ItemDescription">
                                <HeaderStyle Width="500px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="描述1" DataField="ItemDesc1">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="描述2" DataField="ItemDesc2">
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
