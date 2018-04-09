using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class m5_mstr : BasePage
{
    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 绑定dropProject
            dropProject.DataSource = this.GetProjects();
            dropProject.DataBind();
            dropProject.Items.Insert(0, new ListItem("--All--", "0"));
            #endregion
            #region 绑定Market
            ddlMarket.DataSource = this.getMarket();
            ddlMarket.DataBind();
            ddlMarket.Items.Insert(0, new ListItem("--All--", "0"));
            #endregion

            #region 绑定Level
            ddlLevel.DataSource = this.GetDDLLevel();
            ddlLevel.DataBind();
            ddlLevel.Items.Insert(0, new ListItem("--All--", "0"));
            #endregion
            #region 如果是新建，则直接将no传过来
            try
            {
                txt_no.Text = Request.QueryString["no"].ToString();
            }
            catch
            {
            }
            #endregion
            //txtStdDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-7));
            BindGridView();
        }
    }

    private DataTable GetDDLLevel()
    {
        try
        {
            string sqlstr = "SELECT soque_degreeName,soque_did FROM dbo.soque_degree";

            return SqlHelper.ExecuteDataset(strConn, CommandType.Text, sqlstr).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    private DataTable getMarket()
    {
        try
        {
            string sqlstr = "sp_m5_GetDdlMarketing";

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetProjects()
    {
        try
        {
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_m5_selectProject").Tables[0];
        }
        catch
        {
            return null;
        }

    }
    protected override void BindGridView()
    {
        try
        {
            int i = Convert.ToInt32(ddlStatu.SelectedValue);
            string strName = "sp_m5_selectM5Mstr";
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@project", dropProject.SelectedValue);
            param[1] = new SqlParameter("@no", txt_no.Text);
            param[2] = new SqlParameter("@stdDate", txtStdDate.Text);
            param[3] = new SqlParameter("@endDate", txtEndDate.Text);
            param[4] = new SqlParameter("@types", Convert.ToInt32(ddlStatu.SelectedValue));
            param[5] = new SqlParameter("@isApprove", false);
            param[6] = new SqlParameter("@isAgree", false);
            param[7] = new SqlParameter("@desc", txtDesc.Text.Trim());
            param[8] = new SqlParameter("@type", ddlType.SelectedValue);
            param[9] = new SqlParameter("@uID", Session["uID"].ToString());
            param[10] = new SqlParameter("@Market", ddlMarket.SelectedItem.Value.ToString());
            param[11] = new SqlParameter("@Level", ddlLevel.SelectedValue);
            param[12] = new SqlParameter("@modelNo", txtModelNo.Text);
            param[13] = new SqlParameter("@createByself", Convert.ToInt32(chkIsBySelf.Checked));
            param[14] = new SqlParameter("@createdName", txtCreateName.Text.Trim());
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtStdDate.Text) && !this.IsDate(txtStdDate.Text))
        {
            this.Alert("Date format is not correct！");
            return;
        }

        if (!string.IsNullOrEmpty(txtEndDate.Text))
        {
            if (!this.IsDate(txtStdDate.Text))
            {
                this.Alert("Date format is not correct！");
                return;
            }
        }

        BindGridView();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            e.Row.Cells[2].Text = Convert.ToBoolean(rowView["m5_isAboutSafety"]) ? "Y" : "N";
            

            if (!string.Empty.Equals(rowView["m5_isAgreed"].ToString()))
            {
                e.Row.Cells[7].Text = Convert.ToBoolean(rowView["m5_isAgreed"]) ? "Y" : "N";
            }

            e.Row.Cells[8].Text = rowView["m5_createNameEn"].ToString();
            e.Row.Cells[14].Text = rowView["m5_apprNameEn"].ToString();
            e.Row.Cells[16].Text = rowView["m5_agreeNameEn"].ToString();
            //有签名则不能删除
            if (!string.IsNullOrEmpty(rowView["m5_apprBy"].ToString()))
            {
                e.Row.Cells[12].Text = string.Empty;
            }
            //未同意变更
            if (string.IsNullOrEmpty(rowView["m5_agreeName"].ToString()))
            {
                //主管未签字
                if (string.IsNullOrEmpty(rowView["m5_apprName"].ToString()))
                {

                    e.Row.Cells[12].Text = string.Empty;
                }
            }
            else
            {
                e.Row.Cells[13].Text = string.Empty;
            }
            //只有创建人和主管签字的才可以关闭
            string qq = Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["m5_apprName"]);
            string ww = Session["uName"].ToString();
            if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["m5_apprName"]) != Session["uName"].ToString() && Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["m5_createName"]) != Session["uName"].ToString() && Session["uId"].ToString() !=  "94307")
            {
                e.Row.Cells[13].Text = string.Empty;
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["m5_isClose"]) == 1)
            {
                e.Row.Cells[13].Text = Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["m5_closeDate"]);
            }
            
        }
    }
    public DataTable GetM5MstrByNo(string no)
    {
        try
        {
            string strSql = "sp_m5_selectM5MstrByNo";
            SqlParameter param = new SqlParameter("@no", no);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _no = gv.DataKeys[index].Values["m5_no"].ToString();
            this.Redirect("m5_detail.aspx?no=" + _no + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        if (e.CommandName == "ViewDetail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _no = gv.DataKeys[index].Values["m5_no"].ToString();
            //有签名则不能编辑

            DataTable dt = GetM5MstrByNo(_no);
            if (!string.IsNullOrEmpty(dt.Rows[0]["m5_apprName"].ToString()))
            {
                this.Alert("can not make changes！");
                BindGridView();
                return;
            }
            if (Convert.ToInt32(Session["uID"]) != Convert.ToInt32(gv.DataKeys[index].Values["m5_createBy"]))
            {
                this.Alert("can not edit！");
                BindGridView();
                return;
            }
            this.Redirect("m5_new_Edit.aspx?no=" + _no + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int intRow = e.RowIndex;
            if (Convert.ToInt32(Session["uID"]) != Convert.ToInt32(gv.DataKeys[intRow].Values["m5_createBy"]))
            {
                this.Alert("can not delete！");
                return ;
            }
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@m5_no", gv.DataKeys[intRow].Values["m5_no"].ToString());
            param[1] = new SqlParameter("@deleteBy", Convert.ToInt32(Session["uID"]));
            param[2] = new SqlParameter("@deleteName", Session["uName"].ToString());
            param[3] = new SqlParameter("@reValue", SqlDbType.Int);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_m5_deleteM5Mstr", param);

            if (Convert.ToInt32(param[3].Value) > 0)
            {
                this.Alert("Fail！");
                return ;
            }
        }
        catch
        {
            this.Alert("Fail！");
            return;
        }
        BindGridView();
    }
    protected void chkIsBySelf_CheckedChanged(object sender, EventArgs e)
    {
        BindGridView();
    }
}