<%@ Page Language="C#" AutoEventWireup="true" CodeFile="custbondedit.aspx.cs" Inherits="CustBond_custbondedit" %>

<!DOCTYPE HTML>
<html>
<head>
<title></title>
<link media="all" href="../css/main.css" rel="stylesheet" />
<link media="all" href="../css/kendo.common.min.css" rel="stylesheet" />
<link media="all" href="../css/kendo.default.min.css" rel="stylesheet" />
<script src="../script/jquery.min.js"></script>
<script src="../script/kendo.all.min.js"></script>
<script src="../Script/kendo.web.min.js"></script>
<script src="../script/kendo.culture.zh-CN.min.js"></script>
<script src="../Script/kendo.messages.zh-CN.js"></script>
</head>
<body>
<br/>
<div align="center">
 <form id="form1" runat="server">
  <table>
        <tr>
        <td>
        <label for="vend">供应商:</label>
        <input id="vend" style="width: 100px" maxlength="10" runat="server"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <label for="date">订单日期:</label>
        <input id="date" style="width: 90px" maxlength="10" runat="server"/>
        &nbsp;&nbsp;
        <label for="date1">至:</label>
        <input id="date1" style="width: 90px" maxlength="10" runat="server"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <label for="part">物料:</label>
        <input id="part" style="width: 160px" runat="server"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <label for="bstatus">维护状态:</label>
        <input id="bstatus" value="0" style="width: 80px" maxlength="10" runat="server"/>
        </td>
        <td align="left">
        &nbsp;&nbsp;&nbsp;&nbsp;
        <button id="btnQuery" runat="server" onserverclick="btnQuery_Click">查询数据</button>
        </td>
        </tr> 
      
        </table>
        </form>
</div>
<script>
    $(document).ready(function () {
        var ldata = [
            { text: "未维护", value: "0" },
            { text: "已维护", value: "1" },
            { text: "全部", value: "2" }
                   ];
        $("#bstatus").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: ldata,
            index: 0,
            change: onChange
        });

        function onChange() {
            var value = $("#bstatus").val();
        };

        function Query(e) {
            //dataSrc.read();
        }

        $("#vend").kendoMaskedTextBox({
        });

        $("#date").kendoDatePicker({
            format: "yyyy-MM-dd"
        });

        $("#date1").kendoDatePicker({
            format: "yyyy-MM-dd"
        });

        $("#part").kendoMaskedTextBox({
        });

        $("#btnQuery").kendoButton({
            click: Query
        });

        $("#btnUp").kendoButton({
            //click: Query1
        });

    });
</script>
<div> 
</div>
<br/>
<div id="custbondedit">
<div id="grid"></div>
<script>
$(document).ready(function () {
//建立来源
kendo.culture("zh-CN");
var dataSrc = new kendo.data.DataSource({
//type: "json",
transport: {
    read: {
    //AJAX
        type: "GET",
        url: "custbond.ashx",
        dataType: "json",
        contentType: "application/json",
        data: {
        vend:$("#vend").val(),
        date:$("#date").val(),
        date1:$("#date1").val(),
        part:$("#part").val(),
        bstatus:$("#bstatus").val()
        }
    },
    update: {
        type: "GET",
        url: "custbondu.ashx",
        dataType: "json",
        contentType: "application/json"
  }  
},
schema: {
    //取回数据
    data: function (d) { return d.Data; },
    total: function (d) { return d.TotalCount; },
    model: {
     id: "podid", 
            fields: {
                //podomain: { type: "string",editable: false},
                ponbr: { type: "string",editable: false },
                povend: { type: "string",editable: false },
                poline: { type: "string",editable: false },
                //posite: { type: "string",editable: false },
                popart: { type: "string",editable: false },
                podesc: { type: "string", editable: false },
                poord: { type: "number", editable: false },
                poqty: { type: "number",editable: false },
                podate: { type: "string", editable: false },
                poprice: { type: "number", editable: false },
                ctdnumber: { type: "string"},
                ctdrmks: { type: "string"}
                }
           }
},
pageSize: 15,
aggregate: [
{ field: "poord", aggregate: "sum" },
{ field: "poqty", aggregate: "sum" }]
//聚合
});
$("#grid").kendoGrid({
            dataSource: dataSrc,
            height: 480,
            filterable: true,
            sortable: true,
            pageable: true,
columns: [
/*{
   field: "podomain",
   title: "域",
   width: 50
},
*/
{
   field: "ponbr",
   title: "采购单",
   width: 85
},
{
    field: "povend",
    title: "供应商",
    width: 70,
    filterable: false
},
{
    field: "poline",
    title: "行",
    width: 30,
    filterable: false
},
/*{
    field: "posite",
    title: "地点",
    width: 40,
    filterable: false
},*/
{
    field: "popart",
    title: "零件号",
    width: 105
},
{
    field: "podesc",
    title: "描述",
    filterable: false
},
{
    field: "poord",
    title: "订单数量",
    aggregates: ["sum"],
    footerTemplate: "数量合计: #=sum#",
    width: 75,
    filterable: false
},
{
    field: "poqty",
    title: "收货数量",
    aggregates: ["sum"],
    footerTemplate: "数量合计: #=sum#",
    width: 75,
    filterable: false
},
{
    field: "podate",
    title: "订单日期",
    width: 75,
    filterable: false
},
{
    field: "poprice",
    title: "单价(USD)",
    width: 85,
    filterable: false
},
{
    field: "ctdnumber",
    title: "海关手册号",
    width: 125,
    filterable: false
},
{
    field: "ctdrmks",
    title: "备注信息",
    filterable: false
},
{ command: ["edit"], title: "操作", width: "160px" }
],
    editable: "inline",
    pageable: {
    pageSizes: true
    }
});
	//行标签格式等定义	
    });
</script>
</div>
</body>
</html>

