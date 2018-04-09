using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WHInfo;
using System.Text;
using System.IO;

public partial class WareHouse_Wh_unpRct : BasePage
{
    WareHouse WH = new WareHouse();
    adamClass adam = new adamClass();
    private DataTable Source
    {
        get
        {
            if (ViewState["SplitLine"] == null)
            {
                DataTable dt = new DataTable("SplitLine");
                DataColumn TempColumn;

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.Int32");
                TempColumn.ColumnName = "wh_mstrID";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "rowNum";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wh_part";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wh_desc";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wh_pt_um";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wh_pt_loc";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wh_demandQty";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wh_actualQty";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wh_remark";
                dt.Columns.Add(TempColumn);

                ViewState["SplitLine"] = dt;
            }
            return ViewState["SplitLine"] as DataTable;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 默认值初始化
            //申请人
            txt_applyer.Text = Session["uName"].ToString();
            txt_applyer.ReadOnly = true;
            //地点绑定
            ddl_site.SelectedValue = Session["Plantcode"].ToString();
            //ddl_site.Enabled = false;
            //项目类型（会计科目）
            ddl_projType.DataSource = BindProjType();
            ddl_projType.DataBind();
            ddl_projType.Items.Insert(0, new ListItem("--", "0"));
            //部门绑定
            ddl_departmentBind();
            lbl_flag.Text = "0";
            #endregion

            //展示gridview
            hd_domain.Value = Company();
            DataTable dt = GvBind();
            if (dt.Rows.Count == 0)
            {
                //BindEmptySource();
                BindGVHarmful();
            }
            else
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            BindSite();
            txt_date.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            gv.Columns[6].Visible = false;
        }
    }

    private void BindGVHarmful()
    {
        string _hramfluxml = @"<Harmful><SOP><rowNum>999</rowNum><wh_mstrID></wh_mstrID><wh_part></wh_part><wh_desc></wh_desc><wh_pt_um></wh_pt_um><ddlbind></ddlbind><wh_pt_loc></wh_pt_loc><wh_demandQty></wh_demandQty><wh_actualQty></wh_actualQty><wh_remark></wh_remark></SOP></Harmful>";

        DataSet dsharmful = new DataSet();
        try
        {
            string _xmlmainharmful = _hramfluxml;
            byte[] buffer = Encoding.UTF8.GetBytes(_xmlmainharmful);
            dsharmful.ReadXml(new MemoryStream(buffer));
        }
        catch
        {
            string _xmlmainharmful = _hramfluxml;
            byte[] buffer = Encoding.UTF8.GetBytes(_xmlmainharmful);
            dsharmful.ReadXml(new MemoryStream(buffer));
        }
        this.gv.DataSource = dsharmful.Tables[0];
        this.gv.DataBind();
        gv.Rows[0].Visible = false;
    }
    protected DataTable BindProjType()
    {
        try
        {
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_unp_selectProjType").Tables[0];
        }
        catch
        {
            return null;
        }

    }

    /// <summary>
    /// 绑定地点信息
    /// </summary>
    private void BindSite()
    {
        ddl_site.Items.Clear();
        ddl_site.Items.Add("--请选地点--");
        try
        {
            String strSQL = "";
            strSQL = "sp_wh_selectSiteByUnplss";
            SqlParameter[] parma = new SqlParameter[1];
            parma[0] = new SqlParameter("@Domain", hd_domain.Value);
            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddl_site.Items.Add(new ListItem(reader["si_site"].ToString(), reader["si_site"].ToString()));
                }
                reader.Close();
            }
        }
        catch
        {
            this.Alert("获取地点失败,请联系管理员");
        }
    }
    protected string Company()
    {
        string domain = "";
        if (Session["Plantcode"].ToString() == "1")
        {
            domain = "SZX";
        }
        if (Session["Plantcode"].ToString() == "2")
        {
            domain = "ZQL";
        }
        if (Session["Plantcode"].ToString() == "5")
        {
            domain = "YQL";
        }
        if (Session["Plantcode"].ToString() == "8")
        {
            domain = "HQL";
        }
        return domain;
    }
    protected void BindAppNo()
    {
        SqlParameter[] param1 = new SqlParameter[5];
        param1[0] = new SqlParameter("@WH_EmpNo", Session["uID"].ToString());
        param1[1] = new SqlParameter("@WH_EmpName", Session["uName"].ToString());
        param1[2] = new SqlParameter("@WH_plantcode", Session["plantcode"].ToString());
        param1[3] = new SqlParameter("@WH_projType", ddl_projType.SelectedValue);
        param1[4] = new SqlParameter("@retValue", SqlDbType.VarChar, 30);
        param1[4].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_wh_CreateAppNoByUnplss", param1);
        txt_no.Text = param1[4].Value.ToString().Trim();//dt1.Rows[0]["train_AppNo"].ToString();
    }
    protected void ddl_departmentBind()
    {
        ListItem item;
        DataTable dtDropDept = WH.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                ddl_department.Items.Add(item);
            }
        }
        ddl_department.SelectedValue = Session["deptID"].ToString();
    }
    protected DataTable GvBind()
    {
        string sql = "sp_wh_selectunpRct";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@wh_nbr", txt_no.Text.Trim());
        param[1] = new SqlParameter("@domain", hd_domain.Value);
        param[2] = new SqlParameter("@site", Convert.ToInt32(ddl_site.SelectedValue));

        try
        {
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sql, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    private void BindEmptySource()
    {
        DataTable dt = new DataTable();
        DataColumn TempColumn;

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.Int32");
        TempColumn.ColumnName = "wh_mstrID";
        dt.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "rowNum";
        dt.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "wh_part";
        dt.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "wh_desc";
        dt.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "wh_pt_um";
        dt.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "wh_pt_loc";
        dt.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "wh_demandQty";
        dt.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "wh_actualQty";
        dt.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "wh_remark";
        dt.Columns.Add(TempColumn);

        dt.Rows.Add(dt.NewRow());

        gv.DataSource = dt;
        gv.DataBind();

        gv.Rows[0].Cells.Clear();
        gv.Rows[0].Cells.Add(new TableCell());
        gv.Rows[0].Cells[0].ColumnSpan = 8;
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intRow = e.RowIndex;
        string part = gv.DataKeys[intRow].Values["wh_part"].ToString();
        int wh_mstrID = Convert.ToInt32(lbl_flag.Text);

        string sql = "sp_wh_DeleteUnpRct";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@wh_mstrID", wh_mstrID);
        param[1] = new SqlParameter("@wh_part", part);

        try
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql, param);
        }
        catch
        {
            this.Alert("删除失败！");
        }
        Source.Rows.RemoveAt(e.RowIndex);
        if (gv.Rows.Count <= 1)
        {
            BindGVHarmful();
        }
        else
        {
            gv.DataSource = Source;
            gv.DataBind();
        }
    }
    protected SqlDataReader ddlbind()
    {
        return WH.UnPlssGetUM(hd_domain.Value);
    }
    protected string CheckIsOk(string part, string loc, string um, string demandqty, string actualQty)
    {
        string err = "";
        if (ddl_site.SelectedIndex == 0)
        {
            err += "请选择一个地点!\\n";
        }
        if (string.IsNullOrEmpty(txt_no.Text))
        {
            err += "单据号不能为空!\\n";
        }
        if (ddl_projType.SelectedIndex == 0)
        {
            err += "请选择一个项目类型!\\n";
        }
        if (string.IsNullOrEmpty(txt_date.Text))
        {
            err += "出库日期不能为空!\\n";
        }
        try
        {
            DateTime time = Convert.ToDateTime(txt_date.Text.Trim());
        }
        catch
        {
            err += "出库日期格式不正确!\\n";
        }
        if (string.IsNullOrEmpty(txt_reason.Text.Trim()))
        {
            err += "事由不能为空\\n";
        }
        if (WH.UnPlssCheckPart(part, hd_domain.Value) <= 0)
        {
            err += "物料编码不存在，请重新填写!\\n";
        }
        if (um == "--")
        {
            err += "请选择单位!\\n";
        }
        if (WH.UnPlssCheckLoc(loc, hd_domain.Value, ddl_site.SelectedValue) <= 0)
        {
            err += "所填库位不存在!\\n";
        }
        try
        {
            decimal demandqty1 = Convert.ToDecimal(demandqty);
        }
        catch
        {
            err += "应收数量格式不正确!\\n";
        }
        if (actualQty != "0")
        {
            err += "实收数量必选为空!\\n";
        }
        return err;

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddLine")
        {
            #region 取得gridview添加的数据
            string rowNbr = ((TextBox)gv.FooterRow.FindControl("txtLine")).Text.Trim();
            string part = ((TextBox)gv.FooterRow.FindControl("txtPart")).Text.Trim();
            string desc = ((TextBox)gv.FooterRow.FindControl("txtDesc")).Text.Trim();
            string um = ((DropDownList)gv.FooterRow.FindControl("ddl_um")).Text.Trim();
            string loc = ((TextBox)gv.FooterRow.FindControl("txtloc")).Text.Trim();
            //应收数量
            string demandqty = ((TextBox)gv.FooterRow.FindControl("txtdemandQty")).Text.Trim();
            //实收数量
            string actualQty = ((TextBox)gv.FooterRow.FindControl("txtactualQty")).Text.Trim();
            if (string.IsNullOrEmpty(actualQty))
            {
                actualQty = "0";
            }
            string remark = ((TextBox)gv.FooterRow.FindControl("txtremark")).Text.Trim();
            #endregion

            foreach (GridViewRow row1 in gv.Rows)
            {
                if (gv.DataKeys[row1.RowIndex]["rowNum"].ToString() == rowNbr)
                {
                    Alert("序号不能重复！");
                    return;
                }
            }
            string err = CheckIsOk(part, loc, um, demandqty, actualQty);
            if (err != "")
            {
                Alert(err);
                return;
            }

            #region 在gridview添加数据
            DataRow row = Source.NewRow();
            row["wh_part"] = part;
            row["wh_desc"] = desc;
            row["wh_pt_um"] = um;
            row["wh_pt_loc"] = loc;
            row["wh_demandQty"] = demandqty;
            row["wh_actualQty"] = actualQty;
            row["wh_remark"] = remark;
            row["rowNum"] = rowNbr;

            try
            {
                Source.Rows.Add(row);
                gv.DataSource = Source;
                gv.DataBind();
            }
            catch
            {
                return;
            }
            #endregion
            #region 在数据库中添加数据
            try
            {
                string sql = "sp_wh_insertUnpRct";

                SqlParameter[] param = new SqlParameter[22];
                param[0] = new SqlParameter("@wh_nbr", txt_no.Text.Trim());
                param[1] = new SqlParameter("@wh_departmentName", ddl_department.SelectedItem.ToString());
                param[2] = new SqlParameter("@wh_departmentID", Convert.ToInt32(ddl_department.SelectedValue));
                param[3] = new SqlParameter("@wh_domain", hd_domain.Value);
                param[4] = new SqlParameter("@wh_site", Convert.ToInt32(ddl_site.SelectedValue));
                param[5] = new SqlParameter("@wh_type", "UNP-RCT");
                param[6] = new SqlParameter("@wh_projName", ddl_projType.SelectedItem.ToString());
                param[7] = new SqlParameter("@wh_Date", Convert.ToDateTime(txt_date.Text));
                param[8] = new SqlParameter("@createBy", Convert.ToInt32(Session["uID"]));
                param[9] = new SqlParameter("@wh_pt_um", um);
                param[10] = new SqlParameter("@wh_pt_loc", loc);
                param[11] = new SqlParameter("@wh_demandQty", Convert.ToDecimal(demandqty));
                param[12] = new SqlParameter("@wh_actualQty", Convert.ToDecimal(actualQty));
                param[13] = new SqlParameter("@wh_remark", remark);
                if (desc.Length > 24)
                {
                    param[14] = new SqlParameter("@wh_desc1", desc.Substring(0, 23));
                    param[15] = new SqlParameter("@wh_desc2", desc.Substring(23));
                }

                else
                {
                    param[14] = new SqlParameter("@wh_desc1", desc.Substring(0, desc.Length));
                    param[15] = new SqlParameter("@wh_desc2", string.Empty);
                }
                param[16] = new SqlParameter("@wh_part", part);
                param[17] = new SqlParameter("@wh_projID", ddl_projType.SelectedValue.ToString());
                param[18] = new SqlParameter("@createName", Session["uName"].ToString());
                param[19] = new SqlParameter("@id", SqlDbType.Int);
                param[19].Direction = ParameterDirection.Output;
                param[20] = new SqlParameter("@flag", Convert.ToInt32(lbl_flag.Text));
                param[21] = new SqlParameter("@reason", txt_reason.Text.Trim());

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql, param);
                if (param[19].Value.ToString().Length > 0)
                lbl_flag.Text = param[19].Value.ToString();
            }
            catch
            {
                //删除source中的该行数据
                Source.Rows.Remove(row);
                gv.DataSource = Source;
                gv.DataBind();
                this.Alert("添加失败！");
            }
            ddl_site.Enabled = false;
            ddl_projType.Enabled = false;
            txt_reason.ReadOnly = true;
            #endregion
        }
    }
    protected void btn_continue_Click(object sender, EventArgs e)
    {
        this.Redirect("Wh_unpRct.aspx");
    }
    protected void ddl_projType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_projType.SelectedIndex != 0)
        {
            BindAppNo();
        }
        else
        {
            txt_no.Text = "";
        }
    }
    protected void btn_exportexcel_Click(object sender, EventArgs e)
    {
        string DepartmentID = ddl_department.SelectedValue;//领用部门
        string DepartmentName = ddl_department.SelectedItem.Text.Trim();//领用部门
        string Site = ddl_site.SelectedValue;//地点
        string No = txt_no.Text.Trim();
        string ProjTypeID = ddl_projType.SelectedValue;
        string ProjTypeName = ddl_projType.SelectedItem.Text.Trim(); ;
        string Date = txt_date.Text.Trim();
        string Applyer = txt_applyer.Text.Trim();//创建询价单人姓名
        string stroutFile = "pc_inquiry_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        WH.createUNPrct(stroutFile, DepartmentID, DepartmentName, Site, No, ProjTypeID, ProjTypeName, Date, Applyer, Convert.ToInt32(Session["uID"]));
        ltlAlert.Text = "window.open('/Excel/" + stroutFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string err = "";
        if (string.IsNullOrEmpty(txt_no.Text.Trim()))
        {
            err += "单据号不能为空\\n";
        }
        if (string.IsNullOrEmpty(txt_reason.Text.Trim()))
        {
            err += "事由不能为空\\n";
        }
        if (gv.Rows.Count <= 1 && string.IsNullOrEmpty(gv.DataKeys[0]["wh_part"].ToString()))
        {
            err += "无需要提交的信息!\\n";
        }
        if (!string.IsNullOrEmpty(err))
        {
            this.Alert(err);
            return;
        }
        int flag = WH.SubmitApp(txt_no.Text.Trim(), Session["plantcode"].ToString(), "UNP-RCT");
        if (flag < 0)
        {
            this.Alert("提交失败，请联系管理员!");
        }
        else
        {
            btn_submit.Enabled = false;
            gv.Columns[8].Visible = false;    
            this.Alert("提交成功!");
        }
        
    }
}