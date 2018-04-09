using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class price_pc_FinCheckApply : BasePage
{
    private PC_FinCheckApply helper = new PC_FinCheckApply();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                lblApplyId.Text = Request.QueryString["Id"];
            }
            BindApplyData();
            BindApproveData();
        }
    }


    private void BindApplyData()
    {
        string id = lblApplyId.Text;
        DataTable dt;
        DataTable detDt;
        if (id != "")
        {
            dt = helper.GetApply(id); 
            detDt = helper.GetApplyDet(id);
        
           // gvDet.Columns[0].Visible = false;
            DataRow row = dt.Rows[0];
            lblApplyId.Text = row["ApplyId"].ToString();
            lblApplyer.Text = row["Applyer"].ToString();
            lblApplyerEmail.Text = row["ApplyerEmail"].ToString();
            lblAproveDate.Text = row["ApproveDate"] == DBNull.Value ? "" : string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(row["ApproveDate"]));
            txtApplyReason.Text = row["ApplyReason"].ToString();
            btn_ApplySubmit.Visible = false;
            txtApplyReason.Enabled = false;
            if (row["approveby"].ToString() == Session["uID"].ToString())
            {
                btn_approve.Visible = true;
                btn_diaApp.Visible = true;
                approveopinion.Visible = true;

                filename.Visible = true;
                btnUpload.Visible = true;
                btnDownExcel.Visible = true;
            }
            else
            {
                tbApply.Visible = false;
                filename.Visible = false;
                btnUpload.Visible = false;
                btnDownExcel.Visible = false;
            }
            string result = row["ApproveResult"].ToString().ToLower();
            if (result != "")
            {
                chk_isApproved.Checked = true;
                if (result == "true")
                {
                    lbl_ApproveNote.Text = "审批通过";
                }
                else
                {
                    lbl_ApproveNote.Text = "审批未通过";
                }
            }

        }
        else
        {
            string[] parts = Session["FinCheckApply"].ToString().Split(new char[] { ';' });

            DataTable table = new DataTable("Parts");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "part";
            table.Columns.Add(column);
            foreach(string part in parts)
            {
                row = table.NewRow();
                row["part"] = part;
                table.Rows.Add(row);
            }


            detDt = helper.GetApplyDet(table);
            btn_ApplySubmit.Visible = true;
            txtApplyReason.Enabled = true;
            btn_approve.Visible = false;
            btn_diaApp.Visible = false;
        }
        if (!Security["121100021"].isValid)
        {
            gvDet.Columns[11].Visible = false;
            gvDet.Columns[12].Visible = false;
        }
        gvDet.DataSource = detDt;
        gvDet.DataBind();
    }

    private void BindApproveData()
    {
        string applyId = lblApplyId.Text;
        gvApprove.DataSource = helper.GetApprove(applyId);
        gvApprove.DataBind();
    }
    protected void btn_Approver_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('../RDW/rdw_getProjQadApprover.aspx','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btn_ApplySubmit_Click(object sender, EventArgs e)
    {
        if (string.Empty.Equals(txt_approveID.Text.ToString()))
        {
            ltlAlert.Text = "alert('Click the \"选择审批人\"  for choose one Approver,Please!'); ";
            return;
        }
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Apply Reason,Please!'); ";
            return;
        }
        if (txtApproveName.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Select one Approver,Please!'); ";
            return;
        }

        if (chkEmail.Checked)
        {
            if (txt_ApproveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('Enter the Approver Email if you want to send email,Please!'); ";
                return;
            }
        }
        string approver = txt_approveID.Text;
        string applyReason = txtApplyReason.Text.Trim().ToString();
        string uID = Session["uID"].ToString();


        DataTable table = new DataTable("ApplyDet");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "PQDetId";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Price";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "PriceSelf";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "PriceDiscount";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "CheckPrice";
        table.Columns.Add(column);

        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            row = table.NewRow();
            row["PQDetId"] = gvDet.DataKeys[gvRow.RowIndex].Values["PQDetId"].ToString();
            string price = gvDet.DataKeys[gvRow.RowIndex].Values["Price"].ToString();
            if (price != "")
            {
                row["Price"] = price;
            }
            price = gvDet.DataKeys[gvRow.RowIndex].Values["PriceSelf"].ToString();
            if (price != "")
            {
                row["PriceSelf"] = price;
            }
            row["PriceDiscount"] = gvDet.DataKeys[gvRow.RowIndex].Values["PriceDiscount"].ToString();
            price = gvDet.DataKeys[gvRow.RowIndex].Values["CheckPrice"].ToString();
            if (price != "")
            {
                row["CheckPrice"] = price;
            }
            table.Rows.Add(row);
        }
        int applyId = helper.AddApply(approver, applyReason, uID, table);
        if (applyId > 0)
        {
            bool isSuccess = true;
            string message = "";
            lblApplyId.Text = applyId.ToString();

            if (chkEmail.Checked)
            {
                isSuccess = helper.SendMailForApprove(txt_ApproveEmail.Text.Trim(), txtApproveName.Text.Trim(), Session["Email"].ToString(), Session["uName"].ToString(), applyReason, applyId.ToString(), out message);
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Apply successfully!');";

            }
            else
            {
                if (message != "")
                {
                    ltlAlert.Text = "alert('" + message + "');";
                }
            }
            BindApplyData();
            BindApproveData();
        }
        else
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }


    protected void btn_approve_Click(object sender, EventArgs e)
    {
        bool isFinalApprove = Security["121100021"].isValid;
        if (!isFinalApprove && txtApproveName.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Select one Approver,Please!'); ";
            return;
        }
        if (txtApprOpin.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Approve Opinion,Please!'); ";
            return;
        }
        isFinalApprove &= txtApproveName.Text.Trim() == string.Empty;
        string applyId = lblApplyId.Text;
        string approver = txt_approveID.Text;
        string uID = Session["uID"].ToString();
        string approveOpoin = txtApprOpin.Text.Trim().ToString();
        DataTable table = new DataTable("ApplyDet");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "DetId";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "FinCheckPrice";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "StartDate";
        table.Columns.Add(column);

        #region 比较时间是否超过30天的参数设定
        DateTime dateTiemTest = DateTime.Now;
        DateTime dateNowAdd30 = DateTime.Now.AddDays(30);
        #endregion
        foreach (GridViewRow gvRow in gvDet.Rows)
        {

            TextBox txtCheckPrice = gvRow.FindControl("txtCheckPrice") as TextBox;
            TextBox txtStartDate = gvRow.FindControl("txtStartDate") as TextBox;
            if ((txtCheckPrice != null && txtCheckPrice.Text.Trim() != "") || (txtStartDate != null && txtStartDate.Text.Trim() != ""))
            {
                row = table.NewRow();
                int detId = int.Parse(gvDet.DataKeys[gvRow.RowIndex].Values["DetId"].ToString());
                row["DetId"] = detId;
                if (txtCheckPrice != null && txtCheckPrice.Text.Trim() != "")
                {
                    row["FinCheckPrice"] = txtCheckPrice.Text.Trim();
                }
                if (txtStartDate != null && txtStartDate.Text.Trim() != "")
                {
                    row["StartDate"] = txtStartDate.Text.Trim();
                    dateTiemTest = DateTime.Parse(row["StartDate"].ToString());
                    if (DateTime.Compare(dateTiemTest, dateNowAdd30) > 0)
                    {
                        ltlAlert.Text = "alert('生效日期超过当前时间的30天，为不合理数据，请查清后填写');";
                        return;

                    }
                }
               
                table.Rows.Add(row);
            }
        }
        if (table.Rows.Count > 0)
        {
            helper.UpdateApplyDet(table);
        }
        table.Dispose();
        if (isFinalApprove)
        {
            if (helper.Pass(applyId, approver, uID, approveOpoin))
            {
                ltlAlert.Text = "alert('Successfully!');";
                BindApplyData();
                BindApproveData();
            }
            else
            {
                ltlAlert.Text = "alert('Database Operation Failed!');";
                return;
            }
        }
        else
        {
            if (helper.Approve(applyId, approver, uID, approveOpoin, true))
            {
                bool isSuccess = true;
                string message = "";
                if (txt_ApproveEmail.Text.Trim() != "" && chkEmail.Checked)
                {
                    isSuccess = helper.SendMailForApprove(txt_ApproveEmail.Text.Trim(), txtApproveName.Text.Trim(), Session["Email"].ToString(), Session["uName"].ToString(), txtApplyReason.Text.Trim(), applyId, out message);
                }
                if (isSuccess)
                {
                    ltlAlert.Text = "alert('Successfully!');";

                }
                else
                {
                    if (message != "")
                    {
                        ltlAlert.Text = "alert('" + message + "');";
                    }
                }
                BindApplyData();
                BindApproveData();
                return;
            }
            else
            {
                ltlAlert.Text = "alert('Database Operation Failed!');";
                return;
            }
        }

    }
    protected void btn_diaApp_Click(object sender, EventArgs e)
    {
        if (txtApprOpin.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Approve Opinion,Please!'); ";
            return;
        }

        string applyId = lblApplyId.Text;
        string uID = Session["uID"].ToString();
        string approveOpoin = txtApprOpin.Text.Trim().ToString();
        if (helper.Approve(applyId, "", uID, approveOpoin, false))
        {
            if (lblApplyerEmail.Text != "")
            {
                bool isSuccess = true;
                string message = "";
                isSuccess = helper.SendMailForFailed(lblApplyerEmail.Text.Trim(), lblApplyer.Text.Trim(), Session["Email"].ToString(), Session["uName"].ToString(), approveOpoin, applyId, out message);
            }
            BindApplyData();
            BindApproveData();
            return;
        }
        else
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Id"] == null)
        {
            Response.Redirect("pc_PartCheckPriceList.aspx", true);
        }
        else
        {
            Response.Redirect("pc_FinCheckApplyList.aspx", true);
        }
    }


    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToBoolean(gvDet.DataKeys[e.Row.RowIndex].Values["isout"]))
            {
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Orange;
            }


            TextBox txtStartDate = e.Row.FindControl("txtStartDate") as TextBox;
            if (txtStartDate.Text.Trim() != "")
            {
                txtStartDate.Text = DateTime.Parse(txtStartDate.Text).ToString("yyyy-MM-dd");
            }

            if (!((TextBox)e.Row.FindControl("txtCheckPrice")).Text.ToString().Equals(e.Row.Cells[10].Text.ToString()))
            {
                e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void txtCheckPrice_TextChanged(object sender, EventArgs e)
    {

        TextBox txtb = (TextBox)sender;
        int index = ((GridViewRow)((txtb.NamingContainer))).RowIndex;
        decimal fincheck = 0;
        decimal check = 0;
        if ((!decimal.TryParse(txtb.Text.ToString().Trim(), out fincheck)) || txtb.Text.ToString().Trim().Equals(string.Empty))
        {
            ltlAlert.Text = "alert('输入的核准价格必须是数字');";
            return;
        }
        decimal.TryParse(gvDet.Rows[index].Cells[10].Text.ToString().Trim(), out check);
        if (fincheck != check)
        {
            gvDet.Rows[index].Cells[10].BackColor = System.Drawing.Color.Red;

        }
        else
        {
            gvDet.Rows[index].Cells[10].BackColor = gvDet.Rows[index].Cells[9].BackColor;
        }
    }
    protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbBasis")
        {
            Response.Redirect("pc_selectBasis.aspx?IMID=" + e.CommandArgument.ToString()+"&Id="+lblApplyId.Text);
        }
    }
    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        string ExcelString = "100^<b>申请号</b>~^100^<b>申请id</b>~^100^<b>QAD</b>~^100^<b>部件号</b>~^100^<b>供应商</b>~^100^<b>供应商名称</b>~^"
            + "100^<b>单位</b>~^100^<b>币种</b>~^100^<b>核价</b>~^"
           + "100^<b>批准核价</b>~^100^<b>生效日期</b>~^";

        DataTable dt = helper.selectFinCheckToExport(lblApplyId.Text.ToString());

        if (dt != null && dt.Rows.Count > 0)
        {
            ExportExcel(ExcelString, dt, false, ExcelVersion.Excel2003);
        }
        else
        {
            ltlAlert.Text = "alert('导出数据为空，请查询数据状态！');";
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImportExcelFile();
        BindApplyData();
    }
    public void ImportExcelFile()
    {
        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }
        }

        strUserFileName = filename.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }

        if (filename.PostedFile != null)
        {
            if (filename.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);//上传 文件
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                DataTable dt = null;
                try
                {
                    dt = this.GetExcelContents(strFileName);
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "导入文件必须是Excel格式''.";
                    return;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
                try
                {
                    string message = "";
                    bool success = helper.FinUploadCheckPrice(strFileName, strUID, lblApplyId.Text.Trim(), out message, dt);//插入，
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                        }
                    }
                    else
                    {
                        DataTable errDt = helper.GetInquiryPriceImportError(strUID, lblApplyId.Text.Trim());//输出错误信息
                        string title = "100^<b>申请号</b>~^100^<b>申请id</b>~^100^<b>QAD</b>~^100^<b>部件号</b>~^100^<b>供应商</b>~^100^<b>供应商名称</b>~^"
                                        + "100^<b>单位</b>~^100^<b>币种</b>~^100^<b>核价</b>~^"
                                       + "100^<b>批准核价</b>~^100^<b>生效日期</b>~^100^<b>错误信息</b>~^";
                        ltlAlert.Text = "alert('" + message + "')";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title, errDt, false, ExcelVersion.Excel2003);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + ex.Message.ToString() + "'.');";
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }
    }
}