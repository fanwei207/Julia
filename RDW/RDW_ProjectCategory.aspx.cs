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
using System.IO;
using RD_WorkFlow;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class RDW_ProjectCategory : BasePage
{
    RDW rdw = new RDW();

    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

            BindData();
        }
    }

    protected void BindData()
    {
        DataSet dataset = rdw.SelectProjectCategory(txtCategory.Text);
        gv.DataSource = dataset;
        gv.DataBind();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (txtCode.Text.Trim().Length <= 0)
        { 
            ltlAlert.Text = "alert('Enter code,Please!');";
            return;
        }
        else
        {
            if (txtCode.Text.Trim().Length > 3)
            {
                ltlAlert.Text = "alert('the length of code is equal or lesser than!');";
                return;
            }
        }

        if (txtCategory.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('Enter Category Name,Please !');";
            return;
        }

        try
        {
            string strSql = "sp_RDW_insertCategory";
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@name", txtCategory.Text.Trim());
            sqlParam[1] = new SqlParameter("@uID", Session["uID"]);
            sqlParam[2] = new SqlParameter("@uName", Session["eName"]);
            sqlParam[3] = new SqlParameter("@code", txtCode.Text);
            sqlParam[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

            if (!Convert.ToBoolean(sqlParam[4].Value))
            {
              ltlAlert.Text = "alert('An error has occurred or this Code has already exist!');";
            }


            BindData();
        }
        catch
        {
            throw new Exception("DB connection error when inserting...");
        }
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkLinkSample = (CheckBox)e.Row.FindControl("chkLinkSample");
            chkLinkSample.Checked = Convert.ToBoolean(gv.DataKeys[e.Row.RowIndex].Values["cate_linksample"]);
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strSql = "sp_RDW_deleteCategory";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@id", gv.DataKeys[e.RowIndex].Values["cate_id"].ToString());
            sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

            if (Convert.ToBoolean(sqlParam[1].Value))
            {
                ltlAlert.Text = "alert('success!');";
            }
            else
            {
                ltlAlert.Text = "alert('An error has occurred or this Category has already exist!');";
            }

            BindData();
        }
        catch
        {
            throw new Exception("DB connection error when inserting...");
        }
    }

    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindData();
    }

    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string cateId = gv.DataKeys[e.RowIndex].Values["cate_id"].ToString();
        TextBox txtCategory = (TextBox)gv.Rows[e.RowIndex].Cells[1].Controls[0];
        TextBox txtProjCode  = (TextBox)gv.Rows[e.RowIndex].Cells[2].Controls[0];
        //int figures;
        try
        {
            int figures = Convert.ToInt16(txtProjCode.Text.ToString());
        }
        catch
        {
            ltlAlert.Text = "alert('the ProjCode Serial Number must be a number!');";
            return;
        }

        try
        {
            string strSql = "sp_RDW_updateCategoryProjCode";
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@id", cateId);
            sqlParam[1] = new SqlParameter("@Category", txtCategory.Text.Trim().ToString());
            sqlParam[2] = new SqlParameter("@ProjCode", txtProjCode.Text.ToString()); 
            sqlParam[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

            if (!Convert.ToBoolean(sqlParam[3].Value))
            {
                ltlAlert.Text = "alert('An error has occurred!');";
            } 
            BindData();
        }
        catch
        {
            throw new Exception("DB connection error when updating...");
        }

        gv.EditIndex = -1; 
        BindData();
    }

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindData();

    }
        

    protected void chkLinkSample_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkLinkSample = (CheckBox)sender;
        int index = ((GridViewRow)(chkLinkSample.Parent.Parent)).RowIndex;

        try
        {
            string strSql = "sp_RDW_updateCategoryLinkSample";
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@id", gv.DataKeys[index].Values["cate_id"].ToString());
            sqlParam[1] = new SqlParameter("@chk", chkLinkSample.Checked);
            sqlParam[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

            if (!Convert.ToBoolean(sqlParam[2].Value))
            {
                ltlAlert.Text = "alert('An error has occurred!');";
            }

            BindData();
        }
        catch
        {
            throw new Exception("DB connection error when deletting...");
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int intRow = 0;
        string cate_id;
        string cate;
        string TempName;

        if (e.CommandName.ToString()=="DO")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            cate_id = gv.DataKeys[intRow]["cate_id"].ToString();
            cate = gv.Rows[intRow].Cells[1].Text;
            TempName = gv.Rows[intRow].Cells[7].Text;
            Response.Redirect("/RDW/RDW_SetCategoryDetials.aspx?cate_id=" + cate_id + "&TempName=" + TempName + "&cate=" + cate + "&uID=" + Convert.ToString(Session["uID"]));
        }
        else if (e.CommandName == "Doc")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string path = gv.DataKeys[intRow].Values["DocPath"].ToString();

            if (String.IsNullOrEmpty(path))
            {
                ltlAlert.Text = "var w=window.open('/docs/ProjectProposal&ApprovalForm.docx','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

            }
            else
            {
                ltlAlert.Text = "var w=window.open('" + path + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

            }
        }
        
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string title = "<b>Category Code</b>~^100^<b>Category Name</b>~^120^<b>Creator</b>~^200^<b>Create Date</b>~^120^";
        string strSql = "sp_RDW_selectCategoryExport";

        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@name", txtCategory.Text.Trim());

        DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
        ExportExcel(title, dt, false);
    }
}
