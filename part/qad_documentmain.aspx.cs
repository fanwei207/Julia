using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using adamFuncs;
using System.IO;


public partial class Docproject_qad_documentmain : BasePage
{

  
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binddroptype();

            ListItem itemtemp = new ListItem("");
            itemtemp.Value = "";
            catdrop.Items.Add(itemtemp);
            catdrop.SelectedValue = "";

            DataTable temp = createtab();
            datagrid1.DataSource = temp;
            datagrid1.DataBind();


            if (Request.QueryString["flg"] != null)
            {                
                if (Request.QueryString["flg"].ToString() == "1")
                {

                    if (Request.QueryString["code"] != null)
                    {
                        //btnadd.Visible = false;
                        codes.Text = Request.QueryString["code"].ToString();
                        ltlAlert.Text = "$('.remhide').show();";
                    }
                }
                else
                {
                    codes.Text = Request.QueryString["code"].ToString();
                    ltlAlert.Text = "$('.sethide').hide();$('.remhide').show();";
                }

            }

            if (Request.QueryString["gid"] != null)
            {
                gid.Value = Request.QueryString["gid"].ToString();
            }

         
            BindData();
        }
    }

    protected void binddroptype()
    {
        ListItem itemtemp = new ListItem("");
        itemtemp.Value = ""; 
        typedrop.Items.Add(itemtemp);
        string Tsql = "select typeID=typeID,typeName=typename from QadDoc..DocumentCategory group by typeID,typename";
        DataTable ds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, Tsql).Tables[0];
        for (int i = 0; i < ds.Rows.Count; i++)
        {
            ListItem item = new ListItem(ds.Rows[i][1].ToString(), ds.Rows[i][0].ToString());
            typedrop.Items.Add(item);
        }
          
        typedrop.SelectedValue = "";
    }

    protected void binddropcat()
    {
        string ctype = typedrop.SelectedValue;
        catdrop.Items.Clear();
        ListItem itemtemp = new ListItem("");
        itemtemp.Value = "";
        catdrop.Items.Add(itemtemp);
        if (!string.IsNullOrEmpty(ctype))
        {
            string Csql = "select cateid=cateid,catename=catename from QadDoc..DocumentCategory where typeID=" + ctype + " group by cateid,catename";
            DataTable ds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, Csql).Tables[0];
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                ListItem item = new ListItem(ds.Rows[i][1].ToString(), ds.Rows[i][0].ToString());
                catdrop.Items.Add(item);
            }
        } 
        catdrop.SelectedValue = "";
     }

    protected void setdefault(string id)
    {
        string sql = "select typeID,cateID from qaddoc.dbo.NpartDocuments where  id=" + id;
        DataTable sigletab = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, sql).Tables[0];
        if (sigletab.Rows.Count>0 && sigletab!=null)
        {
            try
            {
                typedrop.SelectedValue = sigletab.Rows[0][0].ToString();
                catdrop.SelectedValue  = sigletab.Rows[0][1].ToString();
            }
            catch
            { return; }

        }

    }

    protected string createSql()
    {
        string typeid = typedrop.SelectedValue == null || typedrop.SelectedValue == "" ? "" : typedrop.SelectedValue;
        string catid = catdrop.SelectedValue == null || catdrop.SelectedValue == "" ? "" : catdrop.SelectedValue;
        string strSql = " Select Distinct d.id, Case " + Session["uRole"].ToString() + " When 1 Then 0 Else Case d.createdBy - " + Convert.ToInt32(Session["uID"].ToString()) + " When 0 Then 0 Else Isnull(d.docLevel,3) - da.doc_acc_level End End As docLevel,"
                    + " Isnull(d.docLevel,3) As Level, cnt = Isnull(dt.cnt, 0), vcount = Isnull(dv.vcount, 0), verifycnt = Isnull(verify.cnt, 0), "
                    + " Isnull(d.createdBy,0) As creator ,Isnull(d.filepath,'') As path, "
                    + " isNewMechanism = Isnull(d.isNewMechanism, 0),"
                    + " d.pictureNo, Case d.isPublic When 1 Then 'Yes' Else 'No' End As isPublic,accFileName = Isnull(d.accFileName, N''),"
                    + " d.createdname, d.createdDate, d.name,  isnull(d.modifiedby,0) modifiedby, d.modifiedname,d.modifiedDate, Isnull(d.description,'') As description, d.version, Isnull(d.filename,'') As filename, Case d.isApprove When 1 Then 'Yes' Else 'No' End As Approved, Case d.isall When 1 Then 'Yes' Else 'No' End As IsAll,d.Path as virPath,d.typeid as typeid,d.cateid as cateid,d.typename as typename, catename=d.catename"
                    + " From qaddoc.dbo.NpartDocuments d "
                    + " Left Join(	Select docid, cnt = Count(*) From qaddoc.dbo.documentitem Group By docid ) as dt On dt.docid = d.id "
                    + " Left Join(	Select docid, vcount = Count(*) From qaddoc.dbo.documentvend Group By docid ) as dv On dv.docid = d.id "
                    + " Left Join(	Select docid, typeid, cateid, cnt = Count(*) From qaddoc.dbo.DocumentVerify Where isFailure Is Null Group By docid, typeid, cateid ) As verify On verify.docid = d.id And verify.typeid = d.typeid And verify.cateid = d.cateid "
                    + " Left Outer Join qaddoc.dbo.DocumentAccess da On d.typeID = da.doc_acc_catid ";


              if (Session["uRole"].ToString().Trim() != "1") 
              {
                strSql+= " And da.doc_acc_userid = '" + Session["uID"].ToString() + "' And da.approvedBy Is Not Null ";
              }

              strSql += " where 1=1 ";

              if (!string.IsNullOrEmpty(typeid))
                  strSql += " and d.typeID='" + typeid + "'";

              if (!string.IsNullOrEmpty(catid))
                  strSql += " and d.cateID='" + catid + "'";

              //strSql += " Where d.typeID='" + typeid + "' And  d.cateID='" + catid + "' And da.approvedBy Is Not Null ";
            
              if( !string.IsNullOrEmpty(txbname.Text.Trim()))
              {
                strSql+= " And d.name Like N'%" + txbname.Text.Trim().Replace("'","''") + "%'";
              }

              if( !string.IsNullOrEmpty(txbdesc.Text.Trim()))
              {
                  strSql += " And d.description Like N'%" + txbdesc.Text.Trim().Replace("'", "''") + "%'";
              }
           
              if( !string.IsNullOrEmpty(txtPictureNo.Text.Trim()))
              {
                  strSql += " And d.description Like N'%" + txtPictureNo.Text.Trim().Replace("'", "''") + "%'";
              }
              
              if (!string.IsNullOrEmpty(gid.Value))
   
                 strSql += " And isnull(d.Guid,'')='" +gid.Value.Trim() + "'";
                           
        return strSql;
           
    }

    protected DataTable createtab()
    {
        DataTable dtl = new DataTable();
        dtl.Columns.Add("id", System.Type.GetType("System.Int32"));
        dtl.Columns.Add("name", System.Type.GetType("System.String"));
        dtl.Columns.Add("description", System.Type.GetType("System.String"));
        dtl.Columns.Add("filename", System.Type.GetType("System.String"));
        dtl.Columns.Add("filename1", System.Type.GetType("System.String"));
        dtl.Columns.Add("version", System.Type.GetType("System.String"));
        dtl.Columns.Add("isPublic", System.Type.GetType("System.String"));
        dtl.Columns.Add("isall", System.Type.GetType("System.String"));
        dtl.Columns.Add("preview", System.Type.GetType("System.String"));
        dtl.Columns.Add("assText", System.Type.GetType("System.String"));
        dtl.Columns.Add("vendText", System.Type.GetType("System.String"));
        dtl.Columns.Add("Level", System.Type.GetType("System.Int32"));
        dtl.Columns.Add("verifycnt", System.Type.GetType("System.Int32"));
        dtl.Columns.Add("creator", System.Type.GetType("System.Int32"));
        dtl.Columns.Add("pictureNo", System.Type.GetType("System.String"));
        dtl.Columns.Add("accFileName", System.Type.GetType("System.String"));
        dtl.Columns.Add("createdname", System.Type.GetType("System.String"));
        dtl.Columns.Add("createdDate", System.Type.GetType("System.String"));
        dtl.Columns.Add("modifiedby", System.Type.GetType("System.Int32"));
        dtl.Columns.Add("modifiedname", System.Type.GetType("System.String"));
        dtl.Columns.Add("modifiedDate", System.Type.GetType("System.String"));
        dtl.Columns.Add("typeid", System.Type.GetType("System.String"));
        dtl.Columns.Add("cateid", System.Type.GetType("System.String"));
        dtl.Columns.Add("typename", System.Type.GetType("System.String"));
        dtl.Columns.Add("catename", System.Type.GetType("System.String"));
        return dtl;

    }

    protected void BindData()
    {
        DataTable ds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, createSql()).Tables[0];
        DataTable dtl = createtab();

   
        int total = 0;
        string typeid = typedrop.SelectedValue== null || typedrop.SelectedValue == "" ? "" : typedrop.SelectedValue;
        string catid = catdrop.SelectedValue== null || catdrop.SelectedValue == "" ? "" : catdrop.SelectedValue;

        foreach (DataRow i in ds.Rows)
        {
            DataRow Irow = dtl.NewRow();
            Irow["id"] = i["id"];
            Irow["name"] = i["name"].ToString().Trim();
            Irow["description"] = i["description"].ToString().Trim();       
            Irow["pictureNo"] = i["pictureNo"].ToString().Trim();
            Irow["version"] = i["version"].ToString().Trim();
            Irow["filename"] = i["filename"].ToString().Trim();
            Irow["filename1"] = i["filename"].ToString().Trim(); 
            Irow["accFileName"] = i["accFileName"].ToString().Trim(); 
           
            string paths= i["virPath"].ToString().Trim();
            if (!string.IsNullOrEmpty(i["accFileName"].ToString().Trim()))
            {
                string _accFileName = i["accFileName"].ToString().Trim();
                if (!string.IsNullOrEmpty(paths))
                {
                  Irow["filename"]= Irow["filename"] + "&nbsp;(<a href='/TecDocs/" + typeid +  "/"  +  catid + "/" + _accFileName + "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)";
                }
                else
                {
                  Irow["filename"]=  Irow["filename"] +"&nbsp;(<a href='" + paths + _accFileName + "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)";
                }
            }

            Irow["isall"]= i["isall"].ToString().Trim(); 
            Irow["isPublic"]= i["isPublic"].ToString().Trim(); 

            if (int.Parse(i["docLevel"].ToString()) >=0)
            {
                if (string.IsNullOrEmpty(paths))
                {
                    Irow["preview"]="<a href='/TecDocs/" +  typeid +  "/"   +  catid + "/" + i["filename"].ToString().Trim() + "' target='_blank'><u>Open</u></a>";
                }
                else
                {
                    Irow["preview"]="<a href='" + paths + i["filename"].ToString().Trim() + "' target='_blank'><u>Open</u></a>";
                }
            }
            else
            {
                Irow["preview"] ="&nbsp;";
            }

            Irow["assText"] ="<u>" +  i["cnt"].ToString().Trim() +  "</u>"; 
            Irow["vendText"] ="<u>"  + i["vcount"].ToString().Trim() +  "</u>"; 
            Irow["Level"] = i["Level"].ToString().Trim();
            Irow["verifycnt"] = i["verifycnt"].ToString().Trim();
            Irow["creator"] = i["creator"].ToString().Trim();
            Irow["createdname"] = i["createdname"].ToString().Trim();
            Irow["createdDate"] = i["createdDate"] == DBNull.Value ? "" : DateTime.Parse(i["createdDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            Irow["modifiedby"] =i["modifiedby"].ToString().Trim();
            Irow["modifiedname"] = i["modifiedname"].ToString().Trim();
            Irow["modifiedDate"] = i["modifiedname"]==DBNull.Value ? "" : DateTime.Parse(i["modifiedDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            Irow["typeid"] = i["typeid"].ToString().Trim();
            Irow["cateid"] = i["cateid"].ToString().Trim();
            Irow["typename"] = i["typename"].ToString().Trim();
            Irow["catename"] = i["catename"].ToString().Trim();
            dtl.Rows.Add(Irow);
            total++;
        }
         Label1.Text= "Number of Doc: " + total.ToString();

         int  pageCount;
    
         datagrid1.VirtualItemCount = dtl.Rows.Count;

         pageCount= dtl.Rows.Count / datagrid1.PageSize;
         if (dtl.Rows.Count % datagrid1.PageSize > 0 )
         {
                pageCount += 1;
         }

         if (datagrid1.CurrentPageIndex >= pageCount)
         {
             if (pageCount == 0)
                 datagrid1.CurrentPageIndex = 0;
             else
                 datagrid1.CurrentPageIndex = pageCount - 1;
         }

        DataTable indextab = GetPagedDataTable(datagrid1.CurrentPageIndex, datagrid1.PageSize, dtl);
        datagrid1.DataSource = indextab;
        datagrid1.DataBind();
        
        
    }

    protected void typedrop_TextChanged(object sender, EventArgs e)
    {
        binddropcat();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        datagrid1.CurrentPageIndex = 0;
        datagrid1.SelectedIndex = -1;
        BindData();
    }

    public static DataTable GetPagedDataTable(int pageIndex, int pageSize, DataTable source)
    {
        //分页处理
        DataTable paged = source.Clone();
        int rowbegin = pageIndex * pageSize;
        int rowend = (pageIndex + 1) * pageSize;
        if (rowend > source.Rows.Count)
        {
            rowend = source.Rows.Count;
        }

        for (int i = rowbegin; i < rowend; i++)
        {
            paged.ImportRow(source.Rows[i]);
        }
        return paged;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        int     ispublic=0,docID=0,docver=0;
        string  fname = "", fnameSuffix = "", accName = "";
        int     intLastBackslash=0;
        int     imgdatalen;
        int     ret;
        string  imgtype;
        Decimal size;
        string  _accFileMd5="" , _fileMd5="";
        int     accImgdatalen;
        string  accImgtype;
        Decimal accSize=0;
        int status=0, doclevel, isall;

        if (Session["uID"] == null)
        {
             ltlAlert.Text = "alert('Session timeout, please relogin!')";
             return;
        }

        if (catdrop.SelectedValue =="" || typedrop.SelectedValue=="")
        {
            ltlAlert.Text = "alert('Type or Category is empty')";
            return;
        }

        doclevel = int.Parse(ddlLevel.SelectedValue);

        if(chkIsPublic.Checked)
             ispublic = 1;
        else
             ispublic = 0;

        if (chkall.Checked)
            isall = 1;
        else
            isall = 0;


        if (string.IsNullOrEmpty(txbname.Text.Trim()))
        {
            ltlAlert.Text = "alert('DocName is required')";
            return;
        }

        if (string.IsNullOrEmpty(fileAttachFileDoc.PostedFile.FileName))
        {
            ltlAlert.Text = "alert('The file is required')";
            return;
        }
        else
        {
           if ((new string []{"#","+","%"}.Contains(fileAttachFileDoc.PostedFile.FileName)))
           {
               ltlAlert.Text = "alert('The file name can not contain # or + or %')";
               return;
           }
        }

         fname = fileAttachFileDoc.PostedFile.FileName;
         intLastBackslash = fname.LastIndexOf("\\");
         fname = fname.Substring(intLastBackslash + 1);
         if (string.IsNullOrEmpty(fname))
         {
             ltlAlert.Text = "alert('Please choose the file.')";
             return;
         }

         fnameSuffix = fname.Trim().Substring(fname.Trim().LastIndexOf(".") + 1).ToLower();

         if (!(new string[] { "pdf", "bmp", "png", "jpeg", "jpg", "gif" }.Contains(fnameSuffix.ToLower())))
         {
             ltlAlert.Text = "alert('The file format can only be pdf or pic format')";
             return;
         }

         if (!string.IsNullOrEmpty(fileAccFileDoc.PostedFile.FileName))
         {
             if ((new string[] { "#", "+", "%" }.Contains(fileAccFileDoc.PostedFile.FileName)))
             {
                 ltlAlert.Text = "alert('The file name can not contain # or + or %')";
                 return;
             }

             accName = fileAccFileDoc.PostedFile.FileName;
             intLastBackslash = accName.LastIndexOf("\\");
             accName = accName.Substring(intLastBackslash + 1);
             if (string.IsNullOrEmpty(accName.Trim()))
             {
                 ltlAlert.Text = "alert('Please choose the accFile.')";
                 return;
             }
         }

         if (accName == fname)
         {
              ltlAlert.Text = "alert('Doc and Accfile have same name.')";
              return;
         }

        if  (datagrid1.SelectedIndex != -1)
        {
             docID  = int.Parse(datagrid1.SelectedItem.Cells[0].Text.Trim());
             docver = int.Parse(datagrid1.SelectedItem.Cells[4].Text.Trim() + 1);

             string strsqltype = "";

             strsqltype = "SELECT ISNULL( t.isAppv,0) FROM QadDoc.NpartDocuments d LEFT JOIN QadDoc.dbo.DocumentType t ON d.typeid = t.typeid WHERE d.id = " + docID;
             if (SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strsqltype).ToString() == "0" )
             {
                  SqlParameter[] parm = new SqlParameter[2];
                  parm[0] = new SqlParameter("@docId", docID);
                  parm[1] = new SqlParameter("@lockPart", SqlDbType.VarChar, 500);
                  parm[1].Direction = ParameterDirection.Output;

                  if( SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, "qaddoc.dbo.sp_qad_checkLockULByDoc", parm).ToString()=="1")
                  {
                      ltlAlert.Text = "alert('The associated parts are locked by" + parm[1].Value.ToString() + "，the document can not upgrade！');";
                      return;
                  }
              }
          }
        else
        {
           docID = 0;
           if (string.IsNullOrEmpty(txbversion.Text.Trim()))
           {
               ltlAlert.Text = "alert('Ver is required')";
               return;
           }
           
           try{
               docver=int.Parse(txbversion.Text.Trim());
           }
           catch
           {
               ltlAlert.Text = "alert('Ver must be digitals')";
               return;
           }    
         }

         if (File.Exists(Server.MapPath("/qaddocitemimport/") + fname))
         {
                try
                {
                    File.Delete(Server.MapPath("/qaddocitemimport") + fname);
                }
                catch
                {
                    ltlAlert.Text = "alert('Delete the temp file failed！Please try again！')";
                    return;
                }
         }
        
         try
         {
             fileAttachFileDoc.PostedFile.SaveAs(Server.MapPath("/qaddocitemimport/") + fname);
         }
         catch
         {
             ltlAlert.Text = "alert('Save the temp file failed！Please try again！')";
             return;
         }


         if (fname != string.Empty)
         {
             _fileMd5 = base.GetMD5HashFromFile(Server.MapPath("/qaddocitemimport/") + fname);
         }

         imgdatalen = fileAttachFileDoc.PostedFile.ContentLength;
         imgtype = fileAttachFileDoc.PostedFile.ContentType;
         size = imgdatalen / 1024;

         if (!string.IsNullOrEmpty(fileAccFileDoc.PostedFile.FileName))
         {
             if (File.Exists(Server.MapPath("/qaddocitemimport/")+ accName))
             {
                 try
                 {
                     File.Delete(Server.MapPath("/qaddocitemimport/") + accName);
                 }
                 catch
                 {
                     ltlAlert.Text = "alert('Delete the temp file failed！Please try again！')";
                     return;
                 }
              }

              try
                 {
                     fileAccFileDoc.PostedFile.SaveAs(Server.MapPath("/qaddocitemimport/") + accName);
                 }
                 catch
                 {
                     ltlAlert.Text = "alert('Save the temp file failed！Please try again！')";
                     return;
                 }

              accImgdatalen = fileAttachFileDoc.PostedFile.ContentLength;
              accImgtype = fileAccFileDoc.PostedFile.ContentType;
              accSize = accImgdatalen / 1024;
           }

            SqlParameter[] param = new SqlParameter[22];
            param[0] = new SqlParameter("@cateID", catdrop.SelectedValue);
            param[1] = new SqlParameter("@typeID", typedrop.SelectedValue);
            param[2] = new SqlParameter("@docID", docID);
            param[3] = new SqlParameter("@docname", adam.sqlEncode(txbname.Text.Trim()));
            param[4] = new SqlParameter("@docdesc", adam.sqlEncode(txbdesc.Text.Trim()));
            param[5] = new SqlParameter("@docver", docver);
            param[6] = new SqlParameter("@fname", fname);
            param[7] = new SqlParameter("@docstatus", status);
            param[8] = new SqlParameter("@doclevel", doclevel);
            param[9] = new SqlParameter("@docisall", isall);
            param[10] = new SqlParameter("@imgdata", SqlDbType.Binary);
            param[11] = new SqlParameter("@imgtype", imgtype);
            param[12] = new SqlParameter("@uID", Session["uID"]);
            param[13] = new SqlParameter("@picNo", txtPictureNo.Text.Trim());
            param[14] = new SqlParameter("@isPublic", ispublic);
            param[15] = new SqlParameter("@uName", Session["uName"]);
            param[16] = new SqlParameter("@size", size);
            param[17] = new SqlParameter("@md5Val", _fileMd5);
            param[18] = new SqlParameter("@accFile", accName);
            param[19] = new SqlParameter("@accFileSize", accSize);
            param[20] = new SqlParameter("@accFileMd5Val", _accFileMd5);
            param[21] = new SqlParameter("@gid", gid.Value.Trim());
            ret = int.Parse(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, "qaddoc.dbo.qad_tempdocumentAdd", param).ToString());
           
            if (ret == -1)
            {
                 ltlAlert.Text = "alert('The doc filename is existed！Please rename！')";
                 return;
            }
            else if (ret == -11)
            {
                 ltlAlert.Text = "alert('The accFile filename is existed！Please rename！')";
                 return;
            }
            else if (ret==-12)
            {
                 ltlAlert.Text = "alert('Please upload the accFile！')";
                 return;
            }
            else if (ret<-6)
            {
                 ltlAlert.Text = "alert('The DocName is existed')";
                 return;
            }
            else if (ret<-1)
            {
                 ltlAlert.Text = "alert('Failed')";
                 return;
            }

            try
            {
                string dirPath = "";
                string path = "";
                dirPath = "/TecDocs/";

                dirPath = dirPath + typedrop.SelectedValue + "/";

                //'创建Type文件夹 创建Category文件夹
                if (!Directory.Exists(Server.MapPath(dirPath)))
                    Directory.CreateDirectory(Server.MapPath(dirPath));
                               
                dirPath = dirPath + catdrop.SelectedValue + "/";
                if (!Directory.Exists(Server.MapPath(dirPath)))
                    Directory.CreateDirectory(Server.MapPath(dirPath));
               
                path = dirPath + fname;
                if (File.Exists(Server.MapPath(path)))
                    File.Delete(Server.MapPath(path));


                File.Move(Server.MapPath("/qaddocitemimport/") +  fname, Server.MapPath(path));
                if (!string.IsNullOrEmpty(fileAccFileDoc.PostedFile.FileName))
                {
                    path = dirPath + accName;
                    if (File.Exists(Server.MapPath(path)))
                        File.Delete(Server.MapPath(path));

                    File.Move(Server.MapPath("/qaddocitemimport/")+ accName, Server.MapPath(path));
                }
             }
             catch
             {
                 ltlAlert.Text = "alert('Upload failed！')";
                 return;
             }
            chkAccFileName.Visible = false;
            txbversion.Text = "";
            txbname.Text = "";
            txbdesc.Text = "";
            txtPictureNo.Text = "";
            chkall.Checked = false;
            chkIsPublic.Checked = false;
            datagrid1.SelectedIndex = -1;
            BindData(); 
     }

    protected void Butcancel_Click(object sender, EventArgs e)
    {
            txbname.Text = "";
            txbdesc.Text = "";
            txbversion.Text = "";
            txtPictureNo.Text = "";
            chkall.Checked = false;
            chkIsPublic.Checked = false;
            chkAccFileName.Visible = false;
            datagrid1.SelectedIndex = -1;
            ddlLevel.SelectedIndex = 0;
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
         string EXTitle = "200^<b>DocName</b>~^300^<b>Description</b>~^80^<b>Ver</b>~^250^<b>FileName</b>~^80^<b>Approved</b>~^180^<b>For All Items</b>~^";
         string ExSql  = createSql();
         base.ExportExcel(adam.dsnx(), EXTitle, ExSql, false);
    }
    protected void datagrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            txbname.Text = e.Item.Cells[1].Text.Trim();
            txbdesc.Text = e.Item.Cells[13].Text.Trim().Replace("&nbsp;", "");
            txbversion.Text = e.Item.Cells[4].Text.Trim();
            txtPictureNo.Text = e.Item.Cells[12].Text.Trim();
            hidOldFileName.Value = e.Item.Cells[16].Text.Trim();
            hidOldAccFileName.Value = e.Item.Cells[17].Text.Trim();

            if (e.Item.Cells[5].Text.Trim().ToUpper() == "YES")
                chkall.Checked = true;
            else
                chkall.Checked = false;

            if (e.Item.Cells[6].Text.Trim().ToUpper() == "YES")
                    chkIsPublic.Checked = true;
            else
                    chkIsPublic.Checked = false;

            typedrop.SelectedValue = e.Item.Cells[25].Text.ToString().Trim();
            binddropcat();
            catdrop.SelectedValue= e.Item.Cells[26].Text.ToString().Trim();
             
        }
        if (e.CommandName == "DeleteClick")
        {
            int retvalue;
            string strSql = "qaddoc.dbo.qad_tempdocumentDel";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@cateID", catdrop.SelectedValue);
            param[1] = new SqlParameter("@typeID", typedrop.SelectedValue);
            param[2] = new SqlParameter("@docID", e.Item.Cells[0].Text.Trim());
            param[3] = new SqlParameter("@uID", Session["uID"]);
            retvalue =int.Parse (SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, param).ToString());
            if (retvalue == 2) 
            {
                    ltlAlert.Text = "alert('文档已通过检核，不可删除');";
                    BindData();
                    return;
            }
            else if (retvalue == 3) 
            {
                    ltlAlert.Text = "alert('文档删除失败，请联系文档管理员删除');";
                    BindData();
                    return;
            }
            else if (retvalue == 1)
            {
                    string  fileName = e.Item.Cells[2].Text.Trim();
                    if (fileName.Contains("&nbsp;")) 
                        fileName = fileName.Substring(0, fileName.IndexOf("&nbsp;"));
               
                    if (File.Exists(Server.MapPath("/TecDocs/" + typedrop.SelectedValue + "/" +  catdrop.SelectedValue + "/") + fileName))
                    {
                        try
                        {
                            File.Delete(Server.MapPath("/TecDocs/" + typedrop.SelectedValue +  "/"+ catdrop.SelectedValue+ "/") + fileName);
                        }
                        catch(Exception ex)
                        {
                            throw new Exception("文档：" + ex.ToString());
                        }
                    }

                   if (e.Item.Cells[17].Text.Trim().Length > 0 && e.Item.Cells[17].Text.Trim() != "&nbsp;")
                   {
                        try
                        {
                            File.Delete(Server.MapPath("/TecDocs/" + typedrop.SelectedValue +  "/" +  catdrop.SelectedValue + "/") + e.Item.Cells[17].Text.Trim());
                        }
                        catch(Exception ex)
                        {
                            throw new Exception("关联文档：" + ex.ToString());
                        }
                   }
                    datagrid1.CurrentPageIndex = 0;
                    BindData();
            }
        }

    }
    protected void Btnedit_Click(object sender, EventArgs e)
    {
           int    status = 0;
           int    isall =0;
           int    docID;
           string fname = "";
           string accName = "";
           int docver ;
           int ret ;
           int intLastBackslash;
           int imgdatalen ;
           string imgtype ="";
           int doclevel;    
           decimal size=0 ;
           string _accFileMd5="" ;
           int accImgdatalen;
           string accImgtype;
           decimal accSize=0;
           
           doclevel=int.Parse(ddlLevel.SelectedValue);

           if (Session["uID"]  == null )
           {
                 ltlAlert.Text = "alert('Session timeout, please relogin!')";
                 return;
            }

           if (catdrop.SelectedValue == "" || typedrop.SelectedValue == "")
           {
               ltlAlert.Text = "alert('Type or Category is empty')";
               return;
           }
           
            if (chkall.Checked )
                isall = 1;
            else
                isall = 0;
            
            if (txbname.Text.Trim().Length <= 0) {
                ltlAlert.Text = "alert('DocName is required')";
                return;
            }

            if  (string.IsNullOrEmpty(fileAttachFileDoc.PostedFile.FileName))
            {
                fname = "";
                intLastBackslash = 0;

                imgdatalen = 0;
                imgtype = "";

                if (chkAccFileName.Checked) 
                {
                    ltlAlert.Text = "alert('勾选 修改关联文档 之后，必须选择一个文件！')";
                    return;
                }
            }
            else
            {
                fname = fileAttachFileDoc.PostedFile.FileName;
                intLastBackslash = fname.LastIndexOf("\\");
                fname = fname.Substring(intLastBackslash + 1);

                imgdatalen = fileAttachFileDoc.PostedFile.ContentLength;             
                imgtype = fileAttachFileDoc.PostedFile.ContentType;
                size = (imgdatalen / 1024);

                if (fileAttachFileDoc.PostedFile.FileName.IndexOf("#") > 0 || fileAttachFileDoc.PostedFile.FileName.IndexOf("+") > 0 || fileAttachFileDoc.PostedFile.FileName.IndexOf("%") > 0)
                {
                    ltlAlert.Text = "alert('The file name can not contain # or +  or %')";
                    return;
                }
            }

            if (!string.IsNullOrEmpty(fileAccFileDoc.PostedFile.FileName)) 
            {
                if (fileAccFileDoc.PostedFile.FileName.IndexOf("#") > 0 ||  fileAccFileDoc.PostedFile.FileName.IndexOf("+") > 0 ||  fileAccFileDoc.PostedFile.FileName.IndexOf("%") > 0 )
                { 
                    ltlAlert.Text = "alert('The file name can not contain # or + or %')";               
                    return;;
                }
               
                accName = fileAccFileDoc.PostedFile.FileName;
                intLastBackslash = accName.LastIndexOf("\\");
                accName = accName.Substring(intLastBackslash + 1);
                if (accName.Trim().Length <= 0) 
                {
                    ltlAlert.Text = "alert('Please choose the accFile.')";           
                    return;
                }
             }

             if (fname.Length > 0)
             {
                if (File.Exists(Server.MapPath("/qaddocitemimport/") + fname))
                {
                    try
                    {
                        File.Delete(Server.MapPath("/qaddocitemimport") +  fname);
                    }
                    catch 
                    {
                        ltlAlert.Text = "alert('Delete the temp file failed！Please try again！')";
                        return;
                    }
                }

                try
                {
                   if (!string.IsNullOrEmpty(fileAttachFileDoc.PostedFile.FileName)) 
                   {
                        fileAttachFileDoc.PostedFile.SaveAs(Server.MapPath("/qaddocitemimport/") + fname);
                   }
                }
               catch
                {
                    ltlAlert.Text = "alert('Save the temp file failed！Please try again！')";
                    return;
                }
             }

             
            if (!string.IsNullOrEmpty (fileAccFileDoc.PostedFile.FileName))
            {
                if (File.Exists(Server.MapPath("/qaddocitemimport/") + accName))
                {
                    try
                    {
                        File.Delete(Server.MapPath("/qaddocitemimport") +  accName);
                    }
                    catch 
                    {
                        ltlAlert.Text = "alert('Delete the temp file failed！Please try again！')";           
                        return;
                    }
                } 

                try
                {
                    fileAccFileDoc.PostedFile.SaveAs(Server.MapPath("/qaddocitemimport/") + accName);
                }
                catch 
                {
                    ltlAlert.Text = "alert('Save the temp file failed！Please try again！')";
                    return;
                }

                if (accName != string.Empty)
                {
                    _accFileMd5 = base.GetMD5HashFromFile(Server.MapPath("/qaddocitemimport/")+ accName);
                }


                accImgdatalen = fileAccFileDoc.PostedFile.ContentLength;
                accImgtype = fileAccFileDoc.PostedFile.ContentType;
                accSize =accImgdatalen / 1024;

            }

            if (datagrid1.SelectedIndex != -1)
            {
                docID  = int.Parse(datagrid1.SelectedItem.Cells[0].Text.Trim());
                docver = int.Parse(datagrid1.SelectedItem.Cells[4].Text.Trim());

                if (chkAccFileName.Checked)
                {}
                else
                {
                    //string strsql1="";
                    //strsql1 = "select count(*) from QadDoc.dbo.DocumentItem where docid =" + docID + " and qad is not null";

                    //if  (int.Parse(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strsql1).ToString()) >= 1)
                    //{
                    //    if (fname !=string.Empty || accName != string.Empty) 
                    //    {                      
                    //        ltlAlert.Text = "alert('The document have associated part,can not modify');";
                    //        return;
                    //    }
                    //}

                    //strsql1 = "select count(*) from QadDoc.dbo.DocumentItem_Bak where docid ="+  docID +  " and qad is not null";

                    //if (int.Parse(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strsql1).ToString()) >= 1) 
                    //{
                    //    if (fname != String.Empty || accName !=String.Empty) 
                    //    {
                    //        ltlAlert.Text = "alert('The document have associated part,can not modify');";                           
                    //        return;
                    //    }
                    //}

                    //   strsql1 = "select count(*) from QadDoc.dbo.DocumentItemApprove where docid =" & docID & " and appvResult is null and qad is null"

                    //If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql1) >= 1 Then
                    //    ltlAlert.Text = "alert('正在升级审批中，请等待审批结果');"
                    //    Exit Sub
                   //End If
                   //' End If
                }
            }

            else
                {
                    ltlAlert.Text = "alert('Please select the document.')";
                    return;
                }          

           if (string.IsNullOrEmpty(txbversion.Text.Trim()))
           {
               ltlAlert.Text = "alert('Ver is required')";
               return;
           }
           
           try{
               docver=int.Parse(txbversion.Text.Trim());
           }
           catch
           {
               ltlAlert.Text = "alert('Ver must be digitals')";
               return;
           }    

           if (fname.Trim() != string.Empty) 
           {
                if (!(chkAccFileName.Checked)) 
                {
                    string fnameSuffix;

                    fnameSuffix = fname.Trim().Substring(fname.Trim().LastIndexOf(".") + 1).ToLower();
                    if (!(new string[] { "pdf", "bmp", "png", "jpeg", "jpg", "gif" }.Contains(fnameSuffix.ToLower())))
                    {
                        ltlAlert.Text = "alert('The file format can only be pdf or pic format')" ;                   
                        return;
                    }
                 }

             }

            string _fileMd5="";
            if (fname != string.Empty)
            {
                _fileMd5 =base.GetMD5HashFromFile(Server.MapPath("/qaddocitemimport/") + fname);
            }

            string strSql = "qaddoc.dbo.qad_tempdocumentMod";        
            SqlParameter[] param = new SqlParameter[22];
            param[0] = new SqlParameter("@cateID", catdrop.SelectedValue);
            param[1] = new SqlParameter("@typeID", typedrop.SelectedValue);
            param[2] = new SqlParameter("@docID", docID);
            param[3] = new SqlParameter("@docname", adam.sqlEncode(txbname.Text.Trim()));
            param[4] = new SqlParameter("@docdesc", adam.sqlEncode(txbdesc.Text.Trim()));
            param[5]= new SqlParameter("@docver", docver);
            param[6] = new SqlParameter("@fname", fname);
            param[7] = new SqlParameter("@docstatus", status);
            param[8] = new SqlParameter("@doclevel", doclevel);
            param[9] = new SqlParameter("@docisall", isall);
            param[10] = new SqlParameter("@imgdata", SqlDbType.Binary);
            param[11] = new SqlParameter("@imgtype", imgtype);
            param[12] = new SqlParameter("@uID", Session["uID"]);
            param[13] = new SqlParameter("@uName", Session["uName"]);
            param[14] = new SqlParameter("@picNo", txtPictureNo.Text.Trim());
            param[15] = new SqlParameter("@chkAccFileName", chkAccFileName.Checked);
            param[16] = new SqlParameter("@isPublic", chkIsPublic.Checked);
            param[17] = new SqlParameter("@size", size);
            param[18] = new SqlParameter("@md5Val", _fileMd5);
            param[19] = new SqlParameter("@accFile", accName);
            param[20] = new SqlParameter("@accFileSize", accSize);
            param[21] = new SqlParameter("@accFileMd5Val", _accFileMd5);

            ret = int.Parse(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, param).ToString());

            if (ret == -1)
              {
                ltlAlert.Text = "alert('The doc filename is existed！Please rename！')";           
                return;
                }
            else if (ret == -11)
            {
                ltlAlert.Text = "alert('The accFile filename is existed！Please rename！')";
                return;
            }
            else if (ret < -1) 
            {
                ltlAlert.Text = "alert('It's failure')";
                return;
             }

            string  path ="";

            path = "/TecDocs/" + typedrop.SelectedValue + "/" +  catdrop.SelectedValue + "/";
            
            try 
            {
               if(!Directory.Exists(Server.MapPath(path))) 
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                }
              
                if (accName.Length > 0 )
                {
                    if ( File.Exists(Server.MapPath(path) + hidOldAccFileName.Value))
                    {
                        File.Move(Server.MapPath(path) + hidOldAccFileName.Value, Server.MapPath("/TecDocs/0/") + "det1-638-" + DateTime.Now.ToFileTime().ToString() + "-" + hidOldAccFileName.Value);
                    }
                    if (File.Exists(Server.MapPath(path)+ accName))
                    {
                        File.Move(Server.MapPath(path) + accName, Server.MapPath("/TecDocs/0/") + "det1-652-" + DateTime.Now.ToFileTime().ToString() + "-" + accName);
                    }
                    File.Move(Server.MapPath("/qaddocitemimport/") + accName, Server.MapPath(path) + accName);
                }
                
                if (fname.Length > 0)
                {
                    if (File.Exists(Server.MapPath(path) + hidOldFileName.Value))
                    {
                        if (!Directory.Exists(Server.MapPath("/TecDocs/0/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("/TecDocs/0/"));
                        }
                        File.Move(Server.MapPath(path) +  hidOldFileName.Value, Server.MapPath("/TecDocs/0/") +  "det1-644-" + DateTime.Now.ToFileTime().ToString() +  "-" +  hidOldFileName.Value);
                    }
                    if (File.Exists(Server.MapPath(path) + fname))
                    {
                        File.Move(Server.MapPath(path) +  fname, Server.MapPath("/TecDocs/0/") + "det1-652-" + DateTime.Now.ToFileTime().ToString() + "-" + fname);
                    }
                    File.Move(Server.MapPath("/qaddocitemimport/") + fname, Server.MapPath(path)+ fname);

                }
            }
            catch
            {
                ltlAlert.Text = "alert('Upload failed！')";
                return;
            }
                
          
            chkAccFileName.Checked = false;
            chkAccFileName.Visible = false;
            txbversion.Text = "";
            txbname.Text = "";
            txbdesc.Text = "";
            txtPictureNo.Text = "";
            chkall.Checked = false;
            chkIsPublic.Checked = false;
            datagrid1.SelectedIndex = -1;
            BindData();
                    
    }
    protected void datagrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        datagrid1.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }

    protected void catdrop_TextChanged(object sender, EventArgs e)
    {
        if (catdrop.SelectedValue != "")
            btnSearch_Click(null, null);
    }

    protected void btnup_Click(object sender, EventArgs e)
    {
        if (catdrop.SelectedValue == "" || typedrop.SelectedValue == "")
        {
            ltlAlert.Text = "alert('Type or Category is empty')";
            return;
        }
        this.Redirect("Mutiupload.aspx?typeid=" + typedrop.SelectedValue + "&catid=" + catdrop.SelectedValue);
    }

    protected void datagrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (Request.QueryString["flg"] != null)
        {
            if (Request.QueryString["flg"].ToString() == "0")
            {
                e.Item.Cells[7].Visible = false;
                e.Item.Cells[8].Visible = false;
            }
        }
       
    }
}