using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


public partial class SampleDelivery_SampleDeliveryDocImport : BasePage
{
    SampleDeliveryHelper helper = new SampleDeliveryHelper();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("320250", "送样单上传文件");
        }

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!this.Security["320250"].isValid)
            {
                Response.Redirect("~/public/denied.aspx", true);
            }
            else
            {
                lblNbr.Text = Request.QueryString["bsdNbr"].ToString();

                lblPart.Text = Request.QueryString["bsddetCode"].ToString();
                lblQad.Text = Request.QueryString["bsddetQad"].ToString();
                lblMstrId.Text = Request.QueryString["bsd_mstrid"];
                lblDetId.Text = Request.QueryString["bsdDetId"].ToString();

                BindData();
            }
            if (Request.QueryString["isChecked"].ToLower() == "true")
            {
                uploadPartBtn.Enabled = false;
            }
        }
    }

    private void BindData()
    {
        int detId = int.Parse(lblDetId.Text);
        IList<SampleDeliveryDocImport> docs = helper.GetSampleDeliveryDocImport(detId);
        gvlist.DataSource = docs;
        DataBind();
    }

    protected void uploadPartBtn_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (filename.Value.Trim() == string.Empty)
        {
            this.Alert("请先选择一个文件！");
            BindData();
            return;
        }
        else
        {
            if (Path.GetFileName(filename.PostedFile.FileName).IndexOf("#") > 0)
            {
                this.Alert("文件名包含非法字符:#");
                return;
            }

            if (Path.GetFileName(filename.PostedFile.FileName).IndexOf("%") > 0)
            {
                this.Alert("文件名包含非法字符:%");
                return;
            }

            ImportFile();
        }
    }

    protected void ImportFile()
    {
        string _FileName = Path.GetFileName(filename.PostedFile.FileName);//文件名，包含后缀
        string _ExtentName = Path.GetExtension(filename.PostedFile.FileName);//文件后缀
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../import/"), DateTime.Now.ToFileTime().ToString() + "-" + _FileName);//合并两个路径为上传到服务器上的全路径

        string bosNbr = lblNbr.Text;
        string bosCode = lblPart.Text;
        string bosQad = lblQad.Text;
        string uploadby = Convert.ToString(Session["uLogin"]);

        if (File.Exists(SaveFileName))
        {
            try
            {
                File.Delete(SaveFileName);
            }
            catch
            {
                this.Alert("删除临时文件失败！请刷新后重新操作一次！");
                return;
            }
        }

        try
        {
            filename.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            this.Alert("保存临时文件失败！请刷新后重新操作一次！");
            return;
        }

        string path = "/TecDocs/SampleDelivery/";
        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        string docId = bosNbr + "-" + bosQad.ToString() + "-" + DateTime.Now.ToFileTime().ToString() + _ExtentName;
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
            int detId = int.Parse(lblDetId.Text);
            SampleDeliveryDocImport doc = new SampleDeliveryDocImport();
            doc.FileDescription = docDescs;
            doc.FileName = _FileName;
            doc.FilePath = "/TecDocs/SampleDelivery/";
            doc.UploadBy = Convert.ToInt32(Session["uID"]);
            doc.VirtualFileName = docId;

            if (helper.AddSampleDeliveryDocImport(detId, doc) > 0)
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "c")
        {
            Response.Redirect("SampleDeliveryCheckMaintenance.aspx?bsd_mstrid=" + lblMstrId.Text.ToString());
        }
        else
        {
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                Response.Redirect("SampleDeliveryMaintenance.aspx?bsd_mstrid=" + lblMstrId.Text.ToString() + "&mid=" + Request.QueryString["mid"].ToString());
            }
            else
            {
                Response.Redirect("SampleDeliveryMaintenance.aspx?bsd_mstrid=" + lblMstrId.Text.ToString());
            }
        }
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "download")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = gvrow.RowIndex;
            string[] param = e.CommandArgument.ToString().Split(',');
            string docid = param[0].ToString();
            string path = param[1].ToString();
            if (string.IsNullOrEmpty(path))
            {
                ltlAlert.Text = "window.open('/TecDocs/SampleDelivery/" + docid + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300')";
            }
            else
            {
                ltlAlert.Text = "window.open('" + path + docid + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300')";
            }
        }
        else if (e.CommandName.ToString() == "del")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            int id = Convert.ToInt32(param[0]);
            string docid = param[1].ToString().Trim();
            SampleDeliveryDocImport doc = new SampleDeliveryDocImport(id);
            if (helper.DeleteSampleDeliveryDocImport(doc))
            {
                if (File.Exists(Server.MapPath("/TecDocs/SampleDelivery/" + docid)))
                {
                    try
                    {
                        File.Delete(Server.MapPath("/TecDocs/SampleDelivery/"  + docid));
                    }
                    catch
                    {
                        this.Alert("删除失败，请联系管理员!");
                    }
                }
                BindData();

            }
            else
            {
                this.Alert("删除失败，请联系管理员!");
            }
        }
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