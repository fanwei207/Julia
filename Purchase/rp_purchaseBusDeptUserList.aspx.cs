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
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
public partial class Purchase_rp_purchaseBusDeptUserList : System.Web.UI.Page
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
            hidPrice.Value = "0.00000";
        }

    }
    private void BindGV()
    {
        DataTable dt = pur.SelectPurchaseDet(Request["ID"].ToString());
        gv.DataSource = dt;
        gv.DataBind();
    }
    public bool CheckChinese(string str)
    {
        bool flag = false;
        UnicodeEncoding a = new UnicodeEncoding();
        byte[] b = a.GetBytes(str);
        int i = 0;
        for (i = 0; i < b.Length; i++)
        {
            i++;
            if (b[i] != 0)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
        }
        if (i > 4)
        {
            flag = true;
        }
        return flag;
    }
    protected void ddlUm_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (sender as DropDownList);
        TextBox tb = ddl.Parent.FindControl("txtUmOther") as TextBox;
        if (ddl != null && ddl.SelectedValue.ToString() != "其他")
        {
            tb.Visible = false;
        }
        else
        {
            tb.Visible = true;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //2016年9月7号杨洋要求取消此检验，暂注释
        //if (pur.SelectIsExistsQADFromDet("det", "bus", hidGvID.Value, Request["ID"].ToString(), txtQAD.Text.Trim(), Session["uID"].ToString()))
        //{
        //    ltlAlert.Text = "alert('物料已存在，请重新填写'); ";
        //    return;
        //}
        if (UpdatePurchaseDetByID())
        {
            ltlAlert.Text = "alert('更新失败，请联系管理员！'); ";
            return;
        }
        BindGV();
        hidGvID.Value = string.Empty;
        txtQAD.Text = string.Empty;
        txtVend.Text = string.Empty;
        txtVendName.Text = string.Empty;
        txtUmTemp.Text = string.Empty;
        txtQtyTemp.Text = string.Empty;
        txtPtUm.Text = string.Empty;
        txtUm.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtPrice.Text = string.Empty;
        txtQADDesc1.Text = string.Empty;
        txtQADDesc2.Text = string.Empty;
        txtUses.Text = string.Empty;
        txtDescript.Text = string.Empty;
        txtFormat.Text = string.Empty;
        ddlUm.Items.Clear();
    }
    private bool UpdatePurchaseDetByID()
    {
        string str = "sp_rp_UpdatePurchaseDetByID";
        SqlParameter[] param = new SqlParameter[18];
        param[0] = new SqlParameter("@qad", txtQAD.Text.Trim());
        param[1] = new SqlParameter("@vend", txtVend.Text.Trim());
        param[2] = new SqlParameter("@vendname", txtVendName.Text.Trim());
        param[3] = new SqlParameter("@ptum", hidPtUm.Value.ToString());
        param[4] = new SqlParameter("@um", hidUm.Value.ToString());
        param[5] = new SqlParameter("@price", hidPrice.Value.ToString());
        param[6] = new SqlParameter("@qaddesc1", hidDesc1.Value.ToString());
        param[7] = new SqlParameter("@qaddesc2", hidDesc2.Value.ToString());
        param[8] = new SqlParameter("@format", txtFormat.Text.Trim());
        param[9] = new SqlParameter("@ID", hidGvID.Value.ToString());
        param[10] = new SqlParameter("@qty", txtQty.Text.Trim());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
}