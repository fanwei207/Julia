using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Configuration;
using adamFuncs;
using System.Collections.Generic;

namespace RD_WorkFlow
{
    /// <summary>
    /// Summary description for RD_WorkFlow
    /// </summary>
    public class RD_Steps
    {
        String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];

        #region Add LIU junhong
        private string step;
        public string Step
        {
            get;
            set;
        }
        private long stepId;
        public long StepId
        {
            get;
            set;
        }

        private string stepName;
        public string StepName
        {
            get;
            set;
        }
        private string stepMember;
        public string StepMember
        {
            get;
            set;
        }
        private string stepPartner;
        public string StepPartner
        {
            get;
            set;
        }
        private string stepPartnerID;
        public string StepPartnerID
        {
            get;
            set;
        }
        private string stepMemberID;
        public string StepMemberID
        {
            get;
            set;
        }
        private int duration;
        public int Duration
        {
            get;
            set;
        }

        #endregion

        public RD_Steps()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        #region Company Maintenance
        public DataSet SelectCompany()
        {
            try
            {
                string strName = "sp_RDW_SelectCompany";

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Steps Maintenance

        public DataSet SelectSteps(int mid)
        {
            try
            {
                string strName = "sp_RDW_SelectSteps";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@mid", mid);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public DataSet selectprojectstep(string projectname, string projectcode)
        {
            try
            {
                string strName = "sp_RDW_SelectProjectStep";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@projectname", projectname);
                param[1] = new SqlParameter("@projectcode", projectcode);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }

        }

        public DataSet SelectDetSteps(int mid, int uID)
        {
            try
            {
                string strName = "sp_RDW_SelectDetSteps";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public int InsertSteps(int mid, string step, string missions, string stddate, string enddate, int uID)
        {
            try
            {
                string strName = "sp_RDW_InsertSteps";
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@step", step);
                param[2] = new SqlParameter("@missions", missions);
                param[3] = new SqlParameter("@stddate", stddate);
                param[4] = new SqlParameter("@enddate", enddate);
                param[5] = new SqlParameter("@uID", uID);
                param[6] = new SqlParameter("@returnVal", SqlDbType.Int);
                param[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return int.Parse(param[6].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        public bool InsertDetSteps(int mid, int sid, int uID)
        {
            try
            {
                string strName = "sp_RDW_InsertDetSteps";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@sid", sid);
                param[2] = new SqlParameter("@uID", uID);

                int nRet = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                if (nRet < 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public int UpdateSteps(int mid, int id, string step, string missions, string stddate, string enddate, int uID)
        {
            try
            {
                string strName = "sp_RDW_UpdateSteps";
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@step", step);
                param[3] = new SqlParameter("@missions", missions);
                param[4] = new SqlParameter("@stddate", stddate);
                param[5] = new SqlParameter("@enddate", enddate);
                param[6] = new SqlParameter("@uID", uID);
                param[7] = new SqlParameter("@returnVal", SqlDbType.Int);
                param[7].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return int.Parse(param[7].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        public bool DeleteSteps(int mid, int id)
        {
            try
            {
                string strName = "sp_RDW_DeleteSteps";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@id", id);

                int nRet = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                if (nRet > -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool StepsUp(int mid, string org, string up)
        {
            try
            {
                string strName = "sp_RDW_StepsUp";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@org", org);
                param[2] = new SqlParameter("@up", up);

                int nRet = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                if (nRet > -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool StepsDown(int mid, string org, string down)
        {
            try
            {
                string strName = "sp_RDW_StepsDown";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@org", org);
                param[2] = new SqlParameter("@down", down);

                int nRet = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                if (nRet > -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        //public DataSet SelectReviewers(int plantid, int id, int type, int departmentId, string userName, bool p)
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="department"></param>
        /// <param name="userName"></param>
        /// <param name="isleave">离职人员</param>
        /// <returns></returns>
        public DataSet SelectReviewers(int plant, int id, int type, int department, string userName, bool isleave)
        {
            try
            {
                string strName = "sp_RDW_SelectReviewers";
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@plant", plant);
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@type", type);
                param[3] = new SqlParameter("@department", department);
                param[4] = new SqlParameter("@userName", userName);
                param[5] = new SqlParameter("@isleave", isleave);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public DataSet SelectReviewersAll(int plant, int id, int type)
        {
            try
            {
                string strName = "sp_RDW_SelectReviewersAll";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@plant", plant);
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@type", type);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }


        public bool UpdateReviewers(int id, int type, string reviewers, string reviewernames, int uID)
        {
            //try
            //{
            string strName = "sp_RDW_UpdateReviewers";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@type", type);
            param[2] = new SqlParameter("@reviewers", reviewers);
            param[3] = new SqlParameter("@reviewernames", reviewernames);
            param[4] = new SqlParameter("@uID", uID);

            int nRet = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (nRet > -1)
                return true;
            else
                return false;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public bool DeleteReviewers(int id, int type, int uID)
        {
            try
            {
                string strName = "sp_RDW_DeleteReviewers";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@type", type);
                param[2] = new SqlParameter("@uID", uID);

                int nRet = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                if (nRet > -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public string getLeader(long intMID)
        {
            try
            {
                string strName = "sp_rdw_SelectLeader";
                SqlParameter param = new SqlParameter("@mid", intMID);

                return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strName, param));
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region Add by shanzm 2011.12.13

        public bool DeleteTasks(int mid, int id)
        {
            try
            {
                string strName = "sp_RDW_DeleteTasks";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@id", id);

                int nRet = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                if (nRet > -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public DataSet SelectStep_Mbr(string stepid)
        {
            string strSql = "sp_RDW_SelectTemplateStepEvaluator";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@stepid", stepid);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param);
        }

        public DataSet SelectStep_Ptr(string stepid)
        {
            string strSql = "sp_RDW_SelectTemplateStepPartner";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@stepid", stepid);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param);
        }

        public IList<RD_Steps> SelectTasks(int mid)
        {
            try
            {
                string strName = "sp_RDW_SelectTasks";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@mid", mid);
                IList<RD_Steps> rd_steps = new List<RD_Steps>();
                SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strName, param);
                while (reader.Read())
                {
                    RD_Steps rd_step = new RD_Steps();

                    rd_step.StepId = Convert.ToInt32(reader["Stepid"]);
                    rd_step.Step = reader["Step"].ToString();
                    rd_step.StepName = reader["StepName"].ToString();
                    rd_step.Duration = Convert.ToInt32(reader["RDW_Duration"]);


                    #region Approver
                    strName = "sp_RDW_SelectTemplateStepEvaluator";
                    param[0] = new SqlParameter("@stepid", rd_step.StepId);

                    string strEvaluator = "";
                    string strEvaluatorName = "";
                    SqlDataReader readerEval = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strName, param);
                    while (readerEval.Read())
                    {
                        //if (Convert.ToInt32(readerEval["RDW_UserID"])==0)
                        //{
                        //    strEvaluator="";
                        //    strEvaluatorName = "Add";
                        //}
                        strEvaluator += readerEval["UserID"].ToString() + ";";
                        strEvaluatorName += readerEval["RDW_Member"].ToString() + "<br>";
                    }
                    readerEval.Close();

                    if (strEvaluator.Length > 0)
                    {
                        rd_step.StepMember = strEvaluatorName.Substring(0, strEvaluatorName.Length - 4);
                        rd_step.StepMemberID = ";" + strEvaluator;
                    }
                    else
                    {
                        rd_step.StepMember = strEvaluatorName;
                        rd_step.StepMemberID = strEvaluator;
                    }
                    #endregion

                    #region Partner
                    strName = "sp_RDW_SelectTemplateStepPartner";
                    param[0] = new SqlParameter("@stepid", rd_step.StepId);

                    string strPartner = "";
                    string strPartnerName = "";
                    SqlDataReader readerPartner = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strName, param);
                    while (readerPartner.Read())
                    {
                        strPartner += readerPartner["UserID"].ToString() + ";";
                        strPartnerName += readerPartner["RDW_Partner"].ToString() + "<BR>";
                    }
                    readerPartner.Close();

                    if (strPartner.Length > 0)
                    {
                        rd_step.StepPartnerID = ";" + strPartner;
                        rd_step.StepPartner = strPartnerName.Substring(0, strPartnerName.Length - 4);
                    }
                    else
                    {
                        rd_step.StepPartnerID = strPartner;
                        rd_step.StepPartner = strPartnerName;
                    }
                    #endregion

                    rd_steps.Add(rd_step);
                }
                reader.Close();
                return rd_steps;
            }
            catch
            {
                return null;
            }
        }

        public DataSet SelectTaskById(int mid, int stepid)
        {
            try
            {
                string strName = "sp_RDW_SelectTaskById";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@stepid", stepid);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public DataSet SelectStepById(int mid, int stepid)
        {
            try
            {
                string strName = "sp_RDW_SelectStepById";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@stepid", stepid);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public void CustomSortTasks(string mid, string stepid, string cmd)
        {
            try
            {
                string strName = "sp_RDW_CustomSortTasks";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@stepid", stepid);
                param[2] = new SqlParameter("@cmd", cmd);

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {

            }
        }
        #endregion

        /// <summary>
        ///  获取添加步骤同级步骤
        /// </summary>
        /// <param name="p"> 项目ID</param>
        /// <param name="p_2">ParentID</param>
        /// <returns></returns>
        public DataTable getDetSiblingSteps(string mid, string dParentIdeas)
        {
            try
            {
                string strName = "sp_RDW_SelectDetSiblingSteps";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@dpid", dParentIdeas);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///  获取添加步骤同级步骤
        /// </summary>
        /// <param name="mid">项目ID</param>
        /// <param name="dParentIdeas">本步骤ID</param>
        /// <param name="type">类型修改</param>
        /// <returns></returns>
        public DataTable getDetSiblingSteps(string mid, string dParentIdeas, string type)
        {
            try
            {
                string strName = "sp_RDW_SelectDetSiblingSteps";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@dpid", dParentIdeas);
                param[2] = new SqlParameter("@type", type);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取模板中已经有的步骤(不包含子步骤)
        /// 新建模板时，维护前置步骤
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="dParentIdeas"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable getTempSteps(string tempid)
        {
            try
            {
                string strName = "sp_RDW_SelectTempSteps";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@tempid", tempid);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 根据前置步骤设置默认起始时间
        /// 维护项目的步骤时用到
        /// </summary>
        /// <param name="tempid"></param>
        /// <returns></returns>
        public string getStartTimeByPredecessor(string did, string mid, string type)
        {
            try
            {
                string strName = "sp_RDW_SelectStartTimeByPredecessor";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@did", did);
                param[1] = new SqlParameter("@mid", mid);
                param[2] = new SqlParameter("@type", type);

                return SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strName, param).ToString();
            }
            catch
            {
                return null;
            }
        }

        public bool CheckEnglishName(string id)
        {
            try
            {
                string strName = "sp_RDW_SelectUser";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@uid", id);

                var reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strName, param);
                if (reader.Read())
                {
                    string englishName = reader["englishname"].ToString();
                    if (!string.IsNullOrEmpty(englishName))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 复制别的项目的步骤
        /// </summary>
        /// <param name="did"></param>
        /// <param name="mid"></param>
        /// <returns></returns>
        public int CopySteps(string did, string mid, string uID, string stepforeId)
        {
            string strName = "sp_RDW_CopySteps";

            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@did", did);
                param[1] = new SqlParameter("@mid", Convert.ToInt32(mid));
                param[2] = new SqlParameter("@uID", Convert.ToInt32(uID));
                param[3] = new SqlParameter("@retValue", DbType.Int32);
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@stepforeId", Convert.ToInt32(stepforeId));

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
                return Convert.ToInt32(param[3].Value);
            }
            catch
            {
                return 0;
            }
        }
        
    }

}