using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
public partial class Purchase_RP_personMstr : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // getType();
            BindGridView();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {

    }
    protected void btnnew_Click(object sender, EventArgs e)
    {

    }
    protected override void BindGridView()
    {
        try
        {
            string strName = "sp_RP_selectRPdepartment";
            SqlParameter[] param = new SqlParameter[6];


            param[0] = new SqlParameter("@plantcode", dropPlant.SelectedValue.ToString());

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    public void getType()
    {
        try
        {
            string strName = "sp_RP_selectdept";
            SqlParameter[] param = new SqlParameter[5];


            param[0] = new SqlParameter("@plantcode", dropPlant.SelectedValue.ToString());
            ddlStatu.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, param).Tables[0];
            ddlStatu.DataBind();
            //ddlStatu.Items.Insert(0, new ListItem("--All--", "0"));
            //ddlStatu.SelectedIndex = 0;

        }
        catch (Exception ex)
        {

        }
    }
    protected void dropPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        getType();
    }
}