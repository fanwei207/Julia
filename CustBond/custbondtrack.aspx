<%@ Page Language="C#" AutoEventWireup="true" CodeFile="custbondtrack.aspx.cs" Inherits="CustBond_custbondtrack" %>

<!DOCTYPE html>
<html>
<head>
<title></title>
<link media="all" href="../css/main.css" rel="stylesheet" />
<link media="all" href="../css/kendo.common.min.css" rel="stylesheet" />
<link media="all" href="../css/kendo.default.min.css" rel="stylesheet" />
<script src="../script/jquery.min.js"></script>
<script src="../script/kendo.all.min.js"></script>
<script src="../Script/kendo.web.min.js"></script>
<script src="../Script/jszip.min.js"></script>
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
        <label for="number">海关手册号:</label>
        <input id="number" style="width: 160px" runat="server"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <label for="date">日期:</label>
        <input id="date" style="width: 90px" maxlength="10" runat="server"/>
        &nbsp;&nbsp;
        <label for="date1">至:</label>
        <input id="date1" style="width: 90px" maxlength="10" runat="server"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <label for="part">物料:</label>
        <input id="part" style="width: 160px" runat="server"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <label for="bstatus">确认状态:</label>
        <input id="bstatus" value="2" style="width: 80px" maxlength="10" runat="server"/>
        </td>
        <td align="left">
        &nbsp;&nbsp;&nbsp;&nbsp;
        <button id="btnQuery" runat="server" onserverclick="btnQuery_Click">查询</button>
        &nbsp;&nbsp;
        <button id="btnExport" runat="server" onserverclick="btnExport_Click">导出明细</button>
        </td>
        </tr> 
        </table>
        </form>
</div>
 <script>
     $(document).ready(function () {
         var ldata = [
            { text: "未确认", value: "0" },
            { text: "已确认", value: "1" },
            { text: "已维护", value: "2" },
            { text: "未维护", value: "3" }
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

         $("#number").kendoMaskedTextBox({
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

         $("#btnExport").kendoButton({
             //click: onClick1
         });


     });
</script>
<div> 
</div>
<br/>
 <div id="custbondtrack">
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
        url: "custbondc.ashx",
        dataType: "json",
        contentType: "application/json",
        data: {
        number:$("#number").val(),
        date:$("#date").val(),
        date1:$("#date1").val(),
        part:$("#part").val(),
        bstatus:$("#bstatus").val(),
        timemp:Date()
        }
    }
},
schema: {
    //取回数据
    data: function (h) { return h.Data; },
    total: function (h) { return h.TotalCount; },
    model: {
            fields: {
                ctdnumber: { type: "string" },
                ctdrmks: { type: "string" },
                ponbr: { type: "string" },
                poline: { type: "string" },
                popart: { type: "string" },
                poord: { type: "number" },
                poqty: { type: "number"},
                realqty: { type: "number" },
                podate: { type: "string" },
                poprice: { type: "number" }
                }
           }
},
pageSize: 15,
aggregate: [
{ field: "poord", aggregate: "sum" },
{ field: "poqty", aggregate: "sum" },
{ field: "realqty", aggregate: "sum"}]
//聚合

});

$("#grid").kendoGrid({
            toolbar: ["excel"],
            excel: {
                allPages: true
            },
            dataSource: dataSrc,
            height: 450,
            filterable: true,
            sortable: true,
            pageable: true,
            detailInit: detailInit,
            dataBound: function() {
            //detailSrc.read();
            //this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
columns: [
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
{
   field: "ponbr",
   title: "采购单",
   width: 85
},
{
    field: "poline",
    title: "行",
    width: 30,
    filterable: false
},
{
    field: "posite",
    title: "地点",
    width: 40,
    filterable: false
},
{
    field: "popart",
    title: "零件号",
    width: 105
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
    field: "realqty",
    title: "出运核销数量",
    aggregates: ["sum"],
    footerTemplate: "数量合计: #=sum#",
    width: 100,
    filterable: false
},
{
    field: "poprice",
    title: "单价(USD)",
    width: 85,
    filterable: false
},
{
    field: "podate",
    title: "订单日期",
    width: 80,
    filterable: false
},

//{ command: { text: "作废", click: showDetails }, title: "处理", width: 80 }
],
//editable: "inline",
    pageable: {
    pageSizes: true
    }
});
/*
function showDetails(e) {
	e.preventDefault();
	alert("");
};		*/
function detailInit(e) {
//明细
var detailSrc = new kendo.data.DataSource({
//type: "json",
transport: {
    read: {
    //AJAX
        type: "GET",
        url: "custbondh.ashx",
        dataType: "json",
        contentType: "application/json",
        data: {
        hid:e.data.podid
        }
    },
},
schema: {
    //取回数据
    data: function (d) { return d.Data; },
    total: function (d) { return d.TotalCount; },
    model: {
            fields: {
                sonbr: { type: "string" },
                soline: { type: "string" },
                cust: { type: "string" },
                shipto: { type: "string" },
                name: { type: "string" },
                shipqty: { type: "number" },
                shipdate: { type: "string" },
                createdate: { type: "string" },
                sodid: { type: "string" }
                }
//上述字段名称必须与返回的OBJECT相对应
           }
},
pageSize: 5,
//filter: { field: "tsguid", operator: "eq", value: e.data.tpguid },
aggregate: [{ field: "shipqty", aggregate: "sum" }]
});

$("<div/>").appendTo(e.detailCell).kendoGrid({
        dataSource: detailSrc,
            height: 220,
            scrollable: false,
            filterable: true,
            sortable: true,
            pageable: true,
columns: [
{
   field: "sonbr",
   title: "销售订单",
   width: 100
},
{
    field: "soline",
    title: "行",
    width: 30,
    filterable: false
},
{
    field: "cust",
    title: "销往",
    width: 85
},
{
    field: "shipto",
    title: "运往",
    width: 85
},
{
    field: "name",
    title: "名称",
    filterable: false
},
{
    field: "shipqty",
    title: "出运核销数量",
    width: 100,
    filterable: false
},
{
    field: "shipdate",
    title: "出运日期",
    width: 75,
    filterable: false
},
{
    field: "createdate",
    title: "计算日期",
    width: 75,
    filterable: false
}
]
    });
    };
    
function Query(e){
};

$("#btnQuery").kendoButton({
click: Query
});

});

</script>
</div>
    <script type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>    
</body>
</html>
