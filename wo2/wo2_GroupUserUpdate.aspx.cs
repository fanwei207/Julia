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
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using WOrder;

public partial class wo2_wo2_GroupUserUpdate : BasePage
{
    adamClass adam = new adamClass();
    WorkOrder wk = new WorkOrder ();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FileTypeDropDownList1.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            FileTypeDropDownList1.Items.Add(item1);
        }
    }

    protected void BtnRouting_ServerClick(object sender, EventArgs e)
    {
        //DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
       
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
                return ;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return ;
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

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return ;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return ;
            }



            if (File.Exists(strFileName))
            {

                try
                {
                   //ds = adam.getExcelContents(strFileName);
                   dt = this.GetExcelContents(strFileName);
                   
                }
                catch 
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + e.ToString() + "'.');";
                    return ;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                string StrGroupcode;
                string StrMop;
                string StrSop;
                string StrUsercode;

                string strGroupNo;
                strGroupNo = "";
                // The Excel have datas

                wk.DelTmpData (Convert.ToInt32(Session["Uid"]),Convert.ToInt32(Session["PlantCode"])) ;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns[0].ColumnName.Trim () != "用户组代码" ||
                       dt.Columns[1].ColumnName.Trim() != "工序代码" ||
                       dt.Columns[2].ColumnName.Trim() != "岗位代码" ||
                       dt.Columns[3].ColumnName.Trim() != "工号" ||
                       dt.Columns[4].ColumnName.Trim() != "姓名")
                    {
                        //ds.Reset();
                        ltlAlert.Text = "alert('导入文件的模版不正确!');";
                        return;
                    }

                   

                    bool bolGroup = false;
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        // User Group Code
                        if (dt.Rows[i].IsNull(0))
                        {
                            ltlAlert.Text = "alert('行" + (i + 2).ToString() + ",用户组代码必须要填！');";
                            //ds.Reset();
                            bolGroup = true;
                            return;
                        }
                       

                        //Workshop Code
                        if (dt.Rows[i].IsNull(1))
                        {
                            ltlAlert.Text = "alert('行" + (i + 2).ToString() + ",工序代码必须要填！');";
                            //ds.Reset();
                            bolGroup = true;
                            return;
                        }
                        
                                            
                        //Position Code
                        if (dt.Rows[i].IsNull(2))
                        {
                            ltlAlert.Text = "alert('行" + (i + 2).ToString() + ",岗位代码必须要填！');";
                            //ds.Reset();
                            bolGroup = true;
                            return;
                        }
                       

                        // User
                        if (dt.Rows[i].IsNull(3) || dt.Rows[i].IsNull(4))
                        {
                            ltlAlert.Text = "alert('行" + (i + 2).ToString() + ",工号或姓名必须要填！');";
                            //ds.Reset();
                            bolGroup = true;
                            return;
                        }


                        // Get Group information
                        StrGroupcode = wk.CheckGroupCode(dt.Rows[i].ItemArray[0].ToString().Trim (),Convert.ToInt32(Session["PlantCode"]));
                        if (StrGroupcode.Trim().Length <= 0)
                        {
                            ltlAlert.Text = "alert('行" + (i + 2).ToString() + ",用户组代码不存在！');";
                            //ds.Reset();
                            bolGroup = true;
                            return;
                        }
                        else
                        {
                            if (!strGroupNo.Contains(dt.Rows[i].ItemArray[0].ToString()))
                            {
                                strGroupNo = dt.Rows[i].ItemArray[0].ToString();
                                wk.DelGroupDetail(StrGroupcode, Convert.ToInt32(Session["PlantCode"]));
                            }
                        }

                        StrMop = wk.CheckMop(dt.Rows[i].ItemArray[1].ToString(),0);
                        if (StrMop.Trim().Length <= 0)
                        {
                            ltlAlert.Text = "alert('行" + (i + 2).ToString() + ",工序代码不存在！');";
                            //ds.Reset();
                            bolGroup = true;
                            return;
                        }

                        StrSop = wk.CheckSop(dt.Rows[i].ItemArray[2].ToString(), Convert.ToInt32(dt.Rows[i].ItemArray[1].ToString()));
                        if (StrSop.Trim().Length <= 0)
                        {
                            ltlAlert.Text = "alert('行" + (i + 2).ToString() + ",岗位代码不存在！');";
                            //ds.Reset();
                            bolGroup = true;
                            return;
                        }
                        // End Group information

                        // Get User ID
                        StrUsercode = wk.CheckUser(dt.Rows[i].ItemArray[3].ToString(), dt.Rows[i].ItemArray[4].ToString(), Convert.ToInt32(Session["PlantCode"]));
                        if (StrUsercode.Trim().Length <= 0)
                        {
                            ltlAlert.Text = "alert('行" + (i + 2).ToString() + ",该员工不存在或已离职！');";
                            //ds.Reset();
                            bolGroup = true;
                            return;
                        }
                        //

                        if (wk.UpdateGroup(Convert.ToInt32(StrUsercode), dt.Rows[i].ItemArray[3].ToString(), dt.Rows[i].ItemArray[4].ToString(), StrGroupcode, StrMop, StrSop, Convert.ToInt32(Session["Uid"]), Convert.ToInt32(Session["PlantCode"]), 0) < 0)
                        {
                            ltlAlert.Text = "alert('导入出错1，请重新导入！');";
                            //ds.Reset();
                            bolGroup = true;
                            return;
                        }
                    }

                    if (!bolGroup)
                    {
                        if (wk.UpdateGroup(0, "", "", "", "", "", Convert.ToInt32(Session["Uid"]), Convert.ToInt32(Session["PlantCode"]), 1) < 0)
                        {
                            ltlAlert.Text = "alert('导入出错2，请重新导入！');";
                            //ds.Reset();
                            return;
                        }
                        else
                        {
                            ltlAlert.Text = "alert('保存成功！');";
                        }
                    }
                }
                //ds.Reset();
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
            }
        }
    } // End Import process

}

