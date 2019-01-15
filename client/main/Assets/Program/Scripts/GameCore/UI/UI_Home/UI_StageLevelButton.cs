using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    public class UI_StageLevelButton : BaseUI
    {
        //public static UI_StageLevelButton _instance;
        //private Button mToggle;
        //private Text mText;
        //public bool isOpen;
        //public int ilevel = 1;
        //public bool isClick = false;
        //void Awake()
        //{
        //    _instance = this;
        //    mText = transform.FindChild("Label").GetComponent<Text>();
        //    mToggle = GetComponent<Button>();
        //    mToggle.onClick.AddListener(onValueChange);
        //}

        //void Start()
        //{
           
        //}

        //void onClickCall()
        //{

        //}

        //void onValueChange()
        //{
        //    if (isClick)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        switch (gameObject.name)
        //        {
        //            case "normal": ilevel = 1; break;
        //            case "hard": ilevel = 2; break;
        //            case "hardest": ilevel = 3; break;
        //        }
        //        for (int i = 0; i < UI_SelectFightArea.Inst.mLevelButtonsImage.Count; i++)
        //        {
        //            if (mToggle.transform.GetComponent<Image>() == UI_SelectFightArea.Inst.mLevelButtonsImage[i])
        //            {
        //                UI_SelectFightArea.Inst.mLevelButtonsImage[i].overrideSprite = Resources.Load(common.defaultPath + "UI_Guan_22", typeof(Sprite)) as Sprite;
        //                //UI_SelectFightArea.Inst.mLevelButtonsImage[i].transform.GetComponent<Button>().enabled = false;
        //                UI_SelectFightArea.Inst.mLevelButtons[i].isClick = true;
        //            }
        //            else
        //            {
        //                UI_SelectFightArea.Inst.mLevelButtonsImage[i].overrideSprite = Resources.Load(common.defaultPath + "UI_Guan_23", typeof(Sprite)) as Sprite;
        //               // UI_SelectFightArea.Inst.mLevelButtonsImage[i].transform.GetComponent<Button>().enabled = true;
        //                UI_SelectFightArea.Inst.mLevelButtons[i].isClick = false;
        //            }
        //        }
                
        //        if (isOpen)
        //        {
        //            UI_SelectFightArea.Inst.mScrollMap.enabled = false; ;
        //            UI_SelectFightArea.Inst.onLevelChange(ilevel);
        //        }
        //        else
        //        {
        //            UI_SelectFightArea.Inst.mLevelButtonsImage[UI_SelectFightArea.Inst.mCurlevel-1].overrideSprite = Resources.Load(common.defaultPath + "UI_Guan_22", typeof(Sprite)) as Sprite;
        //            //UI_SelectFightArea.Inst.mLevelButtonsImage[UI_SelectFightArea.Inst.mCurlevel - 1].transform.GetComponent<Button>().enabled = false;
        //            //UI_SelectFightArea.Inst.mLevelButtons[UI_SelectFightArea.Inst.mCurlevel - 1].isClick = true;
        //            mToggle.transform.GetComponent<Image>().overrideSprite = Resources.Load(common.defaultPath + "UI_Guan_23", typeof(Sprite)) as Sprite;
        //            //mToggle.transform.GetComponent<Button>().enabled = false;
        //            isClick = false;
        //            ChapterinfoTemplate stage = (ChapterinfoTemplate)DataTemplate.GetInstance().m_ChapterTable.getTableData(ObjectSelf.GetInstance().GetCurChapterID());
        //            string des = null;
        //            switch (ilevel)
        //            {
        //                case 2:
        //                    des = GameUtils.getString("fight_stageselect_tip1") + GameUtils.getString("fight_stageselect_difficulty1") + string.Format(GameUtils.getString("fight_stageselect_tip2"), GameUtils.getString(stage.getChapterName()));
        //                    break;
        //                case 3:
        //                    des = GameUtils.getString("fight_stageselect_tip1") + GameUtils.getString("fight_stageselect_difficulty2") + string.Format(GameUtils.getString("fight_stageselect_tip2"), GameUtils.getString(stage.getChapterName()));
        //                    break;
        //                default:
        //                    break;
        //            }
        //            InterfaceControler.GetInst().AddMsgBox(des, UI_SelectFightArea.Inst.GetMsgBoxGroup());
        //            // mToggle.isOn = false;
        //        }
        //    }
           
        //    //UI_SelectFightArea.Inst.mMenu.SetActive(false);
            
        //}

        //public void setUnable()
        //{
        //   mToggle.interactable = false;
        //   mText.color = mToggle.colors.disabledColor;
        //}
    }
}
