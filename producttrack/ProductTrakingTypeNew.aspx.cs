using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class ProductTrakingTypeNew : BasePage
{
    private static string strConn = CommClass.admClass.getConnectString("SqlConn.Conn_qaddoc");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            BindData();
        }
    }

    private void BindData()
    {
        DataTable dt = GetProductTrackingType(dropType.SelectedValue, txtDetail.Text.Trim(), txtNote.Text.Trim()); 
        
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

            txtDetail.Text = gv.Rows[index].Cells[1].Text;
            txtNote.Text = gv.Rows[index].Cells[2].Text == "&nbsp;" ? "" : gv.Rows[index].Cells[2].Text;

            if (gv.Rows[index].Cells[3].Text == "是")
            {
                chk_related.Checked = true; 
            }
            else
            { 
                chk_related.Checked = false;
            }
         
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
        if (dropType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项类别！')";
            return;
        }
        
        if (txtDetail.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请填写条目！')";
            return;
        }

        String strDetail = txtDetail.Text.Trim();
        String strNote = txtNote.Text.Trim();
        String strUserId = Session["uID"].ToString();
        String strUserName = Session["uName"].ToString();
        Boolean blIsRelated = chk_related.Checked;

        Int32 insertflag = InsertProductTrackingType(dropType.SelectedValue, strDetail, strNote, strUserId, strUserName, blIsRelated);
        if (insertflag > 0)
        {
            if (insertflag == 2)
            {
                ltlAlert.Text = "alert('此产品类型下的分类已存在，不须再添加')";
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
        if (dropType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项类别！')";
            return;
        }

        if (txtDetail.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请填写条目')";
            return;
        }

        Int32 ptt_id = Convert.ToInt32(lblId.Text);
        String strDetail = txtDetail.Text.Trim();
        String strNote = txtNote.Text.Trim();
        String strUserId = Session["uID"].ToString();
        String strUserName = Session["uName"].ToString();

        if (!UpdateProductTrackingType(ptt_id, dropType.SelectedValue, strDetail, strNote, strUserId, strUserName, chk_related.Checked))
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
        chk_related.Checked = true;
        BindData();
    }

    private DataTable GetProductTrackingType(string strType, string strPackagingDetail, string strNote)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@ptt_type", strType);
        param[1] = new SqlParameter("@ptt_detail", strPackagingDetail);
        param[2] = new SqlParameter("@ptt_rmks", strNote);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_selectProductTrackingTypeNew", param).Tables[0];
    }

    private int InsertProductTrackingType(string strType, string strDetail, string strNote, string strUserId, string strUserName, bool blIsRelated)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@ptt_type", strType);
        param[1] = new SqlParameter("@ptt_detail", strDetail);
        param[2] = new SqlParameter("@ptt_rmks", strNote);
        param[3] = new SqlParameter("@ptt_createby", strUserId);
        param[4] = new SqlParameter("@ptt_createName", strUserName);
        param[5] = new SqlParameter("@ptt_isRelated", blIsRelated);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_insertProductTrackingTypeNew", param));
    }

    private bool DeleteProductTrackingType(int iptt_id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ptt_id", iptt_id);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_deleteProductTrackingTypeNew", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    private bool UpdateProductTrackingType(int ptt_id, string strType, string strDetail, string strNote, string strUserId, string strUserName, bool blIsRelated)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ptt_id", ptt_id);
            param[1] = new SqlParameter("@ptt_type", strType);
            param[2] = new SqlParameter("@ptt_detail", strDetail);
            param[3] = new SqlParameter("@ptt_rmks", strNote);
            param[4] = new SqlParameter("@ptt_Modifyby", strUserId);
            param[5] = new SqlParameter("@ptt_ModifyName", strUserId);
            param[6] = new SqlParameter("@ptt_isRelated", blIsRelated);
            param[7] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[7].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_updateProductTrackingTypeNew", param);
            return Convert.ToBoolean(param[7].Value);
        }
        catch
        {
            return false;
        }
    }


   
}