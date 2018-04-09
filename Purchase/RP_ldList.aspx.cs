using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using CommClass;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_RP_ldList : BasePage
{
    private adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //getType();
        }
    }
    public void getType()
    {
        try
        {
            ddldepartment.Items.Clear();
            //string strSQL = " SELECT [departmentid] , [departmentname] from [RP_department] where [plantcode] = " + Session["PlantCode"] + "";
            string strSQL = " SELECT [departmentid] , [departmentname] from [RP_department] where [plantcode] = " + ddlPlant.SelectedValue + "";
            ddldepartment.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSQL);
            ddldepartment.DataBind();
            ddldepartment.Items.Insert(0, new ListItem("--", "0"));
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected override void BindGridView()
    {

        gvlist.DataSource = GetProductStruApplyList(txt_nbr.Text.Trim(),  ddl_status.SelectedValue);
        gvlist.DataBind();
    }
    public DataTable GetProductStruApplyList(string nbr, string status)
    {
        try
        {
            string strName = "sp_RP_selectldList";
            SqlParameter[] parm = new SqlParameter[10];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@uid", Session["uID"]);  
            if (status != "")
            {
                parm[3] = new SqlParameter("@status", status);
            }
            parm[2] = new SqlParameter("@check", CheckBox1.Checked);
            parm[4] = new SqlParameter("@user", txt_user.Text.Trim());
            parm[5] = new SqlParameter("@depart", ddldepartment.SelectedValue);
            parm[6] = new SqlParameter("@plant", ddlPlant.SelectedValue);  
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void gvlist_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void gvlist_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Confirm")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvlist.DataKeys[index].Values["id"].ToString().Trim();
            string supper="0";
            if (issupper(id))
            {
                supper = "1";
            }
            else
            {
                supper = "0";
            }
            this.Redirect("RP_ldNew.aspx?id=" + id + "&supper=" + supper + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.Redirect("RP_ldNew.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }

    public bool issupper(string id)
    {
        SqlParameter[] sqlParam = new SqlParameter[4];
        sqlParam[0] = new SqlParameter("@uID", Session["uID"].ToString());

        sqlParam[3] = new SqlParameter("@id", id);
        sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
        sqlParam[2].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_checkuspper", sqlParam);

        return Convert.ToBoolean(sqlParam[2].Value);
    }
    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        this.Redirect("RP_purchaseMstrListbyld.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        getType();
    }
}