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
using System.IO;
using CommClass;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class supplier_SampleNotesSupplierDocsImport : BasePage
{
    Sample sap = new Sample();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.SecurityCheck = securityCheck.issecurityCheck(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["uRole"]), 320110);
            if (!this.SecurityCheck.isValid)
            {
                Response.Redirect("~/public/ErrMessage.aspx", true);
            }
            else
            {
                string bosnbr = Request.QueryString["bosNbr"].ToString();
                string bosdetCode = Request.QueryString["bosdetCode"].ToString();
                int line = Convert.ToInt32(Request.QueryString["line"]);

                lblVend.Text = Request.QueryString["bosVend"].ToString();
                lblBosNbr.Text = Request.QueryString["bosNbr"].ToString();
                lblLine.Text = Request.QueryString["line"].ToString();
                lblPart.Text = Request.QueryString["bosdetCode"].ToString();
                lblQad.Text = Request.QueryString["bosdetQad"].ToString();

                BindData();
            }
            if (Request.QueryString["isReceipt"] == "True")
            {
                uploadPartBtn.Enabled = false;
            }
        }
        PostBackTrigger trigger = new PostBackTrigger();
        trigger.ControlID = uploadPartBtn.UniqueID;
        ((UpdatePanel)(this.Master.FindControl("UpdatePanel4"))).Triggers.Add(trigger);
    }

    private void BindData()
    {
        string bosNbr = lblBosNbr.Text;
        int line = Convert.ToInt32(lblLine.Text);

        DataTable dt = sap.getBosSuppImportDocs(bosNbr, line);
        if (dt.Rows.Count == 0)
        {

            dt.Rows.Add(dt.NewRow());
            gvlist.DataSource = dt;
            gvlist.DataBind();
            int columnCount = gvlist.Rows[0].Cells.Count;
            gvlist.Rows[0].Cells.Clear();
            gvlist.Rows[0].Cells.Add(new TableCell());
            gvlist.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvlist.Rows[0].Cells[0].Text = "没有记录";
            gvlist.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            gvlist.DataSource = dt;
            gvlist.DataBind();
        }
    }

    protected void uploadPartBtn_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (dropType.SelectedValue == "N")
        {
            this.Alert("请选择一种文件类型");
            BindData();
            return;
        }

        //string strfile = filename.PostedFile.FileName.ToString();
        if (!fileAttachFile.HasFile)
        {
            this.Alert("请先选择一个文件！");
            BindData();
            return;
        }
        else
        {
            if (Path.GetFileName(fileAttachFile.FileName).IndexOf("#") > 0)
            {
                this.Alert("文件名包含非法字符:#");
                return;
            }

            if (Path.GetFileName(fileAttachFile.FileName).IndexOf("%") > 0)
            {
                this.Alert("文件名包含非法字符:%");
                return;
            }

            if (fileAttachFile.ContentLength > 1024 * 1024 * 100)
            {
                this.Alert("文件大于100M，无法上传！");
                return;
            }

            ImportFile();
        }
    }

    protected void ImportFile()
    {
        string _FileName = Path.GetFileName(fileAttachFile.FileName);//文件名，包含后缀
        string _ExtentName = Path.GetExtension(fileAttachFile.FileName);//文件后缀
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../Temp/"), DateTime.Now.ToFileTime().ToString() + "-" + _FileName);//合并两个路径为上传到服务器上的全路径

        string bosNbr = lblBosNbr.Text;
        string vender = lblVend.Text;
        string bosCode = lblPart.Text;
        string bosQad = lblQad.Text;
        int line = Convert.ToInt32(lblLine.Text);
        string uploadby = Convert.ToString(Session["uLogin"]);
        string type = dropType.SelectedValue;
        string typeName = dropType.SelectedItem.Text;

        try
        {
            if (this.fileAttachFile.ContentLength > 0)
            {
                try
                {
                    this.fileAttachFile.MoveTo(SaveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        catch
        {
            this.Alert("保存临时文件失败！请刷新后重新操作一次！");
            return;
        }

        string path = "/TecDocs/Supplier/" + vender + "/";
        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        string docId = bosNbr + "-" + line.ToString() + "-" + DateTime.Now.ToFileTime().ToString() + _ExtentName;
        path = path + docId;
            if (!File.Exists(Server.MapPath(path)))
            {
                try
                {
                    File.Move(SaveFileName, Server.MapPath(path));
                }
                catch
                {
                    this.Alert("上传文件转移失败，请联系管理员！");
                    return;
                }
                

                string docDescs = txt_docsDecs.Text;

                if (sap.insertBosImportDocs(bosNbr, vender, line, bosCode, bosQad, _FileName, docId, docDescs, uploadby, type, typeName, "/TecDocs/Supplier/" + vender + "/"))
                {
                    txt_docsDecs.Text = string.Empty;
                    BindData();
                }
                else
                {
                    this.Alert("文件信息保存失败，请联系管理员！");
                    return;
                }
            }
            else
            {
                this.Alert("文件已经存在，请重新命名！");
                return;
            }
        }
        

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "download")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string vend = param[0].ToString();
            string docid = param[1].ToString();
            this.OpenWindow("/TecDocs/Supplier/" + vend + "/" + docid + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300");
            return;
        }
        else if (e.CommandName.ToString() == "del")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            int id = Convert.ToInt32(param[0]);
            string vend = param[1].ToString().Trim();
            string docid = param[2].ToString().Trim();

            if (sap.deleteUploadDocs(id))
            {
                if (File.Exists(Server.MapPath("/TecDocs/Supplier/" + vend + "/" + docid)))
                {
                    try
                    {
                        File.Delete(Server.MapPath("/TecDocs/Supplier/" + vend + "/" + docid));
                    }
                    catch
                    {
                        this.Alert("删除失败，请联系管理员!");
                    }
                }
                //BindData();
                
            }
            else
            {
                this.Alert("删除失败，请联系管理员!");
            }
            Response.Redirect("SampleNotesSupplierDocsImport.aspx?bosNbr=" + lblBosNbr.Text + "&line=" + lblLine.Text + "&bosdetCode=" + Server.UrlEncode(lblPart.Text).ToString() + "&bosdetQad=" + lblQad.Text + "&bosVend=" + vend + "&rm=" + DateTime.Now.ToString(), true);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SampleNotesVendConfirm.aspx?bos_nbr=" + lblBosNbr.Text.ToString());
    }

    protected void PageChange(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        gvlist.SelectedIndex = -1;

        BindData();
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Request.QueryString["isReceipt"] == "True")
            {
                e.Row.Cells[5].Enabled = false;
            }
        }
    }
}
