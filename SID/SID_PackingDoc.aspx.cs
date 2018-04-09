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
using adamFuncs;
using QADSID;
using Microsoft.ApplicationBlocks.Data;

using System.Reflection;
using System.IO;
using CommClass;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class SID_PackingDoc : BasePage
{
    adamClass chk = new adamClass();
    //SID sid = new SID();
    SID_Packing packing = new SID_Packing();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFile();
        }
    }

    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSID_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData(); 
        BindFile();
    }

    /// <summary>
    /// ��һ���´���
    /// </summary>
    /// <param name="url"></param>
    public void OpenWindow(string url)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OpenWindow", "window.open('" + url + "', '_blank','menubar=yes,scrollbars=yes,resizable=yes,width=800,height=500,top=0,left=0');", true);
    }


    protected void btn_save_Click(object sender, EventArgs e)
    {
        string nbr = txtShipNo.Text.Trim();
        string boxno = txt_boxno.Text.Trim();//lblPo.Text.Trim();
        string bl = txt_bl.Text.Trim();//lblDelivery.Text.Trim();
        string shipto = txt_shipto.Text.Trim();
        int uid = Convert.ToInt32(Session["uID"].ToString());
        string lcno = txt_lcno.Text.Trim();

        //packing.SaveInfo(nbr, boxno, bl, shipto, uid, lcno, 0, "");
        //packing.SaveInfo(nbr, boxno, bl, shipto, uid, lcno, 0, nbrno, shipdate, "");
        //if (!string.IsNullOrEmpty(txt_boxno.Text) && !string.IsNullOrEmpty(txt_bl.Text))
        //{
        //    SqlParameter[] parm = new SqlParameter[3];
        //    parm[0] = new SqlParameter("@user", "A6069");
        //    parm[1] = new SqlParameter("@pwd", string.Empty);
        //    parm[2] = new SqlParameter("@plantCode", Session["PlantCode"]);
        //    SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_sid_sendemail_BL", parm);
        //    ltlAlert.Text = "alert('Success! \\n Have send email to USA:\t" + "\\n �ύ��Ϣͨ��;\\n');";
        //}
        //if (!string.IsNullOrEmpty(txt_boxno.Text) || string.IsNullOrEmpty(txt_bl.Text))
        //{
        //    SqlParameter[] parm = new SqlParameter[3];
        //    parm[0] = new SqlParameter("@user", "A6069");
        //    parm[1] = new SqlParameter("@pwd", string.Empty);
        //    parm[2] = new SqlParameter("@plantCode", Session["PlantCode"]);
        //    SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_sid_sendemail_BL", parm);
        //    ltlAlert.Text = "alert('Success! \\n Have send email to USA:\t" + "\\n �ύ��Ϣͨ��;\\n');";

        //}
    }

    protected void BindData()
    {
        //�������
        System.Data.DataTable nbr = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
        txtShipNo.Text = nbr.Rows[0]["sid_pk"].ToString();
        string strNbr = nbr.Rows[0]["sid_fob"].ToString();
        string strShipNo = txtShipNo.Text.Trim();


        if (!string.IsNullOrEmpty(strShipNo))
        {
            System.Data.DataTable poMstr1 = packing.SelectExportInfo(strShipNo);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
            //if (poMstr1.Rows.Count <= 0)
            //{
            //    this.Alert("�˳��˵��Ų����ڣ�");
            //    return;
            //}
            if (poMstr1.Rows.Count > 0)
            {
                txt_boxno.Text = poMstr1.Rows[0]["sid_boxno"].ToString().Trim();
                txt_bl.Text = poMstr1.Rows[0]["sid_bl"].ToString().Trim();
                txt_shipto.Text = poMstr1.Rows[0]["sid_shipto"].ToString().Trim();
                txt_lcno.Text = poMstr1.Rows[0]["sid_lcno"].ToString().Trim();
            }
            else
            {
                txt_boxno.Text = string.Empty;
                txt_bl.Text = string.Empty;
                txt_shipto.Text = string.Empty;
            }
        }
    }

    protected void BindFile()
    {
        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
        txtShipNo.Text = nbr1.Rows[0]["sid_pk"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
        string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();
        string pk = nbr1.Rows[0]["sid_pk"].ToString();

        if (Request.QueryString["type"] != null)
        {
            this.dg.DataSource = SID_PackingFile.GetDocument(nbr).Tables[0];
            this.dg.DataBind();
            BindData(); 
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {

        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

        //string sid_shipno = txtShipNo.Text;
        string sid_shipno = nbr1.Rows[0]["sid_fob"].ToString();
        if (string.IsNullOrEmpty(sid_shipno))
        {
            ltlAlert.Text = "alert('���˵��Ų���Ϊ��!');";
            return;
        }
        else
        {
            //System.Data.DataTable poMstr1 = packing.SelectExportInfo(sid_shipno);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
            //if (poMstr1.Rows.Count <= 0)
            //{
            //    this.Alert("�˳��˵��Ų����ڣ�");
            //    return;
            //}
        }

        if (File1.PostedFile.FileName != string.Empty)
        {
            if (File1.PostedFile.ContentLength <= 0 || File1.PostedFile.ContentLength >= 8 * 1024 * 1024)
            {
                ltlAlert.Text = "alert('�ϴ����ļ����Ϊ 8 MB!');";
                return;
            }

            string _filename = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf('\\') + 1); ;

            Int32 bytes = File1.PostedFile.ContentLength;

            string _tempPath = Server.MapPath("/import/") + _filename;
            string _logicalPath = Server.MapPath("/TecDocs/SID/");
            string _physicalFile = "";

            if (File1.PostedFile.ContentLength > 0)
            {
                try
                {
                    File1.PostedFile.SaveAs(_tempPath);
                }
                catch
                {
                    ltlAlert.Text = "alert('�ĵ��ϴ�ʧ��!');";
                    return;
                }

                _physicalFile = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(_tempPath);
                string ext = SID_PackingFile.GetFileExtension(_tempPath);

                if (SID_PackingFile.CheckDocument(ext))
                {

                    ext = Path.GetExtension(_tempPath);

                    if (!SID_PackingFile.InsertDocument(sid_shipno,_filename,doctype.SelectedValue.ToString(), ext, _physicalFile, Convert.ToInt32(Session["uID"]), Session["uName"].ToString()))
                    {
                        this.Alert("�ļ�����ʧ�ܣ���ˢ�º����²���һ�飡");
                    }
                    else
                    {
                        try
                        {
                            File.Move(_tempPath, _logicalPath + _physicalFile);

                            this.Alert("�ļ�����ɹ���");
                        }
                        catch
                        {
                            this.Alert("�ļ�ת��ʧ�ܣ���ɾ����¼�������ϴ���");
                        }
                    }
                }
                else
                {
                    this.Alert("�ĵ���ʽ����ȷ������ϵ����Ա����ʽ���룺" + ext);

                }
            }
            else
            {
                this.Alert("�����ϴ����ĵ�!");
            }
        }
        else
        {
            this.Alert("����ָ��һ��Ҫ�ϴ����ĵ�!");
        }

        BindFile();
    }

    protected void dg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != string.Empty)
            {
            }
        }
    }

    protected void dg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dg.PageIndex = e.NewPageIndex;

        BindFile();
    }

    protected void dg_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!SID_PackingFile.DeleteDocument(dg.DataKeys[e.RowIndex].Values["sid_doc_id"].ToString(), Convert.ToInt32(Session["uID"]), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('����ʧ��,����Ȩɾ�����ĵ�,ֻ�ܲ����Լ��������ļ�!');";
            return;
        }
        string FilePath = Server.MapPath("/TecDocs/ShareDocs/" + dg.DataKeys[e.RowIndex].Values["sid_doc_path"].ToString());
        if (File.Exists(FilePath))
        {
            //�������ɾ��
            File.Delete(FilePath);
        }
        BindFile();
    }

    protected void dg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownLoad")
        {
            LinkButton link = (LinkButton)e.CommandSource;

            int index = ((GridViewRow)(link.Parent.Parent)).RowIndex;

            string _filePath = dg.DataKeys[index].Values["sid_doc_path"].ToString();
            ltlAlert.Text = "var w=window.open('/TecDocs/SID/" + _filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

            BindFile();
        }
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SID/SID_PackingList1.aspx?type=temp", true);
    }
}
