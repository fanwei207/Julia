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
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Collections.Generic;

public partial class EDI_EdiHrdList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvBind("0");
        }
    }

    private void gvBind(string filter)
    {
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

        if (dsPo.Tables[0].Rows.Count == 0)
        {
            dsPo.Tables[0].Rows.Add(dsPo.Tables[0].NewRow());
        }

        DataView dvw = dsPo.Tables[0].DefaultView;

        //if (filter == "0")
        //{
        //    //Button1.Visible = true;
        //    gvlist.Columns[8].Visible = true;
        //}
        //else
        //{
        //    //Button1.Visible = false;
        //    gvlist.Columns[8].Visible = false;
        //}

        //if (filter == "3")
        //{
        //    gvlist.Columns[12].Visible = true;
        //}
        //else
        //{
        //    gvlist.Columns[12].Visible = false;
        //}

        gvlist.DataSource = dvw;
        gvlist.DataBind();
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (rbNormal.Checked) e.Row.Cells[11].Width = new Unit(80);
            else e.Row.Cells[11].Width = new Unit(130);
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Int32 filter = 0;
            if (rbNormal.Checked)
            {
                filter = 0;
            }
            //else if (rbError.Checked)
            //{
            //    filter = 1;
            //}
            //else if (rbPartError.Checked)
            //{
            //    filter = 2;
            //}
            else if (rbFinish.Checked)
            {
                filter = 3;
            }
            //else if (rbRejected.Checked)
            //{
            //    filter = 4;
            //}

            if (((Label)e.Row.FindControl("lblPoId")) != null && ((Label)e.Row.FindControl("lblPoId")).Text != string.Empty)
            {
                if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["detError"]))
                {
                    ((Label)e.Row.FindControl("Label1")).Style["background-color"] = "red";
                }

                //对于应该提交Plan的判定，不需要的，不判定
                //if (((HtmlInputHidden)e.Row.FindControl("hidShouldToPlan")).Value.ToLower() == "true")
                //{
                //    if (((HtmlInputHidden)e.Row.FindControl("hidToPlan")).Value.ToLower() == "true")
                //    {
                //        e.Row.Cells[11].Text = string.Empty;
                //    }
                //}
                //else
                //{
                //    e.Row.Cells[11].Text = string.Empty;
                //}

                Label lblPoId = (Label)e.Row.FindControl("lblPoId");
                
                e.Row.Attributes.Add("OnDblClick", "window.open('EdiDetList.aspx?po_id=" + lblPoId.Text.Trim() + "&filter=" + filter.ToString() + "');");

                if (rbNormal.Checked)
                {
                    e.Row.Cells[11].Width = new Unit(80);
                }
                else
                {
                    e.Row.Cells[11].Width = new Unit(130);
                }

                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            }

            //如果事先没有分配域和地点，则无法导入
            //LinkButton linkImport = (LinkButton)e.Row.FindControl("linkImport");
            //HtmlInputCheckBox chkImport = (HtmlInputCheckBox)e.Row.FindControl("chkImport");

            //try
            //{
            //    if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["notNeeded"]))
            //    {
            //        linkImport.Text = "不导入";

            //        chkImport.Checked = false;
            //        chkImport.Disabled = true;
            //    }
            //    else
            //    {
            //        linkImport.Text = "需要";

            //        //HRD必须维护制地，Det也必须维护制地
            //        if (gvlist.DataKeys[e.Row.RowIndex].Values["domain"].ToString().Length > 0 && Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["detNoSite"]) == false)
            //        {
            //            chkImport.Disabled = false;

            //            if (e.Row.Cells[9].Text.Length == 0 || e.Row.Cells[9].Text == "&nbsp;" || e.Row.Cells[10].Text.Length == 0 || e.Row.Cells[10].Text == "&nbsp;")
            //            {
            //                chkImport.Disabled = true;
            //            }
            //        }
            //        else
            //        {
            //            chkImport.Disabled = true;
            //        }
            //    }
            //}
            //catch
            //{ }

            //try
            //{
            //    LinkButton linkBigOrder = (LinkButton)e.Row.FindControl("linkBigOrder");

            //    if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["inBigOrder"]))
            //    {
            //        linkBigOrder.Text = "不显示";
            //    }
            //    else
            //    {
            //        linkBigOrder.Text = "显示";
            //    }
            //}
            //catch
            //{
            //    ;
            //}
        }
    }

    protected void rbNormal_CheckedChanged(object sender, EventArgs e)
    {
        if (rbNormal.Checked == true)
        {
            this.gvBind("0");
            gvlist.Columns[6].Visible = true;
            //btnImport.Visible = true;
        }
    }

    //protected void rbError_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbError.Checked == true)
    //    {
    //        this.gvBind("1");
    //        gvlist.Columns[6].Visible = false;
    //        btnImport.Visible = false;

    //        txtDate.Text = string.Empty;
    //    }
    //}

    //protected void rbPartError_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbPartError.Checked == true)
    //    {
    //        this.gvBind("2");
    //        gvlist.Columns[6].Visible = false;
    //        btnImport.Visible = false;
    //    }
    //}

    protected void rbFinish_CheckedChanged(object sender, EventArgs e)
    {
        if (rbFinish.Checked == true)
        {
            this.gvBind("3");
            //gvlist.Columns[9].Visible = false;
            //btnImport.Visible = false;
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (rbNormal.Checked)
        {
            if (gvlist.Rows.Count > 0)
            {
                //检验是否35.13.7中存在
                bool isEdiConfig = true;
                foreach (GridViewRow row in gvlist.Rows)
                {
                    if (!Convert.ToBoolean(gvlist.DataKeys[row.RowIndex].Values["isEdiConfig"]))
                    {
                        isEdiConfig = false;
                    }
                }

                if (!isEdiConfig)
                {
                    this.Alert("存在客户未配置35.13.7！");
                    return;
                }

                if (export())
                {

                        //更新状态
                        foreach (GridViewRow row in gvlist.Rows)
                        {
                            string id = gvlist.DataKeys[row.RowIndex].Values["id"].ToString();

                            string domain = gvlist.DataKeys[row.RowIndex].Values["domain"].ToString();

                            string poNbr = gvlist.DataKeys[row.RowIndex].Values["poNbr"].ToString();

                            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

                            //域没有维护的，不更新
                            if (chkImport.Checked)
                            {
                                if (domain != string.Empty)
                                {
                                    try
                                    {
                                        getEdiData.updateOrdStatus1(id);
                                    }
                                    catch (Exception ex)
                                    {
                                        ltlAlert.Text = "alert('订单" + poNbr + "更新状态失败！);";
                                    }
                                }
                            }
                        }
     

                    ltlAlert.Text = "alert('导入成功！');";
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败！');";
                }
            }
        }

        if (rbNormal.Checked)
        {
            this.gvBind("0");
        }
        //else if (rbError.Checked)
        //{
        //    this.gvBind("1");
        //}
        else if (rbFinish.Checked)
        {
            this.gvBind("2");
        }
        //else if (rbRejected.Checked)
        //{
        //    this.gvBind("4");
        //}
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        if (rbNormal.Checked)
        {
            this.gvBind("0");
        }
        //else if(rbError.Checked)
        //{
        //    this.gvBind("1");
        //}
        //else if (rbPartError.Checked)
        //{
        //    this.gvBind("2");
        //}
        else if (rbFinish.Checked)
        {
            this.gvBind("3");
        }
        //else if (rbRejected.Checked)
        //{
        //    this.gvBind("4");
        //}
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

            string strID = string.Empty;
            foreach (GridViewRow row in gvlist.Rows)
            {
                HtmlInputCheckBox chk = (HtmlInputCheckBox)row.FindControl("chkImport");

                if (chk.Checked)
                {
                    strID = strID + gvlist.DataKeys[row.RowIndex].Values["id"].ToString() + ";";
                }
            }

            if (strID.Length == 0)
            {
                return false;
            }

            ds = getEdiData.GetEdiPoHrdExportList1(strID);

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
            }
            
            return true;
        }
        catch(Exception ee)
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

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (rbFinish.Checked == true)
        {
            if (txtDate.Text != string.Empty)
            {
                try
                {
                    DateTime _dt = Convert.ToDateTime(txtDate.Text);

                    ltlAlert.Text = "window.open('EDI850QADExportExcel.aspx?date=" + txtDate.Text.Trim() + "', '_blank');";
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
            string EXTitle = "<b>接收日期</b>~^<b>进QAD时间</b>~^<b>客户代码</b>~^<b>客户名称</b>~^<b>港口</b>~^<b>运输方式</b>~^<b>客户订单号</b>~^<b>SW1</b>~^<b>SW2</b>~^<b>序号</b>~^<b>QAD订单号</b>~^<b>QAD号编码</b>~^<b>描述</b>~^<b>产品型号</b>~^<b>订购数量(套)</b>~^<b>数量(只)</b>~^<b>制地</b>~^<b>备注</b>~^<b>TCP客户订单号</b>~^<b>价格</b>~^<b>价格*0.91</b>~^<b>裸灯QAD号</b>~^<b>描述</b>~^<b>处理意见</b>~^<b>订单操作域</b>~^<b>收货人地址</b>~^<b>审批结果</b>~^";
            DataSet ds = getEdiData.getExcelData(txtDate.Text.Trim(), Session["PlantCode"].ToString());
            this.ExportExcel(EXTitle, ds.Tables[0], false);     
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        string[] ordList;
        foreach (GridViewRow gvr in gvlist.Rows)
        {
            HtmlInputCheckBox chkImport = gvr.FindControl("chkImport") as HtmlInputCheckBox;
            if (chkImport.Checked)
            {
                if (HiddenField1.Value.Trim().IndexOf(chkImport.Value + ",") == -1)
                {
                    HiddenField1.Value = HiddenField1.Value + chkImport.Value + ",";
                }
            }
            else
            {
                if (HiddenField1.Value.Trim().IndexOf(chkImport.Value + ",") != -1)
                {
                    HiddenField1.Value = HiddenField1.Value.Replace(chkImport.Value + ",", "");
                }
            }
        }

        if (HiddenField1.Value.Length <= 0)
        {
            ltlAlert.Text = "alert('Nothing Rejected.');";
            return;
        }

        HiddenField1.Value = HiddenField1.Value.Substring(0, HiddenField1.Value.Length - 1);
        if (HiddenField1.Value != "")
        {
            ordList = HiddenField1.Value.Split(',');

            HiddenField1.Value = "";
            foreach (string i in ordList)
            {
                getEdiData.RejectOrder(i);
            }
        }

        if (rbNormal.Checked)
        {
            this.gvBind("0");
        }
        //else if (rbError.Checked)
        //{
        //    this.gvBind("1");
        //}
        else if (rbFinish.Checked)
        {
            this.gvBind("2");
        }
        //else if (rbRejected.Checked)
        //{
        //    this.gvBind("4");
        //}
    }

    //protected void rbRejected_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbRejected.Checked == true)
    //    {
    //        this.gvBind("4");
    //        gvlist.Columns[6].Visible = false;
    //        btnImport.Visible = false;
    //    }
    //}
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (rbNormal.Checked)
        {
            this.gvBind("0");
        }
        //else if (rbError.Checked)
        //{
        //    this.gvBind("1");
        //}
        //else if (rbPartError.Checked)
        //{
        //    this.gvBind("2");
        //}
        else if (rbFinish.Checked)
        {
            this.gvBind("3");
        }
        //else if (rbRejected.Checked)
        //{
        //    this.gvBind("4");
        //}
    }

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "need")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            LinkButton linkImport = (LinkButton)gvlist.Rows[index].FindControl("linkImport");

            string _id = gvlist.DataKeys[index].Values["id"].ToString();
            string _fob = gvlist.DataKeys[index].Values["fob"].ToString();
            string hrdid = gvlist.DataKeys[index].Values["id"].ToString();
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

            if (!getEdiData.UpdatePoHrdNeedProp(hrdid, _fob, Session["PlantCode"].ToString(), _type))
            {
                ltlAlert.Text = "alert('更新失败！');";
                return;
            }

            if (rbNormal.Checked)
            {
                this.gvBind("0");
            }
            //else if (rbError.Checked)
            //{
            //    this.gvBind("1");
            //}
            //else if (rbPartError.Checked)
            //{
            //    this.gvBind("2");
            //}
            else if (rbFinish.Checked)
            {
                this.gvBind("3");
            }
            //else if (rbRejected.Checked)
            //{
            //    this.gvBind("4");
            //}
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

            if (rbNormal.Checked)
            {
                this.gvBind("0");
            }
            //else if (rbError.Checked)
            //{
            //    this.gvBind("1");
            //}
            //else if (rbPartError.Checked)
            //{
            //    this.gvBind("2");
            //}
            else if (rbFinish.Checked)
            {
                this.gvBind("3");
            }
            //else if (rbRejected.Checked)
            //{
            //    this.gvBind("4");
            //}
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

            if (rbNormal.Checked)
            {
                this.gvBind("0");
            }
            //else if (rbError.Checked)
            //{
            //    this.gvBind("1");
            //}
            //else if (rbPartError.Checked)
            //{
            //    this.gvBind("2");
            //}
            else if (rbFinish.Checked)
            {
                this.gvBind("3");
            }
            //else if (rbRejected.Checked)
            //{
            //    this.gvBind("4");
            //}
        }
        else if (e.CommandName == "CancelDEI")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string hrdid = gvlist.DataKeys[index].Values["id"].ToString();
            LinkButton linkCancel = (LinkButton)gvlist.Rows[index].FindControl("linkCancel");
            getEdiData.UpdatePoHrdCancelProp(hrdid, Session["PlantCode"].ToString(), "整单");

            if (rbNormal.Checked)
            {
                this.gvBind("0");
            }
            //else if (rbError.Checked)
            //{
            //    this.gvBind("1");
            //}
            //else if (rbPartError.Checked)
            //{
            //    this.gvBind("2");
            //}
            else if (rbFinish.Checked)
            {
                this.gvBind("3");
            }
            //else if (rbRejected.Checked)
            //{
            //    this.gvBind("4");
            //}

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateDetErrorMsgInBatch");

            this.gvBind("0");

            ltlAlert.Text = "alert('更新完成！现在可以导出了！');";
        }
        catch
        {
            ltlAlert.Text = "alert('更新失败！请刷新后重新运行！');";
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;

        foreach (GridViewRow row in gvlist.Rows)
        {
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

            if (!chkImport.Disabled)
            {
                chkImport.Checked = chkAll.Checked;
            }
        }
    }
}
