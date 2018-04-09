<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_report_project.aspx.cs"
    Inherits="QC_qc_report_project" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        #Table1 TD
        {
            border-right: 1px solid;
            border-top: 1px solid;
            border-left: 1px solid;
            border-bottom: 1px solid;
        }
    </style>
    <script type="text/javascript">
        $(function(){      
            $("#btnBom").click(function(){
                var _domain = $("#hidDomain").val();             
                var _src = "../IT/Page_Viewer.aspx?pageID=23ace268-7364-48ee-9fab-b74897bd6c66&par=" + $("#lblPart").text() + "&domain=" + _domain;
                $.window("BOM", 700, 500, _src);
                return false;
            });
        });
    </script>
</head>
<body style="top: 0; bottom: 0; left: 0; right: 0; background-color: #ffffff">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <div style="width: 1100px;" class="MainContent_outer">
            <table id="Table" cellspacing="0" cellpadding="1" width="1100px" class="MainContent_inner">
                <tr>
                    
                    <td style="width: 140px; height: 16px ;" align="right">
                        工单号:
                    </td>
                    <td style="height: 16px; width: 100px;">
                        <asp:Menu ID="mOrder" runat="server" Width="53px" Target=" " Font-Bold="False" Font-Size="11px">
                            <DynamicMenuItemStyle Font-Bold="False" Font-Size="12px" />
                        </asp:Menu>
                    </td>
                   <td style="width: 100px; height: 16px" align="right">
                        ID号:
                    </td>
                    <td style="height: 16px" >
                        <asp:Menu ID="mRecv" runat="server" Width="63px" Target=" " Font-Bold="False" Font-Size="11px"
                            ForeColor="Black">
                            <DynamicMenuItemStyle Font-Bold="False" Font-Size="12px" />
                        </asp:Menu>
                    </td>
                    <td style="width:130px" align="right">物料号:</td>
                    <td style="height: 16px;width:150px">
                        <asp:Label ID="lblPart" runat="server" Width="100px"></asp:Label>
                    </td>
                    <td style="width:120px;" align="right">
                         产线:
                    </td>
                    <td style="height: 16px;width:120px;">
                        <%--<asp:Label ID="lblCust" runat="server" Width="100px"></asp:Label>--%>
                        <asp:Menu ID="lblCust" runat="server" Width="63px" Target=" " Font-Bold="False" Font-Size="11px"
                            ForeColor="Black">
                            <DynamicMenuItemStyle Font-Bold="False" Font-Size="12px" />
                        </asp:Menu>
                    </td>
                    <td style="width:100px" align="right">当前数量:</td>
                    <td style="height: 16px; width: 120px;">
                        <asp:Label ID="lblRmn" runat="server" Text="0" Width="80px"></asp:Label><asp:TextBox
                            ID="txtRmn" runat="server" CssClass="SmallTextBox" Visible="False" Width="80px"
                            AutoPostBack="True" OnTextChanged="txtRmn_TextChanged"></asp:TextBox>
                        <asp:CheckBox ID="chkDiv" runat="server" AutoPostBack="True" OnCheckedChanged="chkDiv_CheckedChanged"
                            Text="拆分" visible="false"/>
                    </td>
                     <td style="width:60px;">
                        <asp:Menu ID="mLine" runat="server"  Visible="false">
                            <DynamicMenuItemStyle Font-Bold="False" Font-Size="12px" />
                        </asp:Menu>
                    </td>
                </tr>
                 <tr>
                    
                    <td align="right">周期章:</td>
                    <td><asp:TextBox ID="txtZhouqizhang" runat="server" Width="100px" ReadOnly="True" Enabled="False"></asp:TextBox></td>
                    <td style="width:100px" align="right">UL Model:</td>
                     <td><asp:TextBox ID="txtUL" runat="server" ReadOnly="True" Width="120px" Enabled="False"></asp:TextBox></td>
                       <td align="right">铝基板版号:</td>
                     <td><asp:TextBox ID="txtLvjiban" runat="server" Width ="100px" ReadOnly="True" Enabled="False"></asp:TextBox></td>
                     <td></td>
                     <td><asp:TextBox ID="txtPCB" runat="server" Width ="100px" ReadOnly="True" Visible="false"></asp:TextBox></td>
                   <td></td>
                     <td align="right" colspan="2">
                          <asp:Button ID="btnBom" runat="server" Text="BOM" CssClass="SmallButton3" />
                         
                     </td>
                </tr>
                <tr id="TR1" runat="server">
                    <td align="right">
                        巡检类别:
                    </td>
                   
                    <td runat="server"  style="height: 22px">
                    <asp:DropDownList ID="dType" runat="server" Width="100%" DataTextField="typeName"
                     DataValueField="typeID" AutoPostBack="True" OnSelectedIndexChanged="dType_SelectedIndexChanged">
                        </asp:DropDownList>
                
                    </td>
                    <td align="right">
                        检查工序:
                    </td>
                   
                    <td runat="server"  style="height: 22px">
                        <asp:DropDownList ID="dropProject" runat="server" DataTextField="proName" DataValueField="proName"
                            Width="100px" OnSelectedIndexChanged="dropProject_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 100px; height: 22px;" align="right">
                        检验时间（整点）:
                    </td>
                     <td id="TD7" runat="server" style="height: 22px">
                        <asp:DropDownList ID="dropType" runat="server" DataTextField="typeName" DataValueField="typeID"
                            Width="113px" OnSelectedIndexChanged="dropType_SelectedIndexChanged" AutoPostBack="True" Visible ="false">
                        </asp:DropDownList>
                          <asp:DropDownList ID="dropDate" runat="server" DataTextField="dateValue" DataValueField="dateText"
                            Width="113px" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                    <td align="right">检验水平:</td>
                    <td  id="TD5" runat="server" style="height: 22px">
                        <asp:DropDownList ID="dropLevel" runat="server" DataTextField="gbtLevel" DataValueField="gbtLevel"
                            Width="75px" AutoPostBack="True" OnSelectedIndexChanged="dropLevel_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right"> 接受质量限:</td>
                    <td id="TD6" runat="server" style="height: 22px">
                       <asp:DropDownList ID="dropAql" runat="server" DataTextField="AQL" DataValueField="AQL"
                            Width="69px" AutoPostBack="True" OnSelectedIndexChanged="dropAql_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right"> 不良数:</td>
                    <td id="TD4" runat="server" style="height: 22px;width:100px">
                        <asp:TextBox ID="txtNum" runat="server" CssClass="SmallTextBox" Width="42px">0</asp:TextBox>
                    </td>
                    <td style="width: 162px; height: 22px;" id="TD2" runat="server" align="right">
                        检查数量:<asp:TextBox ID="txtCheckNum" runat="server" CssClass="SmallTextBox" Width="42px">0</asp:TextBox>
                    </td>
                </tr>
               
            </table>
            <table cellspacing="0" cellpadding="1" width="1000" id="TR2" class="MainContent_inner"
                runat="server">
                <tr>
                    <td id="TD10" runat="server">
                        <asp:Label ID="lblNothing" runat="server" Text="无" Width="43px" Visible ="false"></asp:Label>
                    </td>
                    <td colspan="4" style="height: 15px" id="TD8" runat="server">
                        <asp:DataList ID="dltItem" runat="server" RepeatDirection="Horizontal" HorizontalAlign="Left" Visible ="false"
                            Width="747px" RepeatColumns="9">
                            <ItemTemplate>
                                <asp:Label ID="lblpItemID" runat="server" Text='<%#Bind("pItemID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("pItemName") %>'></asp:Label>
                                <asp:TextBox ID="txt" runat="server" Width="47px" Text="0" CssClass="SmallTextBox"></asp:TextBox>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                    <td runat="server" colspan="1" style="height: 15px">
                    </td>
                    <td id="TD9" runat="server">
                    </td>
                </tr>
                <tr>
                    <td runat="server" colspan="5" style="height: 15px">
                        备注:<asp:TextBox ID="txtRemarks" runat="server" CssClass="SmallTextBox" Width="754px"></asp:TextBox>
                    </td>
                    <td runat="server" colspan="1" style="height: 15px">
                        <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" Text="添加" Visible="True"
                            Width="60px" OnClick="btnAdd_Click" />
                    </td>
                    <td runat="server">
                        <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton2"
                            Text="返回" Visible="True" Width="60px" OnClick="btnBack_Click" />
                    </td>
                </tr>

            </table>

            <table cellspacing="0" cellpadding="1" width="1000"  class="MainContent_inner">
                            <tr>
                    <td style="height: 4px;" colspan="9">
                    </td>
                </tr>
                <tr>
                    <td colspan="10" style="vertical-align: top;">
                        <asp:Panel ID="Panel1" Style="overflow: auto; text-align: center;" runat="server"
                            Width="960px" BorderWidth="1px" BorderColor="Black" ScrollBars="Auto" Height="400px"
                            Wrap="False">
                            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                DataKeyNames="repID" OnRowDeleting="gvReport_RowDeleting" AllowPaging="True"
                                Height="10px" OnPageIndexChanging="gvReport_PageIndexChanging" OnRowDataBound="gvReport_RowDataBound"
                                Width="1700px">
                                <Columns>
                                    <asp:BoundField>
                                        <HeaderStyle Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="proName" HeaderText="检查工序">
                                        <HeaderStyle Width="150px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="proRemark" HeaderText="检验内容">
                                        <HeaderStyle Width="300px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="yangben" HeaderText="AQL/样本">
                                        <HeaderStyle Width="120px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="gbtLevel" HeaderText="检验水平">
                                        <HeaderStyle Width="70px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="aqlOrig" HeaderText="检查数量">
                                        <HeaderStyle Width="70px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ac" HeaderText="Ac">
                                        <HeaderStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Re" HeaderText="Re">
                                        <HeaderStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="repQty" HeaderText="不良数">
                                        <HeaderStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="缺陷内容" Visible ="false">
                                        <HeaderStyle Width="440px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="判定" DataField="repResult">
                                        <HeaderStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:CommandField DeleteText="<u>删除</u>" ShowDeleteButton="True">
                                        <HeaderStyle Width="40px" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="remarks" HeaderText="备注">
                                        <ItemStyle Width="400px" />
                                        <HeaderStyle Width="500px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="username" HeaderText="检验员">
                                        <HeaderStyle Width="50px" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="repTime" HeaderText="检验时间">
                                         <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                
            </table>
        </div>
        <br />
        <asp:Label ID="Label2" runat="server" Text="0" Visible="False"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="0" Visible="False"></asp:Label>
        <asp:Label ID="lblGroup" runat="server" Text="0" Visible="False"></asp:Label>
        <asp:Label ID="lblFlag" runat="server" Text="0" Visible="False"></asp:Label>
        <asp:Label ID="lblPage" runat="server" Text="0" Visible="False"></asp:Label>
        <asp:Label ID="lblPrhid" runat="server" Text="0" Visible="False"></asp:Label>
        <asp:Label ID="lblIdentity" runat="server" Text="0" Visible="False"></asp:Label><br />
        <asp:TextBox ID="TextBox1" runat="server" Width="667px" Visible="False"></asp:TextBox>
            <input type="hidden" id="hidDomain" runat="server"/>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
