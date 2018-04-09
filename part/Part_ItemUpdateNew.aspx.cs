using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using CommClass;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OEAppServer;
using System.Configuration;

public partial class part_Part_ItemUpdateNew : BasePage
{
    private adamClass adam = new adamClass();
    adamClass chk = new adamClass();
    String strConn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.Conn9"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //trApply.Visible = false;
            txt_nbr.ReadOnly = true;
            txt_status.ReadOnly = true;
            if (Request.QueryString["id"] != null)
            {
                lbl_id.Text = Request.QueryString["id"];


                BindMstrData();
            }
            else
            {
                trApply.Visible = false;
                btn_Confirm.Visible = false;
                btn_Reject.Visible = false;
                txtReason.Enabled = false;
            }

        }
    }
    private void BindMstrData()
    {
        IDataReader reader = GetProductStruApplyMstr(lbl_id.Text);
        if (reader.Read())
        {
            txt_nbr.Text = reader["nbr"].ToString();
            txt_status.Text = reader["status1"].ToString();
            //txt_prodCode.Text = reader["ProdCode"].ToString();
            txtReason.Text = reader["Reason"].ToString();
            txtRmks.Text = reader["remark"].ToString();
            lbl_Status.Text = reader["status"].ToString();
            hid_CreatedBy.Value = reader["createby"].ToString();
            BindDetailData();
        }
        reader.Close();
        if (lbl_Status.Text == "10" && this.Security["190705004"].isValid)
        {
            trApply.Visible = false;
            btn_Confirm.Visible = true;
            btn_Reject.Visible = true;
            txtReason.Enabled = true;
        }
        else if (lbl_Status.Text == "0" || lbl_Status.Text == "-20")
        {
            trApply.Visible = true;
            btn_Confirm.Visible = false;
            btn_Reject.Visible = false;
            txtReason.Enabled = false;
        }
        else
        {
            trApply.Visible = false;
            btn_Confirm.Visible = false;
            btn_Reject.Visible = false;
            txtReason.Enabled = false;
        }
    }
    public SqlDataReader GetProductStruApplyMstr(string id)
    {
        try
        {
            string strName = "sp_Part_selectItemUpdateMstrbyid";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
    }
    private void BindDetailData()
    {
        gv_product.DataSource = GetProductStruApplyDetail(lbl_id.Text);
        gv_product.DataBind();
    }
    public string SubmitProductStruApplyMstr(string id, string userId, string userName, string status, string mid, string reason)
    {
        string strName = "sp_part_submitItemUpdate";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);

        parm[2] = new SqlParameter("@userId", userId);
        parm[3] = new SqlParameter("@userName", userName);
        parm[4] = new SqlParameter("@mid", mid);
        parm[5] = new SqlParameter("@status", status);
        parm[6] = new SqlParameter("@reason", reason);
        //SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        string result = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        //if (result == 0)
        //{
        //    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique");
        //}
        return result;
    }
    public DataTable GetProductStruApplyDetail(string id)
    {
        try
        {
            string strName = "sp_Part_selectItemUpdatedet";
                SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {

    }
    protected void gv_product_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_product.PageIndex = e.NewPageIndex;
        //BindGridView();
        BindDetailData();
    }
    protected void gv_product_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gv_product_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (lbl_Status.Text != "0" && lbl_Status.Text != "-20")
            {

                e.Row.Cells[9].Text = string.Empty;
                if (e.Row.Cells[1].Text != e.Row.Cells[2].Text)
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                }
                if (e.Row.Cells[3].Text != e.Row.Cells[4].Text)
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                }
                if (e.Row.Cells[5].Text != e.Row.Cells[6].Text)
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                }
                if (e.Row.Cells[7].Text != e.Row.Cells[8].Text)
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void gv_product_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id", gv_product.DataKeys[e.RowIndex].Values["id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_part_deleteitemupdatedet", param);
        }
        catch
        {
            ltlAlert.Text = "alert('删除失败!');";
            BindMstrData();
        }

        BindMstrData();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        string res = SubmitProductStruApplyMstr(lbl_id.Text, Session["uID"].ToString(), Session["uName"].ToString(), "10", lbl_id.Text, txtReason.Text);
        if (res == "0")
        {
            this.Alert("提交成功！");
            BindMstrData();
            setEmailpass();
            //this.Redirect("RP_ldList.aspx");
        }

        else
        {
            this.Alert(res + "有正在进行的修改描述，请等BOM组处理！");
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        string res = SubmitProductStruApplyMstr(lbl_id.Text, Session["uID"].ToString(), Session["uName"].ToString(), "-10", lbl_id.Text, txtReason.Text);
        if (res == "0")
        {
            this.Alert("提交成功！");
            BindMstrData();
            //this.Redirect("RP_ldList.aspx");
        }

        else
        {
            this.Alert("失败！");
        }
    }
    protected void btnget_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        this.Redirect("Part_ItemUpdateList.aspx?rt=" + DateTime.Now.ToFileTime().ToString() + "&status=" + Request.QueryString["status"] + "&byself=" + Request.QueryString["byself"]);

    }
    protected void btn_Confirm_Click(object sender, EventArgs e)
    {




        if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString(), Session["plantCode"].ToString(), lbl_id.Text))
        {
            // ltlAlert.Text = "alert('success!');";WebResource.axd


            string res = SubmitProductStruApplyMstr(lbl_id.Text, Session["uID"].ToString(), Session["uName"].ToString(), "20", lbl_id.Text, txtReason.Text);
            if (res == "0")
            {
                AppServer app = new AppServer();
                if (app.upptmstr(Session["uID"].ToString()))
                {
                    this.Alert("确认成功！");
                    setEmail();
                }
                else
                {
                    this.Alert("AppServer调用失败！");
                }
            }

            else
            {
                this.Alert("数据更新失败！");
            }
            BindMstrData();
        }
        else
        {
            ltlAlert.Text = "alert('fail!');";
        }
        BindMstrData();
    }
    private bool InsertBatchTemp(string uID, string uName, string plantCode, string mid)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@plantCode", plantCode);
            sqlParam[3] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[3].Direction = ParameterDirection.Output;
            sqlParam[4] = new SqlParameter("@mid", mid);
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_part_insertItemUpdatefinish", sqlParam);

            return Convert.ToBoolean(sqlParam[3].Value);
        }
        catch
        {
            return false;
        }

    }
    public bool setEmail()
    {
        try
        {
            string sql = "SELECT us.email FROM dbo.Part_updateItemMstr mstr " +
            "LEFT JOIN dbo.Users us ON mstr.createby = us.userID " +
            "WHERE mstr.id ='" + lbl_id.Text+"'";
            DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                //string createBy = dt.Rows[row]["createBy"].ToString().Trim();
                string email = dt.Rows[row]["email"].ToString().Trim();
                email += ";luosaihong@" + baseDomain.Domain[0]+";yaoping@" + baseDomain.Domain[0];
                if (email != string.Empty)
                {
                    sql = "SELECT  qad,part,partNEW,describe,describeNEW,desc1,desc1NEW,desc2,desc2NEW FROM  dbo.Part_updateItemDet det " +
                    "LEFT  JOIN dbo.Part_updateItemMstr mstr ON det.Mid = mstr.id " +
                    "WHERE mstr.id ='" + lbl_id.Text+"'";
                    dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
                    string body = "物料修改已完成:<br/>";
                    body += "<table border='1' cellspacing='0' cellpadding='0'>" +
                    "<tr><th>QAD</th><th>物料号</th><th>物料号新</th><th>描述</th><th>描述新</th><th>描述1</th><th>描述1新</th><th>描述2</th><th>描述2新</th></tr>";

                    //    N'</table>'	
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        body += "<tr><th>" + dt.Rows[i]["qad"].ToString().Trim() + "</th><th>" + dt.Rows[i]["part"].ToString().Trim() + "</th><th>" + dt.Rows[i]["partNEW"].ToString().Trim() + "</th><th>" + dt.Rows[i]["describe"].ToString().Trim() + "</th><th>" + dt.Rows[i]["describeNEW"].ToString().Trim() + "</th>"+
                            "<th>" + dt.Rows[i]["desc1"].ToString().Trim() + "</th><th>" + dt.Rows[i]["desc1NEW"].ToString().Trim() + "</th><th>" + dt.Rows[i]["desc2"].ToString().Trim() + "</th><th>" + dt.Rows[i]["desc2NEW"].ToString().Trim() + "</th></tr>";
                    }
                    body += "</table>";
                    this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), email, "", "物料修改已完成--" + txt_nbr.Text.ToString().Trim(), body);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool setEmailpass()
    {
        try
        {
            string sql = "SELECT us.email FROM dbo.Part_updateItemMstr mstr " +
            "LEFT JOIN dbo.Users us ON mstr.createby = us.userID " +
            "WHERE mstr.id ='" + lbl_id.Text + "'";
            DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                //string createBy = dt.Rows[row]["createBy"].ToString().Trim();
                string email = dt.Rows[row]["email"].ToString().Trim();
                email += "luosaihong@" + baseDomain.Domain[0]+";yaoping@" +baseDomain.Domain[0];
                if (email != string.Empty)
                {
                    sql = "SELECT  qad,part,partNEW,describe,describeNEW,desc1,desc1NEW,desc2,desc2NEW FROM  dbo.Part_updateItemDet det " +
                    "LEFT  JOIN dbo.Part_updateItemMstr mstr ON det.Mid = mstr.id " +
                    "WHERE mstr.id ='" + lbl_id.Text + "'";
                    dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
                    string body = "有新的物料修改需要你确认:<br/>";
                    body += "<table border='1' cellspacing='0' cellpadding='0'>" +
                    "<tr><th>QAD</th><th>物料号</th><th>物料号新</th><th>描述</th><th>描述新</th><th>描述1</th><th>描述1新</th><th>描述2</th><th>描述2新</th></tr>";

                    //    N'</table>'	
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        body += "<tr><th>" + dt.Rows[i]["qad"].ToString().Trim() + "</th><th>" + dt.Rows[i]["part"].ToString().Trim() + "</th><th>" + dt.Rows[i]["partNEW"].ToString().Trim() + "</th><th>" + dt.Rows[i]["describe"].ToString().Trim() + "</th><th>" + dt.Rows[i]["describeNEW"].ToString().Trim() + "</th>" +
                            "<th>" + dt.Rows[i]["desc1"].ToString().Trim() + "</th><th>" + dt.Rows[i]["desc1NEW"].ToString().Trim() + "</th><th>" + dt.Rows[i]["desc2"].ToString().Trim() + "</th><th>" + dt.Rows[i]["desc2NEW"].ToString().Trim() + "</th></tr>";
                    }
                    body += "</table>";
                    this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), email, "", "有新的物料修改需要你确认", body);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void btn_Reject_Click(object sender, EventArgs e)
    {
        if (txtReason.Text == "")
        {
            this.Alert("驳回原因不能为空！");
            return;
        }
        string res = SubmitProductStruApplyMstr(lbl_id.Text, Session["uID"].ToString(), Session["uName"].ToString(), "-20", lbl_id.Text, txtReason.Text);
        if (res == "0")
        {
            this.Alert("提交成功！");
            BindMstrData();
            //this.Redirect("RP_ldList.aspx");
        }

        else
        {
            this.Alert("失败！");
        }

        BindMstrData();
    }
    protected void likbtn_Click(object sender, EventArgs e)
    {

    }
    protected void btnRouting_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }
        string stautus = lbl_Status.Text == "" ? "0" : lbl_Status.Text;
        if (stautus != "0")
        {
            ltlAlert.Text = "alert('The app have submit!');";
            return;
        }
        if (ImportExcelFile())
        {
            if (CheckValidity(Session["uID"].ToString()))
            {
                string mid = InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString(), txtRmks.Text.Trim());

                if (mid != "")
                {


                    lbl_id.Text = mid;
                    ltlAlert.Text = "alert('success!');";
                    BindMstrData();
                }
                else
                {
                    ltlAlert.Text = "alert('fail!');";
                }
            }
            else
            {
                string title = "100^<b>QAD</b>~^100^<b>物料号</b>~^100^<b>描述</b>~^100^<b>QAD描述1 </b>~^100^<b>QAD描述2</b>~^100^<b>错误信息</b>~^";

                string sql = " 	SELECT QAD,part,describe,desc1,desc2,errMsg	from Part_ItemsUpdateTemp where createBy =" + Session["uID"].ToString();

                DataTable dt = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
                //ltlAlert.Text = "alert('导入失败!');";
                ExportExcel(title, dt, false);

            }
        }
    }
    private string InsertBatchTemp(string uID, string uName, string remark)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@remark", remark);
            sqlParam[3] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[3].Direction = ParameterDirection.Output;
            if (lbl_id.Text != "")
            {
                sqlParam[4] = new SqlParameter("@mid", lbl_id.Text);
            }
            return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_part_insertItemUpdate", sqlParam).ToString();

            //return Convert.ToBoolean(sqlParam[3].Value);
        }
        catch
        {
            return "";
        }

    }
    protected bool CheckValidity(string uID)
    {
        try
        {
            string strSql = "sp_part_checkItemsUpdate";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

            return Convert.ToBoolean(sqlParam[1].Value);
        }
        catch
        {
            return false;
        }
    }
    public Boolean ImportExcelFile()
    {
        DataTable dt;
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;

        #region 上传文档例行处理
        strCatFolder = Server.MapPath("/import");

        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('Fail to upload file.');";
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('Please select a file.');";
            return false;
        }

        strUserFileName = strFileName;

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
        #endregion

        if (filename1.PostedFile != null)
        {
            string error = "";
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum upload file is 8 MB.');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('Failed to upload file.');";
                return false;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    dt = this.GetExcelContents(strFileName); //chk.getExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('Import file must be in Excel format.');";
                    return false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    /*
                     *  导入的Excel文件必须满足：
                     *      1、至少应该有五列
                     *      2、从第五列开始即视为工序
                     *      3、工序名称必须在wo2_mop中存在
                    */

                    if (dt.Columns.Count != 5)
                    {
                        dt.Reset();

                        ltlAlert.Text = "alert('The file must have 5 columns！');";

                        return false;
                    }

                    #region Excel的列名必须保持一致
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "QAD")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD！');";
                            return false;
                        }
                        if (col == 1 && dt.Columns[col].ColumnName.Trim() != "物料号")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 物料号！');";
                            return false;
                        }

                        if (col == 2 && dt.Columns[col].ColumnName.Trim() != "描述")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 描述！');";
                            return false;
                        }

                        if (col == 3 && dt.Columns[col].ColumnName.Trim() != "QAD描述1")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD描述1 ！');";
                            return false;
                        }

                        if (col == 4 && dt.Columns[col].ColumnName.Trim() != "QAD描述2")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD描述2！');";
                            return false;
                        }


                    }
                    #endregion

                    //转换成模板格式
                    DataTable table = new DataTable("temp");
                    DataColumn column;
                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "QAD";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "part";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "describe";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "desc1";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "desc2";
                    table.Columns.Add(column);



                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createBy";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "createname";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);

                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);

                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r[0].ToString().Trim() != string.Empty)
                            {
                                row = table.NewRow();

                                #region 赋值、长度判定

                                if (r[0].ToString().Trim() == string.Empty)
                                {
                                    error += "QAD号不能为空；";
                                }
                                else
                                {
                                    row["QAD"] = r[0].ToString().Trim();
                                }


                                row["part"] = r[1].ToString().Trim();


                                //custPo的长度允许最长20个字符，否则截取

                                row["describe"] = r[2].ToString().Trim();
                                //if (r[3].ToString().Length > 24)
                                if (System.Text.Encoding.GetEncoding("GB2312").GetByteCount(r[3].ToString()) > 24)
                                {
                                    error += "描述1不能超过24个字符；";
                                }
                                else
                                {
                                    row["desc1"] = r[3].ToString().Trim();
                                }
                                if (System.Text.Encoding.GetEncoding("GB2312").GetByteCount(r[4].ToString()) > 24)
                                {
                                    error += "描述2不能超过24个字符；";
                                }
                                else
                                {

                                    row["desc2"] = r[4].ToString().Trim();

                                }




                                #endregion

                                row["createBy"] = _uID;
                                row["createname"] = Session["uName"].ToString();
                                if (error == "")
                                {
                                    row["errMsg"] = string.Empty;
                                }
                                else
                                {
                                    row["errMsg"] = error;
                                }

                                table.Rows.Add(row);
                            }
                        }

                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                            {
                                bulkCopy.DestinationTableName = "dbo.Part_ItemsUpdateTemp";
                                bulkCopy.ColumnMappings.Add("QAD", "QAD");
                                bulkCopy.ColumnMappings.Add("part", "part");
                                bulkCopy.ColumnMappings.Add("describe", "describe");
                                bulkCopy.ColumnMappings.Add("desc1", "desc1");
                                bulkCopy.ColumnMappings.Add("desc2", "desc2");
                                bulkCopy.ColumnMappings.Add("createBy", "createBy");
                                bulkCopy.ColumnMappings.Add("createname", "createname");
                                bulkCopy.ColumnMappings.Add("errMsg", "errMsg");




                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('Operation fails!Please try again!');";

                                    return false;
                                }
                                finally
                                {
                                    table.Dispose();
                                }
                            }
                        }
                    }
                }

                dt.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        return true;
    }
    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_Part_clearItemsUpdateTemp", param);

            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        DataTable errDt = GetProductStruApplyDetailexcel();
        string title = "100^<b>QAD</b>~^100^<b>物料号</b>~^100^<b>描述</b>~^100^<b>QAD描述1</b>~^100^<b>QAD描述2</b>~^";
        if (errDt != null && errDt.Rows.Count > 0)
        {
            ExportExcel(title, errDt, false);
        }
    }
    public DataTable GetProductStruApplyDetailexcel()
    {
        try
        {
            string strName = "sp_part_selectitempartbyany";
            SqlParameter[] parm = new SqlParameter[5];

            string status = "";
            if (radStop.Checked == true)
            { status = "2"; }
            else if (radTry.Checked == true)
            {
                status = "1";
            }
            else if (radNormal.Checked == true)
            {
                status = "0";
            }

            parm[0] = new SqlParameter("@status", status);
            parm[1] = new SqlParameter("@code", txtPartCode.Text.Trim().ToLower().Replace("*", "%"));
            parm[2] = new SqlParameter("@item_qad", txtQad.Text.Trim().ToLower().Replace("*", "%"));
            parm[3] = new SqlParameter("@name", txtCategory.Text.Trim().ToLower().Replace("*", "%"));
            parm[4] = new SqlParameter("@description", txtDesc.Text.Trim().ToLower());

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
}