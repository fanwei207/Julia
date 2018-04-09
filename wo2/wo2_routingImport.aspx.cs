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
using WO2_RI;


public partial class wo2_wo2_routingImport : BasePage
{
    adamClass chk = new adamClass();
    WO2_routingImport wo2 = new WO2_routingImport();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "500^<b>����ԭ��</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select wo2_ro_routing,wo2_mop_proc,wo2_ro_run,errinfo From tcpc0.dbo.wo2_routingtemp Where createdby ='" + Convert.ToInt32(Session["uID"]) + "'  Order By wo2_ro_id ";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            FileTypeDropDownList1.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            FileTypeDropDownList1.Items.Add(item1);

        }
    }    

    public Boolean ImportExcelFile()
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
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('��ѡ�����ļ�.');";
            return false;
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
                ltlAlert.Text = "alert('�ϴ����ļ����Ϊ 8 MB!');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return false;
            }



            if (File.Exists(strFileName))
            {

                try
                {
                    //ds = chk.getExcelContents(strFileName);
                    dt = this.GetExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('�����ļ�������Excel��ʽ'" + e.ToString() + "'.');";
                    return false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    if(dt.Columns[0].ColumnName != "���մ���" ||
                       dt.Columns[1].ColumnName != "�������" ||
                       dt.Columns[2].ColumnName != "���չ�ʱ" )
                    {
                        //ds.Reset();
                        ltlAlert.Text = "alert('�����ļ���ģ�治��ȷ!');";
                        return false;
                    }

                    #region �����������Դ�ı�procOutput
                    DataTable temp = new DataTable("ro_routing");
                    DataColumn column;

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "routing";
                    temp.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "proc";
                    temp.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Decimal");
                    column.ColumnName = "run";
                    temp.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "uID";
                    temp.Columns.Add(column); 

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.DateTime");
                    column.ColumnName = "date";
                    temp.Columns.Add(column);
                    #endregion

                    String _Routing = "";
                    String _Mop = "";
                    Decimal _Run = 0M;

                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow newRow = temp.NewRow();
                        newRow["uID"] = Convert.ToInt32(Session["uID"]);
                        newRow["date"] = DateTime.Now;

                        _Routing = row[0].ToString().Trim();//���մ���
                        _Mop = row[1].ToString().Trim();//�������

                        //���մ���
                        if (_Routing.Length > 15 || _Routing.Length < 14)
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('���մ��볤��ֻ����14λ��15λ(" + _Routing + ")!');";
                            return false;
                        }

                        newRow["routing"] = _Routing;

                        //�������
                        int _proc = 0;
                        try
                        {
                            _proc = Convert.ToInt32(_Mop);
                        }
                        catch
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('�������������(" + _Routing + " " + _Mop + ")!');";
                            return false;
                        }

                        newRow["proc"] = _proc;

                        //��ʱ
                        try
                        {
                            _Run = Convert.ToDecimal(row[2]);//���չ�ʱ
                        }
                        catch
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('�ӹ�ʱ�����������(" + _Routing + " " + _Mop + ")!');";
                            return false;
                        }

                        newRow["run"] = _Run;

                        temp.Rows.Add(newRow);
                    }

                    //InsertTempRouting
                    if (temp != null && temp.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulckCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                        {
                            bulckCopy.DestinationTableName = "wo2_routingtemp";
                            bulckCopy.ColumnMappings.Add("routing", "wo2_ro_routing");
                            bulckCopy.ColumnMappings.Add("proc", "wo2_mop_proc");
                            bulckCopy.ColumnMappings.Add("run", "wo2_ro_run");
                            bulckCopy.ColumnMappings.Add("uID", "createdby");
                            bulckCopy.ColumnMappings.Add("date", "createddate");

                            try
                            {
                                bulckCopy.WriteToServer(temp);
                            }
                            catch
                            {
                                ltlAlert.Text = "alert('�����ϴ�ʱʧ��,����ϵ����Ա!');";
                                return false;
                            }
                            finally
                            {
                                temp.Dispose();
                            }

                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }

                } //dt.Rows.Count > 0                           

                //ds.Reset();


                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            } //File.Exists(strFileName)
        } //filename1.PostedFile != null
        return true;
    }
    protected void btnRouting_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        wo2.DelRoutingTemp(Convert.ToInt32(Session["uID"]));

        if (ImportExcelFile())
        {
            Int32 Ierr = 0;
            //�ٴ��޸ģ����洢���̲��
            Ierr = wo2.CheckRoutingTempError(Convert.ToInt32(Session["uID"]));
            if (Ierr < 0)
            {
                ltlAlert.Text = "alert('����������д���'); window.location.href='" + chk.urlRand("/wo2/wo2_routingImport.aspx?err=y") + "';";

            }
            else
            {
                if(wo2.ImportRouting(Convert.ToInt32(Session["uID"])))
                {
                     ltlAlert.Text = "alert('����ɹ�!');";
                }
                else
                {
                     ltlAlert.Text += "alert('����ʧ��!');";
                }
            }

        }
        else
        {
            ltlAlert.Text += "alert('����ʧ��!');";
        }
    }
}
