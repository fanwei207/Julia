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
public partial class WH_UNP_ISS : BasePage
{
    WareHouse WH = new WareHouse();
    private static adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string nbr = Request.QueryString["nbr"];
            txt_no.Text = nbr;
            BindGridView(nbr);
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
            btn_submit.Enabled = false;
        }
    }
    public void BindGridView(string AppNo)
    {
        DataTable dt = WH.GetUnpMstrInfo(AppNo);
        foreach (DataRow item in dt.Rows)
        {
            txt_applyer.Text = item["createName"].ToString();
            txt_department.Text = item["wh_departmentName"].ToString();
            txt_site.Text = item["wh_site"].ToString();
            txt_no.Text = item["wh_nbr"].ToString();
            txt_projType.Text = item["wh_type"].ToString();
            txt_date.Text = item["wh_Date"].ToString();
            if (item["wh_process"].ToString() == "1")
            {
                btn_submit.Enabled = false;
            }
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
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        int flag = WH.SubmitApp(txt_no.Text.Trim(), Session["plantcode"].ToString(), "UNP-ISS");
        if (flag < 0)
        {
            this.Alert("提交失败，请联系管理员!");
        }
        else
        {
            btn_submit.Enabled = false;
            this.Alert("提交成功!");
            ltlAlert.Text = "window.close();";
        }
    }

}