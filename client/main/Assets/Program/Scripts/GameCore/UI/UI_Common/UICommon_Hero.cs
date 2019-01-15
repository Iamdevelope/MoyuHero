using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Collections;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using System.Collections.Generic;


public class UICommon_Hero : UICommon_HeroBase, UICommonInterface
{
    class SkillIconItem
    {
        Image icon = null;
        Text typeTxt = null;
        Text name = null;
        Text level = null;
        Text hintTxt = null;
        public SkillIconItem(Transform trans)
        {
            icon = trans.FindChild("IconObj/iconImg").GetComponent<Image>();
            typeTxt = trans.FindChild("IconObj/typeTxt").GetComponent<Text>();
            name = trans.FindChild("NameTxt").GetComponent<Text>();
            level = trans.FindChild("LvTxt").GetComponent<Text>();
            hintTxt = trans.FindChild("Text").GetComponent<Text>();
        }

        public void SetIcon(string iconStr)
        {
            icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + iconStr);
        }

        public void SetType(int type)
        {
            switch (type)
            {
                case 1:         //主动;
                    typeTxt.text = GameUtils.getString("initiative");
                    break;
                case 2:         //被动;
                    typeTxt.text = GameUtils.getString("passive");
                    break;
                default:
                    break;
            }
        }

        public void SetName(string nameid)
        {
            name.text = GameUtils.getString(nameid);
        }

        public void SetLevel(string lv)
        {
            hintTxt.gameObject.SetActive(false);
            level.gameObject.SetActive(true);
            level.text = "Lv" + lv;
        }

        public void SetHint(string hint)
        {
            hintTxt.gameObject.SetActive(true);
            level.gameObject.SetActive(false);
            hintTxt.text = hint;
        }
    }

    class StarGroup
    {
        protected List<Image> frontImgs = new List<Image>();
        protected List<Image> backImgs = new List<Image>();

        const int max = 6;
        Transform mTrans = null;

        public StarGroup(Transform trans)
        {
            mTrans = trans;

            for (int i = 0; i < max; i++ )
            {
                backImgs.Add(mTrans.FindChild("Sart_Bg" + i).GetComponent<Image>());
                frontImgs.Add(mTrans.FindChild("Sart" + i).GetComponent<Image>());
            }
        }

        /// <summary>
        /// 如果maxNum为-1，那么表示不显示背景灰色星星;
        /// </summary>
        /// <param name="starNum"></param>
        /// <param name="maxNum"></param>
        public void ShowStar(int starNum, int maxNum = -1)
        {
            if (starNum > max)
            {
                LogManager.LogError("超出最大星星数量");
                return;
            }

            for (int i = 0; i < max; i++ )
            {
                frontImgs[i].gameObject.SetActive(starNum > i);
                backImgs[i].gameObject.SetActive(maxNum > i);
            }
        }
    }
    
    protected Image m_HeroTypeImg = null;    //英雄类型;
    protected Image m_ApitudeImg = null;     //资质;
    protected Slider m_ExpSlider = null;     //经验条;
    protected Image m_HeroModle = null;      //英雄模型;
    private GameObject m_Card3Dmodel = null;                                      //当前实例化3D模型
    private Transform m_Point = null;                                             //3D模型实例化位置
    protected Transform m_StarGroupTrans = null;
    protected List<Transform> m_SkillTrans = null;

    StarGroup mStarGroup = null;
    List<SkillIconItem> mSkillIcons = new List<SkillIconItem>();
    ObjectCard mObjectCard = null;

    public override void InitUIData()
    {
        base.InitUIData();

        m_HeroTypeImg = selfTransform.FindChild("Panel/LeftObj/HeroInfo/TypeImg").GetComponent<Image>();
        m_ApitudeImg = selfTransform.FindChild("Panel/LeftObj/HeroInfo/ApitudeObj/ApitudeType").GetComponent<Image>();
        m_StarGroupTrans = selfTransform.FindChild("Panel/LeftObj/HeroInfo/StarGroup");
        m_ExpSlider = selfTransform.FindChild("Panel/LeftObj/HeroInfo/Slider").GetComponent<Slider>();
        m_HeroModle = selfTransform.FindChild("Panel/LeftObj/heroImg").GetComponent<Image>();

        m_SkillTrans = new List<Transform>();
        for (int i = 0; i< 6; i++)
        {
            Transform trans = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item" + i);
            m_SkillTrans.Add(trans);
            
            SkillIconItem item = new SkillIconItem(trans);
            mSkillIcons.Add(item);
        }

        mStarGroup = new StarGroup(m_StarGroupTrans);
    }

    protected override void OnClickCloseBtn()
    {
        base.OnClickCloseBtn();

        UICommonManager.Inst.RemoveUI(UICommonType.CommonHero, this);
    }

    protected override void OnClickButton0()
    {
        base.OnClickButton0();

        SkillClickHandler(1);
    }

    protected override void OnClickButton1()
    {
        base.OnClickButton1();

        SkillClickHandler(2);
    }

    protected override void OnClickButton2()
    {
        base.OnClickButton2();

        SkillClickHandler(3);
    }

    protected override void OnClickButton3()
    {
        base.OnClickButton3();

        SkillClickHandler(4);
    }

    protected override void OnClickButton4()
    {
        base.OnClickButton4();
        
        SkillClickHandler(5);
    }

    protected override void OnClickButton5()
    {
        base.OnClickButton5();

        SkillClickHandler(6);
    }

    void SkillClickHandler(int idx)
    {
        List<int> skillIds = mObjectCard.GetHeroData().HeroSkillDB.SkillList;
        if (skillIds == null || skillIds.Count != 6)
        {
            LogManager.LogError("技能个数不为6个");
            return;
        }
        else
        {
            SkillTemplate skillT = DataTemplate.GetInstance().m_SkillTable.getTableData(skillIds[idx - 1]) as SkillTemplate;
            bool isSkillOpen = mObjectCard.GetHeroData().QualityLev >= idx;

            UI_SkillPopMgr.SkillPopUIType type = isSkillOpen ? UI_SkillPopMgr.SkillPopUIType.LevelUp : UI_SkillPopMgr.SkillPopUIType.Locked;
            UICommonManager.Inst.ShowSkill(skillT, mObjectCard, idx, type);
        }

    }

    public override void OnDestroy()
    {
 	    base.OnDestroy();
        mStarGroup = null;
        m_SkillTrans = null;
        mSkillIcons = null;
        mObjectCard = null;

        m_Point = null;

        ClearModel();
    }

    public void SetData(ObjectCard objectCard)
    {
        mObjectCard = objectCard;

        HeroData heroData = objectCard.GetHeroData();

        HeroTemplate heroT = objectCard.GetHeroRow();
        //----英雄信息----
        m_Name.text = string.Format(GameUtils.GetHeroNameFontColor(heroData.QualityLev),GameUtils.getString(heroT.getNameID())) ;
        m_Alias.text = string.Format(GameUtils.GetHeroNameFontColor(heroData.QualityLev),GameUtils.getString(heroT.getTitleID()));
        //InterfaceControler.GetInst().ReturnHeroQuailty(heroData.QualityLev);
        //GameUtils.GetItemQualitySprite()
        
        //TODO:资质图;
        m_ApitudeImg.sprite = InterfaceControler.GetInst().GetHeroAptImg(heroT);
        InterfaceControler.GetInst().ShowTypeIcon(heroT, m_HeroTypeImg);


        m_Value.text = heroData.Level + "/" + heroT.getMaxLevel();
        m_ExpTxt.text = heroData.Exp + "/" + objectCard.GetAllExp();
        m_ExpSlider.value = heroData.GetExpPercentage();

        int maxStar = GameUtils.GetCurStarMax(heroT.getBorn(), heroT.getQosition(), heroData.QualityLev);
        mStarGroup.ShowStar(heroData.StarLevel, 6);

        //----模型----
        StartCoroutine(Show3DModel());

        //----属性----;
        m_AText0.text = GameUtils.getString("power");
        m_AText1.text = GameUtils.getString("attribute1name");
        m_AText2.text = GameUtils.getString("attribute2name");
        m_AText3.text = GameUtils.getString("attribute3name");

        m_AValue0.text = heroData.FightVigor.ToString();
        m_AValue1.text = objectCard.GetPhysicalAttack().ToString();
        m_AValue2.text = objectCard.GetPhysicalDefence().ToString();
        m_AValue3.text = objectCard.GetMaxHP().ToString();

        //----特点----
        m_FeatureTxt.text = GameUtils.getString(heroT.getTedian());

        //----技能----
        List<int> skillIds = heroData.HeroSkillDB.SkillList;
        if (skillIds == null || skillIds.Count != 6)
        {
            LogManager.LogError("技能个数不为6个");
            return;
        }

        for (int i = 0; i < skillIds.Count; i++)
        {
            //是否解锁了;
            bool isUnlock = IsSkillLocked(i);

            SkillTemplate skillT = DataTemplate.GetInstance().m_SkillTable.getTableData(skillIds[i]) as SkillTemplate;
            mSkillIcons[i].SetIcon(skillT.getSkillIcon());
            mSkillIcons[i].SetName(skillT.getSkillName());
            mSkillIcons[i].SetType(skillT.getSkillType());

            if (isUnlock)
            {
                mSkillIcons[i].SetLevel(skillT.getSkillLevel().ToString());
            }
            else
            {
                mSkillIcons[i].SetHint(string.Format(GameUtils.getString("skillunlockquality"), GameUtils.ConverQualityToStr(i + 1)));
            }
        }

        //----简介----
        m_IntroductTxt.text = GameUtils.getString(heroT.getDescriptionID());
    }

    private bool IsSkillLocked(int idx)
    {
        return mObjectCard.GetHeroData().QualityLev > idx;
    }

    /// <summary>
    /// 显示3D模型
    /// </summary>
    /// <param name="card"></param>
    IEnumerator Show3DModel()
    {
        ArtresourceTemplate m_Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(mObjectCard.GetHeroData().GetHeroViewID());
        GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(m_Artresourcedata.getArtresources());

        while (AssetLoader.Inst.IsReadyOver == false)
            yield return 1;

        ClearModel();
        GameObject go = GameObject.Find("pos");

        if (go == null)
        {
            GameObject ModelViewRoom = GameObject.Find("ModelViewRoom");
            if (ModelViewRoom == null)
            {
                Vector3 _pos = new Vector3(5000, 0, 0);
                ModelViewRoom = Instantiate(Resources.Load("UI/Prefabs/UI_Home/MainHome/ModelViewRoom"), _pos, Quaternion.identity) as GameObject;
                ModelViewRoom.name = "ModelViewRoom";
            }
            m_Point = ModelViewRoom.transform.FindChild("pos");

            m_Point.localPosition = new Vector3(m_Point.localPosition.x + 0.2f, m_Point.localPosition.y, m_Point.localPosition.z);
            GameObject camObj = m_Point.FindChild("Camera").gameObject;
            camObj.SetActive(true);
        }
        else
        {
            m_Point = GameObject.Find("pos").transform;
        }

        if (_AssetRes != null)
        {
            if (_AssetRes.GetComponent<NavMeshAgent>() != null)
                _AssetRes.GetComponent<NavMeshAgent>().enabled = false;

            m_Card3Dmodel = Instantiate(_AssetRes, m_Point.position, m_Point.rotation) as GameObject;
            float _zoom = m_Artresourcedata.getArtresources_zoom();
            m_Card3Dmodel.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
            m_Card3Dmodel.transform.parent = m_Point;

            ////设置3D模型摩擦力
            //m_Card3Dmodel.rigidbody.angularDrag = 2.8f;
            //m_Card3Dmodel.rigidbody.mass = 1.5f;

            Animation anim = m_Card3Dmodel.GetComponent<Animation>();
            if (anim != null)
            {
                m_Card3Dmodel.GetComponent<Animation>().Play("Nidle1");
                m_Card3Dmodel.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            }
        }
    }

    /// <summary>
    /// 清除模型
    /// </summary>
    private void ClearModel()
    {
        if (m_Card3Dmodel != null)
        {
            Destroy(m_Card3Dmodel);
            m_Card3Dmodel = null;
        }
        
        if (m_Point != null)
        {
            Transform camObj = m_Point.FindChild("CameraPopUp");
            if (camObj != null)
            {
                camObj.gameObject.SetActive(false);
            }
        }
    }
}
