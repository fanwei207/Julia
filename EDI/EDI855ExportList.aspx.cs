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

public partial class EDI_EDI855ExportList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.gvBind("A");
        }
    }

    private void gvBind(string filter)
    {
        DataSet dslist = getEdiData.get855ExportList(filter);

        if (dslist.Tables[0].Rows.Count == 0)
        {
            dslist.Tables[0].Rows.Add(dslist.Tables[0].NewRow());
        }

        gvlist.DataSource = dslist;
        gvlist.DataBind();
    }
    protected void rb855Accept_CheckedChanged(object sender, EventArgs e)
    {
        rb855Accept.Checked = true;
        rb855Change.Checked = false;
        rb865Accept.Checked = false;
        this.gvBind("A");
    }
    protected void rb855Change_CheckedChanged(object sender, EventArgs e)
    {
        rb855Accept.Checked = false;
        rb855Change.Checked = true;
        rb865Accept.Checked = false;
        this.gvBind("C");
    }
    protected void rb865Accept_CheckedChanged(object sender, EventArgs e)
    {
        rb855Accept.Checked = false;
        rb855Change.Checked = false;
        rb865Accept.Checked = true;
        this.gvBind("M");
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("lblPoId")) != null)
            {
                Label lblPoId = (Label)e.Row.FindControl("lblPoId");
                if ((HtmlInputCheckBox)e.Row.FindControl("chkExport") != null)
                {
                    HtmlInputCheckBox chkExport = (HtmlInputCheckBox)e.Row.FindControl("chkExport");
                    if (HiddenField1.Value.Trim().IndexOf(lblPoId.Text) != -1)
                    {
                        chkExport.Checked = true;
                    }
                    else
                    {
                        chkExport.Checked = false;
                    }

                    if (lblPoId.Text.Trim() == "")
                    {
                        chkExport.Visible = false;
                    }
                    else
                    {
                        chkExport.Visible = true;
                    }
                }
            }
        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        foreach (GridViewRow gvr in gvlist.Rows)
        {
            HtmlInputCheckBox chkExport = gvr.FindControl("chkExport") as HtmlInputCheckBox;
            if (chkExport.Checked)
            {
                if (HiddenField1.Value.Trim().IndexOf(chkExport.Value + ",") == -1)
                {
                    HiddenField1.Value = HiddenField1.Value + chkExport.Value + ",";
                }
            }
            else
            {
                if (HiddenField1.Value.Trim().IndexOf(chkExport.Value + ",") != -1)
                {
                    HiddenField1.Value = HiddenField1.Value.Replace(chkExport.Value + ",", "");
                }
            }
        }
        gvlist.PageIndex = e.NewPageIndex;

        if (rb855Accept.Checked)
        {
            this.gvBind("A");
        }
        else if (rb855Change.Checked)
        {
            this.gvBind("C");
        }
        else if (rb865Accept.Checked)
        {
            this.gvBind("M");
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string[] ordList;

        string filter = "";
        foreach (GridViewRow gvr in gvlist.Rows)
        {
            HtmlInputCheckBox chkExport = gvr.FindControl("chkExport") as HtmlInputCheckBox;
            if (chkExport.Checked)
            {
                if (HiddenField1.Value.Trim().IndexOf(chkExport.Value + ",") == -1)
                {
                    HiddenField1.Value = HiddenField1.Value + chkExport.Value + ",";
                }
            }
            else
            {
                if (HiddenField1.Value.Trim().IndexOf(chkExport.Value + ",") != -1)
                {
                    HiddenField1.Value = HiddenField1.Value.Replace(chkExport.Value + ",", "");
                }
            }
        }

        if (HiddenField1.Value.Length <=0)
        {
            ltlAlert.Text = "alert('请先选中一项!');";
            return;
        }
        HiddenField1.Value = HiddenField1.Value.Substring(0, HiddenField1.Value.Length - 1);
        if (HiddenField1.Value != "")
        {
            ordList = HiddenField1.Value.Split(',');
            if (this.rb855Accept.Checked == true)
            {
                filter = "A";
            }
            else if (this.rb855Change.Checked == true)
            {
                filter = "C";
            }
            else if (this.rb865Accept.Checked == true)
            {
                filter = "M";
            }

            foreach (string str in ordList)
            {
                getEdiData.updateOrdStatus(str, filter);
            }
        }

        if (rb855Accept.Checked)
        {
            this.gvBind("A");
        }
        else if (rb855Change.Checked)
        {
            this.gvBind("C");
        }
        else if (rb865Accept.Checked)
        {
            this.gvBind("M");
        }
    }

    private Boolean export(string[] ordList, string exportType)
    {
        DataSet ds = null;
        Int32 n, m;
        string path = @"c:\EDI855_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() +".tcp";
        StreamWriter writer = null;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        writer = File.CreateText(path);

        //写855 ST
        string ST = "ST*855\r\n";

        foreach (string ordid in ordList)
        {
            //写855BAK
            ds = getEdiData.create855BAK(ordid, exportType);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    writer.Write(ST);
                    for (n = 0; n < ds.Tables[0].Rows.Count; n++)
                    {
                        for (m = 0; m < ds.Tables[0].Columns.Count; m++)
                        {
                            writer.Write(ds.Tables[0].Rows[n][m].ToString().Trim());
                            if (m + 1 != ds.Tables[0].Columns.Count)
                            {
                                writer.Write("*");
                            }
                        }
                    }
                writer.Write("\r\n");
                }
            }
            //写855 TD5
            ds = getEdiData.create855TD5(ordid, exportType);
            for (n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                for (m = 0; m < ds.Tables[0].Columns.Count; m++)
                {
                    writer.Write(ds.Tables[0].Rows[n][m].ToString().Trim());
                    if (m + 1 != ds.Tables[0].Columns.Count)
                    {
                        writer.Write("*");
                    }
                }
            }
            writer.Write("\r\n");

            //写855 PO1
            ds = getEdiData.create855PO1(ordid, exportType);
            for (n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                for (m = 0; m < ds.Tables[0].Columns.Count; m++)
                {
                    writer.Write(ds.Tables[0].Rows[n][m].ToString().Trim());
                    if (m + 1 != ds.Tables[0].Columns.Count)
                    {
                        writer.Write("*");
                    }
                }
                writer.Write("\r\n");
            }

            getEdiData.insert855His(ordid, exportType, Session["uID"].ToString().Trim(), Session["uName"].ToString().Trim());
            getEdiData.update855Status(ordid,exportType);

        }
        if (writer != null)
        {
            writer.Close();
            FtpStatusCode status = new FtpStatusCode();
            //上传855文件到tcp服务器上
            if (rb855Accept.Checked == true)
            {
                status = UploadFun(path, "ftp://" + ConfigurationManager.AppSettings["FtpServerAddress"].ToString() + "//EDI_EXPORT//F855_ACCEPT//" + path.Substring(4));
            }
            else if (rb855Change.Checked == true)
            {
                status = UploadFun(path, "ftp://" + ConfigurationManager.AppSettings["FtpServerAddress"].ToString() + "//EDI_EXPORT//F855_CHANGE//" + path.Substring(4));
            }
            else if (rb865Accept.Checked == true)
            {
                status = UploadFun(path, "ftp://" + ConfigurationManager.AppSettings["FtpServerAddress"].ToString() + "//EDI_EXPORT//F865_ACCEPT//" + path.Substring(4));
            }

            if (status == FtpStatusCode.ClosingData)
            {
                if (File.Exists(path))
                {
                    //删除文件
                    File.Delete(path);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
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
            nc.Password = Encoding.Default.GetString(Convert.FromBase64String(ConfigurationManager.AppSettings["FtpServerPassword"].ToString()));

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
        }
        catch (IOException ex)
        {
        }
        catch (WebException ex)
        {
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
        return FtpStatusCode.Undefined;

    }

}
