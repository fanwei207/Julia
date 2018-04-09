<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_scores_mstr.aspx.cs" Inherits="Test_Test_scores_mstr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#chkNotApprove").click(function () {
                if ($(this).prop("checked")) {
                    $("#chkNotValid").prop("checked", false)
                    $("#chkNotAgree").prop("checked", false)
                    $("#chkDecideValid").prop("checked", false)
                }
            })
            $("#chkNotValid").click(function () {
                if ($(this).prop("checked")) {
                    $("#chkNotApprove").prop("checked", false)
                    $("#chkNotAgree").prop("checked", false)
                    $("#chkDecideValid").prop("checked", false)
                }
            })
            $("#chkNotAgree").click(function () {
                if ($(this).prop("checked")) {
                    $("#chkNotApprove").prop("checked", false)
                    $("#chkNotValid").prop("checked", false)
                    $("#chkDecideValid").prop("checked", false)
                }
            })
            $("#chkDecideValid").click(function () {
                if ($(this).prop("checked")) {
                    $("#chkNotApprove").prop("checked", false)
                    $("#chkNotValid").prop("checked", false)
                    $("#chkNotAgree").prop("checked", false)
                }
            })



            $(".close").click(function () {
                if (typeof ($.confirm_retValue) == "undefined" ||
                    (typeof ($.confirm_retValue) != "undefined" && $.confirm_retValue)) {
                    var _no = $(this).parent().parent().find(".no").html();
                    var _src = "/Supplier/FI_closeReason.aspx?no=" + _no;
                    $.window("验厂单关闭原因", "50%", "50%", _src, "", true);
                    return false;
                }
            });
          
            $(".new").click(function () {
                var _no = $(this).parent().parent().find(".no").html();
              
                var _src = "/RDW/prod_Report.aspx?from=rdw&name=" + _no;
                $.window("变更申请试流单", "90%", "95%", _src, "", true);
                return false;
            });
        }

        )

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="left">
            <table cellspacing="0" cellpadding="0" style="width: 1300px;">
                
                <tr>
                    <td>
                        工厂
                        <asp:DropDownList ID="dropPlant" runat="server" Width="250px" AutoPostBack="True"
                             TabIndex="1" 
                            Style="text-align: center;" >
                            <asp:ListItem Value="0">--请选择一个公司--</asp:ListItem>
                            <asp:ListItem Value="1">上海强凌电子有限公司 SZX</asp:ListItem>
                            <asp:ListItem Value="2">镇江强凌电子有限公司 ZQL</asp:ListItem>
                            <asp:ListItem Value="5">扬州强凌有限公司 YQL</asp:ListItem>
                            <asp:ListItem Value="8">淮安强陵照明有限公司  HQL</asp:ListItem>
                              
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right; width: 50px;">考试人</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txt_title" runat="server" Width="120px" CssClass="Param"></asp:TextBox></td>
                   
                    <td>试卷

                    </td>
                    <td>
                            <asp:DropDownList ID="ddlStatu" runat="server" Width="108px" CssClass="Param" DataTextField="exam_name" DataValueField ="exam_id">
                            
                        </asp:DropDownList>
                       
                    </td>   
                      <td>考试时间

                    </td>
                    <td>
                             <asp:TextBox ID="txtstartdate" runat="server" Width="120px" CssClass="Param Date"></asp:TextBox>-
                        <asp:TextBox ID="txtenddate" runat="server" Width="120px" CssClass="Param Date"></asp:TextBox>
                    </td>  
                    
                    <td>
                        <asp:CheckBox ID="ckb_his" runat="server" Text="历史成绩" />
                   

                    </td>
                    <td rowspan="2" style="text-align:left;width:200px;" >
                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnQuery_Click" />
                           <asp:Button ID="btnimport" runat="server" Text="导出" CssClass="SmallButton2" OnClick="btnimport_Click"  />
                    </td>   
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowCommand="gv_RowCommand" DataKeyNames="mark_id" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" Width="1000px">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />    
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="100%" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Width="100%" HorizontalAlign="center">无数据</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                   
                    <asp:BoundField HeaderText="试卷" DataField="exam_name" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="考试人" DataField="createname" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                  

                    <asp:BoundField HeaderText="考试时间" DataField="createdate" ReadOnly="True" >
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" Width="75px" />
                    </asp:BoundField>

                      <asp:BoundField HeaderText="得分" DataField="scores" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="总分" DataField="scoresAll" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                       <asp:BoundField HeaderText="用时（分钟）" DataField="timeall" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="明细">
                        <ItemStyle HorizontalAlign="Center" Width="40" Font-Underline="True" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDetail" runat="server" Text='Detail'
                                CommandName="Detail"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="40"></HeaderStyle>
                    </asp:TemplateField>

                   
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>