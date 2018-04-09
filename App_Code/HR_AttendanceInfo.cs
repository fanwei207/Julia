using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;

namespace Wage
{
    /// <summary>
    /// Ա��������Ϣ DB:hr_AttendanceInfo
    /// </summary>
    public class HR_AttendanceInfo
    {
        adamClass adam = new adamClass();
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public HR_AttendanceInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //private adamClass chk = new adamClass();
        //������ID
        private long _AttendanceID;
        public long AttendanceID
        {
            get { return _AttendanceID; }
            set { _AttendanceID = value; }
        }

        //���ڻ�LogID
        private long _LogID;
        public long LogID
        {
            get { return _LogID; }
            set { _LogID = value; }
        }

        //Ա�����Ż���ָ�Ʊ��
        private string _AttendanceUserNo;
        public string AttendanceUserNo
        {
            get { return _AttendanceUserNo; }
            set { _AttendanceUserNo = value; }
        }

        //���ڼ�¼
        private DateTime _AttendanceTime;
        public DateTime AttendanceTime
        {
            get { return _AttendanceTime; }
            set { _AttendanceTime = value; }
        }

        //��������
        private string _AttendanceType;
        public string AttendanceType
        {
            get { return _AttendanceType; }
            set { _AttendanceType = value; }
        }

        //�����豸��
        private string _Sensor;
        public string Sensor
        {
            get { return _Sensor; }
            set { _Sensor = value; }
        }

        //������ԱID
        private long _CreatedBy;
        public long CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        //��������
        private DateTime _CreatedDate;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        //�޸�����
        private DateTime? _ModiDate;
        public DateTime? ModiDate
        {
            get;
            set;
        }
        //�޸���
        private string _Modi;
        public string Modi
        {
            get;
            set;
        }
        //�Ƿ�©
        private string _IsComp;
        public string IsComp
        {
            get;
            set;
        }
        //��©����
        private string _Comp;
        public string Comp
        {
            get;
            set;
        }
        //������˾orgID
        private int _orgID;
        public int orgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        //�ֹ�¼��
        private bool _IsManual;
        public bool IsManual
        {
            get { return _IsManual; }
            set { _IsManual = value; }
        }

        //�ɱ�����
        private string _Center;
        public string Center
        {
            get { return _Center; }
            set { _Center = value; }
        }

        //�ɱ���������
        private string _CenterName;
        public string CenterName
        {
            get { return _CenterName; }
            set { _CenterName = value; }
        }

        //��������/Ա������
        private string _UserType;
        public string UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }

        //��������ID
        private int _UserTypeID;
        public int UserTypeID
        {
            get { return _UserTypeID; }
            set { _UserTypeID = value; }
        }
        
        //����
        private string _AttendanceUserName;
        public string AttendanceUserName
        {
            get;
            set;
        }
        //���ں�
        private string _AttendenceNo;
        public string AttendenceNo
        {
            get;
            set;
        }
        private string _C_ID;
        public string C_ID
        {
            get;
            set;
        }
        private string _HrType;
        public string HrType
        {
            get { return _HrType; }
            set { _HrType = value; }
        }
        private string _U_ID;
        public string U_ID
        {
            get;
            set;
        }
        public IList<HR_AttendanceInfo> SelectAttenInfo(int year,int month,/*int day,*/string userCenter,int userType,int plantID,string userCode,string checkType,int isManual,int isCompany)
        {
            string strSql = "sp_hr_SelectAttenInfo";
            SqlParameter[] sqlParam = new SqlParameter[9];
            sqlParam[0] = new SqlParameter("@Year", year);
            sqlParam[1] = new SqlParameter("@Month", month);
            sqlParam[2] = new SqlParameter("@userCenter", userCenter);
            sqlParam[3] = new SqlParameter("@userType", userType);
            sqlParam[4] = new SqlParameter("@plantID", plantID);
            sqlParam[5] = new SqlParameter("@userCode", userCode);
            sqlParam[6] = new SqlParameter("@checkType", checkType);
            sqlParam[7] = new SqlParameter("@isManual", isManual);
            sqlParam[8] = new SqlParameter("@isCompany", isCompany);
            //sqlParam[9] = new SqlParameter("@Day", day);

            IList<HR_AttendanceInfo> hr_attendance = new List<HR_AttendanceInfo>();
            IDataReader reader = SqlHelper.ExecuteReader(ConfigurationManager.AppSettings["SqlConn.Conn"], CommandType.StoredProcedure, strSql, sqlParam);
            while (reader.Read())
            {
                HR_AttendanceInfo atteninfo = new HR_AttendanceInfo();
                atteninfo.AttendanceID = Convert.ToInt64(reader["AttendanceID"]);
                atteninfo.C_ID = Convert.ToString(reader["AttendanceUserID"]);
                atteninfo.U_ID = Convert.ToString(reader["AttendanceUserCenterName"]);
                atteninfo.AttendanceUserName = Convert.ToString(reader["AttendanceUserName"]);//Ա������
                atteninfo.AttendanceUserNo = Convert.ToString(reader["AttendanceUserCode"]);//Ա����
                atteninfo.UserType=Convert.ToString(reader["AttendanceUserType"]);//Ա������
                atteninfo.Center = Convert.ToString(reader["AttendanceUserCenter"]);//�ɱ�����
                atteninfo.AttendanceTime = Convert.ToDateTime(reader["checkTime"]);//��������
                atteninfo.AttendanceType = Convert.ToString(reader["checkType"]);//��������

                if (Convert.ToInt32(reader["isManual"]) == 0)
                {
                    atteninfo.IsComp ="��";
                }
                else
                    atteninfo.IsComp = "��";
                if (atteninfo.IsComp == "��")
                { 
                    if (Convert.ToInt32(reader["isCompany"]) == 1 )//��©����
                    {
                        atteninfo.Comp = "����";
                    }
                    else 
                    {
                        atteninfo.Comp = "˽��";
                    }
                
                }
                atteninfo.AttendenceNo = Convert.ToString(reader["AttendanceUserNo"]);//���ں�
                atteninfo.Modi = Convert.ToString(reader["modifiedby"]);//�޸���
                if (reader["modifiedDate"]==DBNull.Value)
                {
                    atteninfo.ModiDate = null;
                }
                else
                    atteninfo.ModiDate=Convert.ToDateTime(reader["modifiedDate"]);    //�޸�����
                atteninfo.Sensor = Convert.ToString(reader["Sensorid"]);//���ڻ���
                hr_attendance.Add(atteninfo);
            }
            reader.Close();
            return hr_attendance;
        }

        //
        public IList<HR_AttendanceInfo> SelectAttenInfos(int year, int month,/*int day,*/string userCenter, int userType, int plantID, string userCode, string checkType, int isManual, int isCompany)
        {
            string strSql = "sp_hr_SelectAttenInfo";
            SqlParameter[] sqlParam = new SqlParameter[9];
            sqlParam[0] = new SqlParameter("@Year", year);
            sqlParam[1] = new SqlParameter("@Month", month);
            sqlParam[2] = new SqlParameter("@userCenter", userCenter);
            sqlParam[3] = new SqlParameter("@userType", userType);
            sqlParam[4] = new SqlParameter("@plantID", plantID);
            sqlParam[5] = new SqlParameter("@userCode", userCode);
            sqlParam[6] = new SqlParameter("@checkType", checkType);
            sqlParam[7] = new SqlParameter("@isManual", isManual);
            sqlParam[8] = new SqlParameter("@isCompany", isCompany);
            //sqlParam[9] = new SqlParameter("@Day", day);

            IList<HR_AttendanceInfo> hr_attendance = new List<HR_AttendanceInfo>();
            IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
            while (reader.Read())
            {
                HR_AttendanceInfo atteninfo = new HR_AttendanceInfo();
                atteninfo.AttendanceID = Convert.ToInt64(reader["AttendanceID"]);
                atteninfo.C_ID = Convert.ToString(reader["AttendanceUserID"]);
                atteninfo.U_ID = Convert.ToString(reader["AttendanceUserCenterName"]);
                atteninfo.AttendanceUserName = Convert.ToString(reader["AttendanceUserName"]);//Ա������
                atteninfo.AttendanceUserNo = Convert.ToString(reader["AttendanceUserCode"]);//Ա����
                atteninfo.UserType = Convert.ToString(reader["AttendanceUserType"]);//Ա������
                atteninfo.Center = Convert.ToString(reader["AttendanceUserCenter"]);//�ɱ�����
                atteninfo.AttendanceTime = Convert.ToDateTime(reader["checkTime"]);//��������
                atteninfo.AttendanceType = Convert.ToString(reader["checkType"]);//��������
                atteninfo.HrType = Convert.ToString(reader["hrtype"]);//����ȡ����ʱ�������Ǳ�

                if (Convert.ToInt32(reader["isManual"]) == 0)
                {
                    atteninfo.IsComp = "��";
                }
                else
                    atteninfo.IsComp = "��";
                if (atteninfo.IsComp == "��")
                {
                    if (Convert.ToInt32(reader["isCompany"]) == 1)//��©����
                    {
                        atteninfo.Comp = "����";
                    }
                    else
                    {
                        atteninfo.Comp = "˽��";
                    }

                }
                atteninfo.AttendenceNo = Convert.ToString(reader["AttendanceUserNo"]);//���ں�
                atteninfo.Modi = Convert.ToString(reader["modifiedby"]);//�޸���
                if (reader["modifiedDate"] == DBNull.Value)
                {
                    atteninfo.ModiDate = null;
                }
                else
                    atteninfo.ModiDate = Convert.ToDateTime(reader["modifiedDate"]);    //�޸�����
                atteninfo.Sensor = Convert.ToString(reader["Sensorid"]);//���ڻ���
                hr_attendance.Add(atteninfo);
            }
            reader.Close();
            return hr_attendance;
        }
    }
}