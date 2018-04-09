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
using adamFuncs;
using System.IO;
using QADSID;
using System.Data.SqlClient;

public partial class SID_SID_LadingNum : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gv.Columns[0].Visible = false;
            BindData();
        }
    }
    private void BindData()
    {
        DataTable dt = GetLadingList(txtShipNum.Text, txtReceipt.Text, txtLadDate.Text);
        gv.DataSource = dt;
        gv.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtShipNum.Text == string.Empty)
        {
            ltlAlert.Text = "alert('出运单号不能为空!')";
            return;
        }
        if(txtReceipt.Text == string.Empty)
        {
            ltlAlert.Text = "alert('发票号不能为空!')";
            return;
        }
        if (txtLadDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('提单日期不能为空!')";
            return;
        }
        if (JudgeLadingList(txtShipNum.Text, txtReceipt.Text))
        {
            ltlAlert.Text = "alert('表中已存在相同的出运单号或发票号!')";
            return;
        }
        if (AddLadingList(txtShipNum.Text, txtReceipt.Text, txtLadDate.Text))
        {
            ltlAlert.Text = "alert('添加成功!')";
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('添加失败，重新添加!')";
            BindData();
        }
    }
    private bool JudgeLadingList(string shipnum, string receipt)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@shipnum", shipnum);
        param[1] = new SqlParameter("@receipt", receipt);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_sid_JudgeLadingList", param));
    }
    protected void btnReach_Click(object sender, EventArgs e)
    {
        GetLadingList(txtShipNum.Text, txtReceipt.Text, txtLadDate.Text);
        BindData();
    }
    private DataTable GetLadingList(string shipnum, string receipt, string laddate)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@shipnum", shipnum);
        param[1] = new SqlParameter("@receipt", receipt);
        param[2] = new SqlParameter("@laddate", laddate);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_sid_GetLadingList", param).Tables[0];
    }
    private bool AddLadingList(string shipnum, string receipt, string laddate)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@shipnum", shipnum);
        param[1] = new SqlParameter("@receipt", receipt);
        param[2] = new SqlParameter("@laddate", laddate);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_sid_AddLadingList", param));
    }

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindData();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindData();
        //String shipnum = (gv.Rows[e.NewEditIndex].Cells[0].FindControl("txtShipNum")).ToString();
        //shipnum = shipnum.ToUpper();
        //((DropDownList)gv.Rows[e.NewEditIndex].Cells[0].FindControl("txtShipNum")).Items.FindByText(shipnum).Selected = true;


        //String receipt = (gv.Rows[e.NewEditIndex].Cells[1].FindControl("txtReceipt")).ToString();
        //receipt = receipt.ToUpper();
        //((DropDownList)gv.Rows[e.NewEditIndex].Cells[1].FindControl("txtReceipt")).Items.FindByText(receipt).Selected = true;

        //String ladingdate = (gv.Rows[e.NewEditIndex].Cells[2].FindControl("txtLadDate")).ToString();
        //ladingdate = ladingdate.ToUpper();
        //((DropDownList)gv.Rows[e.NewEditIndex].Cells[2].FindControl("txtLadDate")).Items.FindByText(ladingdate).Selected = true;

    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //String shipnum = ((TextBox)gv.Rows[e.RowIndex].Cells[1].FindControl("txtShipNum")).Text.ToString().Trim();
        //String receipt = ((TextBox)gv.Rows[e.RowIndex].Cells[2].FindControl("txtReceipt")).Text.ToString().Trim();
        String ladingdate = ((TextBox)gv.Rows[e.RowIndex].Cells[3].FindControl("txtLadDate")).Text.ToString().Trim();

        if (UpdateLadingList(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values[0].ToString()), ladingdate))
        {
            gv.EditIndex = -1;
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('更新失败！');";
            return;
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (DeleteLadingList(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values[0].ToString())))
        {
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('删除失败！');";
            return;
        }
    }
    private bool UpdateLadingList(int id,string laddate)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@id", id);
        //param[1] = new SqlParameter("@shipnum", shipnum);
        //param[2] = new SqlParameter("@receipt", receipt);
        param[1] = new SqlParameter("@laddate", laddate);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_sid_UpdateLadingList", param));
    }
    private bool DeleteLadingList(int id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", id);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_sid_DeleteLadingList", param));
    }
    
}