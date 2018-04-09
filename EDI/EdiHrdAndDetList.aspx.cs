using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Collections.Generic;
using System.Data.Odbc;
using TCPCHINA.ODBCHelper;
using OEAppServer; 

public partial class EDI_EdiHrdAndDetList : BasePage
{

    private string gvUniqueID = String.Empty;
    private int gvNewPageIndex = 0;
    private int gvEditIndex = -1;
    private string filter
    {
        get 
        {
            if (ViewState["filter"] == null)
            {
                ViewState["filter"] = "0";            
            }
            return ViewState["filter"].ToString();
        }
        set
        {
            ViewState["filter"] = value;
        }
    }
    private bool newQuery
    {
        get
        {
            if (ViewState["newQuery"] == null)
            {
                ViewState["newQuery"] = true;
            }
            return (bool)ViewState["newQuery"];
        }
        set
        {
            ViewState["newQuery"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filter = "0";
            gvBind(true);
            gvlist.Columns[7].Visible = true;
            if (Session["PlantCode"].ToString() == "11" || (Session["PlantCode"].ToString() == "1" && Session["deptID"].ToString() == "435") || (Session["PlantCode"].ToString() == "5" && Session["deptID"].ToString() == "8"))
            {
                hidTCB.Value = "1";
                btnImport_TCB.Visible = true;
                btnImport.Visible = false;
            }
            else
            {
                btnImport_TCB.Visible = false;
                btnImport.Visible = true;
            }
            btnCimload.Visible = false;
        }
    }

    protected void rbNormal_CheckedChanged(object sender, EventArgs e)
    {
        if (rbNormal.Checked == true)
        {
            filter = "0";
            this.gvBind(true);
            gvlist.Columns[7].Visible = true;
            if (hidTCB.Value == "1")
            {
                btnImport_TCB.Visible = true;
                btnImport.Visible = false;
            }
            else
            {
                btnImport_TCB.Visible = false;
                btnImport.Visible = true;
            }
            btnCimload.Visible = false;
        }
    }

    protected void rbError_CheckedChanged(object sender, EventArgs e)
    {
        if (rbError.Checked == true)
        {
            filter = "1";
            this.gvBind(true);
            gvlist.Columns[7].Visible = false;
            if (hidTCB.Value == "1")
            {
                btnImport_TCB.Visible = true;
                btnImport.Visible = false;
                btnCimload.Visible = false;
            }
            else
            {
                btnImport_TCB.Visible = false;
                btnImport.Visible = false;
                btnCimload.Visible = true;
            }
            txtDate.Text = string.Empty;
        }
    }

    protected void rbPartError_CheckedChanged(object sender, EventArgs e)
    {
        if (rbPartError.Checked == true)
        {
            filter = "5";
            this.gvBind(true);
            gvlist.Columns[7].Visible = false;
            if (hidTCB.Value == "1")
            {
                btnImport_TCB.Visible = true;
                btnImport.Visible = false;
                btnCimload.Visible = false;
            }
            else
            {
                btnImport_TCB.Visible = false;
                btnImport.Visible = false;
                btnCimload.Visible = true;
            }
        }
    }

    protected void rbFinish_CheckedChanged(object sender, EventArgs e)
    {
        if (rbFinish.Checked == true)
        {
            filter = "6";
            this.gvBind(true);
            if (hidTCB.Value == "1")
            {
                btnImport_TCB.Visible = true;
                btnImport.Visible = false;
                btnCimload.Visible = false;
            }
            else
            {
                btnImport_TCB.Visible = false;
                btnImport.Visible = false;
                btnCimload.Visible = true;
            }
        }
    }

    private void gvBind(bool newQuery)
    {
        this.newQuery = newQuery;

        if (this.newQuery)
        {
            HiddenField1.Value = ";";
        }
        if (txtDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('导入日期格式不正确!');";
                return;
            }
        }
        //如果不是导入的时候，要设置日期
        DataSet dsPo = getEdiData.getEdiPoHrd(txtOrder.Text.Trim(), filter, txtDate.Text.Trim(), Session["plantCode"].ToString(), Session["uID"].ToString());

        //2016-2-17 去掉默认勾选
        //if (this.newQuery && filter != "6")
        //{
        //    foreach (DataRow row in dsPo.Tables[0].Rows)    
        //    {
        //        string hrdId = row["id"].ToString();
        //        DataSet ds = getEdiData.getEdiPoDet(hrdId);
        //        foreach (DataRow detRow in ds.Tables[0].Rows)
        //        {
        //            string detId = detRow["id"].ToString();
        //            string qadPart = detRow["qadPart"].ToString();
        //            string finished = detRow["finished"].ToString().ToLower();
        //            string appvResult = detRow["appvResult"].ToString();
        //            string site = detRow["site"].ToString();
        //            if (qadPart != "" && finished == "false" && appvResult == "1" && site != "")
        //            {
        //                if (!HiddenField1.Value.Contains(";" + detId + ";"))
        //                {
        //                    HiddenField1.Value += detId + ";";
        //                }
        //            }
        //        }
        //    }
        //}

        if (dsPo.Tables[0].Rows.Count == 0)
        {
            dsPo.Tables[0].Rows.Add(dsPo.Tables[0].NewRow());
        }

        DataView dvw = dsPo.Tables[0].DefaultView;

        gvlist.DataSource = dvw;
        gvlist.DataBind();
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        string strSort = string.Empty;

        // Make sure we aren't in header/footer rows
        if (row.DataItem == null)
        {
            return;
        }

        //Find Child GridView control
        GridView gv = (GridView)row.FindControl("gvDet");


        if (gv.UniqueID == gvUniqueID)
        {
            gv.PageIndex = gvNewPageIndex;
            gv.EditIndex = gvEditIndex;         
            //Expand the Child grid
            ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["id"].ToString() + "','one');</script>");
        }

        //Prepare the query for Child GridView by passing the Customer ID of the parent row
        DataSet ds = getEdiData.getEdiPoDet(((DataRowView)e.Row.DataItem)["id"].ToString());
        //if (newQuery)
        //{
            //foreach (DataRow detRow in ds.Tables[0].Rows)
            //{
            //    string detId = detRow["id"].ToString();
            //    string qadPart = detRow["qadPart"].ToString();
            //    string finished = detRow["finished"].ToString().ToLower();
            //    string appvResult = detRow["appvResult"].ToString();
            //    string site = detRow["site"].ToString();
            //    if (qadPart != "" && finished == "false" && appvResult == "1" && site != "")
            //    {
            //        if (!HiddenField1.Value.Contains(";" + detId + ";"))
            //        {
            //            HiddenField1.Value += detId + ";";
            //        }
            //    }
            //}
        //}
        gv.DataSource = ds;
        gv.DataBind();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("lblPoId")) != null && ((Label)e.Row.FindControl("lblPoId")).Text != string.Empty)
            {
                if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["detError"]))
                {
                    ((Label)e.Row.FindControl("Label1")).Style["background-color"] = "red";
                }

                //对于应该提交Plan的判定，不需要的，不判定
                if (((HtmlInputHidden)e.Row.FindControl("hidShouldToPlan")).Value.ToLower() == "true")
                {
                    if (((HtmlInputHidden)e.Row.FindControl("hidToPlan")).Value.ToLower() == "true")
                    {
                        e.Row.Cells[11].Text = string.Empty;
                    }
                }
                else
                {
                    e.Row.Cells[11].Text = string.Empty;
                }
            }

            //如果事先没有分配域和地点，则无法导入
            LinkButton linkImport = (LinkButton)e.Row.FindControl("linkImport");
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)e.Row.FindControl("chkImport");

            try
            {
                if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["notNeeded"]))
                {
                    linkImport.Text = "不导入";

                    chkImport.Checked = false;
                    chkImport.Disabled = true;
                }
                else
                {
                    linkImport.Text = "需要";

                    //HRD必须维护制地，Det也必须维护制地
                    if (gvlist.DataKeys[e.Row.RowIndex].Values["domain"].ToString().Length > 0 && Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["detNoSite"]) == false)
                    {
                        chkImport.Disabled = false;

                        if (e.Row.Cells[9].Text.Length == 0 || e.Row.Cells[9].Text == "&nbsp;" || e.Row.Cells[10].Text.Length == 0 || e.Row.Cells[10].Text == "&nbsp;")
                        {
                            chkImport.Disabled = true;
                        }
                    }
                    else
                    {
                        chkImport.Disabled = true;
                    }
                }
            }
            catch
            { }

            try
            {
                LinkButton linkBigOrder = (LinkButton)e.Row.FindControl("linkBigOrder");

                if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["inBigOrder"]))
                {
                    linkBigOrder.Text = "不显示";
                }
                else
                {
                    linkBigOrder.Text = "显示";
                }
            }
            catch
            {
                ;
            }
        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GetSelectDetId();
        gvlist.PageIndex = e.NewPageIndex;
        gvBind(false);
       
    }

    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string detId = gvTemp.DataKeys[e.Row.RowIndex].Values["id"].ToString();
            string qadPart = gvTemp.DataKeys[e.Row.RowIndex].Values["qadPart"].ToString();
            string errMsg=gvTemp.DataKeys[e.Row.RowIndex].Values["errMsg"].ToString();
            string finished = gvTemp.DataKeys[e.Row.RowIndex].Values["finished"].ToString();
            string cannotselect = gvTemp.DataKeys[e.Row.RowIndex].Values["cannotselect"].ToString();
            if ((Label)e.Row.FindControl("lbl_dateInfo") != null)
            {
                Label lbl_dateInfo = (Label)e.Row.FindControl("lbl_dateInfo");
                if (lbl_dateInfo.Text.Trim() == "1900-01-01")
                {
                    lbl_dateInfo.Text = "";
                }
            }
            Label lbl_appvResult = e.Row.FindControl("lbl_appvResult") as Label;
            bool canSplit = false;
            if (lbl_appvResult != null)
            {
                if (lbl_appvResult.Text == "1")
                {
                    lbl_appvResult.Text = "Pass";
                    if( gvTemp.DataKeys[e.Row.RowIndex].Values["parentLine"].ToString()=="")
                    {
                        canSplit = true;
                    }
                }
                else
                {
                    lbl_appvResult.Text = "";
                }
            }

            //(e.Row.FindControl("linkSplit") as LinkButton).Visible = canSplit;

            CheckBox chk = e.Row.FindControl("chk") as CheckBox;


            if (chk != null)
            {
                if (qadPart == "" || finished.ToUpper() == "TRUE" || cannotselect == "1")// ||errMsg != "" && !errMsg.StartsWith("价格不一致")))
                {
                    chk.Enabled = false;
                }

                if (HiddenField1.Value.Contains(";" + detId + ";"))
                {
                    chk.Checked = true;
                }
            }
        }
    }

    protected void gvDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        GetSelectDetId();
        gvUniqueID = gvTemp.UniqueID;
        gvNewPageIndex = e.NewPageIndex;
        gvBind(false);
    }

    private void GetSelectDetId()
    {
        foreach (GridViewRow hrdRow in gvlist.Rows)
        {
            GridView gvTemp = (GridView)hrdRow.FindControl("gvDet");
            if (gvTemp != null)
            {
                foreach (GridViewRow row in gvTemp.Rows)
                {
                    string detId = gvTemp.DataKeys[row.RowIndex].Values["id"].ToString();
                    CheckBox chk = row.FindControl("chk") as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        if (!HiddenField1.Value.Contains(";" + detId + ";"))
                        {
                            HiddenField1.Value += detId + ";";
                        }
                    }
                    else
                    {
                        if (HiddenField1.Value.Contains(";" + detId + ";"))
                        {
                            HiddenField1.Value = HiddenField1.Value.Replace(";" + detId + ";", ";");
                        }
                    }
                }
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        gvBind(true);
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        GetSelectDetId();
        if (rbNormal.Checked)
        {
            if (gvlist.Rows.Count > 0)
            {
                if (HiddenField1.Value == ";")
                {
                    ltlAlert.Text = "alert('请选择数据！');";
                    return;
                }
                string ids = HiddenField1.Value.Substring(1, HiddenField1.Value.Length - 2).Replace(";", ",");
                DataTable dt = getEdiData.GetEdiPoDetWithIds(ids);
                foreach (DataRow row in dt.Rows)
                {
                    if (row["isEdiConfig"].ToString().ToLower() == "false")
                    {
                        ltlAlert.Text = "alert('订单" + row["poNbr"].ToString() + "存在客户未配置35.13.7！');";
                        return;
                    }
                    string line = "订单" + row["poNbr"].ToString() + "第" + row["poLine"].ToString() + "行";
                    if (string.IsNullOrWhiteSpace(row["qadPart"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的QAD号为空！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["domain"].ToString()))
                    {
                        ltlAlert.Text = "alert('订单" + row["poNbr"].ToString() + "的域为空！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["site"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的地点为空！');";
                        return;
                    }
                    if (row["appvResult"].ToString() == "0" && row["domain"].ToString().ToUpper() != "TCB" && string.IsNullOrWhiteSpace(row["loadRmks"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "没有审批通过，若要导入请填写导入备注！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["qad_dueDate"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的截止日期为空！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["qad_perfDate"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的截止日期为空！');";
                        return;
                    }
                }
                DataTable dt_so = GetSoDet();
                UpdateSO(dt_so);
                if (export())
                {
                    getEdiData.UpdateEdiPoDetFinishedByIds(ids);
                    try
                    {                   
                        getEdiData.updateOrdStatus2(ids);
                    }
                    catch (Exception ex)
                    {
                        ltlAlert.Text = "alert('订单更新状态失败！);";
                    }
                    
                    ltlAlert.Text = "alert('导入成功！');";
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败！');";
                }
                gvBind(false);
            }
        }
    }

    protected void btnCimload_Click(object sender, EventArgs e)
    {
        GetSelectDetId();
        if (HiddenField1.Value == ";")
        {
            ltlAlert.Text = "alert('请选择数据！');";
        }
        else
        {
            string ids = HiddenField1.Value.Substring(1, HiddenField1.Value.Length - 2).Replace(";", ",");
            DataTable dt = getEdiData.GetEdiPoDetWithIds(ids);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string line = "订单" + row["poNbr"].ToString() + "第" + row["poLine"].ToString() + "行";
                    if (string.IsNullOrWhiteSpace(row["so_nbr"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "不存在对应的销售单！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["qadPart"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的QAD号为空！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["site"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的地点为空！');";
                        return;
                    }
                    if (row["appvResult"].ToString() == "0" && row["domain"].ToString().ToUpper() != "TCB" && string.IsNullOrWhiteSpace(row["loadRmks"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "没有审批通过，若要导入请填写导入备注！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["qad_dueDate"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的截止日期为空！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["qad_perfDate"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的截止日期为空！');";
                        return;
                    }
                }
                getEdiData.UpdateEdiPoDetFinishedByIds(ids);
                ltlAlert.Text = "alert('导入成功！');";
                //string tempFile = Server.MapPath("/docs/sodcimload.xls");
                //string outputFile = "sodcimload_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                //getEdiData.ExportCimLoadExcel(tempFile, Server.MapPath("../Excel/") + outputFile, dt);
                //ltlAlert.Text = "window.open('/Excel/" + outputFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
                gvBind(false);
            }
        }
    }
    protected void gvDet_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        gvUniqueID = gvTemp.UniqueID;
        gvEditIndex = e.NewEditIndex;
        gvNewPageIndex = gvTemp.PageIndex;
        gvBind(false);
    }

    protected void gvDet_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        gvUniqueID = gvTemp.UniqueID;
        gvEditIndex = -1;
        gvBind(false);
    }

    protected void gvDet_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridView gvTemp = (GridView)sender;
        gvUniqueID = gvTemp.UniqueID;
        string id = gvTemp.DataKeys[e.RowIndex].Values["id"].ToString();
        string loadRmks = (gvTemp.Rows[e.RowIndex].FindControl("txt_loadRmks") as TextBox).Text;
        string Rmks = (gvTemp.Rows[e.RowIndex].FindControl("txt_Rmks") as TextBox).Text;

        getEdiData.UpdateDetLoadRmks(id, loadRmks, Rmks);
        ltlAlert.Text = "alert('Success！');";
        gvEditIndex = -1;
        gvBind(false);

    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;

        GridViewRow row = chk.Parent.Parent as GridViewRow;
        GridView gv = (GridView)row.FindControl("gvDet");
        gvUniqueID = gv.UniqueID;
        string domain = gvlist.DataKeys[row.RowIndex].Values["mpo_domain"].ToString();
        DataSet ds = getEdiData.getEdiPoDet(gvlist.DataKeys[row.RowIndex].Values["id"].ToString());
        if (chk.Checked)
        {
            foreach (DataRow detRow in ds.Tables[0].Rows)
            {
                string detId = detRow["id"].ToString();
                string qadPart = detRow["qadPart"].ToString();
                string finished = detRow["finished"].ToString().ToLower();
                string appvResult = detRow["appvResult"].ToString();
                string site = detRow["site"].ToString();
                if (qadPart != "" && finished == "false" && (appvResult == "1" || domain.ToUpper()=="TCB") && site != "")
                {
                    if (!HiddenField1.Value.Contains(";" + detId + ";"))
                    {
                        HiddenField1.Value += detId + ";";
                    }
                }
            }
        }
        else
        {
            foreach (DataRow detRow in ds.Tables[0].Rows)
            {
                string detId = detRow["id"].ToString();
                if (HiddenField1.Value.Contains(";" + detId + ";"))
                {
                    HiddenField1.Value = HiddenField1.Value.Replace(";" + detId + ";", ";");
                }
            }
        }
        gv.DataSource = ds;
        gv.DataBind();
        
    }


    private Boolean export()
    {
        try
        {
            DataSet ds = null;
            FtpStatusCode status;
            string root = @"c:\\"; //Server.MapPath("..\\Excel\\");
            string path_szx = root + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + "szx.tcp";
            string path_zql = root + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + "zql.tcp";
            string path_zqz = root + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + "zqz.tcp";
            string path_tcb = root + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + "tcb.tcp";
            string path_yql = root + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + "yql.tcp";
            //Response.Write(path_szx + "<br />" + path_zqz);

            if (File.Exists(path_szx))
            {
                //删除文件
                File.Delete(path_szx);
            }
            if (File.Exists(path_zql))
            {
                //删除文件
                File.Delete(path_zql);
            }
            if (File.Exists(path_zqz))
            {
                //删除文件
                File.Delete(path_zqz);
            }
            if (File.Exists(path_tcb))
            {
                //删除文件
                File.Delete(path_tcb);
            }
            if (File.Exists(path_yql))
            {
                //删除文件
                File.Delete(path_yql);
            }

            string strID = HiddenField1.Value.Substring(1, HiddenField1.Value.Length - 2).Replace(";", ",");

            if (strID.Length == 0)
            {
                return false;
            }

            ds = getEdiData.GetEdiPoDetExportList(strID);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    StreamWriter writer_szx = new StreamWriter(path_szx, false, Encoding.GetEncoding("gb2312"));

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        writer_szx.WriteLine(row["val"].ToString());
                    }

                    writer_szx.Close();

                    status = UploadFun(path_szx, "ftp://" + ConfigurationManager.AppSettings["FtpServerAddress"].ToString() + "/" + path_szx.Substring(4));

                    if (status == FtpStatusCode.ClosingData)
                    {
                        if (File.Exists(path_szx))
                        {
                            //删除文件
                            File.Delete(path_szx);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    StreamWriter writer_zql = new StreamWriter(path_zql, false, Encoding.GetEncoding("gb2312"));

                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        writer_zql.WriteLine(row["val"].ToString());
                    }

                    writer_zql.Close();

                    status = UploadFun(path_zql, "ftp://" + ConfigurationManager.AppSettings["FtpServerAddress"].ToString() + "/" + path_zql.Substring(4));

                    if (status == FtpStatusCode.ClosingData)
                    {
                        if (File.Exists(path_zql))
                        {
                            //删除文件
                            File.Delete(path_zql);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    StreamWriter writer_zqz = new StreamWriter(path_zqz, false, Encoding.GetEncoding("gb2312"));

                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        writer_zqz.WriteLine(row["val"].ToString());
                    }

                    writer_zqz.Close();

                    status = UploadFun(path_zqz, "ftp://" + ConfigurationManager.AppSettings["FtpServerAddress"].ToString() + "/" + path_zqz.Substring(4));

                    if (status == FtpStatusCode.ClosingData)
                    {
                        if (File.Exists(path_zqz))
                        {
                            //删除文件
                            File.Delete(path_zqz);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (ds.Tables[4].Rows.Count > 0)
                {
                    StreamWriter writer_tcb = new StreamWriter(path_tcb, false, Encoding.GetEncoding("gb2312"));

                    foreach (DataRow row in ds.Tables[4].Rows)
                    {
                        writer_tcb.WriteLine(row["val"].ToString());
                    }

                    writer_tcb.Close();

                    status = UploadFun(path_tcb, "ftp://" + ConfigurationManager.AppSettings["FtpServerAddress"].ToString() + "/" + path_tcb.Substring(4));

                    if (status == FtpStatusCode.ClosingData)
                    {
                        if (File.Exists(path_tcb))
                        {
                            //删除文件
                            File.Delete(path_tcb);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                if (ds.Tables[5].Rows.Count > 0)
                {
                    StreamWriter writer_yql = new StreamWriter(path_yql, false, Encoding.GetEncoding("gb2312"));

                    foreach (DataRow row in ds.Tables[4].Rows)
                    {
                        writer_yql.WriteLine(row["val"].ToString());
                    }

                    writer_yql.Close();

                    status = UploadFun(path_yql, "ftp://" + ConfigurationManager.AppSettings["FtpServerAddress"].ToString() + "/" + path_yql.Substring(4));

                    if (status == FtpStatusCode.ClosingData)
                    {
                        if (File.Exists(path_yql))
                        {
                            //删除文件
                            File.Delete(path_yql);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        catch (Exception ee)
        {
            Response.Write(ee.ToString());

            return false;
        }
    }

    private FtpStatusCode UploadFun(string fileName, string uploadUrl)
    {
        Stream requestStream = null;
        FileStream fileStream = null;
        FtpWebResponse uploadResponse = null;

        try
        {
            FtpWebRequest uploadRequest =
            (FtpWebRequest)WebRequest.Create(uploadUrl);
            uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;

            uploadRequest.Proxy = null;
            NetworkCredential nc = new NetworkCredential();
            nc.UserName = ConfigurationManager.AppSettings["FtpServerUserName"].ToString();
            //nc.Password = ConfigurationManager.AppSettings["FtpServerPassword"].ToString();
            nc.Password = Encoding.Default.GetString(Convert.FromBase64String(ConfigurationManager.AppSettings["FtpServerPassword"].ToString()));
            nc.Domain = "";
            uploadRequest.Credentials = nc;


            requestStream = uploadRequest.GetRequestStream();
            fileStream = File.Open(fileName, FileMode.Open);

            byte[] buffer = new byte[1024];
            int bytesRead;
            while (true)
            {
                bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                    break;
                requestStream.Write(buffer, 0, bytesRead);
            }
            requestStream.Close();
            fileStream.Close();
            uploadResponse = (FtpWebResponse)uploadRequest.GetResponse();
            return uploadResponse.StatusCode;

        }
        catch (UriFormatException ex)
        {
            return FtpStatusCode.Undefined;
        }
        catch (IOException ex)
        {
            return FtpStatusCode.Undefined;
        }
        catch (WebException ex)
        {
            return FtpStatusCode.Undefined;
        }
        finally
        {
            if (uploadResponse != null)
                uploadResponse.Close();
            if (fileStream != null)
                fileStream.Close();
            if (requestStream != null)
                requestStream.Close();
        }
    }

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "need")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            LinkButton linkImport = (LinkButton)gvlist.Rows[index].FindControl("linkImport");

            string _id = gvlist.DataKeys[index].Values["id"].ToString();
            string _fob = gvlist.DataKeys[index].Values["fob"].ToString();
            string _type = linkImport.Text;

            //在不需要订单时候，必须找到一张匹配的订单，通常存在FOB中
            //恢复订单的时候，则不要考虑，因为被替代的订单已经被移走
            if (_type == "需要")
            {
                _type = "need";
                if (string.IsNullOrEmpty(gvlist.DataKeys[index].Values["fob"].ToString()) || (!getEdiData.CheckPoExist(_fob)))
                {
                    ltlAlert.Text = "window.showModalDialog('EdiHrdReplace.aspx?id=" + _id + "&type=" + _type + "&rt=" + DateTime.Now.ToString() + "', window, 'dialogHeight: 350px; dialogWidth: 500px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                    ltlAlert.Text += "window.location.href = 'EdiHrdList.aspx?rt=" + DateTime.Now.ToFileTime().ToString() + "'";
                    return;
                }
            }
            else
            {
                _type = "notneed";
            }

            if (!getEdiData.UpdatePoHrdNeedProp(_id, _fob, Session["PlantCode"].ToString(), _type))
            {
                ltlAlert.Text = "alert('更新失败！');";
                return;
            }

            gvBind(true);
        }
        else if (e.CommandName == "plan")
        {
            LinkButton linkPlan = (LinkButton)e.CommandSource;
            int index = ((GridViewRow)linkPlan.Parent.Parent).RowIndex;
            string _nbr = ((Label)gvlist.Rows[index].FindControl("Label1")).Text.Trim();

            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@nbr", _nbr);
                param[1] = new SqlParameter("@uID", Session["uID"]);
                param[2] = new SqlParameter("@uName", Session["uName"]);

                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_submitToPlan", param);
            }
            catch
            {
                ltlAlert.Text = "alert('操作失败！刷新后重新操作一次！');";
                return;
            }

            gvBind(true);
        }
        else if (e.CommandName == "bigorder")
        {
            LinkButton linkBigOrder = (LinkButton)e.CommandSource;
            int index = ((GridViewRow)linkBigOrder.Parent.Parent).RowIndex;
            string _nbr = ((Label)gvlist.Rows[index].FindControl("Label1")).Text.Trim();

            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@nbr", _nbr);
                param[1] = new SqlParameter("@uID", Session["uID"]);
                param[2] = new SqlParameter("@uName", Session["uName"]);

                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_showBigOrder", param);
            }
            catch
            {
                ltlAlert.Text = "alert('操作失败！刷新后重新操作一次！');";
                return;
            }

        }
        else if (e.CommandName == "CancelDEI")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string hrdid = gvlist.DataKeys[index].Values["id"].ToString();
            LinkButton linkCancel = (LinkButton)gvlist.Rows[index].FindControl("linkCancel");
            getEdiData.UpdatePoHrdCancelProp(hrdid, Session["PlantCode"].ToString(), "整单");

            gvBind(true);

        }
    }
    protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        gvUniqueID = gvTemp.UniqueID;
        if (e.CommandName == "ToPlan")
        {
            getEdiData.updateDetToPlan(e.CommandArgument.ToString().Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            gvBind(true);
        }
        else if (e.CommandName == "need")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            LinkButton linkImport = (LinkButton)gvTemp.Rows[index].FindControl("linkImport");
            string type = linkImport.Text;
            string id = gvTemp.DataKeys[index].Values["id"].ToString();
            getEdiData.UpdatePoDetNeedProp(id, Session["PlantCode"].ToString(), type);
            gvBind(true);

        }
        else if (e.CommandName == "CancelDEI")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvTemp.DataKeys[index].Values["id"].ToString();
            LinkButton linkCancel = (LinkButton)gvTemp.Rows[index].FindControl("linkCancel");

            getEdiData.UpdatePoHrdCancelProp(id, Session["PlantCode"].ToString(), "行");

            gvBind(true);
        }
        else if (e.CommandName == "split")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvTemp.DataKeys[index].Values["id"].ToString();
            string poLine=gvTemp.DataKeys[index].Values["poLine"].ToString();
            string qty = gvTemp.DataKeys[index].Values["ordQty"].ToString();
            string qadPart = gvTemp.DataKeys[index].Values["qadPart"].ToString();
            string partNbr = gvTemp.DataKeys[index].Values["partNbr"].ToString();
            string date = gvTemp.DataKeys[index].Values["det_PoRecDate"].ToString();
            string url = string.Format("/EDI/EdiPoDetSplitLine.aspx?detId={0}&poLine={1}&ordQty={2}&qadPart={3}&partNbr={4}&date={5}", id, poLine, qty, qadPart, partNbr, date);
            ltlAlert.Text = "$.window('明细',1000,800,'" + url.ToString() + "');";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateDetErrorMsgInBatch");

            this.gvBind(true);

            ltlAlert.Text = "alert('更新完成！现在可以导出了！');";
        }
        catch
        {
            ltlAlert.Text = "alert('更新失败！请刷新后重新运行！');";
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (rbFinish.Checked == true || rbPartError.Checked)
        {
            if (txtDate.Text != string.Empty)
            {
                DataTable dt = GetSoDet();
                UpdateSO(dt);
                try
                {
                    DateTime _dt = Convert.ToDateTime(txtDate.Text);

                    ltlAlert.Text = "window.open('EDI850QADExportExcel.aspx?date=" + txtDate.Text.Trim() + "', '_blank');";
                    //DataTable dtExcel = getEdiData.get850QADExcelData(Session["uID"].ToString().Trim(), txtDate.Text, Session["PlantCode"].ToString()).Tables[0];
                    //string title = "<b>订单日期</b>~^<b>港口</b>~^<b>TCP订单号</b>~^<b>客户订单号</b>~^<b>SW2</b>~^<b>截止日期</b>~^<b>序号</b>~^<b>QAD订单号</b>~^<b>QAD号编码</b>~^<b>产品型号</b>~^<b>SKU</b>~^<b>订购数量(套)</b>~^<b>制地</b>~^<b>处理意见</b>~^<b>订单操作域</b>~^<b>收货人地址</b>~^<b>计划日期</b>~^";
                    //this.ExportExcel(title, dtExcel, false);
                }
                catch
                {
                    ltlAlert.Text = "alert('日期格式不正确!');";

                    return;
                }
            }
        }
        else
        {
            if (txtDate.Text != string.Empty)
            {
                DataTable dt = GetSoDet();
                UpdateSO(dt);
                try
                {
                    DateTime _d = Convert.ToDateTime(txtDate.Text);
                    txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                catch
                {
                    ltlAlert.Text = "alert('日期格式不正确!');";
                    return;
                }
            }
            //string EXTitle = "<b>接收日期</b>~^<b>进QAD时间</b>~^<b>客户代码</b>~^<b>客户名称</b>~^<b>港口</b>~^<b>运输方式</b>~^<b>客户订单号</b>~^<b>SW1</b>~^<b>SW2</b>~^<b>序号</b>~^<b>QAD订单号</b>~^<b>描述</b>~^<b>QAD号编码</b>~^<b>产品型号</b>~^<b>订购数量(套)</b>~^<b>数量(只)</b>~^<b>域</b>~^<b>销售单地点</b>~^<b>制地</b>~^<b>备注</b>~^<b>TCP客户订单号</b>~^<b>价格</b>~^<b>价格*0.91</b>~^<b>描述</b>~^<b>裸灯QAD号</b>~^<b>处理意见</b>~^<b>订单操作域</b>~^<b>收货人地址</b>~^<b>SKU</b>~^<b>审批结果</b>~^<b>样品</b>~^<b>创建人</b>~^<b>芯片QAD</b>~^<b>用量</b>~^<b>保税库存</b>~^<b>一般库存</b>~^";
            string EXTitle = "<b>接收日期</b>~^<b>进QAD时间</b>~^<b>客户代码</b>~^<b>客户名称</b>~^<b>港口</b>~^<b>运输方式</b>~^<b>客户订单号</b>~^<b>SW1</b>~^<b>SW2</b>~^<b>序号</b>~^<b>QAD订单号</b>~^<b>描述</b>~^<b>QAD号编码</b>~^<b>产品型号</b>~^<b>订购数量(套)</b>~^<b>数量(只)</b>~^<b>域</b>~^<b>销售单地点</b>~^<b>制地</b>~^<b>备注</b>~^<b>TCP客户订单号</b>~^<b>价格</b>~^<b>价格*0.91</b>~^<b>描述</b>~^<b>裸灯QAD号</b>~^<b>处理意见</b>~^<b>订单操作域</b>~^<b>收货人地址</b>~^<b>SKU</b>~^<b>审批结果</b>~^<b>样品</b>~^<b>创建人</b>~^";//~^<b>芯片QAD</b>~^<b>用量</b>~^<b>保税库存</b>~^<b>一般库存</b>~^";

            DataSet ds = getEdiData.getExcelData(txtDate.Text.Trim(), Session["PlantCode"].ToString());
            //this.ExportExcel(EXTitle, ds.Tables[0], false, 32, "poNbr", "poLine");
            this.ExportExcel(EXTitle, ds.Tables[0], false, 14, "poNbr", "poLine");
        }
    }

    public DataTable GetSo()
    {
        string strConn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.Conn9"];
        string strSQL = "Select so_nbr,so_po,so_ord_date,so_due_date from PUB.so_mstr Where so_ord_date>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' with (nolock)";
        return OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL).Tables[0];
    }

    public DataTable GetSoDet()
    {
        string strConn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.Conn9"];
        string strSQL = "Select so_nbr,sod_line,so_po,so_ord_date,so_due_date from PUB.sod_det inner join PUB.so_mstr on so_nbr=sod_nbr and so_domain=sod_domain Where so_ord_date>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and so_domain in ('SZX','ZQZ','ZQL','TCB','YQL') with (nolock)";
        return OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL).Tables[0];
    }

    public void UpdateSO(DataTable dt)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@detail", xmlDetail);

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateSo", param);
    }
    protected void btnImport_TCB_Click(object sender, EventArgs e)
    {
        GetSelectDetId();
        if (HiddenField1.Value == ";")
        {
            ltlAlert.Text = "alert('请选择数据！');";
        }
        else
        {
            string ids = HiddenField1.Value.Substring(1, HiddenField1.Value.Length - 2).Replace(";", ",");
            DataTable dt = getEdiData.GetEdiPoDetWithIds(ids);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string line = "订单" + row["poNbr"].ToString() + "第" + row["poLine"].ToString() + "行";
                    if (string.IsNullOrWhiteSpace(row["qadPart"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的QAD号为空！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["site"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的地点为空！');";
                        return;
                    }
                    if (row["appvResult"].ToString() == "0" && row["shipto"].ToString() != "310118" && row["cusCode"].ToString() != "CS310139" && row["cusCode"].ToString() != "CS310118")
                    {
                        ltlAlert.Text = "alert('" + line + "没有审批通过！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["qad_dueDate"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的截止日期为空！');";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(row["qad_perfDate"].ToString()))
                    {
                        ltlAlert.Text = "alert('" + line + "的截止日期为空！');";
                        return;
                    }
                }
                getEdiData.InsertEdiPoLoadTemp(ids,Session["uID"].ToString());
                AppServer appsv = new AppServer();
                try
                {
                    if (appsv.RPCMETHOD("d:\\sotcbbatch\\xxsotcbld.bat"))
                    {
                        DataTable dtError = getEdiData.GetEdiPoLoadTempError(Session["uID"].ToString());
                        if (dtError.Rows.Count > 0)
                        {
                            string title = "120^<b>Po No</b>~^60^<b>Line</b>~^300^<b>Error Message</b>~^";
                            this.ExportExcel(title, dtError, false);
                        }
                        else
                        {
                            ltlAlert.Text = "alert('导入成功！');";
                        }
                    }
                    else
                    {
                        ltlAlert.Text = "alert('导入失败！');";
                    }
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入失败！');";
                }
                gvBind(false);
            }
        }
        

    }
}