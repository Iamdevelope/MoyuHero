using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    /// <summary>
    /// 关卡选择关卡信息，继承自BaseUI
    /// </summary>
    public class UI_StageInfo : BaseUI
    {
        public static UI_StageInfo _instance;
        private int iStageID;       // 关卡id

        public Text mStageDescription;  // 关卡描述
        public Text mConsumePower;  // 体力消耗
        public Text mRewardGold;    // 奖励金钱
        public Text mRewardExp;     // 奖励经验
        public Text mRewarExpMoney;
        public GameObject expMoney;
        public Transform mGoodsItemGroup;
        public List<GameObject> objList = new List<GameObject>();
        public GameObject Tips;
        public Text TipsDes;
        public override void InitUIData()
        {
            base.InitUIData();
            _instance = this;
        }
        public void setData(int iStageId)
        {
            iStageID = iStageId;

            mConsumePower.text = "0";
            mRewardGold.text = "0";
            mRewardExp.text = "0";

            //StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(iStageID);
            StageTemplate stageinfo = StageModule.GetStageTemplateById(iStageID);
            //if (list.ContainsKey(iStageId))
            {
                //StageTemplate stageinfo = (StageTemplate)list[iStageID];
                if (stageinfo != null)
                {
                    mStageDescription.text = GameUtils.getString(stageinfo.m_stageinfo);
                    mConsumePower.text = stageinfo.m_cost.ToString();
                    mRewardGold.text = stageinfo.m_goldreward.ToString();
                    mRewardExp.text = stageinfo.m_heroexp.ToString();
                    if (expMoney!=null)
                    {
                        if (stageinfo.m_expcrystal == -1)
                        {
                            expMoney.SetActive(false);
                        }
                        else
                        {
                            expMoney.SetActive(true);
                            mRewarExpMoney.text = stageinfo.m_expcrystal.ToString();

                        }
                    }
                }
            }
        }
        public void SetGoodsItem(int iStageId)
        {
            for (int i = 0; i < objList.Count; i++)
            {
                Destroy(objList[i]);
            }
            objList.Clear();
            int count = mGoodsItemGroup.childCount;
            for (int i = 0; i < count; ++i)
            {
                Destroy(mGoodsItemGroup.GetChild(i).gameObject);
            }
            iStageID = iStageId;
            //StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(iStageID);
            StageTemplate stageinfo = StageModule.GetStageTemplateById(iStageID);
            ////if (list.ContainsKey(iStageId))
            //{
            //    //StageTemplate stageinfo = (StageTemplate)list[iStageID];
            //    if (stageinfo != null)
            //    {
            //        Dictionary<string, int> temp = stageinfo.getDisplaydrop();
            //        foreach (var info in temp)
            //        {
            //            GameObject gooditem = Instantiate(Resources.Load("UI/Prefabs/UI_Fight/UI_LGoodItem")) as GameObject;
            //            gooditem.transform.SetParent(mGoodsItemGroup, false);
            //            gooditem.AddComponent<UI_GoodItem>().InitFightAreaGoodItem(info.Key, info.Value);
            //        }
            //    }
            //}
            string displaydrop = stageinfo.m_displaydrop;
            if (displaydrop == "-1" || string.IsNullOrEmpty(displaydrop))
            {

            }
            else
            {
                string[] displaydropList = displaydrop.Split('#');
                if (displaydropList.Length == 0)
                    return;

                for (int i = 0; i < displaydropList.Length; i++)
                {
                    string[] itemList = displaydropList[i].Split('-');
                    switch (int.Parse(itemList[0]))
                    {
                        case 1:
                            int inner = int.Parse(itemList[1]);
                            int itemid = inner / 1000000;
                            switch (itemid)
                            {
                                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                                    {
                                        GameObject item = Instantiate(Resources.Load("UI/Prefabs/UI_Fight/UI_LGoodRune"), transform.position, transform.rotation) as GameObject;
                                        item.transform.SetParent(mGoodsItemGroup, false);
                                        //item.transform.parent = mGoodsItemGroup;
                                        //item.transform.localScale = Vector3.one;
                                        //ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(inner);
                                        ItemTemplate itemTable = DataTemplate.GetInstance().GetItemTemplateById(inner);
                                        UI_DropFightItemManage uifigt = item.GetComponent<UI_DropFightItemManage>();
                                        uifigt.typeNum = 1;
                                        uifigt.id = inner;
                                        uifigt.mIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + itemTable.getIcon());
                                        uifigt.mIcon.SetNativeSize();
                                        if (int.Parse(itemList[2]) == 0)
                                        {
                                            uifigt.mProbability.SetActive(true);
                                            uifigt.mNumObj.SetActive(false);
                                        }
                                        else
                                        {
                                            uifigt.mProbability.SetActive(false);
                                            uifigt.mNumObj.SetActive(true);
                                            uifigt.mNum.text = "x" + itemList[3];
                                        }

                                        objList.Add(item);
                                    }
                                    break;
                                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                                    {
                                        GameObject item = Instantiate(Resources.Load("UI/Prefabs/UI_Fight/UI_LGoodItem"), transform.position, transform.rotation) as GameObject;
                                        item.transform.SetParent(mGoodsItemGroup, false);
                                        //item.transform.parent = mGoodsItemGroup;
                                        //item.transform.localScale = Vector3.one;
                                        //ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(inner);
                                        ItemTemplate itemTable = DataTemplate.GetInstance().GetItemTemplateById(inner);
                                        UI_DropFightItemManage uifigt = item.GetComponent<UI_DropFightItemManage>();
                                        uifigt.id = inner;
                                        uifigt.typeNum = 2;
                                        uifigt.mIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + itemTable.getIcon());
                                        uifigt.mIcon.SetNativeSize();
                                        if (int.Parse(itemList[2]) == 0)
                                        {
                                            uifigt.mProbability.SetActive(true);
                                            uifigt.mNumObj.SetActive(false);
                                        }
                                        else
                                        {
                                            uifigt.mProbability.SetActive(false);
                                            uifigt.mNumObj.SetActive(true);
                                            uifigt.mNum.text = "x" + itemList[3];
                                        }
                                        objList.Add(item);
                                    }
                                    break;
                                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                                    {
                                        GameObject item = Instantiate(Resources.Load("UI/Prefabs/UI_Fight/UI_LGoodHero"), transform.position, transform.rotation) as GameObject;
                                        item.transform.SetParent(mGoodsItemGroup, false);
                                        //item.transform.parent = mGoodsItemGroup;
                                        //item.transform.localScale = Vector3.one;
                                        //HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(inner);
                                        HeroTemplate hero = DataTemplate.GetInstance().GetHeroTemplateById(inner);
                                        UI_DropFightItemManage uihero = item.GetComponent<UI_DropFightItemManage>();
                                        uihero.id = inner;
                                        uihero.typeNum = 3;
                                        //ArtresourceTemplate art = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(hero.getArtresources());
                                        ArtresourceTemplate art = DataTemplate.GetInstance().GetArtResourceTemplate(hero.getArtresources());
                                        uihero.mIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + art.getHeadiconresource());
                                        uihero.mIcon.SetNativeSize();
                                        int star = hero.getQuality();
                                        int maxStar = hero.getMaxQuality();

                                        for (int j = 0; j < 5;j++ )
                                        {
                                            uihero.starList[j].SetActive(j<star);
                                            uihero.starList[j + 5].SetActive(j < maxStar);
                                        }
                                        //for (int j = 0; j < star; j++)
                                        //{
                                        //    uihero.starList[j].SetActive(true);
                                        //}
                                        //for (int k = star; k < 5; k++)
                                        //{
                                        //    uihero.starList[k].SetActive(false);
                                        //}
                                        if (int.Parse(itemList[2]) == 0)
                                        {
                                            uihero.mProbability.SetActive(true);
                                        }
                                        else
                                        {
                                            uihero.mProbability.SetActive(false);
                                        }
                                        objList.Add(item);
                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;
                        case 2:
                            GameObject items = Instantiate(Resources.Load("UI/Prefabs/UI_Fight/UI_LGoodItem"), transform.position, transform.rotation) as GameObject;
                            items.transform.SetParent(mGoodsItemGroup, false);
                            //items.transform.parent = mGoodsItemGroup;
                            //items.transform.localScale = Vector3.one;
                            UI_DropFightItemManage uiitems = items.GetComponent<UI_DropFightItemManage>();
                            uiitems.id = 2;
                            uiitems.mIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + itemList[1]);
                            uiitems.mIcon.SetNativeSize();
                            if (int.Parse(itemList[2]) == 0)
                            {
                                uiitems.mProbability.SetActive(true);
                                uiitems.mNumObj.SetActive(false);
                            }
                            else
                            {
                                uiitems.mProbability.SetActive(false);
                                uiitems.mNumObj.SetActive(true);
                                uiitems.mNum.text = "x" + itemList[3];
                            }
                            if (itemList[3] == "0")
                            {
                                uiitems.mTipsText = null;
                            }
                            else
                            {
                                uiitems.mTipsText = itemList[3];
                            }
                            objList.Add(items);

                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public override void InitUIView()
        {
            
        }
    }
}
