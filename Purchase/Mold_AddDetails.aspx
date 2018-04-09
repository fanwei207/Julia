<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mold_AddDetails.aspx.cs" Inherits="Purchase_Mold_AddDetails" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

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
        
        <table id ="tb1" width="700px" rules="none" border="1" bordercolor="gray">
            <tr style="height:30px">
                <td align="left">模具类编号</td><td align="left"><asp:TextBox runat="server" ID="txt_MoldNbr" Width="100px" ></asp:TextBox></td>
                <td align="left">模具类状态</td><td align="left"><asp:TextBox runat="server" ID="txt_MoldStatus" Width="60px"></asp:TextBox></td>
                <td align="left">生产类型</td><td align="left"><asp:TextBox runat="server" ID="txt_MoldType" Width="80px"></asp:TextBox></td>
            </tr>
            <tr style="height:30px">
                
                <td align="left">供应商</td><td align="left"><asp:TextBox runat="server" ID="txt_vend" Width ="80px"></asp:TextBox></td>
            </tr>
            <tr style="height:30px">
                <td align="left">备注</td>
                <td align="left" colspan="5">
                    <asp:TextBox runat="server" ID="txt_remark" Width="490px" Height="30px"></asp:TextBox>
                </td>
            </tr>
            
        </table>
        <table id="tb2" width="700px" style="margin-top:10px;">
            <tr style="height:30px">
                <td align="left" width="80px">模具编号</td><td align="left"><asp:TextBox runat="server" ID="txt_detailsNbr" Width ="80px" Maxlength="15"></asp:TextBox></td>
              <td align="left" width="80px">供应商模具号</td><td align="left"><asp:TextBox runat="server" ID="txt_venderMoldNo" Width ="80px" Maxlength="15"></asp:TextBox></td>
                <td align="left" width="60px">模具状态</td>
                <td align="left">
                    <asp:DropDownList runat="server" ID="ddl_detailsStatus" Width="70px">
                        <asp:ListItem Value="1">正常</asp:ListItem>
                        <asp:ListItem Value="0">停用</asp:ListItem>
                    </asp:DropDownList>
                </td>               
            </tr>
            <tr>
                  <td align="left" width="60px">启用时间</td>
                <td align="left"><asp:TextBox runat="server" ID="txt_startDate" Width ="80px" CssClass="EnglishDate"></asp:TextBox></td>
                <td align="left">开模周期</td><td align="left"><asp:TextBox runat="server" ID="txt_TimeCost" Width ="80px"></asp:TextBox></td>
                <td align="left">一模几穴</td><td align="left"><asp:TextBox runat="server" ID="txt_Cavity" Width ="80px"></asp:TextBox></td>
            </tr>
            <tr>
                
                <td align="left">产能</td><td align="left"><asp:TextBox runat="server" ID="txt_capacity" Width ="80px"></asp:TextBox></td>
                <td align="left">生产寿命</td><td align="left"><asp:TextBox runat="server" ID="txt_WorkingLife" Width ="80px"></asp:TextBox></td>
                <td align="left" width="60px">已生产数量</td>
                <td align="left" ><asp:TextBox runat="server" ID="txt_qty" Width ="80px" Maxlength="10" ></asp:TextBox></td>
            </tr>
            <tr>
                <td align="left">穴编号</br>（以分号隔开）</td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txt_cavityNbr" runat ="server" Width ="490" Maxlength="150"></asp:TextBox>
                </td>
            </tr>
            <tr style="height:30px">
                <td align="left">备注</td>
                <td align="left" colspan="5">
                    <asp:TextBox TextMode="MultiLine" runat="server" ID="txt_detailsRemark" Width="490px" Height="50px" Maxlength="150"></asp:TextBox>
                </td>
            </tr>
            <tr style="height:30px">
                <td align="center" colspan ="6"><asp:Button runat="server" ID="btn_save" Text="添加" Width="70px" CssClass="SmallButton2" OnClick="btn_save_Click" />
                    <asp:Button runat="server" ID="btn_mody" Text="修改" Width="70px" CssClass="SmallButton2" Visible="false" OnClick="btn_mody_Click" />
                    <asp:Label runat="server" ID="lbl_MoldID" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
     <asp:GridView ID="gv" runat="server" Width="900px" AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames ="Mold_ID"
                            PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowCommand="gv_RowCommand" OnRowEditing="gv_RowEditing" >
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText ="序号">
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="server" Text='<%# Bind("rowNbr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText ="模具编号">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNbr" runat="server" Text='<%# Bind("Mold_Nbr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText ="供应商模具编号">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVenderNbr" runat="server" Text='<%# Bind("Mold_venderMoldNbr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="130px" HorizontalAlign="Center" />
                                    <ItemStyle Width="130px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:TemplateField>                                       
                                <asp:TemplateField HeaderText ="状态">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Mold_Status") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText ="开模周期">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTimeCost" runat="server" Text='<%# Bind("Mold_TimeCost") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText ="产能">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCapacity" runat="server" Text='<%# Bind("Mold_Capacity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText ="模具寿命">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkingLife" runat="server" Text='<%# Bind("Mold_WorkingLife") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText ="一模几穴">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCavity" runat="server" Text='<%# Bind("Mold_Cavity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText ="数量">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Mold_Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText ="启用时间">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("Mold_StartDate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText ="穴编号">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcavityNbr" runat="server" Text='<%# Bind("Mold_CavityDetails") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle Width="100px" HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText ="备注">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Mold_remark") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                    <ItemStyle Width="200px" HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbt_modify" CommandName ="Edit" Text="编辑"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbt_del" CommandName ="Del" Text="删除"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        </asp:GridView>
    </div>
    </form>
</body>
</html>
