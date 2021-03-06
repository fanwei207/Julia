﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using WHInfo;
public partial class plan_WH_PRHdetAccount : System.Web.UI.Page
{
    WareHouse WH = new WareHouse();

    private static adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string domain = Request.QueryString["domain"];
            string nbr = Request.QueryString["nbr"];
            string lot = Request.QueryString["lot"];
            txtdomain.Text = domain;
            txtnbr.Text = nbr;
            txtlot.Text = lot;
            BindGridView(nbr, domain, lot);
            txt_reason.Visible = false;
            lt_reason.Visible = false;
            if (Request.QueryString["process"] != "0")
            {
                btn_retrun.Enabled = false;
            }
        }
    }
    public void BindGridView(string nbr, string domain, string lot)
    {
        string process = Request.QueryString["process"];
        if (process.Trim() == "1" || process.Trim() == "2")
        {
            btn_update.Enabled = false;
        }
        DataTable dt = selectwhViewdet(nbr, domain, lot);
        foreach (DataRow item in dt.Rows)
        {
            txtshipto.Text = item["wh_shipto"].ToString();
            txtname.Text = item["ad_name"].ToString();
            txtvent.Text = item["wh_vend"].ToString();
        }
        gv.DataSource = selectwhView(nbr, domain, lot);
        gv.DataBind();
    }
    public static DataTable selectwhView(string nbr, string domain, string lot)
    {
        string strName = "sp_wh_selectwhPRHdet";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@nbr", nbr);
        parm[1] = new SqlParameter("@domain", domain);
        parm[2] = new SqlParameter("@lot", lot);
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }
    public static DataTable selectwhViewdet(string nbr, string domain, string lot)
    {
        string strName = "sp_wh_selectwhViewdet";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@nbr", nbr);
        parm[1] = new SqlParameter("@domain", domain);
        parm[2] = new SqlParameter("@lot", lot);
        parm[3] = new SqlParameter("@type", "RCT-PO");
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }
    public static DataTable cimloadprh(string nbr, string domain, string lot)
    {
        string strName = "sp_Wh_cimloadPRH";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@nbr", nbr);
        parm[1] = new SqlParameter("@domain", domain);
        parm[2] = new SqlParameter("@lot", lot);
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        string domain = Request.QueryString["domain"];
        string nbr = Request.QueryString["nbr"];
        string lot = Request.QueryString["lot"];
        //string root = @"d:\\";
        string root = Server.MapPath("..\\Excel\\");
        string path_szx = root + domain + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".pod";
        try
        {
            DataTable dt = cimloadprh(nbr, domain, lot);
            StreamWriter writer_szx = new StreamWriter(path_szx, false, Encoding.GetEncoding("gb2312"));
            string str = "";
            str += "@@BEGIN" + "\r\n";
            foreach (DataRow row in dt.Rows)
            {
                str += row["ps_nbr"].ToString() + "\r\n";
                str += row["ps_lot"].ToString() + "\r\n";
                str += row["ps_part"].ToString() + "\r\n";
                str += row["ps_m"].ToString() + "\r\n";
                str += row["ps_qty"].ToString() + "\r\n";
                str += row["ps_site"].ToString() + "\r\n";
                str += row["ps_loc"].ToString() + "\r\n";
                str += row["ps_x"].ToString() + "\r\n";
                str += row["ps_remark"].ToString() + "\r\n";
                str += row["ps_end"].ToString() + "\r\n";
            }
            str += "@@FINISH";
            writer_szx.WriteLine(str);
            writer_szx.Flush();
            writer_szx.Close();
            ltlAlert.Text = "alert('成功导出到CIM文件！');";

            if (updatetwhView(nbr, domain, lot))
            {
                //ltlAlert.Text = "alert('提交成功!');";
                // ltlAlert.Text = "window.close();";
                btn_update.Enabled = false;
            }
            else
            {
                ltlAlert.Text = "alert('提交失败!');";
            }

        }
        catch
        {
            ltlAlert.Text = "alert('导出文件失败！');";
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[9].Text.Trim() == "0")
            {
                btn_update.Enabled = false;
            }
        }
    }
    public static bool updatetwhView(string nbr, string domain, string lot)
    {
        try
        {
            string strName = "sp_wh_updatewhPRHAccount";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@domain", domain);
            parm[2] = new SqlParameter("@lot", lot);
            SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    protected void btn_retrun_Click(object sender, EventArgs e)
    {
        btn_submit.Visible = true;
        btn_back.Visible = true;
        btn_update.Visible = false;
        btn_retrun.Visible = false;
        gv.Visible = false;
        txt_reason.Visible = true;
        lt_reason.Visible = true;
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_reason.Text.Trim()))
        {
            ltlAlert.Text = "alert('退回原因不能为空！');";
            return;
        }
        Int32 IErr = 0;
        string domain = Request.QueryString["domain"];
        string nbr = Request.QueryString["nbr"];
        string lot = Request.QueryString["lot"];
        string remarks = txt_reason.Text.Trim();
        string type = "RCT-PO";
        IErr = WH.ReturnAccounts(Convert.ToString(Session["uID"]), domain, nbr, lot, type, "", remarks);
        if (IErr < 0)
        {
            ltlAlert.Text = "alert('退回失败！');";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('退回成功！');";
            btn_update.Enabled = false;
            btn_retrun.Enabled = false;
            BindGridView(nbr, domain, lot);
        }
        ltlAlert.Text = "window.close();";
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        btn_submit.Visible = false;
        btn_back.Visible = false;
        btn_update.Visible = true;
        btn_retrun.Visible = true;
        gv.Visible = true;
        txt_reason.Visible = false;
        lt_reason.Visible = false;
    }
}