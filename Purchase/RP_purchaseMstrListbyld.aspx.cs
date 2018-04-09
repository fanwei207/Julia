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

public partial class Purchase_rp_purchaseMstrLise : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlPlant.SelectedValue = Session["PlantCode"].ToString();
            txt_user.Text = Session["uName"].ToString();
            BindGV();
            BindBusDept();
        }
    }
    protected void txtNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Purchase/RP_purchaseDetbyld.aspx?type=new");
    }
    private void BindGV()
    {
        DataTable dt = SelectPurchaseMstrList();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable SelectPurchaseMstrList()
    {
        string str = "sp_RP_SelectPurchaseMstrListbyld";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@plant", ddlPlant.SelectedValue.ToString());
        param[1] = new SqlParameter("@busDept", ddlBusDept.SelectedValue.ToString());
        param[2] = new SqlParameter("@rp_no", txt_no.Text.Trim());
        param[3] = new SqlParameter("@qad", txt_qad.Text.Trim());
        param[4] = new SqlParameter("@user", txt_user.Text.Trim());
        param[5] = new SqlParameter("@dept", txt_userdept.Text.Trim());
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {//定义参数
        if (e.CommandName.ToString() == "ViewEdit")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            Response.Redirect("../Purchase/RP_purchaseDetbyld.aspx?type=det&ID=" + gv.DataKeys[index].Values["ID"].ToString().Trim()
                                                                          + "&no=" + gv.DataKeys[index].Values["rp_No"].ToString().Trim()
                                                                          + "&deptid=" + gv.DataKeys[index].Values["rp_BusinessDept"].ToString().Trim()
                                                                            );
        }
        if (e.CommandName == "Detial")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string _ID = gv.DataKeys[intRow].Values["ID"].ToString();
            /*
            string _leaderIsAgree = gv.DataKeys[index].Values["supplier_LeaderIsAgree"].ToString();
            //有签名则不能编辑
            if (_leaderIsAgree != "0")
            {
                //ltlAlert.Text = "alert('主管已签字不能修改！');";
                //return;
                Response.Redirect("supplier_newApply1.aspx?type=edit&no=" + _no);
            }
            else
            {
                Response.Redirect("supplier_newApply1.aspx?type=edit&no=" + _no);
            }*/
            Response.Redirect("../Purchase/rp_purchaseMstrDetial.aspx?type=edit&no=" + gv.DataKeys[intRow].Values["rp_No"].ToString() +
                                                                "&ID=" + gv.DataKeys[intRow].Values["ID"].ToString() +
                                                                "&busDept=" + gv.DataKeys[intRow].Values["rp_BusinessDept"].ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGV();
    }
    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBusDept();
    }
    private void BindBusDept()
    {
        DataTable dt = SelectBusDept();

        ddlBusDept.DataSource = dt;
        ddlBusDept.DataBind();

        ddlBusDept.Items.Insert(0, new ListItem("--全部--", "0"));
    }
    private DataTable SelectBusDept()
    {
        string sql = "select departmentid,departmentname from RP_department Where plantcode = " + ddlPlant.SelectedValue.ToString() + "";
        //SqlParameter[] param = new SqlParameter[10];
        //param[0] = new SqlParameter("@plant", ddlPlant.SelectedValue.ToString());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["rp_plantCode"]) == 1)
            {
                e.Row.Cells[0].Text = "SZX";
            }
            else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["rp_plantCode"]) == 2)
            {
                e.Row.Cells[0].Text = "ZQL";
            }
            else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["rp_plantCode"]) == 5)
            {
                e.Row.Cells[0].Text = "YQL";
            }
            else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["rp_plantCode"]) == 8)
            {
                e.Row.Cells[0].Text = "HQL";
            }

           
        }
    }

    protected void gv_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGV();
    }
}