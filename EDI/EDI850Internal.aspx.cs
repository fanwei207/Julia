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
using TCPCHINA.ODBCHelper;
using CommClass;

public partial class EDI_EDI850Internal : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvBind("NotInQad",txtContact.Text.Trim());
        }
    }

    private void gvBind(string filter,string contact)
    {
        DataSet dsPo = getEdiData.getEdiPoHrdInternal(filter, Convert.ToString(Session["PlantCode"]), contact, Convert.ToString(Session["uName"]));

        if (dsPo.Tables[0].Rows.Count == 0)
        {
            dsPo.Tables[0].Rows.Add(dsPo.Tables[0].NewRow());
            dsPo.Tables[0].Rows[0]["HRD_consignment"] = "false";
        }

        gvlist.DataSource = dsPo;
        gvlist.DataBind();
    }
    protected void rbNormal_CheckedChanged(object sender, EventArgs e)
    {
        gvBind("NotInQad", txtContact.Text.Trim());
        gvlist.Columns[0].Visible = true;
        btnImport.Visible = true;
    }
    protected void rbFinish_CheckedChanged(object sender, EventArgs e)
    {
        gvBind("hasInQad", txtContact.Text.Trim());
        gvlist.Columns[0].Visible = false;
        btnImport.Visible = false;
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (rbNormal.Checked == true)
        {
            //ltlAlert.Text = "window.open('ExportInternal850Excel.aspx?filter=NotInQad&contact="+txtContact.Text.Trim()+"')";
            DataTable dt = getEdiData.get850QADExcelDataInternal("NotInQad", Session["PlantCode"].ToString().Trim(), txtContact.Text.Trim(), Session["uName"].ToString().Trim(), "").Tables[0];
            string title = "<b>采购订单号</b>~^<b>采购员</b>~^<b>截止日期</b>~^<b>运输方式</b>~^<b>行号</b>~^<b>物料号</b>~^<b>单位</b>~^<b>订购数量</b>~^<b>单价</b>~^<b>行需求日期</b>~^<b>行截止日期</b>~^<b>地点</b>~^<b>联系人</b>~^<b>备注</b>~^<b>QAD销售订单</b>~^";
            this.ExportExcel(title, dt, false);
        }

        if (rbFinish.Checked == true)
        {
            string strsql = "";
            string tempSonbr = "";
            DataSet ds;
            int m;
            strsql = "select HRD_Id, HRD_PoNbr,HRD_Domain  from Internal850Hrd where HRD_QadSonbr is null or HRD_QadSonbr = ''";
            ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strsql);
            for (m = 0; m < ds.Tables[0].Rows.Count; m++)
            {
                strsql = "select so_nbr from pub.so_mstr where so_po='" + ds.Tables[0].Rows[m][1].ToString().Trim() + "' and so_domain = '" + ds.Tables[0].Rows[m][2].ToString().Trim() + "'";
                tempSonbr = Convert.ToString(OdbcHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn9"), CommandType.Text, strsql));

                strsql = "update Internal850Hrd set HRD_QadSonbr = '" + tempSonbr + "' where HRD_Id=" + ds.Tables[0].Rows[m][0].ToString().Trim();
                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strsql);
            }

            //ltlAlert.Text = "window.open('ExportInternal850Excel.aspx?filter=hasInQad&contact=" + txtContact.Text.Trim() + "&date=" + txtDate.Text + "')";
            DataTable dt = getEdiData.get850QADExcelDataInternal("hasInQad", Session["PlantCode"].ToString().Trim(), txtContact.Text.Trim(), Session["uName"].ToString().Trim(), txtDate.Text).Tables[0];
            string title = "<b>采购订单号</b>~^<b>采购员</b>~^<b>截止日期</b>~^<b>运输方式</b>~^<b>行号</b>~^<b>物料号</b>~^<b>单位</b>~^<b>订购数量</b>~^<b>单价</b>~^<b>行需求日期</b>~^<b>行截止日期</b>~^<b>地点</b>~^<b>联系人</b>~^<b>备注</b>~^<b>QAD销售订单</b>~^";
            this.ExportExcel(title, dt, false);
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
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
            ltlAlert.Text = "alert('请选择导入文件');";
            return;
        }

        HiddenField1.Value = HiddenField1.Value.Substring(0, HiddenField1.Value.Length - 1);
        if (HiddenField1.Value != "")
        {
            ordList = HiddenField1.Value.Split(',');

            if (export(ordList, Convert.ToString(Session["PlantCode"])))
            {
                ltlAlert.Text = "alert('导入QAD成功');";
                HiddenField1.Value = "";
                foreach (string i in ordList)
                {
                    getEdiData.updateOrdStatusInternal(i, GetConsignment(i));
                }
            }
            else
            {
                ltlAlert.Text = "alert('导入QAD失败');";
            }
        }
        if (rbNormal.Checked)
        {
            this.gvBind("NotInQad", txtContact.Text.Trim());
        }
        else if (rbFinish.Checked)
        {
            this.gvBind("hasInQad", txtContact.Text.Trim());
        }
    }

    protected bool GetConsignment(string ordid)
    {
        bool bRet = false;

        foreach (GridViewRow gvr in gvlist.Rows)
        {
            HtmlInputCheckBox chkImport = gvr.FindControl("chkImport") as HtmlInputCheckBox;
            CheckBox chk = (CheckBox)gvr.FindControl("CheckBox1");

            if (chkImport.Value.Trim() == ordid)
                bRet = chk.Checked;
        }

        return bRet;
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.Cells[9].FindControl("CheckBox1")).Checked = bool.Parse(gvlist.DataKeys[e.Row.RowIndex].Value.ToString());

            if (rbFinish.Checked)
            {
                ((CheckBox)e.Row.Cells[9].FindControl("CheckBox1")).Enabled = false;
            }
            else
            {
                if (((Label)e.Row.FindControl("Label1")).Text.ToString().Trim() != string.Empty)
                    ((CheckBox)e.Row.Cells[9].FindControl("CheckBox1")).Checked = true;
                else
                    ((CheckBox)e.Row.Cells[9].FindControl("CheckBox1")).Visible = false;
            }

            if (((Label)e.Row.FindControl("lblPoId")) != null)
            {
                Label lblPoId = (Label)e.Row.FindControl("lblPoId");

                e.Row.Attributes.Add("OnDblClick", "window.open('EDI850InternalDet.aspx?po_id=" + lblPoId.Text.Trim() + "');");

                if ((HtmlInputCheckBox)e.Row.FindControl("chkImport") != null)
                {
                    HtmlInputCheckBox chkImport = (HtmlInputCheckBox)e.Row.FindControl("chkImport");
                    if (HiddenField1.Value.Trim().IndexOf(lblPoId.Text) != -1)
                    {
                        chkImport.Checked = true;
                    }
                    else
                    {
                        chkImport.Checked = false;
                    }

                    if (lblPoId.Text.Trim() == "")
                    {
                        chkImport.Visible = false;
                    }
                    else
                    {
                        chkImport.Visible = true;
                    }
                }

                if (e.Row.Cells[9].Text.Trim() != String.Empty && e.Row.Cells[9].Text.Trim() != "&nbsp;")
                {
                    HtmlInputCheckBox chkImport = (HtmlInputCheckBox)e.Row.FindControl("chkImport");
                    chkImport.Checked = false;
                    e.Row.Cells[0].Enabled = false;
                }
            }

            //若有错误，则显示更新按钮
            if (e.Row.Cells[9].Text == string.Empty || e.Row.Cells[9].Text.Trim() == "&nbsp;")
            {
                e.Row.Cells[11].Text = string.Empty;
            }
        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
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
        gvlist.PageIndex = e.NewPageIndex;

        if (rbNormal.Checked)
        {
            this.gvBind("NotInQad", txtContact.Text.Trim());
        }
        else if (rbFinish.Checked)
        {
            this.gvBind("hasInQad", txtContact.Text.Trim());
        }
    }

    private Boolean export(string[] ordList, string plantCode)
    {
        DataSet ds = null;
        Int32 n, m;
        string path;
        string domain;
        if (plantCode == "2")
            domain = "zql";
        else if (plantCode == "5")
            domain = "yql";
        else if (plantCode == "1")
            domain = "szx";
        else if (plantCode == "6")
            domain = "zqz";
        else if (plantCode == "8")
            domain = "hql";
        else
            return false;
        path = @"c:\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + domain + ".tcp";
        string ISA = "";
        if (File.Exists(path))
        {
            //删除文件
            File.Delete(path);
        }

        StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));

        foreach (string ordid in ordList)
        {
            bool chk = GetConsignment(ordid);

            ds = getEdiData.createISAInternal(ordid);
            for (n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                for (m = 0; m < ds.Tables[0].Columns.Count; m++)
                {
                    ISA = ISA + ds.Tables[0].Rows[n][m].ToString().Trim();
                    if (m + 1 != ds.Tables[0].Columns.Count)
                    {
                        ISA = ISA + "*";
                    }
                    else
                    {
                        break;
                    }
                }
            }
            ISA = ISA + "\r\n";

            ds= null;

            ds = getEdiData.createEDI850HRDInternal(ordid);

            ds.Tables[0].Columns.Add("consi");

            if (chk)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    row["consi"] = "Yes";
                }
            }
            else
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    row["consi"] = "No";
                }
            }


            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    writer.Write(ISA);
                    for (n = 0; n < ds.Tables[0].Rows.Count; n++)
                    {
                        for (m = 0; m < ds.Tables[0].Columns.Count; m++)
                        {
                            writer.Write(ds.Tables[0].Rows[n][m].ToString().Trim());
                            if (m + 1 != ds.Tables[0].Columns.Count)
                            {
                                if (m != 0)
                                {
                                    writer.Write("\"");
                                    writer.Write("*");
                                    writer.Write("\"");
                                }
                                else
                                {
                                    writer.Write("*");
                                    writer.Write("\"");
                                }
                            }
                            else
                            {
                                writer.Write("\"");
                                break;
                            }
                        }
                        writer.Write("\r\n");
                    }
                    ds = getEdiData.createEDI850DETInternal(ordid);
                    ds.Tables[0].Columns.Add("consi");

                    if (chk)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            row["consi"] = "Yes";
                        }
                    }
                    else
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            row["consi"] = "No";
                        }
                    }

                    for (n = 0; n < ds.Tables[0].Rows.Count; n++)
                    {
                        for (m = 0; m < ds.Tables[0].Columns.Count; m++)
                        {
                            writer.Write(ds.Tables[0].Rows[n][m].ToString().Trim());
                            if (m + 1 != ds.Tables[0].Columns.Count)
                            {
                                if (m != 0)
                                {
                                    writer.Write("\"");
                                    writer.Write("*");
                                    writer.Write("\"");
                                }
                                else
                                {
                                    writer.Write("*");
                                    writer.Write("\"");
                                }
                            }
                            else
                            {
                                writer.Write("\"");
                                break;
                            }
                        }
                        writer.Write("\r\n");
                    }
                }
            }
            ISA = "";
           ds= null;
        }
        if (writer != null)
        {
            writer.Close();

            FileInfo file = new FileInfo(path);

            FtpStatusCode status = UploadFun(path, "ftp://" + ConfigurationManager.AppSettings["FtpServerAddress"].ToString() + "/" + file.Name);
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
            return true;
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
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (rbNormal.Checked == true)
        {
            this.gvBind("NotInQad", txtContact.Text.Trim());
        }

        if (rbFinish.Checked == true)
        {
            this.gvBind("hasInQad", txtContact.Text.Trim());
        }
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myUpdate")
        {
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;

            string nbr = ((Label)gvlist.Rows[index].Cells[2].FindControl("Label1")).Text.Trim();
            string domain = ((Label)gvlist.Rows[index].Cells[5].FindControl("Label5")).Text.Trim();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@nbr", nbr);
            param[1] = new SqlParameter("@domain", domain);
            
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateEdiPoHrdError", param);

            this.gvBind("NotInQad", txtContact.Text.Trim());
        }
    }
}
