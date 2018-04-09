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
using CommClass;
using System.Data.OleDb;
using System.Text.RegularExpressions;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Data;
using System.Text;
using System.Text;


namespace MRInfo
{
    /// <summary>
    /// Summary description for PI
    /// </summary>
    public class MeetingRoom
    {
        adamClass adam = new adamClass();
        String strSQL = "";

        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public MeetingRoom()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ��ȡ��������Ϣ
        /// </summary>
        public DataTable GetMeetingRoomNO(String plants,string roomid)
        {
            strSQL = "sp_mr_selectroominfo";
            SqlParameter[] parma = new SqlParameter[2];
            parma[0] = new SqlParameter("@plants", plants);
            parma[1] = new SqlParameter("@roomid", roomid);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }

        /// <summary>
        /// ��ȡ��������Ϣ
        /// </summary>
        public DataTable GeDepartMentInfo(string Domain)
        {
            strSQL = "sp_mr_selectDepartMentinfo";
            SqlParameter[] parma = new SqlParameter[1];
            parma[0] = new SqlParameter("@plantcode", Domain);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }

        /// <summary>
        /// ��ȡ���й�˾���
        /// </summary>
        public DataTable GetCompanyList(String DID)
        {
            strSQL = "sp_mr_selectplantsinfo";
            SqlParameter[] parma = new SqlParameter[0];
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }
        /// <summary>
        /// ��ȡ��������Ʒ��Ϣ
        /// </summary>
        public DataTable GetMeetingRoomGoodsList(String CompanyCode,string RoomID)
        {
            strSQL = "sp_mr_selectroomgoods";
            SqlParameter[] parma = new SqlParameter[2];
            parma[0] = new SqlParameter("@companycode", CompanyCode);
            parma[1] = new SqlParameter("@roomid",RoomID);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int ImportRmData(string FormId,string DeptNo,string DeptMame,string AgentDate,string AppEno,string AppName,string AppExtNo,string CompanyCode
                                    ,string MeettingMemberNum,string Reason,string BorrowThings,string RoomID,string otherDes)
        {
            strSQL = "sp_mr_saveapp";
            SqlParameter[] parm = new SqlParameter[13];
            parm[0] = new SqlParameter("@FormId", FormId);
            parm[1] = new SqlParameter("@DeptNo", Convert.ToInt32(DeptNo));
            parm[2] = new SqlParameter("@DeptMame", DeptMame);
            parm[3] = new SqlParameter("@AgentDate", AgentDate);
            parm[4] = new SqlParameter("@AppEno", Convert.ToInt32(AppEno));
            parm[5] = new SqlParameter("@AppName", AppName);
            parm[6] = new SqlParameter("@AppExtNo", AppExtNo);
            parm[7] = new SqlParameter("@CompanyCode", CompanyCode);
            parm[8] = new SqlParameter("@MeettingMemberNum", Convert.ToInt32(MeettingMemberNum));
            parm[9] = new SqlParameter("@Reason", Reason);
            parm[10] = new SqlParameter("@BorrowThings", BorrowThings);
            parm[11] = new SqlParameter("@RoomID", RoomID);
            parm[12] = new SqlParameter("@otherDes", otherDes);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }


        /// <summary>
        /// ����ʧ�ܣ�ɾ����¼
        /// </summary>
        public int DeleteRmDataByErr(string FormId)
        {
            strSQL = "sp_mr_Deleteapp";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@FormId", FormId);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }

        /// <summary>
        /// ����������ϸ
        /// </summary>
        public int ImportRmDetailsData(string FormId, DateTime RBeginTime, DateTime REndTime, bool IsFullDay,int uID, string uName)
        {
            strSQL = "sp_mr_saveDetails";
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@FormId", FormId);
            parm[1] = new SqlParameter("@RBeginTime", RBeginTime);
            parm[2] = new SqlParameter("@REndTime", REndTime);
            parm[3] = new SqlParameter("@IsFullDay", IsFullDay);
            parm[4] = new SqlParameter("@uID", uID);
            parm[5] = new SqlParameter("@uName", uName);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
        }

        /// <summary>
        /// ��ȡ������Ԥ����Ϣ
        /// </summary>
        public DataTable GetOrderMRAndDetails(String CompanyCode, string RoomID)
        {
            strSQL = "sp_mr_selectroomOrderDetails";
            SqlParameter[] parma = new SqlParameter[2];
            parma[0] = new SqlParameter("@companycode", CompanyCode);
            parma[1] = new SqlParameter("@roomid", RoomID);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }

        /// <summary>
        /// ���һ�����Ԥ����Ϣ
        /// </summary>
        public DataTable FindOrderMRAndDetails(int plantcode, string RoomID, string StartViewDay, string EndViewDay, string Domain, int DepartMent, bool Effective,bool isCheck)
        {
            strSQL = "sp_mr_FindroomOrderDetails";
            SqlParameter[] parma = new SqlParameter[8];
            parma[0] = new SqlParameter("@plantcode", plantcode);
            parma[1] = new SqlParameter("@RoomID", RoomID);
            parma[2] = new SqlParameter("@StartViewDay", StartViewDay);
            parma[3] = new SqlParameter("@EndViewDay", EndViewDay);
            parma[4] = new SqlParameter("@Domain", Domain);
            parma[5] = new SqlParameter("@DepartMent", DepartMent);
            parma[6] = new SqlParameter("@Effective", Effective);
            parma[7] = new SqlParameter("@isCheck", isCheck);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }
        public DataTable FindOrderApprMRAndDetails(int plantcode, string RoomID, string StartViewDay, string EndViewDay, string Domain, int DepartMent,int self,bool ischeck)
        {
            strSQL = "sp_mr_FindroomApprOrderDetails";
            SqlParameter[] parma = new SqlParameter[8];
            parma[0] = new SqlParameter("@plantcode", plantcode);
            parma[1] = new SqlParameter("@RoomID", RoomID);
            parma[2] = new SqlParameter("@StartViewDay", StartViewDay);
            parma[3] = new SqlParameter("@EndViewDay", EndViewDay);
            parma[4] = new SqlParameter("@Domain", Domain);
            parma[5] = new SqlParameter("@DepartMent", DepartMent);       
            parma[6] = new SqlParameter("@self", self);
            parma[7] = new SqlParameter("@ischeck", ischeck);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }
        /// <summary>
        /// �������ȡ������Ԥ����Ϣ
        /// </summary>
        public DataTable GetOrderMRAndDetailsByDay(String CompanyCode, string RoomID, DateTime days)
        {
            strSQL = "sp_mr_selectroomOrderDetailsByDay";
            SqlParameter[] parma = new SqlParameter[3];
            parma[0] = new SqlParameter("@companycode", CompanyCode);
            parma[1] = new SqlParameter("@roomid", RoomID);
            parma[2] = new SqlParameter("@days", days);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }

        /// <summary>
        /// �ж��Ƿ����������¼
        /// </summary>
        public DataTable CheckHaveReport(String CompanyCode, string RoomID, DateTime RBeginTime, DateTime REndTime,string uID)
        {
            strSQL = "sp_mr_selectCheckHaveReport";
            SqlParameter[] parma = new SqlParameter[5];
            parma[0] = new SqlParameter("@CompanyCode", CompanyCode);
            parma[1] = new SqlParameter("@RoomId", RoomID);
            parma[2] = new SqlParameter("@RBeginTime", RBeginTime);
            parma[3] = new SqlParameter("@REndTime", REndTime);
            parma[4] = new SqlParameter("@uID", uID);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }

        /// <summary>
        /// ��ȡ��������Ϣ
        /// </summary>
        public DataTable GetMeetingRoomNo(String CompanyCode, string @roomid)
        {
            String strSQL = "";
            strSQL = "sp_mr_selectroominfo";
            SqlParameter[] parma = new SqlParameter[2];
            parma[0] = new SqlParameter("@plants", CompanyCode);
            parma[1] = new SqlParameter("@roomid", "");
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }


        /// <summary>
        /// ɾ����������Ϣ�ж�
        /// </summary>
        public int DeleteMeetingRoomAppCheck(String CompanyCode, string roomid)
        {
            int nRet = -1;
            try
            {
                String strSQL = "";
                strSQL = "sp_mr_DeleteMeetingRommAppCheck";
                SqlParameter[] parma = new SqlParameter[2];
                parma[0] = new SqlParameter("@PlantCode", Convert.ToInt32(CompanyCode));
                parma[1] = new SqlParameter("@RoomId", roomid);
                nRet = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma);
            }
            catch
            {
                nRet = -1;
            }
            return nRet;
        }

        /// <summary>
        /// ��������ɾ����������Ϣ
        /// </summary>
        public int DeleteMeetingRoomApp(String CompanyCode, string roomid, DateTime begintime, DateTime endtime, string uID, string uName, string FormId)
        {
            int nRet = -1;
            try
            {
                String strSQL = "";
                strSQL = "sp_mr_DeleteMeetingRommApp";
                SqlParameter[] parma = new SqlParameter[7];
                parma[0] = new SqlParameter("@PlantCode", Convert.ToInt32(CompanyCode));
                parma[1] = new SqlParameter("@RoomId", roomid);
                parma[2] = new SqlParameter("@BeginTime", begintime);
                parma[3] = new SqlParameter("@EndTime", endtime);
                parma[4] = new SqlParameter("@uID", uID);
                parma[5] = new SqlParameter("@uName", uName);
                parma[6] = new SqlParameter("@FormId", FormId);

                nRet = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma);
            }
            catch
            {
               nRet = -1;
            }
            return nRet;
        }

        /// <summary>
        /// ɾ���������Ա�ʼ�֪ͨ
        /// </summary>
        public DataTable GetMeetingRoomEmail(string roomid)
        {
                String strSQL = "";
                strSQL = "sp_mr_GetMeetingRommEmail";
                SqlParameter[] parma = new SqlParameter[1];
                parma[0] = new SqlParameter("@RoomId", roomid);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }



        /// <summary>
        /// �޸Ļ����������ж��Ƿ����������¼
        /// </summary>
        public DataTable CheckHaveReportByUpdate(String CompanyCode, string FormID, DateTime RBeginTime, DateTime REndTime)
        {
            strSQL = "sp_mr_selectCheckHaveReportByUpdate";
            SqlParameter[] parma = new SqlParameter[4];
            parma[0] = new SqlParameter("@CompanyCode", CompanyCode);
            parma[1] = new SqlParameter("@FormID", FormID);
            parma[2] = new SqlParameter("@RBeginTime", RBeginTime);
            parma[3] = new SqlParameter("@REndTime", REndTime);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }


        /// <summary>
        /// ���»���������ʱ��
        /// </summary>
        public int UpdateMeetingDate(int uID, string FormID, string StartDate, string EndDate, int id)
        {
            int nRet = -1;
            try
            {
                String strSQL = "";
                strSQL = "sp_mr_UpdateMeetingDate";
                SqlParameter[] parma = new SqlParameter[5];
                parma[0] = new SqlParameter("@uID", Convert.ToInt32(uID));
                parma[1] = new SqlParameter("@FormID", FormID);
                parma[2] = new SqlParameter("@StartDate", StartDate);
                parma[3] = new SqlParameter("@EndDate", EndDate);
                parma[4] = new SqlParameter("@id", id);
                nRet = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma);
            }
            catch
            {
                nRet = -1;
            }
            return nRet;
        }




        public System.Data.DataTable GetApplyMeetingRoomEmail(string roomid)
        {
            String strSQL = "";
            strSQL = "sp_mr_GetApplyMeetingRommEmail";
            SqlParameter[] parma = new SqlParameter[1];
            parma[0] = new SqlParameter("@RoomId", roomid);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }

        public System.Data.DataTable GetApproveMeetingRoomEmail(int createby)
        {
            String strSQL = "";
            strSQL = "sp_mr_GetApproveMeetingRommEmail";
            SqlParameter[] parma = new SqlParameter[2];
            parma[0] = new SqlParameter("@createby", createby);
            //parma[1] = new SqlParameter("@FormId", FormId);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }
    }

}