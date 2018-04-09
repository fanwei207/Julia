using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class New_TypeNew : BasePage
{
    adamClass adam = new adamClass();
    private static string strConn = CommClass.admClass.getConnectString("SqlConn.Conn_qaddoc");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDetail.Text = "";
            txtNote.Text = "";
            BindData();
        }
    }

    private void BindData()
    {
        DataTable dt = GetType(dropType.SelectedValue, txtDetail.Text.Trim(), txtNote.Text.Trim()); 
        
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
        }
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int iptt_id = Convert.ToInt32(gv.DataKeys[e.RowIndex][0].ToString());
        DeleteProductTrackingType(iptt_id);  
        BindData();
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ModifyDesc")
        {
            LinkButton linkBtn = (LinkButton)(e.CommandSource);
            int index = ((GridViewRow)(linkBtn.Parent.Parent)).RowIndex;
 
            lblId.Text = gv.DataKeys[index][0].ToString();

            txtDetail.Text = gv.Rows[index].Cells[0].Text;
            txtNote.Text = gv.Rows[index].Cells[1].Text == "&nbsp;" ? "" : gv.Rows[index].Cells[1].Text;
         
            btnAdd.Visible = false;
            btnSave.Visible = true;
            BindData();
        }
        else if (e.CommandName == "Relate")
        {
            Response.Redirect("ProductTypeRelateCateNew.aspx?id=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now, true);
        }
    }
     
    protected void btnSearch_Click(object sender, EventArgs e)
    { 
        BindData();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if (dropType.SelectedIndex == 0)
        //{
        //    ltlAlert.Text = "alert('请选择一项类别！')";
        //    return;
        //}
        
        if (txtDetail.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请填写类型！')";
            return;
        }
        Int32 insertflag = InsertProductTrackingType(txtDetail.Text.Trim(), txtNote.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
        if (insertflag > 0)
        {
            if (insertflag == 2)
            {
                ltlAlert.Text = "alert('此类型下的分类已存在，不须再添加')";
                return;
            }
            else
            {
                txtNote.Text = string.Empty;

                BindData();
            }
        }
        else
        {
            ltlAlert.Text = "alert('添加失败')";
            return;
        } 
     
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (dropType.SelectedIndex == 0)
        //{
        //    ltlAlert.Text = "alert('请选择一项类别！')";
        //    return;
        //}

        if (txtDetail.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请填写类型')";
            return;
        }

        Int32 ptt_id = Convert.ToInt32(lblId.Text);
        String strDetail = txtDetail.Text.Trim();
        String strNote = txtNote.Text.Trim();
        String strUserId = Session["uID"].ToString();
        String strUserName = Session["uName"].ToString();
        if (checkEdit(txtDetail.Text.Trim(), ptt_id))
        {
            this.Alert("此类型已经存在，无须再添加！");
            return;
        }
        if (!UpdateProductTrackingType(ptt_id, txtDetail.Text, strNote, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('修改失败！')";
            return;
        }
        else
        {
            btnAdd.Visible = true;
            btnSave.Visible = false;
            txtDetail.Text = String.Empty;
            txtNote.Text = String.Empty;
            BindData();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtDetail.Text = String.Empty;
        txtNote.Text = String.Empty;
        btnAdd.Visible = true;
        btnSave.Visible = false;
        BindData();
    }

    private DataTable GetType(string strType, string strPackagingDetail, string strNote)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@ptt_type", strType);
        param[1] = new SqlParameter("@ptt_detail", strPackagingDetail);
        param[2] = new SqlParameter("@ptt_rmks", strNote);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_perf_ManagePerformanceType", param).Tables[0];
    }

    private int InsertProductTrackingType(string strDetail, string strNote, string strUserId, string strUserName)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@ptt_detail", strDetail);
        param[1] = new SqlParameter("@ptt_rmks", strNote);
        param[2] = new SqlParameter("@ptt_createby", strUserId);
        param[3] = new SqlParameter("@ptt_createName", strUserName);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, "sp_perf_insertPerformanceType", param));
    }

    private bool DeleteProductTrackingType(int iptt_id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ptt_id", iptt_id);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_perf_DeletePerformanceType", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// 检查编辑项是否存在
    /// </summary>
    /// <param name="type"></param>
    /// <param name="remark"></param>
    /// <returns></returns>
    protected bool checkEdit(string type, int id)
    {
        string sp_name = "sp_perf_checkType";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@type", type);
        parm[1] = new SqlParameter("@id", id);
        parm[2] = new SqlParameter("@retValue", SqlDbType.Bit);
        parm[2].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sp_name, parm);
        return Convert.ToBoolean(parm[2].Value);
    }
    private bool UpdateProductTrackingType(int ptt_id, string strType, string strNote, string modifyby, string modifyName)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ptt_id", ptt_id);
            param[1] = new SqlParameter("@ptt_type", strType);
            param[2] = new SqlParameter("@ptt_rmks", strNote);
            param[3] = new SqlParameter("@ptt_modifyby", modifyby);
            param[4] = new SqlParameter("@ptt_modifyName", modifyName);
            param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[5].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_perf_UpdatePerformanceType", param);
            return Convert.ToBoolean(param[5].Value);
        }
        catch
        {
            return false;
        }
    }

}