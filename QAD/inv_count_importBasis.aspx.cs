using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using adamFuncs;
using System.IO;

public partial class QAD_inv_count_importBasis : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        /*
            1,传入的值有该单的id
            2，通过id查出对应的已经存在的上传文件
         */
        if (!IsPostBack)
        {
            lbID.Text = Request.QueryString["invid"].ToString(); //获取id
            Bind();
        
        }
    }
    /*
     绑定数据方法
     */
    private void Bind()
    { 
        /*
         * 查数据将数据显示到gridview
         */
        gvBasisInfo.DataSource = this.getBasisInfo();
        gvBasisInfo.DataBind();
    
    }

    /// <summary>
    /// 查出导入的数据列表
    /// </summary>
    /// <returns></returns>
    private DataTable getBasisInfo()
    {
        try 
        {
            string sqlstr = "sp_inv_getBasisInfo";

            SqlParameter[] param = new  SqlParameter[]{
                new SqlParameter("@MSTRID",lbID.Text.ToString())
            };

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        }
        catch
        {
            return null;
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!filename.HasFile)
        {
            ltlAlert.Text = "alert('文件不存在')";
            return;
        }

        if (filename.FileName.IndexOf("#") > 0)
        {
            ltlAlert.Text = "alert('文件名不可包含#，请重命名后上传')";
            return;
        }

       string fname = filename.FileName;//得到上传的完整路径
       int intLastBackslash = fname.LastIndexOf("\\");//获取最后一个斜杠的位置
       fname = fname.Substring(intLastBackslash + 1);//得到文件名
       string suffix = fname.Substring(fname.LastIndexOf(".") + 1 ); //后缀
       string fnameIn = fname.Substring(0, fname.LastIndexOf(".") - 1);//纯文件名
       string url = "/TecDocs/Inventory/" + fnameIn + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + suffix; //存储文件的url路径

       if (fname.Trim().Length <= 0)
       {
           ltlAlert.Text = "alert('请选文件')";
           return;
       
       }


       try
       {
           filename.MoveTo(Server.MapPath(url), Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
           if (this.addNewBasis(fname, url,lbID.Text.ToString()))
           {
               ltlAlert.Text = "alert('上传成功！')";
               Bind();
           }
           else
           {
               File.Delete(Server.MapPath(url));
               ltlAlert.Text = "alert('插入数据库失败，请联系管理员')";
               return;
           }
       }
       catch
       {
           if (File.Exists(Server.MapPath(url)))
           {
               File.Delete(Server.MapPath(url));
           }
           ltlAlert.Text = "alert('保存文件失败，请联系管理员')";
           return;
       }
    }

    private bool addNewBasis(string fname,string url,string ID)
    {
        int uID = Convert.ToInt32(Session["uID"].ToString());
        try
        {
            string sqlstr = "sp_inv_AddNewBasis";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@fname",fname)
                , new SqlParameter("@url",url)
                , new SqlParameter("@ID",ID)
                , new SqlParameter("@uID",uID)
            };

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));
        }
        catch
        {
            return false; 
        }
    }



    protected void gvBasisInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnView")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        if (e.CommandName == "lkbtndelete")
        {
            string  url = e.CommandArgument.ToString();

             GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)); //此得出的值是表示那行被选中的索引值
            int importID =Convert.ToInt32(gvBasisInfo.DataKeys[drv.RowIndex].Values["importID"]);

            /*
               0-失败数据库错误
             * 1-正确运行
             * -1 - 修改人与创建人不相符
             */
            int flag = DeleteForID( importID,Convert.ToInt32(Session["uID"].ToString()));

            if (flag == 1)
            {
                if (File.Exists(Server.MapPath(url)))
                {
                    File.Delete(Server.MapPath(url));
                }
                Bind();
                ltlAlert.Text = "alert('删除成功！')";
            }
            else if (flag == 0)
            {
                ltlAlert.Text = "alert('删除失败！')";
            }
            else
            {
                ltlAlert.Text = "alert('只有上传文件者有操作该文件的权限')";
            
            }
        }
    }

    private int DeleteForID(int ID,int uID)
    {
        try
        {
            string sqlstr = "sp_Inv_DeleteForID";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@ID",ID)
                , new SqlParameter("@uID",uID)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));
        }
        catch
        {
            return 0;
            
        }
    }
    protected void gvBasisInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvBasisInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBasisInfo.PageIndex = e.NewPageIndex;
        Bind();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("inv_count_mstr.aspx");
    }
}