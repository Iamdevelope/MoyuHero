using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using GNET;

public class UI_LookHeroClick : BaseUI 
{
    public int m_HeroId = 0;           // 英雄配表ID
    public int m_Exp = 0;              // 当前经验
    public int m_HeroLevel = 0;        // 英雄等级
    public int m_Hp = 0;               // 血量
    public int m_PysicalAttack = 0;    // 物理攻击
    public int m_Physicaldefence = 0;  // 物理防御
    public int m_MagicAttack = 0;      // 魔法攻击
    public int m_MagicDefence = 0;     // 魔法防御
    public int m_Skill1 = 0;           // 技能1编号（未开通为0）
    public int m_Skill2 = 0;           // 技能2编号（未开通为0）
    public int m_Skill3 = 0;           // 技能3编号（未开通为0）
    public int m_HeroViewId = 0;       // 英雄外观

    private HeroTemplate m_HeroData;
    private GameObject m_DataParent;                                     //用于隐藏信息
    private Text m_HeroName;                                             //名字
    private Transform m_HeroLevelTxt;                                    //等级
    private int m_HeroStar = 0;                                          //星级\
    private Button m_Btn;                                                //点击自己查看按钮

    public override void InitUIData()
    {
        base.InitUIData();
        m_DataParent = selfTransform.FindChild("Data").gameObject;
        m_HeroName = m_DataParent.transform.FindChild("Name_txt").GetComponent<Text>();
        m_HeroLevelTxt = m_DataParent.transform.FindChild("Level_txt");
        m_Btn = selfTransform.GetComponent<Button>();
        m_Btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBtn));
    }

    /// <summary>
    /// 初始显示按钮 and 信息
    /// </summary>
    public void InitShowUI()
    {
        m_DataParent.SetActive(false);
        if (m_HeroId != 0)
            m_DataParent.SetActive(true);

        m_HeroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(m_HeroId);
        InitHeroSharData();
        InitTextShow();
    }

    /// <summary>
    /// 初始化英雄星级
    /// </summary>
    private void InitHeroSharData()
    {
        //int _maxStart = m_HeroData.getMaxQuality();
        //m_HeroStar = m_HeroData.getQuality();
        //for (int i = 5; i < 10; ++i)//星级
        //{
        //    m_DataParent.transform.FindChild("Star_Image").GetChild(i - 5).GetComponent<Image>().enabled = i < 5 + _maxStart;
        //    m_DataParent.transform.FindChild("Star_Image").GetChild(i).GetComponent<Image>().enabled = i < 5 + m_HeroStar;
        //}

        InterfaceControler.GetInst().AddSharLevel(m_DataParent.transform.FindChild("Star_Image"), m_HeroData);
    }

    /// <summary>
    /// 初始化等级 名称
    /// </summary>
    private void InitTextShow()
    {
        string _heroNameStr =m_HeroLevel.ToString();
        InterfaceControler.AddLevelNum(_heroNameStr, m_HeroLevelTxt);

        m_HeroName.text = GameUtils.getString(m_HeroData.getTitleID());
    }

    /// <summary>
    /// 查看英雄按钮
    /// </summary>
    private void OnClickBtn()
    {
        ObjectCard _card = new ObjectCard();
        Hero hero = new Hero();
        hero.skill1 = m_Skill1;
        if(m_Skill2 > 0) hero.skill2 = m_Skill2;
        if(m_Skill3 > 0) hero.skill3 = m_Skill3;
        hero.heroid = m_HeroId;
        hero.heroexp = m_Exp;
        hero.herolevel = m_HeroLevel;
        hero.heroviewid = m_HeroViewId;
        _card.GetHeroData().Init(hero);

        
        UI_HomeControler.Inst.AddUI(HeroInfoPop.UI_ResPath);
        HeroInfoPop.inst.ShowInfo(_card);
        HeroInfoPop.inst.Show3DModel(_card);

    }
}
