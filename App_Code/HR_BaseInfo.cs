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
    /// ���ʻ�����Ϣ DB:hr_bi_mstr
    /// </summary>
    public class HR_BaseInfo
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public HR_BaseInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //ÿ�³�������
        private decimal _WorkDays;
        /// <summary>
        /// ÿ�³�������
        /// </summary>
        public decimal WorkDays
        {
            get { return _WorkDays; }
            set { _WorkDays = value; }
        }

        //ÿ�³����������ޣ�����ȫ�ڽ�ʱʹ�ã�
        private decimal _MinWorkDays;
        /// <summary>
        /// ÿ�³�����������
        /// </summary>
        public decimal MinWorkDays
        {
            get { return _MinWorkDays; }
            set { _MinWorkDays = value; }
        }

        //��ʱ�̶�������
        private decimal _FixedDays;
        /// <summary>
        /// ��ʱ�̶�������
        /// </summary>
        public decimal FixedDays
        {
            get { return _FixedDays; }
            set { _FixedDays = value; }
        }

        //�Ӱ๤����
        private decimal _OverTimeRate;
        /// <summary>
        /// �Ӱ๤����
        /// </summary>
        public decimal OverTimeRate
        {
            get { return _OverTimeRate; }
            set { _OverTimeRate = value; }
        }

        //�ڼ��չ�����
        private decimal _HolidayRate;
        /// <summary>
        /// �ڼ��չ�����
        /// </summary>
        public decimal HolidayRate
        {
            get { return _HolidayRate; }
            set { _HolidayRate = value; }
        }

        //������������
        private decimal _SaturdayRate;
        public decimal SaturdayRate
        {
            get { return _SaturdayRate; }
            set { _SaturdayRate = value; }
        }

        //��ҹ�࿪ʼʱ��
        private string _MidNightStartTime;
        /// <summary>
        /// ��ҹ�࿪ʼʱ��
        /// </summary>
        public string MidNightStartTime
        {
            get { return _MidNightStartTime; }
            set { _MidNightStartTime = value; }
        }

        //ҹ�࿪ʼʱ��
        private string _NightStartTime;
        /// <summary>
        /// ҹ�࿪ʼʱ��
        /// </summary>
        public string NightStartTime
        {
            get { return _NightStartTime; }
            set { _NightStartTime = value; }
        }

        //ȫҹ��ʼʱ��
        private string _WholeNightStartTime;
        /// <summary>
        /// ȫҹ��ʼʱ��
        /// </summary>
        public string WholeNightStartTime
        {
            get { return _WholeNightStartTime; }
            set { _WholeNightStartTime = value; }
        }

        //��ҹ�����
        private decimal _MidNightSubsidy;
        /// <summary>
        /// ��ҹ�����
        /// </summary>
        public decimal MidNightSubsidy
        {
            get { return _MidNightSubsidy; }
            set { _MidNightSubsidy = value; }
        }

        //ҹ�����
        private decimal _NightSubsidy;
        /// <summary>
        /// ҹ�����
        /// </summary>
        public decimal NightSubsidy
        {
            get { return _NightSubsidy; }
            set { _NightSubsidy = value; }
        }

        //ȫҹ����
        private decimal _WholeNightSubsidy;
        /// <summary>
        /// ȫҹ����
        /// </summary>
        public decimal WholeNightSubsidy
        {
            get { return _WholeNightSubsidy; }
            set { _WholeNightSubsidy = value; }
        }

        //�����ϰ�ʱ��
        private string _AMStartTime;
        /// <summary>
        /// �����ϰ�ʱ��
        /// </summary>
        public string AMStartTime
        {
            get { return _AMStartTime; }
            set { _AMStartTime = value; }
        }

        //�����°�ʱ��
        private string _PMEndTime;
        /// <summary>
        /// �����°�ʱ��
        /// </summary>
        public string PMEndTime
        {
            get { return _PMEndTime; }
            set { _PMEndTime = value; }
        }

        //����ʱ��
        private decimal  _LunchTime;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public decimal LunchTime
        {
            get { return _LunchTime; }
            set { _LunchTime = value; }
        }

        //���ʱ��
        private decimal _DinnerTime;
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public decimal DinnerTime
        {
            get { return _DinnerTime; }
            set { _DinnerTime = value; }
        }

        //��������
        private decimal _BasicPrice;
        /// <summary>
        /// ��������
        /// </summary>
        public decimal BasicPrice
        {
            get { return _BasicPrice; }
            set { _BasicPrice = value; }
        }

        //��������
        private decimal _BasicWage;
        /// <summary>
        /// �������ʡ��Ϻ�1450/��
        /// </summary>
        public decimal BasicWage
        {
            get { return _BasicWage; }
            set { _BasicWage = value; }
        }

        //������������
        private int _WorkHours;
        /// <summary>
        /// ������������
        /// </summary>
        public int WorkHours
        {
            get { return _WorkHours; }
            set { _WorkHours = value; }
        }

        //��ƽ��������
        private decimal _AvgDays;
        /// <summary>
        /// ��ƽ��������
        /// </summary>
        public decimal AvgDays
        {
            get { return _AvgDays; }
            set { _AvgDays = value; }
        }

        //�������
        private decimal _LaborRate;
        /// <summary>
        /// �������
        /// </summary>
        public decimal LaborRate
        {
            get { return _LaborRate; }
            set { _LaborRate = value; }
        }

        //�ۿ����
        private decimal _DeductRate;
        /// <summary>
        /// �ۿ����
        /// </summary>
        public decimal DeductRate
        {
            get { return _DeductRate; }
            set { _DeductRate = value; }
        }

        //��˰������
        private int _Tax;
        /// <summary>
        /// ��˰������
        /// </summary>
        public int Tax
        {
            get { return _Tax; }
            set { _Tax = value; }
        }


        //��ʱ�ϰ�ʱ��
        private string _StartTime;
        /// <summary>
        /// ��ʱ�ϰ�ʱ��
        /// </summary>
        public string StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        //��ʱ�°�ʱ��
        private string _EndTime;
        /// <summary>
        /// ��ʱ�°�ʱ��
        /// </summary>
        public string EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        //��ʱ����ʱ��
        private decimal  _RestTime;
        /// <summary>
        /// ��ʱ����ʱ��
        /// </summary>
        public decimal RestTime
        {
            get { return _RestTime; }
            set { _RestTime = value; }
        }

        //��ʱ���ʱ��
        private decimal _OtherTime;
        /// <summary>
        /// ��ʱ���ʱ��
        /// </summary>
        public decimal OtherTime
        {
            get { return _OtherTime; }
            set { _OtherTime = value; }
        }

        //�Ƽ�����Сʱ
        private decimal _bi_Work1;
        /// <summary>
        /// �Ƽ�����Сʱ
        /// </summary>
        public decimal bi_Work1
        {
            get { return _bi_Work1; }
            set { _bi_Work1 = value; }
        }

        //��ʱ����Сʱ
        private decimal _bi_Work2;
        /// <summary>
        /// ��ʱ����Сʱ
        /// </summary>
        public decimal bi_Work2
        {
            get { return _bi_Work2; }
            set { _bi_Work2 = value; }
        }

        //��ҹ��Ƽ�
        private int _bi_Night1;
        /// <summary>
        /// ��ҹ��Ƽ�
        /// </summary>
        public int bi_Night1
        {
            get { return _bi_Night1; }
            set { _bi_Night1 = value; }
        }

        //��ҹ���ʱ
        private int _bi_Night2;
        /// <summary>
        /// ��ҹ���ʱ
        /// </summary>
        public int bi_Night2
        {
            get { return _bi_Night2; }
            set { _bi_Night2 = value; }
        }


        //���ٹ�����
        private decimal _SickleaveRate;
        /// <summary>
        /// ���ٹ�����
        /// </summary>
        public decimal SickleaveRate
        {
            get { return _SickleaveRate; }
            set { _SickleaveRate = value; }
        }

        private decimal _SickleaveDay;

        /// <summary>
        /// ��������
        /// </summary>
        public decimal SickleaveDay
        {
            get { return _SickleaveDay; }
            set { _SickleaveDay = value; }
        }

        private decimal _OverPrice;
        /// <summary>
        /// �Ӱ���ʣ��������ʰ��������ۼ��㣬�Ӱ�Ѱ��ռӰ���ʼ��㣩
        /// </summary>
        public decimal OverPrice
        {
            get { return _OverPrice; }
            set { _OverPrice = value; }
        }

        //��ᱣ�ջ���
        private decimal _Social;
        /// <summary>
        /// ��ᱣ�ջ���
        /// </summary>
        public decimal Social
        {
            get { return _Social; }
            set { _Social = value; }
        }

        //���ϱ���
        private decimal _Oldage;
        /// <summary>
        /// ���ϱ���
        /// </summary>
        public decimal Oldage
        {
            get { return _Oldage; }
            set { _Oldage = value; }
        }

        //ʧҵ����
        private decimal _Unemploy;
        /// <summary>
        /// ʧҵ����
        /// </summary>
        public decimal Unemploy
        {
            get { return _Unemploy; }
            set { _Unemploy = value; }
        }

        //���˱���
        private decimal _Injury;
        /// <summary>
        /// ���˱���
        /// </summary>
        public decimal Injury
        {
            get { return _Injury; }
            set { _Injury = value; }
        }

        //��������
        private decimal _Maternity;
        /// <summary>
        /// ��������
        /// </summary>
        public decimal Maternity
        {
            get { return _Maternity; }
            set { _Maternity = value; }
        }

        //ҽ�Ʊ���
        private decimal _Health;
        /// <summary>
        /// ҽ�Ʊ���
        /// </summary>
        public decimal Health
        {
            get { return _Health; }
            set { _Health = value; }
        }

        //ס��������
        private decimal _HousingFund;
        /// <summary>
        /// ס��������
        /// </summary>
        public decimal HousingFund
        {
            get { return _HousingFund; }
            set { _HousingFund = value; }
        }

        //�����
        private decimal _UnionFee;
        /// <summary>
        /// �����
        /// </summary>
        public decimal UnionFee
        {
            get { return _UnionFee; }
            set { _UnionFee = value; }
        }

        //ũ�����ϱ���
        private decimal _A_Oldage;
        /// <summary>
        /// ũ�����ϱ���
        /// </summary>
        public decimal A_Oldage
        {
            get { return _A_Oldage; }
            set { _A_Oldage = value; }
        }

        //ũ��ҽ�Ʊ���
        private decimal _A_Health;
        /// <summary>
        /// ũ��ҽ�Ʊ���
        /// </summary>
        public decimal A_Health
        {
            get { return _A_Health; }
            set { _A_Health = value; }
        }

        //ũ�����˱���
        private decimal _A_Injury;
        /// <summary>
        /// ũ�����˱���
        /// </summary>
        public decimal A_Injury
        {
            get { return _A_Injury; }
            set { _A_Injury = value; }
        }

        //ȫ�ڽ�����
        private decimal _MaxAttbonus;
        /// <summary>
        /// ȫ�ڽ�����
        /// </summary>
        public decimal MaxAttbonus
        {
            get { return _MaxAttbonus; }
            set { _MaxAttbonus = value; }
        }

        //ȫ�ڽ�����
        private decimal _MinAttbonus;
        /// <summary>
        /// ȫ�ڽ�����
        /// </summary>
        public decimal MinAttbonus
        {
            get { return _MinAttbonus; }
            set { _MinAttbonus = value; }
        }

        //ȫ�ڽ�%
        private decimal _PercentAttbonus;
        /// <summary>
        /// ȫ�ڽ�%
        /// </summary>
        public decimal PercentAttbonus
        {
            get { return _PercentAttbonus; }
            set { _PercentAttbonus = value; }
        }

        //��������������
        private decimal _MaxWYbonus;
        /// <summary>
        /// ��������������
        /// </summary>
        public decimal MaxWYbonus
        {
            get { return _MaxWYbonus; }
            set { _MaxWYbonus = value; }
        }

        //��������������
        private decimal _MinWYbonus;
        /// <summary>
        /// ��������������
        /// </summary>
        public decimal MinWYbonus
        {
            get { return _MinWYbonus; }
            set { _MinWYbonus = value; }
        }

        //����������
        private decimal _WorkYearbonus;
        /// <summary>
        /// ����������
        /// </summary>
        public decimal WorkYearbonus
        {
            get { return _WorkYearbonus; }
            set { _WorkYearbonus = value; }
        }

        //������%
        private decimal _PercentWYbonus;
        /// <summary>
        /// ������%
        /// </summary>
        public decimal PercentWYbonus
        {
            get { return _PercentWYbonus; }
            set { _PercentWYbonus = value; }
        }

        //ֻ�ܸ���������
        private bool _isMinus;
        /// <summary>
        /// ֻ�ܸ���������
        /// </summary>
        public bool isMinus
        {
            get { return _isMinus; }
            set { _isMinus = value; }
        }
        //������������
        private decimal _basicWageNew;
        /// <summary>
        /// �����������ʣ���������ʱ�Ļ������ʺ�ֽ��������ʲ�һ�� 
        /// </summary>
        public decimal BasicWageNew
        {
            get { return _basicWageNew; }
            set { _basicWageNew = value; }
        }
    }
}