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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class pur_ResultCheckStandard : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    PurResult result = new PurResult();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPro();
            BindTypes();
            txt_version.Text = Request.QueryString["name"];
            txt_flag.Text = Request.QueryString["flag"];
            GridViewBind();
            this.gv.Columns[0].Visible = false;
            //if (txt_flag.Text == "停用" || txt_flag.Text == "启用")
            //{
            //    btn_add.Visible = false;
            //    this.gv.Columns[8].Visible = false;
            //}
        }
    }
    private void BindPro()
    {
        try
        {
            ddl_pro.Items.Clear();
            //ddl_pro.DataSource = result.GetPro();
            ddl_pro.DataSource = result.GetProByTypeid(Request.QueryString["typeid"]);
            ddl_pro.DataBind();
            //ddl_pro.Items.Insert(0, "--请选择--");
            //ddl_pro.SelectedIndex = 0;
        }
        catch
        {
            this.Alert("获取项目名称失败!");
        }
    }
    private void BindTypes()
    {
        try
        {
            ddl_types.Items.Clear();
            string proid = "";
            if (ddl_pro.SelectedIndex == 0)
            {
                proid = "0";
            }
            else
            {
                proid = ddl_pro.SelectedValue;
            }

            //ddl_types.DataSource = result.GetTypeList(proid, "");
            ddl_types.DataSource = result.GetTypeByTypeid(Request.QueryString["typeid"]);
            ddl_types.DataBind();
            //ddl_types.Items.Insert(0, "--请选择--");
            //ddl_types.SelectedIndex = 0;
        }
        catch
        {
            this.Alert("获取类型失败!");
        }
    }

    private void GridViewBind() 
    {
        try
        {
            string version = Request.QueryString["id"];
            string strName = "sp_purresult_selectStandardlist";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@proid", ddl_pro.SelectedValue);
            param[1] = new SqlParameter("@typeid", ddl_types.SelectedValue);
            param[2] = new SqlParameter("@version", version);
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    private bool checkNoteIsExist(string type)
    {
        try
        {
            string strName = "sp_vc_checkVendCompCateIsExist";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@name", type);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        GridViewBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string str = gv.DataKeys[e.Row.RowIndex].Values["pur_valuetype"].ToString();
            if (gv.DataKeys[e.Row.RowIndex].Values["pur_valuetype"].ToString() != string.Empty && gv.DataKeys[e.Row.RowIndex].Values["pur_valuetype"].ToString() != "False")
            {
                gv.Columns[3].Visible = false;
                gv.Columns[4].Visible = false;
                gv.Columns[5].Visible = false;
                gv.Columns[6].Visible = false;
                lb_valuefrom.Text = "考核标准";
                div_valuetype.Visible = false;
                ck_valuetype.Checked = true;
                ck_valuetype.Enabled = false;
            }
            else
            {
                gv.Columns[7].Visible = false;
                lb_valuefrom.Text = "起始值";
                div_valuetype.Visible = true;
            }
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strName = "sp_purresult_deleteValueResult";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", gv.DataKeys[e.RowIndex].Values["pur_id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[3].Value))
            {
                this.Alert("删除成功！");
            }
            else
            {
                this.Alert("删除失败！");
            }
        }
        catch
        {
            this.Alert("删除失败！请联系管理员！"); ;
        }

        GridViewBind();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        GridViewBind();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string _id = gv.DataKeys[e.RowIndex].Values["vcc_id"].ToString();
        TextBox txDesc = (TextBox)gv.Rows[e.RowIndex].FindControl("txDesc");
        if (txDesc.Text.Length == 0)
        {
            this.Alert("罚款科目不能为空！");
            return;
        }

        if (checkNoteIsExist(txDesc.Text))
        {
            ltlAlert.Text = "alert('罚款科目不能重复!');";
            return;
        }

        try
        {
            string strName = "sp_vc_updateVendCompCate";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@id", _id);
            param[1] = new SqlParameter("@name", txDesc.Text);
            param[2] = new SqlParameter("@uID", Session["uID"].ToString());
            param[3] = new SqlParameter("@uName", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[4].Value))
            {
                this.Alert("更新成功！");
            }
            else
            {
                this.Alert("更新失败！");
            }
        }
        catch
        {
            this.Alert("更新失败！请联系管理员！"); ;
        }

        gv.EditIndex = -1;
        GridViewBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {        
        //if (txtName.Text.Trim() == string.Empty) 
        //{
        //    ltlAlert.Text = "alert('罚款科目不能为空!');";
        //    return;
        //}

        //if (checkNoteIsExist(txtName.Text))
        //{
        //    ltlAlert.Text = "alert('罚款科目不能重复!');";
        //    return;
        //}

        try
        {
            string strName = "sp_vc_insertVendCompCate";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@name", ddl_pro.SelectedValue);
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[3].Value))
            {
                this.Alert("新建成功！");
            }
            else
            {
                this.Alert("新建失败！");
            }
        }
        catch
        {
            this.Alert("新建失败！请联系管理员！"); ;
        }

        GridViewBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //BindTypes();
        GridViewBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void ddl_checkpro_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTypes();
        GridViewBind();
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //if (txt_flag.Text == "停用" || txt_flag.Text == "启用")
        //{
        //    this.Alert("已停用或启用的版本不能编辑!");
        //    return;
        //}
        //if (ddl_pro.SelectedIndex == 0)
        //{
        //    this.Alert("请选择考评项目!");
        //    return;
        //}
        //if (ddl_types.SelectedIndex == 0)
        //{
        //    this.Alert("请选择考评类型!");
        //    return;
        //}
        int valuefrom = 0;
        int valueto = 0;
        Decimal valuescore = 0;
        int version = 0;
        string valuescore1 = "";
        string valuetext = "";
        if (!ck_valuetype.Checked)
        {
            try
            {
                valuefrom = Convert.ToInt32(txt_valuefrom.Text.ToString());
            }
            catch
            {
                this.Alert("起始值必须为数字!");
                return;
            }
            try
            {
                valueto = Convert.ToInt32(txt_valueto.Text.ToString());
            }
            catch
            {
                this.Alert("截至值必须为数字!");
                return;
            }
            if (valuefrom > valueto)
            {
                this.Alert("起始值不能大于截止值!");
                return;
            }
            if (valuefrom != valueto && ddl_valueoperator.SelectedItem.Text == "=")
            {
                this.Alert("起始值与截止值不同不能使用 " + "=  !");
                return;
            }
            if (valuefrom == valueto && ddl_valueoperator.SelectedItem.Text != "=")
            {
                this.Alert("起始值与截止值相同只能使用 " + "=  !");
                return;
            }
        }
        else
        {
            valuetext = txt_valuefrom.Text;
        }
        try
        {
            valuescore = Convert.ToDecimal(txt_valuescore.Text.ToString());
        }
        catch
        {
            this.Alert("分值必须为数字!");
            return;
        }
        if (valuescore > Convert.ToDecimal(Request.QueryString["maxvalue"]))
        {
            this.Alert("分值不得大于限定分值!");
            return;
        }
        version = Convert.ToInt32(Request.QueryString["id"]);
        try
        {
            string proid = ddl_pro.SelectedValue;
            string proname = ddl_pro.SelectedItem.ToString();
            string typeid = ddl_types.SelectedValue;
            string typename = ddl_types.SelectedItem.ToString();
            //string valuefrom = txt_valuefrom.Text;
            //string valueStartOperation = ddl_valueStartOperator.SelectedItem.ToString();
            string valueStartOperation = "";
            if (ddl_valueStartOperator.SelectedItem.ToString() == ">")
            {
                valueStartOperation = "<";
            }
            else if (ddl_valueStartOperator.SelectedItem.ToString() == ">=")
            {
                valueStartOperation = "<=";
            }
            else if (ddl_valueStartOperator.SelectedItem.ToString() == "=")
            {
                valueStartOperation = "=";
            }
            string valueoperation = ddl_valueoperator.SelectedItem.ToString();
            //string valueto = txt_valueto.Text;
            //int TotalScore = (int)result.CheckScoreValue(proid, proname, typeid, typename, valuefrom, valueoperation, valueto, valuescore, version);
            bool valuetype = ck_valuetype.Checked;
            //int maxvalue = (int)result.GetMaxScoreValue(proid, proname, typeid, typename, valuefrom, valueoperation, valueto, valuescore, version, valuetype);
            //if (valuescore > maxvalue)
            //{
            //    this.Alert("此类型总分值已超过" + maxvalue + ", 共记" + valuescore + ",总分值需在" + maxvalue + "以内!");
            //    return;
            //}

            bool sysvalue = true;

            int result1 = (int)result.CheckPurListExists(proid, proname, typeid, typename, valuefrom, valueStartOperation, valueoperation, valueto, version, valuetext, valuetype);

            //if (!result.CheckPurListExists(proid, proname, typeid, typename, valuefrom, valueoperation, valueto))
            if (result1 > 0)
            {
                this.Alert("此区间端存在重合记录!");
                return;
            }
            if (!result.SavePurTypeList(proid, proname, typeid, typename, valueStartOperation, valuefrom, valueoperation, valueto, valuescore, sysvalue, Session["uID"].ToString(), Session["uName"].ToString(), version, valuetext, valuetype))
            {
                this.Alert("记录保存失败!");
                return;
            }
            this.Alert("记录保存成功!");
        }
        catch
        {
            this.Alert("记录保存失败!");
            return;
        }
        //div_add.Visible = false;
        //btn_save.Visible = false;
        GridViewBind();

    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        div_add.Visible = true;
        btn_save.Visible = true;
        btn_cancel.Visible = true;

    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        div_add.Visible = false;
        btn_save.Visible = false;
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        string versionid = Request.QueryString["id"];
        string versionname = Request.QueryString["name"];
        string flag = Request.QueryString["flag"];
        Response.Redirect("pur_ResultTypeList.aspx?id=" + versionid + "&name=" + versionname + "&flag=" + flag + "&rm=" + DateTime.Now);


    }
    protected void ck_valuetype_CheckedChanged(object sender, EventArgs e)
    {
        if (ck_valuetype.Checked)
        {
            lb_valuefrom.Text = "考核标准";
            div_valuetype.Visible = false;
            div_Operator.Visible = false;
        }
        else
        {
            lb_valuefrom.Text = "起始值";
            div_valuetype.Visible = true;
            div_Operator.Visible = true;
        }
    }
}
