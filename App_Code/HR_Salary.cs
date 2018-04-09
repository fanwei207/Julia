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
    /// Summary description for HR_Salary
    /// </summary>
    public class HR_Salary
    {
        public HR_Salary()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// �������ֶ�
        /// </summary>
        private int s_userID;           //Ա��ID
        private string s_userNo;        //Ա������
        private string s_userName;      //Ա������
        private string s_salaryDate;    //��������
        private decimal s_basic;        //��������
        private decimal s_over;         //�Ӱ๤��
        private decimal s_nightMoney;   //��ҹ����
        private decimal s_holiday;      //���ٹ���
        private decimal s_oallowance;   //��������
        private decimal s_subsidies;    //����
        private decimal s_duereward;    //Ӧ�����
        private decimal s_hfound;       //����
        private decimal s_rfound;       //����
        private decimal s_memship;      //����
        private decimal s_duct;         //�ۿ�
        private decimal s_tax;          //����˰
        private decimal s_workpay;      //ʵ�����


        /// <summary>
        /// �������������Ϣ
        /// </summary>
        private bool s_leave;           //�Ƿ�ȫ�� ��yes --  ȫ��  /  No -- ����٣�
        private decimal s_temday;       //������Ա������Ӧ����
        private bool s_fire;            //�Ƿ���ְ ��yes --  ��ְ  /  No -- ��ְ��
        private decimal s_lastduct;     //������ۿ�
        private decimal s_currduct;     //������ۿ�     ����ʵ�� s_currduct + s_duct - s_lastduct
        private decimal s_restleave;    //������������  
        private decimal s_shday;        //���¹����ϰ�����
        private decimal s_shsalary;     //���¹��ٹ���
        private decimal s_childwance;    //������Ů����
        private int s_department;        //���� �ɣ�
        private int s_employTypeID;      //�ù����ʣɣ�
        private int s_insuranceTypeID;����//�籣���ͣɣ�
        private bool s_temp;             //�Ƿ���������Ա����yes --��  / No��--���� ��
        private int s_workyear;           //����


        private int s_workshopID;       //����
        private int s_workgroupID;      //����
        private int s_workkindID;       //����

        private decimal  s_sickLeave;   //������
        private decimal s_sickLeavePay;  //���ٹ���

        private decimal s_decRateDeduct;  //����

        private decimal s_workMoney;   //���¹���
        private decimal s_maternityDays; //����
        private decimal s_maternityPay; //���ٹ���

        private bool s_time; // �Ƽ�ת��ʱ

        private decimal s_realMoney; //ʵ�ʹ��ѣ��ѽ���ӹ�����

        private int s_userType; //Ա������

        private decimal s_leaveday;  //�¼�
        private decimal s_minerDays; //����
        private decimal s_annualLeaveDays; //���ݼ�
        private decimal s_otherday; //��������


        private decimal s_allattendence;  //ȫ�ڽ�
        private decimal s_wYbonus; //���䲹��


        private decimal s_attdays;  //������
        private decimal s_attHours; //����Сʱ��

        private decimal s_accrFound; //���ϱ���
        private decimal s_acceFound; //ʧҵ����
        private decimal s_accmFound; //ҽ�Ʊ���

        private decimal s_materialDeduct; //���Ͽۿ�
        private decimal s_assessDeduct; //�����Ŀۿ�
        private decimal s_vacationDeduct; //���ڿۿ�
        private decimal s_otherDeduct; //�����ۿ�
        private decimal s_ship; // ����
        private decimal s_locFin; //�������
        private decimal s_quanlity;
        private decimal s_break;

        private int s_mid;  //�а�
        private int s_night; //ҹ��
        private int s_whole; //ȫҹ


        private decimal s_taxRate; //˰��
        private decimal s_taxDeduct; //˰����۳���

        private bool s_exChange; // �Ƽ���ʱת��   0- No  1 - Yes

        private decimal s_hightTemp; // ���·�
        private decimal s_humanAllowance;  //�й�����
        private decimal s_normal;    //����
        private decimal s_other;    //��������
        private decimal s_newuser;  //�¹��˲���
        private decimal s_student;  //ѧ������
        private decimal s_bonus;    //����
        private decimal s_allMobile;    //�ֻ���
        private decimal s_allKilometer;    //������
        private decimal s_allBox;    //װ����
        private decimal s_allBusiness;    //������
        private decimal s_allOnDuty;    //ֵ����
        private decimal s_allPost;    //��λ��


        private decimal s_coef;      //ϵ��
        private decimal s_rate;      //ƽ��������
        private decimal s_benefit;    //��Ч����

        private decimal s_original;  //  ԭʼ��ʱ


        //ʵ��������
        public int s_UserID
        {
            get { return s_userID; }
            set { s_userID = value; }
        }

        public string s_UserNo
        {
            get { return s_userNo; }
            set { s_userNo = value; }
        }

        public string s_UserName
        {
            get { return s_userName; }
            set { s_userName = value; }
        }

        public string s_SalaryDate
        {
            get { return s_salaryDate; }
            set { s_salaryDate = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public decimal  s_Basic
        {
            get { return s_basic; }
            set { s_basic = value; }
        }
        /// <summary>
        /// �Ӱ๤��
        /// </summary>
        public decimal  s_Over
        {
            get { return s_over; }
            set { s_over = value; }
        }
        /// <summary>
        /// ҹ�ಹ������ҹ�ࡢҹ�ࡢȫҹ������ * ���Բ�������
        /// </summary>
        public decimal s_NightMoney
        {
            get { return s_nightMoney; }
            set { s_nightMoney = value; }
        }
        /// <summary>
        /// s_Holiday = s_Shsalary����s_Shsalary�ǹ��ٹ��ʡ������л㱨��ʱ��ż��㹤��
        /// </summary>
        public decimal s_Holiday
        {
            get { return s_holiday; }
            set { s_holiday = value; }
        }
        /// <summary>
        /// ���ֽ���֮��
        /// </summary>
        public decimal s_Oallowance
        {
            get { return s_oallowance; }
            set { s_oallowance = value; }
        }
        /// <summary>
        /// Ӧ�����Ľ���Ӧ�����ʿ۳��籣��֮��С����ͻ�������ʱ����Ҫ������ң����ض���Ⱥ��Ч��δ��ְ������١�����Ա���ȣ���
        /// </summary>
        public decimal s_Subsidies
        {
            get { return s_subsidies; }
            set { s_subsidies = value; }
        }
        /// <summary>
        /// Ӧ������
        /// </summary>
        public decimal s_Duereward
        {
            get { return s_duereward; }
            set { s_duereward = value; }
        }
        /// <summary>
        /// ס��������
        /// </summary>
        public decimal s_Hfound
        {
            get { return s_hfound; }
            set { s_hfound = value; }
        }
        /// <summary>
        /// ���ϱ��� + ʧҵ���� + ҽ�Ʊ���֮��
        /// </summary>
        public decimal s_Rfound
        {
            get { return s_rfound; }
            set { s_rfound = value; }
        }

        /// <summary>
        /// Ӧ�ɹ�����á�����ֵ = �������� * �������
        /// </summary>
        public decimal s_Memship
        {
            get { return s_memship; }
            set { s_memship = value; }
        }
        /// <summary>
        /// �ۿ����ۿ���ʴ��ڹ��ʵ�20%���򰴹��ʵ�20%�ۿ����ʵ�ʿۿ��۷�������ǲ�����������ۿ��ۼƵ��¸���ִ��
        /// </summary>
        public decimal s_Duct
        {
            get { return s_duct; }
            set { s_duct = value; }
        }
        /// <summary>
        /// ��������˰��Ӧ�����ʿ۳��ۿ�籣�ȣ����⣬������Ů������Ҫ��˰��
        /// </summary>
        public decimal s_Tax
        {
            get { return s_tax; }
            set { s_tax = value; }
        }
        /// <summary>
        /// ʵ������
        /// </summary>
        public decimal s_Workpay
        {
            get { return s_workpay; }
            set { s_workpay = value; }
        }

        /// <summary>
        /// ��١�ɥ�١�����ȫ��Ϊ0ʱΪ��
        /// </summary>
        public bool s_Leave
        {
            get { return s_leave; }
            set { s_leave = value; }
        }

        public decimal s_Temday
        {
            get { return s_temday; }
            set { s_temday = value; }
        }

        /// <summary>
        /// �Ƿ���ְ��������ڽ�������ְ�ģ��򱾴ι��ʽ����ΰ��շ���ְ����
        /// </summary>
        public bool s_Fire
        {
            get { return s_fire; }
            set { s_fire = value; }
        }
        /// <summary>
        /// ����ʣ����ۿۿ����¿ۿ� = ���¿ۿ���� + ���·��������Ӧ�ü�ȥ���˽���
        /// </summary>
        public decimal s_Lastduct
        {
            get { return s_lastduct; }
            set { s_lastduct = value; }
        }
        /// <summary>
        /// ���ν���ʱ��Ӧ�ۿ����ֵ��=����δ�۽�� + ���·������
        /// </summary>
        public decimal s_Currduct
        {
            get { return s_currduct; }
            set { s_currduct = value; }
        }

        /// <summary>
        /// ������������ = Convert.ToDecimal(sdtr["number"])
        /// </summary>
        public decimal s_Restleave
        {
            get { return s_restleave; }
            set { s_restleave = value; }
        }

        /// <summary>
        /// ���ٹ��졣�����ڼ�Ĺ���Ҫ�������
        /// Math.Round(Convert.ToDecimal(sdtr["holidaycost"]) / 8, 2)
        /// </summary>
        public decimal s_Shday
        {
            get { return s_shday; }
            set { s_shday = value; }
        }
        /// <summary>
        /// �����չ��� = ���ٹ��� * �������� * �ڼ��չ�����
        /// </summary>
        public decimal s_Shsalary
        {
            get { return s_shsalary; }
            set { s_shsalary = value; }
        }
        /// <summary>
        /// ������Ů����
        /// </summary>
        public decimal s_Childwance
        {
            get { return s_childwance; }
            set { s_childwance = value; }
        }

        public int s_Department
        {
            get { return s_department; }
            set { s_department = value; }
        }

        public int s_EmployTypeID
        {
            get { return s_employTypeID; }
            set { s_employTypeID = value; }
        }
        public int s_InsuranceTypeID
        {
            get { return s_insuranceTypeID; }
            set { s_insuranceTypeID = value; }
        }

        public bool s_Temp
        {
            get { return s_temp; }
            set { s_temp = value; }
        }
        /// <summary>
        /// ���䡣������ڽ����»�֮ǰ��ְ�ģ�����һ��
        /// </summary>
        public int s_Workyear
        {
            get { return s_workyear; }
            set { s_workyear = value; }
        }

        public int s_WorkshopID
        {
            get { return s_workshopID; }
            set { s_workshopID = value; }
        }

        public int s_WorkgroupID
        {
            get { return s_workgroupID; }
            set { s_workgroupID = value; }
        }

        public int s_WorkkindID
        {
            get { return s_workkindID; }
            set { s_workkindID = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public decimal  s_SickLeave
        {
            get { return s_sickLeave; }
            set { s_sickLeave = value; }
        }
        /// <summary>
        /// �����ڼ�Ӧ������
        /// </summary>
        public decimal  s_SickLeavePay
        {
            get { return s_sickLeavePay; }
            set { s_sickLeavePay = value; }
        }

        public decimal s_DecRateDeduct
        {
            get { return s_decRateDeduct; }
            set { s_decRateDeduct = value; }
        }

        /// <summary>
        /// ���¾����졣cost������������ڼ�Ĺ��죬���Ե��ȼ�����Math.Round(Convert.ToDecimal(sdtr["cost"]) / 8, 2) - hrs.s_Shday�����ٹ�ʱ����
        /// �����������С�ڹ��ٹ��죬��Ϊ0
        /// </summary>
        public decimal s_WorkMoney
        {
            get { return s_workMoney; }
            set { s_workMoney = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal s_MaternityDays
        {
            get { return s_maternityDays; }
            set { s_maternityDays = value; }
        }
        /// <summary>
        /// ���ٹ��� = �������� * �������� / ��������
        /// </summary>
        public decimal s_MaternityPay
        {
            get { return s_maternityPay; }
            set { s_maternityPay = value; }
        }

        public bool s_Time
        {
            get { return s_time; }
            set { s_time = value; }
        }

        public decimal s_RealMoney
        {
            get { return s_realMoney; }
            set { s_realMoney = value; }
        }

        public int s_UserType
        {
            get { return s_userType; }
            set { s_userType = value; }
        }
        /// <summary>
        /// �¼�
        /// </summary>
        public decimal s_Leaveday
        {
            get { return s_leaveday; }
            set { s_leaveday = value; }
        }

        public decimal s_MinerDays
        {
            get { return s_minerDays; }
            set { s_minerDays = value; }
        }
        /// <summary>
        /// ȫ�ڲ����������tcpc0.dbo.a_Allattendence�У�ֱ��ά���ľ��ǽ��
        /// </summary>
        public decimal s_Allattendence
        {
            get { return s_allattendence; }
            set { s_allattendence = value; }
        }
        /// <summary>
        /// ���䲹���������tcpc0.dbo.a_WorkYear�У�ֱ��ά���ľ��ǽ��
        /// </summary>
        public decimal s_WYbonus
        {
            get { return s_wYbonus; }
            set { s_wYbonus = value; }
        }

        public decimal s_AnnualLeaveDays
        {
            get { return s_annualLeaveDays; }
            set { s_annualLeaveDays = value; }
        }

        public decimal s_Attdays
        {
            get { return s_attdays; }
            set { s_attdays = value; }
        }
        /// <summary>
        /// ����Сʱ��
        /// </summary>
        public decimal s_AttHours
        {
            get { return s_attHours; }
            set { s_attHours = value; }
        }
        /// <summary>
        /// ���ϱ���
        /// </summary>
        public decimal s_AccrFound
        {
            get { return s_accrFound; }
            set { s_accrFound = value; }
        }

        /// <summary>
        /// ʧҵ����
        /// </summary>
        public decimal s_AcceFound
        {
            get { return s_acceFound; }
            set { s_acceFound = value; }
        }

        /// <summary>
        /// ҽ�Ʊ���
        /// </summary>
        public decimal s_AccmFound
        {
            get { return s_accmFound; }
            set { s_accmFound = value; }
        }

        public decimal s_MaterialDeduct
        {
            get { return s_materialDeduct; }
            set { s_materialDeduct = value; }
        }

        public decimal s_AssessDeduct
        {
            get { return s_assessDeduct; }
            set { s_assessDeduct = value; }
        }

        public decimal s_VacationDeduct
        {
            get { return s_vacationDeduct; }
            set { s_vacationDeduct = value; }
        }

        public decimal s_OtherDeduct
        {
            get { return s_otherDeduct; }
            set { s_otherDeduct = value; }
        }
        /// <summary>
        /// ��ҹ������
        /// </summary>
        public int s_Mid
        {
            get { return s_mid; }
            set { s_mid = value; }
        }
        /// <summary>
        /// ҹ������
        /// </summary>
        public int s_Night
        {
            get { return s_night; }
            set { s_night = value; }
        }
        /// <summary>
        /// ȫҹ������
        /// </summary>
        public int s_Whole
        {
            get { return s_whole; }
            set { s_whole = value; }
        }
        /// <summary>
        /// û������Ļ�+ɥ+���˼�
        /// </summary>
        public decimal s_Otherday
        {
            get { return s_otherday; }
            set { s_otherday = value; }
        }

        public decimal s_TaxRate
        {
            get { return s_taxRate; }
            set { s_taxRate = value; }
        }

        public decimal s_TaxDeduct
        {
            get { return s_taxDeduct; }
            set { s_taxDeduct = value; }
        }

        public bool s_ExChange
        {
            get { return s_exChange; }
            set { s_exChange = value; }
        }


        //public decimal s_TaxDeduct
        //{
        //    get { return s_taxDeduct; }
        //    set { s_taxDeduct = value; }
        //}

        public decimal s_Ship
        {
            get { return s_ship; }
            set { s_ship = value; }
        }

        public decimal s_LocFin
        {
            get { return s_locFin; }
            set { s_locFin = value; }
        }
        /// <summary>
        /// ���·�
        /// </summary>
        public decimal s_HightTemp
        {
            get { return s_hightTemp; }
            set { s_hightTemp = value; }
        }
        /// <summary>
        /// �й�����
        /// </summary>
        public decimal s_HumanAllowance
        {
            get { return s_humanAllowance; }
            set { s_humanAllowance = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public decimal s_NOrmal
        {
            get { return s_normal; }
            set { s_normal = value; }
        }
        /// <summary>
        /// ������Ů
        /// </summary>
        public decimal s_Other
        {
            get { return s_other; }
            set { s_other = value; }
        }

        public decimal s_Quanlity
        {
            get { return s_quanlity; }
            set { s_quanlity = value; }
        }

        public decimal s_Break
        {
            get { return s_break; }
            set { s_break = value; }
        }
        /// <summary>
        /// ��Ա������
        /// </summary>
        public decimal s_Newuser
        {
            get { return s_newuser; }
            set { s_newuser = value; }
        }
        /// <summary>
        /// ѧ������
        /// </summary>
        public decimal s_Student
        {
            get { return s_student; }
            set { s_student = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal s_Bonus
        {
            get { return s_bonus; }
            set { s_bonus = value; }
        }



        public decimal s_Coef
        {
            get { return s_coef; }
            set { s_coef = value; }
        }

        public decimal s_Rate
        {
            get { return s_rate; }
            set { s_rate = value; }
        }
        /// <summary>
        /// A��B�಻ʹ���������
        /// </summary>
        public decimal s_Benefit
        {
            get { return s_benefit; }
            set { s_benefit = value; }
        }

        public decimal s_Original
        {
            get { return s_original; }
            set { s_original = value; }
        }

        private decimal bt_tiaoxiu;
        /// <summary>
        /// ���ݲ���
        /// </summary>
        public decimal bt_TiaoXiu
        {
            get { return bt_tiaoxiu; }
            set { bt_tiaoxiu = value; }
        }

        private decimal bt_chaoe;
        /// <summary>
        /// ���������
        /// </summary>
        public decimal bt_ChoaE
        {
            get { return bt_chaoe; }
            set { bt_chaoe = value; }
        }

        private decimal bt_newCost;
        /// <summary>
        /// ��Ա����ʱ����
        /// </summary>
        public decimal bt_NewCost
        {
            get { return bt_newCost; }
            set { bt_newCost = value; }
        }

        private decimal _s_MutualFunds;
        /// <summary>
        /// ���������Ѱ������ֶ�deduct�У���������ϸ��
        /// </summary>
        public decimal s_MutualFunds
        {
            get { return _s_MutualFunds; }
            set { _s_MutualFunds = value; }
        }

        private decimal _f_Electricity;
        /// <summary>
        /// ��ѣ��Ѱ������ֶ�deduct�У���������ϸ��
        /// </summary>
        public decimal f_Electricity
        {
            get { return _f_Electricity; }
            set { _f_Electricity = value; }
        }

        private decimal _f_Water;
        /// <summary>
        /// ˮ�ѣ��Ѱ������ֶ�deduct�У���������ϸ��
        /// </summary>
        public decimal f_Water
        {
            get { return _f_Water; }
            set { _f_Water = value; }
        }

        private decimal _f_Dormitory;
        /// <summary>
        /// ס�޷ѣ��Ѱ������ֶ�deduct�У���������ϸ��
        /// </summary>
        public decimal f_Dormitory
        {
            get { return _f_Dormitory; }
            set { _f_Dormitory = value; }
        }

        /// <summary>
        /// �ֻ���
        /// </summary>
        public decimal s_AllMobile
        {
            get { return s_allMobile; }
            set { s_allMobile = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public decimal s_AllKilometer
        {
            get { return s_allKilometer; }
            set { s_allKilometer = value; }
        }
        /// <summary>
        /// װ����
        /// </summary>
        public decimal s_AllBox
        {
            get { return s_allBox; }
            set { s_allBox = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public decimal s_AllBusiness
        {
            get { return s_allBusiness; }
            set { s_allBusiness = value; }
        }
        /// <summary>
        /// ֵ����
        /// </summary>
        public decimal s_AllOnDuty
        {
            get { return s_allOnDuty; }
            set { s_allOnDuty = value; }
        }
        /// <summary>
        /// ��λ��
        /// </summary>
        public decimal s_AllPost
        {
            get { return s_allPost; }
            set { s_allPost = value; }
        }

        private bool s_isProbationer;
        /// <summary>
        /// �Ƿ���������Ա��
        /// </summary>
        public bool s_IsProbationer
        {
            get { return s_isProbationer; }
            set { s_isProbationer = value; } 
        }

        private decimal s_probWorkDays;
        /// <summary>
        /// �����ڣ������½��ģ�Ա��Ӧ��������
        /// </summary>
        public decimal s_ProbWorkDays
        {
            get { return s_probWorkDays; }
            set { s_probWorkDays = value; }
        }
    }

}