using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;

public partial class plan_PCD_View : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["poNbr"] != null)
            {
                txtpoNbr.Text = Request.QueryString["poNbr"];
            }
            if (Request.QueryString["poLine"] != null)
            {
                txtLine.Text = Request.QueryString["poLine"];
            }
            BindGridView();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        DataTable dt2101 = GetData2101();
        DataTable dt210171 = GetData210171();

        DataTable dtExcel = new DataTable();
        DataColumn column;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "type";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "poNbr";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "poLine";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "region";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "cusCode";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "CustName";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "partNbr";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "part";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "sod_nbr";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "wo_nbr";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "wo_lot";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "wo_domain";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "wo_site";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "users";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "PoRecDate";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "planDate";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "actPlanDate";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "jinshujian";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "suliaojian";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "sanreqi";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "jiegoujian";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "baozhuang";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "cob";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xianluban";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xianlubanPart";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xianlubanNeedQty";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xianlubanLackQty";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xianlubanIssQty";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xianlubanType";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xianlubanNbr";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xianlubanLot";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "yinzhiban";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "dianzu";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "erjiguan";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "dianrong";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xinpianban";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xinpianbanPart";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xinpianbanNeedQty";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xinpianbanLackQty";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xinpianbanIssQty";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xinpianbanType";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xinpianbanNbr";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xinpianbanLot";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "xinpian";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "lvjiban";
        dtExcel.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "3019wuliao";
        dtExcel.Columns.Add(column);

        int index=0;
        int index1 = 0;
        foreach (DataRow row in dt.Rows)
        {
            DataRow newRow = dtExcel.NewRow();
            foreach (DataColumn col in dt.Columns)
            {
                newRow[col.ColumnName] = row[col.ColumnName];
            }
            dtExcel.Rows.Add(newRow);
            index1 = index;
            string nbr = row["wo_nbr"].ToString();
            string lot = row["wo_lot"].ToString();
            string xianluban = row["xianluban"].ToString();
            string xinpianban = row["xinpianban"].ToString();
            DataRow[] data2101Rows = dt2101.Select("wo_nbr='" + nbr + "' and wo_lot='" + lot + "'", "planDate desc");

          
            if (data2101Rows.Length > 0)
            {
                if (xianluban == "缺料" && data2101Rows[0]["issQty"].ToString() != "")
                {

                    dtExcel.Rows[index]["xianlubanPart"] = data2101Rows[0]["wodPart"];
                    dtExcel.Rows[index]["xianlubanNeedQty"] = data2101Rows[0]["needQty"];
                    dtExcel.Rows[index]["xianlubanLackQty"] = data2101Rows[0]["lackQty"];
                    newRow = dtExcel.NewRow();
                    newRow["xianluban"] = data2101Rows[0]["type"].ToString().ToUpper() == "LOC" ? "LOC" : data2101Rows[0]["planDate"];
                    newRow["xianlubanPart"] = data2101Rows[0]["wodPart"];
                    newRow["xianlubanNeedQty"] = data2101Rows[0]["needQty"];
                    newRow["xianlubanLackQty"] = data2101Rows[0]["lackQty"];
                    newRow["xianlubanType"] = data2101Rows[0]["type"];
                    newRow["xianlubanNbr"] = data2101Rows[0]["nbr"];
                    newRow["xianlubanLot"] = data2101Rows[0]["lot"];
                    newRow["xianlubanIssQty"] = data2101Rows[0]["issQty"];
                    newRow["yinzhiban"] = data2101Rows[0]["StrCol11"];
                    newRow["dianzu"] = data2101Rows[0]["StrCol12"];
                    newRow["erjiguan"] = data2101Rows[0]["StrCol13"];
                    newRow["dianrong"] = data2101Rows[0]["StrCol14"];
                    dtExcel.Rows.Add(newRow);
                    index++;
                }
                else
                {
                    if (dtExcel.Rows[index]["xianluban"].ToString() != "")
                    {
                        dtExcel.Rows[index]["xianluban"] = data2101Rows[0]["type"].ToString().ToUpper() == "LOC" ? "LOC" : data2101Rows[0]["planDate"];
                    }
                    dtExcel.Rows[index]["xianlubanPart"] = data2101Rows[0]["wodPart"];
                    dtExcel.Rows[index]["xianlubanNeedQty"] = data2101Rows[0]["needQty"];
                    dtExcel.Rows[index]["xianlubanLackQty"] = data2101Rows[0]["lackQty"];
                    dtExcel.Rows[index]["xianlubanType"] = data2101Rows[0]["type"];
                    dtExcel.Rows[index]["xianlubanNbr"] = data2101Rows[0]["nbr"];
                    dtExcel.Rows[index]["xianlubanLot"] = data2101Rows[0]["lot"];
                    dtExcel.Rows[index]["xianlubanIssQty"] = data2101Rows[0]["issQty"];
                    dtExcel.Rows[index]["yinzhiban"] = data2101Rows[0]["StrCol11"];
                    dtExcel.Rows[index]["dianzu"] = data2101Rows[0]["StrCol12"];
                    dtExcel.Rows[index]["erjiguan"] = data2101Rows[0]["StrCol13"];
                    dtExcel.Rows[index]["dianrong"] = data2101Rows[0]["StrCol14"];
                }
                for (int i = 1; i < data2101Rows.Length; i++)
                {
                    newRow = dtExcel.NewRow();
                    newRow["xianluban"] = data2101Rows[i]["type"].ToString().ToUpper() == "LOC" ? "LOC" : data2101Rows[i]["planDate"];
                    newRow["xianlubanPart"] = data2101Rows[i]["wodPart"];
                    newRow["xianlubanNeedQty"] = data2101Rows[i]["needQty"];
                    newRow["xianlubanLackQty"] = data2101Rows[i]["lackQty"];
                    newRow["xianlubanType"] = data2101Rows[i]["type"];
                    newRow["xianlubanNbr"] = data2101Rows[i]["nbr"];
                    newRow["xianlubanLot"] = data2101Rows[i]["lot"];
                    newRow["xianlubanIssQty"] = data2101Rows[i]["issQty"];
                    newRow["yinzhiban"] = data2101Rows[i]["StrCol11"];
                    newRow["dianzu"] = data2101Rows[i]["StrCol12"];
                    newRow["erjiguan"] = data2101Rows[i]["StrCol13"];
                    newRow["dianrong"] = data2101Rows[i]["StrCol14"];
                    dtExcel.Rows.Add(newRow);
                    index++;
                }
            }
            DataRow[] data210171Rows = dt210171.Select("wo_nbr='" + nbr + "' and wo_lot='" + lot + "'", "planDate desc");

            if (xinpianban == "缺料" && data210171Rows[0]["issQty"].ToString() != "")
            {
                dtExcel.Rows[index1]["xinpianbanPart"] = data210171Rows[0]["wodPart"];
                dtExcel.Rows[index1]["xinpianbanNeedQty"] = data210171Rows[0]["needQty"];
                dtExcel.Rows[index1]["xinpianbanLackQty"] = data210171Rows[0]["lackQty"];
                index1 += 1;
            }
            for (int i = 0; i < data210171Rows.Length; i++)
            {
                if (index1 + i <= index)
                {
                    if (dtExcel.Rows[index1 + i]["xinpianban"].ToString() == "")
                    {
                        dtExcel.Rows[index1 + i]["xinpianban"] = data210171Rows[i]["type"].ToString().ToUpper() == "LOC" ? "LOC" : data210171Rows[i]["planDate"];
                    }
                    dtExcel.Rows[index1 + i]["xinpianbanPart"] = data210171Rows[i]["wodPart"];
                    dtExcel.Rows[index1 + i]["xinpianbanNeedQty"] = data210171Rows[i]["needQty"];
                    dtExcel.Rows[index1 + i]["xinpianbanLackQty"] = data210171Rows[i]["lackQty"];
                    dtExcel.Rows[index1 + i]["xinpianbanType"] = data210171Rows[i]["type"];
                    dtExcel.Rows[index1 + i]["xinpianbanNbr"] = data210171Rows[i]["nbr"];
                    dtExcel.Rows[index1 + i]["xinpianbanLot"] = data210171Rows[i]["lot"];
                    dtExcel.Rows[index1 + i]["xinpianbanIssQty"] = data210171Rows[i]["issQty"];
                    dtExcel.Rows[index1 + i]["xinpian"] = data210171Rows[i]["StrCol11"];
                    dtExcel.Rows[index1 + i]["lvjiban"] = data210171Rows[i]["StrCol12"];
                    dtExcel.Rows[index1 + i]["3019wuliao"] = data210171Rows[i]["StrCol14"];
                }
                else
                {
                    newRow = dtExcel.NewRow();
                    newRow["xinpianban"] = data210171Rows[i]["type"].ToString().ToUpper() == "LOC" ? "LOC" : data210171Rows[i]["planDate"];
                    newRow["xinpianbanPart"] = data210171Rows[i]["wodPart"];
                    newRow["xinpianbanNeedQty"] = data210171Rows[i]["needQty"];
                    newRow["xinpianbanLackQty"] = data210171Rows[i]["lackQty"];
                    newRow["xinpianbanType"] = data210171Rows[i]["type"];
                    newRow["xinpianbanNbr"] = data210171Rows[i]["nbr"];
                    newRow["xinpianbanLot"] = data210171Rows[i]["lot"];
                    newRow["xinpianbanIssQty"] = data210171Rows[i]["issQty"];
                    newRow["xinpian"] = data210171Rows[i]["StrCol11"];
                    newRow["lvjiban"] = data210171Rows[i]["StrCol12"];
                    newRow["3019wuliao"] = data210171Rows[i]["StrCol14"];
                    dtExcel.Rows.Add(newRow);
                    index++;
                }

            }
            index++;
        }

        string title = "30^<b>类型</b>~^<b>订单号</b>~^<b>行号</b>~^<b>区域</b>~^<b>客户</b>~^<b>客户名称</b>~^<b>客户零件号</b>~^<b>QAD号</b>~^<b>销售单号</b>~^<b>工单号</b>~^<b>工单ID</b>~^<b>域</b>~^<b>地点</b>~^<b>客户PCD导入人</b>~^<b>订单日期</b>~^" +
                     "<b>预计PCD</b>~^<b>运算PCD</b>~^<b>塑料件，透镜</b>~^<b>金属件，其他</b>~^<b>散热器</b>~^<b>结构件，灯罩</b>~^<b>包装</b>~^<b>COB</b>~^" +
                     "<b>线路板</b>~^<b>物料号</b>~^<b>需求数量</b>~^<b>缺料量</b>~^<b>分配数量</b>~^<b>类型</b>~^<b>工单号/Loc Status</b>~^<b>工单ID</b>~^<b>印制板，工帽，E型电感</b>~^<b>电阻，场效应管，发光管，三极管</b>~^<b>二极管</b>~^<b>电容，环形电感，保险丝</b>~^"+
                     "<b>芯片板</b>~^<b>物料号</b>~^<b>需求数量</b>~^<b>缺料量</b>~^<b>分配数量</b>~^<b>类型</b>~^<b>工单号/Loc Status</b>~^<b>工单ID</b>~^<b>芯片</b>~^<b>铝基板</b>~^<b>3019物料</b>~^";
        this.ExportExcel(title, dtExcel, false);
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "xianluban")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string nbr = gvlist.DataKeys[index].Values["wo_nbr"].ToString().Trim();
            string lot = gvlist.DataKeys[index].Values["wo_lot"].ToString().Trim();
            //ltlAlert.Text = "window.open('PCD_2101View.aspx?poNbr=" + nbr + "&poLine=" + line +"&rt=" + DateTime.Now.ToString() + "', '_blank');";
            ltlAlert.Text = "$.window('线路板明细',1200,800,'/plan/PCD_2101View.aspx?nbr=" + nbr + "&lot=" + lot + "');";
        }
        if (e.CommandName == "xinpianban")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string nbr = gvlist.DataKeys[index].Values["wo_nbr"].ToString().Trim();
            string lot = gvlist.DataKeys[index].Values["wo_lot"].ToString().Trim();
            //ltlAlert.Text = "window.open('PCD_2101View.aspx? poNbr=" + nbr + "&poLine=" + line +"&rt=" + DateTime.Now.ToString() + "', '_blank');";
            ltlAlert.Text = "$.window('芯片板明细',1200,800,'/plan/PCD_210171View.aspx?nbr=" + nbr + "&lot=" + lot + "');";
        }
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    public void BindGridView()
    {
        try
        {

            DataTable dt = GetData();
            gvlist.DataSource = dt;
            gvlist.DataBind();
        }
        catch (Exception ee)
        { ;}
    }

    private DataTable GetData()
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@poNbr", txtpoNbr.Text.Trim());
        param[1] = new SqlParameter("@Part", txtPart.Text.Trim());
        param[2] = new SqlParameter("@cusCode", txtcode.Text.Trim());
        param[3] = new SqlParameter("@QAD", txtQAD.Text.Trim());
        param[4] = new SqlParameter("@poLine", txtLine.Text.Trim());
        param[5] = new SqlParameter("@type", ddlType.SelectedValue);
        param[6] = new SqlParameter("@status", ddlStatus.SelectedValue);
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_PCD_selectPCD_View", param).Tables[0];
    }

    private DataTable GetData2101()
    {
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_PCD_selectPCD_2101View").Tables[0];
    }

    private DataTable GetData210171()
    {
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_PCD_selectPCD_210171View").Tables[0];
    }
}