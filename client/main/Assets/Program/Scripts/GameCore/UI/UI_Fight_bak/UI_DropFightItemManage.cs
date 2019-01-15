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
    public class UI_DropFightItemManage : BaseUI
    {
        public int id;
        public GameObject mProbability;
        public Image mIcon;
        public Text mNum;
        public GameObject mNumObj;
        public List<GameObject> starList = new List<GameObject>();
        public int typeNum;//类型 1为符文  2为道具 3为英雄
        public Button mSelf;
        public GameObject mTips;
        public string mTipsText;
        public override void InitUIData()
        {
            base.InitUIData();
            mSelf = transform.GetComponent<Button>();
            mSelf.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickMySelf));
        }

        public override void UpdateUIData()
        {
            base.UpdateUIData();
            if (mTips!=null)
            {
                if (mTips.activeSelf)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        mTips.SetActive(false);
                        UI_StageInfo._instance.Tips.SetActive(false);
                    }
                }
            }
        }

        public void OnClickMySelf()
        {
            if (id==2)
            {
                if (mTipsText==null)
                {
                    mTips.SetActive(false);
                    UI_StageInfo._instance.Tips.SetActive(false);
                }
                else
                {
                    mTips.SetActive(true);
                    UI_StageInfo._instance.Tips.SetActive(true);
                    UI_StageInfo._instance.TipsDes.text = GameUtils.getString(mTipsText);
                }
            }
            else
            {
                switch (typeNum)
                {
                    //符文
                    case 1:
                        
                        ItemTemplate rune = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(id);
                        UI_RuneInfo.SetShowRuneDate(rune);
                        UI_HomeControler.Inst.AddUI("UI_Rune/UI_RuneInfo_1_3");
                        break;
                    //道具
                    case 2:
                        ItemTemplate item = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(id);
                        UI_Item.SetItemTemplate(item);
                        UI_HomeControler.Inst.AddUI("UI_Home/UI_Item_1_3");


                        break;
                    //英雄
                    case 3:
                        UI_HomeControler.Inst.AddUI("UI_Home/UI_HeroInfoPop_1_3");
                        ObjectCard obj = new ObjectCard();
                        Hero hero = new Hero();
                        HeroTemplate _hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(id);
                        hero.skill1 = _hero.getSkill1ID();
                        hero.skill2 = _hero.getSkill2ID();
                        hero.skill3 = _hero.getSkill3ID();
                        hero.heroid = id;
                        hero.herolevel = 1;
                        hero.heroviewid = _hero.getArtresources();
                        obj.GetHeroData().Init(hero);
                        //Destroy(UI_SelectFightArea.Inst.Card3Dmodel);
                        HeroInfoPop.inst.Show3DModel(obj);
                        HeroInfoPop.inst.ShowInfo(obj);
                        break;
                    default:
                        break;
                }
            }
        }
     
    }
}
