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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using IT;

public partial class IT_Page_configDet : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (PageMakerHelper.ExistsTableDet(Request["pk0"].ToString(), Request["pk1"].ToString()))
            {
                btnSave.Enabled = false;
                Bind("Y");
            }
            else
            {
                Bind("N");
                btnConfig.Enabled = false;
                gv.Columns[7].Visible = false;
                gv.Columns[8].Visible = false;
                gv.Columns[9].Visible = false;
                gv.Columns[10].Visible = false;
                gv.Columns[11].Visible = false;
                txtSaveProc.Enabled = false;
                txtValidateProc.Enabled = false;
            }
            Label1.Text = Request["pk0"].ToString();
            Label2.Text = Request["pk1"].ToString();
            Label3.Text = PageMakerHelper.GetPageID(Request["pk0"].ToString(), Request["pk1"].ToString());
        }
    }
    private void Bind(string status)
    {
        DataTable mstrTable = PageMakerHelper.GetPageMstr(PageMakerHelper.GetPageID(Request["pk0"].ToString(), Request["pk1"].ToString()));
        foreach (DataRow row in mstrTable.Rows)
        {
            txtSaveProc.Text = row["pm_saveProc"].ToString();
            txtValidateProc.Text = row["pm_validateProc"].ToString();
            txtEditProc.Text = row["pm_editProc"].ToString();
            txtDelProc.Text = row["pm_delProc"].ToString();
            txtImportPorc.Text = row["pm_importProc"].ToString();
        }

        DataTable dt = PageMakerHelper.GetTableColDet(Request["pk0"].ToString(), Request["pk1"].ToString(), status);
        gv.DataSource = dt;
        gv.DataBind();
    }
    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        if (PageMakerHelper.ConfigurePageDet(Request["pk0"].ToString(), Request["pk1"].ToString(), Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("页面配置成功！");

            if (PageMakerHelper.ExistsTableDet(Request["pk0"].ToString(), Request["pk1"].ToString()))
            {
                btnSave.Enabled = false;
                Bind("Y");
            }
            else
            {
                Bind("N");
                btnConfig.Enabled = false;
                gv.Columns[7].Visible = false;
                gv.Columns[8].Visible = false;
                gv.Columns[9].Visible = false;
                gv.Columns[10].Visible = false;
                gv.Columns[11].Visible = false;
            }
            return;

        }
        if (PageMakerHelper.ExistsTableDet(Request["pk0"].ToString(), Request["pk1"].ToString()))
        {
            btnSave.Enabled = false;
            Bind("Y");
        }
        else
        {
            Bind("N");
            btnConfig.Enabled = false;
            gv.Columns[7].Visible = false;
            gv.Columns[8].Visible = false;
            gv.Columns[9].Visible = false;
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //CheckBox chkUsers = (CheckBox)FindControl("chkUsers");
        //CheckBox chkUser = (CheckBox)FindControl("chkUser");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gv.DataKeys[e.Row.RowIndex].Values["isnullable"].ToString() == "1")
            {
                e.Row.Cells[5].Text = "Yes";
            }
            else
            {
                e.Row.Cells[5].Text = "No";
            }
            if (e.Row.Cells[3].Text == "0")
            {
                e.Row.Cells[3].Text = "";
            }
            if (e.Row.Cells[4].Text == "0")
            {
                e.Row.Cells[4].Text = "";
            }
            //导入
            if (e.Row.Cells[0].Text.ToLower() == "createby" || e.Row.Cells[0].Text.ToLower() == "createname" || e.Row.Cells[0].Text.ToLower() == "createdate" ||
                e.Row.Cells[0].Text.ToLower() == "modifyby" || e.Row.Cells[0].Text.ToLower() == "modifyname" || e.Row.Cells[0].Text.ToLower() == "modifydate" ||
                e.Row.Cells[4].Text.ToLower() == "1")
            {
                e.Row.Cells[7].Enabled = false;
            }

            DataTable mstrTable = PageMakerHelper.GetPageMstr(PageMakerHelper.GetPageID(Request["pk0"].ToString(), Request["pk1"].ToString()));
            foreach (DataRow row in mstrTable.Rows)
            {
                if (row["pm_isProc"].ToString() != "View")
                {
                    e.Row.Cells[3].Enabled = false;
                }
            }
        }
    }
    /// <summary>
    /// 对目标表的信息进行配置保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConfig_Click(object sender, System.EventArgs e)
    {
        //保存验证的存储过程的名字
        PageMakerHelper.saveDetProc(Label3.Text, txtSaveProc.Text, txtValidateProc.Text, txtDelProc.Text, txtImportPorc.Text, txtEditProc.Text, Session["uID"].ToString(), Session["uName"].ToString());
        DataSet ds = PageMakerHelper.ConfigTableColDet(Request["pk0"].ToString(), Request["pk1"].ToString(), "Y");
        
        #region 创建临时表
        DataTable table = new DataTable("temp");
        DataColumn column;
        DataRow row;

        //添加Guid列
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "pd_pageID";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "pd_colName";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "pd_isImport";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "pd_import_index";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "pd_isExport";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "pd_export_index";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "pd_orderby_index";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "uID";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "uName";
        table.Columns.Add(column);
        
        #endregion
        DataSet dd = new DataSet();

        int i = 0;

        
        foreach (GridViewRow rw in gv.Rows)
        {
            CheckBox chkImport = (CheckBox)rw.FindControl("chkImport");
            CheckBox chkExport = (CheckBox)rw.FindControl("chkExport");
            //TextBox txtImportIndex = (TextBox)rw.FindControl("importIndex");
            TextBox txtExportIndex = (TextBox)rw.FindControl("exportIndex");
            TextBox txtOrderbyIndex = (TextBox)rw.FindControl("orderbyIndex");
            row = table.NewRow();
            //Guid 方便对临时表的操作
            row["pd_pageID"] = Convert.ToString(PageMakerHelper.GetPageID(Request["pk0"].ToString(), Request["pk1"].ToString()));
            row["pd_colName"] = gv.Rows[i].Cells[0].Text;
            if (chkImport.Checked)
            {
                row["pd_isImport"] = "1";
            }
            else
            {
                row["pd_isImport"] = "0";
            }
            row["pd_import_index"] = ((TextBox)gv.Rows[i].FindControl("importIndex")).Text;// "2";// txtImportIndex.Text.ToString();

            if (chkExport.Checked)
            {
                row["pd_isExport"] = "1";
            }
            else
            {
                row["pd_isExport"] = "0";
            }
            row["pd_export_index"] = ((TextBox)gv.Rows[i].FindControl("exportIndex")).Text;// gv.Rows[i].Cells[10].Text;//txtExportIndex.Text.ToString();
            row["pd_orderby_index"] = ((TextBox)gv.Rows[i].FindControl("orderbyIndex")).Text;//txtOrderbyIndex.Text.ToString();
            row["uID"] = Session["uID"].ToString();
            row["uName"] = Session["uName"].ToString();
            table.Rows.Add(row);
            i++;
        }
        
        if (table != null && table.Rows.Count > 0)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strConn))
            {
                bulkCopy.DestinationTableName = "WorkFlow.dbo.Page_configDetTemp";

                bulkCopy.ColumnMappings.Add("pd_pageID", "pageID");
                bulkCopy.ColumnMappings.Add("pd_colName", "colName");
                bulkCopy.ColumnMappings.Add("pd_isImport", "isImport");
                bulkCopy.ColumnMappings.Add("pd_import_index", "importIndex");
                bulkCopy.ColumnMappings.Add("pd_isExport", "isExport");
                bulkCopy.ColumnMappings.Add("pd_export_index", "exportIndex");
                bulkCopy.ColumnMappings.Add("pd_orderby_index", "orderBy");
                bulkCopy.ColumnMappings.Add("uID", "createBy");
                bulkCopy.ColumnMappings.Add("uName", "createName");
                try
                {
                    if (PageMakerHelper.DeletePageDetTemp(Request["pk0"].ToString(), Request["pk1"].ToString(), Session["uID"].ToString()))
                    {
                        bulkCopy.WriteToServer(table);
                    }
                }
                catch (Exception ex)
                {
                    this.Alert("导入临时表有误，请联系管理员！");
                    return;
                }
                finally
                {
                    table.Dispose();
                }
            }
        }
        if (!PageMakerHelper.UpdatePageDet(Request["pk0"].ToString(), Request["pk1"].ToString()))
        {
            this.Alert("更新失败，请联系管理员！");
            return;
        }
        else
        {
            if (PageMakerHelper.ExistsTableDet(Request["pk0"].ToString(), Request["pk1"].ToString()))
            {
                btnSave.Enabled = false;
                Bind("Y");
            }
            else
            {
                Bind("N");
                btnConfig.Enabled = false;
                gv.Columns[7].Visible = false;
                gv.Columns[8].Visible = false;
                gv.Columns[9].Visible = false;
            }
        }
        //dd.Tables.Add(table);
        //UpdateByDataSet(dd, "Page_Det", ConfigurationSettings.AppSettings["SqlConn.Conn_WF"].ToString());
    }
    /// <summary>
    /// 批量更新
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="strTblName"></param>
    /// <param name="strConnection"></param>
    /// <returns></returns>
    public DataSet UpdateByDataSet(DataSet ds,string strTblName,string strConnection)
    {
        //dAdapter.SelectCommand = new OleDbCommand("select * from [StuInfo]", connection);
        //SqlCommandBuilder cb = new OleDbCommandBuilder(dAdapter);
        //dAdapter.Fill(dSet, "StuInfo");
        //dSet.Tables["StuInfo"].Rows[0][1] = "9999";
        //dAdapter.Update(dSet.Tables["StuInfo"]);

        //SqlConnection conn = new SqlConnection(@"Data Source=10.3.80.95;Initial Catalog=WorkFlow;Persist Security Info=True;user id=sa;Password=sa");

        //Data Source=10.3.0.70;Initial Catalog=tcpc0;Persist Security Info=True;user id=sa;Password=sa

        SqlConnection  conn = new SqlConnection(strConnection);        
        SqlDataAdapter myAdapter = new SqlDataAdapter();
        //SqlCommand myCommand = new SqlCommand("select * from " + strTblName, conn);//(("select * from "+strTblName),(SqlConnection) conn);   

        SqlCommand myCommand = new SqlCommand(("select * from [Page_Det]" ), (SqlConnection)conn);  
        myAdapter.SelectCommand = myCommand;
        SqlCommandBuilder myCommandBuilder = new SqlCommandBuilder(myAdapter);    
        try
        {  
                lock(this)//处理并发情况(分布式情况)
                {
                    myAdapter.Fill(ds, strTblName);
                    myAdapter.Update(ds,strTblName); 
                }
        }
        catch(Exception err)
        {
            conn.Close();   
            //throw new BusinessException(err);
        }
        return ds;//数据集的行状态在更新后会都变为: UnChange,在这次更新后客户端要用返回的ds
    }
}