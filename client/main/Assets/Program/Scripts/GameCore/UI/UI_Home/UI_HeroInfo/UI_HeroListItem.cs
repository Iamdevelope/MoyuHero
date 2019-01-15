using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.UI;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DreamFaction.GameCore;

public enum ItemType
{
    Get,  //已获得
    NoGet, //未获得
    Empty, //空状态 
    None,  //默认
}
public class UI_HeroListItem : CellItem
{
    public int m_id;
    public int tableId = 0;
    public int specialValue = -1;
    private int m_heroLevel;
    public int m_heroStar; //英雄星级
    private string m_heroName;
    private string url = common.defaultPath;
    public ObjectCard objHero;
    private HeroItemData objHeroItemData;
    private Transform parent;
    private Text HeroName;                                       //英雄名称
    private ItemType itemType;   //item是属于英雄信息 还是熔灵  还是其他
    private HeroTemplate _HeroItem;
    private ArtresourceTemplate _Artresourcedata;
    //***************10.22日版本修改Wyf
    private Transform m_GetTran;   //已获得界面
    private Transform m_NoGetTran; //未获得界面
    private Image m_HeroIcon; //英雄头像
    private Transform m_ShangZhen; //上阵
    private Transform m_HeroLevelTran; //英雄等级父节点
    private Text m_HeroLevelText;  //英雄等级文本
    private Image m_HeroTypeIcon; //英雄类型图标
    private Text m_HeroTypeText; //英雄类型文本
    private Text m_FigthVigor; //战斗力
    //private Slider m_CompoundSlider; //碎片合成进度条
    private Text m_CompoundText; //合成进度text
    private Button m_EquieBt; //装备按钮
    private Button m_StrengthenBt; //强化按钮
    private Button m_GetHeroBt; //获取
    private Button m_RecruitBt; //招募
    private Image[] star = new Image[5];
    RectTransform m_RectFill; // 碎片进度
    float m_FillInitWidth; //初始碎片进度image宽度
    private Image m_Qulit; //品质
    private Transform m_StarsHui;//灰色星
    public override void InitUIData()
    {
        base.InitUIData();
        parent = selfTransform.FindChild("Parent");
        HeroName = selfTransform.FindChild("Parent/Name_txt").GetComponent<Text>();
        // GameEventDispatcher.Inst.addEventListener(GameEventID.HE_HeroBeginnerUpdateShow, OnSelectClick);

        //*************10.22日版本Wyf******************
        Transform m_New = parent.FindChild("new");
        m_GetTran = m_New.FindChild("get");
        m_NoGetTran = m_New.FindChild("noget");
        m_GetTran.gameObject.SetActive(false);
        m_NoGetTran.gameObject.SetActive(false);
        m_HeroIcon=m_New.FindChild("touxiang/icon").GetComponent<Image>();
        Button m_touxiang= m_New.FindChild("touxiang").GetComponent<Button>();
        m_touxiang.onClick.AddListener(OnIcon);
        m_ShangZhen = m_New.FindChild("touxiang/shangzhen");
        m_HeroLevelTran = m_New.FindChild("touxiang/level");
        m_Qulit = m_New.FindChild("touxiang/pinzhi").GetComponent<Image>();
        m_HeroLevelText = m_New.FindChild("touxiang/level/Text").GetComponent<Text>();
        m_HeroTypeIcon = m_New.FindChild("type").GetComponent<Image>();
        m_HeroTypeText = m_New.FindChild("typeText").GetComponent<Text>();
        m_CompoundText = m_New.FindChild("noget/progress").GetComponent<Text>();
        m_EquieBt = m_New.FindChild("get/equie").GetComponent<Button>();
        m_EquieBt.onClick.AddListener(OnEquie);
        m_StrengthenBt = m_New.FindChild("get/qianghua").GetComponent<Button>();
        m_StrengthenBt.onClick.AddListener(OnStrengthen);
        m_GetHeroBt = m_New.FindChild("noget/huoqu").GetComponent<Button>();
        m_GetHeroBt.onClick.AddListener(OnGetHero);
        m_RecruitBt = m_New.FindChild("noget/zhaomu").GetComponent<Button>();
        m_RecruitBt.onClick.AddListener(OnRecruit);
        m_FigthVigor = m_New.FindChild("get/zhandouli").GetComponent<Text>();
        m_RectFill =  m_New.FindChild("noget/Slider").GetChild(1).GetChild(0).GetComponent<RectTransform>();
        m_FillInitWidth = m_RectFill.sizeDelta.x;
        m_StarsHui = m_New.FindChild("touxiang/Star_Image(hui)");
    }
    //protected void OnDestroy()
    //{
    //    GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_HeroBeginnerUpdateShow, OnSelectClick);
    //}

    public override void InitUIView()
    {
        base.InitUIView();
        selfTransform.FindChild("Parent/JobType_Img").gameObject.SetActive(false);
        selfTransform.FindChild("Parent/RaceType_Img").gameObject.SetActive(false);
        selfTransform.FindChild("Parent/AttackType_Img").gameObject.SetActive(false);
        //Initialize();
    }

    /// <summary>
    /// 填充数据 需要标识一下是哪个界面的item
    /// </summary>
    /// <param name="objHero">英雄卡牌信息</param>
    public void Initialize(HeroItemData objHero,ItemType type=ItemType.None)
    {
        if (type == ItemType.Empty)
        {
            parent.gameObject.SetActive(false);
            return;
        }
        else
        {
            parent.gameObject.SetActive(true);
        }
         this.objHero = objHero.objcet;
         objHeroItemData = objHero;
        if (objHero.objcet != null)  //获取item
        {
            m_GetTran.gameObject.SetActive(true);
            m_NoGetTran.gameObject.SetActive(false);
            m_HeroLevelTran.gameObject.SetActive(true);
            //设置
            _HeroItem = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(objHero.objcet.GetHeroData().TableID);
            //卡牌图标
            _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(objHero.objcet.GetHeroData().GetHeroViewID());
            //等级显示
            m_heroLevel = objHero.objcet.GetHeroData().Level;
            m_HeroLevelText.text = m_heroLevel.ToString();
            //设置是否上阵
            int mid = -1;
            m_ShangZhen.gameObject.SetActive(ObjectSelf.GetInstance().Teams.IsHeroInTeam(objHero.objcet.GetGuid(), ref mid));
            //设置战斗力
            m_FigthVigor.text = objHero.objcet.GetHeroData().FightVigor.ToString();
            //星级
            m_heroStar = this.objHero.GetHeroData().StarLevel;
            int maxStar = _HeroItem.getMaxQuality();
            for (int i = 0; i < 6; i++)
            {
                Image temp = selfTransform.FindChild("Parent/new/touxiang/Star_Image").GetChild(i).GetComponent<Image>();
                if (i < m_heroStar)
                {
                    temp.gameObject.SetActive(true);
                }
                else
                {
                    temp.gameObject.SetActive(false);
                }
            }
            //m_StarsHui.gameObject.SetActive(true);
            m_StarsHui.gameObject.SetActive(false);
        }
        else if (objHero.objcet == null)  //未获取
        {
            m_GetTran.gameObject.SetActive(false);
            m_NoGetTran.gameObject.SetActive(true);
            m_HeroLevelTran.gameObject.SetActive(false);
            m_ShangZhen.gameObject.SetActive(false);
            m_StarsHui.gameObject.SetActive(false);
            for (int i = 0; i < 6; i++)
            {
              selfTransform.FindChild("Parent/new/touxiang/Star_Image").GetChild(i).gameObject.SetActive(false);
            }
            //设置
            _HeroItem = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(objHero.heroTableID);
            //卡牌图标
            _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_HeroItem.getArtresources());
            //碎片合成进度
            int fragmentCount = ObjectSelf.GetInstance().CommonItemContainer.GetFragmentCount(_HeroItem.GetID());
            int needCount=_HeroItem.getSyntheticCount();
            //Debug.LogError("process:" + fragmentCount + "max:" + needCount);
            if (fragmentCount >= needCount)
            {
                m_GetHeroBt.gameObject.SetActive(false);
                m_RecruitBt.gameObject.SetActive(true);
            }
            else
            {
                m_GetHeroBt.gameObject.SetActive(true);
                m_RecruitBt.gameObject.SetActive(false);
            }
            m_CompoundText.text = fragmentCount + "/" + needCount;
            float _baifenbi= ((float)fragmentCount / needCount);
            m_RectFill.sizeDelta = new Vector2((_baifenbi > 1 ? 1 :_baifenbi) *m_FillInitWidth, m_RectFill.sizeDelta.y);
        }
        m_HeroIcon.sprite = UIResourceMgr.LoadSprite(url + _Artresourcedata.getHeadiconresource());
        //类型
        InterfaceControler.GetInst().ShowTypeIcon(_HeroItem, m_HeroTypeIcon, m_HeroTypeText);
        //品质
        if (objHeroItemData.objcet == null)
        {
            m_Qulit.sprite = InterfaceControler.GetInst().ReturnHeroQuailty(_HeroItem.getQuality());
        }
        else
        {
            m_Qulit.sprite = InterfaceControler.GetInst().ReturnHeroQuailty(this.objHero.GetHeroData().QualityLev);
        }
        //Debug.Log(m_heroLevel);
        //名称
        ChsTextTemplate chs = new ChsTextTemplate();
        chs = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData(_HeroItem.getTitleID());
        m_heroName = chs.languageMap["Chinese"];
        HeroName.text = m_heroName;
        if (objHeroItemData.objcet == null)
        {
            SetTextColorByQuilt(_HeroItem.getQuality());
        }
        else
        {
            SetTextColorByQuilt(this.objHero.GetHeroData().QualityLev);
        }
       
    }
    void SetTextColorByQuilt(int quilt)
    {
        switch (quilt)
        { 
            case 1:
            case 0:
                HeroName.color = new Color(1,1,1);
                break;
            case 2:
                HeroName.color = new Color(15/255f, 208/255f, 0);
                break;
            case 3:
                HeroName.color = new Color(0, 155/255f, 1);
                break;
            case 4:
                HeroName.color = new Color(174/255f, 0, 1);
                break;
            case 5:
                HeroName.color = new Color(1,144/255f,0);
                break;
            case 6:
                HeroName.color = new Color(1, 0, 0);
                break;
            default:
                break;
        }
    }
    public void ShowInfo()
    {
        parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(parent.GetComponent<RectTransform>().anchoredPosition.x, 42);
    }

    //刷新英雄卡牌
    public void UpdateHeroCardData(ObjectCard heroObject)
    {
        objHero = heroObject;
        _HeroItem = heroObject.GetHeroRow();

        SetBaseInfo(heroObject.GetHeroData().Level);
    }
    //显示基础属性
    private void SetBaseInfo(int lv)
    {
        HeroName.text = GameUtils.getString(_HeroItem.getTitleID());//名称
        m_heroLevel = objHero.GetHeroData().Level;
        _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(objHero.GetHeroData().GetHeroViewID());
        Sprite _img = UIResourceMgr.LoadSprite(common.defaultPath + _Artresourcedata.getHeadartresource());
        m_heroStar = _HeroItem.getQuality();
        for (int i = 5; i < 10; ++i)//星级
        {
            Image temp = selfTransform.FindChild("Parent/bottomGrid/Star_Image").GetChild(i).GetComponent<Image>();
            temp.enabled = false;
        }
        for (int i = 5; i < 5 + m_heroStar; ++i)
        {
            Image temp = selfTransform.FindChild("Parent/bottomGrid/Star_Image").GetChild(i).GetComponent<Image>();
            temp.enabled = true;
        }
    }
    //显示所在队伍图标
    private void SetFormationNum()
    {
       
    }

    // 显示上阵标示
    public void ShowUpFront(bool ret = true)
    {
        Debug.LogError("是否已上阵:"+ret);
    }

    public void SetBoxEff(bool Active)
    {
       // BoxEff.SetActive(Active);
       
    }


    // 设置选择按钮的回调
    public void SetSelectClick()
    {
    }

    // 显示每个英雄的熔灵值
    public void ShowStar()
    {
    }

    // 添加下面的选择按钮
    public void AddSelectBtn()
    {

    }

    // 更新选择按钮，是否为取消或者无效
    public void updateSelectBtnState(bool isGrey)
    {

    }

    // 更新选择按钮，是否为取消或者无效
    public void updateSelectBtn(bool isGrey)
    {
  
    }

    // 选择熔灵按钮的回调
    private void OnSelectLitholysin()
    {

    }

    public void SetSelectState(bool isSelect)
    {
        //_isSelect = isSelect;
        //if (isSelect)
        //{
        //    SetBoxEff(true);
        //    _selectObj.GetComponent<Button>().transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("heromelt_button6");
        //    _selectObj.GetComponent<Image>().sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_quxiao");
        //}
        //else
        //{
        //    _selectObj.GetComponent<Button>().transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("heromelt_button5");
        //    _selectObj.GetComponent<Image>().sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_xuanze");

        //    SetBoxEff(false);
        //}
    }

    // 英雄是否是在探险中
    public void SetExplore(bool value)
    {

    }

    // 点击英雄图标时调用
    void OnHeroSelectClick()
    {
        UI_HomeControler.Inst.AddUI(HeroInfoPop.UI_ResPath);
        HeroInfoPop.inst.ShowInfo(objHero);
        HeroInfoPop.inst.Show3DModel(objHero);
    }

    // 获取这个英雄的 card
    public ObjectCard GetSelectedObjectCard()
    {
        return objHero;
    }

    public void SetActiveSkinImg(bool isActive)
    {

    }

    /////////////////////////////////
    //////// 铸魂界面
    // 添加下面的选择按钮
    public void AddSelectBtnSoul()
    {

    }

    void OnClickSoul()
    {
        //if (m_IsExplore)
        //{
        //    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble12"), selfTransform.transform.parent.parent.parent.parent);
        //    return;
        //}


        //if (_isSelect)
        //{
        //    _isSelect = false;
        //    UI_ArtifactSoul.inst.RemoveSelectHero(m_id, objHero, specialValue);
        //    _selectObj.GetComponent<Button>().transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("heromelt_button5");
        //    _selectObj.GetComponent<Image>().sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_xuanze");

        //    GameUtils.SetBtnSpriteGrayState(_selectObj.GetComponent<Button>(), false);
        //}
        //else
        //{
        //    if (_isGrey)
        //    {
        //        InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("relics_bubble2"), selfTransform.transform.parent.parent.parent.parent);
        //        return;
        //    }
        //    _isSelect = true;
        //    UI_ArtifactSoul.inst.AddSelectHero(m_id, objHero, specialValue);
        //    SetBoxEff(true);
        //    _selectObj.GetComponent<Button>().transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("heromelt_button6");
        //    _selectObj.GetComponent<Image>().sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_quxiao");

        //    GameUtils.SetBtnSpriteGrayState(_selectObj.GetComponent<Button>(), false);
        //}
    }

    //刷新数据
    public void UpdateShow()
    {

    }
    void OnEquie()
    {
      UI_HomeControler.Inst.AddUI(HeroStrengthen.UI_ResPath);
      HeroStrengthen.Inst.OnClickHeroIcon(objHero);
      HeroStrengthen.Inst.OpenEquipStrengthen();

    }
    void OnStrengthen()
    { 
        UI_HomeControler.Inst.AddUI(HeroStrengthen.UI_ResPath);
        HeroStrengthen.Inst.OnClickHeroIcon(objHero);
    }
    void OnGetHero()
    {
        UICommonManager.Inst.ShowHeroObtain(_HeroItem.getSyntheticItemid());
    }
    void OnRecruit()
    {
        UI_HeroListManager._instance.m_SelectRecruitTableId =this.objHeroItemData.heroTableID;
        ItemFragment item=(ItemFragment) ObjectSelf.GetInstance().CommonItemContainer.GetFragmentBaseItem(_HeroItem.GetID());
        if (item != null)
        {
            item.OnUseItem(1);
        }
    }
    void OnIcon()
    {
        if (objHeroItemData.objcet != null)
        {
            UICommonManager.Inst.ShowHero(objHero);
        }
        else
        {
            UICommonManager.Inst.ShowHeroFragment(objHeroItemData.heroTableID);
        }

    }
}
