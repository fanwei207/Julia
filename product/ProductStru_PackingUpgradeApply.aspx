<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductStru_PackingUpgradeApply.aspx.cs" Inherits="ProductStru_PackingUpgradeApply" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
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
                    产&nbsp;品&nbsp;型&nbsp;号：
                </td>
                <td class="style1">
                    <asp:TextBox ID="txt_prodCode" runat="server" Width="149px"></asp:TextBox>
                </td>
                <td align="right">
                </td>
                <td class="style3">
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
                    <asp:Button ID="btn_Submit" runat="server" Text="提交" Width="53px" OnClick="btn_Submit_Click" CssClass="SmallButton2"/>
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Cancel" runat="server"
                        OnClick="btn_Cancel_Click" OnClientClick="return confirm('确定取消该申请单');" 
                        Text="取消" Width="56px" CssClass="SmallButton2"/>&nbsp; &nbsp;
                    <asp:Button ID="btn_Back" runat="server" Text="返回" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_Back_Click" />
                </td>
            </tr>
            <tr id="trReview" runat="server">
                <td colspan="5" align="center">
                    <asp:Button ID="btn_Confirm" runat="server" Text="导入" Width="53px" OnClick="btn_Confirm_Click"
                        CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_submitbom" runat="server" Text="提交" Width="53px" OnClick="btn_submitbom_Click" CssClass="SmallButton2"/>
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
                    <input id="filename1" style="width: 492px; height: 22px" type="file" size="45" name="filename1"
                        runat="server">
                </td>
            </tr>
             <tr id="trUpload1" runat="server">
                <td align="right" class="style1">
                    <font size="3">下载：</font>
                </td>
                <td align="left" colspan="2" width="135">
                    <label id="here" onclick="submit();">
                        <a href="/docs/ProductStruUpdateApplyTemplate.xls" target="blank"><font color="blue">导入产品修改文件的模版</font></a></label>
                </td>
                <td align="center">
                    <asp:Button ID="btn_Upload" runat="server" Text="产品结构导入" Width="117px"
                        OnClick="btn_Upload_Click" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td id="trLine" colspan="2" runat="server">
                    <hr />
                </td>
            </tr>
            <tr id="trUpload2" runat="server">
                <td align="right" class="style1">
                    导入文件：
                </td>
                <td colspan="1" valign="top" width="500" class="auto-style1">
                    <input id="filename2" style="width: 492px; height: 22px" type="file" size="45" name="filename2"
                        runat="server"> &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_UploadProdcut" runat="server" Text="新产品信息导入" Width="117px" CssClass="SmallButton2"
                        OnClick="btn_UploadProdcut_Click" />
                </td>
            </tr>
            <tr id="trprodupgrade" runat="server">
                <td align="left">
                    升级产品
                </td>
                <td align="center">
                    
                
                    <asp:Button ID="btn_ExportProd" runat="server" Text="导出" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_ExportProd_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                    <asp:GridView ID="gv_product" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" DataKeyNames="id,selected,code" OnRowDataBound="gv_product_RowDataBound" OnRowEditing="gvShipdetail_RowEditing" 
                        OnRowUpdating = "gv_product_RowUpdating" OnRowCancelingEdit="gvShipdetail_RowCancelingEdit" 
                         Width="1236px" EnableModelValidation="True">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>  
                            <asp:TemplateField>
                    <HeaderTemplate>
                        <%--<input id="chkAll" type="checkbox">--%>
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
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="新整灯型号">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_ProdCodeNew" runat="server" Text='<%# Bind("ProdCodeNew") %>'  Width="95%"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemTemplate>
                                    <%#Eval("ProdCodeNew")%>
                                </ItemTemplate>
                            </asp:TemplateField>


               <%--             <asp:BoundField DataField="ProdCodeNew" HeaderText="新整灯型号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="prodcodeqadnew" HeaderText="QAD号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="产品描述">
                                <ItemTemplate>
                                    <%# Eval("description")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_description" Text='<%# Bind("description") %>' runat="server"
                                        Width="95%" />
                                </EditItemTemplate>
                                <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:TemplateField>
                            <asp:CommandField EditText="编辑" ShowEditButton="True" CancelText="取消" UpdateText="更新" Visible="true">
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                <ControlStyle ForeColor="Black" Font-Underline="True" />
                            </asp:CommandField>
                            <asp:BoundField DataField="QAD1" HeaderText="裸灯QAD" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numOfChild" HeaderText="裸灯用量" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                            </asp:BoundField>
                        </Columns>  
                    </asp:GridView>
                </td>
            </tr>

<%--            <tr>
                <td align="left">
                    新产品
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                    <asp:GridView ID="gv_product" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_product_PageIndexChanging"
                        PageSize="8" Width="1036px" EnableModelValidation="True">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="code" HeaderText="产品型号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
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
                            <asp:BoundField DataField="category" HeaderText="产品分类" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="状态" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="item_qad" HeaderText="Qad零件号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="item_qad_desc1" HeaderText=" QAD描述1 " ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="item_qad_desc2" HeaderText=" QAD描述2" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>--%>
            <tr id="trprodBom" runat="server">
                <td align="left">
                    产品结构
                </td>
            </tr>
            <tr id="trpdetail" runat="server">
               
                        <td colspan="2" align="left">
                            <%--整灯型号：--%>
                              <table cellpadding="0" cellspacing="0">
                                    <tr>
                                       <td style="width:60px;">
                                            
                                        </td>
                                        <td>
                                            整灯编号：
                                        </td>
                                              <td><asp:DropDownList ID="Prodcd" AutoPostBack = "true" runat="server" DataValueField = "ProdCode" DataTextField = "ProdCode" Width="151px" OnSelectedIndexChanged="Prodcd_SelectedIndexChanged" Enabled="false">
                                              </asp:DropDownList>
                                             <%--<asp:TextBox ID="Prodcd" runat="server" Width="149px"  Enabled="false"></asp:TextBox>--%>    
                                        </td>

                                        <td>
                                            qad号：
                                        </td>
                                        <td>
                                             <asp:TextBox ID="prodcdqad" runat="server" Width="149px" Enabled="false"></asp:TextBox>    
                                        </td>
                                        <td>
                                            新整灯型号：
                                        </td>
                                        <td>
                                             <asp:TextBox ID="Prodcdnew" runat="server" Width="149px" Enabled="false"></asp:TextBox>    
                                        </td>

                                         <td colspan="2">
                                             
                                        </td>
                                       
                                  </tr>
                                   <tr>
                                        <td style="width:60px;">
                                            
                                        </td>
                                        <td>
                                            子级部件号：
                                        </td>
                                        <td>
                                             <asp:TextBox ID="light1" runat="server" Width="149px"  Enabled="false" ></asp:TextBox>    
                                        </td>

                                        <td>
                                            qad号：
                                        </td>
                                        <td>
                                             <asp:TextBox ID="qad1" runat="server" AutoPostBack="true" OnTextChanged="qad1_TextChanged" Width="149px" Enabled="false"></asp:TextBox>    
                                        </td>

                                        <td>
                                            数量：
                                        </td>
                                        <td>
                                             <asp:TextBox ID="num" runat="server" Width="149px" Enabled="false"></asp:TextBox>    
                                        </td>

                                         <td>
                                            位号 ： 
                                        </td>
                                         <td>
                                             <asp:TextBox ID="posino" runat="server" Width="149px" Enabled="false"></asp:TextBox> 
                                        </td>

                                        
                                   </tr>

                                    <tr style="padding-top:3px; padding-bottom:3px;">
                                        <td style="width:60px;">

                                        </td>
                                        <td>
                                            原因：
                                        </td>
                                         <td >
                                              <asp:TextBox ID="txtread" runat="server" AutoComplete="Off" Enabled="false" TextMode="MultiLine" Width="230px" Height="40px" ></asp:TextBox>
                                              <asp:HiddenField ID="light" runat="server" />
                                              <asp:HiddenField ID="qad" runat="server" />
                                              <asp:HiddenField ID="edtflg"  Value="0" runat="server" />
                                         </td>
                                         <td>
                                            替代品：
                                        </td>
                                         <td>
                                              <asp:TextBox ID="Textrep" runat="server" AutoComplete="Off" Enabled="false" TextMode="MultiLine" Width="230px" Height="40px" ></asp:TextBox>
                                              
                                         </td>
                                          <td>
                                            desc：
                                        </td>
                                         <td>
                                              <asp:TextBox ID="Textdesc" runat="server" AutoComplete="Off" Enabled="false" TextMode="MultiLine" Width="230px" Height="40px" ></asp:TextBox>
                                              
                                         </td>
<%--                                           <td>
                                            位号 ： 
                                        </td>
                                         <td>
                                             <asp:TextBox ID="posino" runat="server" Width="149px" Enabled="false"></asp:TextBox> 
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="7">
                                         <asp:Button ID="Butadd" runat="server" Text="新增" OnClick="Butadd_Click" Width="53px" 
                                          CssClass="SmallButton2" />
                                          <asp:Button ID="Butsave" runat="server" Enabled="false" Text="保存" Width="53px" 
                                          CssClass="SmallButton2" OnClick="Butsave_Click" />
                                         <asp:Button ID="Butcal" runat="server"  Enabled="false" Text="取消" Width="53px"  OnClick="Butcal_Click"
                                          CssClass="SmallButton2" />                                      
                                         
                                        </td>
                                    </tr>
                                   
                              </table>
                        </td>
                      
               

            </tr>
            <tr id="opers" runat="server">
       
                <td  align="left">
                        <asp:Button ID="btn_bomexport" runat="server" Text="产品结构导出" Width="118px" CssClass="SmallButton2"
                             OnClick="btn_bomexport_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                    <asp:GridView ID="gv_det" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_det_PageIndexChanging"  OnRowCommand="gv_det_RowCommand"  
                        DataKeyNames="ProdCode,ProdCodeQad,childcode,childqad,numofchild,posCode,reason,itemreplace, productNumbernew,description"   OnRowEditing="gv_det_RowEditing" OnRowDeleting="gv_det_RowDeleting"
                        PageSize="30" Width="1236px" EnableModelValidation="True">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="ProdCode" HeaderText="整灯型号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProdCodeQad" HeaderText="QAD号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="childcode" HeaderText="子级部件号 " ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="childqad" HeaderText="QAD号 " ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numofchild" HeaderText="数量 " ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="right" Width="50px" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="数量" Visible="false">
                                <ItemTemplate>
                                    <%# Eval("numofchild")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_gvdetqty" Text='<%# Bind("numofchild") %>' runat="server" Width="60px" Enabled="false"
                                        MaxLength="4" />
                                </EditItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="right" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                              <asp:BoundField DataField="posCode" HeaderText="位号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="位号" Visible="false">
                                    <ItemTemplate>
                                    <%# Eval("posCode")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_posCode" Text='<%# Bind("posCode") %>' runat="server" Enabled="false"
                                        Width="90px" />
                                </EditItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                             </asp:TemplateField>
                            <asp:BoundField DataField="description" HeaderText="描述" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="reason" HeaderText="原因" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="原因" Visible="false">
                                <ItemTemplate>
                                    <%# Eval("reason")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_notes" Text='<%# Bind("reason") %>' runat="server" Enabled="false"
                                        Width="200px" />
                                </EditItemTemplate>
                                <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="itemreplace" HeaderText="替代品"  Visible="false" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="productNumbernew" HeaderText="新QAD号" Visible="false" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="right" Width="50px" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="编辑">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnedit" runat="server" Text="<u>edit</u>" ForeColor="Black"
                                        CommandName="edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="删除">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btndelete" runat="server" Text="<u>delete</u>" ForeColor="Black"  
                                        CommandName="delete"  ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                           <%-- <asp:CommandField EditText="编辑" ShowEditButton="True" CancelText="取消" UpdateText="更新" Visible="true">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ControlStyle ForeColor="Black" Font-Underline="True" />
                            </asp:CommandField>--%>
                            
                            
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
