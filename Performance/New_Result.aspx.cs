using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;

public partial class Performance_New_Result : System.Web.UI.Page
{
    static string ids;
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }

    protected DataTable GetPerformanceResult()
    {
        try
        {
            string strSql = "sp_perf_selectResult";

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void GetPerformanceResult(string id)
    {
        try
        {
            string strSql = "sp_perf_selectResult";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@id", id);
            SqlDataReader reader =SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, parms);
            while (reader.Read())
            {
                txbType.Text = reader["perfr_type"].ToString();
                txbRemark.Text = reader["perfr_remark"].ToString();
            }
        }
        catch
        {
           
        }
    }
    protected DataTable SelectPerformanceResult(string type)
    {
        try
        {
            string strSql = "sp_perf_selectResult";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@type", type);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parms).Tables[0];
            
        }
        catch
        {
            return null;
        }
    }

    protected int UpdatePerformanceResult(string id,string type,string remark)
    {
        try
        {
            string strSql = "sp_perf_updatePerformanceResult";
            SqlParameter[] parms = new SqlParameter[6];
            parms[0] = new SqlParameter("@id", id);
            parms[1] = new SqlParameter("@type", type);
            parms[2] = new SqlParameter("@remark", remark);
            parms[3] = new SqlParameter("@modifyby", Session["uID"].ToString());
            parms[4] = new SqlParameter("@modifyname", Session["uName"].ToString());
            parms[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            parms[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parms);
            return Convert.ToInt32(parms[5].Value);
        }
        catch
        {
            return -1;
        }
    }

    protected int InsertPerformanceResult( string type, string remark)
    {
        try
        {
            string strSql = "sp_perf_insertPerformanceResult";
            SqlParameter[] parms = new SqlParameter[5];
           
            parms[0] = new SqlParameter("@type", type);
            parms[1] = new SqlParameter("@remark", remark);
            parms[2] = new SqlParameter("@createby", Session["uID"].ToString());
            parms[3] = new SqlParameter("@createname", Session["uName"].ToString());
            parms[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            parms[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parms);
            return Convert.ToInt32(parms[4].Value);
        }
        catch
        {
            return -1;
        }
    }
    protected bool DeletePerformanceResult(string id)
    {
        try
        {
            string strSql = "sp_perf_deletePerformanceResult";
            SqlParameter[] parms = new SqlParameter[1];

            parms[0] = new SqlParameter("@id", id);
        
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parms);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mydelete")
        {
            if(DeletePerformanceResult(e.CommandArgument.ToString()))
            {
                ltlAlert.Text = "alert('删除成功！')";
                
                bind();
            }
            else
        {
            ltlAlert.Text = "alert('删除失败！')";
        }
            //if (status == "1")
            //{
            //    ltlAlert.Text = "alert('已签核！')";
            //}
            //else
            //{
            //    if (SIDFactoryHelper.deleteSIDdet(e.CommandArgument.ToString()))
            //    {
            //        ltlAlert.Text = "alert('删除成功！')";
            //        BindData();
            //    }
            //    else
            //    {
            //        ltlAlert.Text = "alert('删除失败！')";
            //    }
            //}
            //Response.Redirect("/IT/SID_det.aspx?Id=" + e.CommandArgument.ToString() + "&rt=" + DateTime.Now + "','_blank'");
        }
        if (e.CommandName == "myupdate")
        {

            ids = e.CommandArgument.ToString();
               btnSave.Visible=true;
               btnAdd.Visible = false;
               GetPerformanceResult(e.CommandArgument.ToString());

           
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        gv.DataSource = SelectPerformanceResult(txbType.Text);
        gv.DataBind();

        btnSave.Visible = false;
        btnAdd.Visible = true;
        //txbRemark.Text = string.Empty;
        //txbType.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txbRemark.Text = string.Empty;
        txbType.Text = string.Empty;
        btnSave.Visible = false;
        btnAdd.Visible = true;
        bind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       int stuts= UpdatePerformanceResult(ids, txbType.Text.Trim(), txbRemark.Text.Trim());
          if (stuts==1)
            {
                ltlAlert.Text = "alert('修改成功！')";
                btnSave.Visible = false;
                bind();
                btnAdd.Visible = true;
                txbRemark.Text = string.Empty;
                txbType.Text = string.Empty;
            }
          else if (stuts == -1)
          {
              ltlAlert.Text = "alert('修改失败！')";
          }
          else
          {
              ltlAlert.Text = "alert('类型已存在,不可进行修改！')";
          }
         
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txbType.Text == string.Empty || txbType.Text == "")
        {
            ltlAlert.Text = "alert('类型不能为空！')";
        }
        else
        {
            int stus = InsertPerformanceResult(txbType.Text.Trim(), txbRemark.Text.Trim());

            if (stus==1)
            {
                ltlAlert.Text = "alert('新增成功！')";
                btnSave.Visible = false;
                bind();
                txbRemark.Text = string.Empty;
                txbType.Text = string.Empty;
            }
            else if (stus == -1)
            {
                ltlAlert.Text = "alert('新增失败！')";
            }
            else
            {
                ltlAlert.Text = "alert('类型已存在，不可进行新增！')";
            }
        }
    }
    public void bind()
    {
        gv.DataSource = GetPerformanceResult();
        gv.DataBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}





