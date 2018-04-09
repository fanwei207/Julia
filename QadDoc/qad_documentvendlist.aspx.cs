using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

public partial class QadDoc_qad_documentvendlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

            BindData();

    }

    protected void BindData()
    {
        string docid = "0";
        if (Request.QueryString["docid"] != null)
        {
            docid = Request.QueryString["docid"].ToString();
        }
        DataTable dt = SelectVendList(docid);
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            gvVend.DataSource = dt;
            gvVend.DataBind();
            int columnCount = gvVend.Rows[0].Cells.Count;
            gvVend.Rows[0].Cells.Clear();
            gvVend.Rows[0].Cells.Add(new TableCell());
            gvVend.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvVend.Rows[0].Cells[0].Text = "No data";
            gvVend.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            gvVend.DataSource = dt;
            gvVend.DataBind();
        }

    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["docid"] == null)
        {
            ltlAlert.Text = "alert('No Documents!'); ";
            BindData();
            return;
        }
        if (txtVend.Text.Length == 0)
        {
            ltlAlert.Text = "alert('vend can not be empty!'); ";
            BindData();
            return;
        }
        if (!IsExistVend(txtVend.Text.Trim(),Session["PlantCode"].ToString()))
        {
            ltlAlert.Text = "alert('vend does not exist!'); ";
            BindData();
            return;
        }
        if (IsExistVendList(Request.QueryString["docid"], txtVend.Text.Trim()))
        {
            ltlAlert.Text = "alert('this vend does already exist!'); ";
            BindData();
            return;
        }
        string message = InsertVend(Request.QueryString["docid"], txtVend.Text.Trim(), Convert.ToInt32(Session["uID"]), Session["PlantCode"].ToString());
             if(message!=string.Empty)
        {
            ltlAlert.Text = "alert('" + message + "'); ";
            BindData();
            return;
        }
        BindData();




    }
    protected void gvVend_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strID = gvVend.DataKeys[e.RowIndex]["id"].ToString();
        string message = DeleteVend(strID);
        if (message != string.Empty)
        {
            ltlAlert.Text = "alert('" + message + "'); ";
            BindData();
            return;
        }
        else
        {
            BindData();
            return;
        }
    }
    protected static DataTable SelectVendList(string docid)
    {
        try
        {
            string strName = "qad_document_selectVendList";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@docid", docid);
            return SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, sqlParam).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    protected static string  DeleteVend(string id)
    {
        try
        {
            string strName = "qad_document_deleteVendList";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;
            param[2] = new SqlParameter("@message", SqlDbType.NVarChar, 100);
            param[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param);


            string message = param[2].Value.ToString();


            return message;


        }
        catch (Exception ex)
        {
            return "删除失败！";
        }
    }

    protected static string InsertVend(string docid, string vend, int uID, string plantCode)
    {
        try
        {
            string strName = "qad_document_insertVendList";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@docid", docid);
            param[1] = new SqlParameter("@vend", vend);
            param[2] = new SqlParameter("@uID", uID);
            param[3] = new SqlParameter("@plantCode", plantCode);
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@message", SqlDbType.NVarChar, 100);
            param[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param);
            string message = param[5].Value.ToString();


            return message;

        }
        catch (Exception ex)
        {
            return "新增失败！";
        }
    }

    protected static bool IsExistVendList(string docid, string vend)
    {
        try
        {
            string strName = "qad_document_checkVendList";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@docid", docid);
            param[1] = new SqlParameter("@vend", vend);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[2].Value);

        }
        catch (Exception ex)
        {
            return false;
        }


    }

    protected static bool IsExistVend(string vend, string plantCode)
    {
        try
        {
            string strName = "qad_document_checkVend";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@vend", vend);
            param[1] = new SqlParameter("@plantCode", plantCode);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[2].Value);

        }
        catch (Exception ex)
        {
            return false;
        }


    }

    protected void gvVend_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVend.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvVend_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


        }
    }
}