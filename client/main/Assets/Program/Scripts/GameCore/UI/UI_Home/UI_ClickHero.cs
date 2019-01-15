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
    public class UI_ClickHero : BaseUI
    {
        private GameObject DataParent;                                     //用于隐藏信息
        private GameObject m_NullStateOBJ = null;
        public Transform Eff_Trans;                                         //显示特效
        private GameObject m_Eff = null;
        private GameObject m_EffNull = null;
        private Text HeroName;                                             //名字
        private Transform HeroLevel;                                       //等级
        private ObjectCard objhero;                                        //当前在该位置英雄信息
        public int FormationNum;                                           //几号位
        public int FormationType;                                          //阵型类型
        public int ClickType;                                              //该点击点是前排还是后排0是前排,1是后排
        private HeroTemplate HeroData;


        public override void InitUIData()
        {

            Eff_Trans = GameObject.Find("TeamViewRoom").transform.FindChild("Team1").GetChild(FormationNum - 1).gameObject.transform;
            m_Eff = Eff_Trans.GetChild(0).gameObject;
            m_EffNull = Eff_Trans.GetChild(1).gameObject;


            DataParent = selfTransform.FindChild("Data").gameObject;
            m_NullStateOBJ = selfTransform.FindChild("NullStateOBJ").gameObject;
            HeroName = DataParent.transform.FindChild("Name_txt").GetComponent<Text>();
            HeroLevel = DataParent.transform.FindChild("Level_txt");

            selfTransform.FindChild("Data/HeroBrn").GetComponent<Button>().onClick.AddListener(new UnityAction(OnClickHero));
            selfTransform.FindChild("NullStateOBJ/NullBtn").GetComponent<Button>().onClick.AddListener(new UnityAction(OnClickHero));

        }

        public void InitData(ObjectCard hero)
        {
            objhero = hero;
            HeroData = objhero.GetHeroRow();

            DataParent.SetActive(true);
            m_NullStateOBJ.SetActive(false);

            HeroName.text = string.Format(GameUtils.GetHeroNameFontColor(hero.GetHeroData().QualityLev), GameUtils.getString(HeroData.getTitleID()));            
            InterfaceControler.AddLevelNum(hero.GetHeroData().Level.ToString(), HeroLevel);
            int star = hero.GetHeroData().StarLevel;
            Transform BrightStar = DataParent.transform.FindChild("HeroStar/BrightStar");
            for (int i = 0; i < BrightStar.childCount; ++i)
            {
                BrightStar.transform.GetChild(i).GetComponent<Image>().enabled = (i < star);
            }
            //InterfaceControler.GetInst().AddSharLevel(DataParent.transform.FindChild("Star_Image"),HeroData);

            //如果是远程英雄
            if (ClickType == 1)
            {
                int group = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                X_GUID guid = ObjectSelf.GetInstance().Teams.m_Matrix[group, FormationNum - 1];
                UI_FormMgr.Inst.SetBackHeroGuids(guid.GUID_value);
            }
        }
        public void OnClearObj()
        {
            objhero = null;
            DataParent.SetActive(false);
        }

        /// <summary>
        /// 设置当前特效显示状态
        /// </summary>
        /// <param name="active"></param>
        public void SetEffectActive(bool active)
        {
            m_Eff.SetActive(active);
            m_EffNull.SetActive(!active);
        }

        //点击当前模型
        private void OnClickHero()
        {
            UI_FormMgr.Inst.onClose();

            if (objhero != null)
            {
                GameObject go = UI_HomeControler.Inst.AddUI(UI_RepartoMgr.UI_ResPath);
                UI_RepartoMgr uiRepartoMgr = go.GetComponent<UI_RepartoMgr>();
                uiRepartoMgr.SetSelectHeoData(objhero, ClickType, FormationNum);
            }
            else
            {
                GameObject go = UI_HomeControler.Inst.AddUI(UI_SelectHeroMgr.UI_ResPath);
                UI_SelectHeroMgr uiSelectHeroMgr = go.GetComponent<UI_SelectHeroMgr>();
                uiSelectHeroMgr.SetSelectHeoData(null, ClickType, FormationNum);
            }
        }

    }
}

