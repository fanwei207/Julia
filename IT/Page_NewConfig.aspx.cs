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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using IT;

public partial class Page_NewConfig : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pageID"] == null)
            {
                this.Alert("缺少pageID参数，无法执行本程序！");
                return;
            }

            this.hidPageID.Value = Request.QueryString["pageID"];
            
            BindGridView();
        }
    }
    protected new void BindGridView()
    {
        gv.DataSource = PageMakerHelper.GetAddableFields(this.hidPageID.Value);
        gv.DataBind();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        BindGridView();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        BindGridView();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtRowIndex = gv.Rows[e.RowIndex].FindControl("txtRowIndex") as TextBox;
        TextBox txtRowHeight = gv.Rows[e.RowIndex].FindControl("txtRowHeight") as TextBox;
        TextBox txtRowSpan = gv.Rows[e.RowIndex].FindControl("txtRowSpan") as TextBox;

        TextBox txtColIndex = gv.Rows[e.RowIndex].FindControl("txtColIndex") as TextBox;
        TextBox txtColWidth = gv.Rows[e.RowIndex].FindControl("txtColWidth") as TextBox;
        TextBox txtColSpan = gv.Rows[e.RowIndex].FindControl("txtColSpan") as TextBox;

        #region Validation
        if (!this.IsNumber(txtRowIndex.Text))
        {
            this.Alert("Row Index must be a number");
            return;
        }

        if (!this.IsNumber(txtRowHeight.Text))
        {
            this.Alert("Row Height must be a number");
            return;
        }

        if (!this.IsNumber(txtRowSpan.Text))
        {
            this.Alert("Row Span must be a number");
            return;
        }

        if (!this.IsNumber(txtColIndex.Text))
        {
            this.Alert("Col Index must be a number");
            return;
        }

        if (!this.IsNumber(txtColWidth.Text))
        {
            this.Alert("Col Width must be a number");
            return;
        }

        if (!this.IsNumber(txtColSpan.Text))
        {
            this.Alert("Col Span must be a number");
            return;
        }
        #endregion

        try
        {
            string strName = "sp_page_updateAddableConfig";
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@pageID", hidPageID.Value);
            param[1] = new SqlParameter("@colName", gv.DataKeys[e.RowIndex].Values["pd_colName"].ToString());
            param[2] = new SqlParameter("@rowIndex", txtRowIndex.Text);
            param[3] = new SqlParameter("@rowHeight", txtRowHeight.Text);
            param[4] = new SqlParameter("@rowSpan", txtRowSpan.Text);
            param[5] = new SqlParameter("@colIndex", txtColIndex.Text);
            param[6] = new SqlParameter("@colWidth", txtColWidth.Text);
            param[7] = new SqlParameter("@colSpan", txtColSpan.Text);
            param[8] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[8].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[8].Value))
            {
                this.Alert("更新失败！可能是格式不正确！");
            }
        }
        catch
        {
            this.Alert("更新失败！请联系管理员！");
        }

        gv.EditIndex = -1;
        BindGridView();
    }
}
