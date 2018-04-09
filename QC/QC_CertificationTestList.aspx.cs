using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class QC_QC_CertificationTestList : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindGV(); 
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.Redirect("../QC/QC_CertificationTestNew.aspx?type=new");
    }
    protected override void BindGridView()
    {
        DataTable dt = selectQCTestListInfo();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable selectQCTestListInfo()
    {
        string str = "sp_qc_selectQCTestMstrList";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@no", txtNo.Text.Trim());
        param[1] = new SqlParameter("@nbr", txtNbr.Text.Trim());
        param[2] = new SqlParameter("@lot", txtLot.Text.Trim());
        param[3] = new SqlParameter("@testType", ddlTestType.SelectedValue.ToString());
        param[4] = new SqlParameter("@status", ddlStatus.SelectedValue.ToString());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["QC_TestType"])) 
            {
                case 0:
                    e.Row.Cells[7].Text = "不通过";
                    break;
                case 1:
                    e.Row.Cells[7].Text = "通过";
                    break;
            }
            switch (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]))
            {
                case 0:
                    e.Row.Cells[8].Text = "已保存";
                    break;
                case 1:
                    e.Row.Cells[8].Text = "已完成";
                    break;
            }
            if (gv.DataKeys[e.Row.RowIndex].Values["createBy"].ToString() != Session["uID"].ToString())
            {
                e.Row.Cells[9].Enabled = false;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detial")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string _ID = gv.DataKeys[intRow].Values["ID"].ToString();
            this.Redirect("../QC/QC_CertificationTestDet.aspx?ID=" + gv.DataKeys[intRow].Values["ID"].ToString());
        }
        if (e.CommandName.ToString() == "ViewEdit")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            this.Redirect("../QC/QC_CertificationTestNew.aspx?type=det&ID=" + gv.DataKeys[index].Values["ID"].ToString().Trim()
                                                                          + "&no=" + gv.DataKeys[index].Values["QC_No"].ToString().Trim());
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string ID = gv.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", ID);
        string str = "sp_qc_DeleteQCTestMstr";
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

        BindGridView();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
}