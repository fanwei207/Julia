using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using adamFuncs;

namespace WO2Group
{
    /// <summary>
    /// Summary description for wo2Group
    /// </summary>
    public class WO2
    {
        adamClass adam = new adamClass();

        public WO2()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        /// <summary>
        /// ��ȡ�û�����Ϣ
        /// </summary>
        /// <param name="intCode"></param>
        /// <param name="strName"></param>
        /// <returns>�����û�����Ϣ�б�</returns>
        public IList<WO2_Group> SelectGroupList(string strCode, string strName)
        {
            try
            {
                string strSql = "sp_wo2_SelectGroup";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@code", strCode);
                sqlParam[1] = new SqlParameter("@name", strName);

                IList<WO2_Group> Group = new List<WO2_Group>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    WO2_Group wg = new WO2_Group();
                    wg.GroupID = Convert.ToInt32(reader["GroupID"]);
                    wg.GroupCode = reader["GroupCode"].ToString().Trim();
                    wg.GroupName = reader["GroupName"].ToString().Trim();
                    wg.GroupCount = Convert.ToInt32(reader["GroupCount"]);
                    wg.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    wg.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    Group.Add(wg);
                }
                reader.Close();
                return Group;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// �����û�����Ϣ
        /// </summary>
        /// <param name="intCode"></param>
        /// <param name="strName"></param>
        /// <param name="intUID"></param>
        /// <returns>�����Ƿ������ɹ�</returns>
        public bool InsertGroup(int intCode, string strName, int uID)
        {
            try
            {
                string strSql = "sp_wo2_InsertGroup";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@code", intCode);
                sqlParam[1] = new SqlParameter("@name", strName);
                sqlParam[2] = new SqlParameter("@uID", uID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ɾ���û�����Ϣ
        /// </summary>
        /// <param name="intGroupID"></param>
        /// <returns>�����Ƿ�ɾ���ɹ�</returns>
        public bool DeleteGroup(int intGroupID)
        {
            try
            {
                string strSql = "sp_wo2_DeleteGroup";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@groupID", intGroupID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// �����û�����Ϣ
        /// </summary>
        /// <param name="intGroupID"></param>
        /// <param name="strGroupName"></param>
        /// <returns>�����Ƿ���³ɹ�</returns>
        public bool UpdateGroup(int intGroupID, string strName)
        {
            try
            {
                string strSql = "sp_wo2_UpdateGroup";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@groupID", intGroupID);
                sqlParam[1] = new SqlParameter("@name", strName);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ��ȡ�û�����ϸ��Ϣ
        /// </summary>
        /// <param name="intCode"></param>
        /// <param name="strName"></param>
        /// <returns>�����û�����ϸ��Ϣ�б�</returns>
        public IList<WO2_GroupDetail> SelectGroupDetailList(int intGroupID)
        {
            try
            {
                string strSql = "sp_wo2_SelectGroupDetail";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@groupID", intGroupID);

                IList<WO2_GroupDetail> GroupDetail = new List<WO2_GroupDetail>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    WO2_GroupDetail wgd = new WO2_GroupDetail();
                    wgd.DetailID = Convert.ToInt32(reader["DetailID"]);
                    wgd.UserNo = reader["UserNo"].ToString().Trim();
                    wgd.UserName = reader["UserName"].ToString().Trim();
                    wgd.GroupID = Convert.ToInt32(reader["GroupID"]);
                    wgd.MOPID = Convert.ToInt32(reader["MopID"]);
                    wgd.MOPName = reader["MopName"].ToString().Trim();
                    wgd.MOPProc = reader["MopProc"].ToString().Trim();
                    wgd.SOPID = Convert.ToInt32(reader["SopID"]);
                    wgd.SOPName = reader["SopName"].ToString().Trim();
                    wgd.SOPProc = reader["SopProc"].ToString().Trim();
                    wgd.SOPRate = Convert.ToDecimal(reader["SopRate"]);
                    wgd.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    wgd.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    GroupDetail.Add(wgd);
                }
                reader.Close();
                return GroupDetail;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ɾ���û�����ϸ��Ϣ
        /// </summary>
        /// <param name="intGroupID"></param>
        /// <returns>�����Ƿ�ɾ���ɹ�</returns>
        public bool DeleteGroupDetail(int intDetailID)
        {
            try
            {
                string strSql = "sp_wo2_DeleteGroupDetail";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@detailID", intDetailID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ��ȡ�û�����Ϣ
        /// </summary>
        /// <returns>�����û�����Ϣ</returns>
        public DataTable SelectGroupInfo()
        {
            try
            {
                string strSql = "sp_wo2_SelectGroupInfo";
                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns>���ع�����Ϣ</returns>
        public DataTable SelectMOPInfo()
        {
            try
            {
                string strSql = "sp_wo2_SelectMOPInfo";
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ��λ��Ϣ
        /// </summary>
        /// <param name="intMOPID"></param>
        /// <returns>���ظ�λ��Ϣ</returns>
        public DataTable SelectSOPInfo(int intMopProc)
        {
            try
            {
                string strSql = "sp_wo2_SelectSOPInfo";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@mopproc", intMopProc);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns>���ز�����Ϣ</returns>
        public DataTable SelectDepartmentInfo()
        {
            try
            {
                string strSql = "sp_wo2_SelectDepartmentInfo";
                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="intDeptID"></param>
        /// <returns>���ع�����Ϣ</returns>
        public DataTable SelectWorkShopInfo(int intDeptID)
        {
            try
            {
                string strSql = "sp_wo2_SelectWorkShopInfo";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@DeptID", intDeptID);

                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="intDeptID"></param>
        /// <returns>���ع�����Ϣ</returns>
        public DataTable SelectWorkTypeInfo(int intWorkShopID)
        {
            try
            {
                string strSql = "sp_wo2_SelectWorkTypeInfo";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@WorkShopID", intWorkShopID);

                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡԱ����Ϣ
        /// </summary>
        /// <param name="intDeptID"></param>
        /// <param name="intWSID"></param>
        /// <param name="intWTID"></param>
        /// <returns>����Ա����Ϣ</returns>
        public DataTable SelectUserInfo(int intDeptID, int intWSID, int intWTID, int intPlantID)
        {
            try
            {
                string strSql = "sp_wo2_SelectUserInfo";

                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@DeptID", intDeptID);
                sqlParam[1] = new SqlParameter("@WSID", intWSID);
                sqlParam[2] = new SqlParameter("@WTID", intWTID);
                sqlParam[3] = new SqlParameter("@plantID", intPlantID);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// �ж��û��Ƿ�ѡ��
        /// </summary>
        /// <param name="intGroupID"></param>
        /// <param name="intUserID"></param>
        /// <returns>�����Ƿ�ѡ��</returns>
        public bool IsUserChecked(int intGroupID, int intUserID,int intMop, int intSop)
        {
            try
            {
                string strSql = "sp_wo2_IsUserChecked";

                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@GroupID", intGroupID);
                sqlParam[1] = new SqlParameter("@UserID", intUserID);
                sqlParam[2] = new SqlParameter("@mopID", intMop);
                sqlParam[3] = new SqlParameter("@sopID", intSop);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// �����û���Ա��Ϣ
        /// </summary>
        /// <param name="intGroupID"></param>
        /// <param name="intUserID"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="intMOPID"></param>
        /// <param name="strMOPProc"></param>
        /// <param name="strMOPName"></param>
        /// <param name="intSOPID"></param>
        /// <param name="strSOPProc"></param>
        /// <param name="strSOPName"></param>
        /// <param name="SopRate"></param>
        /// <param name="isCheck"></param>
        /// <returns>�����Ƿ���³ɹ�</returns>
        public bool UpdateGroupDetail(int intGroupID, int intUserID, string strUserNo, string strUserName, int intMOPID, string strMOPProc, string strMOPName, int intSOPID, string strSOPProc, string strSOPName, decimal SopRate, bool isCheck, int intUID)
        {
            try
            {
                string strSql = "sp_wo2_UpdateGroupDetail";

                SqlParameter[] sqlParam = new SqlParameter[13];
                sqlParam[0] = new SqlParameter("@GroupID", intGroupID);
                sqlParam[1] = new SqlParameter("@UserID", intUserID);
                sqlParam[2] = new SqlParameter("@UserNo", strUserNo);
                sqlParam[3] = new SqlParameter("@UserName", strUserName);
                sqlParam[4] = new SqlParameter("@MOPID", intMOPID);
                sqlParam[5] = new SqlParameter("@MOPProc", strMOPProc);
                sqlParam[6] = new SqlParameter("@MOPName", strMOPName);
                sqlParam[7] = new SqlParameter("@SOPID", intSOPID);
                sqlParam[8] = new SqlParameter("@SOPProc", strSOPProc);
                sqlParam[9] = new SqlParameter("@SOPName", strSOPName);
                sqlParam[10] = new SqlParameter("@SOPRate", SopRate);
                sqlParam[11] = new SqlParameter("@IsCheck", isCheck);
                sqlParam[12] = new SqlParameter("@uID", intUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ��ȡ�û��鹤��,��λ��Ϣ
        /// </summary>
        /// <param name="intGroupID"></param>
        /// <returns>�����û��鹤��,��λ��Ϣ</returns>
        public SqlDataReader SelectGroupDetailInfo(int intGroupID)
        {
            try
            {
                string strSql = "sp_wo2_SelectGroupDetailInfo";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@GroupID", intGroupID);

                return SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch
            {
                return null;
            }
        }
    }
}
