using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
using System.Data;

public partial class EDI_EDI_ChangePoNew : BasePage
{
    poc_helper helper = new poc_helper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["poc_id"]==null)
            {
                hidPocId.Value = System.Guid.NewGuid().ToString();
                btnControl("initNoID");
            }
            else
            {

                hidPocId.Value = Request.QueryString["poc_id"].ToString();

                GetManualPoHrd();
                BindGridView();
                BindUpload();
                btnControl("initHaveID");
            }
        
        
        }
    }





    /// <summary>
    /// 按钮的控制
    /// 1，刚刚进入的时候
    /// 2，修改的时候
    /// 3，取消订单行的修改
    /// 4，删除上传
    /// 5，上传文件后
    /// 6，添加行后
    /// 
    /// 
    /// </summary>
    /// <param name="flag">标志是什么操作之后的行为</param>
    protected void btnControl(string flag)
    {
        if (flag.Equals("initHaveID"))
        {
            if (!txtPoNbr.Text.Equals(string.Empty))
            {
                txtPoNbr.Enabled = false;
                if (Convert.ToBoolean(haveDet.Value))
                {
                    btnSaveLine.Enabled = false;
                    gvlist.Columns[12].Visible = false;
                    gvlist.Columns[11].Visible = true;
                }
                else
                {
                    btnSaveLine.Enabled = true;
                    gvlist.Columns[12].Visible = true;
                    gvlist.Columns[11].Visible = false;
                }
                if (Convert.ToBoolean(haveDoc.Value))
                {
                    btnUpload.Enabled = false;
                }
                else
                {
                    btnUpload.Enabled = true;
                }
                if (!(btnSaveLine.Enabled || btnUpload.Enabled))
                {
                    btnCommit.Enabled = true;
                   
                }
                else
                {
                    btnCommit.Enabled = false;
                    
                }

                
            }
           
        }
        else if (flag.Equals("initNoID"))
        {
            txtPoNbr.Enabled = true;
            btnUpload.Enabled = false;
            btnCommit.Enabled = false;
            btnSaveLine.Enabled = false;
            //btnBack.Enabled = false;
        }
        else if (flag.Equals("CancelLine"))
        {
            txtPoNbr.Enabled = false;
 
            btnSaveLine.Enabled = true;
            gvlist.Columns[12].Visible = true;
            gvlist.Columns[11].Visible = false;
            
            if (Convert.ToBoolean(haveDoc.Value))
            {
                btnUpload.Enabled = false;
            }
            else
            {
                btnUpload.Enabled = true;
            }

            btnCommit.Enabled = false;
            
        }
        else if (flag.Equals("deleteDoc"))
        {
            txtPoNbr.Enabled = false;

            btnSaveLine.Enabled = true;
            
            if (Convert.ToBoolean(haveDoc.Value))
            {
                btnUpload.Enabled = false;
            }
            else
            {
                btnUpload.Enabled = true;
            }

           btnCommit.Enabled = false;
            
        }
        else if (flag.Equals("uploadDoc"))
        {
            txtPoNbr.Enabled = false;
            if (Convert.ToBoolean(haveDet.Value))
            {
                btnSaveLine.Enabled = false;
            }
            else
            {
                btnSaveLine.Enabled = true;
            }
            btnUpload.Enabled = false;
            if (!(btnSaveLine.Enabled || btnUpload.Enabled))
            {
                btnCommit.Enabled = true;
            }
            else
            {
                btnCommit.Enabled = false;
            }
        }
        else if (flag.Equals("addLine"))
        {
            txtPoNbr.Enabled = false;

            btnSaveLine.Enabled = false;
            gvlist.Columns[12].Visible = false;
            gvlist.Columns[11].Visible = true;

            if (Convert.ToBoolean(haveDoc.Value))
            {
                btnUpload.Enabled = false;
            }
            else
            {
                btnUpload.Enabled = true;
            }
            if (!(btnSaveLine.Enabled || btnUpload.Enabled))
            {
                btnCommit.Enabled = true;
            }
            else
            {
                btnCommit.Enabled = false;
            }
        }

    }

    protected void GetManualPoHrd()
    {
        try
        {

            DataSet ds = helper.selectEDIPoHrd(hidPocId.Value, txtPoNbr.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());



            if (ds.Tables[0].Rows.Count > 0)
            {
                txtPoNbr.Text = ds.Tables[0].Rows[0]["poNbr"].ToString();
                txtCust.Text = ds.Tables[0].Rows[0]["cusCode"].ToString();

                dropShipToBind();
                dropShipTo.SelectedIndex = -1;
                try
                {
                    dropShipTo.Items.FindByValue(ds.Tables[0].Rows[0]["shipto"].ToString()).Selected = true;
                }
                catch
                {
                    dropShipTo.SelectedIndex = -1;
                }
                dropChannelBind();
                dropChannel.SelectedIndex = -1;
                try
                {
                    dropChannel.Items.FindByValue(ds.Tables[0].Rows[0]["mpo_channel"].ToString()).Selected = true;
                }
                catch
                {
                    dropChannel.SelectedIndex = -1;
                }

                txtShipVia.Text = ds.Tables[0].Rows[0]["shipVia"].ToString();
                //txtReqDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["mpo_req_date"]));
                txtDueDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["dueDate"]));
                if (ds.Tables[0].Rows[0]["createdDate"].ToString() != string.Empty)
                {
                    txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["createdDate"]));
                }
                txtReason.Text = ds.Tables[0].Rows[0]["poc_reason"].ToString();

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["haveDet"].ToString()) == 0)
                {
                    haveDet.Value = "false";
                }
                else
                {
                    haveDet.Value = "true";
                }

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["haveDoc"].ToString()) == 0)
                {
                    haveDoc.Value = "false";
                }
                else
                {
                    haveDoc.Value = "true";
                }
              

            }
            else
            {
                ltlAlert.Text = "alert('The po number is not exist');";
                txtPoNbr.Text = "";
                txtPoNbr.Focus();
            }

           
            
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails while getting po!');";
        }
    }
    protected void txtPoNbr_TextChanged(object sender, EventArgs e)
    {
        GetManualPoHrd();
        BindGridView();
        BindUpload();
        gvlist.Columns[11].Visible = false;
    }

    private void BindUpload()
    {
        DataTable dt = helper.selectPoDocByID(hidPocId.Value,txtPoNbr.Text.Trim());

        gvUpload.DataSource = dt;
        gvUpload.DataBind();
    }


    protected void BindGridView()
    {

        DataSet ds = helper.selectEDIPoDet(hidPocId.Value, txtPoNbr.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());

        gvlist.DataSource = ds.Tables[0];
        gvlist.DataBind();
    }


    public void dropChannelBind()
    {
        DataTable dt = helper.BindChannel(Session["PlantCode"].ToString());

        dropChannel.DataSource = dt;
        dropChannel.DataBind();
    
    }

    public void dropShipToBind()
    {
        DataTable dt = helper.BindShipTo(txtCust.Text.Trim(),Session["PlantCode"].ToString());

        dropShipTo.DataSource = dt;
        dropShipTo.DataBind();
    }


    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            
            GridViewRow row = e.Row;
            bool flag = false;

            if (gvlist.DataKeys[e.Row.RowIndex].Values["addflag"].ToString() == "add")
            {
                flag = true;
            }
            // Make sure we aren't in header/footer rows
            if (row.DataItem == null)
            {
                return;
            }

            CellShow(row, "partNbr", ref flag);
            CellShow(row, "sku", ref flag);
            CellShow(row, "qadPart", ref flag);
            CellShow(row, "ordQty", ref flag);
            CellShow(row, "um", ref flag);
            CellShow(row, "price", ref flag);
            CellShow(row, "reqDate", ref flag);
            CellShow(row, "dueDate", ref flag);
            CellShow(row, "desc", ref flag);
            CellShow(row, "remark", ref flag);

            if (flag )
            {
                ((LinkButton)e.Row.FindControl("lkbEdit")).Visible = true;
                ((LinkButton)e.Row.FindControl("lkbCancel")).Visible = true;
                gvlist.Columns[12].Visible = false;

            }

            if( !gvlist.DataKeys[e.Row.RowIndex].Values["parentLine"].ToString().Equals("null"))
            {
                ((LinkButton)e.Row.FindControl("lkbEide")).Visible = false;
                ((LinkButton)e.Row.FindControl("lkbDeleteLine")).Visible = false;
            }
            
        }
    }

    /// <summary>
    /// 处理行的方法
    /// </summary>
    /// <param name="row">当前行的对象</param>
    /// <param name="field">列的字段</param>
    private void CellShow(GridViewRow row, string field, ref bool flag)
    {
        string value = ((DataRowView)row.DataItem)[field].ToString();
        Label link = row.FindControl("link" + field) as Label;
        Label label = row.FindControl("lbl" + field) as Label;
        if (value.Contains("|"))
        {
            string[] values = value.Split(new char[] { '|' });
            string oldValue = values[0];
            string newValue = values[1];
            if (link != null)
            {
                link.Text = newValue;
               
            }
            if (label != null)
            {
                label.Text = oldValue;
                label.Font.Strikeout = true;
            }
            flag = true;
        }
        else
        {
            if (label != null)
            {
                label.Text = value;
                label.Font.Strikeout = false;
            }
            if (link != null)
            {
                link.Visible = false;
            }
        }
    }
    protected void  gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {

       
       
        if (e.CommandName == "btnEdit")
        {
            int rowindex = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex; //此得出的值是表示那行被选中的索引值
            txtDetLine.Text = gvlist.DataKeys[rowindex].Values["poLine"].ToString();
            SqlDataReader sdr = helper.selectPocDetByID(gvlist.DataKeys[rowindex].Values["id"].ToString());


            if (sdr.Read())
            {
                txtDetCustPart.Text = sdr["pocd_cust_part"].ToString();
                txtSKU.Text = sdr["pocd_SKU"].ToString();
                txtDetQad.Text = sdr["pocd_qad"].ToString();
                txtDetQty.Text = sdr["pocd_ord_qty"].ToString();
                txtDetUm.Text = sdr["pocd_um"].ToString();
                txtDetPrice.Text = sdr["pocd_price"].ToString();
                txtDetReqDate.Text = sdr["pocd_req_date"].ToString();
                txtDetDueDate.Text = sdr["pocd_due_date"].ToString();
                txtDesc.Text = sdr["pocd_desc"].ToString();
                txtDetRemarks.Text = sdr["pocd_rmks"].ToString();
                
            }
            sdr.Dispose();
            sdr.Close();

            btnSaveLine.Enabled = true;
        }

        if (e.CommandName == "btnCancel")
        {
            int rowindex = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex; //此得出的值是表示那行被选中的索引值
            if (helper.deletePocDetByID(gvlist.DataKeys[rowindex].Values["id"].ToString()))
            {
                ltlAlert.Text = "alert('Cancel Success!');";
                
            }
            else
            {
                ltlAlert.Text = "alert('Cancel failed!');";
            }
            GetManualPoHrd();
            BindGridView();
            txtDetLine.Text = "";
            txtDetCustPart.Text = "";
            txtSKU.Text = "";
            txtDetQad.Text = "";
            txtDetQty.Text = "";
            txtDetUm.Text = "EA";
            txtDetPrice.Text = "";
            txtDetReqDate.Text = "";
            txtDetDueDate.Text = "";
            txtDetRemarks.Text = "";
            txtDesc.Text = "";
            btnControl("CancelLine");
        }
        if (e.CommandName == "lkbDeleteLine")
        {
            int rowindex = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex; //此得出的值是表示那行被选中的索引值
            if (helper.deleteEDIPoLine(gvlist.DataKeys[rowindex].Values["poLine"].ToString(), hidPocId.Value, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('Cancel Success!');";
               
            }
            else
            {
                ltlAlert.Text = "alert('Cancel failed!');";
            }
            GetManualPoHrd();
            BindGridView();
            btnControl("addLine");
        }

        if (e.CommandName == "lkbEide")
        {
            int rowindex = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex; //此得出的值是表示那行被选中的索引值
            txtDetLine.Text = gvlist.DataKeys[rowindex].Values["poLine"].ToString();
            txtDetCustPart.Text = gvlist.DataKeys[rowindex].Values["partNbr"].ToString();
            txtSKU.Text = gvlist.DataKeys[rowindex].Values["sku"].ToString();
            txtDetQad.Text = gvlist.DataKeys[rowindex].Values["qadPart"].ToString();
            txtDetQty.Text = gvlist.DataKeys[rowindex].Values["ordQty"].ToString();
            txtDetUm.Text = gvlist.DataKeys[rowindex].Values["um"].ToString();
            txtDetPrice.Text = gvlist.DataKeys[rowindex].Values["price"].ToString();
            txtDetReqDate.Text = gvlist.DataKeys[rowindex].Values["reqDate"].ToString();
            txtDetDueDate.Text = gvlist.DataKeys[rowindex].Values["dueDate"].ToString();
            txtDetRemarks.Text = gvlist.DataKeys[rowindex].Values["remark"].ToString();
            txtDesc.Text = gvlist.DataKeys[rowindex].Values["desc"].ToString();
        }
    }
    protected void btnSaveLine_Click(object sender, EventArgs e)
    {
        if (!txtPoNbr.Text.Equals(string.Empty))
        {
            int lineint = -1;
            string line = txtDetLine.Text.Trim();

            if (line.Equals(string.Empty))
            {
                ltlAlert.Text = "alert('Line can not Empty');";
                return;
            }
            else if (!int.TryParse(line, out lineint))
            {
                ltlAlert.Text = "alert('Line must be number');";
                return;
            }
            else if (lineint == 0)
            {
                ltlAlert.Text = "alert('Line can not be 0');";
                return;
            }

            string custPart = txtDetCustPart.Text.Trim();

            if (custPart.Equals(string.Empty))
            {
                ltlAlert.Text = "alert('Cust part can not Empty');";
                return;
            }

            string SKU = txtSKU.Text.Trim();

           

            string QAD = txtDetQad.Text.Trim();
            //int QADint = 0;

            //if (QAD.Equals(string.Empty))
            //{
            //    ltlAlert.Text = "alert('QAD can not Empty');";
            //    return;
            //}
            //else if (QAD.Length != 14)
            //{
            //    ltlAlert.Text = "alert('The QAD length must be 14');";
            //    return;
            //}
            //else if (int.TryParse(QAD, out QADint))
            //{
            //    ltlAlert.Text = "alert('The QAD  must be number');";
            //    return;
            //}

            string qty = txtDetQty.Text.Trim();
            decimal qtydec = 0;

            if (qty.Equals(string.Empty))
            {
                ltlAlert.Text = "alert('Qty can not Empty');";
                return;
            }
            else if (!decimal.TryParse(qty, out qtydec))
            {
                ltlAlert.Text = "alert('Qty must be number');";
                return;
            }
            else if (qtydec < 0)
            {
                ltlAlert.Text = "alert('Qty can not be than 0');";
                return;
            }

            string price = txtDetPrice.Text.Trim();
            decimal priced = -1;

            if (price.Equals(string.Empty))
            {
                ltlAlert.Text = "alert('Price can not Empty');";
                return;
            }
            else if (!decimal.TryParse(price, out priced))
            {
                ltlAlert.Text = "alert('Price must be number');";
                return;
            }
            else if (priced < 0)
            {
                ltlAlert.Text = "alert('Price can not be than 0');";
                return;
            }

            string um = txtDetUm.Text.Trim();

            if (um.Equals(string.Empty))
            {
                ltlAlert.Text = "alert('Um can not Empty');";
                return;
            }
            else if (!(um.Equals("EA") || um.Equals("BO") || um.Equals("G") || um.Equals("KG") || um.Equals("M")
                || um.Equals("PC") || um.Equals("RL") || um.Equals("L")))
            {
                ltlAlert.Text = "alert('Um must in EA,BO,G,KG,M,PC,RL,L');"; 
                return;
            }

            string reqDate = txtDetReqDate.Text.Trim();
            string dueDate = txtDetDueDate.Text.Trim();

            try
            {
                Convert.ToDateTime(reqDate);
                Convert.ToDateTime(dueDate);
            }
            catch
            {
                ltlAlert.Text = "alert('ReqDate or dueDate must be Date');";
                return;
            }

            if (txtDesc.Text.Trim().Equals(string.Empty))
            {
                ltlAlert.Text = "alert('Description can not Empty');";
                return;
            }

            string desc = txtDesc.Text.Trim();
            string remark = txtDetRemarks.Text.Trim();

            string flag = helper.saveLine(hidPocId.Value, line, custPart, SKU, QAD, qty, price, um, reqDate, dueDate, remark
                , Session["uID"].ToString(), Session["uName"].ToString(), txtPoNbr.Text.Trim(), desc);
            if (flag == "1")
            {
                ltlAlert.Text = "alert('Save line success');";
                GetManualPoHrd();
                BindGridView();
                txtDetLine.Text = "";
                txtDetCustPart.Text = "";
                txtSKU.Text = "";
                txtDetQad.Text = "";
                txtDetQty.Text = "";
                txtDetUm.Text = "EA";
                txtDetPrice.Text = "";
                txtDetReqDate.Text = "";
                txtDetDueDate.Text = "";
                txtDetRemarks.Text = "";
                txtDesc.Text = "";

                btnControl("addLine");
                

            }
            else if(flag =="-5")
            {
                ltlAlert.Text = "alert('Save line failed,The line in Apply,already');";
            }
            else if (flag == "-6")
            {
                ltlAlert.Text = "alert('Save line failed,The line have not change!');";
            }
            else if (flag == "-7")
            {
                ltlAlert.Text = "alert('Save line failed,The line had be splited!');";
            }
            else
            {
                ltlAlert.Text = "alert('Save line failed');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('Please enter Po number!');";
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string path = "/TecDocs/QC/";
        string fileName = string.Empty;//原文件名
        string filePate = string.Empty;//文件路径+文件名（存储的）
        if (string.Empty.Equals(fileManager.PostedFile.FileName))
        {
            ltlAlert.Text = "alert('Upload path can not be null');";
            return;

        }
        else
        {
            if (!ImportFile(ref fileName, ref filePate, path, fileManager))
            {
                ltlAlert.Text = "alert('Upload file failed! please Linking administrators');";
                return;
            }
            if (Request["url"] != null)
            {
                if (File.Exists(Request["url"].ToString()))
                {
                    File.Delete(Request["url"].ToString());
                }
            
            }
          
        }

        if (helper.uploadManualPo(hidPocId.Value, fileName, filePate, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('Upload file success!');";
            GetManualPoHrd();
            BindGridView();
            BindUpload();
            btnControl("uploadDoc");
        }
        else
        {

            ltlAlert.Text = "alert('Upload file failed! please Linking administrators');";
            return;
        }
    }


    protected bool ImportFile(ref string _fileName, ref string _filePath, string path, System.Web.UI.HtmlControls.HtmlInputFile filename)
    {
        string attachName = Path.GetFileNameWithoutExtension(filename.PostedFile.FileName);
        string newFileName = DateTime.Now.ToFileTime().ToString();

        string attachExtension = Path.GetExtension(filename.PostedFile.FileName);
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../import/"), newFileName + attachExtension);//合并两个路径为上传到服务器上的全路径

        if (File.Exists(SaveFileName))
        {
            try
            {
                File.Delete(SaveFileName);
            }
            catch
            {
                return false;
            }
        }

        try
        {
            filename.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            return false;
        }



        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        path += newFileName + attachExtension;

        try
        {
            File.Move(SaveFileName, Server.MapPath(path));
        }
        catch
        {
            return false;
        }

        _fileName = attachName + attachExtension;
        _filePath = path;

        return true;
    }
    protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int rowIndex = e.Row.RowIndex;

            if (!Convert.ToBoolean(Convert.ToInt32(gvUpload.DataKeys[rowIndex].Values["flag"].ToString())))
            {
                ((LinkButton)e.Row.FindControl("btnDelete")).Text = "";
            }
        }
    }
    protected void gvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnView")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }
    }
    protected void gvUpload_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = gvUpload.DataKeys[e.RowIndex].Values["id"].ToString();

        if (helper.deletePoDoc(id))
        {
            ltlAlert.Text = "alert('Delete file success!');";
            GetManualPoHrd();
            BindGridView();
            BindUpload();
            btnControl("deleteDoc");
        }
        else
        {
            ltlAlert.Text = "alert('Delete file failed!');";
            return;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDI_ChangePoMstr.aspx");
    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (helper.commitDet(hidPocId.Value,Session["uID"].ToString(),Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('Commit success!');";
            Response.Redirect("EDI_ChangePoMstr.aspx");
        }
        else
        {
            ltlAlert.Text = "alert('Commit failed! Please Linking administrators');";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!txtPoNbr.Text.Equals(string.Empty))
        {
            if (helper.saveReason(hidPocId.Value, txtReason.Text, Session["uID"].ToString(), Session["uName"].ToString(), txtPoNbr.Text.Trim()))
            {
                this.ltlAlert.Text = "alert('Save reason success！');";
                GetManualPoHrd();
                BindGridView();
                btnControl("initHaveID");


            }
            else
            {
                this.ltlAlert.Text = "alert('Save reason failed！');";
            }
        }
        else
        {
            this.ltlAlert.Text = "alert('Please enter Po number!');";
        }
    }
}