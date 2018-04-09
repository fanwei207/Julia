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

public partial class Purchase_rp_purchaseListDet : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtVend.Text = Request["vend"].ToString();
            ddlStatus.SelectedValue = Request["type"].ToString();
            BindGV();
            try
            {
                string strSQL = " SELECT  RP_vend FROM RP_webVend where RP_vend = '" + Request["vend"].ToString() + "'";

                string vend = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSQL).ToString();
                if (vend == Request["vend"].ToString())
                {
                   
                    ddlStatus.Items.Add(new ListItem("已生成网购单","2"));
                    ddlStatus.Items.Add(new ListItem("未生成网购单", "3"));
                }
                else
                {
                    btnNewWeb.Visible = false;
                }
            }
            catch (Exception)
            {

                btnNewWeb.Visible = false;
            }
        }
    }

    private void BindGV()
    {
        //if (ddlStatus.SelectedValue == "0")
        //{
        //    btnNewPur.Visible = true;
        //    btnNewWeb.Visible = false;
        //}
        //else
        //{
        //    btnNewPur.Visible = false;
        //}
        if (ddlStatus.SelectedValue == "3")
        {
            btnNewWeb.Visible = true;
            btnNewPur.Visible = false;
        }
        else
        {
            btnNewWeb.Visible = false;
        }
        DataTable dt = SelectPurchaseDet();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable SelectPurchaseDet()
    {
        string str = "sp_rp_SelectPurchaseListDet";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@vend", txtVend.Text);
        param[1] = new SqlParameter("@status", ddlStatus.SelectedValue.ToString());
        param[2] = new SqlParameter("@domain", Request["domain"].ToString());
        param[3] = new SqlParameter("@QAD", txtQAD.Text.Trim());
        param[4] = new SqlParameter("@purNbr", txtPurNbr.Text.Trim());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGV();
        chkAll.Checked = false;
    }
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
        string nbr = ((TextBox)gv.Rows[e.RowIndex].Cells[9].FindControl("txtNbr")).Text.ToString().Trim();
        string line = ((TextBox)gv.Rows[e.RowIndex].Cells[10].FindControl("txtLine")).Text.ToString().Trim();
        string ID = gv.DataKeys[e.RowIndex].Values["ID"].ToString().Trim();

        string str = "sp_rp_UpdatePurchaseList";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@ID", ID);
        param[1] = new SqlParameter("@nbr", nbr);
        param[2] = new SqlParameter("@line", line);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, param);

        gv.EditIndex = -1;
        BindGV();
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gv.Rows[i].FindControl("chk_Select");
           
            if (chkAll.Checked && cb.Enabled)
            {
                cb.Checked = true;
            }
            else
            {
                cb.Checked = false;
            }
        }
    }
    protected void btnNewPur_Click(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue.ToString() == "1")
        {
            ltlAlert.Text = "alert('已采购的不允许生成采购单'); ";
            return;
        }
        #region 保存头栏
        try
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@domain", Request["domain"].ToString());
            param[1] = new SqlParameter("@vend", txtVend.Text);
            param[2] = new SqlParameter("@retValue", SqlDbType.Int);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@id", SqlDbType.Int);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_rp_InsertListDetXXMstr", param);

            if (Convert.ToInt32(param[2].Value) <= 0)
            {
                ltlAlert.Text = "alert('头栏保存失败1，请联系管理员'); ";
                return;
            }
            hidXXMstrID.Value = param[3].Value.ToString();
        }
        catch
        {
            ltlAlert.Text = "alert('头栏保存失败2，请联系管理员'); ";
            return;
        }
        #endregion

        #region 删除临时表
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", Convert.ToString(Session["uID"]));
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_rp_DeleteListDetXXTemp", param);
        }
        catch
        {
            ltlAlert.Text = "alert('删除临时表失败！请联系管理员！'); ";
            return;
        }
        #endregion

        #region 创建存放数据源的表procOutput
        DataTable procOutput = new DataTable("reporterOutput");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Guid");
        column.ColumnName = "ID";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "XXMstrID";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "domain";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "site";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "line";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "part";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qty";
        procOutput.Columns.Add(column); 

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "rpstatus";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "um";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "uID";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "uName";
        procOutput.Columns.Add(column);

        #endregion

        #region 为数据源表procOutput赋值
        int line = 0;
        foreach (GridViewRow gvRow in gv.Rows)
        {
            int i = gvRow.RowIndex;
            
            string plant = string.Empty;
            int domain = 0;
            CheckBox chkSel = (CheckBox)gvRow.FindControl("chk_Select");
            if (!chkSel.Checked)
            {
                continue;
            }
            row = procOutput.NewRow();
            line += 1;
            switch(Request["domain"].ToString())
            {
                case "1": plant = "SZX";
                    domain = 1000;
                    break;
                case "2": plant = "ZQL";
                    domain = 2000;
                    break;
                case "5": plant = "YQL";
                    domain = 4000;
                    break;
                case "8": plant = "HQL";
                    domain = 5000;
                    break;
            }
            row["ID"] = gv.DataKeys[gvRow.RowIndex].Values["ID"].ToString();
            row["XXMstrID"] = hidXXMstrID.Value.ToString();
            row["domain"] = plant;
            row["site"] = domain.ToString();
            row["line"] = line.ToString();
            row["part"] = gv.DataKeys[gvRow.RowIndex].Values["rp_QAD"].ToString();
            row["qty"] = gv.DataKeys[gvRow.RowIndex].Values["qty"].ToString();
            row["uID"] = Session["uID"].ToString();
            row["uName"] = Session["uName"].ToString();
            row["rpstatus"] = "1";
            row["um"] = gv.DataKeys[gvRow.RowIndex].Values["rp_Um"].ToString();
            procOutput.Rows.Add(row);
        }
        #endregion

        #region 将明细导入临时表
        if (procOutput != null && procOutput.Rows.Count > 0)
        {
            using (SqlBulkCopy bulckCopy = new SqlBulkCopy(adam.dsn0()))
            {
                bulckCopy.DestinationTableName = "tcpc0.dbo.rp_purchaseListDetXXTemp";
                bulckCopy.ColumnMappings.Add("ID", "ID");
                bulckCopy.ColumnMappings.Add("XXMstrID", "XXMstrID");
                bulckCopy.ColumnMappings.Add("domain", "XXdomain");
                bulckCopy.ColumnMappings.Add("site", "XXsite");
                bulckCopy.ColumnMappings.Add("line", "XXline");
                bulckCopy.ColumnMappings.Add("part", "XXpart");
                bulckCopy.ColumnMappings.Add("qty", "XXqty");
                bulckCopy.ColumnMappings.Add("rpstatus", "rpstatus");
                bulckCopy.ColumnMappings.Add("um", "XXUm");

                bulckCopy.ColumnMappings.Add("uID", "createBy");
                bulckCopy.ColumnMappings.Add("uName", "createName");

                try
                {
                    bulckCopy.WriteToServer(procOutput);

                    #region 导入到数据库
                    try
                    {
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@MID", hidXXMstrID.Value);
                        param[1] = new SqlParameter("@uID", Session["uID"].ToString());
                        param[2] = new SqlParameter("@retValue", SqlDbType.Int);
                        param[2].Direction = ParameterDirection.Output;

                        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_rp_InsertListDetXXDet", param);

                        if (Convert.ToBoolean(param[2].Value))
                        {
                            ltlAlert.Text = "alert('保存成功'); ";
                        }
                        else
                        {
                            ltlAlert.Text = "alert('保存明细失败！请联系管理员'); ";
                            return;
                        }
                    }
                    catch
                    {

                    }
                    #endregion
                }
                catch
                {
                    ltlAlert.Text = "alert('同步明细数据失败'); ";
                }
                finally
                {
                    procOutput.Dispose();
                }
            }
        }
        #endregion

        //QadService.WebService1SoapClient re = new QadService.WebService1SoapClient();
        //re.PoNbr_LOAD_Submit();
        BindGV();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindGV();
        chkAll.Checked = false;
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGV();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlStatus.SelectedValue.ToString() != "0" && ddlStatus.SelectedValue.ToString() != "3")
            {

                ((CheckBox)e.Row.FindControl("chk_Select")).Enabled=false;

            }
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewEdit")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _ID = gv.DataKeys[index].Values["ID"].ToString();
            string _MID = gv.DataKeys[index].Values["MID"].ToString();
            Response.Redirect("../Purchase/rp_purchaseMstrDetial.aspx?type=listdet&ID=" + _MID + "&DID=" + _ID +

            "&vend=" + Request["vend"].ToString() + "&types=" + Request["type"].ToString() + "&domain=" + Request["domain"].ToString());
        }
    }
    protected void btnNewWeb_Click(object sender, EventArgs e)
    {

       

       
        #region 保存头栏
        try
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@domain", Request["domain"].ToString());
            param[1] = new SqlParameter("@vend", txtVend.Text);
            param[2] = new SqlParameter("@retValue", SqlDbType.Int);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@uname", Session["uName"].ToString());
            param[5] = new SqlParameter("@uid", Session["uID"].ToString());
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_rp_Insertwebmstr", param);

            if (Convert.ToInt32(param[2].Value) <= 0)
            {
                ltlAlert.Text = "alert('头栏保存失败1，请联系管理员'); ";
                return;
            }
            hidXXMstrID.Value = param[3].Value.ToString();
        }
        catch
        {
            ltlAlert.Text = "alert('头栏保存失败2，请联系管理员'); ";
            return;
        }
        #endregion

        #region 删除临时表
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", Convert.ToString(Session["uID"]));
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_rp_DeleteWEBTemp", param);
        }
        catch
        {
            ltlAlert.Text = "alert('删除临时表失败！请联系管理员！'); ";
            return;
        }
        #endregion

        #region 创建存放数据源的表procOutput
        DataTable procOutput = new DataTable("reporterOutput");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Guid");
        column.ColumnName = "ID";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Guid");
        column.ColumnName = "MID";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "rp_domain";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "rp_site";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "rp_QADDesc1";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "rp_QADDesc2";
        procOutput.Columns.Add(column);
       

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "rp_QAD";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "rp_Qty";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "rp_Price";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "rp_Um";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "rp_uid";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "rp_uname";
        procOutput.Columns.Add(column);

        #endregion

        #region 为数据源表procOutput赋值
        int line = 0;
        foreach (GridViewRow gvRow in gv.Rows)
        {
            int i = gvRow.RowIndex;

            string plant = string.Empty;
            int domain = 0;
            CheckBox chkSel = (CheckBox)gvRow.FindControl("chk_Select");
            if (!chkSel.Checked)
            {
                continue;
            }
            row = procOutput.NewRow();
            line += 1;
            switch (Request["domain"].ToString())
            {
                case "1": plant = "SZX";
                    domain = 1000;
                    break;
                case "2": plant = "ZQL";
                    domain = 2000;
                    break;
                case "5": plant = "YQL";
                    domain = 4000;
                    break;
                case "8": plant = "HQL";
                    domain = 5000;
                    break;
            }
            row["ID"] = gv.DataKeys[gvRow.RowIndex].Values["ID"].ToString();
            row["MID"] = hidXXMstrID.Value.ToString();
            row["rp_domain"] = plant;
            row["rp_site"] = domain.ToString();
            //row["line"] = line.ToString();
            row["rp_QAD"] = gv.DataKeys[gvRow.RowIndex].Values["rp_QAD"].ToString();
            row["rp_Qty"] = gv.DataKeys[gvRow.RowIndex].Values["qty"].ToString();
            row["rp_uid"] = Session["uID"].ToString();
            row["rp_uname"] = Session["uName"].ToString();
            //row["rpstatus"] = "1";
            row["rp_Um"] = gv.DataKeys[gvRow.RowIndex].Values["rp_Um"].ToString();
            row["rp_Price"] = gv.DataKeys[gvRow.RowIndex].Values["rp_price"].ToString();
            row["rp_QADDesc1"] = gv.DataKeys[gvRow.RowIndex].Values["rp_QADDesc1"].ToString();
            row["rp_QADDesc2"] = gv.DataKeys[gvRow.RowIndex].Values["rp_QADDesc2"].ToString();
            procOutput.Rows.Add(row);
        }
        #endregion

        #region 将明细导入临时表
        if (procOutput != null && procOutput.Rows.Count > 0)
        {
            using (SqlBulkCopy bulckCopy = new SqlBulkCopy(adam.dsn0()))
            {
                bulckCopy.DestinationTableName = "tcpc0.dbo.RP_webdetTemp";
                bulckCopy.ColumnMappings.Add("ID", "ID");
                bulckCopy.ColumnMappings.Add("MID", "MID");
                bulckCopy.ColumnMappings.Add("rp_domain", "rp_domain");
                bulckCopy.ColumnMappings.Add("rp_site", "rp_site");
                bulckCopy.ColumnMappings.Add("rp_QAD", "rp_QAD");
                bulckCopy.ColumnMappings.Add("rp_Qty", "rp_Qty");
                bulckCopy.ColumnMappings.Add("rp_uid", "rp_uid");
                bulckCopy.ColumnMappings.Add("rp_uname", "rp_uname");
                bulckCopy.ColumnMappings.Add("rp_Um", "rp_Um");

                bulckCopy.ColumnMappings.Add("rp_Price", "rp_Price");
                bulckCopy.ColumnMappings.Add("rp_QADDesc1", "rp_QADDesc1");
                bulckCopy.ColumnMappings.Add("rp_QADDesc2", "rp_QADDesc2");
                try
                {
                    bulckCopy.WriteToServer(procOutput);

                    #region 导入到数据库
                    try
                    {
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@MID", hidXXMstrID.Value);
                        param[1] = new SqlParameter("@uID", Session["uID"].ToString());
                        param[2] = new SqlParameter("@retValue", SqlDbType.Int);
                        param[2].Direction = ParameterDirection.Output;

                        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_rp_InsertWEBDet", param);

                        if (Convert.ToBoolean(param[2].Value))
                        {
                            ltlAlert.Text = "alert('保存成功'); ";
                            Response.Redirect("RP_WebDETList.aspx?Mid=" + hidXXMstrID.Value + "&rt=" + DateTime.Now.ToFileTime().ToString() + "&Type=new&domain=" + Request["domain"].ToString());
                        }
                        else
                        {
                            ltlAlert.Text = "alert('保存明细失败！请联系管理员'); ";
                            return;
                        }
                    }
                    catch
                    {
                        ltlAlert.Text = "alert('保存明细失败！请联系管理员22'); ";
                        return;
                    }
                    #endregion
                }
                catch
                {
                    ltlAlert.Text = "alert('同步明细数据失败'); ";
                }
                finally
                {
                    procOutput.Dispose();
                }
            }
        }
        #endregion

        //QadService.WebService1SoapClient re = new QadService.WebService1SoapClient();
        //re.PoNbr_LOAD_Submit();
        BindGV();
    }
}