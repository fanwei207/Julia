<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_productReworkStreamlineQC.aspx.cs" Inherits="QC_qc_productReworkStreamlineQC" %>


<!DOCTYPE html>

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
        <label style="font-size:20px">质量报告</label>
                <hr />
        <div>
            <label>结果:<asp:RadioButton id="rbPass" runat="server" GroupName="pan" Text="OK" Checked="true"/>&nbsp;<asp:RadioButton id="rbRefuse" runat="server" GroupName="pan" Text="NO"/></label>
        </div>
        <div>
             <input id="fileManager" style="width: 400px; height: 23px" type="file" size="45" name="filename2"
                                runat="server" />

        </div>
        
        <div>

            <asp:Button ID="btnPass" runat="server" CssClass="SmallButton2"
                            Text="保存" 
                            Width="78px" Height="22px" OnClick="btnPass_Click"></asp:Button>       
<%--              &nbsp;  &nbsp; 
             <asp:Button ID="btnRefuse" runat="server" CssClass="SmallButton2"
                            Text="拒绝" 
                            Width="78px" Height="22px" OnClick="btnRefuse_Click"></asp:Button> --%>
        &nbsp;  &nbsp; 
            <asp:Button ID="btnReturn" runat="server" CssClass="SmallButton2" Text="返回"  Width="78px" Height="22px" OnClick="btnReturn_Click"/>
        </div>
    </div>
    </form>
     <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
