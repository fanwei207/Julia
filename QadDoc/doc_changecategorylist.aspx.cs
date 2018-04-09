using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class QadDoc_doc_changecategorylist:BasePage
{
    private adamClass chk=new adamClass();

    private string typeId
    {
        get
        {
            return ViewState["typeid"].ToString();
        }
        set
        {
            ViewState["typeid"] = value;
        }
    }

    private string cateId
    {
        get
        {
            return ViewState["cateid"].ToString();
        }
        set
        {
            ViewState["cateid"] = value;
        }
    }

    private string docId
    {
        get
        {
            return ViewState["docid"].ToString();
        }
        set
        {
            ViewState["docid"] = value;
        }
    }

    private string fileName
    {
        get
        {
            return ViewState["filename"].ToString();
        }
        set
        {
            ViewState["filename"] = value;
        }
    }

    private string accFileName
    {
        get
        {
            return ViewState["accfilename"].ToString();
        }
        set
        {
            ViewState["accfilename"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            btncancel.Visible = false;
            Session["type"] = "";
            Session["cate"] = "";
            LoadDocType();
            LoadDocCate();
            BindData();
        }
    }

    private void LoadDocType()
    {
        ListItem ls = new ListItem();
        ls.Value = "0";
        ls.Text = "--";
        SelectTypeDropDown.Items.Add(ls);

        string strSql = " Select d.typeid, d.typename From qaddoc.dbo.DocumentType d  ";

        if (!string.IsNullOrEmpty(SelectTypeDropDown.SelectedValue) && SelectTypeDropDown.SelectedValue != "0")
        {
            strSql += " Where d.typeid = '" + SelectTypeDropDown.SelectedValue + "' And d.isDeleted Is Null ";
        }
        else
        {
            strSql += " Where d.isDeleted Is Null ";
        }
        strSql += " Order By d.typename ";
        SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, strSql);
        while (reader.Read())
        {
            ls = new ListItem();
            ls.Value = reader[0].ToString();
            ls.Text = reader[1].ToString().Trim();
            SelectTypeDropDown.Items.Add(ls);
        }
        reader.Close();
        SelectTypeDropDown.SelectedValue = "0";
    }

    private void LoadDocCate()
    {
        SelectCateDropDown.Items.Clear();

        ListItem ls = new ListItem();
        ls.Value = "0";
        ls.Text = "--";
        SelectCateDropDown.Items.Add(ls);

        string strSql = " Select d.cateid, d.catename From qaddoc.dbo.DocumentCategory d ";

        strSql += " Where d.catename Is Not Null And d.typeid = '" + SelectTypeDropDown.SelectedValue + "'";
        if (!string.IsNullOrEmpty(SelectCateDropDown.SelectedValue) && SelectCateDropDown.SelectedValue != "0")
        {
            strSql += " And d.cateid = '" + SelectCateDropDown.SelectedValue + "'";
        }
        strSql += " Order By d.catename ";
        SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, strSql);
        while (reader.Read())
        {
            ls = new ListItem();
            ls.Value = reader[0].ToString();
            ls.Text = reader[1].ToString().Trim();
            SelectCateDropDown.Items.Add(ls);
        }
        reader.Close();
        SelectCateDropDown.SelectedValue = "0";
    }

    private void BindData()
    {
        int accDocLevel;  //用户文档权限访问级
        string strSqldocLevel = "select doc_acc_level from qaddoc.dbo.documentaccess where ";
        strSqldocLevel += " doc_acc_catid = '" + SelectTypeDropDown.SelectedValue + "'";
        strSqldocLevel += " And doc_acc_userid = '" + Session["uID"] + "' And approvedBy Is Not Null ";
        if (Session["uRole"].ToString() == "1")
        {
            accDocLevel = 0; // 管理员可查看所有文档
        }
        else
        {
            SqlDataReader reader_doclevel = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, strSqldocLevel);
            if (reader_doclevel.Read())
            {
                accDocLevel = Convert.ToInt32(reader_doclevel["doc_acc_level"]);
            }
            else
            {
                accDocLevel = 6; // 因文档设了5个级别，且等级1 最高
            }
            reader_doclevel.Close();
        }

        string strSql = " Select Distinct d.id, Isnull(d.docLevel,3) as docLevel, "
               + " Isnull(d.filepath,'') As path, d.typeid, d.cateid, d.typename, d.catename, d.name, Isnull(d.description,'') As description, d.version, "
               + "d.filepath, Isnull(d.docLevel,3) As Level, Isnull(d.filename,'') As filename, Case d.isApprove When 1 Then 'Yes' Else 'No' End As Approved, "
               + " Case d.isall When 1 Then 'Yes' Else 'No' End As IsAll, isNewMechanism = Isnull(isNewMechanism, 0), d.pictureNo ,d.accFileName,"
               + " Case d.isPublic When 1 Then 'Yes' Else 'No' End As isPublic,d.Path as virPath, "
               + " hiscnt = Isnull(his.cnt, 0), "
               + " Assocnt = Isnull(dt.cnt, 0)"
               + " From qaddoc.dbo.documents d "
               + " Left Join(	Select typeid, cateid, Name, cnt = Count(*) From qaddoc.dbo.documents_his Group By typeid, cateid, Name ) as his On his.typeid = d.typeid And his.cateid = d.cateid And his.Name = d.Name "
               + " Left Join(	Select docid, cnt = Count(*) From qaddoc.dbo.documentitem Group By docid ) as dt On dt.docid = d.id "
                   + (Session["uRole"].ToString() == "1" ? "Left Outer" : "Inner") + " Join qaddoc.dbo.DocumentAccess da On d.typeID = da.doc_acc_catid And da.approvedBy Is Not Null ";

        //strSql += " Where d.isApprove = 1 And da.approvedBy Is Not Null ";

        strSql += " Where d.typeid = '" + SelectTypeDropDown.SelectedValue + "'";

        strSql += " And d.cateid = '" + SelectCateDropDown.SelectedValue + "'";

        if (txb_search.Text.Trim().Length > 0)
        {
            strSql += " And (d.[name] like N'%" + txb_search.Text.Trim() + "%' Or d.filename like N'%" + txb_search.Text.Trim() + "%')";
        }

        DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, strSql);

        DataTable dtl = new DataTable();
        int total = 0;
        dtl.Columns.Add(new DataColumn("docid", System.Type.GetType("System.Int32")));
        dtl.Columns.Add(new DataColumn("typeid", System.Type.GetType("System.Int32")));
        dtl.Columns.Add(new DataColumn("cateid", System.Type.GetType("System.Int32")));
        dtl.Columns.Add(new DataColumn("typename", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("catename", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("filename", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("name", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("version", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("isAppr", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("isall", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("preview", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("oldview", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("Level", System.Type.GetType("System.Int32")));
        dtl.Columns.Add(new DataColumn("assText", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("description", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("pictureNo", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("isPublic", System.Type.GetType("System.String")));
        dtl.Columns.Add(new DataColumn("accFileName", System.Type.GetType("System.String")));

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            DataRow drow = dtl.NewRow();
            drow["docid"] = row["id"].ToString().Trim();
            drow["typeid"] = row["typeid"].ToString().Trim();
            drow["typename"] = row["typename"].ToString().Trim();
            drow["catename"] = row["catename"].ToString().Trim();
            drow["cateid"] = row["cateid"].ToString().Trim();
            drow["name"] = row["name"].ToString().Trim();
            drow["filename"] = row["filename"].ToString().Trim();
            drow["description"] = row["description"].ToString().Trim();
            drow["pictureNo"] = row["pictureNo"].ToString().Trim();
            drow["version"] = row["version"].ToString();
            drow["isAppr"] = row["Approved"].ToString().Trim();
            drow["isall"] = row["IsAll"].ToString().Trim();
            drow["isPublic"] = row["isPublic"].ToString().Trim();
            drow["accFileName"] = row["accFileName"].ToString().Trim();
            /*如果有附加文件，就追加在后面 2013-11-13 接上次精神，暂时不放开
            If .Rows(i).Item("accFileName").ToString().Trim().Length > 0 Then
                Dim _accFileName As String = .Rows(i).Item("accFileName").ToString().Trim()
                drow.Item("filename") = drow.Item("filename") & "&nbsp;(<a href='/TecDocs/" & .Rows(i).Item("typeid").ToString().Trim() & "/" & .Rows(i).Item("cateid").ToString().Trim() & "/" & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
            End If*/

            //验证查看文档权限 
            if (Convert.ToInt32(row["docLevel"]) - accDocLevel >= 0 || row["isPublic"].ToString().Trim().ToUpper() == "YES")
            {
                //if (Convert.ToBoolean(row["isNewMechanism"]))
                //{
                //    drow["preview"] = "<a href='/TecDocs/" + row["typeid"].ToString().Trim() + "/" + row["cateid"].ToString().Trim() + "/" + row["filename"].ToString().Trim() + "' target='_blank'><u>Open</u></a>";
                //}
                //else
                //{
                //    drow["preview"] = "<a href='/qaddoc/qad_viewdocument.aspx?filepath=" + row["path"].ToString().Trim() + "&code="
                //                                                         + "document" + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0' target='_blank'><u>Open</u></a>";
                //}
                string path = row["virPath"].ToString().Trim();
                if (string.IsNullOrEmpty(path))
                {
                    drow["preview"] = "<a href='/TecDocs/" + row["typeid"].ToString().Trim() + "/" + row["cateid"].ToString().Trim() + "/" + row["filename"].ToString().Trim() + "' target='_blank'><u>Open</u></a>";
                }
                else 
                {
                    drow["preview"] = "<a href='" + path + row["filename"].ToString().Trim() + "' target='_blank'><u>Open</u></a>";
                }

                if (Convert.ToInt32(row["hiscnt"]) > 0)
                {
                    drow["oldview"] = "<a href='/qaddoc/qad_olddocumentlist.aspx?code=" + Server.UrlEncode(row["name"].ToString().Trim())
                                         + "&typeid=" + row["typeid"].ToString().Trim() + "&cateid=" + row["cateid"].ToString().Trim() + "&id=" + row["id"].ToString().Trim()
                                         + "',''docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300' target='_blank'><u>List</u></a>";
                }
                else
                {
                    drow["oldview"] = "&nbsp;";
                }
            }
            else
            {
                drow["preview"] = "&nbsp;";
                drow["oldview"] = "&nbsp;";
            }
            drow["assText"] = "<u>" + row["Assocnt"].ToString().Trim() + "</u>";
            drow["Level"] = row["Level"].ToString().Trim();
            total = total + 1;
            dtl.Rows.Add(drow);
        }
        ds.Reset();
        ds.Dispose();

        Label1.Text = "Total: " + total.ToString();

        try
        {
            DgDoc.DataSource = dtl;
            DgDoc.DataBind();
        }
        catch
        {
            //Response.Write(ex.Message);
        }
    }

    protected void DgDoc_PageIndexChanged(Object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        DgDoc.CurrentPageIndex = e.NewPageIndex;
        DgDoc.SelectedIndex = -1;
        BindData();
    }
    protected void DgDoc_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.CompareTo("associated_item") == 0)
        {
            ltlAlert.Text = "var w=window.open('/qaddoc/qad_documentitemlist.aspx?id=" + e.Item.Cells[1].Text.Trim() + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300'); w.focus();";
        }
        else if (e.CommandName.CompareTo("myEdit") == 0)
        {
            btn_search.Text = "Save";
            btncancel.Visible = true;

            int index = ((e.CommandSource as LinkButton).Parent.Parent as DataGridItem).ItemIndex;
            try
            {
                docId = DgDoc.Items[index].Cells[1].Text.ToString();
                typeId = DgDoc.Items[index].Cells[2].Text.ToString();
                cateId = DgDoc.Items[index].Cells[3].Text.ToString();
                fileName = DgDoc.Items[index].Cells[7].Text.ToString();
                accFileName = DgDoc.Items[index].Cells[18].Text.ToString().Replace("&nbsp;","");

                SelectTypeDropDown.SelectedValue = typeId;
                LoadDocCate();
                SelectCateDropDown.SelectedValue = cateId;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void SelectCateDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DgDoc.EditItemIndex = -1;
        DgDoc.CurrentPageIndex = 0;
        //BindData();
    }
    protected void SelectTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DgDoc.EditItemIndex = -1;
        DgDoc.CurrentPageIndex = 0;
        LoadDocCate();
        //BindData();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        
            
        if (btn_search.Text == "Search")
        {
            DgDoc.EditItemIndex = -1;
            BindData();
            txb_search.Text = "";
        }
        else if (btn_search.Text == "Save")
        {

            if (SelectTypeDropDown.SelectedValue == typeId && SelectCateDropDown.SelectedValue == cateId)
            {
                ltlAlert.Text = "alert('Change category or type,please!');";
                return;
            }
            else if (SelectTypeDropDown.SelectedValue == "0")
            {
                ltlAlert.Text = "alert('Select category,please!');";
                return;
            }
            else if (SelectCateDropDown.SelectedValue == "0")
            {
                ltlAlert.Text = "alert('Select type,please!');";
                return;
            }
            else
            {

                string sourceDirPath = "/TecDocs/" + typeId + "/" + cateId + "/";
                string sourcePath = Path.Combine(Server.MapPath(sourceDirPath), fileName);

                string destDirPath = "/TecDocs/" + SelectTypeDropDown.SelectedValue + "/" + SelectCateDropDown.SelectedValue + "/";
                string destPath = Path.Combine(Server.MapPath(destDirPath), fileName);
                string sourceAccPath = "";
                string destAccPath = "";
                bool success = false;

                if (File.Exists(sourcePath))
                {
                    try
                    {
                        if (!Directory.Exists(Server.MapPath(destDirPath)))
                        {
                            Directory.CreateDirectory(Server.MapPath(destDirPath));
                        }

                        if (!string.IsNullOrEmpty(accFileName))
                        {
                            sourceAccPath = Path.Combine(Server.MapPath(sourceDirPath), accFileName);
                            destAccPath = Path.Combine(Server.MapPath(destDirPath), accFileName);
                            if (File.Exists(sourceAccPath))
                            {
                                File.Copy(sourceAccPath, destAccPath);
                            }
                            else
                            {
                                btn_search.Text = "Search";
                                btncancel.Visible = false;
                                ltlAlert.Text = "alert('The associate document does not exist!');";
                                return;
                            }

                        }
                        File.Copy(sourcePath, destPath);

                        success = ChangeCategory(docId, SelectCateDropDown.SelectedValue, SelectCateDropDown.SelectedItem.Text, SelectTypeDropDown.SelectedValue, SelectTypeDropDown.SelectedItem.Text, destDirPath, Session["uID"].ToString(), Session["uName"].ToString());

                    }
                    catch (Exception ex)
                    {
                        success = false;
                    }
                    if (success)
                    {
                        SelectTypeDropDown.SelectedValue = typeId;
                        LoadDocCate();
                        SelectCateDropDown.SelectedValue = cateId;
                        BindData();
                        btn_search.Text = "Search";
                        btncancel.Visible = false;
                        File.Delete(sourcePath);
                        if (sourceAccPath != "" && File.Exists(sourceAccPath))
                        {
                            File.Delete(sourceAccPath);
                        }

                    }
                    else
                    {
                        if (File.Exists(destPath))
                        {
                            File.Delete(destPath);
                        }
                        if (destAccPath != "" && File.Exists(destAccPath))
                        {
                            File.Delete(destAccPath);
                        }
                    }

                }
                else
                {
                    btn_search.Text = "Search";
                    btncancel.Visible = false;
                    ltlAlert.Text = "alert('The document does not exist!');";
                }

            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/public/SetupDWGTrueView2009_32bit_CHS.exe','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        btn_search.Text = "Search";
        btncancel.Visible = false;
    }

    private bool ChangeCategory(string docId, string cateId, string cateName, string typeId, string typeName, string path, string userId, string userName)
    {
        try
        {
            string strName = "qad_documentChangeCategory";
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@docid", docId);
            param[1] = new SqlParameter("@cateid", cateId);
            param[2] = new SqlParameter("@catename", cateName);
            param[3] = new SqlParameter("@typeid", typeId);
            param[4] = new SqlParameter("@typename", typeName);
            param[5] = new SqlParameter("@path", path);
            param[6] = new SqlParameter("@userId", userId);
            param[7] = new SqlParameter("@userName", userName);
            string result = SqlHelper.ExecuteScalar(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param).ToString();
            if (result == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}