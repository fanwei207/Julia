using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class price_pc_ViewBasis : BasePage
{



    PC_price pc = new PC_price();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbIMID.Text = Request["IMID"].ToString();




            if ("2".Equals(Request["status"].ToString()))
            {
                ddlImport.SelectedValue = "1";
            }
            else
            {
                ddlImport.SelectedValue = Request["status"].ToString(); 
            }
            
            bind();
        }
    }
    protected void gvBasisInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBasisInfo.PageIndex = e.NewPageIndex;
        bind();
    }
    private void bind()
    {
        DataTable dt = pc.viewBasis(lbIMID.Text);
       gvBasisInfo.DataSource = dt;
       gvBasisInfo.DataBind();
    }
    protected void gvBasisInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int type = Convert.ToInt32( e.Row.Cells[1].Text.ToString());
            if (type == 0)
            {
                e.Row.Cells[1].Text = "报价";
            }
            else if (type == 1)
            {
                e.Row.Cells[1].Text = "核价";
            }
        }
    }
    protected void gvBasisInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtndelete")
        {
            if (pc.updateBasis(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["uID"])))
            {
                ltlAlert.Text = "alert('删除成功！');";
                bind();
            
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
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("pc_InquiryDet.aspx?IMID=" + lbIMID.Text + "&ComeFrom=" + Request["ComeFrom"].ToString() + "&vender=" + Request["vender"].ToString() + "&ItemCode="
                + Request["ItemCode"].ToString() + "&QAD=" + Request["QAD"].ToString() + "&VenderName=" + Request["VenderName"].ToString() + "&chkSelf=" + Request["chkSelf"].ToString() + "&ddlStatus=" + Request["ddlStatus"].ToString() + "&txtIMID=" + Request["txtIMID"].ToString());
    }
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="_fileName"></param>
    /// <param name="_filePath"></param>
    /// <returns></returns>
    protected bool ImportFile(ref string _fileName, ref string _filePath, string path)
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


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        int type = 0;
        string path = "/TecDocs/Quotation/";
        string fileName = string.Empty;//原文件名
        string filePate = string.Empty;//文件路径+文件名（存储的）
        if (string.Empty.Equals(filename.PostedFile.FileName))
        {
            ltlAlert.Text = "alert('上传路径不能为空');";
            return;

        }
        else
        {
            if (!ImportFile(ref fileName, ref filePate, path))
            {
                ltlAlert.Text = "alert('上传文件失败，请联系管理员');";
                return;
            }
            //else 
            //{
            //    ltlAlert.Text = "alert('上传文件成功！');";
            //}
        }
        if (!"0".Equals(ddlImport.SelectedItem.Value))
        {
            type = 1;
        }
        if (pc.insertBasis(lbIMID.Text, Convert.ToInt32(Session["uID"]), fileName, filePate, type))
        {
            ltlAlert.Text = "alert('上传文件成功！');";
            bind();
        }
        else
        {

            ltlAlert.Text = "alert('上传文件失败，请联系管理员！');";
            return;
        }
    }
}