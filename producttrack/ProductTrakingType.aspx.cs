using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class ProductTrakingType : BasePage
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
        String strProductType = ddlProductType.SelectedItem.Text;
        String strPakagingType = txtPackagingType.Text.ToString().Trim();
        String strNote = txtNote.Text.ToString().Trim();

        DataTable dt = getProductTrakingType(strProductType, strPakagingType, strNote); 
        
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
        deleteProductTrakingType(iptt_id);  
        BindData(); 
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ModifyDesc")
        {
            LinkButton linkBtn = (LinkButton)(e.CommandSource);
            int index = ((GridViewRow)(linkBtn.Parent.Parent)).RowIndex;
 
            lblId.Text = gv.DataKeys[index][0].ToString();

            ddlProductType.SelectedValue = gv.Rows[index].Cells[1].Text.ToString(); 
            txtPackagingType.Text = gv.Rows[index].Cells[2].Text.ToString();
            txtNote.Text = gv.Rows[index].Cells[3].Text.ToString() == "&nbsp;" ? "" : gv.Rows[index].Cells[3].Text.ToString();

            if (gv.Rows[index].Cells[4].Text.ToString() == "是")
            {
                chk_related.Checked = true; 
            }
            else
            { 
                chk_related.Checked = false;
            }
         

            ddlProductType.Enabled = false;
            txtPackagingType.Enabled = false;
            btnAdd.Visible = false;
            btnSave.Visible = true;
            BindData();
        }
        else if (e.CommandName == "Relate")
        {
            Response.Redirect("ProductTypeRelateCate.aspx?id=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now, true);
        }
    }
     
    protected void btnSearch_Click(object sender, EventArgs e)
    {
       
        BindData();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlProductType.SelectedIndex <= 0)
        {
            ltlAlert.Text = "alert('请选择产品类型')";
            return; 
        }
        if(txtPackagingType.Text.ToString().Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请填写产品分类')";
            return; 
        } 
        String strProductType = ddlProductType.SelectedItem.Text;
        String strPakagingType = txtPackagingType.Text.ToString().Trim();
        String strNote = txtNote.Text.ToString().Trim();
        String strUserId = Session["uID"].ToString();
        String strUserName = Session["uName"].ToString();
        Boolean blIsRelated = chk_related.Checked;


        Int32 insertflag = insertProductTrakingType(strProductType, strPakagingType, strNote, strUserId, strUserName, blIsRelated);
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
        if (ddlProductType.SelectedIndex <= 0)
        {
            ltlAlert.Text = "alert('请选择产品分类')";
            return;
        }
        if (txtPackagingType.Text.ToString().Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请填写包装分类')";
            return;
        }
        Int32 IpttId = Convert.ToInt32(lblId.Text.ToString());
        String strProductType = ddlProductType.SelectedItem.Text;
        String strPakagingType = txtPackagingType.Text.ToString().Trim();
        String strNote = txtNote.Text.ToString().Trim();
        String strUserId = Session["uID"].ToString();
        String strUserName = Session["uName"].ToString();
        Int32 IflagUpdate = UpdateProductTrakingType(IpttId,strProductType, strPakagingType, strNote, strUserId, strUserName,chk_related.Checked);

        if (IflagUpdate < 0)
        {
            ltlAlert.Text = "alert('修改失败！')";
            return;
        }
        else
        {
            btnAdd.Visible = true;
            btnSave.Visible = false;
            ddlProductType.Enabled = true;
            txtPackagingType.Enabled = true;
            txtPackagingType.Text = String.Empty;
            txtNote.Text = String.Empty;
            BindData();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlProductType.SelectedIndex = 0;
        txtPackagingType.Text = String.Empty;
        txtNote.Text = String.Empty;
        btnAdd.Visible = true;
        btnSave.Visible = false;
        ddlProductType.Enabled = true;
        txtPackagingType.Enabled = true;
        chk_related.Checked = true;
        BindData();
    }

  
    private DataTable getProductTrakingType(string strProductType, string strPakagingType, string strNote)
    {

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@ptt_type", strProductType);
        param[1] = new SqlParameter("@ptt_detail", strPakagingType);
        param[2] = new SqlParameter("@ptt_rmks", strNote);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_selectProductTrackingType", param).Tables[0];
    }

    private int insertProductTrakingType(string strProductType, string strPakagingType, string strNote, string strUserId, string strUserName, bool blIsRelated)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@ptt_type", strProductType);
        param[1] = new SqlParameter("@ptt_detail", strPakagingType);
        param[2] = new SqlParameter("@ptt_rmks", strNote);
        param[3] = new SqlParameter("@ptt_createby", strUserId);
        param[4] = new SqlParameter("@ptt_createName", strUserName);
        param[5] = new SqlParameter("@ptt_isRelated", blIsRelated);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_InsertProductTrackingType", param));
    }

    private int deleteProductTrakingType(int iptt_id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@ptt_id", iptt_id);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_deleteProductTrackingType", param));

    }

    private int UpdateProductTrakingType(int IpttId, string strProductType, string strPakagingType, string strNote, string strUserId, string strUserName, bool blIsRelated)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@ptt_id", IpttId);
        param[1] = new SqlParameter("@ptt_type", strProductType);
        param[2] = new SqlParameter("@ptt_detail", strPakagingType);
        param[3] = new SqlParameter("@ptt_rmks", strNote);
        param[4] = new SqlParameter("@ptt_Modifyby", strUserId);
        param[5] = new SqlParameter("@ptt_ModifyName", strUserId);
        param[6] = new SqlParameter("@ptt_isRelated", blIsRelated);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_UpdateProductTrackingType", param));
  
    }


   
}