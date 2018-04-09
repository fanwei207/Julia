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
using Purchase;
public partial class Purchase_rp_purchaseEquipment : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    RPPurchase pur = new RPPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidID.Value = Request["ID"].ToString();
            //labNo.Text = Request["no"].ToString();
            BindGV();
        }

    }
    private void BindGV()
    {
        DataTable dt = pur.SelectPurchaseDet(Request["ID"].ToString());
        gv.DataSource = dt;
        gv.DataBind();
    }
    //private DataTable SelectPurchaseDet()
    //{
    //    string str = "sp_rp_SelectPurchaseDet";
    //    SqlParameter[] param = new SqlParameter[2];
    //    param[0] = new SqlParameter("@ID", Request["ID"].ToString());

    //    return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    //}
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindGV();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindGV();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string ID = gv.DataKeys[e.RowIndex].Values["ID"].ToString().Trim();
        string qad = ((TextBox)gv.Rows[e.RowIndex].Cells[0].FindControl("txtQAD")).Text.ToString().Trim();
        //string vend = ((Label)gv.Rows[e.RowIndex].Cells[1].FindControl("txtVend")).Text.ToString().Trim();
        //string vendName = ((Label)gv.Rows[e.RowIndex].Cells[2].FindControl("txtVendName")).Text.ToString().Trim();
        //string um = ((Label)gv.Rows[e.RowIndex].Cells[6].FindControl("txtUm")).Text.ToString().Trim();
        string vend = gv.DataKeys[e.RowIndex].Values["rp_Supplier"].ToString().Trim();
        string vendName = gv.DataKeys[e.RowIndex].Values["rp_SupplierName"].ToString().Trim();
        string um = gv.DataKeys[e.RowIndex].Values["rp_Um"].ToString().Trim();

        if (!pur.SelectIsExistsQADFromDet("det", "equipment", ID,Request["ID"].ToString(), qad, Session["uID"].ToString()))
        {
            ltlAlert.Text = "alert('物料已存在，请重新填写'); ";
            return;
        }
        
        string sql = "";
        sql += "Select top 1 isnull(pc_list,''),isnull(vendname,''),isnull(pc_um,pt_um),pt_um,cast(isnull(pc_price1,0.00000) as varchar),replace(replace(pt_desc1,char(10),N''),char(13),N''),replace(replace(pt_desc2,char(10),N''),char(13),N'')";
        sql += " From tcpc0.dbo.PC_mstr pm ";
        sql += " Right join qad_data.dbo.pt_mstr pt on pm.pc_part = pt.pt_part ";
        sql += " And pm.pc_List = '" + vend + "' ";
        sql += " And pc_um = '" + um + "'";
        sql += " Left join ";
        sql += " ( ";
        sql += "     SELECT ad_addr , MAX(ad_name) vendname ";
        sql += "     FROM QAD_Data.dbo.ad_mstr ";
        sql += "     WHERE ad_type = 'supplier' ";
        sql += "     GROUP BY ad_addr ";
        sql += " ) vend on pm.pc_list = vend.ad_addr";
        sql += " Where ";

        switch (Session["plantCode"].ToString())
        {
            case "1":
                sql += " pt_domain = 'SZX'";
                break;
            case "2":
                sql += " pt_domain = 'ZQL'";
                break;
            case "5":
                sql += " pt_domain = 'YQL'";
                break;
            case "8":
                sql += " pt_domain = 'HQL'";
                break;
        }
        sql += "     And getdate() >= isnull(pc_start,1900-01-01) ";
        sql += "     And (pc_expire is null or (pc_expire is not null And getdate() <= pc_expire)) ";
        sql += "     And pt_part like '" + qad + "%' ";
        //if (vends != "")
        //{
        //    sql += "     And pc_list = '" + vends + "' ";
        //}
        sql += " Order by case when pc_price <= 0.00000 then 99999 else pc_price end asc ";
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];

        if (dt != null || dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                hidPtUm.Value = row[3].ToString();
                hidPrice.Value = row[4].ToString();
                hidDesc1.Value = row[5].ToString();
                hidDesc2.Value = row[6].ToString();
            }
        }
        if(qad == "")
        {
            hidDesc1.Value = "";
            hidDesc2.Value = "";
        }

        if (qad != "")
        {
            if (qad.Length != 14 && qad.Length != 15)
            {
                ltlAlert.Text = "alert('QAD位数不正确！'); ";
                return;
            }
        }
        string str = "sp_rp_UpdateEquipmentDetByID";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@id", ID);
        param[1] = new SqlParameter("@qad", qad);
        param[2] = new SqlParameter("@Ptum", hidPtUm.Value.ToString());
        param[3] = new SqlParameter("@price", hidPrice.Value.ToString());
        param[4] = new SqlParameter("@qadDesc1", hidDesc1.Value.ToString());
        param[5] = new SqlParameter("@qadDesc2",hidDesc2.Value.ToString());

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, param);

        gv.EditIndex = -1;
        BindGV();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //2016年9月7号杨洋要求取消此检验，暂注释
        //if (pur.SelectIsExistsQADFromDet("det", "equipment", hidGvID.Value, Request["ID"].ToString(), txtQAD.Text.Trim(), Session["uID"].ToString()))
        //{
        //    ltlAlert.Text = "alert('物料已存在，请重新填写'); ";
        //    return;
        //}
        if (UpdateEquipmentDetByID())
        {
            ltlAlert.Text = "alert('更新失败，请联系管理员！'); ";
            return;
        }
        BindGV();
        hidGvID.Value = string.Empty;
        txtQAD.Text = string.Empty;
        txtVend.Text = string.Empty;
        txtVendName.Text = string.Empty;
        txtPtUm.Text = string.Empty;
        txtUm.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtPrice.Text = string.Empty;
        txtQADDesc1.Text = string.Empty;
        txtQADDesc2.Text = string.Empty;
        txtUses.Text = string.Empty;
        txtDescript.Text = string.Empty;
        txtFormat.Text = string.Empty;
    }
    private bool UpdateEquipmentDetByID()
    {
        string str = "sp_rp_UpdateEquipmentDetByID";

        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@qad", txtQAD.Text.Trim());
        param[1] = new SqlParameter("@price", hidPrice.Value.ToString());
        param[2] = new SqlParameter("@qaddesc1", hidDesc1.Value.ToString());
        param[3] = new SqlParameter("@qaddesc2", hidDesc2.Value.ToString());
        param[4] = new SqlParameter("@ID", hidGvID.Value.ToString());
        param[5] = new SqlParameter("@vend",hidVend.Value.ToString());
        param[6] = new SqlParameter("@um", hidUm.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));

    }
}