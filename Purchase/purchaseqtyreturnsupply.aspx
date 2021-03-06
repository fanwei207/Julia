 <%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PurchaseQtyReturnSupply" CodeFile="PurchaseQtyReturnSupply.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>
         
    </title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link media="all" href="/script/main.css" rel="stylesheet">
    <script language="JavaScript">
        function TXclear() {
            var today = new Date();
            /*var year=today.getFullYear();
            var month=today.getMonth()+1;
            var day=today.getDate();
            document.getElementById("warehouse").value="";
            document.getElementById("partCode").value="";
            document.getElementById("lblPartDesc").value="";
            document.getElementById("lblPartCat").value="";
            document.getElementById("CompanyCode").value="";
            document.getElementById("lblCompName").value="";
            document.getElementById("txtEnterDate").value=year + "-" + month + "-" +day;
            document.getElementById("txtPartQty").value="";*/
            window.location.href = "/Purchase/PurchaseQtyIn.aspx?rm=" + today + Math.random();
        }
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:ValidationSummary ID="cMsg" runat="server" ShowMessageBox="True" ShowSummary="False"
            HeaderText="请注意以下输入项 !"></asp:ValidationSummary> 
        <asp:Label ID="Label1" Style="padding-top: 5px; text-align: center" runat="server"
            Width="700px" ForeColor="Black" Font-Bold="True" Font-Size="12pt" Height="30px">材料退供应商</asp:Label><br>
        <asp:Table ID="Table1" runat="server" Width="700px" BorderColor="White" BorderWidth="0px"
            GridLines="None" CellSpacing="0">
            <asp:TableRow Height="30px" BorderStyle="None" BorderColor="white">
                <asp:TableCell Text="仓库名称"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:DropDownList ID="warehouse" runat="server" Width="550px">
                    </asp:DropDownList>
                    <asp:CompareValidator Display="none" ValueToCompare="0" ControlToValidate="warehouse"
                        ID="cMsg12" Operator="NotEqual" Type="String" runat="server" ErrorMessage="选择仓库" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px" BorderStyle="None" BorderColor="white">
                <asp:TableCell Text="状态"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:DropDownList ID="status" runat="server" Width="550px">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="材料代码"></asp:TableCell>
                <asp:TableCell ColumnSpan="2">
                    <asp:TextBox TabIndex="0" runat="server" AutoPostBack="True" Width="300px" Wrap="False"
                        ID="partCode" OnTextChanged="txtPartDescBinding"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="partCode"
                        ErrorMessage="材料代码不能为空" ID="Requiredfieldvalidator2"></asp:RequiredFieldValidator>
                </asp:TableCell>
                <asp:TableCell Text="材料分类" Width="150px"></asp:TableCell>
                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left">
                    <asp:Label runat="server" ID="lblPartCat"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="材料描述"></asp:TableCell>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Left">
                    <asp:Label runat="server" ID="lblPartDesc"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="供应商代码"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" AutoPostBack="True" Width="550px" Wrap="False"
                        ID="CompanyCode" OnTextChanged="txtCompanyCodeInputed"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="CompanyCode"
                        ErrorMessage="供应商代码不能为空" ID="Requiredfieldvalidator1"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="供应商名称"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:Label runat="server" ID="lblCompName"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="退库日期(年-月-日)"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" Width="550px" Wrap="False" ID="txtEnterDate" CssClass="smalltextbox Date"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtEnterDate"
                        ErrorMessage="退库日期不能为空" ID="Requiredfieldvalidator4"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" Display="None" Operator="DataTypeCheck" Type="Date"
                        ControlToValidate="txtEnterDate" ErrorMessage="请输入正确的日期格式（年-月-日）" ID="Comparevalidator1"></asp:CompareValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="退库数量"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" Width="550px" Wrap="False" ID="txtPartQty"  CssClass="smalltextbox Numeric"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ValidationExpression="[0-9.]+" Display="None"
                        ControlToValidate="txtPartQty" ErrorMessage="请输入正确的退库数量" ID="Regularexpressionvalidator1"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtPartQty"
                        ErrorMessage="退库数量不能为空" ID="Requiredfieldvalidator5"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="计划单号"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" Width="550px" Wrap="False" ID="txtPlanCode"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtPlanCode"
                        ErrorMessage="计划单号不能为空" ID="Requiredfieldvalidator3"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="定单号"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" Width="550px" Wrap="False" ID="txtOrderCode"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="备注"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" Width="550px" Wrap="False" ID="txtNotes"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom" Height="30px">
                <asp:TableCell ColumnSpan="1" Width="150px" HorizontalAlign="center">
                    <asp:Button runat="server" Width="100px" Text="退库" OnClick="addRecord" TabIndex="0"
                        CssClass="SmallButton2" ID="BtnAdd"></asp:Button>
                    <asp:Label runat="server" Width="0" Visible="False" ID="partID"></asp:Label>
                    <asp:Label runat="server" Width="0" Visible="False" ID="CompanyID"></asp:Label>
                    <asp:Label runat="server" Width="0" Visible="false" ID="lblOrderID"></asp:Label>
                    <asp:Label runat="server" Width="0" Visible="False" ID="lblPlanID"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="200px" Text="&#160;"></asp:TableCell>
                <asp:TableCell Width="150px" Text="&#160;"></asp:TableCell>
                <asp:TableCell Text="&#160;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" ColumnSpan="1" Width="200px" HorizontalAlign="center">
							<input type="button" value="&nbsp;清除&nbsp;" onclick="TXclear();" TabIndex="0" class="SmallButton2"
								style="width:100px" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br>
         </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    <script language="vbscript" type="text/vbscript"> 
			Sub document_onkeydown 
				if window.event.srcelement.id="BtnAdd" then
					exit sub
				end if					
				if window.event.keyCode=13 then 
					window.event.keyCode=9 
				end if 
			End Sub 
    </script>
</body>
</html>
