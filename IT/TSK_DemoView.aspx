<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_DemoView.aspx.cs" Inherits="TSK_DemoView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.obj.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {

            //因为需要使用事件，所以必须注册
            //如果是Tab调取的，则无需执行
            $(".j-demo-tabs").each(function () {
                var _idHolder = $(this).attr("id");
                $(this).Tabs({ containment: "#" + _idHolder + "_holder" });
            })







        })
    </script>
    <style type="text/css">
        .j-demo-tabitem
        {
            background-color: silver;
        }
        .j-demo-tabactive
        {
            background-color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="demoHolder" style="width: 100%; height: 500px; background-color: #fff" runat="server">
    </div>
    </form>
</body>
</html>
