using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace Wage
{
    /// <summary>
    /// Summary description for hr_TimeSalary
    /// </summary>
    public class hr_TimeSalary
    {
        public hr_TimeSalary()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        private int t_userID;           //����ID
        private string t_userNo;        //����
        private string t_userName;      //����
        private string t_salaryDate;    //��������
        private decimal t_basic;        //��������
        private decimal t_benefit;      //Ч�湤��
        private decimal t_nightWork;    //��ҹ���
        private decimal t_allowance;    //����
        private decimal t_subsidies;    //����
        private decimal t_assess;       //���˽����Ӱ�ѣ�
        private decimal t_holiday;      //���ٹ���
        private decimal t_duereward;    //Ӧ�����
        private decimal t_hfound;       //������
        private decimal t_rfound;       // ����+ҽ��+��������
        private decimal t_member;       //�����
        private decimal t_deduct;       //�ۿ�
        private decimal t_tax;          //˰
        private decimal t_workpay;      //ʵ��
      
        private bool t_fire;             //�Ƿ���ְ
        private decimal t_currdeduct;   //����Ӧ�ۿ�
        private decimal t_lastdeduct;   //����ʣ��ۿ�
        private decimal t_annual;       //��������
        private decimal t_hday;         //��������
        private decimal t_sleave;       //��������
        private decimal t_sleavepay;    //���ٹ���
        private int t_department;       //����ID
        private int t_employTypeID;     //�ù�����ID 
        private int t_insuranceTypeID;  //��������ID
        private int t_worktype;         //�Ƴ귽ʽID
        private decimal t_attendance;   //����Сʱ
        private decimal t_attday;       //������
        private int t_mid;              //�а�
        private int t_night;            //ҹ��
        private int t_whole;            //ȫҹ
        private decimal t_fixedsalary;  // �̶�����
        private decimal t_leave;        //�¼�
        private decimal t_other;        //�������
        private decimal t_Rate;          //��������
        private int t_workyear;     //����

        private decimal t_maternityDays;   //����
        private decimal t_maternityPay;   //���ٹ���

        private decimal t_workOrder;  //�Ƽ�����

        private int t_workShopID; //����

        private int t_userType; //Ա������

        private decimal t_fullattendence;   //ȫ��
        private decimal t_lengService;   //���䲹��

        private decimal t_accrFound; //���ϱ���
        private decimal t_acceFound; //ʧҵ����
        private decimal t_accmFound; //ҽ�Ʊ���

        private decimal t_taxRate; //˰��
        private decimal t_taxDeduct; //˰����۳���

        private decimal t_materialDeduct; //���Ͽۿ�
        private decimal t_assessDeduct; //���˿ۿ�
        private decimal t_vacationDeduct; //���ڿۿ�
        private decimal t_otherDeduct; //�����ۿ�
        private decimal t_ship; // ����
        private decimal t_locFin; //�������

        private decimal t_hightTemp; // ���·�
        private decimal t_humanAllowance;  //�й�����
        private decimal t_normal;    //��������
        private decimal t_allother;    //������Ů
        private decimal t_newuser;    //�¹��˲���
        private decimal t_student; //ѧ������
        private decimal t_bonus;   //���� 
        private decimal t_allMobile;    //�ֻ���
        private decimal t_allKilometer;    //������
        private decimal t_allBox;    //װ����
        private decimal t_allBusiness;    //������
        private decimal t_allOnDuty;    //ֵ����
        private decimal t_allPost;    //��λ��

        private decimal t_quanlity;
        private decimal t_break;



        //Add By Shanzm 2013.01.23
        private decimal t_restTimeMoney;   //���ݽ�� 


        //ʵ��������
        public int T_userID
        {
            get { return t_userID; }
            set { t_userID = value; }
        }

        public string T_userNo
        {
            get { return t_userNo; }
            set { t_userNo = value; }
        }

        public string T_userName
        {
            get { return t_userName; }
            set { t_userName = value; }
        }

        public string T_salaryDate
        {
            get { return t_salaryDate; }
            set { t_salaryDate = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal T_basic
        {
            get { return t_basic; }
            set { t_basic = value; }
        }
        /// <summary>
        ///  Ч�湤��
        /// </summary>
        public decimal T_benefit
        {
            get { return t_benefit; }
            set { t_benefit = value; }
        }
        /// <summary>
        /// ��ҹ����
        /// </summary>
        public decimal T_nightWork
        {
            get { return t_nightWork; }
            set { t_nightWork = value; }
        }
        /// <summary>
        /// ���ֽ���֮��
        /// </summary>
        public decimal  T_allowance
        {
            get { return t_allowance; }
            set { t_allowance = value; }
        }
        /// <summary>
        /// Ӧ�����Ľ���Ӧ�����ʿ۳��籣��֮��С����ͻ�������ʱ����Ҫ������ң����ض���Ⱥ��Ч��δ��ְ������١�����Ա���ȣ���
        /// </summary>
        public decimal T_subsidies 
        {
            get { return t_subsidies; }
            set { t_subsidies = value; }
        }
        /// <summary>
        /// ���˽����Ӱ�ѣ�
        /// </summary>
        public decimal T_assess
        {
            get { return t_assess; }
            set { t_assess = value; }
        }
        /// <summary>
        /// ���ٹ���
        /// </summary>
        public decimal T_holiday
        {
            get { return t_holiday; }
            set { t_holiday = value; }
        }
        /// <summary>
        /// Ӧ������
        /// </summary>
        public decimal T_duereward
        {
            get { return t_duereward; }
            set { t_duereward = value; }
        }

        public decimal T_hfound
        {
            get { return t_hfound; }
            set { t_hfound = value; }
        }

        public decimal T_rfound
        {
            get { return t_rfound; }
            set { t_rfound = value; }
        }

        public decimal T_member
        {
            get { return t_member; }
            set { t_member = value; }
        }

        public decimal T_deduct
        {
            get { return t_deduct; }
            set { t_deduct = value; }
        }

        public decimal T_tax
        {
            get { return t_tax; }
            set { t_tax = value; }
        }
        /// <summary>
        /// ʵ������
        /// </summary>
        public decimal T_workpay
        {
            get { return t_workpay; }
            set { t_workpay = value; }
        }
        //End ������

        public bool T_fire
        {
            get { return t_fire; }
            set { t_fire = value; }
        }

        public decimal T_currdeduct
        {
            get { return t_currdeduct; }
            set { t_currdeduct = value; }
        }

        public decimal T_lastdeduct
        {
            get { return t_lastdeduct; }
            set { t_lastdeduct = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public decimal T_annual
        {
            get { return t_annual; }
            set { t_annual = value; }
        }

        public decimal T_hday
        {
            get { return t_hday; }
            set { t_hday = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public decimal T_sleave
        {
            get { return t_sleave; }
            set { t_sleave = value; }
        }
        /// <summary>
        /// ���ٹ��� = �������� * ����������  / �������ڣ������ٹ��ʵ��ۣ� * ���ٹ����ʣ����粡��ʱ���Ż������ʵ�80%�ȣ�
        /// </summary>
        public decimal T_sleavepay
        {
            get { return t_sleavepay; }
            set { t_sleavepay = value; }
        }

        public int T_department
        {
            get { return t_department; }
            set { t_department = value; }
        }

        public int T_employTypeID
        {
            get { return t_employTypeID; }
            set { t_employTypeID = value; }
        }

        public int T_insuranceTypeID
        {
            get { return t_insuranceTypeID; }
            set { t_insuranceTypeID = value; }
        }

        public int T_worktype
        {
            get { return t_worktype; }
            set { t_worktype = value; }
        }
        /// <summary>
        /// ����Сʱ = ����ʱ�� + ����ʱ�� - ����
        /// </summary>
        public decimal T_attendance
        {
            get { return t_attendance; }
            set { t_attendance = value; }
        }
        /// <summary>
        /// ������ = ������ + ������
        /// </summary>
        public decimal T_attday
        {
            get { return t_attday; }
            set { t_attday = value; }
        }

        public int T_mid
        {
            get { return t_mid; }
            set { t_mid = value; }
        }

        public int T_night
        {
            get { return t_night; }
            set { t_night = value; }
        }

        public int T_whole
        {
            get { return t_whole; }
            set { t_whole = value; }
        }
        /// <summary>
        /// �̶�����
        /// </summary>
        public decimal T_fixedsalary
        {
            get { return t_fixedsalary; }
            set { t_fixedsalary = value; }
        }
        /// <summary>
        /// �¼�
        /// </summary>
        public decimal T_leave
        {
            get { return t_leave; }
            set { t_leave = value; }
        }
        /// <summary>
        /// ������� = ����+ɥ+���ˣ� / ��������  * ��ƽ��������
        /// </summary>
        public decimal T_other
        {
            get { return t_other; }
            set { t_other = value; }
        }

        public decimal T_Rate
        {
            get { return t_Rate; }
            set { t_Rate = value; }
        }

        public int T_workyear
        {
            get { return t_workyear; }
            set { t_workyear = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public decimal T_maternityDays
        {
            get { return t_maternityDays; }
            set { t_maternityDays = value; }
        }
        /// <summary>
        /// ���ٹ��� = �������� * ���������� / �������ڣ�
        /// </summary>
        public decimal T_maternityPay
        {
            get { return t_maternityPay; }
            set { t_maternityPay = value; }
        }

        public decimal T_workOrder
        {
            get { return t_workOrder; }
            set { t_workOrder = value; }
        }

        public int T_workShopID
        {
            get { return t_workShopID; }
            set { t_workShopID = value; }
        }

        public int T_userType
        {
            get { return t_userType; }
            set { t_userType = value; }
        }
        /// <summary>
        /// ȫ�ڽ�
        /// </summary>
        public decimal T_fullattendence
        {
            get { return t_fullattendence; }
            set { t_fullattendence = value; }
        }
        /// <summary>
        /// ���佱
        /// </summary>
        public decimal T_lengService
        {
            get { return t_lengService; }
            set { t_lengService = value; }
        }

        public decimal T_accrFound
        {
            get { return t_accrFound; }
            set { t_accrFound = value; }
        }

        public decimal T_acceFound
        {
            get { return t_acceFound; }
            set { t_acceFound = value; }
        }

        public decimal T_accmFound
        {
            get { return t_accmFound; }
            set { t_accmFound = value; }
        }

        public decimal T_taxRate
        {
            get { return t_taxRate; }
            set { t_taxRate = value; }
        }

        public decimal T_taxDeduct
        {
            get { return t_taxDeduct; }
            set { t_taxDeduct = value; }
        }

        public decimal T_materialDeduct
        {
            get { return t_materialDeduct; }
            set { t_materialDeduct = value; }
        }


        public decimal T_assessDeduct
        {
            get { return t_assessDeduct; }
            set { t_assessDeduct = value; }
        }

        public decimal T_vacationDeduct
        {
            get { return t_vacationDeduct; }
            set { t_vacationDeduct = value; }
        }

        public decimal T_otherDeduct
        {
            get { return t_otherDeduct; }
            set { t_otherDeduct = value; }
        }

        /// <summary>
        /// ���·�
        /// </summary>
        public decimal t_HightTemp
        {
            get { return t_hightTemp; }
            set { t_hightTemp = value; }
        }
        /// <summary>
        /// �й�����
        /// </summary>
        public decimal t_HumanAllowance
        {
            get { return t_humanAllowance; }
            set { t_humanAllowance = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public decimal t_Normal
        {
            get { return t_normal; }
            set { t_normal = value; }
        }
        /// <summary>
        /// ������Ů
        /// </summary>
        public decimal t_Allother
        {
            get { return t_allother; }
            set { t_allother = value; }
        }

        public decimal t_Ship
        {
            get { return t_ship; }
            set { t_ship = value; }
        }

        public decimal t_LocFin
        {
            get { return t_locFin; }
            set { t_locFin = value; }
        }

        public decimal t_Quanlity
        {
            get { return t_quanlity; }
            set { t_quanlity = value; }
        }

        public decimal t_Break
        {
            get { return t_break; }
            set { t_break = value; }
        }
        /// <summary>
        /// ��Ա������
        /// </summary>
        public decimal T_newuser
        {
            get { return t_newuser; }
            set { t_newuser = value; }
        }
        /// <summary>
        /// ѧ������
        /// </summary>
        public decimal T_student
        {
            get { return t_student; }
            set { t_student = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal T_bonus
        {
            get { return t_bonus; }
            set { t_bonus = value; }
        }

        /// <summary>
        /// ���ݽ�ȡ��tcpcx.dbo.RestTime.Money��ֱ�Ӽ����Ӱ����
        /// </summary>
        public decimal T_restTimeMoney
        {
            get { return t_restTimeMoney; }
            set { t_restTimeMoney = value; }
        }

        private decimal _MutualFunds;
        /// <summary>
        /// ��������
        /// </summary>
        public decimal T_MutualFunds
        {
            get { return _MutualFunds; }
            set { _MutualFunds = value; }
        }

        private decimal _f_Electricity;
        /// <summary>
        /// ���
        /// </summary>
        public decimal F_Electricity
        {
            get { return _f_Electricity; }
            set { _f_Electricity = value; }
        }

        private decimal _f_Water;
        /// <summary>
        /// ˮ��
        /// </summary>
        public decimal F_Water
        {
            get { return _f_Water; }
            set { _f_Water = value; }
        }

        private decimal _f_Dormitory;
        /// <summary>
        /// ס�޷�
        /// </summary>
        public decimal F_Dormitory
        {
            get { return _f_Dormitory; }
            set { _f_Dormitory = value; }
        }

        /// <summary>
        /// �ֻ���
        /// </summary>
        public decimal t_AllMobile
        {
            get { return t_allMobile; }
            set { t_allMobile = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public decimal t_AllKilometer
        {
            get { return t_allKilometer; }
            set { t_allKilometer = value; }
        }
        /// <summary>
        /// װ����
        /// </summary>
        public decimal t_AllBox
        {
            get { return t_allBox; }
            set { t_allBox = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public decimal t_AllBusiness
        {
            get { return t_allBusiness; }
            set { t_allBusiness = value; }
        }
        /// <summary>
        /// ֵ����
        /// </summary>
        public decimal t_AllOnDuty
        {
            get { return t_allOnDuty; }
            set { t_allOnDuty = value; }
        }
        /// <summary>
        /// ��λ��
        /// </summary>
        public decimal t_AllPost
        {
            get { return t_allPost; }
            set { t_allPost = value; }
        }


    }
}
