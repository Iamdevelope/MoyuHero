using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DG.Tweening;
using DreamFaction.LogSystem;

namespace DreamFaction.UI
{
    public class UI_RewardsItemManage : BaseUI
    {
        //public static UI_RewardsItemManage _instanse;
        public int id = 0;//物品id
        public Text mName;//名称
        public Image mIcon;//图标
        public Text mNum;//数量
        public List<GameObject> starList = new List<GameObject>();//存放个数
        public int typeNum; //类型 1为符文  2为道具 3为英雄
        public Button mSelf;

        //符文ui相关
        public GameObject RuneIcon;//符文的父物体
        private Image mNorBg = null;
        private Image mSpecBg = null;
        private Image RuneImage = null;
        private GameObject[] mTypeObjs = null;
        private UIHeroStar star;

        public override void InitUIData()
        {
            base.InitUIData();
            // _instanse = this;
            mSelf = transform.GetComponent<Button>();
            mSelf.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSelf));
        }

        public void OnClickSelf()
        {
            switch (typeNum)
            {
                //符文
                case 1:

                    ItemTemplate rune = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(id);
                    UI_RuneInfo.SetShowRuneDate(rune);
                    if (UI_HomeControler.Inst == null)
                    {
                        UI_FightControler.Inst.AddUI("UI_Rune/UI_RuneInfo_1_3");
                    }
                    else
                    {
                        UI_HomeControler.Inst.AddUI("UI_Rune/UI_RuneInfo_1_3");
                    }
                    break;
                //道具
                case 2:
                    ItemTemplate item = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(id);
                    UI_Item.SetItemTemplate(item);
                    if (UI_HomeControler.Inst == null)
                    {
                        UI_FightControler.Inst.AddUI("UI_Home/UI_Item_1_3");
                    }
                    else
                    {
                        UI_HomeControler.Inst.AddUI("UI_Home/UI_Item_1_3");
                    }


                    break;
                //英雄
                case 3:
                    if (UI_HomeControler.Inst == null)
                    {
                        UI_FightControler.Inst.AddUI("UI_Home/UI_HeroInfoPop_1_3");
                    }
                    else
                    {
                        UI_HomeControler.Inst.AddUI("UI_Home/UI_HeroInfoPop_1_3");
                    }

                    //ObjectCard obj = new ObjectCard();
                    //Hero hero = new Hero();
                    HeroTemplate _hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(id);
                    //hero.skill1 = _hero.getSkill1ID();  
                    //hero.skill2 = _hero.getSkill2ID();
                    //hero.skill3 = _hero.getSkill3ID();
                    //hero.heroid = id;
                    //hero.herolevel = 1;
                    //hero.heroviewid = _hero.getArtresources();
                    //obj.GetHeroData().Init(hero);
                    //Destroy(UI_SelectFightArea.Inst.Card3Dmodel); 

                    HeroInfoPop.inst.SetShowData(_hero);

                    break;
                default:
                    break;
            }
        }

        public void ShowRune(ItemTemplate _temp_rune, bool nativeSize = true)
        {
            //符文
            RuneIcon = selfTransform.FindChild("RuneIconItem").gameObject;
            mNorBg = selfTransform.FindChild("RuneIconItem/RuneIconList/bg").GetComponent<Image>();
            mSpecBg = selfTransform.FindChild("RuneIconItem/RuneIconList/bg1").GetComponent<Image>();
            RuneImage = selfTransform.FindChild("RuneIconItem/RuneIconList/icon").GetComponent<Image>();
            this.star = transform.FindChild("star").GetComponent<UIHeroStar>();
            mTypeObjs = new GameObject[4];
            for (int i = 0; i < 4; i++)
            {
                mTypeObjs[i] = transform.FindChild("RuneIconItem/RuneIconList/bg/type" + (i + 1)).gameObject;
            }

            string _tempIconNam_2 = _temp_rune.getIcon();
            RuneImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_2);
            if (nativeSize)
            {
                RuneImage.SetNativeSize();
            }

            bool isSpecial = RuneModule.IsSpecialRune(_temp_rune);
            SetIsSpecial(isSpecial);
            SetRuneType(_temp_rune.getRune_type());

            this.star.gameObject.SetActive(true);
            int star = _temp_rune.getRune_quality();
            int maxStar = 5;
            this.star.Set(star, maxStar);
        }
        private void SetIsSpecial(bool isSpecial)
        {
            mNorBg.gameObject.SetActive(!isSpecial);
            mSpecBg.gameObject.SetActive(isSpecial);
        }
        private void SetRuneType(int runeType)
        {
            SetRuneType((EM_RUNE_TYPE)runeType);
        }

        private void SetRuneType(EM_RUNE_TYPE type)
        {
            int idx = -1;
            switch (type)
            {
                case EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID:
                    break;
                case EM_RUNE_TYPE.EM_RUNE_TYPE_BLUE:
                    idx = 0;
                    break;
                case EM_RUNE_TYPE.EM_RUNE_TYPE_PURPLE:
                    idx = 1;
                    break;
                case EM_RUNE_TYPE.EM_RUNE_TYPE_GREEN:
                    idx = 2;
                    break;
                case EM_RUNE_TYPE.EM_RUNE_TYPE_RED:
                    idx = 3;
                    break;
                case EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL:
                    break;
                case EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL_UNIQUE:
                    break;
                default:
                    break;
            }

            for (int i = 0; i < 4; i++)
            {
                mTypeObjs[i].SetActive(i == idx);
            }
        }
    }
}