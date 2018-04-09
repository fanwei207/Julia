using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ObjectBuilder;
using System.Data.Common;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace BudgetProcess
{
    /// <summary>
    /// Summary description for getBudget
    /// </summary>
    public class Budget
    {
        static Database db = DatabaseFactory.CreateDatabase("SqlConn.ConnStr");
        static adamClass adam = new adamClass();

        public Budget()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region
        /// <summary>
        /// Get Budget Cost Center
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet getBudgetCC(string strdomain, string strcode, string strmaster, string strdept, string strdesc)
        {
            string sqlCommand = "sp_bg_getBudgetCC";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "domain", DbType.String, strdomain.Trim());
            db.AddInParameter(dbCommand, "code", DbType.String, strcode.Trim());
            db.AddInParameter(dbCommand, "master", DbType.String, strmaster.Trim());
            db.AddInParameter(dbCommand, "desc", DbType.String, strdesc.Trim());
            db.AddInParameter(dbCommand, "dept", DbType.String, strdept.Trim());
            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Delete Budget Cost Center
        /// </summary>
        /// <param name="bcid"></param>
        public static void deleteBudgetCC(string bcid)
        {
            string sqlCommand = "sp_bg_deleteBudgetCC";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "ccid", DbType.Int32, Convert.ToInt32(bcid.Trim()));

            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                //throw ex;
                return;
            }
        }

        /// <summary>
        /// Update Budget Cost Center
        /// </summary>
        /// <param name="bcid"></param>
        /// <param name="strdomain"></param>
        /// <param name="strcode"></param>
        /// <param name="strdept"></param>
        /// <param name="strdesc"></param>
        /// <param name="strmaster"></param>
        public static void updateBudgetCC(string bcid, string strdomain, string strcode, string strdept, string strdesc)
        {
            string sqlCommand = "sp_bg_updateBudgetCC";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "ccid", DbType.Int32, Convert.ToInt32(bcid.Trim()));
            db.AddInParameter(dbCommand, "domain", DbType.String,  strdomain.Trim());
            db.AddInParameter(dbCommand, "code", DbType.String, strcode.Trim());
            db.AddInParameter(dbCommand, "dept", DbType.String, strdept.Trim());
            db.AddInParameter(dbCommand, "desc", DbType.String, strdesc.Trim());

            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                //throw ex;
                return;
            }
        }

        /// <summary>
        /// Insert Budget Cost Center
        /// </summary>
        /// <param name="strdomain"></param>
        /// <param name="strcode"></param>
        /// <param name="strdept"></param>
        /// <param name="strdesc"></param>
        /// <param name="strmaster"></param>
        /// <returns>New Budget Cost Center ID</returns>
        public static Int32 insertBudgetCC(string strdomain, string strcode, string strdept, string strdesc)
        {
            string sqlCommand = "sp_bg_insertBudgetCC";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "domain", DbType.String, strdomain.Trim());
            db.AddInParameter(dbCommand, "code", DbType.String, strcode.Trim());
            db.AddInParameter(dbCommand, "dept", DbType.String, strdept.Trim());
            db.AddInParameter(dbCommand, "desc", DbType.String, strdesc.Trim());

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return 0;
            }
        }

        /// <summary>
        /// Get Plants
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet getPlant()
        {
            string sqlCommand = "sp_bg_getplant";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Get Department
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns>DataSet</returns>
        public static DataSet getDept(string strsql)
        {
            string sqlCommand = "sp_bg_getDept";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "strsql", DbType.String, strsql.Trim());

            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="strplant"></param>
        /// <param name="strdept"></param>
        /// <returns>DataSet</returns>
        public static DataSet getUser(int intplant, int intdept, int intorg)
        {
            string sqlCommand = "sp_bg_getUser";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "plant", DbType.Int32, intplant);
            db.AddInParameter(dbCommand, "dept", DbType.Int32, intdept);
            db.AddInParameter(dbCommand, "org", DbType.Int32, intorg);

            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Get Budget Cost Center User
        /// </summary>
        /// <param name="strid"></param>
        /// <returns>User Str</returns>
        public static string getBudgetUser(string strid, string strtype)
        {
            string sqlCommand = "sp_bg_getUserStr";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "ccid", DbType.Int32, Convert.ToInt32(strid.Trim()));
            db.AddInParameter(dbCommand, "type", DbType.Int32, Convert.ToInt32(strtype.Trim()));

            try
            {
                string strUser = Convert.ToString(db.ExecuteScalar(dbCommand));

                return strUser;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Update Budget Cost Center User
        /// </summary>
        /// <param name="struser"></param>
        /// <param name="strtype"></param>
        public static void updateBudgetUser(int intccid, string struserid, string struser, string strtype)
        {
            string sqlCommand = "sp_bg_updateUserStr";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "ccid", DbType.Int32, intccid);
            db.AddInParameter(dbCommand, "userid", DbType.String, struserid.Trim());
            db.AddInParameter(dbCommand, "user", DbType.String, struser.Trim());
            db.AddInParameter(dbCommand, "type", DbType.Int32, Convert.ToInt32(strtype.Trim()));

            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                //throw ex;
                return;
            }
        }

        /// <summary>
        /// Get Budget Cost Center UserID
        /// </summary>
        /// <param name="strid"></param>
        /// <param name="strtype"></param>
        /// <returns>User ID Str</returns>
        public static string getBudgetUserID(string strid, string strtype)
        {
            string sqlCommand = "sp_bg_getUserIDStr";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "ccid", DbType.Int32, Convert.ToInt32(strid.Trim()));
            db.AddInParameter(dbCommand, "type", DbType.Int32, Convert.ToInt32(strtype.Trim()));

            try
            {
                string strUser = Convert.ToString(db.ExecuteScalar(dbCommand));

                return strUser;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        #endregion
        
        #region budget
        public DataTable GetBudget(string condition)
        {
            try
            {
                DataSet dst = null;
                string strName = "select * from tcpc0.dbo.bg_sub " + condition;
                dst = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strName);

                return dst.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataTable DeleteBudget(string ID)
        {
            try
            {
                DataSet dst = null;
                string strName = "delete from tcpc0.dbo.bg_sub where ID = @ID";
                SqlParameter parm = new SqlParameter("@ID", ID);

                dst = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strName, parm);

                return dst.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataTable ModifyBudget(string ID, string code, string desc, string prpt)
        {
            try
            {
                DataSet dst = null;
                string strName = "update tcpc0.dbo.bg_sub set sub_code = @code,sub_desc=@desc,sub_property=@prpt where ID = @ID";
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@code", code);
                parm[2] = new SqlParameter("@desc", desc);
                parm[3] = new SqlParameter("@prpt", prpt);


                dst = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strName, parm);

                return dst.Tables[0];
            }
            catch
            {
                return null;
            }
        }


        #endregion

        #region calendar
        public DataTable GetCalendar(string condition)
        {
            try
            {
                DataSet dst = null;
                string strName = "select * from tcpc0.dbo.bg_calendar " + condition + " order by ca_domain,ca_date";
                dst = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strName);

                return dst.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetDomain()
        {
            try
            {
                DataSet dst = null;
                string strName = "SELECT DISTINCT UPPER(cc_domain) cc_domain FROM tcpc0.dbo.bg_cc order by UPPER(cc_domain)";
                dst = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strName);

                return dst.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public void AddCalendar(string domain, string date, string close,ref int msg)
        {
            try
            {
                string strName = "sp_bg_addcalendar";
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@domain", domain);
                parm[1] = new SqlParameter("@date", date);
                parm[2] = new SqlParameter("@close", close);
                parm[3] = new SqlParameter("@returnVal",SqlDbType.Int);
                parm[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm);

                msg = int.Parse(parm[3].Value.ToString());
            }
            catch
            {
                return;
            }
        }
        public DataTable DeleteCalendar(string ID)
        {
            try
            {
                DataSet dst = null;
                string strName = "delete from tcpc0.dbo.bg_calendar where ID = @ID";
                SqlParameter parm = new SqlParameter("@ID", ID);

                dst = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strName, parm);

                return dst.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public void ModifyCalendar(string ID, string domain, string date, string close,ref int msg)
        {
            try
            {
                string strName = "sp_bg_updatecalendar";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@domain", domain);
                parm[2] = new SqlParameter("@date", date);
                parm[3] = new SqlParameter("@close", close);
                parm[4] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm);

                msg = int.Parse(parm[4].Value.ToString());
            }
            catch
            {
                return;
            }
        }

        #endregion

        #region mstr
        public void DynamicBindGridView(DataTable dt, GridView gv)
        {
            gv.Columns.Clear();

            for (int i = 1; i < dt.Columns.Count; i++)
            {
                TemplateField tf = new TemplateField();

                 if (i > 7 && i < dt.Columns.Count)
                {
                     tf.HeaderText = dt.Columns[i].Caption.ToString();
                     tf.HeaderStyle.Wrap = false;
                     tf.ItemStyle.Wrap = false;
                     tf.ControlStyle.Font.Bold = false;
                     tf.ControlStyle.Font.Size = FontUnit.Point(8);

                     tf.ItemTemplate = new GridViewTemplate("hyperlink", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);

                     TemplateField temp = new TemplateField();
                }
                else
                {
                     tf.HeaderText = dt.Columns[i].Caption.ToString();
                     tf.HeaderStyle.Wrap = false;
                     tf.HeaderStyle.CssClass = "fixCol";
                     tf.ItemStyle.Wrap = false;
                     tf.ItemStyle.CssClass = "fixCol";

                     tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                    
                }

                gv.Columns.Add(tf);
            }

            CommandField cfEdit = new CommandField();
            cfEdit.ButtonType = ButtonType.Link;
            cfEdit.ShowEditButton = true;
            cfEdit.CausesValidation = false;
            cfEdit.ItemStyle.Width = Unit.Pixel(80);
            cfEdit.HeaderStyle.Wrap = false;
            cfEdit.ItemStyle.Wrap = false;

            cfEdit.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gv.Columns.Insert(7,cfEdit);

            gv.DataSource = dt;
            gv.DataBind();
        }
        public DataTable GetMstr(string condition, string year)
        {
            try
            {
                DataSet dst = null;
                string strName = "sp_bg_getMstr";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@condition", condition);
                parm[1] = new SqlParameter("@year", year);

                dst = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm);

                return dst.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataTable QueryMstr(string condition)
        {
            try
            {
                DataSet dst = null;
                string strName = "select distinct t.*,";
                strName += "str(bg_year,len(bg_year))+case when bg_per<10 then '0'+str(bg_per,len(bg_per)) else str(bg_per,len(bg_per)) end period, ";
                strName += "case when bg_ecurr_amt is null then bg_budget else bg_budget-bg_ecurr_amt end diff ";
                strName += "from( ";
                strName += "select m.bg_master,m.bg_masterC,m.bg_dept,m.bg_acc,m.bg_sub,m.bg_desc,m.bg_cc,m.bg_project,m.bg_year,m.bg_per,m.bg_ecurr_amt,b.bg_budget,m.bg_reader ";
                strName += "from tcpc0.dbo.bg_mstr m  ";
                strName += "left join tcpc0.dbo.bg_mstr_bg b on m.bg_master=b.bg_master and m.bg_dept=b.bg_dept and m.bg_acc=b.bg_acc and m.bg_sub=b.bg_sub and m.bg_project=b.bg_project and m.bg_year=b.bg_year and m.bg_per=b.bg_per ";
                strName += "union  ";
                strName += "select bg_master,bg_masterC,bg_dept,bg_acc,bg_sub,bg_desc,bg_cc,bg_project,bg_year,bg_per,bg_ecurr_amt,bg_budget,bg_reader from tcpc0.dbo.bg_mstr_bg ";
                strName += " where not exists(select * from bg_mstr where bg_mstr.bg_master = bg_mstr_bg.bg_master and bg_mstr.bg_dept = bg_mstr_bg.bg_dept and bg_mstr.bg_acc = bg_mstr_bg.bg_acc ";
                strName += " and bg_mstr.bg_sub = bg_mstr_bg.bg_sub and bg_mstr.bg_project = bg_mstr_bg.bg_project  and bg_mstr.bg_year = bg_mstr_bg.bg_year and bg_mstr.bg_per = bg_mstr_bg.bg_per)";
                strName += ") t ";

                strName += condition;

                dst = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strName);

                return dst.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public void ModifyMstr(string budget, string master, string dept, string acc, string sub, string project, string cc, string year, string month,ref int msg)
        {
            try
            {
                string strName = "sp_bg_updateMstr";
                SqlParameter[] parm = new SqlParameter[10];
                parm[0] = new SqlParameter("@budget", budget);
                parm[1] = new SqlParameter("@master", master);
                parm[2] = new SqlParameter("@dept", dept);
                parm[3] = new SqlParameter("@acc", acc);
                parm[4] = new SqlParameter("@sub", sub);
                parm[5] = new SqlParameter("@project", project);
                parm[6] = new SqlParameter("@cc", cc);
                parm[7] = new SqlParameter("@year", year);
                parm[8] = new SqlParameter("@month", month);
                parm[9] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[9].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm);

                msg = int.Parse(parm[9].Value.ToString());
            }
            catch
            {
                return;
            }
        }

        #endregion

        #region acd
        public DataTable GetAcd(string master, string dept, string acc, string sub, string project, string year, string per)
        {
            try
            {
                DataSet dst = null;
                string strName = "sp_bg_acd";
                SqlParameter[] parm = new SqlParameter[7];
                parm[0] = new SqlParameter("@master", master);
                parm[1] = new SqlParameter("@dept", dept);
                parm[2] = new SqlParameter("@acc", acc);
                parm[3] = new SqlParameter("@sub", sub);
                parm[4] = new SqlParameter("@project", project);
                parm[5] = new SqlParameter("@year", year);
                parm[6] = new SqlParameter("@per", per);

                dst = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm);

                return dst.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region mstr_edit
        public DataTable GetMstrEdit(string master, string dept, string acc, string sub, string project,string year)
        {
            try
            {
                DataSet dst = null;
                string strName = "sp_bg_getMstrEdit";
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@master", master);
                parm[1] = new SqlParameter("@dept", dept);
                parm[2] = new SqlParameter("@acc", acc);
                parm[3] = new SqlParameter("@sub", sub);
                parm[4] = new SqlParameter("@project", project);
                parm[5] = new SqlParameter("@year", year);

                dst = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm);

                return dst.Tables[0];
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Audit
        /// <summary>
        /// Get User List
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns>DataSet</returns>
        public static DataSet getUserList(string strsql)
        {
            string sqlCommand = "sp_bg_getUserList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "strsql", DbType.String, strsql.Trim());

            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Get Audit List
        /// </summary>
        /// <param name="strDept"></param>
        /// <param name="strUser"></param>
        /// <returns>DataSet</returns>
        public static DataSet getAuditList(string strDept, string strUser, string strPlant)
        {
            string sqlCommand = "sp_bg_getAuditList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "dept", DbType.String, strDept);
            db.AddInParameter(dbCommand, "user", DbType.String, strUser);
            db.AddInParameter(dbCommand, "plant", DbType.String, strPlant);
            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Insert Audit
        /// </summary>
        /// <param name="strDept"></param>
        /// <param name="strUser"></param>
        /// <param name="strLevel"></param>
        /// <param name="strPlant"></param>
        /// <returns>Boolean</returns>
        public static int InsertAudit(string strDept, string strUser, string strLevel, string strParent, string strPlant)
        {
            string sqlCommand = "sp_bg_InsertAuditList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "dept", DbType.Int32, Convert.ToInt32(strDept));
            db.AddInParameter(dbCommand, "user", DbType.Int64, Convert.ToInt64(strUser));
            db.AddInParameter(dbCommand, "level", DbType.Int32, Convert.ToInt32(strLevel.Trim()));
            db.AddInParameter(dbCommand, "parent", DbType.Int32, Convert.ToInt32(strParent.Trim()));
            db.AddInParameter(dbCommand, "plant", DbType.Int32, Convert.ToInt32(strPlant));
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return 0;
            }
        }

        /// <summary>
        /// Check Audit has child
        /// </summary>
        /// <param name="strAudit"></param>
        /// <returns>Boolean</returns>
        public static bool CheckAuditChild(string strAudit)
        {
            string sqlCommand = "sp_bg_CheckAuditChild";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "audit", DbType.Int32, Convert.ToInt32(strAudit));
            
            try
            {
                return Convert.ToBoolean(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        public static bool DeleteAudit(string strAudit)
        {
            string sqlCommand = "sp_bg_DeleteAudit";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "audit", DbType.Int32, Convert.ToInt32(strAudit));

            try
            {
                return Convert.ToBoolean(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        #endregion

        #region Apply

        /// <summary>
        /// 获取费用申请列表
        /// </summary>
        /// <param name="strDept">部门</param>
        /// <param name="strUser">申请人</param>
        /// <param name="strKeyword">关键词</param>
        /// <param name="strPlant">所属公司</param>
        /// <param name="strYear">年</param>
        /// <param name="strMonth">月</param>
        /// <param name="isMeOnly">只显示自己需审核的</param>
        /// <param name="isAll">显示全部</param>
        /// <returns>DataSet</returns>
        public static DataSet getApplyList(string strDept, string strUser, string strKeyword, string strPlant, string strYear, string strMonth, long isMeOnly, int isAll)
        {
            string sqlCommand = "sp_bg_getApplyList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "dept", DbType.String, strDept);
            db.AddInParameter(dbCommand, "user", DbType.String, strUser);
            db.AddInParameter(dbCommand, "key", DbType.String, strKeyword);
            db.AddInParameter(dbCommand, "plant", DbType.String, strPlant);
            db.AddInParameter(dbCommand, "year", DbType.String, strYear);
            db.AddInParameter(dbCommand, "month", DbType.String, strMonth);
            db.AddInParameter(dbCommand, "me", DbType.Int64, isMeOnly);
            db.AddInParameter(dbCommand, "all", DbType.Int32, isAll);

            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得费用用途信息
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet getSubAccount()
        {
            string sqlCommand = "sp_bg_getSubAccount";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得成本中心信息
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet getCostCenter()
        {
            string sqlCommand = "sp_bg_getCostCenter";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得账户信息
        /// </summary>
        /// <param name="strSub">费用用途</param>
        /// <param name="strCC">成本中心</param>
        /// <returns>账户信息</returns>
        public static string getAccount(string strSub, string strCC)
        {
            string sqlCommand = "sp_bg_getAccount";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "sub", DbType.String, strSub);
            db.AddInParameter(dbCommand, "cc", DbType.String, strCC);

            try
            {
                return Convert.ToString(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 新增申请
        /// </summary>
        /// <param name="strFrom">申请人</param>
        /// <param name="strTo">受理人</param>
        /// <param name="strAccount">相关账户</param>
        /// <param name="strSub">费用用途</param>
        /// <param name="strCC">成本中心</param>
        /// <param name="strNotes">申请内容</param>
        /// <param name="strAmount">申请金额</param>
        /// <param name="strDesc">账户说明</param>
        /// <returns>申请ID</returns>
        public static Int64 InsertApply(string strFrom, string strFromName, string strTo, string strToName, string strCopyTo, string strAccount, string strSub, string strCC, string strNotes, string strAmount, string strDesc)
        {
            string sqlCommand = "sp_bg_InsertApply";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "from", DbType.Int64, Convert.ToInt64(strFrom));
            db.AddInParameter(dbCommand, "fromname", DbType.String, strFromName);
            db.AddInParameter(dbCommand, "to", DbType.Int64, Convert.ToInt64(strTo));
            //db.AddInParameter(dbCommand, "to", DbType.String, strTo);
            db.AddInParameter(dbCommand, "toname", DbType.String, strToName);
            db.AddInParameter(dbCommand, "copyto", DbType.String, strCopyTo);
            db.AddInParameter(dbCommand, "acc", DbType.String, strAccount);
            db.AddInParameter(dbCommand, "sub", DbType.String, strSub);
            db.AddInParameter(dbCommand, "cc", DbType.String, strCC);
            db.AddInParameter(dbCommand, "notes", DbType.String, strNotes);
            db.AddInParameter(dbCommand, "amount", DbType.Decimal, Convert.ToDecimal(strAmount));
            db.AddInParameter(dbCommand, "subdesc", DbType.String, strDesc);

            try
            {
                return Convert.ToInt64(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -1;
            }
        }

        /// <summary>
        /// Get User With Email
        /// </summary>
        /// <param name="strplant"></param>
        /// <param name="strdept"></param>
        /// <returns>DataSet</returns>
        public static DataSet getUserEmail(int intplant, int intdept, int intorg)
        {
            string sqlCommand = "sp_bg_getUserEmail";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "plant", DbType.Int32, intplant);
            db.AddInParameter(dbCommand, "dept", DbType.Int32, intdept);
            db.AddInParameter(dbCommand, "org", DbType.Int32, intorg);

            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 判断是否有审核权限
        /// </summary>
        /// <param name="strAID">ApplyID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public static int checkApplyEvaluate(string strAID, string strUID)
        {
            try
            {
                string sqlCommand = "sp_bg_CheckApplyEvaluate";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "aid", DbType.Int64, Convert.ToInt64(strAID));
                db.AddInParameter(dbCommand, "uid", DbType.Int64, Convert.ToInt64(strUID));

                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -2;
            }
        }

        /// <summary>
        /// 获得指定申请明细
        /// </summary>
        /// <param name="strAID">ApplyID</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader getApplyInfo(string strAID)
        {
            try
            {
                string sqlCommand = "sp_bg_getApplyInfo";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "aid", DbType.Int64, Convert.ToInt64(strAID));

                return (SqlDataReader)db.ExecuteReader(dbCommand);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 更新申请状态
        /// </summary>
        /// <param name="strAID">Apply ID</param>
        /// <param name="isPassed">是否通过</param>
        /// <returns>True/False</returns>
        public static bool UpdateApplyStatus(string strAID, string strUID, bool isPassed)
        {
            string sqlCommand = "sp_bg_UpdateApplyStatus";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "aid", DbType.Int64, Convert.ToInt64(strAID));
            db.AddInParameter(dbCommand, "uid", DbType.Int64, Convert.ToInt64(strUID));
            db.AddInParameter(dbCommand, "pass", DbType.Int32, isPassed == true ? 1: -1);

            try
            {
                return Convert.ToBoolean(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获得指定申请明细
        /// </summary>
        /// <param name="strAID">ApplyID</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader getApplyEvaluate(string strAID)
        {
            try
            {
                string sqlCommand = "sp_bg_getApplyEvaluate";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "aid", DbType.Int64, Convert.ToInt64(strAID));

                return (SqlDataReader)db.ExecuteReader(dbCommand);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得指定申请审核明细
        /// </summary>
        /// <param name="strAID">ApplyID</param>
        /// <returns>DataSet</returns>
        public static DataSet getApplyEvaluateInfo(string strAID)
        {
            string sqlCommand = "sp_bg_getApplyEvaluateInfo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "aid", DbType.Int64, Convert.ToInt64(strAID));
            
            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 更新费用申请
        /// </summary>
        /// <param name="strAID">Apply ID</param>
        /// <param name="strTID">提交至uID</param>
        /// <param name="strTo">提交至人名</param>
        /// <param name="strCopyTo">抄送至</param>
        /// <param name="strNote">审核备注</param>
        /// <param name="isPass">是否审核通过</param>
        /// <param name="intUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public static bool UpdateApplyDetail(string strAID, string strTID, string strTo, string strCopyTo, string strNote, int isPass, int isClose, long intUID)
        {
            string sqlCommand = "sp_bg_updateApplyDetail";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "aid", DbType.Int64, Convert.ToInt64(strAID));
            db.AddInParameter(dbCommand, "tid", DbType.Int64, Convert.ToInt64(strTID));
            db.AddInParameter(dbCommand, "to", DbType.String, strTo);
            db.AddInParameter(dbCommand, "copyto", DbType.String, strCopyTo);
            db.AddInParameter(dbCommand, "note", DbType.String, strNote);
            db.AddInParameter(dbCommand, "pass", DbType.Int32, isPass);
            db.AddInParameter(dbCommand, "close", DbType.Int32, isClose);
            db.AddInParameter(dbCommand, "uid", DbType.Int64, intUID);

            try
            {
                return Convert.ToBoolean(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获得预测金额
        /// </summary>
        /// <param name="strSub">费用用途</param>
        /// <param name="strCC">成本中心</param>
        /// <returns>预测金额</returns>
        public static string getBudgetValue(string strSub, string strCC)
        {
            string sqlCommand = "sp_bg_getBudgetValue";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "sub", DbType.String, strSub);
            db.AddInParameter(dbCommand, "cc", DbType.String, strCC);

            try
            {
                return Convert.ToString(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得累计申请金额
        /// </summary>
        /// <param name="strSub">费用用途</param>
        /// <param name="strCC">成本中心</param>
        /// <returns>累计申请金额</returns>
        public static string getCumulation(string strSub, string strCC)
        {
            string sqlCommand = "sp_bg_getCumulation";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "sub", DbType.String, strSub);
            db.AddInParameter(dbCommand, "cc", DbType.String, strCC);

            try
            {
                return Convert.ToString(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 更新实际费用
        /// </summary>
        /// <param name="strAID">Apply ID</param>
        /// <param name="strActual">实际费用</param>
        /// <returns>True/False</returns>
        public static bool updateApplyActual(string strAID, string strActual)
        {
            string sqlCommand = "sp_bg_updateApplyActual";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "aid", DbType.Int64, Convert.ToInt64(strAID));
            db.AddInParameter(dbCommand, "fee", DbType.Int64, Convert.ToDecimal(strActual));
            
            try
            {
                return Convert.ToBoolean(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获取需要填写实际费用列表
        /// </summary>
        /// <param name="strDept">部门</param>
        /// <param name="strUser">申请人</param>
        /// <param name="strKeyword">关键词</param>
        /// <param name="strPlant">所属公司</param>
        /// <param name="strYear">年</param>
        /// <param name="strMonth">月</param>
        /// <returns>DataSet</returns>
        public static DataSet getApplyActualList(string strDept, string strUser, string strKeyword, string strPlant, string strYear, string strMonth)
        {
            string sqlCommand = "sp_bg_getApplyActualList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "dept", DbType.String, strDept);
            db.AddInParameter(dbCommand, "user", DbType.String, strUser);
            db.AddInParameter(dbCommand, "key", DbType.String, strKeyword);
            db.AddInParameter(dbCommand, "plant", DbType.String, strPlant);
            db.AddInParameter(dbCommand, "year", DbType.String, strYear);
            db.AddInParameter(dbCommand, "month", DbType.String, strMonth);

            try
            {
                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获取费用申请列表(含实际费用)
        /// </summary>
        /// <param name="strDept">部门</param>
        /// <param name="strUser">申请人</param>
        /// <param name="strKeyword">关键词</param>
        /// <param name="strPlant">所属公司</param>
        /// <param name="strYear">年</param>
        /// <param name="strMonth">月</param>
        /// <returns>DataSet</returns>
        public static DataSet getApplyResultList(string strDept, string strUser, string strKeyword, string strPlant, string strYear, string strMonth)
        {
            string sqlCommand = "sp_bg_getApplyResultList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "dept", DbType.String, strDept);
            db.AddInParameter(dbCommand, "user", DbType.String, strUser);
            db.AddInParameter(dbCommand, "key", DbType.String, strKeyword);
            db.AddInParameter(dbCommand, "plant", DbType.String, strPlant);
            db.AddInParameter(dbCommand, "year", DbType.String, strYear);
            db.AddInParameter(dbCommand, "month", DbType.String, strMonth);

            try
            {
                string strResult = Convert.ToString(db.ExecuteScalar(dbCommand));

                dbCommand = db.GetSqlStringCommand(strResult);

                DataSet dst = db.ExecuteDataSet(dbCommand);

                return dst;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获取费用申请列表(含实际费用)
        /// </summary>
        /// <param name="strDept">部门</param>
        /// <param name="strUser">申请人</param>
        /// <param name="strKeyword">关键词</param>
        /// <param name="strPlant">所属公司</param>
        /// <param name="strYear">年</param>
        /// <param name="strMonth">月</param>
        /// <returns>String</returns>
        public static string getApplyResultExcel(string strDept, string strUser, string strKeyword, string strPlant, string strYear, string strMonth)
        {
            string sqlCommand = "sp_bg_getApplyResultList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "dept", DbType.String, strDept);
            db.AddInParameter(dbCommand, "user", DbType.String, strUser);
            db.AddInParameter(dbCommand, "key", DbType.String, strKeyword);
            db.AddInParameter(dbCommand, "plant", DbType.String, strPlant);
            db.AddInParameter(dbCommand, "year", DbType.String, strYear);
            db.AddInParameter(dbCommand, "month", DbType.String, strMonth);

            try
            {
                return Convert.ToString(db.ExecuteScalar(dbCommand));

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        #endregion
    }
}