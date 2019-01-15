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
using System;

namespace DreamFaction.UI
{
    public class UI_FightRewards : BaseUI
    {
        public static readonly string UI_ResPath = "UI_Level/UI_Rewards_2_5";

        public static UI_FightRewards _instance;
        private Button mCloseBtn;
        private Image mCloseImage;
        private Text mCloseText;
        private Transform mGrid;
        private UniversalItemCell m_Cell;
        private Text mHeader;
        private List<GameObject> objList = new List<GameObject>();
        List<int> innerdropIDList = new List<int>();
        public override void InitUIData()
        {
            base.InitUIData();
            _instance = this;
            mHeader = selfTransform.FindChild("Image/Text").GetComponent<Text>();
            mCloseBtn = selfTransform.FindChild("CloseBtn").GetComponent<Button>();
            mCloseImage = selfTransform.FindChild("CloseBtn").GetComponent<Image>();
            mCloseText = selfTransform.FindChild("CloseBtn/Text").GetComponent<Text>();
            mGrid = selfTransform.FindChild("ItemList/Grid");
            mCloseBtn.onClick.AddListener(OnClickBtn);

            UpdateShow();
        }

        //public bool isReward(int difficulttype, int rewardnum)
        //{
        //    int reward = 0;
        //    if (difficulttype == 1)
        //    {
        //        reward = rewardnum % 10;
        //    }
        //    else if (difficulttype == 2)
        //    {
        //        reward = rewardnum % 100 / 10;
        //    }
        //    else if (difficulttype == 3)
        //    {
        //        reward = rewardnum / 100;
        //    }
        //    return reward != 0;
        //}

        public void UpdateShow()
        {
            //Debug.Log(mGrid.transform.position);
            //mGrid.transform.position = new Vector3(-225.4f, -4.7f, -263.4f);
            //与服务器交互显示所得物品
            //UI_SelectFightArea sfight = UI_SelectFightArea.Inst;
            int chapterId = ObjectSelf.GetInstance().GetCurChapterID();
            int difficultLv = ObjectSelf.GetInstance().CurChapterLevel;
            ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(chapterId);
            if (chapterT != null)
            {
                int total = 0;
                int curstart = 0;

                StageModule.GetCurTotalStarsCount(chapterT, (EM_STAGE_DIFFICULTTYPE)difficultLv, out curstart, out total);

                if (curstart >= total)
                {
                    //TODO 宝箱弹窗
                    //if (isReward(difficultLv, sfight.iTotalCharpter[sfight.iChapterID].m_bRewardGot))
                    if (StageModule.isReward(difficultLv, ObjectSelf.GetInstance().BattleStageData.GetRewardGot(chapterId)))
                    {
                        mHeader.text = GameUtils.getString("fight_bosbox_content");
                        mCloseImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_xuanze");
                        //mCloseText.text = "关  闭";
                        mCloseText.text = GameUtils.getString("common_button_close");
                    }
                    else
                    {
                        mHeader.text = GameUtils.getString("sign_content5");
                        mCloseImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_xuanze");
                        //mCloseText.text = "领  取";
                        mCloseText.text = GameUtils.getString("common_button_receive");
                    }
                }
                else
                {
                    mHeader.text = GameUtils.getString("fight_bosbox_content");
                    mCloseImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_xuanze");
                    //mCloseText.text = "关  闭";
                    mCloseText.text = GameUtils.getString("common_button_close");
                }
                innerdropIDList.Clear();
                //int[] chapterDropList = ((ChapterinfoTemplate)DataTemplate.GetInstance().m_ChapterTable.getTableData(sfight.iChapterID)).getChapterDrop();
                //int chapterDropID = chapterDropList[difficultLv - 1];
                //int[] innerdropList = ((NormaldropTemplate)DataTemplate.GetInstance().m_NormaldropTable.getTableData(chapterDropID)).getInnerdrop();
                //Dictionary<int, IExcelBean> innerIExcel = DataTemplate.GetInstance().m_InnerdropTable.getData();
                ////List<InnerdropTemplate> innerdropIDList = new List<InnerdropTemplate>();
                //for (int i = 0; i < innerdropList.Length; i++)
                //{
                //    foreach (var item in innerIExcel.Values)
                //    {
                //        if (((InnerdropTemplate)item).getInnerdropid() == innerdropList[i])
                //        {
                //            innerdropIDList.Add((InnerdropTemplate)item);
                //        }
                //    }
                //}

                //ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(ObjectSelf.GetInstance().GetCurChapterID());
                //if (chapterT == null)
                //{
                //    LogSystem.LogManager.LogToFile("战斗奖励界面失败---ChapterinfoTemplate is NULL. id=" + chapterId);
                //    return;
                //}
                int dropId = difficultLv - 1;

                if (chapterT.getChapterDrop().Length <= 0)
                {
                    LogSystem.LogManager.LogToFile("战斗奖励界面失败---ChapterinfoTemplate 中章节掉落包数组是空的. id=" + chapterId);
                    return;
                }

                int chapterDropId = chapterT.getChapterDrop()[dropId];
                NormaldropTemplate normalT = DataTemplate.GetInstance().GetNormaldropTemplateById(chapterDropId);
                if (normalT == null)
                {
                    LogSystem.LogManager.LogToFile("战斗奖励界面失败---ChapterinfoTemplate is NULL. id=" + chapterDropId);
                    return;
                }
                int[] innerDropList = normalT.getInnerdrop();
                for (int i = 0, j = innerDropList.Length; i < j;  i++)
                {
                foreach (int k in DataTemplate.GetInstance().m_InnerdropTable.GetDataKeys())
                {
                    InnerdropTemplate _it = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(k);

                        if (_it == null) continue;

                        if (_it.getInnerdropid() == innerDropList[i])
                        {
                            innerdropIDList.Add(k);
                        }
                        
                    }
                }
            }
            //char[] sfightList = sfight.iTotalCharpter[sfight.iChapterID].m_bRewardGot.ToString().ToCharArray();
            else
            {
                mCloseImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_xuanze");
                //mCloseText.text = "关  闭";
                mCloseText.text = GameUtils.getString("common_button_close");
                for (int i = 0; i < UI_SacredAltar._instance.m_FallItemList.Count; i++)
                {
                    //InnerdropTemplate item = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(UI_SacredAltar._instance.m_FallItemList[i]);
                    //InnerdropTemplate item = DataTemplate.GetInstance().GetInnerdropTemplateById(UI_SacredAltar._instance.m_FallItemList[i]);
                    innerdropIDList.Add(UI_SacredAltar._instance.m_FallItemList[i]);
                } 
            }

            for (int i = 0; i < innerdropIDList.Count; i++)
            {
                m_Cell = UniversalItemCell.GenerateItem(mGrid.transform);

                InnerdropTemplate value = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(innerdropIDList[i]);
                if (value == null) return;
             
                int itemid = value.getObjectid();//掉落物ID
                int type = value.getObjectid() / 1000000;
                m_Cell.AddClickListener(ShowItemPreviewUIHandler);

                switch (type)
                {
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                        ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                        if (_temp_res != null)
                        {
                            m_Cell.InitByID(itemid, value.getDropnum());
                            m_Cell.SetText(GameUtils.getString(_temp_res.getName()), "", "");
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE: //符文
                        {
                            ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                            if (itemTable != null)
                            {
                                m_Cell.InitByID(itemid, -1);
                                m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                            }    
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                        {
                            ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                            if (itemTable != null)
                            {
                                m_Cell.InitByID(itemid, value.getDropnum());
                                m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                            }
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                        {
                            HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(itemid);
                            if (hero != null)
                            {
                                m_Cell.InitByID(itemid, value.getDropnum());
                                m_Cell.SetText(GameUtils.getString(hero.getTitleID()), "", "");
                            }
                        }
                        break;

                    default:
                        break;
                }
            } 
        }


        public static void ShowItemPreviewUIHandler(int tableID)
        {
            EM_OBJECT_CLASS eoc = GameUtils.GetObjectClassById(tableID);
            switch (eoc)
            {
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                    ItemTemplate runeItemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                    if (runeItemT == null)
                    {
                        LogManager.LogError("item表格中缺少物品id=" + tableID);
                    }
                    UI_RuneInfo.SetShowRuneDate(runeItemT);
                    UI_HomeControler.Inst.AddUI(UI_RuneInfo.UI_ResPath);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                    ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                    if (itemT == null)
                    {
                        LogManager.LogError("item表格中缺少物品id=" + tableID);
                    }
                    UI_Item.SetItemTemplate(itemT);
                    UI_HomeControler.Inst.AddUI(UI_Item.UI_ResPath);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN:
                    ArtresourceTemplate artT = DataTemplate.GetInstance().GetArtResourceTemplate(tableID);
                    if (artT == null)
                    {
                        LogManager.LogError("ArtResource时装表格中缺少物品id=" + tableID);
                    }
                    UI_SkinPreviewMgr.SetShowArtTemplate(artT);
                    UI_HomeControler.Inst.AddUI(UI_SkinPreviewMgr.UI_ResPath);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                    HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(tableID);
                    if (heroT == null)
                    {
                        LogManager.LogError("hero表格中缺少物品id=" + tableID);
                    }
                    UI_HomeControler.Inst.AddUI(HeroInfoPop.UI_ResPath);
                    HeroInfoPop.inst.SetShowData(heroT);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                    //资源类型点击无响应;
                    break;
                default:
                    LogManager.LogError("未处理的商城物品预览类型");
                    break;
            }
        }
        public void OnClickBtn()
        {
            for (int i = 0; i < objList.Count; i++)
            {
                Destroy(objList[i]);

            }
            objList.Clear();
            //UI_SelectFightArea sfight = UI_SelectFightArea.Inst;
           // char[] sfightList = sfight.iTotalCharpter[sfight.iChapterID].m_bRewardGot.ToString().ToCharArray();
            int difficultLv = ObjectSelf.GetInstance().CurChapterLevel;
            int chapterId = ObjectSelf.GetInstance().GetCurChapterID();
            ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(chapterId);

            if (chapterT!=null)
            {
                int total = 0;
                int curstart = 0;

                StageModule.GetCurTotalStarsCount(chapterT, (EM_STAGE_DIFFICULTTYPE)difficultLv, out curstart, out total);
                if (total == curstart)
                {
                    //TODO 宝箱弹窗
                    //if (isReward(ObjectSelf.GetInstance().CurChapterLevel, sfight.iTotalCharpter[sfight.iChapterID].m_bRewardGot))
                    if (StageModule.isReward(ObjectSelf.GetInstance().CurChapterLevel, ObjectSelf.GetInstance().BattleStageData.GetRewardGot(chapterId)))
                    {
                        UI_HomeControler.Inst.ReMoveUI(gameObject);
                    }
                    else
                    {
                        //与服务器交互
                        CGetStageReward cStage = new CGetStageReward();
                        cStage.difficulttype = (byte)ObjectSelf.GetInstance().CurChapterLevel;
                        cStage.stageid = (byte)chapterId;
                        IOControler.GetInstance().SendProtocol(cStage);
                        UI_HomeControler.Inst.ReMoveUI(gameObject);
                    }
                }
                else
                {
                    UI_HomeControler.Inst.ReMoveUI(gameObject);
                }
            }
            else
            {
                UI_HomeControler.Inst.ReMoveUI(gameObject);
            }
        }
    }
}