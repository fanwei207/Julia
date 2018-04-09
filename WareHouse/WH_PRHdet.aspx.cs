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
public partial class plan_WH_PRHdet : BasePage
{
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
            GroupName(0);
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
    public void BindGridView(string nbr, string domain, string lot)
    {
        DataTable dt = selectWhViewDet(nbr, domain, lot);
        foreach (DataRow item in dt.Rows)
        {
            txtshipto.Text = item["wh_shipto"].ToString();
            txtname.Text = item["ad_name"].ToString();
            txtvent.Text = item["wh_vend"].ToString();
        }
        gv.DataSource = selectWhView(nbr, domain, lot);
        gv.DataBind();
    }
    public static DataTable selectWhView(string nbr, string domain, string lot)
    {
        string strName = "sp_wh_selectWhPrhDet";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@nbr", nbr);
        parm[1] = new SqlParameter("@domain", domain);
        parm[2] = new SqlParameter("@lot", lot);
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }

    public static DataTable selectWhViewDet(string nbr, string domain, string lot)
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

    protected string CheckActualQty()
    {
        string err = "";
        if (gv.Rows.Count <= 0)
        {
            err = "提交明细不能为空";
        }
        for (int i = 0; i <= gv.Rows.Count - 1; i++)
        {
            string Aqty = gv.Rows[i].Cells[9].Text.ToString().Replace("&nbsp;", "");
            string Dqty = gv.Rows[i].Cells[6].Text.ToString();
            if (string.IsNullOrEmpty(Aqty) || Convert.ToDecimal(Aqty) == 0)
            {
                int Row1 = i + 1;
                Aqty = "0";
                err += "第" + Row1 + "行实际数量不能为空!\\n";
            }
            if (Convert.ToDecimal(Aqty) > Convert.ToDecimal(Dqty))
            {
                int Row = i + 1;
                err += "第" + Row + "行送货数量不能大于送货数量!\\n";
            }
        }

        return err;
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        string err = CheckActualQty();
        if (!string.IsNullOrEmpty(err))
        {
            ltlAlert.Text = "alert('" + err + "')";
            return;
        }
        string domain = Request.QueryString["domain"];
        string nbr = Request.QueryString["nbr"];
        string lot = Request.QueryString["lot"];

        if (updatetwhView(nbr, domain, lot))
        {
            //ltlAlert.Text = "alert('提交成功!');";
            ltlAlert.Text = "window.close();";
        }
        else
        {
            ltlAlert.Text = "alert('提交失败!');";
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[10].Text.Trim() == "0")
            {
                btn_update.Enabled = false;
            }
        }
    }
    public static bool updatetwhView(string nbr, string domain, string lot)
    {
        try
        {
            string strName = "sp_wh_updatewhPRH";
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
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        if (txt_reason.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须要填写理由!');";
            return;
        } 
        string domain = Request.QueryString["domain"];
        string nbr = Request.QueryString["nbr"];
        string lot = Request.QueryString["lot"];

        string strName = "sp_wh_cancelwhWO_PRH";
        SqlParameter[] parm = new SqlParameter[6];
        parm[0] = new SqlParameter("@nbr", nbr);
        parm[1] = new SqlParameter("@domain", domain);
        parm[2] = new SqlParameter("@lot", lot);
        parm[3] = new SqlParameter("@cancelBy", Convert.ToInt32(Session["uID"]));
        parm[4] = new SqlParameter("@cancelName", Session["uName"].ToString());
        parm[5] = new SqlParameter("@reason", txt_reason.Text.Trim());
        try
        {
            SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            ltlAlert.Text = "alert('取消失败!');";
        }
        ltlAlert.Text = "window.close();";
    }
}