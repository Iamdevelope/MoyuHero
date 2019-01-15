using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
public class UI_WordMap : BaseUI
{
    public static UI_WordMap _instance;
    public static string UI_ResPath = "UI_Home/UI_WorldMap_2_2";
    private Button returnBtn;
    public override void InitUIData()
    {
        base.InitUIData();
        _instance=this;
        returnBtn = selfTransform.FindChild("UI_Btn_Return").GetComponent<Button>();
        returnBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(ChapterShow));
        selfTransform.FindChild("PlayerInfoItem").GetComponent<UI_PlayerInfo>().mBackEvent = new UnityEngine.Events.UnityAction(onBackCall);
    }
    public override void OnReadyForClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
    public override void UpdateUIView()
    {
      
        if (UIState == UIStateEnum.PlayingEnterAnimation)
        {
            transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
            if (transform.localScale.x >= 1.0f)
            {
                UIState = UIStateEnum.PlayingEnterAnimationOver;
            }
        }
        else if (UIState == UIStateEnum.PlayingExitAnimation)
        {
            //Debug.Log(transform.position.x);
            transform.position += new Vector3(0.1f, 0.00f, 0.00f);
            if (transform.position.x > -200)
            {
                UIState = UIStateEnum.PlayingExitAnimationOver;
            }
        }
    }

    public void onBackCall()
    {
        UI_HomeControler.Inst.ReMoveUI(UI_WordMap.UI_ResPath);
    }
    public void ChapterShow()
    {

        //UI_HomeControler.Inst.AddUI(UI_SelectFightArea.UI_ResPath);
        //if (ObjectSelf.GetInstance().GetCurChapterID() > 0)
        //{
        //    //Debug.Log("按钮缩小为原来" +( ObjectSelf.GetInstance().GetCurChapterID() - 1));
        //    UI_WorldMapManage._instance.chapterList[ObjectSelf.GetInstance().GetCurChapterID() - 1].transform.localScale = Vector3.one;
        //}
        //// UIState = UIStateEnum.PlayingExitAnimation;

        //UI_HomeControler.Inst.ReMoveUI(gameObject); 

        UI_HomeControler.Inst.AddUI(UI_SelectLevelMgrNew.UI_ResPath);
        if (ObjectSelf.GetInstance().GetCurChapterID() > 0)
        {
            UI_SelectLevelMgrNew.InitChapterId = ObjectSelf.GetInstance().GetCurChapterID();
            //Debug.Log("按钮缩小为原来" +( ObjectSelf.GetInstance().GetCurChapterID() - 1));
            UI_WorldMapManage._instance.chapterList[ObjectSelf.GetInstance().GetCurChapterID() - 1].transform.localScale = Vector3.one;
        }
        // UIState = UIStateEnum.PlayingExitAnimation;

        UI_HomeControler.Inst.ReMoveUI(gameObject);
       
        
    }

   
}
