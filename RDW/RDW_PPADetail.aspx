<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_PPADetail.aspx.cs" Inherits="RDW_RDW_PPADetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            $(".showDetil").mousemove(function () {
                var content = $(this).val();
                //position()获取当前元素基于父容器的位置             
                var positiontop = $(this).position().top;
                var positionleft = $(this).position().left;
                //获取上级空间的长宽
                var positwidth = $(this).width();
                var positheight = $(this).height();
                $("#txt").val(content);
                if(content != '')
                {
                    $("#txt").show();
                    //$("#txt").css({ position: "relative", 'top': 10, 'left': 10, 'z-index': 2 });
                    //设置txt的位置基于showDetil的坐标  
                    $("#txt").css({ position: "absolute", 
                        'top': positiontop + positheight + 5, 
                        'left': positionleft, 
                        'z-index': 2 ,
                        'width' : 2 * positwidth,
                        'height' : 150,
                        'border-radius': 5,
                        'background-color': '#c9daf8'
                    });
                }
            });
            $(".showDetil").mouseleave(function () {
                $(".comment").hide();
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <div >
            <table width="1000px" border="1" cellspacing="0" cellpadding="0" rules="all" style="border-color:black">
                <tr align="center">
                    <td>
                        <a href="/docs/ProjectProposal&ApprovalForm.xlsx" >Project Proposal & Approval Form 立项模版</a>
                    </td>
                </tr>
                <tr align="center">
                   
                    <td>
                        File Upload: <input id="UpLoadFile" runat="server" style="width: 300px;" name="resumename" type="file" />      
                        <asp:Button runat="server" ID="txt_UpLoadPPA" Text="UpLoad" CssClass="SmallButton2" OnClick="txt_UpLoadPPA_Click"  />
                        
                        <asp:HiddenField ID="hidMstrId" runat="server" />
                        <asp:HiddenField ID ="hidView" runat="server" />
                         <asp:HiddenField ID ="hidAppv" runat="server" />
                    </td>
                </tr>
                <tr>
                <td colspan="8" valign="top" align="center">
                    <asp:GridView ID="gvUpload" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Width="800px" CssClass="GridViewStyle" PageSize="5" OnRowDataBound="gvUpload_RowDataBound"
                        OnRowCommand="gvUpload_RowCommand" OnRowDeleting="gvUpload_RowDeleting" 
                        DataKeyNames="ppa_docId,ppa_mstrId,ppa_path,ppa_createdBy,ppa_fileName">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                        <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table2" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="Attach File Name" Width="540px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Upload User" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Upload Date" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="View" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Delete" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text=""   Width="50px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="ppa_fileName" HeaderText="Attach File Name">
                                <HeaderStyle Width="540px" HorizontalAlign="Center" />
                                <ItemStyle Width="540px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ppa_uploader" HeaderText="Upload User">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ppa_createdDate" HeaderText="Upload Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:ButtonField Text="View" HeaderText="View" CommandName="View">
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="Delete">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr> 
                <tr>
                    <td colspan="8" valign="top" align="center">
                        <asp:Button runat="server" Text="Back" ID="btn_back2" CssClass="SmallButton2" OnClick="btn_back_Click" />
                    </td>
                </tr>               
            </table>
            <table>
                <tr style="height:30px">
                    <td></td>
                </tr>
            </table>
        </div>
        <div>
            <table width="1000px" border="1" cellspacing="0" cellpadding="0" rules="all" style="border-color:black">
                <tr align ="left">
                    <td width="300px">
                        Project Identifier 项目号:
                    </td>
                    <td width="250px">
                        <asp:TextBox ID="txt_projID" runat="server" Width="250px"></asp:TextBox>
                    </td>
                    <td width="450px" colspan="2">
                        Reference /Competitor Information 竞争对手产品参考信息
                    </td>
                </tr>
                <tr align ="left">
                    <td >
                        Classification 类别说明:
                    </td>
                    <td >
                        <asp:TextBox ID="txt_clssification" runat="server" Width="250px"></asp:TextBox>
                    </td>
                    <td width="150px">
                        Spec 基本规格参数
                    </td>
                    <td width="300px" style="left">
                        Picture 参考图片<br>
                        <asp:Label ID="lbl_imgName" runat="server" Visible="false" Width="0px"></asp:Label>
                        <asp:Label ID="lbl_imgPath" runat="server" Visible="false" Width="0px"></asp:Label>
                    </td>
                </tr>
                <tr align ="left">
                    <td >
                        Replacement Strategy  替代产品:
                    </td>
                    <td >
                        <asp:TextBox ID="txt_replacement" runat="server" Width="250px"></asp:TextBox>
                    </td>
                    <td rowspan="8">
                        <asp:TextBox runat="server" ID="txt_competitorInfo" Width="250px" TextMode="MultiLine" Height="300px"></asp:TextBox>
                    </td>
                    <td rowspan="8" align="center" valign="top">
                        <input id="FileUpload2" runat="server" style="width: 200px;" name="resumename" type="file" />
                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btn_upLoad" Text="UpLoad" CssClass="SmallButton2" OnClick="btn_upLoad_Click" />
                        <br /><br />
                        <asp:Image ID="image" runat="server" Height="177px" ImageAlign="Middle"
                        ImageUrl="~/image/Image1.gif" Width="250px"/>
                    </td>
                </tr>
                <tr align ="left">
                    <td>
                        Description  描述:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_description" runat="server" Width="250px"></asp:TextBox>
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr align ="left">
                    <td>
                        Target Product Cost  整灯目标价格:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_prodCost" runat="server" Width="250px"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr align ="left">
                    <td>
                        Region 领域:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_region" runat="server" Width="250px"></asp:TextBox>
                    </td>                    
                </tr>
                <tr align ="left">
                    <td>
                        Estimated Annual Volume: 预测年需量
                    </td>
                    <td>
                        <asp:TextBox ID="txt_forecastFY" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr align ="left">
                    <td>
                        Key Customer  主要客户:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_keyCustomer" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr align ="left">
                    <td>
                        Reqested Product Availability Date:
                        (Project Requestor) 项目申请者要求的产品完成日期

                    </td>
                    <td>
                        <asp:TextBox ID="txt_rpaDate" runat="server" Width="250px" CssClass="EnglishDate"></asp:TextBox>
                    </td>
                </tr>
                <tr align ="left">
                    <td>
                        ENERGY STAR / DLC?: 能源之星 / 灯具设计联盟DLC？
                    </td>
                    <td>
                        <asp:TextBox ID="txt_start" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width:1000px;background-color:lightblue">
                <tr align ="left" style="height:25px">  
                    <td><h5>Photometric Requirements <font color="#FF0000">光学指标</font></h5></td>
                </tr>
            </table>
            <asp:GridView runat="server" Width="1000px" ID="gv_pr" AutoGenerateColumns="False" CssClass="GridViewStyle"
                PageSize="20" AllowPaging="True" DataKeyNames="ppa_detID,ppa_type,ppa_metric,ppa_ID,ppa_sort">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="Metric  度量标准" DataField="ppa_metric" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="350px" />
                        <ItemStyle HorizontalAlign="Left" Width="350px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Target 参数要求">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_prTarget" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_target")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual 实际值">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_prActual" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_actual")%>' CssClass="showDetil"></asp:TextBox>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments 意见">
                        <HeaderStyle Width="300px" HorizontalAlign="Center" />
                        <ItemStyle Width="300px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_prComments" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_comments")%>' CssClass="showDetil"></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pass? 是否通过">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_prIsPass" runat="server" Checked='<% #Bind("ppa_isPass")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <table style="width:1000px;background-color:lightblue">
                <tr align ="left" style="height:25px">  
                    <td><h5>Life & Lumen Maintenance <font color="#FF0000">寿命及流明稳定性</font></h5></td>
                </tr>
            </table>
            <asp:GridView runat="server" Width="1000px" ID="gv_llm" AutoGenerateColumns="False" CssClass="GridViewStyle"
                PageSize="20" AllowPaging="True" DataKeyNames="ppa_detID,ppa_type,ppa_metric,ppa_ID,ppa_sort">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="Metric  度量标准" DataField="ppa_metric" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="350px" />
                        <ItemStyle HorizontalAlign="Left" Width="350px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Target 参数要求">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_llmTarget" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_target")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual 实际值">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_llmActual" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_actual")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments 意见">
                        <HeaderStyle Width="300px" HorizontalAlign="Center" />
                        <ItemStyle Width="300px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_llmComments" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_comments")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pass? 是否通过">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_llmIsPass" runat="server" Checked='<% #Bind("ppa_isPass")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <table style="width:1000px;background-color:lightblue">
                <tr align ="left" style="height:25px">    
                    <td><h5>Electrical Performance Requirements <font color="#FF0000">电气特性指标</font></h5></td>
                </tr>
            </table>
            <asp:GridView runat="server" Width="1000px" ID="gv_epr" AutoGenerateColumns="False" CssClass="GridViewStyle"
                PageSize="20" AllowPaging="True" DataKeyNames="ppa_detID,ppa_type,ppa_metric,ppa_ID,ppa_sort">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="Metric  度量标准" DataField="ppa_metric" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="350px" />
                        <ItemStyle HorizontalAlign="Left" Width="350px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Target 参数要求">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_eprTarget" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_target")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual 实际值">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_eprActual" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_actual")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments 意见">
                        <HeaderStyle Width="300px" HorizontalAlign="Center" />
                        <ItemStyle Width="300px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_eprComments" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_comments")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pass? 是否通过">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_eprIsPass" runat="server" Checked='<% #Bind("ppa_isPass")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <table style="width:1000px;background-color:lightblue">
                <tr align ="left" style="height:25px">   
                    <td><h5>Mechanical Requirements <font color="#FF0000">机械结构指标</font></h5></td>
                </tr>
            </table>
            <asp:GridView runat="server" Width="1000px" ID="gv_mr" AutoGenerateColumns="False" CssClass="GridViewStyle"
                PageSize="20" AllowPaging="True" DataKeyNames="ppa_detID,ppa_type,ppa_metric,ppa_ID,ppa_sort">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="Metric  度量标准" DataField="ppa_metric" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="350px" />
                        <ItemStyle HorizontalAlign="Left" Width="350px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Target 参数要求">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_mrTarget" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_target")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual 实际值">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_mrActual" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_actual")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments 意见">
                        <HeaderStyle Width="300px" HorizontalAlign="Center" />
                        <ItemStyle Width="300px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_mrComments" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_comments")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pass? 是否通过">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_mrIsPass" runat="server" Checked='<% #Bind("ppa_isPass")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <table style="width:1000px;background-color:lightblue">
                <tr align ="left" style="height:25px">    
                    <td><h5>Dimming Requirements <font color="#FF0000">调光性能指标</font></h5></td>
                </tr>
            </table>
            <asp:GridView runat="server" Width="1000px" ID="gv_dr" AutoGenerateColumns="False" CssClass="GridViewStyle"
                PageSize="20" AllowPaging="True" DataKeyNames="ppa_detID,ppa_type,ppa_metric,ppa_ID,ppa_sort">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="Metric  度量标准" DataField="ppa_metric" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="350px" />
                        <ItemStyle HorizontalAlign="Left" Width="350px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Target 参数要求">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_drTarget" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_target")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual 实际值">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_drActual" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_actual")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments 意见">
                        <HeaderStyle Width="300px" HorizontalAlign="Center" />
                        <ItemStyle Width="300px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_drComments" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_comments")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pass? 是否通过">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_drIsPass" runat="server" Checked='<% #Bind("ppa_isPass")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <table style="width:1000px;background-color:lightblue">
                <tr align ="left" style="height:25px">    
                    <td><h5>Miscellaneous System Requirements <font color="#FF0000">其它的参数要求</font></h5></td>
                </tr>
            </table>
            <asp:GridView runat="server" Width="1000px" ID="gv_msr" AutoGenerateColumns="False" CssClass="GridViewStyle"
                PageSize="20" AllowPaging="True" DataKeyNames="ppa_detID,ppa_type,ppa_metric,ppa_ID,ppa_sort">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="Metric  度量标准" DataField="ppa_metric" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="350px" />
                        <ItemStyle HorizontalAlign="Left" Width="350px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Target 参数要求">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_msrTarget" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_target")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual 实际值">
                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                        <ItemStyle Width="125px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_msrActual" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_actual")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments 意见">
                        <HeaderStyle Width="300px" HorizontalAlign="Center" />
                        <ItemStyle Width="300px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_msrComments" runat="server" TextMode="MultiLine" Text='<% #Bind("ppa_comments")%>' CssClass="showDetil"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pass? 是否通过">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_msrIsPass" runat="server" Checked='<% #Bind("ppa_isPass")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <table style="width:1000px" border="0" cellspacing="0" cellpadding="0" rules="all">
                <tr align ="left" style="height:25px;background-color:lightblue">    
                    <td colspan="4"><h5>Program Request <font color="#FF0000">项目申请人</font></h5></td>
                </tr>
                <tr>
                    <td width="350px">Requestor 申请者</td>
                    <td width="300px">Signature 签名</td>
                    <td width="150px"></td>
                    <td width="200px">Date 日期</td>
                </tr>
                <tr>
                    <td width="350px">Product Manager 项目经理:</td>
                    <td width="300px"><asp:TextBox runat="server" ID="txt_PMSignature" Width="300px" ></asp:TextBox></td>
                    <td width="150px"></td>
                    <td width="200px"><asp:TextBox runat="server" ID="txt_PMDate" Width="300px" CssClass="EnglishDate" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="350px">R&D 研发工程师:</td>
                    <td width="300px"><asp:TextBox runat="server" ID="txt_RDSignature" Width="300px" ></asp:TextBox></td>
                    <td width="150px"></td>
                    <td width="200px"><asp:TextBox runat="server" ID="txt_RDDate" Width="300px" CssClass="EnglishDate"  ></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width:1000px" border="0" cellspacing="0" cellpadding="0" rules="all">
                <tr align ="left" style="height:25px;background-color:lightblue">      
                    <td colspan="4"><h5>Review- Product Specifications <font color="#FF0000">审核-产品规格</font></h5></td>
                </tr>
                <tr>
                    <td width="350px">Review 审核者</td>
                    <td width="300px">Signature 签名</td>
                    <td width="150px"></td>
                    <td width="200px">Date 日期</td>
                </tr>
                <tr>
                    <td width="350px">Director Product Management  产品主管:</td>
                    <td width="300px"><asp:TextBox runat="server" ID="txt_DPMSignature" Width="300px" ></asp:TextBox></td>
                    <td width="150px"></td>
                    <td width="200px"><asp:TextBox runat="server" ID="txt_DPMDate" Width="200px" CssClass="EnglishDate"  ></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="350px">Engineering Management 工程主管:</td>
                    <td width="300px"><asp:TextBox runat="server" ID="txt_EMSignature" Width="300px" ></asp:TextBox></td>
                    <td width="150px"></td>
                    <td width="200px"><asp:TextBox runat="server" ID="txt_EMDate" Width="200px" CssClass="EnglishDate"  ></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="350px">Comments 评论:</td>
                    <td width="300px"><asp:TextBox runat="server" ID="txt_ReviewComments" Width="300px" ></asp:TextBox></td>
                    <td width="150px"></td>
                    <td width="200px"><asp:TextBox runat="server" ID="txt_ReviewCommentsDate" Width="200px" CssClass="EnglishDate" ></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width:1000px" border="0" cellspacing="0" cellpadding="0" rules="all">
                <tr align ="left" style="height:25px;background-color:lightblue">     
                    <td colspan="4"><h5>Initial Program Approval - Program Funding & Resource Allocation <font color="#FF0000">立项启动审批-项目资金和资源分配</font></h5></td>
                </tr>
                <tr>
                    <td width="350px">Approvers 审批者</td>
                    <td width="300px">Signature 签名</td>
                    <td width="150px"></td>
                    <td width="200px">Date 日期</td>
                </tr>
                <tr>
                    <td width="350px">Sr. VP R&D  高级副总裁:</td>
                    <td width="300px"><asp:TextBox runat="server" ID="txt_IPASignature" Width="300px" ></asp:TextBox></td>
                    <td width="150px"></td>
                    <td width="200px"><asp:TextBox runat="server" ID="txt_IPASigDate" Width="200px" CssClass="EnglishDate"></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="350px">Recommendations 批示:</td>
                    <td width="300px">
                        <asp:RadioButton runat="server" ID="rb_IPAApprove" Text="Approve" GroupName="IPA" />&nbsp;&nbsp;
                        <asp:RadioButton runat="server" ID="rb_IPAReject" Text="Reject" GroupName="IPA" />&nbsp;&nbsp;
                        <asp:RadioButton runat="server" ID="rb_IPAHold" Text="Hold" GroupName="IPA" />
                    </td>
                    <td width="150px"></td>
                    <td width="200px"><asp:TextBox runat="server" ID="txt_IPADate" Width="200px" CssClass="EnglishDate"></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width:1000px" border="0" cellspacing="0" cellpadding="0" rules="all">
                <tr style="height:25px;background-color:lightblue" align="left">    
                    <td colspan="4" ><h5>Final Program Approval - Program Funding & Resource Allocation <font color="#FF0000">立项最终审批-项目资金和资源分配</font></h5></td>
                </tr>
                <tr>
                    <td width="350px">Approvers 审批者</td>
                    <td width="300px">Signature 签名</td>
                    <td width="150px"></td>
                    <td width="200px">Date 日期</td>
                </tr>
                <tr>
                    <td width="350px">CEO  行政总裁:</td>
                    <td width="300px"><asp:TextBox runat="server" ID="txt_FPASignature" Width="300px" ></asp:TextBox></td>
                    <td width="150px"></td>
                    <td width="200px"><asp:TextBox runat="server" ID="txt_FPASigDate" Width="200px" CssClass="EnglishDate"></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="350px">Recommendations 批示:</td>
                    <td width="300px">
                        <asp:RadioButton runat="server" ID="rb_FPAApprove" Text="Approve" GroupName="FPA" />&nbsp;&nbsp;
                        <asp:RadioButton runat="server" ID="rb_FPAReject" Text="Reject" GroupName="FPA" />
                    </td>
                    <td width="150px"></td>
                    <td width="200px"><asp:TextBox runat="server" ID="txt_FPADate" Width="200px" CssClass="EnglishDate" ></asp:TextBox></td>
                </tr>
                </table>
        </div>
        <table><tr style="height:20px"><td></td></tr></table>
        <asp:Button runat="server" Text="Save" ID="btn_save" CssClass="SmallButton2" OnClick="btn_save_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button runat="server" Text="Submit" ID="btn_Submit" CssClass="SmallButton2" OnClick="btn_Submit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" Text="Back" ID="btn_back" CssClass="SmallButton2" OnClick="btn_back_Click" />
    </div>
        <asp:HiddenField ID="hidDetID" runat="server" />
        <textarea cols="5" runat="server" class="comment" id="txt" style=" display:none; overflow:hidden;" ></textarea>
    </form>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
