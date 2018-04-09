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

public partial class note_temp : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.BarCodeSys"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetNoteType();
            GridViewBind();
        }
    }

    private void GridViewBind() 
    {
        try
        {
            string strName = "sp_note_selectNoteTemp";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@desc", txtDesc.Text);
            param[1] = new SqlParameter("@type", dropType.SelectedValue);

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    private void GetNoteType()
    {
        try
        {
            string strName = "sp_note_selectNoteType";

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@duty", "");
            param[1] = new SqlParameter("@type", "");
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            dropType.DataSource = ds;
            dropType.DataBind();

            dropType.Items.Insert(0, new ListItem("--", "0"));
        }
            catch
        {
            ;
        }
    }

    private bool checkNoteIsExist(string desc)
    {
        try
        {
            string strName = "sp_note_checkNoteTempIsExist";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@desc", desc);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        GridViewBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strName = "sp_note_deleteNoteTemp";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", gv.DataKeys[e.RowIndex].Values["ntt_id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[3].Value))
            {
                this.Alert("删除成功！");
            }
            else
            {
                this.Alert("删除失败！");
            }
        }
        catch
        {
            this.Alert("删除失败！请联系管理员！"); ;
        }

        GridViewBind();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        GridViewBind();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string _id = gv.DataKeys[e.RowIndex].Values["ntt_id"].ToString();
        TextBox txDesc = (TextBox)gv.Rows[e.RowIndex].FindControl("txDesc");
        if (txDesc.Text.Length == 0)
        {
            this.Alert("工作内容不能为空！");
            return;
        }

        if (checkNoteIsExist(txDesc.Text))
        {
            ltlAlert.Text = "alert('工作内容不能重复!');";
            return;
        }

        try
        {
            string strName = "sp_note_updateNoteTemp";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@id", _id);
            param[1] = new SqlParameter("@desc", txDesc.Text);
            param[2] = new SqlParameter("@uID", Session["uID"].ToString());
            param[3] = new SqlParameter("@uName", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[4].Value))
            {
                this.Alert("更新成功！");
            }
            else
            {
                this.Alert("更新失败！");
            }
        }
        catch
        {
            this.Alert("更新失败！请联系管理员！"); ;
        }

        gv.EditIndex = -1;
        GridViewBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请先选择一个 工作事项!');";
            return;
        }

        if (txtDesc.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('工作内容不能为空!');";
            return;
        }

        if (checkNoteIsExist(txtDesc.Text))
        {
            ltlAlert.Text = "alert('工作内容不能重复!');";
            return;
        }

        try
        {
            string strName = "sp_note_insertNoteTemp";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@desc", txtDesc.Text.Trim());
            param[1] = new SqlParameter("@type", dropType.SelectedValue);
            param[2] = new SqlParameter("@uID", Session["uID"].ToString());
            param[3] = new SqlParameter("@uName", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[4].Value))
            {
                this.Alert("新建成功！");
            }
            else
            {
                this.Alert("新建失败！");
            }
        }
        catch
        {
            this.Alert("新建失败！请联系管理员！"); ;
        }

        GridViewBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
}
