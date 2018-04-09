using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class part_NPart_AddPcApply : BasePage
{

    PC_price pc = new PC_price();//数据固化类

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvbind();
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        gvbind();
    }
    private void gvbind()
    {
        gvDet.DataSource = pc.selectApplyPassList(txtQAD.Text.Trim(), txtCode.Text.Trim(), txtvendor.Text.Trim(), txtVendorName.Text.Trim(),Session["uID"].ToString(),ddlSou.SelectedValue);
        gvDet.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {


        if(ddlType.SelectedValue == "0")
        {

            ltlAlert.Text = "alert('请选择类型再添加！');";
            return;
        }

        DataTable table = new DataTable("NPartAdd");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "id";
        table.Columns.Add(column);

        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            CheckBox chk = gvRow.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {
                
                row = table.NewRow();
                row["id"] = gvDet.DataKeys[gvRow.RowIndex].Values["id"].ToString();

                table.Rows.Add(row);
            }
        }


        if (table.Rows.Count > 0)
        {
            string PQID = string.Empty;
            string Status = "0";
            string reason = string.Empty;
            string uName = string.Empty;
            bool isout = false;
            if (pc.AddNewPQFromNPart(table, Session["uID"].ToString(), out PQID, out uName, isout, ddlType.SelectedValue))
            {
                Response.Redirect("../price/pc_PriceApply.aspx?PQID=" + PQID + "&Status=" + Status + "&uName=" + uName + "&ApplyDate=" + string.Format("{0:d}", DateTime.Now) + "&AppliByID=" + Convert.ToInt32(Session["uID"]) + "&isout=false");

            }
            else
            {
                ltlAlert.Text = "alert('添加申请失败，请重试');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择数据！')";
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../price/pc_PriceApplyList.aspx");
    }
    protected void btnDownLode_Click(object sender, EventArgs e)
    {
        DataTable errDt = pc.selectUpdateFormateTemp(txtQAD.Text.Trim(), txtCode.Text.Trim(), txtvendor.Text.Trim(), txtVendorName.Text.Trim(), Session["uID"].ToString(), ddlSou.SelectedValue);//输出错误信息
        string title = "100^<b>QAD</b>~^100^<b>供应商</b>~^100^<b>单位</b>~^100^<b>需求规格</b>~^";
        if (errDt != null && errDt.Rows.Count > 0)
        {
            ExportExcel(title, errDt, false);
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        ImportExcelFile();
        gvbind();
    }

    public void ImportExcelFile()
    {

        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }
        }

        strUserFileName = filename.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }

        if (filename.PostedFile != null)
        {
            if (filename.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);//上传 文件
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {


                    DataTable errDt = null;
                    DataTable dt = null;
                    bool success = false;
                    try
                    {
                        //dt = adam.getExcelContents(filePath).Tables[0];
                        //NPOIHelper helper = new NPOIHelper();
                        dt = GetExcelContents(strFileName);
                    }
                    catch (Exception ex)
                    {
                        ltlAlert.Text = "alert('导入文件必须是Excel格式a');";

                    }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }
                    }

                    string message = "";
                    try
                    {
                        success = pc.importNPartPassFormate(dt, out message, Session["uID"].ToString());//插入，
                    }
                    catch { }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }

                    }
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                        }
                    }
                    else
                    {
                        ltlAlert.Text = "alert('" + message + "')";
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式a');";
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }
    }
    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string flag=gvDet.DataKeys[e.Row.RowIndex].Values["NPartStatus"].ToString();//-10 是驳回 其他是空

            if (flag == "-10")
            {
                ((Label)e.Row.FindControl("lbFrom")).Text = "驳回";
            }
            else if (flag == "-15")
            {
                ((Label)e.Row.FindControl("lbFrom")).Text = "关闭申请";
            }
            else
            {
                ((Label)e.Row.FindControl("lbFrom")).Text = "申请";
            }

        }
    }
    protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbDelete")
        {
            string id = e.CommandArgument.ToString();

            if (pc.deleteNPartFromAddApplyPage(id, Session["uID"].ToString(),Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('删除成功');";
                gvbind();
            }
            else
            {

                ltlAlert.Text = "alert('删除失败');";
            }
        }
    }
    protected void btnModel_Click(object sender, EventArgs e)
    {
        string title = "100^<b>QAD</b>~^100^<b>供应商</b>~^160^<b>单位</b>~^100^<b>技术参考价</b>~^100^<b>需求规格</b>~^100^<b>备注</b>~^";

        string[] titleSub = title.Split(new char[] { '~' });

        DataTable dtExcel = new DataTable("temp");
        DataColumn col;

        foreach (string colName in titleSub)
        {
            col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = colName;
            dtExcel.Columns.Add(col);
        }

        ExportExcel(title, dtExcel, false);
    }
    //protected void btnImportUM_Click(object sender, EventArgs e)
    //{
    //    ImportExcelFileUM();
    //    gvbind();
    //}


    //public void ImportExcelFileUM()
    //{

    //    string strFileName = "";
    //    string strCatFolder = "";
    //    string strUserFileName = "";
    //    int intLastBackslash = 0;

    //    string strUID = Convert.ToString(Session["uID"]);
    //    string struName = Convert.ToString(Session["uName"]);

    //    strCatFolder = Server.MapPath("/import");
    //    if (!Directory.Exists(strCatFolder))
    //    {
    //        try
    //        {
    //            Directory.CreateDirectory(strCatFolder);
    //        }
    //        catch
    //        {
    //            ltlAlert.Text = "alert('上传文件失败.');";
    //            return;
    //        }
    //    }

    //    strUserFileName = fileUpdateUM.PostedFile.FileName;
    //    intLastBackslash = strUserFileName.LastIndexOf("\\");
    //    strFileName = strUserFileName.Substring(intLastBackslash + 1);
    //    if (strFileName.Trim().Length <= 0)
    //    {
    //        ltlAlert.Text = "alert('请选择导入文件.');";
    //        return;
    //    }
    //    strUserFileName = strFileName;

    //    int i = 0;
    //    while (i < 1000)
    //    {
    //        strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
    //        if (!File.Exists(strFileName))
    //        {
    //            break;
    //        }
    //        i += 1;
    //    }

    //    if (filename.PostedFile != null)
    //    {
    //        if (filename.PostedFile.ContentLength > 8388608)
    //        {
    //            ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
    //            return;
    //        }

    //        try
    //        {
    //            filename.PostedFile.SaveAs(strFileName);//上传 文件
    //        }
    //        catch
    //        {
    //            ltlAlert.Text = "alert('上传文件失败.');";
    //            return;
    //        }

    //        if (File.Exists(strFileName))
    //        {
    //            try
    //            {


    //                DataTable errDt = null;
    //                DataTable dt = null;
    //                bool success = false;
    //                try
    //                {
    //                    dt = GetExcelContents(strFileName);
    //                }
    //                catch (Exception ex)
    //                {
    //                    ltlAlert.Text = "alert('导入文件必须是Excel格式a');";

    //                }
    //                finally
    //                {
    //                    if (File.Exists(strFileName))
    //                    {
    //                        File.Delete(strFileName);
    //                    }
    //                }

    //                string message = "";
    //                try
    //                {
    //                    success = pc.importNPartPassUM(dt, out message, strUID,struName, out errDt);//插入，
    //                }
    //                catch 
    //                {
                    
    //                }
    //                finally
    //                {
    //                    if (File.Exists(strFileName))
    //                    {
    //                        File.Delete(strFileName);
    //                    }

    //                }
    //                if (success)
    //                {
    //                    if (message != "")
    //                    {
    //                        ltlAlert.Text = "alert('" + message + "')";
    //                    }
    //                }
    //                else
    //                {

    //                    string title = "100^<b>QAD</b>~^100^<b>供应商</b>~^160^<b>单位</b>~^100^<b>技术参考价</b>~^100^<b>需求规格</b>~^100^<b>备注</b>~^100^<b>错误信息</b>~^";
    //                    ltlAlert.Text = "alert('" + message + "')";
    //                    if (errDt != null && errDt.Rows.Count > 0)
    //                    {
    //                        ExportExcel(title, errDt, false);
    //                    }
    //                }

    //            }
    //            catch (Exception ex)
    //            {
    //                ltlAlert.Text = "alert('导入文件必须是Excel格式a');";
    //            }
    //            finally
    //            {
    //                if (File.Exists(strFileName))
    //                {
    //                    File.Delete(strFileName);
    //                }
    //            }
    //        }
    //    }
    //}
}