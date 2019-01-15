using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using UnityEngine.UI;

public class UI_WorldMapManage : BaseUI
{
    public static UI_WorldMapManage _instance;

    public List<GameObject> chapterList = new List<GameObject>();


    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;
        for (int i = 0; i < chapterList.Count; i++)
        {
            chapterList[i].GetComponent<Button>().enabled = false;
        }
        
    }
    
    public void WorldMapShow()
    {
     
        Dictionary<int, BattleStage>.KeyCollection keycol = ObjectSelf.GetInstance().BattleStageData.m_BattleStageList.Keys;
        
        foreach (int key in keycol)
        {
            if (key!=1001)
            {
                //Debug.Log("Map_2Key=" + key);
                GameObject obj = chapterList[key - 1];
                obj.GetComponent<Button>().enabled = true;
                obj.transform.FindChild("Image").gameObject.SetActive(true);
            }
            
        }
        
            chapterList[ObjectSelf.GetInstance().GetCurChapterID()-1].transform.localScale = Vector3.one * 1.5f;
        
       
    }

    
}
