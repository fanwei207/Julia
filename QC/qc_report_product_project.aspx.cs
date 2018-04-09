using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QCProgress;
using System.IO;

public partial class QC_qc_report_product_project : BasePage
{
    QC oqc = new QC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Request.QueryString["group"].ToString();

            //lbOrder.Text = Request.QueryString["order"].ToString();
            ////lbLine.Text = Request.QueryString["line"].ToString();
            //lbPart.Text = Request.QueryString["part"].ToString();
            //lbguest.Text = Request.QueryString["guest"].ToString();

           
            this.Bind();
            this.bindUpload();
        }
    }

    private void bindUpload()
    {


        string isPass = string.Empty;

        gvSize.DataSource = oqc.selectReportProductImportByGroup(Request.QueryString["group"].ToString()
            , Session["PlantCode"].ToString(),out isPass);
        gvSize.DataBind();

        if (gvSize.Rows.Count == 0)
        {
            gvSize.Visible = false;
            btnFail.Visible = false;
            btnPass.Visible = false;
        }
        else
        {
            gvSize.Visible = true;
            btnFail.Visible = true;
            btnPass.Visible = true;
        }
        if (Request["finish"] != null)
        {
            gvSize.Columns[2].Visible=false;
            btnFail.Enabled = false;
            btnPass.Enabled = false;
        }

        if (isPass == "1")
        {
            btnPass.ForeColor = System.Drawing.Color.Green;
            btnFail.ForeColor = System.Drawing.Color.Black;
            if (!btnPass.Enabled)
            {
                btnFail.ForeColor = System.Drawing.Color.Gray;
            }
            
        }
        else if (isPass == "0")
        {
           

            btnPass.ForeColor = System.Drawing.Color.Black;
            btnFail.ForeColor = System.Drawing.Color.Red;
            if (!btnPass.Enabled)
            {
                btnPass.ForeColor = System.Drawing.Color.Gray;
            }
        }
        else
        {
            btnPass.ForeColor = System.Drawing.Color.Black;
            btnFail.ForeColor = System.Drawing.Color.Black;

            if (!btnPass.Enabled)
            {
                btnPass.ForeColor = System.Drawing.Color.Gray;
                btnFail.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
    private void Bind()
    {

        string order = string.Empty;
        string line = string.Empty;
        string part = string.Empty;
        string guest = string.Empty;

       gvlist.DataSource = oqc.selectSuppBasisListByReportGroup(Request.QueryString["group"].ToString(), Session["PlantCode"].ToString()
           , out order, out line, out part, out guest);
        gvlist.DataBind();


        lbOrder.Text = order;
        lbLine.Text = line;
        lbPart.Text = part;
        lbguest.Text = guest;

    }


    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (Request["undo"] != null)
        {
            Response.Redirect("qc_report_product_undo.aspx");
        }
        else if (Request["do"] != null)
        {
            Response.Redirect("qc_report_product.aspx");
        }
        else if (Request["finish"] != null)
        {
            Response.Redirect("qc_report_product_complete.aspx");
        }
        
    }
    protected void gvSize_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "lkbtndelete")
        {
            string importID = e.CommandArgument.ToString();
            
            int index = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;
            string  URL = gvSize.DataKeys[index].Values["URL"].ToString();

            if (oqc.deleteReportProductImport(importID, Convert.ToInt32(Session["uID"]), Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('删除成功！');";

                File.Delete(Server.MapPath(URL));
                bindUpload();

            }
            else
            {

                ltlAlert.Text = "alert('删除失败！');";
            }

        }
        if (e.CommandName == "lkbtnView")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        this.Bind();
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "download")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = gvrow.RowIndex;
            string[] param = e.CommandArgument.ToString().Split(',');
            string vend = param[0].ToString();
            string docid = param[1].ToString();
            string path = param[2].ToString();
            string status = param[3].ToString();
            string url = string.Empty;
            if (status == "False")
            {
                if (string.IsNullOrEmpty(path))
                {
                    url = "/TecDocs/Supplier/" + vend + "/" + docid;
                }
                else
                {
                    url = path + docid;
                }
            }
            else
            {
                url = path + gvlist.Rows[index].Cells[2].Text;
            }
            ltlAlert.Text = "var w=window.open('" + url + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        
        string path = "/TecDocs/QC/";
        string fileName = string.Empty;//原文件名
        string filePate = string.Empty;//文件路径+文件名（存储的）
        if (string.Empty.Equals(fileSize.PostedFile.FileName))
        {
            ltlAlert.Text = "alert('上传路径不能为空');";
            return;

        }
        else
        {
            if (!ImportFile(ref fileName, ref filePate, path, fileSize))
            {
                ltlAlert.Text = "alert('上传文件失败，请联系管理员');";
                return;
            }
            //else 
            //{
            //    ltlAlert.Text = "alert('上传文件成功！');";
            //}
        }
        if (oqc.uploadReportProductBasis(Request.QueryString["group"].ToString(), fileName, filePate, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('上传文件成功！');";
            bindUpload();
        }
        else
        {

            ltlAlert.Text = "alert('上传文件失败，请联系管理员！');";
            return;
        }
    }
    protected void gvSize_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSize.PageSize = e.NewPageIndex;
        bindUpload();
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
    protected void btnPass_Click(object sender, EventArgs e)
    {
        if (isPass("1"))
        {
            ltlAlert.Text = "alert('判定通过！');";
            bindUpload();
        }
        else
        {
            ltlAlert.Text = "alert('失败，请联系管理员！');";
        }
    }
    protected void btnFail_Click(object sender, EventArgs e)
    {
        if (isPass("0"))
        {
            ltlAlert.Text = "alert('判定不通过！');";
            bindUpload();
        }
        else
        {
            ltlAlert.Text = "alert('失败，请联系管理员！');";
        }
    }

    protected bool isPass(string isPass)
    {
        return oqc.updateReportProductIsPass(Request.QueryString["group"].ToString(), isPass,Session["uID"].ToString(),Session["uName"].ToString());
    }
}