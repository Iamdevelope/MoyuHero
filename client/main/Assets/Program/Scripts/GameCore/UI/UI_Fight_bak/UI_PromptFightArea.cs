using UnityEngine;
using System;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using UnityEngine.UI;
using DG.Tweening;
namespace DreamFaction.UI
{
    public class UI_PromptFightArea : BaseUI
    {
        public static UI_PromptFightArea Inst;
        public static string UI_ResPath = "UI_Home/UI_PromptFightArea_2_1";
        private Image mLeftGray;                                                        //左边灰色前景图
        private Image mRightGray;                                                       //右边灰色前景图
        private Button GoNext_Btn;                                                      // 布阵按钮
        public Sprite mHadOpenItemSprite;                                               // 已开启的关卡图标
        public Sprite mCurOpenItemSprite;                                               // 当前开启的关卡图标
        public Sprite mHadNotOpenSprite;                                                // 未开启的关卡图标
        private int iStageID;                                                           // 当前关卡id
        private UI_StageInfo mStageInfo;                                                // 关卡信息页
        private List<int> mStageList = new List<int>();                                 // 章节关卡id列表
        public List<UI_StageItem> mStageItemList = new List<UI_StageItem>();            // 关卡UI列表
        private GameObject GoNextEffect = null;
        private Transform MsgBoxGroup;                                                       //消息父节点
        private long severTime;

        private Button m_BackBtn;

        public Color EnableTextColor;
        public Color DisableTextColor;
        private Text m_LevelText;
        private Text m_LeftUnOpenText;
        private Text m_RightUnOpenText;
        private Text m_LeftNameText;
        private Text m_RightNameText;
        private Transform m_CaptionPoint;
        public override void InitUIData()
        {
            base.InitUIData();
            Inst = this;
            m_CaptionPoint = selfTransform.FindChild("CaptionPoint");
            m_LevelText = selfTransform.FindChild("UI_Menu/ButtomLayer/Panel/LevelText").GetComponent<Text>();
            m_LeftUnOpenText = selfTransform.FindChild("UI_Menu/Left/NameBack/UnOpenText").GetComponent<Text>();
            m_RightUnOpenText = selfTransform.FindChild("UI_Menu/Right/NameBack/UnOpenText").GetComponent<Text>();
            m_LeftNameText = selfTransform.FindChild("UI_Menu/Left/Name").GetComponent<Text>();
            m_RightNameText = selfTransform.FindChild("UI_Menu/Right/Name").GetComponent<Text>();
            MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");
            GoNext_Btn = selfTransform.FindChild("UI_Menu/StartFightButton01").GetComponent<Button>();
            GoNext_Btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickGoNextBtn));
            severTime = ObjectSelf.GetInstance().ServerTime;
            mLeftGray = selfTransform.FindChild("UI_Menu/Left/LeftBg").GetComponent<Image>();
            mRightGray = selfTransform.FindChild("UI_Menu/Right/RightBg").GetComponent<Image>();
            mStageInfo = selfTransform.FindChild("UI_Menu/ButtomLayer/Panel").GetComponent<UI_StageInfo>();
            GoNextEffect = selfTransform.FindChild("UI_Menu/StartFightButton01/StartFightStar01").gameObject;
            m_BackBtn = selfTransform.FindChild("backBtn").GetComponent<Button>();
            m_BackBtn.onClick.AddListener(onBackCall);
            selfTransform.FindChild("PlayerInfoItem").GetComponent<UI_PlayerInfo>().mBackEvent = new UnityEngine.Events.UnityAction(onBackCall);
            UpdateShow();
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_FightNumSucceed, FightNumShow);
        }
        public override void InitUIView()
        {
            base.InitUIView();
            UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
            if (_caption != null)
                _caption.AwakeUp(m_CaptionPoint);
        }
        private void OnDestroy()
        {
            UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
            if (_caption != null)
                _caption.Release(m_CaptionPoint);

            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_FightNumSucceed, FightNumShow);
        }
        public void FightNumShow()
        {
            ChapterinfoTemplate levelInfo = (ChapterinfoTemplate)DataTemplate.GetInstance().m_ChapterTable.getTableData(1001);
            int[] levelID = levelInfo.getStageID();
            for (int i = 0; i < levelID.Length; i++)
            {
                if (levelID[i]==ObjectSelf.GetInstance().CurStageID)
                {
                    StageTemplate stage = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(levelID[i]);
                    mStageItemList[i].init(stage, i);
                }
                
            }
        }

        public void UpdateShow()
        {
            ChapterinfoTemplate levelInfo= (ChapterinfoTemplate)DataTemplate.GetInstance().m_ChapterTable.getTableData(1001);
            int[] levelID = levelInfo.getStageID();
            for (int i = 0; i < mStageItemList.Count; i++)
            {
                StageTemplate stage = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(levelID[i]);
                mStageItemList[i].init(stage, i);
            }
            int week = ObjectSelf.GetInstance().GetWeek();
            if (week==1)
            {
                GameUtils.SetImageGrayState(mLeftGray, false);
                GameUtils.SetImageGrayState(mRightGray, true);
                m_LeftUnOpenText.gameObject.SetActive(false); 
                m_RightUnOpenText.gameObject.SetActive(true);
                m_LeftNameText.color = EnableTextColor;
                m_RightNameText.color = DisableTextColor; 
                //mLeftGray.color = Color.white;
                //mRightGray.color = Color.gray;
                for (int i = 3; i < mStageItemList.Count; i++)
                {
                    mStageItemList[i].GetComponent<Button>().enabled = true;
                }
                for (int i = 0; i < 3; i++)
                {
                    mStageItemList[i].GetComponent<Button>().enabled = false;
                    UI_StageItem item = mStageItemList[i].GetComponent<UI_StageItem>();
                    item.mIcon.overrideSprite = Instantiate(mHadNotOpenSprite) as Sprite;
                    GameUtils.SetImageGrayState(item.mNameBox, true);
                    item.mlevel.color = Color.gray;
                    item.mStart0.gameObject.SetActive(false);
                    item.mStart1.gameObject.SetActive(false);
                    item.mStart2.gameObject.SetActive(false);
                    item.mLimit.SetActive(false);
                    item.mTag.SetActive(false);
                    
                }
                //mLeftGray.SetActive(false);
                //mRightGray.SetActive(true);
                if (ObjectSelf.GetInstance().GetPromptCurCampaignID() != -1)
                {
                    onStageSelect(ObjectSelf.GetInstance().GetPromptCurCampaignID());
                }
                else
                {
                    onStageSelect(levelID[3]);
                }

            }
            if (week == 2)
            {
                GameUtils.SetImageGrayState(mLeftGray, true);
                GameUtils.SetImageGrayState(mRightGray, false);
                m_LeftUnOpenText.gameObject.SetActive(true);
                m_RightUnOpenText.gameObject.SetActive(false);
                m_LeftNameText.color = DisableTextColor;
                m_RightNameText.color = EnableTextColor; 
                //mLeftGray.color = Color.gray;
                //mRightGray.color = Color.white;
                for (int i = 0; i < 3; i++)
                {

                    mStageItemList[i].GetComponent<Button>().enabled = true;
                }
                for (int i = 3; i < mStageItemList.Count; i++)
                {
                    
                    mStageItemList[i].GetComponent<Button>().enabled = false;
                    UI_StageItem item = mStageItemList[i].GetComponent<UI_StageItem>();
                    item.mIcon.overrideSprite = Instantiate(mHadNotOpenSprite) as Sprite;
                    item.mlevel.color = Color.gray;
                    GameUtils.SetImageGrayState(item.mNameBox, true);
                    item.mStart0.gameObject.SetActive(false);
                    item.mStart1.gameObject.SetActive(false);
                    item.mStart2.gameObject.SetActive(false);
                    item.mLimit.SetActive(false);
                    item.mTag.SetActive(false);
                }
                //mLeftGray.SetActive(true);
                //mRightGray.SetActive(false);
                if (ObjectSelf.GetInstance().GetPromptCurCampaignID() != -1)
                {
                    onStageSelect(ObjectSelf.GetInstance().GetPromptCurCampaignID());
                }
                else
                {
                    onStageSelect(levelID[0]);
                }
            }
            if (week==3)
            {
                for (int i = 0; i < 3; i++)
                {
                    mStageItemList[i].GetComponent<Button>().enabled = true;
                }
                for (int i = 3; i < mStageItemList.Count; i++)
                {
                    mStageItemList[i].GetComponent<Button>().enabled = true;
                }
                GameUtils.SetImageGrayState(mLeftGray,true);
                GameUtils.SetImageGrayState(mRightGray,true);
                m_LeftUnOpenText.gameObject.SetActive(true);
                m_RightUnOpenText.gameObject.SetActive(true);
                m_LeftNameText.color = DisableTextColor;
                m_RightNameText.color = DisableTextColor; 
               // mLeftGray.color = Color.white;
               // mRightGray.color = Color.white;
                //mLeftGray.SetActive(true);
                //mRightGray.SetActive(true);
                 if (ObjectSelf.GetInstance().GetPromptCurCampaignID() != -1)
                {
                    onStageSelect(ObjectSelf.GetInstance().GetPromptCurCampaignID());
                }
                else
                {
                    onStageSelect(levelID[3]);
                }
            }
         
            
        }

        public void onStageSelect(int id)
        {
            //if (iStageID == id) return;

            for (int idx = 0; idx < mStageItemList.Count; idx++)
            {
                if (mStageItemList[idx].transform.parent && mStageItemList[idx].iStageID == iStageID)
                {
                    mStageItemList[idx].unSelect();
                    break;
                }
            }

            for (int idx = 0; idx < mStageItemList.Count; idx++)
            {
                if (mStageItemList[idx].transform.parent && mStageItemList[idx].iStageID == id)
                {
                    mStageItemList[idx].onSelect();
                    m_LevelText.text = mStageItemList[idx].mlevel.text;
                    break;
                }
            }

            iStageID = id;
            ObjectSelf.GetInstance().CurStageID = iStageID;
            mStageInfo.setData(iStageID);
            mStageInfo.SetGoodsItem(iStageID);
        }
        private void OnClickGoNextBtn()
        {
            GoNextEffect.gameObject.SetActive(true);
            ObjectSelf.GetInstance().SetPromptNum(ObjectSelf.GetInstance().GetWeek());
            StartCoroutine(GoFight());  
        }

        IEnumerator GoFight()
        {
            yield return new WaitForSeconds(0.2f);
            UI_Form.UI_ResPath = "UI_Home/UI_ReadyToFight_2_2";
            UI_HomeControler.Inst.AddUI(UI_Form.UI_ResPath);
            GoNextEffect.gameObject.SetActive(false);
            //标记由限时关卡进入战斗
            ObjectSelf.GetInstance().SetPromptFight(true);
        }

        public void onBackCall()
        {
            UI_HomeControler.Inst.ReMoveUI(UI_PromptFightArea.UI_ResPath);
        }
        public void AddMsgBox(string text)
        {
            DreamFaction.GameCore.InterfaceControler.GetInst().AddMsgBox(text);
        }
    }

}