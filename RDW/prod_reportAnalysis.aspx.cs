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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;
using RD_WorkFlow;

public partial class RDW_prod_reportAnalysis : System.Web.UI.Page
{
    RDW rdw = new RDW();
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["prodStatus"].ToString() != "0" && Request["prodStatus"].ToString() != "")
            {
                Button1.Enabled = false;
            }
            if (Request["typeStatus"].ToString() == "5")
            {
                Button1.Enabled = false;
            }            
            if (Request["username"].ToString().IndexOf(Session["uName"].ToString()) == -1)
            {
                Button1.Enabled = false;
            }
            Bind();
        }
    }
    private void Bind()
    {
        DataTable dt = getReportAnalysis();
        gv.DataSource = dt;
        gv.DataBind();

        gv.Columns[3].Visible = false;
        gv.Columns[4].Visible = false;
    }
    private DataTable getReportAnalysis()
    {
        string sql = "select  fs.prod_flowname,isnull(fr.prod_flowValueYes,0) as prod_flowValueYes,isnull(fr.prod_flowValueNo,0) as prod_flowValueNo " +
                     "  from  prod_FlowStandard  fs " +
                     "  left join  prod_FlowRecord fr   on  fr.prod_flowname = fs.prod_flowname and fr.prod_procNo = '" + Request["no"].ToString() + "'";
        return SqlHelper.ExecuteDataset(strConn,CommandType.Text,sql).Tables[0];
    }
    //protected void chkYes_CheckedChanged(object sender, EventArgs e)
    //{

    //    CheckBox chkYes = (CheckBox)FindControl("chkYes");
    //    CheckBox chkNo = (CheckBox)FindControl("chkNo");
    //    if (chkYes.Checked)
    //    {
    //        chkNo.Checked = false;
    //    }
    //}
    //protected void chkNo_CheckedChanged(object sender, EventArgs e)
    //{

    //    CheckBox chkYes = (CheckBox)FindControl("chkYes");
    //    CheckBox chkNo = (CheckBox)FindControl("chkNo");
    //    if (chkNo.Checked)
    //    {
    //        chkYes.Checked = false;
    //    }
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        int i =  0;
        int score = 0;
        int count = 0;
        bool magStatus = false; //check有变化
        bool status = false;
        bool isChecked = false;
        string massage = string.Empty;
        string msg = string.Empty;
        foreach (GridViewRow rw in gv.Rows)
        {
            string flowYstatus = string.Empty;           
            string flowNstatus = string.Empty;
            DataTable dt = selectFlowRecord(Request["no"].ToString(), rw.Cells[0].Text);
            foreach (DataRow rw1 in dt.Rows)
            {
                flowYstatus = rw1["prod_flowValueYes"].ToString();
                flowNstatus = rw1["prod_flowValueNo"].ToString();
            }
            status = false;
            CheckBox chkYes = (CheckBox)rw.FindControl("chkYes");
            CheckBox chkNo = (CheckBox)rw.FindControl("chkNo");
            //CheckBox chkEnableYes = (CheckBox)rw.FindControl("chkEnableYes");
            //CheckBox chkEnableNo = (CheckBox)rw.FindControl("chkEnableNo");
            if (chkYes.Checked)
            {
                status = true;//表示已经有评分标准
                //if (!chkEnableYes.Checked)
                if (flowYstatus.ToLower() == "false" || flowYstatus.ToLower() == "")
                {
                    isChecked = true;
                    magStatus = true; //check有变化
                    //更新或新增
                    if (!saveOrUpdateRecord(Request["no"].ToString(), "Y", 1, gv.Rows[i].Cells[0].Text))
                    {
                        ltlAlert.Text = "alert('提交失败，请联系管理员！')";
                        return;
                    }
                }
                count = count + 1; //总数量+1
                score = score + 1; //通过数量+1
            }
            else
            {
                if (flowYstatus.ToLower() == "true")
                {
                    isChecked = true;
                }
            }
            if (chkNo.Checked)
            {
                status = true;
                //if (!chkEnableNo.Checked)
                if (flowNstatus.ToLower() == "false" || flowNstatus.ToLower() == "")
                {
                    isChecked = true;
                    magStatus = true; //check有变化
                    if (!saveOrUpdateRecord(Request["no"].ToString(), "N", 1, gv.Rows[i].Cells[0].Text))
                    {
                        ltlAlert.Text = "alert('提交失败，请联系管理员！')";
                        return;
                    }
                }
                count = count + 1;
            }
            else
            {
                if (flowNstatus.ToLower() == "true")
                {
                    isChecked = true;
                }
            }
            if (!status)
            {
                magStatus = true;
                //删除记录
                string sql = "delete from prod_flowRecord where prod_procNo = '" + Request["no"].ToString() + "' and prod_flowName = N'" + gv.Rows[i].Cells[0].Text + "'";
                SqlHelper.ExecuteNonQuery(strConn,CommandType.Text,sql);
            }
            i++;
        }
        if (count > 0)
        {
            isChecked = true;
        }
        if (!isChecked)
        {
            ltlAlert.Text = "alert('没有进行试流标准评分 ，无须保存！');";
            return;            
        }
        if (magStatus)
        {
            double fs = Convert.ToDouble(score) / Convert.ToDouble(count);
            if (count > 0)
            {
                Label1.Text = "评分为:" + score.ToString() + "/" + count.ToString() + "=" + fs.ToString("p");
                massage = Session["eName"].ToString() + " 计算试流标准评分，结果：" + score.ToString() + "/" + count.ToString() + "=" + fs.ToString("p") + " 跟踪单号: " + Request["no"].ToString();
                msg = Label1.Text;            
            }
            else
            {
                Label1.Text = "取消所有试流标准评分";
                massage = Session["eName"].ToString() + " 取消所有试流标准评分";
                msg = Label1.Text;            
            }
            if (!saveMassage(Request["no"].ToString(), Request["code"].ToString(), Request["name"].ToString(), msg, "", ""))
            {
                ltlAlert.Text = "alert('消息保存失败 ，请联系管理员！');";
                return;
            } 
            if (!insertMassage(Request.QueryString["mid"], Request.QueryString["did"], massage, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('消息保存失败，请联系管理员！');";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('试流标准评分没有变化 ，无须保存！');";
            return;
        }
        Bind();
    }

    private DataTable selectFlowRecord(string no, string name)
    {
        string str = "sp_prod_selectFlowRecord";

        SqlParameter []param = new SqlParameter[2];
        param[0] = new SqlParameter("@no",no);
        param[1] = new SqlParameter("@name",name);

        return SqlHelper.ExecuteDataset(strConn,CommandType.StoredProcedure,str,param).Tables[0];
    }

    private bool saveOrUpdateRecord(string no, string type, int val, string name)
    {
        string str = "sp_prod_saveOrUpdateRecord";

        SqlParameter []param = new SqlParameter[7];
        param[0] = new SqlParameter("@no",no);
        param[1] = new SqlParameter("@type",type);
        param[2] = new SqlParameter("@val", val);
        param[3] = new SqlParameter("@name",name);
        param[4] = new SqlParameter("@uID",Session["uID"].ToString());
        param[5] = new SqlParameter("@uName", Session["uName"].ToString());
        param[6] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[6].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn,CommandType.StoredProcedure,str,param);
        return Convert.ToBoolean(param[6].Value);
    }

    private bool insertMassage(string mid, string did, string massage, string uID, string uName)
    {
        try
        {
            string str = "sp_prod_insertProdMassage";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@mid", mid);
            param[1] = new SqlParameter("@did", did);
            param[2] = new SqlParameter("@massage", massage);
            param[3] = new SqlParameter("@uID", uID);
            param[4] = new SqlParameter("@uName", uName);
            param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
            return Convert.ToBoolean(param[5].Value);
        }
        catch
        {
            return false;
        }
    }
    private bool saveMassage(string no, string code, string proj, string massage, string fname, string fpath)
    {
        string str = "sp_prod_saveMassage";
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@code", code);
        param[2] = new SqlParameter("@proj", proj);
        param[3] = new SqlParameter("@massage", massage);
        param[4] = new SqlParameter("@dept", Request["dept"].ToString());
        param[5] = new SqlParameter("@mid", Request["mid"].ToString());
        param[6] = new SqlParameter("@did", Request["did"].ToString());
        param[7] = new SqlParameter("@uID", Session["uID"].ToString());
        param[8] = new SqlParameter("@uName", Session["uName"].ToString());
        param[9] = new SqlParameter("@fname", fname);
        param[10] = new SqlParameter("@fpath", fpath);
        param[11] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[11].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
        return Convert.ToBoolean(param[11].Value);
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //CheckBox chkYes = (CheckBox)e.Row.FindControl("chkYes");
            //CheckBox chkNo = (CheckBox)e.Row.FindControl("chkNo");
            //CheckBox chkEnableYes = (CheckBox)e.Row.FindControl("chkEnableYes");
            //CheckBox chkEnableNo = (CheckBox)e.Row.FindControl("chkEnableNo");
            //if (chkYes.Checked)
            //{
            //    chkEnableYes.Checked = true;
            //}
            //if (chkNo.Checked)
            //{
            //    chkEnableNo.Checked = true;
            //}
        }
    }
}