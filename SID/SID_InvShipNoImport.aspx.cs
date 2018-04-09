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
using QADSID;


public partial class SID_InvShipNoImport : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "500^<b>����ԭ��</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select sid_inv_nbr,sid_shipNo,sid_errinfo From tcpc0.dbo.sid_invshipno_temp Where sid_createBy ='" + Convert.ToInt32(Session["uID"]) + "'  Order By sid_id ";
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
                    if (dt.Columns[0].ColumnName != "��Ʊ��" ||
                       dt.Columns[1].ColumnName != "�����")
                    {
                        //ds.Reset();
                        ltlAlert.Text = "alert('�����ļ���ģ�治��ȷ!');";
                        return false;
                    }

                    #region �����������Դ�ı�procOutput
                    DataTable temp = new DataTable("ro_Inv");
                    DataColumn column;

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Inv";
                    temp.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "ShipNo";
                    temp.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "uID";
                    temp.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "uName";
                    temp.Columns.Add(column);

                    //column = new DataColumn();
                    //column.DataType = System.Type.GetType("System.datetime");
                    //column.ColumnName = "date";
                    //temp.Columns.Add(column);

                    #endregion

                    String _Inv = "";
                    String _ShipNo = "";

                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow newRow = temp.NewRow();
                        newRow["uID"] = Convert.ToInt32(Session["uID"]);
                        newRow["uName"] = Session["uName"];
                        //newRow["date"] = DateTime.Now;

                        _Inv = row[0].ToString().Trim();//��Ʊ��
                        _ShipNo = row[1].ToString().Trim();//���ص���

                        //��Ʊ��
                        if (_Inv.Length > 9 || _Inv.Length < 8)
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('��Ʊ�ų���ֻ����8λ��9λ(" + _Inv + ")!');";
                            return false;
                        }

                        newRow["Inv"] = _Inv;

                        newRow["ShipNo"] = _ShipNo;

                        temp.Rows.Add(newRow);
                    }

                    //InsertTempInv
                    if (temp != null && temp.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulckCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                        {
                            bulckCopy.DestinationTableName = "sid_invshipno_temp";
                            bulckCopy.ColumnMappings.Add("Inv", "sid_inv_nbr");
                            bulckCopy.ColumnMappings.Add("ShipNo", "sid_shipNo");
                            bulckCopy.ColumnMappings.Add("uID", "sid_createBy");
                            bulckCopy.ColumnMappings.Add("uName", "sid_createName");
                            //bulckCopy.ColumnMappings.Add("date", "sid_createDate");

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

                } 
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            } 
        }
        return true;
    }
    protected void btnRouting_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }
        sid.DelInvTemp(Convert.ToInt32(Session["uID"]));
        if (ImportExcelFile())
        {
            Int32 Ierr = 0;
            //�ж������Ƿ���ϵ���
            Ierr = sid.CheckInvTempError(Convert.ToInt32(Session["uID"]));
            if (Ierr < 0)
            {
                ltlAlert.Text = "alert('����������д���'); window.location.href='" + chk.urlRand("/sid/SID_InvShipNoImport.aspx?err=y") + "';";
            }
            else
            {
                if (sid.ImportInvShip(Convert.ToInt32(Session["uID"])))
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
