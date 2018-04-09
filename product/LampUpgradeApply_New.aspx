<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LampUpgradeApply_New.aspx.cs" Inherits="product_LampUpgradeApply_New" %>

<!DOCTYPE html>

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
            $("input[type='checkbox'][id='chkAll']:eq(1)").remove();
            $("#chkAll").click(function () {            
                $("#gv_product input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked", $(this).prop("checked"))
            })
        })
    </script>
    <style type="text/css">
        .style1
        {
            width: 60px;
        }
        .auto-style1 {
            width: 60px;
            height: 20px;
        }
        .auto-style2 {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table cellpadding="0" cellspacing="0" style="text-align: left">
            <tr>
                <td class="style1" align="right">
                    申&nbsp;&nbsp;请&nbsp;&nbsp;单：
                </td>
                <td class="style1">
                    <asp:TextBox ID="txt_nbr" runat="server" Width="149px" ReadOnly="true"></asp:TextBox>
                    <asp:Label ID="lbl_id" runat="server" Visible="false"></asp:Label>
                </td>
                <td align="right">
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </td>
                <td class="style3">
                   <asp:TextBox ID="txt_status" runat="server" Width="149px" ReadOnly="true"></asp:TextBox>
                   <asp:Label ID="lbl_Status" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1" align="right">
                    原裸灯号：
                </td>
                <td class="style1">
                    <asp:TextBox ID="txt_lampCode" runat="server" Width="149px"></asp:TextBox>
                    <asp:Label ID="lbl_lampCode" runat="server" Visible="false"></asp:Label>
                </td>
                <td align="right">
                    新裸灯号：
                </td>
                <td class="style3">
                   <asp:TextBox ID="txt_lampCodeNew" runat="server" Width="149px" ReadOnly="true"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td class="auto-style1" align="right">
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
                </td>
                <td colspan="3" class="auto-style2">
                    <asp:TextBox ID="txtRmks" runat="server" Width="598px"></asp:TextBox>
                </td>
            </tr>
            <tr id="trReason" runat="server">
                <td class="style1" align="right">
                    驳回意见：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtReason" runat="server"  Width="598px" Height="50px" TextMode="MultiLine" Wrap="true"></asp:TextBox>
                </td>
            </tr>
            <tr id="trApply" runat="server">
                <td colspan="5" align="center">
                    <asp:Button ID="btn_Save" runat="server" Text="保存" Width="53px" OnClick="btn_Save_Click"
                        CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Submit" runat="server" Text="提交" Width="53px" OnClick="btn_Submit_Click"
                        CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Confirm" runat="server" Text="导入" Width="53px" OnClick="btn_Confirm_Click"
                        CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Cancel" runat="server" CssClass="SmallButton2" 
                        OnClick="btn_Cancel_Click" OnClientClick="return confirm('确定取消该申请单');" 
                        Text="取消" Width="56px" />&nbsp; &nbsp;
                    <asp:Button ID="btn_Back" runat="server" Text="返回" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_Back_Click" />
                </td>
            </tr>
            <tr id="trReview" runat="server">
                <td colspan="5" align="center">
                    <asp:Button ID="btn_Packing" runat="server" Text="确认" Width="53px" OnClick="btn_Packing_Click"
                        CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Reject" runat="server" CssClass="SmallButton2" 
                        OnClick="btn_Reject_Click" OnClientClick="return confirm('确定驳回该申请单');" 
                        Text="驳回" Width="56px" />&nbsp; &nbsp;
                    <asp:Button ID="btn_Back1" runat="server" Text="返回" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_Back_Click" />
                </td>
            </tr>
            <tr id="trUpload" runat="server">
                <td align="right" class="style1">
                    导入文件：
                </td>
                <td colspan="3" valign="top" width="500" class="auto-style1">
                    <input id="filename1" style="width: 430px; height: 22px" type="file" size="45" name="filename1"
                        runat="server">
                    <asp:Button ID="btn_Upload" runat="server" Text="产品结构导入" Width="117px" CssClass="SmallButton2"
                        OnClick="btn_Upload_Click" />

                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="left">
                    升级产品
                </td>
                <td align="right">
                    <asp:Button ID="btn_ExportProd" runat="server" Text="导出" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_ExportProd_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                    <asp:GridView ID="gv_product" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" DataKeyNames="id,selected" OnRowDataBound="gv_product_RowDataBound"
                         Width="1036px" EnableModelValidation="True">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
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
                            <asp:BoundField DataField="code" HeaderText="整灯型号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="item_qad" HeaderText="QAD号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="产品描述">
                                <ItemTemplate>
                                    <%# Eval("description")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_description" Text='<%# Bind("description") %>' runat="server"
                                        Width="300px" />
                                </EditItemTemplate>
                                <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:TemplateField>
                            <asp:CommandField EditText="编辑" ShowEditButton="True" CancelText="取消" UpdateText="更新" Visible="false">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                <ControlStyle ForeColor="Black" Font-Underline="True" />
                            </asp:CommandField>
                            <asp:BoundField DataField="numOfChild" HeaderText="裸灯用量" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="domain" HeaderText="域" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>

                            
                            <asp:BoundField DataField="box_weight" HeaderText="重量" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="box_size" HeaderText="体积" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="box_length" HeaderText="长度" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="box_width" HeaderText="宽度" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="box_depth" HeaderText="深度" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="num_per_pack" HeaderText="只数/套" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="num_per_box" HeaderText="套数/箱" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="box_per_pallet" HeaderText="箱数/货盘" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>


                        </Columns>  
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="left">
                    产品结构
                </td>
                <td align="right">
                    <asp:Button ID="btn_ExportDetail" runat="server" Text="导出" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_ExportDetail_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                    <asp:GridView ID="gv_det" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_det_PageIndexChanging"
                        PageSize="30" Width="1036px" EnableModelValidation="True">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="productNumber" HeaderText="父级型号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="productQad" HeaderText="QAD号 " ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="childNumber" HeaderText="子级型号 " ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="childQad" HeaderText="QAD号 " ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="childDescription" HeaderText="子级描述 " ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="数量">
                                <ItemTemplate>
                                    <%# Eval("numOfChild")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_gvdetqty" Text='<%# Bind("numOfChild") %>' runat="server" Width="60px"
                                        MaxLength="4" />
                                </EditItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="right" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="childCategory" HeaderText="类型 " ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="位号">
                                    <ItemTemplate>
                                    <%# Eval("posCode")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_posCode" Text='<%# Bind("posCode") %>' runat="server"
                                        Width="90px" />
                                </EditItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <%# Eval("notes")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_notes" Text='<%# Bind("notes") %>' runat="server"
                                        Width="200px" />
                                </EditItemTemplate>
                                <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="itemNumber" HeaderText="替代品" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="itemNumber" HeaderText="子级负责人" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:CommandField EditText="编辑" ShowEditButton="True" CancelText="取消" UpdateText="更新" Visible="false">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ControlStyle ForeColor="Black" Font-Underline="True" />
                            </asp:CommandField>
                            
                            
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
                <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
    </form>
</body>
</html>
