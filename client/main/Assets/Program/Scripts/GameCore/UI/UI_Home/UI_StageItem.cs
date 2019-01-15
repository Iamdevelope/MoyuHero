using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    /// <summary>
    /// 关卡图标，继承自BaseUI
    /// </summary>
    public class UI_StageItem : BaseUI
    {
        [HideInInspector]
        public int iStageID;    // 关卡id
        public StageTemplate mStageInfo;
        [HideInInspector]
        public int iIdx;        // 当前编号
        public Image mNameBox;
        public Image mIcon;
        public Text mlevel;
        public Transform mStart0;
        public Transform mStart1;
        public Transform mStart2;
        public GameObject mTag;
        public GameObject m3dModel;
        public GameObject mLimit;
        public Text mLimitValue;
        public bool newFight = false;
        private bool isSelect = false;
        private bool isOpen = true; // 是否开启
        BattleStage list = null;
        public void init(StageTemplate stageinfo, int idx)
        {
            isSelect = false;
            isOpen = false;
            this.iStageID = stageinfo.m_stageid;
            this.iIdx = idx + 1;
            this.mStageInfo = stageinfo;

            var table = stageinfo;
            if (stageinfo.m_stagetype != 6)
            {
                if (stageinfo.m_stagetype != 7)
                {
                    GetComponent<RectTransform>().anchoredPosition3D = new Vector3(table.getStageiconposition()[0], table.getStageiconposition()[1], 0);
                    if (stageinfo.m_stagetype != 4)
                    {
                        mlevel.text = string.Format("{0}-{1}", ObjectSelf.GetInstance().GetCurChapterID(), iIdx);
                    }
                    else
                    {
                        mlevel.text = GameUtils.getString("stage_type_branch"); //"支线"
                    }
                }

            }
            if (stageinfo.m_stagetype == 6 || stageinfo.m_stagetype == 7)
            {
                // mlevel.text = GameUtils.getString(stageinfo.m_stagename);
            }

            var info = ObjectSelf.GetInstance();

            if (ObjectSelf.GetInstance().GetIsPrompt())
            {
                //list = info.BattleStageData.m_BattleStageList[1001];
                list = info.BattleStageData.GetBattleStageByChapterId(1001);
            }
            else
            {
                //if (info.BattleStageData.m_BattleStageList.ContainsKey(info.GetCurChapterID()))
                //{
                //    list = info.BattleStageData.m_BattleStageList[info.GetCurChapterID()];
                //}
                BattleStage bs = info.BattleStageData.GetBattleStageByChapterId(info.GetCurChapterID());
                if (bs != null)
                {
                    list = bs;
                }
            }
            if (transform.FindChild("limit") != null)
            {
                mLimit = transform.FindChild("limit").gameObject;
            }
            StageData data = list == null ? null : list.GetStageData(iStageID);
            mTag.SetActive(false);
            if (data != null)
            {
                isOpen = true;

                // 初始化
                switch (data.m_StageStar)
                {
                    case 0:
                        {
                            mStart0.gameObject.SetActive(false);
                            mStart1.gameObject.SetActive(false);
                            mStart2.gameObject.SetActive(false);
                            if (info.BattleStageData.isStageNew(this.iStageID))
                            {
                                mTag.SetActive(true);
                                newFight = true;
                            }
                        }
                        break;
                    case 1:
                        {
                            mStart0.gameObject.SetActive(true);
                            mStart1.gameObject.SetActive(false);
                            mStart2.gameObject.SetActive(false);
                        }
                        break;
                    case 2:
                        {
                            mStart0.gameObject.SetActive(false);
                            mStart1.gameObject.SetActive(true);
                            mStart2.gameObject.SetActive(false);
                        }
                        break;
                    case 3:
                        {
                            mStart0.gameObject.SetActive(false);
                            mStart1.gameObject.SetActive(false);
                            mStart2.gameObject.SetActive(true);
                        }
                        break;
                }
                // 开启
                //if (UI_SelectFightArea.Inst != null)
                //{
                //    mIcon.overrideSprite = Instantiate(UI_SelectFightArea.Inst.mHadOpenItemSprite) as Sprite;
                //    mlevel.color = Color.white;
                //    //selfTransform.FindChild("icon").GetComponent<Button>().enabled = true;

                //}
                //else
                {
                    transform.GetComponent<Button>().enabled = true;
                    mIcon.overrideSprite = Instantiate(UI_PromptFightArea.Inst.mHadOpenItemSprite) as Sprite;
                    mlevel.color = Color.white;
                }
                if (mLimit != null)
                {
                    mLimitValue = mLimit.transform.FindChild("value").GetComponent<Text>();
                    if (stageinfo.m_limittime == -1 || stageinfo.m_limittime - data.m_FightSum > 10)
                    {
                        mLimit.SetActive(false);
                    }
                    else
                    {
                        mLimit.SetActive(true);
                        mLimitValue.text = (stageinfo.m_limittime - data.m_FightSum).ToString();
                    }
                }
            }
            else
            {
                //GameObject.Destroy(gameObject);
                isOpen = false;
                mStart0.gameObject.SetActive(false);
                mStart1.gameObject.SetActive(false);
                mStart2.gameObject.SetActive(false);
                // 未开启
                //if (UI_SelectFightArea.Inst != null)
                //{
                //    mIcon.overrideSprite = Instantiate(UI_SelectFightArea.Inst.mHadNotOpenSprite) as Sprite;
                //    mlevel.color = Color.gray;

                //    //selfTransform.FindChild("icon").GetComponent<Button>().enabled = false;
                //}
                //else
                {
                    mIcon.overrideSprite = Instantiate(UI_PromptFightArea.Inst.mHadNotOpenSprite) as Sprite;
                    mlevel.color = Color.gray;
                    transform.GetComponent<Button>().enabled = false;
                }
                if (mLimit != null)
                {
                    mLimit.SetActive(false);
                }
            }
        }
        public override void InitUIView()
        {

        }
        public void onClick()
        {
            //if (isSelect) return;
            if (!isOpen)
            {
                if (mStageInfo.m_premissionid != -1)
                {
                    ObjectSelf.GetInstance().SetCurCampaignID(iStageID);
                    //StageTemplate newStage = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(mStageInfo.m_premissionid);
                    StageTemplate newStage = StageModule.GetStageTemplateById(mStageInfo.m_premissionid);
                    //需修改
                    if (ObjectSelf.GetInstance().GetIsPrompt())
                    {
                        UI_PromptFightArea.Inst.AddMsgBox(string.Format(GameUtils.getString("activitystage_tip1"), GameUtils.getString(newStage.m_stagename)));
                    }
                    else
                    {
                        string level = null;
                        switch (ObjectSelf.GetInstance().CurChapterLevel)
                        {
                            case 1:
                                level = GameUtils.getString("fight_stageselect_difficulty1");
                                break;
                            case 2:
                                level = GameUtils.getString("fight_stageselect_difficulty2");
                                break;
                            case 3:
                                level = GameUtils.getString("fight_stageselect_difficulty3");
                                break;
                            default:
                                break;
                        }

                        InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("fight_stageselect_tip3"), level, GameUtils.getString(newStage.m_stagename)));
                    }
                    //GameUtils.getString(newStage.m_stagename)
                }
            }
            else
            {
                ObjectSelf.GetInstance().SetCurCampaignID(iStageID);
                //if (UI_SelectFightArea.Inst != null)
                //{
                //    UI_SelectFightArea.Inst.onStageSelect(iStageID);
                //}
                //else
                {
                    UI_PromptFightArea.Inst.onStageSelect(iStageID);
                }
                if (mTag.activeSelf)
                {
                    mTag.SetActive(false);
                }

            }
        }

        public void onSelect()
        {
            // 播放选中效果
            isSelect = true;
            //transform.localScale = new Vector3(1.5f, 1.5f, 1);
            //Debug.Log("Current Select Level is " + iStageID);
            // 选中
            // var info = ObjectSelf.GetInstance();
            if (!newFight)
            {
                if (mTag.activeSelf)
                {
                    mTag.SetActive(false);
                }
            }
            //if (UI_SelectFightArea.Inst != null)
            //{
            //    mIcon.overrideSprite = Instantiate(UI_SelectFightArea.Inst.mCurOpenItemSprite) as Sprite;
            //}
            //else
            {
                mIcon.overrideSprite = Instantiate(UI_PromptFightArea.Inst.mCurOpenItemSprite) as Sprite;
            }
            if (m3dModel != null)
            {
                m3dModel.SetActive(true);
            }
            newFight = false;

        }

        public void unSelect()
        {
            // 取消选中
            isSelect = false;
            transform.localScale = new Vector3(1, 1, 1);


            // 开启
            //if (UI_SelectFightArea.Inst != null)
            //{
            //    mIcon.overrideSprite = Instantiate(UI_SelectFightArea.Inst.mHadOpenItemSprite) as Sprite;
            //}
            //else
            {
                mIcon.overrideSprite = Instantiate(UI_PromptFightArea.Inst.mHadOpenItemSprite) as Sprite;
            }
            if (m3dModel != null)
            {
                m3dModel.SetActive(false);
            }

        }
    }
}
