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

public partial class Supplier_FI_check : BasePage
{
    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bandgv(Request.QueryString["FI_id"].ToString());
            binddetil(Request.QueryString["FI_id"].ToString());
        }
    }
    public void bandgv(string id)
    {
        gv1.DataSource = getdepartment(id);
        gv1.DataBind();
    }
    public DataTable getdepartment(string FI_id)
    {
        try
        {
            string strName = "sp_FI_selectFIcheck";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@fi_id", FI_id);


            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];


        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public void binddetil(string FI_id)
    {
        try
        {
            string strName = "sp_FI_selectFIcheckbyuid";

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@fi_id", FI_id);
            param[1] = new SqlParameter("@uid", Session["uID"].ToString());

            DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            
            if (dt.Rows.Count==0)
            {
                tablecheck.Visible = false;
                return;
            }
            lbldepart.Text = dt.Rows[0]["departmentName"].ToString()+"评价:";
            txtReason.Text = dt.Rows[0]["FI_desc"].ToString() ;
            string FI_use = dt.Rows[0]["FI_use"].ToString() ;
            if (FI_use == "是" || FI_use == "")
            {
                rbtnuse.SelectedValue = "1";
            }
            else if (FI_use == "否")
            {
                rbtnuse.SelectedValue = "0";
            }
            else
            {
                rbtnuse.SelectedValue = "2";
                txtother.Text = FI_use;
            }
            txtremark.Text = dt.Rows[0]["FI_remark"].ToString();
            lblcreateby.Text = dt.Rows[0]["FI_createname"].ToString();


        }
        catch (Exception ex)
        {
            tablecheck.Visible = false;
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supp_FactoryInspection_mstr.aspx?no=" + Request.QueryString["fi_no"].ToString());
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string desc = txtReason.Text.Trim();
        if (desc == string.Empty)
        {
            this.Alert("评价不能为空！");
            return;
        }
        string use;
        if (rbtnuse.SelectedValue == "2")
        {
            use = txtother.Text;
            if (use == string.Empty)
            {
                this.Alert("其他理由不能空！");
                return;
            }
        }
        else
        {
            use = rbtnuse.SelectedItem.Text;
        }
        
        try
        {
            string strName = "sp_FI_saveFIcheck";
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@fi_id", Request.QueryString["FI_id"].ToString());
            param[1] = new SqlParameter("@fi_dese", desc);
            param[2] = new SqlParameter("@fi_use", use);
            param[3] = new SqlParameter("@fi_remark", txtremark.Text);
           
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;
          
            param[5] = new SqlParameter("@uID", Session["uID"].ToString());
            param[6] = new SqlParameter("@Uname", Session["uName"].ToString());
 

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[4].Value))
            {
                this.Alert("save failed！");
            }
            else
            {
                this.ltlAlert.Text = "alert('success！');";
               // Response.Redirect("Supp_FactoryInspection_mstr.aspx?no=" + Request.QueryString["fi_no"].ToString());
            }
        }
        catch (Exception ex)
        {
            this.Alert("save failed！");
        }
    }
    protected void btncancle_Click(object sender, EventArgs e)
    {
        txtReason.Text = "";
        txtother.Text = "";
        txtremark.Text = "";
        rbtnuse.SelectedValue = "1";
    }
}