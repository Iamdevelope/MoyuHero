using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using UnityEngine.Events;
namespace DreamFaction.UI
{
    public class UI_HeroItem : CellItem
    {
        private ArtresourceTemplate _Artresourcedata;                //资源表数据
        private HeroTemplate _Herodata;                              //英雄表数据
        public Transform _CardLevelText;                             //英雄等级

        private FormationState Formationstate;                       //是否是上阵状态
        private ObjectCard HeroObject;                               //英雄数据
        private Transform Parent;                                    //父节点
        //private Transform Pos;                                       //边界点
        private Image CardImage;                                     //卡牌图片
        private GameObject BoxEff;                                   //边框特效
        private Image AttackType_Img;                                //攻击类型
        private Image JobType_Img;                                   //职责类型
        private Image RaceType_Img;                                  //种族类型
        private Text CardNameText;                                   //卡牌名称
        public  Slider HeroEx;                                       //英雄经验
        private List<Image> FormationImageList;                      //所在阵型显示
        private string CardName;                                     //英雄名称
        public  int CardLevel;                                       //英雄等级
        private int CardStar;                                        //英雄星级
        private int maxStar;
        private int HeroType;                                        //是近战卡牌还是远程卡牌0近战1远程

        private Button SelectBtn;                                    //按钮
        private GameObject DownFeontImg;                                  //下阵
        private GameObject YetUpFrontImg;                                 //上阵
        //private GameObject SkinImg;
        private GameObject SelectBox;
        private string url = "UI/Number/card_number/";               //等级数字Sprite路径
        public enum FormationState
        {
            Null,
            Upstate,
            DownState,
        }
        public override void InitUIData() { base.InitUIData(); }

        /// <summary>
        /// 初始化阵型卡牌
        /// </summary>
        /// <param name="heroObject">卡牌</param>
        public void InitUIFormation(ObjectCard heroObject)
        {
            if (this.GetComponent<Button>() == null)
            {
                gameObject.AddComponent<Button>();
            }
            SelectBtn = this.GetComponent<Button>();
            SelectBtn.onClick.RemoveAllListeners();
            SelectBtn.onClick.AddListener(new UnityAction(OnclickSelectBtn));

            //SkinImg = selfTransform.FindChild("Parent/skinImg").gameObject;
            //SkinImg.SetActive(false);

            Parent = selfTransform.FindChild("Parent");
            //Pos = Parent.FindChild("Pos");
            HeroObject = heroObject;
            _Herodata = heroObject.GetHeroRow();

            HeroType = _Herodata.getClientSignType()[0];
            FormationImageList = new List<Image>();
            FormationImageList.Add(Parent.FindChild("TeamNum1").GetComponent<Image>());
            FormationImageList.Add(Parent.FindChild("TeamNum2").GetComponent<Image>());
            FormationImageList.Add(Parent.FindChild("TeamNum3").GetComponent<Image>());
           
            CardImage = Parent.FindChild("Icon_Img").GetComponent<Image>();
            CardNameText = Parent.FindChild("Name_txt").GetComponent<Text>();
            _CardLevelText = Parent.FindChild("Level_txt");
            BoxEff = Parent.FindChild("Box_Eff").gameObject;
            BoxEff.SetActive(false);
            AttackType_Img = Parent.FindChild("AttackType_Img").GetComponent<Image>();
            JobType_Img = Parent.FindChild("JobType_Img").GetComponent<Image>();
            RaceType_Img = Parent.FindChild("RaceType_Img").GetComponent<Image>();

            SetBaseInfo(heroObject.GetHeroData().Level);
            InterfaceControler.GetInst().ShowHeroImg(_Herodata, AttackType_Img, JobType_Img, RaceType_Img);
            SetFormationNum();
            InitIsUpFront();
            InitIsKYUpFront();

        }
        //刷新英雄卡牌
        public void UpdateHeroCardData(ObjectCard heroObject)
        {
            HeroObject = heroObject;
            _Herodata = heroObject.GetHeroRow();
            HeroType = _Herodata.getClientSignType()[0];
            _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(heroObject.GetHeroData().GetHeroViewID());
            SetBaseInfo(heroObject.GetHeroData().Level);
            InterfaceControler.GetInst().ShowHeroImg(_Herodata, AttackType_Img, JobType_Img, RaceType_Img);
            SetFormationNum();
            YetUpFrontImg.SetActive(false);
            DownFeontImg.SetActive(false);
            for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
            {
                int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                if (nGroup < 0 && nGroup >= GlobalMembers.MAX_MATRIX_COUNT)
                    continue;
                X_GUID pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[nGroup, i];
                if (pMemberGuiD.GUID_value == HeroObject.GetGuid().GUID_value)
                {
                    YetUpFrontImg.SetActive(true);
                }
            }
        }





        //初始话英雄搭配卡牌信息
        public void InitHeroMatchCardData(HeroTemplate carddata, ObjectCard card)
        {
            CardImage = this.transform.FindChild("Icon_Img").GetComponent<Image>();
            CardNameText = this.transform.FindChild("Name_txt").GetComponent<Text>();
            _Herodata = carddata;
            _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(carddata.getArtresources());
            CardImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _Artresourcedata.getHeadartresource());
            CardName = GameUtils.getString(_Herodata.getTitleID());
            CardNameText.text = CardName;//名称
        }
        //初始化战斗结束卡牌
        public void InitFightEnd(ObjectCard heroObject)
        {
            HeroObject = heroObject;
            _Herodata = heroObject.GetHeroRow();
            Parent = selfTransform.FindChild("Parent");
            int _ArtresourcedataID = HeroObject.GetHeroData().GetHeroViewID();
            CardImage = Parent.FindChild("Icon_Img").GetComponent<Image>();
            CardNameText = Parent.FindChild("Name_txt").GetComponent<Text>();
            _CardLevelText = Parent.FindChild("Level_txt");
            HeroEx = Parent.FindChild("HeroExbar").GetComponent<Slider>();
            //HeroEx.value = heroObject.GetHeroData().Exp;
            CardName = GameUtils.getString(_Herodata.getTitleID());
            CardNameText.text = CardName;//名称
            CardLevel = heroObject.GetHeroData().Level;
            InterfaceControler.GetInst().AddLevelNum(CardLevel.ToString(), _CardLevelText, url);
            _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(HeroObject.GetHeroData().GetHeroViewID());
            CardImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _Artresourcedata.getHeadartresource());

            InterfaceControler.GetInst().AddSharLevel(Parent.FindChild("Star_Image"), _Herodata);
            //SetBaseInfo(heroObject.GetHeroData().Level);
        }
        //初始化怪物卡牌
        public void InitMonsterCard(MonsterTemplate _monster)
        {
            Parent = selfTransform.FindChild("Parent");
            Image Boss_Image = Parent.FindChild("Boss_Image").GetComponent<Image>();
            CardNameText = Parent.FindChild("Name_txt").GetComponent<Text>();
            _CardLevelText = Parent.FindChild("Level_txt");
            CardImage = Parent.FindChild("Icon_Img").GetComponent<Image>();
            CardStar = _monster.getMonsterstar();
            maxStar = _monster.getMonstermaxstar();
            CardLevel = _monster.getMonsterlevel();
            CardName = GameUtils.getString( _monster.getMonstername());
            CardNameText.text = CardName;//名称
            InterfaceControler.GetInst().AddLevelNum(CardLevel.ToString(), _CardLevelText, url);
            _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_monster.getArtresources());
            Sprite _img = UIResourceMgr.LoadSprite(common.defaultPath + _Artresourcedata.getHeadiconresource());
            CardImage.sprite = _img;//图片

            for (int i = 0; i < 5; i++)
            {
                Image temp = Parent.FindChild("Star_Image").GetChild(i).GetComponent<Image>();
                temp.enabled = i < maxStar;

                temp = Parent.FindChild("Star_Image").GetChild(i + 5).GetComponent<Image>();
                temp.enabled = i < CardStar;
            }

            Boss_Image.enabled = _monster.getMonstertype() == 2 ? true : false;
        }
        public int GetHeroType() { return HeroType; }
        
        public ObjectCard GetHeroObject() { return HeroObject; }
        public int GetHeroID()
        {
            return HeroObject.GetHeroData().TableID;
        }

        //显示基础属性
        private void SetBaseInfo(int lv)
        {
            CardName = GameUtils.getString( _Herodata.getTitleID());
            CardNameText.text = CardName;//名称
            CardLevel = lv;
            InterfaceControler.GetInst().AddLevelNum(CardLevel.ToString(), _CardLevelText,url);
            _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(HeroObject.GetHeroData().GetHeroViewID());
            CardImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _Artresourcedata.getHeadiconresource());

            InterfaceControler.GetInst().AddSharLevel(Parent.FindChild("Star_Image"),_Herodata);
        }

        //显示所在队伍图片
        private void SetFormationNum()
        {
            ClearFormNum();
            List<int> NumList = new List<int>();
            for (int k = 0; k < GlobalMembers.MAX_MATRIX_COUNT; ++k)
            {
                for (int j = 0; j < GlobalMembers.MAX_TEAM_CELL_COUNT; ++j)
                {
                    ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[k, j]);
                    if (temp == null)
                        continue;
                    if (HeroObject.GetHeroData().GUID == temp.GetHeroData().GUID)
                    {
                        NumList.Add(k+1);
                    }
                }
            }
            int count = NumList.Count;
            for(int i=0;i<count-1;i++)
            {
                for(int p=0;p<count-1-i;p++)
                {
                    if(NumList[p]>NumList[p+1])
                    {
                        int a = NumList[p];
                        NumList[p] = NumList[p + 1];
                        NumList[p + 1] = a;
                    }
                }
            }
            for(int o=0;o<count;++o)
            {
                FormationImageList[o].enabled = true;
                FormationImageList[o].sprite = UIResourceMgr.LoadSprite("UI/Number/formation_num/" + NumList[o].ToString());//图片
            }
        }

        private void ClearFormNum()
        {
            int count = FormationImageList.Count;
            for (int i = 0; i < count; ++i)
            {
                FormationImageList[i].enabled = false;
            }
        }

        //显示是否上阵
        void InitIsUpFront()
        {
            if (selfTransform.FindChild("Parent/Box_Eff/HeroFrameLight01").gameObject != null)
            {
                SelectBox = selfTransform.FindChild("Parent/Box_Eff/HeroFrameLight01").gameObject;
            }
            if (selfTransform.FindChild("Parent/DownFrontImg").gameObject != null)
            {
                DownFeontImg = selfTransform.FindChild("Parent/DownFrontImg").gameObject;
            }
            if (selfTransform.FindChild("Parent/YetUpFrontImg").gameObject != null)
            {
                YetUpFrontImg = selfTransform.FindChild("Parent/YetUpFrontImg").gameObject;
            }
            Formationstate = FormationState.Null;
            YetUpFrontImg.SetActive(false);
            DownFeontImg.SetActive(false);
            SelectBox.SetActive(false);
            for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
            {
                int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                if (nGroup < 0 && nGroup >= GlobalMembers.MAX_MATRIX_COUNT)
                    continue;
                X_GUID pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[nGroup, i];
                if (pMemberGuiD.GUID_value == HeroObject.GetGuid().GUID_value)
                {
                    Formationstate = FormationState.Upstate;
                    YetUpFrontImg.SetActive(true);
                    if (UI_Form.GetInst().GetGuid() != null && UI_Form.GetInst().GetGuid().GUID_value == HeroObject.GetGuid().GUID_value)
                    {
                        Formationstate = FormationState.DownState;
                        YetUpFrontImg.SetActive(false);
                        DownFeontImg.SetActive(true);
                        SelectBox.SetActive(true);
                    }
                }
            }
        }

        //显示是否可以上场  按钮置灰
        private void InitIsKYUpFront()
        {
            GameUtils.SetImageGrayState(CardImage,false);
            if (UI_Form.GetInst().GetCurPos() == 1 && _Herodata.getClientSignType()[0] == 0)
            {
                GameUtils.SetImageGrayState(CardImage, true);
            }
            if (UI_Form.GetInst().GetGuid().GUID_value > 0)
            {
                if (UI_Form.GetInst().GetAttackPos() == 0 && _Herodata.getClientSignType()[0] == 1)
                {
                    int count = UI_Form.GetInst().GetBackHeroGuids().Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (UI_Form.GetInst().GetBackHeroGuids().Contains(HeroObject.GetGuid().GUID_value))
                        {
                            GameUtils.SetImageGrayState(CardImage, true);
                        }
                    }
                }
            }
            for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
            {
                int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                if (nGroup < 0 && nGroup >= GlobalMembers.MAX_MATRIX_COUNT)
                    continue;
                X_GUID pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[nGroup, i];
                if (pMemberGuiD.IsValid())
                {
                    ObjectCard card = ObjectSelf.GetInstance().HeroContainerBag.FindHero(pMemberGuiD);
                    if (card == null)
                        continue;
                    int mid = GameUtils.GetHeroIDNum(card.GetHeroRow().getId());
                    if (UI_Form.GetInst().GetGuid() != null && UI_Form.GetInst().GetGuid().GUID_value != pMemberGuiD.GUID_value)
                    {
                        if (mid == GameUtils.GetHeroIDNum(HeroObject.GetHeroRow().getId()) && pMemberGuiD.GUID_value != HeroObject.GetGuid().GUID_value)
                        {
                            GameUtils.SetImageGrayState(CardImage, true);
                        }
                    }
                }
            }
        }



        //判断是否上阵
        void IsUpState()
        {
            int pos = UI_Form.GetInst().GetSelectModelPos();
            switch (Formationstate)
            {
//                 case FormationState.Null:
//                     UI_Form.GetInst().SendProtocol(ObjectSelf.GetInstance().Teams.GetDefaultGroup(), pos);
//                     break;
//                 case FormationState.Upstate:
//                     UI_Form.GetInst().SendProtocol(ObjectSelf.GetInstance().Teams.GetDefaultGroup(), pos);
//                     break;
//                 case FormationState.DownState:
//                     UI_Form.GetInst().SendDownProtocol(ObjectSelf.GetInstance().Teams.GetDefaultGroup(), pos);
//                     break;
            }
        }

        void OnclickSelectBtn()
        {
            UI_Form.GetInst().SetCurrentHeroItem(this);
            //UI_Form.GetInst().SetSelectModel(HeroObject);//
            if (UI_Form.GetInst().isClick)
            {
                //相同英雄不可同时上阵
                for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
                {
                    int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                    if (nGroup < 0 && nGroup >= GlobalMembers.MAX_MATRIX_COUNT)
                        continue;
                    X_GUID pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[nGroup, i];
                    if (pMemberGuiD.IsValid())
                    {
                        ObjectCard card = ObjectSelf.GetInstance().HeroContainerBag.FindHero(pMemberGuiD);
                        if (card == null)
                            continue;
                        int mid = GameUtils.GetHeroIDNum( card.GetHeroRow().getId());
                        if (UI_Form.GetInst().GetGuid() != null && UI_Form.GetInst().GetGuid().GUID_value != pMemberGuiD.GUID_value)
                        {
                            if (mid == GameUtils.GetHeroIDNum(HeroObject.GetHeroRow().getId()) && pMemberGuiD.GUID_value != HeroObject.GetGuid().GUID_value)
                            {
                                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_fightprepare_tip3"), UI_Form.GetInst().GetMsgBoxGroup(),1);
                                return;
                            }
                        }
                    }
                }
                //近战英雄只能在前排
                if (UI_Form.GetInst().GetCurPos() == 1 && _Herodata.getClientSignType()[0] == 0)
                {
                    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_fightprepare_tip4"), UI_Form.GetInst().GetMsgBoxGroup(),1);
                    return;
                }
                //换人 近战英雄只能在前排
                if (UI_Form.GetInst().GetGuid().GUID_value > 0)
                {
                    if (UI_Form.GetInst().GetAttackPos() == 0 && _Herodata.getClientSignType()[0] == 1)
                    {
                        int count = UI_Form.GetInst().GetBackHeroGuids().Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (UI_Form.GetInst().GetBackHeroGuids().Contains(HeroObject.GetGuid().GUID_value))
                            {
                                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_fightprepare_tip4"), UI_Form.GetInst().GetMsgBoxGroup(),1);
                                return;
                            }
                        }
                    }
                }
                IsUpState();
                UI_Form.GetInst().isClick = false;
            }

            UI_Form.GetInst().OnClickPackBtn();

            // 新手引导
            if (GuideManager.GetInstance().isGuideUser && 
                GuideManager.GetInstance().IsContentGuideID(100305) == false && 
                GuideManager.GetInstance().GetBackCount(100304))
            {
                GuideManager.GetInstance().ShowGuideWithIndex(100305);
            }

            // 新手引导
            if (GuideManager.GetInstance().isGuideUser && 
                GuideManager.GetInstance().IsContentGuideID(100404) == false && 
                GuideManager.GetInstance().GetBackCount(100403))
            {
                GuideManager.GetInstance().ShowGuideWithIndex(100404);
            }
        }
        public void StartSliderRun(float min, int oldLevel)
        {
            HeroEx.value = min;
            InterfaceControler.GetInst().AddLevelNum(oldLevel.ToString(), _CardLevelText,url);
            //UI_Form.GetInst().AddLevelNum(oldLevel.ToString(), _CardLevelText);
            float max=HeroObject.GetHeroData().GetExpPercentage();
            int newLevel=HeroObject.GetHeroData().Level;
            StartCoroutine(SliderRun(min,max , oldLevel, newLevel, _CardLevelText));
        }
        public IEnumerator SliderRun(float min, float max, int oldLevel, int newLevel, Transform preant = null)
        {
            float maxValue = 0;
            if (oldLevel < newLevel)
            {
                maxValue = max;
                max = 1.0f;
            }
            for (; ; min += 0.01f)
            {
                if (min > max)
                {
                    if (oldLevel < newLevel)
                    {
                        oldLevel = newLevel;
                        InterfaceControler.GetInst().AddLevelNum(CardLevel.ToString(), preant, url);
                        //UI_Form.GetInst().AddLevelNum(newLevel.ToString(), preant);
                        StartCoroutine(SliderRun( 0, maxValue, oldLevel, newLevel,preant));
                    }
                    else
                    {
                        HeroEx.value = max;
                        break;
                    }

                }
                else
                {
                    //min = objc.GetHeroData().GetExpPercentage();
                    HeroEx.value = min;
                }

                yield return new WaitForSeconds(0.001f);
            }
            // StopCoroutine(SliderRun(mExpProgress, min, max));
        }


        //private void FormationStateUpdate()
        //{
        //    switch (Formationstate)
        //    {
        //        case FormationState.Null:
        //            {
        //                BoxEff.SetActive(false);
        //                //UpFormationText.text = GameUtils.getString("fight_fightprepare_button5"); //"上 阵"
        //                //Battle_ButtonImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Public_14");
        //            }
        //            break;
        //        case FormationState.Upstate:
        //            {
        //                //是否在探险
        //                BoxEff.SetActive(true);
        //                //UpFormationText.text = GameUtils.getString("common_button_cancel1"); //"取 消"
        //                //Battle_ButtonImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Public_16");
        //                UI_Form.GetInst().UpdateHeroClick();
        //            }
        //            break;
        //        case FormationState.DownState:
        //            {
        //                BoxEff.SetActive(false);
        //                // UpFormationText.text = GameUtils.getString("fight_fightprepare_button5"); //"上 阵"
        //                //Battle_ButtonImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Public_14");
        //                if (UI_Form.GetInst().GetSelectModel() == HeroObject)
        //                    UI_Form.GetInst().SetSelectModel(null);
        //                if (UI_Form.GetInst().GetCurrentHeroItem() == this)
        //                    UI_Form.GetInst().SetCurrentHeroItem(null);
        //                // UI_Form.GetInst().SetCurrentHeroItem(null);
        //                //  UI_Form.GetInst().ClearHeroClick();
        //            }
        //            break;
        //    }
        //}
    }
   
}

