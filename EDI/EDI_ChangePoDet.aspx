<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI_ChangePoDet.aspx.cs" Inherits="EDI_EDI_ChangePoDet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
         <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $("table tr").hover(
                function () {
                    if (!$(this).hasClass("NoTrHover")) {
                        $(this).css("background-color", "#E1FCCE");
                    }
                },
                function () {
                    if (!$(this).hasClass("NoTrHover")) {
                        $(this).css("background-color", "#fff");
                    }
                });

            var _index = $("#hidTabIndex").val();

            var poc_id  =$("#hidPocId").val();  

            var $tabs = $("#divTabs").tabs({ active: _index });

            $("#gv1").dblclick(function(){
                if($("#chkCanEffect").prop("checked"))
                { 
                    var _no = $("#lblNo").text();
                    var _src = "/EDI/EDI_pocEffectDet.aspx?poc_id=" + poc_id ;
                    $.window("review", "70%", "80%", _src, "", true);
                }  
                else
                {
                    alert("Supervisor has not yet signed！");
                }
              
            });

        })

        </script>
    <style type="text/css">
        .FixedGrid {
            border-collapse: collapse;
            table-layout: fixed;
        }

            .FixedGrid .FixedGridWidth {
                width: 50px;
                height: 25px;
                border-top: 1px solid #fff;
                border-right: 1px solid #fff;
            }

            .FixedGrid .FixedGridHeight {
                width: 1px;
                height: 25px;
                border-top: 1px solid #fff;
                border-bottom: 1px solid #fff;
                border-right: 1px solid #fff;
            }

            .FixedGrid .FixedGridLeftCorner {
                width: 1px;
                border-left: 1px solid #fff;
                border-bottom: 1px solid #000;
            }

            .FixedGrid .FixedGridLeft {
                width: 100px;
                text-align: center;
                border-left: 1px solid #fff;
                border-right: 1px solid #000;
                font-weight: bold;
            }

            .FixedGrid .FixedGridRight {
                width: 100px;
                text-align: center;
                border-right: 1px solid #000;
                font-weight: bold;
            }

            .FixedGrid tr {
                border-right: 1px solid #000;
            }

            .FixedGrid td {
                text-align: left;
                word-break: break-all;
                word-wrap: break-word;
                border: 1px solid #000;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <div id="divTabs">
                <ul>
                    <li><a href="#tabs-1">&nbsp;&nbsp;FORM&nbsp;&nbsp;</a></li>
                    <li><a href="#tabs-2">&nbsp;&nbsp;REVIEW&nbsp;&nbsp;</a></li>
         
                    <li>
                        <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="SmallButton3" OnClick="btnBack_Click" Width="80px" />
                        <input id="hidTabIndex" type="hidden" value="0" runat="server" />
                        <input id="hidEmailAddress" type="hidden" value="" runat="server" />
                    </li>
                </ul>
              <div id="tabs-1">
     <table class="FixedGrid" border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight FixedGridLeftCorner"></td>
                            <td class="FixedGridLeft" style="border-right: 1px solid #fff;"></td>
                            <td class="FixedGridWidth"><asp:HiddenField ID="hidAgreeAuth" runat="server" /></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridRight" style="border-right: 1px solid #fff; width:150px;"></td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft"><strong>No.</strong></td>
                            <td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblNo" runat="server" ></asp:Label>
                            </td>
                            <td colspan="2"><strong>Applicant</strong></td>
                            <td colspan="4">
                                <asp:Label ID="lblCommitName" runat="server" Font-Bold="False"></asp:Label>
                                <input id="hidCreateBy" runat="server" type="hidden" />
                            </td>
                             <td colspan="2"><strong>Applicantion Date</strong></td>
                            <td colspan="4">
                                <asp:Label ID="lbCommitDate" runat="server"></asp:Label>
                                
                            </td>
                            <td class="FixedGridRight" rowspan="5">
                                <asp:CheckBox ID="chkIsApprove" Style="display: none;" runat="server" />
                                <asp:CheckBox ID="chkIsAgree" Style="display: none;" runat="server" /></td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft"><strong>Cust Po</strong></td>
                            <td colspan="4">
                               <asp:Label ID="lbCustPo" runat="server" ></asp:Label>
                            </td>
                            <td colspan="2"><strong>Cust Code</strong></td>
                            <td colspan="4">
                                <asp:Label ID="lblCusrCode" runat="server" Font-Bold="False"></asp:Label>
                            </td>
                            <td colspan="2"><strong>Ship To</strong></td>
                            <td colspan="4">
                                <asp:Label ID="lbShipTo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                             <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft"><strong>Ship Via</strong></td>
                            <td colspan="4"><asp:Label ID="lbShipVia" runat="server"></asp:Label></td>
                            <td colspan="2"><strong>Due Date</strong></td>
                            <td colspan="4"><asp:Label ID="lbDueDate" runat="server"></asp:Label></td>
                             <td colspan="2"><strong>Channel</strong></td>
                            <td colspan="4"><asp:Label ID="lbChannel" runat="server"></asp:Label></td>

                        </tr>
                       
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="6"><strong>Reason</strong
                                </td>
                            <td colspan="16" rowspan="6">
                                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="99%" Height="150px"
                                    MaxLength="500" BorderStyle="None" ReadOnly="True" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="FixedGridRight" rowspan="6"></td>
                        </tr>
                        
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                                             <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>

                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="6"><strong>Attach File</strong
                                </td>
                            <td colspan="16" rowspan="6">
                               <asp:GridView ID="gvUpload" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                Width="800px" CssClass="GridViewStyle" PageSize="20" 
                                OnRowCommand="gvUpload_RowCommand" 
                                DataKeyNames="id,poc_docFileName,poc_docURL,createdName,createdDate,flag">
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
                                      
                                        </asp:TableRow>
                                    </asp:Table>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="poc_docFileName" HeaderText="Attach File Name">
                                        <HeaderStyle Width="540px" HorizontalAlign="Center" />
                                        <ItemStyle Width="540px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="createdName" HeaderText="Upload User">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="createdDate" HeaderText="Upload Date">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="view">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnView" runat="server" CommandName="lkbtnView" CommandArgument='<%# Eval("poc_docURL") %>'
                                        Text="view"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                               
                                </Columns>
                            </asp:GridView>
                            </td>
                            <td class="FixedGridRight" rowspan="6"></td>
                        </tr>
                        <tr>

                            
                            <td class="FixedGridHeight"></td>

                        </tr>

                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                            <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="6"><strong>Po Detailtd</strong></td>
                           
                            <td colspan="16" rowspan="6">
                              <asp:GridView ID="gvlist" runat="server"  AllowPaging="true"  AutoGenerateColumns="False" Width="800px"
                                DataKeyNames="id,poLine,partNbr,sku,qadPart,ordQty,um,price,dueDate,reqDate,remark,addflag,isdelete" OnPageIndexChanging="gvlist_PageIndexChanging"
                                OnRowDataBound="gvlist_RowDataBound"  PageSize="20"
                                CssClass="GridViewStyle AutoPageSize">
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

                                     <asp:TemplateField HeaderText="Description" SortExpression="desc">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkdesc" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

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
                            </td>
                            <td class="FixedGridRight" rowspan="6" id="lbparentLine" runat="server" style="color:red"></td>
                        </tr>
                                 <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
         <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="7"><strong>Supervisord</strong></td>
                         
                            <td colspan="16" rowspan="5">
                                <asp:TextBox ID="txtApprMsg" runat="server" TextMode="MultiLine" Width="99%" Height="150px"
                                    MaxLength="300" BorderStyle="None" BackColor="#DDFFDD" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="FixedGridRight" rowspan="7" id="lbManagerAgreeBy" runat="server"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>

                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td colspan="16">
                                <asp:HyperLink ID="hlinkManager" runat="server" Font-Bold="False" Font-Size="12px" Font-Underline="True" Height="18px" Target="_blank">Attachment:</asp:HyperLink>
                                <input id="fileManager" style="width: 90%; height: 23px" type="file" size="45" name="filename2"
                                    runat="server" visible="false"/></td>
                        </tr>

                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td colspan="10"></td>
                            <td colspan="3">
                                <asp:Button ID="btnDone" runat="server" Text="Agree" CssClass="SmallButton3" OnClick="btnDone_Click" Width="150px" Enabled="false" />
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnNotDone" runat="server" Text="Disagree" CssClass="SmallButton3" OnClick="btnNotDone_Click" Width="150px" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" colspan="18"><strong>
                                Each dept. is to check the  effects caused by the Change and confirm whether agree to the Change</strong></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" colspan="4"><strong>Responsibility</strong></td>
                            <td colspan="13" style="text-align: center;"><strong>Comment</strong></td>
                            <td class="FixedGridRight"><strong>Charged By</strong></td>
                        </tr>
                        <% Response.Write(EffectTR); %>
                         <tr id="agree" >
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="5">Financial Controller</td>
                            
                            <td colspan="16" rowspan="4" id="tdAgreeMsg" runat="server">
                                <asp:TextBox ID="txtNotice" runat="server" TextMode="MultiLine" Width="99%" Height="150px" Enabled="false"
                                    MaxLength="300" BorderStyle="None" BackColor="#DDFFDD"></asp:TextBox>
                            </td>
                             <td align="center" class="FixedGridRight" id="tdAgree" rowspan="5"  runat="server" ></td>
                        </tr>
                        <tr> <td class="FixedGridHeight"></td>
                           
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr >
                            <td class="FixedGridHeight" style="height: 25px"></td>
                             <td colspan="16">
                                <asp:HyperLink ID="hlnotice" runat="server" Font-Bold="False" Font-Size="12px" Font-Underline="True" Height="18px" Target="_blank">Attachment:</asp:HyperLink>
                                <input id="fileNotice" style="width: 90%; height: 23px" type="file" size="45" name="filename2"
                                    runat="server" visible="false" />

                             </td>
                        </tr>
                        <tr >
                            <td class="FixedGridHeight"></td>
                            <td></td>
                            <td colspan="10">
                                
                                <asp:CheckBox ID="chkValid" runat="server" Text="是" Enabled="False" Style="display: none;" />
                                <asp:CheckBox ID="chkNotValid" runat="server" Text="否" Enabled="False" Style="display: none;" />
                              
                            </td>
                            <td colspan="3" >
                                <asp:Button ID="btnValid" runat="server" Text="Agree" CssClass="SmallButton3" Width="150px" OnClick="btnValid_Click" Visible="false" Enabled="False" />
                            </td>
                            <td colspan="3" >
                                <asp:Button ID="btnNotValid" runat="server" Text="DisAgree" CssClass="SmallButton3" OnClick="btnNotValid_Click" Width="150px" Enabled="False" />
                            </td>
                            <td style="border-bottom:none;border-top:none" class ="FixedGridRight"></td>
                        </tr>


                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="6"><strong>Result</strong></td>
                            <td colspan="16" rowspan="5"  >
                              
                                <asp:TextBox ID="txtExcuteMsg" runat="server" TextMode="MultiLine" Width="99%" Height="150px"
                                    MaxLength="300" BorderStyle="None" BackColor="#DDFFDD" Enabled ="false"></asp:TextBox>

                            </td>
                            <td align="center" class="FixedGridRight" id="td1" rowspan="6"  runat="server"></td>
                             </tr>
                             <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight" style="height: 25px"></td>
                        </tr>

                             <tr>
                            
                            <td colspan="10">
                                  
                                <asp:CheckBox ID="chkAccessExcute" Style="display: none;" runat="server" />
                                <asp:CheckBox ID="chkIsExcute" Style="display: none;" runat="server" />
                                </td>
                            <td colspan="8">
                                
                                <asp:Button ID="btnExcute" runat="server" Text="Result Confirm" CssClass="SmallButton3" OnClick="btnExcute_Click" Width="155px" Enabled="False" />
                            </td>
                          
                        </tr>
                        
         </table>
                  </div>
                        <div id="tabs-2">
                    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="15" OnRowDataBound="gv1_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" Height="50px" VerticalAlign="Top" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table3" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="各单位责任" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="意见" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="责任人" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="poce_enDesc" HeaderText="Responsibility">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Comment">
                                <HeaderStyle Width="400px" HorizontalAlign="Center" />
                                <ItemStyle Width="400px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Charged By">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                 </div>
        <asp:HiddenField  ID="hidPocId" runat="server"/>
        <asp:CheckBox ID ="chkCanEffect" runat="server" Checked="false" style="display: none;" />
        <asp:HiddenField  ID="hidCreatedBy" runat="server"/>
        <asp:HiddenField  ID="HiddenField3" runat="server"/>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
     
       
    </script>
</body>
</html>
