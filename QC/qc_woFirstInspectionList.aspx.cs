using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using adamFuncs;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CommClass;
public partial class QC_qc_woFirstInspectionList : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtActDateFrom.Text = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
            txtActDateTo.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            BindLine();
            //BindGridView();


        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    protected void BindLine()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@plantCode", Convert.ToString(Session["PlantCode"]));
            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "BarCodeSys.dbo.sp_note_selectLine", param);

            dropLine.DataSource = ds;
            dropLine.DataBind();

            dropLine.Items.Insert(0, new ListItem("--", ""));

            
        }
        catch { }
    }
    protected override void BindGridView()
    {
        string woNbr = txtNbr.Text.Trim();
        string part = txtQAD.Text.Trim();
        string relDateFrom = "";
        string relDateTo = "";
        string actDateFrom = txtActDateFrom.Text.Trim();
        string actDateTo = txtActDateTo.Text.Trim();
        //string domain = ddlDomain.SelectedItem.Text;
        string domain = "";
        if (Session["PlantCode"] != null)
        {
            domain = Session["PlantCode"].ToString();
            if (domain == "1")
            { domain = "SZX"; }
            else if (domain == "2")
            { domain = "ZQL"; }
            else if (domain == "5")
            { domain = "YQL"; }
            else if (domain == "8")
            { domain = "HQL"; }
        }
        else
        {
            domain = "--";
        }
        gvlist.DataSource = GetWoActRelListEX("BarCodeSys.dbo.sp_QC_FirstInspectionList", adam.dsnx(), woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain, dropLine.SelectedValue, txtctr.Text.Trim(), ddlGet.SelectedValue.ToString()
         , ddlststus.SelectedValue.ToString().Trim(), Convert.ToInt32(ddlonline.SelectedValue.ToString().Trim()), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["uID"]), ddlHasTracking.SelectedValue ,ddlXunJian.SelectedValue);
        //gvlist.DataSource = GetWoActRelListEX("sp_wo_selectWoActRelEX", strConn, woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain, dropLine.SelectedValue, txtctr.Text.Trim(), ddlGet.SelectedValue.ToString()
        //   , ddlststus.SelectedValue.ToString().Trim(), Convert.ToInt32(ddlonline.SelectedValue.ToString().Trim()), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["uID"]));
        //switch (ddlXunJian.SelectedValue)
        //{
        //    case "0"://所有工单
        //        gvlist.DataSource = GetWoActRelListEX("BarCodeSys.dbo.sp_QC_FirstInspectionList", adam.dsnx(), woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain, dropLine.SelectedValue, txtctr.Text.Trim(), ddlGet.SelectedValue.ToString()
        //    , ddlststus.SelectedValue.ToString().Trim(), Convert.ToInt32(ddlonline.SelectedValue.ToString().Trim()), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["uID"]), ddlHasTracking.SelectedValue);
        //        break;
        //    //case "1"://未巡检
        //    //    gvlist.DataSource = GetWoActRelListEX("sp_xun_QC_GetReportUndo1", adam.dsnx(), woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain, dropLine.SelectedValue, txtctr.Text.Trim(), ddlGet.SelectedValue.ToString()
        //    //, ddlststus.SelectedValue.ToString().Trim(), Convert.ToInt32(ddlonline.SelectedValue.ToString().Trim()), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["uID"]), ddlHasTracking.SelectedValue);
        //    //    break;
        //    //case "2"://进行中
        //    //    gvlist.DataSource = GetWoActRelListEX("sp_xun_QC_GetReport1", adam.dsnx(), woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain, dropLine.SelectedValue, txtctr.Text.Trim(), ddlGet.SelectedValue.ToString()
        //    //, ddlststus.SelectedValue.ToString().Trim(), Convert.ToInt32(ddlonline.SelectedValue.ToString().Trim()), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["uID"]), ddlHasTracking.SelectedValue);
        //    //    break;
        //}



        gvlist.DataBind();
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

        //string woNbr = txtNbr.Text.Trim();
        //string part = txtQAD.Text.Trim();
        //string relDateFrom = txtDateFrom.Text.Trim();
        //string relDateTo = txtDateTo.Text.Trim();
        //string actDateFrom = txtActDateFrom.Text.Trim();
        //string actDateTo = txtActDateTo.Text.Trim();
        //string domain = ddlDomain.SelectedItem.Text;
        //DataTable dt = helper.GetWoActRelList(woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain);
        //string title = "100^<b>加工单</b>~^100^<b>ID</b>~^120^<b>QAD</b>~^100^<b>QAD下达日期</b>~^100^<b>计划日期</b>~^100^<b>评审日期</b>~^100^<b>上线日期</b>~^100^<b>地点</b>~^100^<b>成本中心</b>~^100^<b>工厂</b>~^";
        //ExportExcel(title, dt, false);
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {

         if (e.CommandName == "detail")
        {
            //Session["xunjian"] = ddlXunJian.SelectedValue;
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            string _domian = gvlist.DataKeys[index].Values["wo_domain"].ToString();
            string _parentID = gvlist.DataKeys[index].Values["wo_id"].ToString();
            string _nbr = gvlist.DataKeys[index].Values["wo_nbr"].ToString();
            string _Id = gvlist.DataKeys[index].Values["wo_lot"].ToString();
            string _routing = string.Empty;
            DataTable dt = GetWoDetailInfo(_nbr, _Id);

            if (dt != null && dt.Rows.Count > 0)
            {
                _routing = dt.Rows[0]["wo_routing"].ToString();

            }
            else
            {
                ltlAlert.Text = "alert('工艺流程为空，请联系相关人员！');";
            }
            this.Redirect("qc_firstInspection.aspx?nbr=" + _nbr + "&lot=" + _Id + "&routing=" + _routing + "&domain=" + _domian);
        }
    }

    public DataTable GetWoDetailInfo(string nbr, string lot)
    {
        try
        {
            string sp = "BarCodeSys.dbo.sp_wo_selectWoDetail";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@wo_nbr", nbr);
            parms[1] = new SqlParameter("@wo_lot", lot);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sp, parms).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="conn"></param>
    /// <param name="woNbr"></param>
    /// <param name="part"></param>
    /// <param name="relDateFrom"></param>
    /// <param name="relDateTo"></param>
    /// <param name="actDateFrom"></param>
    /// <param name="actDateTo"></param>
    /// <param name="domain"></param>
    /// <param name="line"></param>
    /// <param name="ctr"></param>
    /// <param name="get"></param>
    /// <param name="stuts"></param>
    /// <param name="online"></param>
    /// <param name="plant"></param>
    /// <param name="uId"></param>
    /// <param name="tracking"></param>
    /// <param name="xunjian"> 0 全部
    ///                         1 线长未确认
    ///                         2 巡检员未确认
    ///                         3 巡检员已确认
    ///                         </param>
    /// <returns></returns>
    public DataTable GetWoActRelListEX(string sp, string conn, string woNbr, string part, string relDateFrom, string relDateTo, string actDateFrom, string actDateTo, string domain, string line, string ctr, string get, string stuts, int online, int plant, int uId, string tracking , string xunjian)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@woNbr", woNbr);
            param[1] = new SqlParameter("@part", part);
            param[2] = new SqlParameter("@relDateFrom", relDateFrom);
            param[3] = new SqlParameter("@relDateTo", relDateTo);
            param[4] = new SqlParameter("@actDateFrom", actDateFrom);
            param[5] = new SqlParameter("@actDateTo", actDateTo);
            param[6] = new SqlParameter("@domain", domain);
            param[7] = new SqlParameter("@line", line);
            param[8] = new SqlParameter("@ctr", ctr);
            param[9] = new SqlParameter("@get", get);
            param[10] = new SqlParameter("@stuts", stuts);
            param[11] = new SqlParameter("@online", online);
            param[12] = new SqlParameter("@hasTracking", tracking);
            param[13] = new SqlParameter("@plantid", plant);
            param[14] = new SqlParameter("@uID", uId);
            param[15] = new SqlParameter("@xunjian", xunjian);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, sp, param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    public DataTable updateWoActRelEXget(string id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "BarCodeSys.dbo.sp_wo_updateWoActRelEXget", param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Text = e.Row.Cells[9].Text.Replace(";", "<br />");
            if (gvlist.DataKeys[e.Row.RowIndex].Values["wo_get"].ToString() != "领料")
            {
                e.Row.Cells[14].Text = gvlist.DataKeys[e.Row.RowIndex].Values["wo_get"].ToString();
            }

            if (gvlist.DataKeys[e.Row.RowIndex].Values["first_status"].ToString().Equals("线长未确认"))
            {
                ((Button)e.Row.FindControl("Detail")).Visible = false;

            }
            
        }
    }
    //add by liuHb in 2015-06-25
    protected void ddlXunJian_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindGridView();
    }

}