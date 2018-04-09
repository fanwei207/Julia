<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prodcostnew.aspx.cs" Inherits="prodcostnew" %>

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
        <label for="project_code">项目代码:</label>
        <input id="project_code" style="width: 100px" maxlength="10" runat="server"/>
        &nbsp;&nbsp;
        <label for="part_code">零件号:</label>
        <input id="part_code" style="width: 150px" runat="server"/>
        &nbsp;&nbsp;
        <label for="tax">含税比例:</label>
        <input id="tax" value="1.00" style="width: 60px" runat="server"/>
        &nbsp;&nbsp;
        <label for="rate">美元汇率:</label>
        <input id="rate" style="width: 80px" runat="server"/>
        &nbsp;&nbsp;
        <label for="dif">价格差异率基准:</label>
        <input id="dif" value="0.05" style="width: 60px" runat="server"/>
        </td>
        <td align="left">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <button id="btnQuery" runat="server" onserverclick="btnQuery_Click">查询</button>
        &nbsp;&nbsp;
        <button id="btnExport" runat="server" onserverclick="btnExport_Click">导出</button>
        </td>
        </tr> 
        </table>
        </form>
</div>
<script>
    $(document).ready(function () {

        function onClick1(e) {
            if ($("#project_code").val() == "" && $("#part_code").val() == "") {
                alert("请输入零件号或者项目号！");
                return false;
            }
            if ($("#tax").val() == "") {
                alert("Tax can not be empty!");
                return false;
            }
            if ($("#rate").val() == "") {
                alert("Rate can not be empty!");
                return false;
            }
            if ($("#dif").val() == "") {
                alert("Dif can not be empty!");
                return false;
            }
			
        }

        function Query(e) {
            onClick1();
            //dataSrc.read();
        }

        $("#project_code").kendoMaskedTextBox({
        });

        $("#part_code").kendoMaskedTextBox({
        });

        $("#tax").kendoNumericTextBox({
            decimals: 2,
            min: 1.00,
            max: 1.17,
            step: 0.01
        });
        $("#rate").kendoNumericTextBox({
            decimals: 8,
            min: 1,
            max: 20,
            step: 0.01
        });
        $("#dif").kendoNumericTextBox({
            format: "p0",
            min: 0,
            max: 0.10,
            step: 0.01
        });
        $("#btnQuery").kendoButton({
            click: Query
        });

        $("#btnExport").kendoButton({
            click: onClick1
        });

    });
</script>
<div> 
</div>
<br/>
<div id="prodcost">
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
        url: "prodcostb.ashx",
        dataType: "json",
        contentType: "application/json",
        data: {
        project:$("#project_code").val(),
        part:$("#part_code").val(),
        tax:$("#tax").val(),
        rate:$("#rate").val(),
        dif:$("#dif").val()
        }
    },
    /*parameterMap: function (options, operation) {
        if (operation == "read") {
            var parameter = {
                page: options.page,
                pageSize: options.pageSize
            };
            return kendo.stringify(parameter);
        }
    }*/  /*不启用服务器分页*/ /*若启用必须使用POST方式*/

},
/*
success:function(result){
//hideLayer()
alert("success!");
},  
error:function(){  
//hideLayer()
},*/
schema: {
    //取回数据
    data: function (d) { return d.Data; },
    //取出总数
    total: function (d) { return d.TotalCount; },
    model: {
            fields: {
                lel: { type: "number" },
                ptype: { type: "string" },
                pspar: { type: "string" },
                pscomp: { type: "string" },
                pdesc2: { type: "string" },
                pstat: { type: "string" },
               // uqty: { type: "number" },
                psqty: { type: "number" },
                pcurr: { type: "string" },
                porc: { type: "number" },
                cur_mtl_tl: { type: "number" },
                pcvend: { type: "string" },
                isbond: { type: "string" },
                mtl: { type: "number" },
                lbr: { type: "number" },
                bdn: { type: "number" }
                }
           }
},
pageSize: 15,
/*serverPaging: true,
serverFiltering: true,
serverSorting: true,*/ /*不启用服务器分页*/

aggregate: [{ field: "lel", aggregate: "max" },
{ field: "pspar", aggregate: "count" },
{ field: "mtl", aggregate: "sum" },
{ field: "lbr", aggregate: "sum" },
{ field: "bdn", aggregate: "sum"}]

});
//合计
$("#grid").kendoGrid({
            dataSource: dataSrc,
            height: 500,
            filterable: true,
            sortable: true,
            pageable: true,

columns: [
{
   field: "lel",
   title: "层",
   aggregates: ["max"],
   footerTemplate: "层: #=max#",
   width: 30,
   filterable: false
},
{
    field: "ptype",
    title: "分类",
    width: 80
},
{
    field: "pspar",
    title: "父零件",
    aggregates: ["count"],
    footerTemplate: "零件计数: #=count#",
    width: 105,
    filterable: false
},
{
    field: "pscomp",
    title: "子零件",
    width: 105,
    filterable: false
},
{
    field: "pdesc2",
    title: "描述",
    filterable: false
},
{
    field: "pstat",
    title: "状态",
    width: 65
},
/*
{
    field: "uqty",
    title: "本层用量",
    width: 65,
    filterable: false
},
*/
{
    field: "psqty",
    title: "单位用量",
    width: 65,
    filterable: false
},
{
    field: "pcurr",
    title: "币种",
    width: 40,
    filterable: false
},
{
    field: "porc",
    title: "原币价格",
    width: 65,
    filterable: false
},

{
    field: "cur_mtl_tl",
    title: "单位价格",
    width: 65,
    filterable: false
},
{
    field: "pcvend",
    title: "供应商"
},
{
    field: "isbond",
    title: "保税",
    width: 40,
    filterable: false
},
{
    field: "mtl",
    title: "材料",
    aggregates: ["sum"],
    footerTemplate: "材料合计: #=sum#",
    width: 75,
    filterable: false
},
{
    field: "lbr",
    title: "人工",
    aggregates: ["sum"],
    footerTemplate: "人工合计: #=sum#",
    width: 70,
    filterable: false
},
{
    field: "bdn",
    title: "费用",
    aggregates: ["sum"],
    footerTemplate: "费用合计: #=sum#",
    width: 70,
    filterable: false
}],
    sortable: true,
    pageable: {
    refresh: true,
    pageSizes: true
    }
});
   });
</script>
</div>
    <script type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>

