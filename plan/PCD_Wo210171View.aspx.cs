﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;


public partial class plan_PCD_Wo210171View : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
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
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@nbr", txtWoNbr.Text.Trim());
        param[1] = new SqlParameter("@lot", txtWoLot.Text.Trim());
        param[2] = new SqlParameter("@part", txtPart.Text.Trim());
        param[3] = new SqlParameter("@status", ddlStatus.SelectedValue);
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_PCD_selectPCD_Wo210171View", param).Tables[0];
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        string EXTitle = "<b>工单号</b>~^<b>ID</b>~^<b>QAD号</b>~^<b>域</b>~^<b>地点</b>~^<b>截止日期</b>~^<b>PCD</b>~^<b>芯片</b>~^<b>铝基板</b>~^<b>3019物料</b>~^";
        this.ExportExcel(EXTitle, dt, false);
    }
}