<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiHrdAndDetList.aspx.cs" Inherits="EDI_EdiHrdAndDetList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function expandcollapse(obj,row)
        {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);
        
            if (div.style.display == "none")
            {
                div.style.display = "block";
                if (row == 'alt')
                {
                    img.src = "/images/minus.gif";
                }
                else
                {
                    img.src = "/images/minus.gif";
                }
            }
            else
            {
                div.style.display = "none";
                if (row == 'alt')
                {
                    img.src = "/images/plus.gif";
                }
                else
                {
                    img.src = "/images/plus.gif";
                }
            }
            $.loading("none");
        }

    </script>
</head>
<body>
    <div align="left">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" bgcolor="#f4f4f4" border="0" style="width: 1040px;
            border: 1px solid white;">
            <tr>
                <td>
                    <asp:RadioButton ID="rbNormal" runat="server" GroupName="gpType" Checked="true" Text="Can Import"
                        AutoPostBack="True" OnCheckedChanged="rbNormal_CheckedChanged" />
                    <asp:RadioButton ID="rbError" runat="server" GroupName="gpType" Text="Can Not Import"
                        AutoPostBack="True" OnCheckedChanged="rbError_CheckedChanged" />&nbsp;
                    <asp:RadioButton ID="rbPartError" runat="server" GroupName="gpType" Text="Should Import"
                        AutoPostBack="True" OnCheckedChanged="rbPartError_CheckedChanged" Visible="false"/>
                    <asp:RadioButton ID="rbFinish" runat="server" GroupName="gpType" Text="Imported"
                        AutoPostBack="True" OnCheckedChanged="rbFinish_CheckedChanged" />&nbsp;
                    订单号:<asp:TextBox
                            ID="txtOrder" runat="server" CssClass="smalltextbox" Width="58px"> </asp:TextBox>
                    导入日期:<asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date" Width="75px"></asp:TextBox>             
                    &nbsp;
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnImport" runat="server" Text="Import to Qad" OnClick="btnImport_Click"
                        OnClientClick="oneclick();" CssClass="SmallButton3" Width="86px" />
                    <asp:Button ID="btnImport_TCB" runat="server" Text="Import to Qad" OnClick="btnImport_TCB_Click"
                        OnClientClick="oneclick();" CssClass="SmallButton3" Width="86px" />
                    <asp:Button ID="btnCimload" runat="server" Text="Cimload" OnClick="btnCimload_Click"
                        OnClientClick="oneclick();" CssClass="SmallButton3" Width="86px" Visible="false" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExportExcel" runat="server" Text="Export Excel" OnClick="btnExportExcel_Click"
                        CssClass="SmallButton3" Width="70" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="更新" CssClass="SmallButton2" OnClick="Button1_Click"
                        Width="32px" />
                    <asp:HiddenField ID="hidTCB" runat="server" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvlist" name="gvlist" runat="server" 
            style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 30px"
            AllowPaging="True" AutoGenerateColumns="False"
            PageSize="20" Width="1370px" OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
            OnRowCommand="gvlist_RowCommand"
            DataKeyNames="id,domain,notNeeded,detError,detNoSite,inBigOrder,fob,poNbr,mpo_domain"
            CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
               <asp:TemplateField>
                    <ItemTemplate>
                        <a href="javascript:expandcollapse('div<%# Eval("id") %>', 'one');">
                            <img id="imgdiv<%# Eval("id") %>" alt="Click to show/hide Details" width="9px" border="0" src="/images/plus.gif" onclick="return imgdiv<%# Eval("id") %>_onclick()"/>
                        </a>
                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblPoId" runat="server" Text='<%# Bind("id")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO Number" SortExpression="poNbr">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("poNbr")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tcp So Number" SortExpression="fob">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("fob")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Due Date" SortExpression="dueDate">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("dueDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ship Via" SortExpression="shipVia">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblPoNbr" runat="server" Text='<%# Bind("shipVia")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ship To" SortExpression="rmk">
                    <HeaderStyle Width="240px" HorizontalAlign="Center" />
                    <ItemStyle Width="240px" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("rmk")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Import">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkImport" runat="server" CommandName="need">需要</asp:LinkButton>
                        <input id="hidToPlan" runat="server" style="width: 38px" type="hidden" value='<%# Bind("toPlan")%>' />
                        <input id="hidShouldToPlan" runat="server" style="width: 38px" type="hidden" value='<%# Bind("shouldToPlan")%>' />
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Underline="True" />
                </asp:TemplateField>
                <asp:BoundField DataField="PoRecDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="接收日期"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="qad_dueDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="截止日期"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="qad_perfDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="承诺日期"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkPlan" runat="server" CommandName="plan" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black" Text="To Plan"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="大订单">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkBigOrder" runat="server" CommandName="bigorder" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black" Text="显示"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cancel">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkCancel" runat="server" CommandName="CancelDEI">取消</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Underline="True" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="EDI Error Info" SortExpression="errMsg">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblErrorMsg" runat="server" Text='<%# Bind("errMsg")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EDI Type" SortExpression="fileType">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("fileType")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Domain">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("mpo_domain") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("id") %>" style="display:none;position:relative;left:15px;OVERFLOW: auto;WIDTH:97%" >
                                <asp:GridView ID="gvDet" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="5" Width="1320px" OnRowDataBound="gvDet_RowDataBound" OnRowEditing="gvDet_RowEditing"
                        OnPageIndexChanging="gvDet_PageIndexChanging" OnRowCancelingEdit="gvDet_RowCancelingEdit"
                        OnRowUpdating="gvDet_RowUpdating" OnRowCommand="gvDet_RowCommand"
                        DataKeyNames="id,qadPart,finished,appvResult,loadRmks,errMsg,ordQty,partNbr,poLine,parentLine,det_PoRecDate,cannotselect"
                        CssClass="GridViewStyle">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server"/>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Line">
                                <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("poLine")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Part #">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("partNbr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QAD #">
                                <HeaderStyle Width="00px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQadPart" runat="server" Text='<%# Bind("qadPart")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SKU #">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                <ItemStyle Width="70px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("sku")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Domain">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_domain" runat="server" Text='<%# Bind("domain")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Site">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" />
                                <ItemTemplate>
                                    <asp:Label ID="Labe22" runat="server" Text='<%# Bind("site")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Wo Site">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_wosite" runat="server" Text='<%# Bind("wo_site")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Qty">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ordQty")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UM">
                                <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPoNbr" runat="server" Text='<%# Bind("um")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("price")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Require Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("reqDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("dueDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Input Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_dateInfo" runat="server" Text='<%# Bind("dateInfo","{0:yyyy-MM-dd}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Load Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_loadDate" runat="server" Text='<%# Bind("loadDate","{0:yyyy-MM-dd}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approve Status">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_appvResult" runat="server" Text='<%# Bind("appvResult")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Plan">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="btnToPlan" runat="server" Text="ToPlan" Width='50' CommandArgument='<%# Eval("id") %>'
                                        CssClass="SmallButton2" CommandName="ToPlan" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="拆行">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkSplit" runat="server" CommandName="split">拆行</asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Import">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkImport" runat="server" CommandName="need">需要</asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cancel">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkCancel" runat="server" CommandName="CancelDEI">取消</asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Error Message">
                                <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_err" runat="server" Text='<%# Bind("errMsg")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Rmks" runat="server" Text='<%# Bind("Remark")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Rmks" runat="server" Text='<%# Bind("Remark")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Load Remarks">
                                <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_loadRmks" runat="server" Text='<%# Bind("loadRmks")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_loadRmks" runat="server" Text='<%# Bind("loadRmks")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                        </Columns>
                    </asp:GridView>
                                </div>
                             </td>
                        </tr>
			        </ItemTemplate>	
                </asp:TemplateField>	
            </Columns>
        </asp:GridView>
        <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="true" Value=";" />
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>

