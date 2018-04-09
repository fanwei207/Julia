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

public partial class Purchase_rp_purchaseDet : BasePage
{
    adamClass adam = new adamClass();
    public string _fpath
    {
        get
        {
            return ViewState["fpath"].ToString();
        }
        set
        {
            ViewState["fpath"] = value;
        }
    }
    public string _fname
    {
        get
        {
            return ViewState["fname"].ToString();
        }
        set
        {
            ViewState["fname"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            hidPlant.Value = Session["plantCode"].ToString();
            
          
                BindGV();
            
           
        }
    }
   
   
   
    protected void btnNew_Click(object sender, EventArgs e)
    {
       
    }
   
    private DataTable selectBusDept()
    {
        string sql = "Select departmentid,departmentname From RP_department Where plantcode = " + Session["plantCode"].ToString() + " And isproc = 0";

        return SqlHelper.ExecuteDataset(adam.dsn0(),  CommandType.Text, sql).Tables[0]; ;
    }
  
    private void BindGV()
    {
        DataTable dt = SelectPurchaseDet();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable SelectPurchaseDet()
    {
        string str = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
       
            str = "sp_rp_SelectPurchaseDet";
            param[0] = new SqlParameter("@ID", Request["ID"]);
        

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string ID = gv.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", ID);
        string str = string.Empty;
        if (Request["type"].ToString() == "new")
        {
            str = "sp_rp_DeletePurchaseDetTemp";
        }
        else if (Request["type"].ToString() == "det")
        {
            str = "sp_rp_DeletePurchaseDet";
        }
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

        BindGV();
    }
  
   
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Purchase/RP_purchaseMstrListbyld.aspx");
    }






    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string site = "";
        if (Session["PlantCode"].ToString() == "1")
        {
            site = "1000";
        }
        else if (Session["PlantCode"].ToString() == "2")
        {
            site = "2000";
        }
        else if (Session["PlantCode"].ToString() == "5")
        {
            site = "4000";
        }
        else if (Session["PlantCode"].ToString() == "8")
        {
            site = "5000";
        }
        int status = 0;
        string id = "0";
        foreach (GridViewRow row in gv.Rows)
        {
            System.Web.UI.WebControls.CheckBox chkSinger = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkSinger");
            System.Web.UI.WebControls.TextBox txt_prd_qty_dev = (System.Web.UI.WebControls.TextBox)row.FindControl("txt_prd_qty_dev");
            

            if (chkSinger.Checked)
            {
                if (status == 0)
                {
                    id = AddProductStruApplyMstr("采购申请生成领料单", Session["uID"].ToString(), Session["uName"].ToString(), Request["ID"].ToString());
                status = 1;
                }
                if (id != "0")
                {
                    SqlParameter[] sqlParam = new SqlParameter[10];
                    sqlParam[0] = new SqlParameter("@uID", Session["uID"].ToString());
                    sqlParam[1] = new SqlParameter("@uName", Session["uName"].ToString());
                    sqlParam[3] = new SqlParameter("@mstr_id", id);
                    sqlParam[4] = new SqlParameter("@line", "");
                    sqlParam[5] = new SqlParameter("@qad", gv.DataKeys[row.RowIndex]["rp_QAD"].ToString());
                    sqlParam[6] = new SqlParameter("@num", txt_prd_qty_dev.Text);
                    sqlParam[7] = new SqlParameter("@site", site);
                    sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
                    sqlParam[2].Direction = ParameterDirection.Output;
                    sqlParam[8] = new SqlParameter("@purdetid", gv.DataKeys[row.RowIndex]["ID"].ToString());
                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_insertlddetlinebypur", sqlParam);
                }
                else
                {
                    this.Alert("申请保存失败");
                    return;
                }
               
                //PassPrhDet(Session["uID"].ToString(), Session["uName"].ToString(), lblDelivery.Text.Trim(), dtgList.DataKeys[row.RowIndex]["prd_line"].ToString(), "1");




            }

        }
        if (id != "0")
        {
            this.Redirect("RP_ldNew.aspx?id=" + id + "&rt=" + DateTime.Now.ToFileTime().ToString());
            //BindData();
        }
        else
        {
            this.Alert("请勾选需要生成领料单的行");
        }
    }
    public string AddProductStruApplyMstr(string desc, string userId, string userName, string ID)
    {
        try
        {
            string strName = "sp_RP_Insertldmstrbypur";
            SqlParameter[] parm = new SqlParameter[6];

            parm[1] = new SqlParameter("@desc", desc);
            parm[2] = new SqlParameter("@userId", userId);
            parm[3] = new SqlParameter("@userName", userName);
            parm[4] = new SqlParameter("@purID", ID);
            parm[5] = new SqlParameter("@plantcode", Session["PlantCode"]);
            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }
}