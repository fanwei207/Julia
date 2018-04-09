<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_flux_mstr.aspx.cs" Inherits="QC_qc_flux_mstr" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
            <div style="width: 1002px;" class="MainContent_outer">
                <table cellspacing="0" cellpadding="1" width="1000" class="MainContent_inner">
                    <tr>
                        <td align="left">产品型号:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtProduct" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left"></td>
                        <td align="left">QAD号:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtQad" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left"></td>
                        <td align="left">裸灯号:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtOldPro" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left"></td>
                        <td align="left">裸灯QAD号:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtOldQad" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">订单号:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPo" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>客户:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>加工单号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtWo" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>供应商:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProvider" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td>生产周期:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPeriod" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>电子版号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVersion" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td align="left">送测部门:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDept" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>

                        <td>测试日期:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date" Width="80px"></asp:TextBox>
                            ---
                    <span style="background-color: #bbb; width: 230px; padding-top: 3px; padding-bottom: 0px">&nbsp;
                    <asp:TextBox ID="txtDate1" runat="server" CssClass="smalltextbox Date" Width="80px"></asp:TextBox>
                        <span style="color: red">*本填写框只在查询时使用</span></span></td>
                    </tr>
                    <tr style="display: none">


                        <td align="left" >灯管号:
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtLamp" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td >批次号:
                        </td>
                        <td >
                            <asp:TextBox ID="txtSeries" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>临时老化台号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTemp" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>长点老化台号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAging" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="left">测试员:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTester" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>状态:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropStatu" runat="server" Width="71px">
                                <asp:ListItem Value="--">--</asp:ListItem>
                                <asp:ListItem Value="创建">创建</asp:ListItem>
                                <asp:ListItem>测试</asp:ListItem>
                                <asp:ListItem>完成</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>类型:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" Width="100px">
                                <asp:ListItem Value="--">--</asp:ListItem>
                                <asp:ListItem Value="1">技术样品</asp:ListItem>
                                <asp:ListItem Value="2">量产留样品</asp:ListItem>
                                <asp:ListItem Value="3">市场样品</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>备注:
                        </td>
                        <td colspan="9">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="SmallTextBox" Width="80%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" Text="查询" Visible="True"
                                Width="34px" OnClick="btnQuery_Click" /><asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3"
                                    Text="添加" Visible="True" Width="34px" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="SmallButton3" Text="清空" Visible="True"
                                Width="34px" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr runat="server" id="trupload1">
                        <td>标准文件导入:
                        </td>
                        <td colspan="11">测试时间:<asp:TextBox ID="txtHour" runat="server" CssClass="SmallTextBox" Width="83px"></asp:TextBox>/h
                    &nbsp; &nbsp;点燃方式:<asp:DropDownList ID="dropWay" runat="server" CssClass="SmallTextBox"
                        Width="67px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem Value="UP">UP </asp:ListItem>
                        <asp:ListItem Value="DOWN">DOWN</asp:ListItem>
                    </asp:DropDownList>
                            <br />
                            <input id="txtFile" runat="server" class="SmallTextBox" style="width: 612px; height: 22px"
                                type="file" />
                            <input id="btnUploadTxt" runat="server" class="SmallButton2" name="uploadBtn" style="width: 80px"
                                type="button" validationgroup="Import" value="导入数据" onserverclick="btnUploadTxt_ServerClick" />
                        </td>
                    </tr>
                    <tr runat="server" id="trupload2">
                        <td>Excel文件导入:
                        </td>
                        <td colspan="11">
                            <input id="excelFile" runat="server" class="SmallTextBox" style="width: 612px; height: 22px"
                                type="file" />
                            <input id="btnUploadExcel" runat="server" class="SmallButton2" name="uploadBtn" style="width: 80px"
                                type="button" validationgroup="Import" value="导入数据" onserverclick="btnUploadExcel_ServerClick" />
                            &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnTemplate" Text="下载模板" runat="server" class="SmallButton2" Style="width: 80px" OnClick="btnTemplate_Click" />
                        </td>
                    </tr>
                    <tr runat="server" id="trupload3">
                        <td>附件导入：</td>
                        <td colspan="11">
                            <input id="filename" style="width: 612px; height: 22px" type="file" name="filename1"
                                runat="server" />
                            <input id="btnUplodeFile" runat="server" class="SmallButton2" name="btnUplodeFile" style="width: 80px"
                                type="button" validationgroup="Import" value="导入数据" onserverclick="btnUplodeFile_Click" />

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px;" colspan="11"></td>
                    </tr>
                    <tr>
                        <td colspan="11" style="vertical-align: top;">
                            <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="960px" BorderWidth="1px"
                                BorderColor="Black" ScrollBars="Auto" Height="305px" HorizontalAlign="Left">
                                <asp:GridView ID="gvFluxMstr" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PageSize="6" TabIndex="5" Width="1650px" OnRowCommand="gvFluxMstr_RowCommand"
                                    DataKeyNames="fl_id,fl_type" OnRowDeleting="gvFluxMstr_RowDeleting" OnRowDataBound="gvFluxMstr_RowDataBound"
                                    OnPageIndexChanging="gvFluxMstr_PageIndexChanging" CssClass="GridViewStyle">
                                    <Columns>
                                        <asp:BoundField HeaderText="产品型号" DataField="fl_product">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="QAD号" DataField="fl_qad">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fl_oldpro" HeaderText="老部件号">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fl_oldqad" HeaderText="老QAD号">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="订单号" DataField="fl_po">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="客户" DataField="fl_cust">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="加工单号" DataField="fl_wo">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="供应商" DataField="fl_provider">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="灯管号" DataField="fl_lamp" Visible="false">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="批次号" DataField="fl_series" Visible="false">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="生产周期" DataField="fl_period">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="电子版号" DataField="fl_version">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="送测部门" DataField="fl_dept">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="临时老化台号" DataField="fl_temp" Visible="false">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="长点老化台号" DataField="fl_aging" Visible="false">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="测试日期" DataField="fl_date" DataFormatString="{0:yyyy-MM-dd}"
                                            HtmlEncode="False">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="测试员" DataField="fl_tester">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fl_statu" HeaderText="状态">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fl_typeName" HeaderText="类型">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="备注" DataField="fl_rmks">
                                            <HeaderStyle Width="150px" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkDetail" runat="server">详细</asp:LinkButton>
                                            </ItemTemplate>
                                            <ControlStyle Font-Underline="True" />
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("fl_id") %>'
                                                    CommandName="Cust_Edit">编辑</asp:LinkButton>
                                            </ItemTemplate>
                                            <ControlStyle Font-Underline="True" />
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:CommandField DeleteText="<u>删除</u>"
                                            ShowDeleteButton="True">
                                            <ControlStyle Font-Underline="True" />
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                </asp:GridView>
                            </asp:Panel>
                            <asp:TextBox ID="txtID" runat="server" CssClass="SmallTextBox" Width="140px" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
