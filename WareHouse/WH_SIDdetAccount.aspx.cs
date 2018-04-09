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
using System.Text;
using WHInfo;
public partial class plan_WH_SIDdetAccount : System.Web.UI.Page
{
    WareHouse WH = new WareHouse();
    private static adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request.QueryString["id"];
            BindGridView(id);
            txt_reason.Visible = false;
            lt_reason.Visible = false;
            if (Request.QueryString["process"] != "0")
            {
                btn_retrun.Enabled = false;
            }
        }
    }
    public void BindGridView(string id)
    {
        string process = Request.QueryString["process"];
        if (process.Trim() == "1")
        {
            btn_update.Enabled = false;
        }
        DataTable dt = selectwhViewdet(id);
        foreach (DataRow item in dt.Rows)
        {
            txtshipto.Text = item["SID_shipto"].ToString();
            txtdomain.Text = item["SID_domain"].ToString();
            txtPK.Text = item["SID_PK"].ToString();
            txtnbr.Text = item["SID_nbr"].ToString();
        }
        gv.DataSource = selectwhView(id);
        gv.DataBind();
    }
    public static DataTable selectwhView(string id)
    {
        string strName = "sp_wh_selectwhSIDdet";
        SqlParameter[] parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@id", id);
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }
    public static DataTable cimloadSID(string id)
    {
        string strName = "sp_Wh_cimloadSID";
        SqlParameter[] parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@id", id);
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }
    public static DataTable selectwhViewdet(string id)
    {
        string strName = "sp_wh_selectwhViewdet";
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@nbr", "");
        parm[1] = new SqlParameter("@domain", "");
        parm[2] = new SqlParameter("@lot", "");
        parm[3] = new SqlParameter("@type", "ISS-SO");
        parm[4] = new SqlParameter("@id", id);
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        //string root = @"d:\\"; 
        string root = Server.MapPath("..\\Excel\\");
        string path_szx = root + txtdomain.Text + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".sod";
        try
        {
            DataTable dt = cimloadSID(id);
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
            if (updatetwhView(id))
            {
                //ltlAlert.Text = "alert('提交成功!');";
                //ltlAlert.Text = "window.close();";
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
    public static bool updatetwhView(string id)
    {
        try
        {
            string strName = "sp_wh_updatewhSIDAccount";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);
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
        string type = "ISS-SO";
        string id = Request.QueryString["id"];
        IErr = WH.ReturnAccounts(Convert.ToString(Session["uID"]), domain, nbr, lot, type, id, remarks);
        if (IErr < 0)
        {
            ltlAlert.Text = "alert('退回失败！');";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('退回成功！');";
            BindGridView(id);
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