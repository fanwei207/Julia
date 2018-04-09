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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using QCProgress;
using CommClass;
public partial class plan_wo_actualRelease : BasePage
{
    private wo.Wo_ActualRelease helper = new wo.Wo_ActualRelease();
    static string strConn = ConfigurationManager.AppSettings["SqlConn.BarCodeSys"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime now = DateTime.Now;
            DateTime monthFirstDate = new DateTime(now.Year, now.Month, 1);
            //DateTime monthLastDate = monthFirstDate.AddMonths(1).AddDays(-1);  
            string date = monthFirstDate.ToShortDateString();
            date = date.Replace("/", "-");
            txtActDateFrom.Text = date;
            BindData();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
       

        string woNbr = txtNbr.Text.Trim();
        string part = txtQAD.Text.Trim();
        string relDateFrom = "";
        string relDateTo = "";
        string actDateFrom = txtActDateFrom.Text.Trim();
        string actDateTo = txtActDateTo.Text.Trim();
        //string domain = ddlDomain.SelectedItem.Text;
        string domain = "";
        if(Session["PlantCode"] != null)
        {
              domain =Session["PlantCode"].ToString();
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
        gvlist.DataSource = GetWoActRelListEX(woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain, txtline.Text.Trim(), txtctr.Text.Trim(),ddlGet.SelectedValue.ToString(),ddlststus.SelectedValue.ToString().Trim(),ddlonline.SelectedValue.ToString().Trim());
        gvlist.DataBind();
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
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
        if (e.CommandName == "BOM")
        {
            //_mstr = ((LinkButton)e.CommandSource).Text;
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            this.Redirect("/plan/qad_bomviewdoc.aspx?cmd=newwo&part=" + gvlist.DataKeys[index].Values["wo_part"].ToString());
        }
        if (e.CommandName == "part")
        {
            //_mstr = ((LinkButton)e.CommandSource).Text;
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            string _parentID = gvlist.DataKeys[index].Values["wo_id"].ToString();
            //string Uid = gvMessage.DataKeys[index].Values["fst_createBy"].ToString();
            //string closed = gvMessage.DataKeys[index].Values["fst_IsClosed"].ToString().Trim();
            this.Redirect("wo_actualPart.aspx?id=" + _parentID + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        if (e.CommandName == "report")
        {
            
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            string _parentID = gvlist.DataKeys[index].Values["wo_id"].ToString();
            string _nbr = gvlist.DataKeys[index].Values["wo_nbr"].ToString();
            string _Id = gvlist.DataKeys[index].Values["wo_lot"].ToString();
            string _domain = gvlist.DataKeys[index].Values["wo_domain"].ToString();
            string _line = gvlist.DataKeys[index].Values["wo_line"].ToString();
            this.Redirect("note_WorkLineReport.aspx?wonbr=" + _nbr + "&wolot=" + _Id + "&wodomain=" + _domain + "&woline=" + _line + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        if (e.CommandName == "get")
        {
            
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            string _parentID = gvlist.DataKeys[index].Values["wo_id"].ToString();
            updateWoActRelEXget(_parentID);
            BindData();
        }
    }
    public DataTable GetWoActRelListEX(string woNbr, string part, string relDateFrom, string relDateTo, string actDateFrom, string actDateTo, string domain, string line, string ctr, string get, string stuts, string online)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[12];
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
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wo_selectWoActRelEX", param).Tables[0];
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

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wo_updateWoActRelEXget", param).Tables[0];
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
        }
    }
}