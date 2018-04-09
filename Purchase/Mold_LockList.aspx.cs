using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Purchase_Mold_LockList : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnFree.Attributes.Add("onclick", "return confirm('您将对勾选的模具进行解锁操作，您确认操作吗？')");
            bind();
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }

    private void bind()
    {
        string QAD = txtQAD.Text.Trim();
        string vender = txtVender.Text.Trim();
        string venderName = txtvenderName.Text.Trim();
        string moldMstrName = txtMoldMstrName.Text.Trim();
        string moldDetName = txtMoldDetName.Text.Trim();


        string sqlstr = "sp_mold_selectMoldLockList";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@QAD",QAD)
            , new SqlParameter("@vender",vender)
            , new SqlParameter("@venderName",venderName)
            , new SqlParameter("@moldMstrName",moldMstrName)
            , new SqlParameter("@moldDetName",moldDetName)
        
        };

        gvDet.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
        gvDet.DataBind();

    }
    protected void btnFree_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable("toFreeList");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Mold_ID";
        table.Columns.Add(column);

        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            CheckBox chk = gvRow.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {

                row = table.NewRow();
                row["Mold_ID"] = gvDet.DataKeys[gvRow.RowIndex].Values["detID"].ToString();

                table.Rows.Add(row);
            }
        }

        if (table.Rows.Count > 0)
        {
            
            if (this.toFreeList(table, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                
                this.Alert("解锁成功");
                bind();
            }
            else
            {
                this.Alert("解锁失败，请重试");
            }
        }
        else
        {
            this.Alert("请选择数据!");
            
        }
    }

    private bool toFreeList(DataTable table, string uID, string uName)
    {
        StringWriter writer = new StringWriter();
        table.WriteXml(writer);
        string xmlDetail = writer.ToString();


        string sqlstr = "sp_mold_updateMoldDetToFree";

        SqlParameter[] param = new SqlParameter[] { 
            new SqlParameter("@xml",xmlDetail )
            , new SqlParameter("@uID",uID)
            , new SqlParameter("@uName",uName)
        
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn,CommandType.StoredProcedure,sqlstr,param));


    }
    protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnSelectQADDOC")
        {

            string molddetID = e.CommandArgument.ToString();

            string url = "../Purchase/Mold_DocList.aspx?molddetID=" + molddetID;

            Response.Redirect(url);

            //ltlAlert.Text = "$.window('文档列表', '70%', '80%','" + url + "', '', true);";
        }
    }
    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}