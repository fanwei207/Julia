using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data.OleDb;
using WHInfo;
public partial class WH_UNP_RCTdet : BasePage
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
            txt_no.Text = nbr;
            txt_projType.Text = lot;
            BindGridView(nbr, domain, lot);
            BindGridView(nbr, domain, lot);
            gv.Columns[7].Visible = false;
        }
    }
    public void GroupName(int col)
    {
        try
        {
            TableCell oldName = gv.Rows[0].Cells[col];
            for (int i = 1; i < gv.Rows.Count; i++)
            {
                TableCell Name = gv.Rows[i].Cells[col];
                if (oldName.Text == Name.Text)
                {
                    Name.Visible = false;
                    if (oldName.RowSpan == 0)
                    {
                        oldName.RowSpan = 1;
                    }
                    oldName.RowSpan++;
                    oldName.VerticalAlign = VerticalAlign.Middle;
                }
                else
                {
                    oldName = Name;
                }
            }
        }
        catch (Exception)
        {
            btn_update.Enabled = false;
        }
    }
    public void BindGridView(string AppNo, string domain, string lot)
    {
        DataTable dt = WH.GetUnpISSInfo(AppNo);
        foreach (DataRow item in dt.Rows)
        {
            txt_applyer.Text = item["createName"].ToString();
            txt_department.Text = item["wh_departmentName"].ToString();
            txt_site.Text = item["wh_site"].ToString();
            txt_no.Text = item["wh_nbr"].ToString();
            txt_projType.Text = item["wh_type"].ToString();
            txt_date.Text = item["wh_Date"].ToString();
            txt_reason.Text = item["wh_reason"].ToString();
        }

        gv.DataSource = WH.GetUnpISSInfo(AppNo);
        gv.DataBind();
    }
    public static DataTable GetWhView(string nbr, string domain, string lot)
    {
        string strName = "sp_wh_selectWhWoRct";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@nbr", nbr);
        parm[1] = new SqlParameter("@domain", domain);
        parm[2] = new SqlParameter("@lot", lot);
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }
    public static DataTable GetWhViewDet(string nbr, string domain, string lot)
    {
        string strName = "sp_wh_selectWhViewDet";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@nbr", nbr);
        parm[1] = new SqlParameter("@domain", domain);
        parm[2] = new SqlParameter("@lot", lot);
        parm[3] = new SqlParameter("@type", "RCT-WO");
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }

    protected string CheckActualQty()
    {
        string err = "";
        if (gv.Rows.Count <= 0)
        {
            err = "提交明细不能为空";
        }
        for (int i = 0; i <= gv.Rows.Count - 1; i++)
        {
            Label demandQty = (Label)gv.Rows[i].FindControl("lbldemandQty");
            Label actualQty = (Label)gv.Rows[i].FindControl("lblactualQty");
            string Aqty = actualQty.Text;
            string Dqty = demandQty.Text.ToString();
            if (string.IsNullOrEmpty(Aqty) || Convert.ToDecimal(Aqty) == 0)
            {
                int Row1 = i + 1;
                Aqty = "0";
                err += "第" + Row1 + "行实收数量不能为空!\\n";
            }
            if (Convert.ToDecimal(Aqty) > Convert.ToDecimal(Dqty))
            {
                int Row = i + 1;
                err += "第" + Row + "行实收数量不能大于需求数量!\\n";
            }
        }

        return err;
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        string nbr = Request.QueryString["nbr"];
        string err = CheckActualQty();
        if (!string.IsNullOrEmpty(err))
        {
            ltlAlert.Text = "alert('"+err+"')";
            return;
        }
        if (updatetwhView(nbr))
        {
            //ltlAlert.Text = "alert('提交成功!');";
            ltlAlert.Text = "window.close();";
        }
        else
        {
            ltlAlert.Text = "alert('提交失败!');";
        }
    }
    public static bool updatetwhAqty(string nbr, decimal Aqty,int ID)
    {
        try
        {
            string strName = "sp_wh_updatewhUNP_Aqty";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@ID", ID);
            parm[2] = new SqlParameter("@Aqty", Aqty);
            SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool updatetwhView(string nbr)
    {
        try
        {
            string strName = "sp_wh_updatewhUNP_Type";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@nbr", nbr);
            SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        btn_submit.Visible = true;
        btn_back.Visible = true;
        btn_update.Visible = false;
        btn_cancel.Visible = false;
        gv.Visible = false;
        txt_backreason.Visible = true;
        lt_reason.Visible = true;
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txt_backreason.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须要填写理由!');";
            return;
        }
        string domain = Request.QueryString["domain"];
        string nbr = Request.QueryString["nbr"];
        string lot = Request.QueryString["lot"];

        string strName = "sp_wh_cancelwhUNP_App";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@nbr", nbr);
        parm[1] = new SqlParameter("@cancelBy", Convert.ToInt32(Session["uID"]));
        parm[2] = new SqlParameter("@cancelName", Session["uName"].ToString());
        parm[3] = new SqlParameter("@backreason", txt_backreason.Text.Trim());
        try
        {
            SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            ltlAlert.Text = "alert('取消失败!');";
            txt_backreason.Visible = true;
        }
        ltlAlert.Text = "window.close();";
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        btn_submit.Visible = false;
        btn_back.Visible = false;
        btn_update.Visible = true;
        btn_cancel.Visible = true;
        gv.Visible = true;
        txt_backreason.Visible = false;
        lt_reason.Visible = false;
    }
}