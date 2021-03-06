﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_CheckReviewPower.aspx.cs" Inherits="HR_hr_checkReviewPower" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    
    <div align="center" style=" margin-top:60px;">
    <p align="center">集团（公司）各职层岗位招聘申请审核权限表</p>
        <table id="review" cellspacing="4" cellpadding="0" style="width: 600px; border:1px solid #000000; margin-top:2px; font-size:13px; border-collapse:collapse;"  border="1">
            <tr style="height:25px;">
                <th style="width:50px;">类别</th><th style="width:100px;">岗位级别</th>
                <th style="width:70px;">申请</th><th style="width:80px;">审核</th>
                <th style="width:120px;">复核</th><th style="width:120px;">批准/决定</th>
                <th style="width:100px;">备案</th>
            </tr>
            <tr style="height:35px;">
                <th rowspan = "2" align="center">集团总部</th><td align="center">管理(业务)中心经理级岗位</td>
                <td align="center">相关管理(业务)中心</td><td align="center">中心分管领导</td><td align="center">人事总监</td>
                <td align="center">集团总经理</td><td rowspan = "5" align="center">集团（公司）人力资源中心</td>
            </tr>
            <tr style="height:35px;">
                <td align="center">管理(业务)中心经理级以下岗位</td><td align="center">相关管理(业务)中心</td>
                <td align="center">中心分管领导</td><td align="center">人事总监</td><td align="center">集团总经理</td>
            </tr>
            <tr style="height:35px;">
                <th rowspan = "3" align="center">子公司</th><td align="center">职能部门部长级岗位</td>
                <td align="center">人力资源部</td><td align="center">子公司总/副总经理</td><td align="center">人事总监</td><td align="center">集团总经理</td>
            </tr>
            <tr style="height:35px;">
                <td align="center">职能部门部长级以下岗位</td><td align="center">职能部门</td>
                <td align="center">人力资源部</td><td align="center">子公司总/副总经理</td><td align="center">人事总监</td>
            </tr>
            <tr style="height:25px;">
                <td align="center">一线操作岗位</td><td align="center">职能部门</td>
                <td align="center">人力资源部</td><td align="center">-</td><td>子公司总/副总经理</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
