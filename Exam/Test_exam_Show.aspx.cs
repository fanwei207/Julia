using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;

using System.Timers;

public partial class Test_Test_exam_Show : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    //static string id;
    public static System.Timers.Timer gtimer = null;  // 定义全局定时器类对象 
    public static int gcount = 0;  //  测试用计时器变量 
    protected void Page_Load(object sender, EventArgs e)
    {
        
            if (!IsPostBack)
            {
                lblid.Text = "E965A990-F0DB-4F0B-BECD-1B45EEF3BBA2";
                BindGridView();
                second.Text = "100";

              
                

            }
        
    }

  
    protected override void BindGridView()
    {
        try
        {
            string strName = "sp_test_selectexamshow";
            SqlParameter[] param = new SqlParameter[2];


            param[0] = new SqlParameter("@id", lblid.Text);
               
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void gvMessagereply_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvMessagereply_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ques_id = gv.DataKeys[e.Row.RowIndex].Values["ques_id"].ToString();
            string typeid = gv.DataKeys[e.Row.RowIndex].Values["ques_type_id"].ToString();
            if (typeid.ToLower() == "0056C16C-75A0-4022-AEEF-252B4DC2490C".ToLower())
            {
                RadioButtonList radType = ((RadioButtonList)e.Row.FindControl("radType"));

                string strName = "sp_test_selectquesbyid";
                SqlParameter[] param = new SqlParameter[2];


                param[0] = new SqlParameter("@id", ques_id);
               
                radType.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName,param).Tables[0];
                radType.DataBind();
            }
            if (typeid.ToLower() == "054FFE45-697B-4001-AE54-918154669C6B".ToLower())
            {
                    CheckBoxList ckbtype = ((CheckBoxList)e.Row.FindControl("ckbtype"));

                string strName = "sp_test_selectquesbyid";
                SqlParameter[] param = new SqlParameter[2];


                param[0] = new SqlParameter("@id", ques_id);

                ckbtype.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
                ckbtype.DataBind();
            }
          

        }
    }
    
    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@careteby", uID);

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_deletequestemp", param);

            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ClearTemp(Convert.ToInt32(Session["uID"]));
        DataTable table = new DataTable("temp");
        DataColumn column;
        DataRow row;

       
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ques_id";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "exam_id";
        table.Columns.Add(column);

        //column = new DataColumn();
        //column.DataType = System.Type.GetType("System.String");
        //column.ColumnName = "type_id";
        //table.Columns.Add(column);

        //column = new DataColumn();
        //column.DataType = System.Type.GetType("System.String");
        //column.ColumnName = "cate_id";
        //table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "answer";
        table.Columns.Add(column);
      

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "createdBy";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "scores";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "errMsg";
        table.Columns.Add(column);

      

        for (int i = 0; i < gv.Rows.Count; i++)
        {
            string typeid = gv.DataKeys[i].Values["ques_type_id"].ToString();
            string quesid = gv.DataKeys[i].Values["ques_id"].ToString();
            row = table.NewRow();
            if (gv.DataKeys[i].Values["scores"].ToString().Length > 10)
            {
                row["scores"] = gv.DataKeys[i].Values["scores"].ToString().Substring(0, 10);
            }
            else
            {
                row["scores"] = gv.DataKeys[i].Values["scores"].ToString();
            }
            row["ques_id"]=quesid;
            row["exam_id"] = lblid.Text;
            row["createdBy"] = Convert.ToInt32(Session["uID"]);
            //CheckBox cb = (CheckBox)gv.Rows[i].FindControl("chk_Select");
            if (typeid.ToLower() == "0056C16C-75A0-4022-AEEF-252B4DC2490C".ToLower())
            {
                RadioButtonList radType = ((RadioButtonList)gv.Rows[i].FindControl("radType"));
                row["answer"] = radType.SelectedValue.ToString();
               
            }
            if (typeid.ToLower() == "054FFE45-697B-4001-AE54-918154669C6B".ToLower())
            {
                CheckBoxList ckbtype = ((CheckBoxList)gv.Rows[i].FindControl("ckbtype"));
                string save_cblJL = "";   
                for (int j = 0; j < ckbtype.Items.Count; j++)
                {

                    if (ckbtype.Items[j].Selected == true)
                    {

                        save_cblJL += ckbtype.Items[j].Value;
                        row["answer"] = save_cblJL;

                    }

                }  

            }
            table.Rows.Add(row);
            
        }

        if (table != null && table.Rows.Count > 0)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strConn))
            {
                bulkCopy.DestinationTableName = "dbo.Test_answer_temp";
                bulkCopy.ColumnMappings.Add("ques_id", "ques_id");
                bulkCopy.ColumnMappings.Add("answer", "answer");
                bulkCopy.ColumnMappings.Add("createdBy", "createdBy");

                bulkCopy.ColumnMappings.Add("exam_id", "exam_id");
                bulkCopy.ColumnMappings.Add("scores", "scores");
                try
                {
                    bulkCopy.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    this.Alert("Operation fails!Please try again!");


                }
                finally
                {
                    table.Dispose();
                }
            }
        }


        try
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@uid", Session["uID"].ToString());
            param[1] = new SqlParameter("@createname", Session["uName"].ToString());
            param[2] = new SqlParameter("@exam_id", lblid.Text);

            param[3] = new SqlParameter("@retValue", SqlDbType.Int);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@retid", SqlDbType.NChar, 50);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@time", "0");

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_insertscores", param);

            if (Convert.ToInt32(param[3].Value) > 0)
            {
                if (param[4].Value.ToString().Trim() != "")
                {
                    this.Redirect("Test_Finally.aspx?mark_id=" + param[4].Value.ToString().Trim() + "&rt=" + DateTime.Now.ToFileTime().ToString());
                    //lblid.Text = param[4].Value.ToString().Trim();
                }

                //txtDetReqDate.Text = txtReqDate.Text.Trim();
                //txtDetDueDate.Text = txtDueDate.Text.Trim();




                //BindGridView();
            }
            else
            {
                //ltlAlert.Text = "alert('Operation fails!Please try again!');";
                //return;
            }
        }
        catch
        {
            //ltlAlert.Text = "alert('Operation fails!Please try again!');";
            //return;
        }
        //if (cb.Checked)
        //{
        //   // strSelect = strSelect + gvSID.DataKeys[i].Value.ToString() + ",";
        //}
    }

   
}