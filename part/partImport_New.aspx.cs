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
using CommClass;


public partial class ManualPoImport : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    private bool InsertBatchTemp(string uID, string uName, string plantCode)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@plantCode", plantCode);
            sqlParam[3] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_pro_insertBatchPro", sqlParam);

            return Convert.ToBoolean(sqlParam[3].Value);
        }
        catch {
            return false;
        }

    }

    protected bool CheckValidity(string uID)
    {
        try
        {
            string strSql = "sp_pro_checkproItem";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

            return Convert.ToBoolean(sqlParam[1].Value);
        }
        catch
        {
            return false;
        }
    }

    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_pro_clearProTemp", param);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public Boolean ImportExcelFile()
    {
        DataTable dt ;
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;

        #region �ϴ��ĵ����д���
        strCatFolder = Server.MapPath("/import");

        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('Fail to upload file.');";
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('Please select a file.');";
            return false;
        }

        strUserFileName = strFileName;

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
        #endregion

        if (filename1.PostedFile != null)
        {
            string error = "";
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum upload file is 8 MB.');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('Failed to upload file.');";
                return false;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    dt = this.GetExcelContents(strFileName); //chk.getExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('Import file must be in Excel format.');";
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
                    /*
                     *  �����Excel�ļ��������㣺
                     *      1������Ӧ��������
                     *      2���ӵ����п�ʼ����Ϊ����
                     *      3���������Ʊ�����wo2_mop�д���
                    */

                    if (dt.Columns.Count != 29)

                    {
                        dt.Reset();

                        ltlAlert.Text = "alert('The file must have 29 columns��');";

                        return false;
                    }

                    #region Excel���������뱣��һ��
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "������")
                        {
                            dt.Reset(); 
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be �����ţ�');";
                            return false;
                        }

                        if (col == 1 && dt.Columns[col].ColumnName.Trim() != "��������")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ����������');";
                            return false;
                        }

                        if (col == 2 && dt.Columns[col].ColumnName.Trim() != "��������")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be �������� ��');";
                            return false;
                        }

                        if (col == 3 && dt.Columns[col].ColumnName.Trim() != "״̬(0:���� 1:���� 2:ͣ��)")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ״̬(0:���� 1:���� 2:ͣ��)��');";
                            return false;
                        }

                        if (col == 4 && dt.Columns[col].ColumnName.Trim() != "��С�����")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ��С�������');";
                            return false;
                        }

                        if (col == 5 && dt.Columns[col].ColumnName.Trim() != "��λ(�ɿ�)")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ��λ(�ɿ�)��');";
                            return false;
                        }

                        if (col == 6 && dt.Columns[col].ColumnName.Trim() != "ת��ǰ��λ(�ɿ�)")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ת��ǰ��λ(�ɿ�)��');";
                            return false;
                        }
                        if (col == 7 && dt.Columns[col].ColumnName.Trim() != "ת��ϵ��(�ɿ�)")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ת��ϵ��(�ɿ�) ��');";
                            return false;
                        }

                        if (col == 8 && dt.Columns[col].ColumnName.Trim() != "QAD�����")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD����ţ�');";
                            return false;
                        }

                        if (col == 9 && dt.Columns[col].ColumnName.Trim() != "QAD����1")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD����1��');";
                            return false;
                        }

                        if (col == 10 && dt.Columns[col].ColumnName.Trim() != "QAD����2")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD����2��');";
                            return false;
                        }

                        if (col == 11 && dt.Columns[col].ColumnName.Trim() != "��λ-um")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ��λ-um��');";
                            return false;
                        }

                        if (col == 12 && dt.Columns[col].ColumnName.Trim() != "�����-dsgn")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be �����-dsgn��');";
                            return false;
                        }

                        if (col == 13 && dt.Columns[col].ColumnName.Trim() != "������-promo")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ������-promo��');";
                            return false;
                        }
                        //ͬ���ģ��Զ���1
                        if (col == 14 && dt.Columns[col].ColumnName.Trim() != "��Ʒ����-parttype")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ��Ʒ����-parttype��');";
                            return false;
                        }

                        if (col == 15 && dt.Columns[col].ColumnName.Trim() != "״̬-QADstatus")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ״̬-QADstatus��');";
                            return false;
                        }

                        if (col == 16 && dt.Columns[col].ColumnName.Trim() != "��-group")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ��-group��');";
                            return false;
                        }
                        if (col == 17 && dt.Columns[col].ColumnName.Trim() != "ͼֽ-draw")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ͼֽ-draw��');";
                            return false;
                        }


                        if (col == 18 && dt.Columns[col].ColumnName.Trim() != "�汾-rev")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be �汾-rev��');";
                            return false;
                        }
                        if (col == 19 && dt.Columns[col].ColumnName.Trim() != "ͼֽλ��-drwg")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ͼֽλ��-drwg��');";
                            return false;
                        }

                        if (col == 20 && dt.Columns[col].ColumnName.Trim() != "��λ-loc")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ��λ-loc��');";
                            return false;
                        }

                        if (col == 21 && dt.Columns[col].ColumnName.Trim() != "����ԭ��-isspol")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ����ԭ��-isspol��');";
                            return false;
                        }
                        if (col == 22 && dt.Columns[col].ColumnName.Trim() != "�ɹ�Ա-buyer")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be �ɹ�Ա-buyer��');";
                            return false;
                        }


                        if (col == 23 && dt.Columns[col].ColumnName.Trim() != "��Ӧ��-vend")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ��Ӧ��-vend��');";
                            return false;
                        }
                        if (col == 24 && dt.Columns[col].ColumnName.Trim() != "���ƴ���-pmcode")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ���ƴ���-pmcode��');";
                            return false;
                        }
                        if (col == 25 && dt.Columns[col].ColumnName.Trim() != "������ǰ��-mfglead")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ������ǰ��-mfglead��');";
                            return false;
                        }
                        if (col == 26 && dt.Columns[col].ColumnName.Trim() != "�ɹ���ǰ��-purlead")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be �ɹ���ǰ��-purlead��');";
                            return false;
                        }


                        if (col == 27 && dt.Columns[col].ColumnName.Trim() != "��ʵ��-phantom")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ��ʵ��-phantom��');";
                            return false;
                        }
                        if (col == 28 && dt.Columns[col].ColumnName.Trim() != "��-domain")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be ��-domain��');";
                            return false;
                        }
                    }																
                    #endregion

                    //ת����ģ���ʽ
                    DataTable table = new DataTable("temp");
                    DataColumn column;
                    DataRow row;

                    #region �������
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "partNumber";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "partdesc";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "category";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "status";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "min_inv";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "unit";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "tranunit";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "tranrate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_part";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_um";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_desc1";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_desc2";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_prodline";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_dsgn";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_promo";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_parttype";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_status";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_group";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_draw";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_rev";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_drwg";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_abc";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_loc";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_article";
                    table.Columns.Add(column);

                    column = new DataColumn();//null
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "xxptmt_ordper";
                    table.Columns.Add(column);

                    column = new DataColumn();//null
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "xxptmt_sftystk";
                    table.Columns.Add(column);

                    column = new DataColumn();//null
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "xxptmt_sftytime";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_isspol";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_buyer";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_vend";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_pmcode";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "xxptmt_mfglead";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "xxptmt_purlead";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_phantom";
                    table.Columns.Add(column);

                    column = new DataColumn();//null
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_yield";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_domain";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createBy";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "createname";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);

                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);

                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r[0].ToString().Trim() != string.Empty)
                            {
                                row = table.NewRow();

                                #region ��ֵ�������ж�

                                if (r[0].ToString().Trim() == string.Empty)
                                {
                                    error += "�����Ų���Ϊ�գ�";
                                }
                                else
                                {
                                    row["partNumber"] = r[0].ToString().Trim();
                                }
                                //custPo�ĳ��������20���ַ��������ȡ
                                if (r[1].ToString().Trim().Length == 0)
                                {
                                    error += "������������Ϊ�գ�";
                                }
                                else
                                {
                                    row["partdesc"] = r[1].ToString().Trim();
                                }

                                //hrdReqDate�ĳ��������10���ַ��������ȡ
                                if (r[2].ToString().Trim().Length ==0 )
                                {
                                    error += "�������಻��Ϊ�գ�";
                                }
                                else
                                {
                                    if (r[2].ToString().Trim().Length > 10)
                                    {
                                        row["category"] = r[2].ToString().Trim().Substring(0, 10);
                                    }
                                    else
                                    {
                                        row["category"] = r[2].ToString().Trim();
                                    }
                                }

                                //hrdDueDate�ĳ��������10���ַ��������ȡ
                                if (r[3].ToString().Trim().Length ==0)
                                {
                                    error += "״̬(0:���� 1:���� 2:ͣ��)����Ϊ�գ�";
                                }
                                else if (r[3].ToString().Trim() != "0" && r[3].ToString().Trim() != "1" &&  r[3].ToString().Trim() != "2")
                                {
                                    error += "״̬(0:���� 1:���� 2:ͣ��)����Ϊ0��1��2��";
                                }
                                else
                                {

                                    row["status"] = r[3].ToString().Trim();
                                    
                                }

                                //shipTo�ĳ��������20���ַ��������ȡ
                                if (r[4].ToString().Trim().Length == 0)
                                {
                                    row["min_inv"] = 0;
                                }
                                else
                                {
                                    if (!IsNumber(r[4].ToString().Trim()))
                                    {
                                        error += "��С��������������֣�";
                                    }
                                    else
                                    {
                                        row["min_inv"] = r[4].ToString().Trim();
                                    }
                                }
                                //shipTo�ĳ��������20���ַ��������ȡ
                                if (r[5].ToString().Trim().Length > 5)
                                {
                                    row["unit"] = r[5].ToString().Trim().Substring(0, 5);
                                }
                                else
                                {
                                    row["unit"] = r[5].ToString().Trim();
                                }

                                //channel�ĳ��������8���ַ��������ȡ
                                if (r[6].ToString().Trim().Length > 5)
                                {
                                    row["tranunit"] = r[6].ToString().Trim().Substring(0, 5);
                                }
                                else
                                {
                                    row["tranunit"] = r[6].ToString().Trim();
                                }
                                //hrdRmks�ĳ��������50���ַ��������ȡ
                                if (r[7].ToString().Trim().Length == 0)
                                {
                                    row["tranrate"] = 0;
                                }

                                else if (!IsNumber(r[7].ToString().Trim()))
                                {
                                    error += "ת��ϵ�����������֣�";
                                }
                                else if (Convert.ToDouble(r[7].ToString().Trim())<0)
                                {
                                    error += "ת��ϵ������С���㣻";
                                }
                                else if (r[7].ToString().Trim().Length > 50)
                                {
                                    row["tranrate"] = r[7].ToString().Trim().Substring(0, 50);
                                }
                                else
                                {
                                    row["tranrate"] = r[7].ToString().Trim();
                                }

                                //line�ĳ��������4���ַ��������ȡ
                                if (r[8].ToString().Trim().Length == 0)
                                {
                                    error += "QAD����Ų���Ϊ�գ�";
                                }
                                else
                                {
                                    if (r[8].ToString().Trim().Length > 14)
                                    {
                                        row["xxptmt_part"] = r[8].ToString().Trim().Substring(0,14);
                                    }
                                    else
                                    {
                                       row["xxptmt_part"] = r[8].ToString().Trim();
                                    }
                                   
                                }

                                //custPart�ĳ��������20���ַ��������ȡ
                                if (r[9].ToString().Trim().Length > 24)
                                {
                                     row["xxptmt_desc1"] = r[9].ToString().Trim().Substring(0,24);
                                }
                                else
                                {
                                     row["xxptmt_desc1"] = r[9].ToString().Trim();
                                }
                               
                               

                                //Qad�ĳ��������15���ַ��������ȡ
                                if (r[10].ToString().Trim().Length > 24)
                                {
                                     row["xxptmt_desc2"] = r[10].ToString().Trim().Substring(0, 24);
                                }
                                else
                                {
                                    row["xxptmt_desc2"] = r[10].ToString().Trim();
                                }
                               
                               

                                //ordQty�ĳ��������15���ַ��������ȡ
                                if (r[11].ToString().Trim().Length == 0)
                                {
                                    error += "��λ-um����Ϊ�գ�";
                                }
                                else
                                {
                                    if (r[11].ToString().Trim().Length > 2)
                                        {
                                              row["xxptmt_um"] = r[11].ToString().Trim().Substring(0,2);
                                        }
                                        else
                                        {
                                               row["xxptmt_um"] = r[11].ToString().Trim();
                                        }
                                 
                                }

                                //um�ĳ��������5���ַ��������ȡ
                                //if (r[12].ToString().Trim().Length ==0 )
                                //{
                                //    error += "�����-dsgn����Ϊ�գ�";
                                //}
                                //else
                                //{
                                    if (r[12].ToString().Trim().Length > 8)
                                {
                                     row["xxptmt_dsgn"] = r[12].ToString().Trim().Substring(0, 8);
                                }
                                else
                                {
                                    row["xxptmt_dsgn"] = r[12].ToString().Trim();
                                }
                                    
                                //}

                                //price�ĳ��������15���ַ��������ȡ
                                //if (r[13].ToString().Trim().Length ==0 )
                                //{   
                                //    error += "������-promo����Ϊ�գ�";
                                //}
                                //else
                                //{
                                    if (r[13].ToString().Trim().Length > 10)
                                {
                                     row["xxptmt_promo"] = r[13].ToString().Trim().Substring(0, 10);
                                }
                                else
                                {
                                     row["xxptmt_promo"] = r[13].ToString().Trim();
                                }
                                  
                                //}

                                //detReqDate�ĳ��������10���ַ��������ȡ
                                if (r[14].ToString().Trim().Length == 0 )
                                {
                                    error += "��Ʒ����-parttype����Ϊ�գ�";
                                }
                                else
                                {
                                    if (r[14].ToString().Trim().Length > 8)
                                {
                                    row["xxptmt_parttype"] = r[14].ToString().Trim().Substring(0, 8);
                                }
                                else
                                {
                                   row["xxptmt_parttype"] = r[14].ToString().Trim();
                                }
                                    
                                   
                                }

                                //detDueDate�ĳ��������10���ַ��������ȡ
                                if (r[15].ToString().Trim().Length==0)
                                {
                                    error += "״̬-QADstatus����Ϊ�գ�";
                                }
                                else
                                {
                                    if (r[15].ToString().Trim().Length > 8)
                                {
                                    row["xxptmt_status"] = r[15].ToString().Trim().Substring(0, 8);
                                }
                                else
                                {
                                     row["xxptmt_status"] = r[15].ToString().Trim();
                                }
                                   
                                    
                                }

                                //detRmks�ĳ��������50���ַ��������ȡ
                                if (r[16].ToString().Trim().Length ==0)
                                {
                                    error += "��-group����Ϊ�գ�";
                                }
                                else
                                {
                                    
                                    if (r[16].ToString().Trim().Length > 8)
                                        {
                                            row["xxptmt_group"] = r[16].ToString().Trim().Substring(0, 8);
                                        }
                                        else
                                        {
                                            row["xxptmt_group"] = r[16].ToString().Trim();
                                        }
                                   
                                 }

                                //if (r[17].ToString().Trim().Length ==0)
                                //{
                                //    //error += "ͼֽ-draw����Ϊ�գ�";
                                //}
                                //else
                                //{   
                                    if (r[17].ToString().Trim().Length > 18)
                                        {
                                             row["xxptmt_draw"] = r[17].ToString().Trim().Substring(0, 18);
                                        }
                                        else
                                        {
                                            row["xxptmt_draw"] = r[17].ToString().Trim();
                                        }
                                   
                                   
                                //}

                                //if (r[18].ToString().Trim().Length == 0)
                                //{
                                //    //error += "�汾-rev����Ϊ�գ�";
                                //}
                                //else
                                //{   
                                    if (r[18].ToString().Trim().Length > 4)
                                        {
                                             row["xxptmt_rev"] = r[18].ToString().Trim().Substring(0, 4);
                                        }
                                        else
                                        {
                                             row["xxptmt_rev"] = r[18].ToString().Trim();
                                        }
                                   
                                   
                                //}

                                //if (r[19].ToString().Trim().Length == 0)
                                //{
                                //    //error += "ͼֽλ��-drwg����Ϊ�գ�";
                                //}
                                //else
                                //{   
                                    if (r[19].ToString().Trim().Length > 18)
                                        {
                                             row["xxptmt_drwg"] = r[19].ToString().Trim().Substring(0, 18);
                                        }
                                        else
                                        {
                                            row["xxptmt_drwg"] = r[19].ToString().Trim();
                                        }
                                   
                                   
                               // }
                                if (r[20].ToString().Trim().Length == 0)
                                {
                                    error += "��λ-loc����Ϊ�գ�";
                                }
                                else
                                {   
                                    if (r[20].ToString().Trim().Length > 8)
                                        {
                                             row["xxptmt_loc"] = r[20].ToString().Trim().Substring(0, 8);
                                        }
                                        else
                                        {
                                            row["xxptmt_loc"] = r[20].ToString().Trim();
                                        }
                                   
                                   
                                }
                                if (r[21].ToString().Trim().Length == 0)
                                {
                                    error += "����ԭ��-isspol����Ϊ�գ�";
                                }
                                else if (r[21].ToString().Trim().ToLower() != "y" && r[21].ToString().Trim().ToLower() != "n")
                                {
                                    error += "����ԭ��-isspolֻ��ΪY,N��YΪ���ϣ�NΪ���跢�ϣ�";
                                }
                                else if (r[21].ToString().Trim().ToLower() == "y")
                                {
                                    row["xxptmt_isspol"] = "True";
                                }
                                else//---bit
                                {
                                    row["xxptmt_isspol"] = "False";
                                }


                                //if (r[22].ToString().Trim().Length == 0)
                                //{
                                //    error += "�ɹ�Ա-buyer����Ϊ�գ�";
                                //}
                                //else
                                //{   
                                    if (r[22].ToString().Trim().Length > 8)
                                        {
                                              row["xxptmt_buyer"] = r[22].ToString().Trim().Substring(0, 8);
                                        }
                                        else
                                        {
                                            row["xxptmt_buyer"] = r[22].ToString().Trim();
                                        }
                                   
                                  
                               // }

                                if (r[23].ToString().Trim().Length == 0)
                                {
                                    error += "��Ӧ��-vend����Ϊ�գ�";
                                }
                                else
                                {   
                                    if (r[23].ToString().Trim().Length > 8)
                                        {
                                             row["xxptmt_vend"] = r[23].ToString().Trim().Substring(0, 8);
                                        }
                                        else
                                        {
                                            row["xxptmt_vend"] = r[23].ToString().Trim();
                                        }
                                   
                                   
                                }
                                if (r[24].ToString().Trim().Length == 0)
                                {
                                    error += "���ƴ���-pmcode����Ϊ�գ�";
                                }
                                else
                                {   
                                    if (r[24].ToString().Trim().Length > 1)
                                        {
                                            row["xxptmt_pmcode"] = r[24].ToString().Trim().Substring(0, 1);
                                        }
                                        else
                                        {
                                           row["xxptmt_pmcode"] = r[24].ToString().Trim();
                                        }
                                   
                                    
                                }
                                if (r[25].ToString().Trim().Length == 0)
                                {
                                    error += "������ǰ��-mfglead����Ϊ�գ�";
                                }
                                else if (!IsNumber(r[25].ToString().Trim()))
                                {
                                    error += "������ǰ��-mfglead���������֣�";
                                }
                                else
                                {   
                                    if (r[25].ToString().Trim().Length > 8)
                                        {
                                             row["xxptmt_mfglead"] = r[25].ToString().Trim().Substring(0, 3);//int
                                        }
                                        else
                                        {
                                             row["xxptmt_mfglead"] = r[25].ToString().Trim();//int
                                        }
                                   
                                   
                                }
                                if (r[26].ToString().Trim().Length == 0)
                                {
                                    error += "�ɹ���ǰ��-purlead����Ϊ�գ�";
                                }
                                else if (!IsNumber(r[26].ToString().Trim()))
                                {
                                    error += "�ɹ���ǰ��-purlead���������֣�";
                                }
                                else
                                {   
                                    if (r[26].ToString().Trim().Length > 14)
                                        {
                                             row["xxptmt_purlead"] = r[26].ToString().Trim().Substring(0, 14);//int
                                        }
                                        else
                                        {
                                            row["xxptmt_purlead"] = r[26].ToString().Trim();//int
                                        }
                                   
                                   
                                }
                                if (r[27].ToString().Trim().Length == 0)
                                {
                                    error += "��ʵ��-phantom����Ϊ�գ�";
                                }
                                else if (r[27].ToString().Trim().ToLower() != "y" && r[27].ToString().Trim().ToLower() != "n")
                                {
                                    error += "��ʵ��-phantomֻ��ΪY,N,��գ�YΪʵ����NΪ�����";
                                }
                                else if (r[27].ToString().Trim().ToLower() == "y")
                                {
                                    row["xxptmt_phantom"] = "True";
                                }
                                else//---bit
                                {
                                    row["xxptmt_phantom"] = "False";
                                }
                              
                                if (r[28].ToString().Trim().Length == 0)
                                {
                                    error += "��-domain����Ϊ�գ�";
                                }
                                else
                                {   
                                    if (r[28].ToString().Trim().Length > 40)
                                        {
                                            row["xxptmt_domain"] = r[28].ToString().Trim().Substring(0, 40);
                                        }
                                        else
                                        {
                                           row["xxptmt_domain"] = r[28].ToString().Trim();
                                        }
                                   
                                    
                                }
                               


                                
                                #endregion

                                row["createBy"] = _uID;
                                if (error == "")
                                {
                                    row["errMsg"] = string.Empty;
                                }
                                else
                                {
                                    row["errMsg"] = error;
                                }

                                table.Rows.Add(row);
                            }
                        }

                        //table�����ݵ������
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                            {
                                bulkCopy.DestinationTableName = "dbo.part_temp";
                                bulkCopy.ColumnMappings.Add("partNumber", "partNumber");
                                bulkCopy.ColumnMappings.Add("partdesc", "partdesc");
                                bulkCopy.ColumnMappings.Add("category", "category");
                                bulkCopy.ColumnMappings.Add("status", "status");
                                bulkCopy.ColumnMappings.Add("min_inv", "min_inv");
                                bulkCopy.ColumnMappings.Add("unit", "unit");
                                bulkCopy.ColumnMappings.Add("tranunit", "tranunit");
                                bulkCopy.ColumnMappings.Add("tranrate", "tranrate");
                                bulkCopy.ColumnMappings.Add("xxptmt_part", "xxptmt_part");
                                bulkCopy.ColumnMappings.Add("xxptmt_um", "xxptmt_um");
                                bulkCopy.ColumnMappings.Add("xxptmt_desc1", "xxptmt_desc1");
                                bulkCopy.ColumnMappings.Add("xxptmt_desc2", "xxptmt_desc2");
                                bulkCopy.ColumnMappings.Add("xxptmt_prodline", "xxptmt_prodline");
                                bulkCopy.ColumnMappings.Add("xxptmt_dsgn", "xxptmt_dsgn");
                                bulkCopy.ColumnMappings.Add("xxptmt_promo", "xxptmt_promo");
                                bulkCopy.ColumnMappings.Add("xxptmt_parttype", "xxptmt_parttype");
                                bulkCopy.ColumnMappings.Add("xxptmt_status", "xxptmt_status");
                                bulkCopy.ColumnMappings.Add("xxptmt_group", "xxptmt_group");
                                bulkCopy.ColumnMappings.Add("xxptmt_draw", "xxptmt_draw");
                                bulkCopy.ColumnMappings.Add("xxptmt_rev", "xxptmt_rev");

                                bulkCopy.ColumnMappings.Add("xxptmt_drwg", "xxptmt_drwg");
                                bulkCopy.ColumnMappings.Add("xxptmt_abc", "xxptmt_abc");
                                bulkCopy.ColumnMappings.Add("xxptmt_loc", "xxptmt_loc");
                                bulkCopy.ColumnMappings.Add("xxptmt_article", "xxptmt_article");
                                bulkCopy.ColumnMappings.Add("xxptmt_ordper", "xxptmt_ordper");

                                bulkCopy.ColumnMappings.Add("xxptmt_sftystk", "xxptmt_sftystk");
                                bulkCopy.ColumnMappings.Add("xxptmt_sftytime", "xxptmt_sftytime");
                                bulkCopy.ColumnMappings.Add("xxptmt_isspol", "xxptmt_isspol");
                                bulkCopy.ColumnMappings.Add("xxptmt_buyer", "xxptmt_buyer");
                                bulkCopy.ColumnMappings.Add("xxptmt_vend", "xxptmt_vend");
                                bulkCopy.ColumnMappings.Add("xxptmt_pmcode", "xxptmt_pmcode");
                                bulkCopy.ColumnMappings.Add("xxptmt_mfglead", "xxptmt_mfglead");
                                bulkCopy.ColumnMappings.Add("xxptmt_purlead", "xxptmt_purlead");
                                bulkCopy.ColumnMappings.Add("xxptmt_phantom", "xxptmt_phantom");
                                bulkCopy.ColumnMappings.Add("xxptmt_yield", "xxptmt_yield");
                                bulkCopy.ColumnMappings.Add("xxptmt_domain", "xxptmt_domain");

                                bulkCopy.ColumnMappings.Add("createBy", "createBy");
                                bulkCopy.ColumnMappings.Add("errMsg", "errMsg");



                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('Operation fails!Please try again!');";

                                    return false;
                                }
                                finally
                                {
                                    table.Dispose();
                                }
                            }
                        }
                    }
                }

                dt.Reset();

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

        if (ImportExcelFile())
        {
            if (!CheckValidity(Session["uID"].ToString()))
            {
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString(), Session["plantCode"].ToString()))
                {
                    QadService.WebService1SoapClient client = new QadService.WebService1SoapClient();
                    client.Product_Add_Submit();
                    ltlAlert.Text = "alert('success!');";
                }
                else
                {
                    ltlAlert.Text = "alert('fail!');";
                }
            }
            else
            {
                string title = "100^<b>������</b>~^100^<b>��������</b>~^100^<b>�������� </b>~^100^<b>״̬(0:���� 1:���� 2:ͣ��)</b>~^100^<b>��С�����</b>~^100^<b>��λ(�ɿ�)</b>~^100^<b>ת��ǰ��λ(�ɿ�)</b>~^100^<b>QAD����� </b>~^100^<b>QAD����1 </b>~^100^<b>QAD����2 </b>~^100^<b>��λ-um </b>~^100^<b>�����-dsgn </b>~^100^<b>������-promo </b>~^100^<b>��Ʒ����-parttype </b>~^100^<b>״̬-QADstatus </b>~^100^<b>��-group </b>~^100^<b>ͼֽ-draw</b>~^100^<b>�汾-rev </b>~^100^<b>ͼֽλ��-drwg </b>~^100^<b>��λ-loc </b>~^100^<b>����ԭ��-isspol </b>~^100^<b>�ɹ�Ա-buyer </b>~^100^<b>��Ӧ��-vend </b>~^100^<b>���ƴ���-pmcode </b>~^100^<b>������ǰ��-mfglead</b>~^100^<b>�ɹ���ǰ��-purlead </b>~^100^<b>��ʵ��-phantom </b>~^100^<b>��-domain </b>~^100^<b>������Ϣ</b>~^";

                string sql = " select partNumber,[partdesc],[category],[status],[min_inv],[unit],[tranunit],[tranrate],  xxptmt_part,xxptmt_desc1,xxptmt_desc2,xxptmt_um,xxptmt_dsgn,xxptmt_promo,xxptmt_parttype,xxptmt_status,xxptmt_group,xxptmt_draw,xxptmt_rev,xxptmt_drwg,xxptmt_loc,xxptmt_isspol,xxptmt_buyer,xxptmt_vend,xxptmt_pmcode,xxptmt_mfglead,xxptmt_purlead,xxptmt_phantom,xxptmt_domain,errMsg from part_temp  where createBy =  " + Session["uID"].ToString();

                DataTable dt = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
                //ltlAlert.Text = "alert('����ʧ��!');";
                ExportExcel(title, dt, false);
               
            }
        }
    }
}
