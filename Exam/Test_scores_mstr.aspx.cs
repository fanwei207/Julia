using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;

public partial class Test_Test_scores_mstr : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getType();
            BindGridView();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    protected override void BindGridView()
    {
        try
        {
            string strName = "sp_test_selectscoresmstr";
            SqlParameter[] param = new SqlParameter[6];


            param[0] = new SqlParameter("@uname", txt_title.Text);
            if (ddlStatu.SelectedValue.ToString() != "0")
            {
                param[1] = new SqlParameter("@examid", ddlStatu.SelectedValue.ToString());
            }
            param[2] = new SqlParameter("@stratdate", txtstartdate.Text);
            param[3] = new SqlParameter("@enddate", txtenddate.Text);
            param[4] = new SqlParameter("@plant", dropPlant.SelectedValue.ToString());
            param[5] = new SqlParameter("@his", ckb_his.Checked);
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    public void getType()
    {
        try
        {
            string strName = "sp_test_selectexam";

            ddlStatu.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName).Tables[0];
            ddlStatu.DataBind();
            ddlStatu.Items.Insert(0, new ListItem("--All--", "0"));
            ddlStatu.SelectedIndex = 0;

        }
        catch (Exception ex)
        {

        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string ques_id = gv.DataKeys[index].Values["mark_id"].ToString();



            this.Redirect("test_scores_det.aspx?mark_id=" + ques_id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnimport_Click(object sender, EventArgs e)
    {
        string strName = "sp_test_selectscoresmstr";
        SqlParameter[] param = new SqlParameter[5];


        param[0] = new SqlParameter("@uname", txt_title.Text);
        if (ddlStatu.SelectedValue.ToString() != "0")
        {
            param[1] = new SqlParameter("@examid", ddlStatu.SelectedValue.ToString());
        }
        param[2] = new SqlParameter("@stratdate", txtstartdate.Text);
        param[3] = new SqlParameter("@enddate", txtenddate.Text);
        param[4] = new SqlParameter("@plant", dropPlant.SelectedValue.ToString());
        DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
        string title = "100^<b>试卷</b>~^100^<b>考试人</b>~^100^<b>工号</b>~^100^<b>部门</b>~^100^<b>考试时间</b>~^100^<b>得分</b>~^100^<b>总分</b>~^100^<b>用时（分钟）</b>~^";
        ExportExcel(title, ds.Tables[0], false);
    }
}