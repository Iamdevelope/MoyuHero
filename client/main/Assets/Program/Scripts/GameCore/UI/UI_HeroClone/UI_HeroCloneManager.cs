using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using System.Collections.Generic;
using DreamFaction.UI;
using DreamFaction.Utils;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;

public class UI_HeroCloneManager :UI_HeroCloneManagerBase
{
    public static string UI_ResPath = "UI_HeroClone/UI_HeroClone_2_2";
    public static UI_HeroCloneManager Inst = null;

    private Animation m_HeroCloneAnim = null;
    private Animator m_MakAnim = null;                                                    
    private List<HerocloneTemplate> m_HeroCloneList = new List<HerocloneTemplate>();  //所有的克隆英雄数据
    private GameObject m_HeroCloneItemObj = null;                                     //克隆英雄UIItem的 Prrfab
    private string m_HeroCloneItemUrl = "UI_HeroClone/UI_HeroCloneItem";              //克隆英雄的Prrfab路径
    private int m_ConAssetId = 0;                                                     //克隆消耗资源id
    private int m_conCount = 0;                                                       //克隆消耗资源总个数
    private Transform m_Grid = null;                                                  //克隆英雄的列表Grid
    private Image m_ConAssetIcon = null;                                              //克隆消耗资源图标
    public bool isNotCos = false;                                                     //注条件
    protected LoopLayout m_HeroLayout;

    public override void InitUIData()
    {
        base.InitUIData();

        Inst = this;
        ParseHeroCloneXmlData();        
        m_Grid = selfTransform.FindChild("HeroList/ListLayOut");
        m_HeroLayout = selfTransform.FindChild("HeroList/ListLayOut").GetComponent<LoopLayout>();
        m_ConAssetIcon = selfTransform.FindChild("TopPanel/ConImg").GetComponent<Image>();
        m_MakAnim = selfTransform.FindChild("Msk").GetComponent<Animator>();
        GameObject _modelViewRoom = GameObject.Find("ModelViewRoom");
        m_HeroCloneAnim = _modelViewRoom.transform.FindChild("Camera2/ElementBody01/Playground Light/ElementBody01").GetComponent<Animation>();
//        m_HeroCloneAnim = GameObject.Find("ModelViewRoom/Camera2/ElementBody01/Playground Light/ElementBody01").GetComponent<Animation>();
        m_ConAssetId = m_HeroCloneList[0].getCloneCostId();
        m_HeroCloneItemObj = UIResourceMgr.LoadPrefab(common.prefabPath + m_HeroCloneItemUrl) as GameObject;
        HomeControler.Inst.PushFunly(7, 114);


        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.AwakeUp(M_CapPos);

        GameEventDispatcher.Inst.addEventListener(GameEventID.HE_HeroCloneInject,UpdateHeroCloneDataUI);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        UI_MainHome.m_CamClone.SetActive(true);
        m_MakAnim.gameObject.SetActive(false);
        SortListData();

        // load 
        m_HeroLayout.cellCount = m_HeroCloneList.Count;
        m_HeroLayout.updateCellEvent = UpdateHeroItem;
        m_HeroLayout.Reload();

        InitHeroCloneConAsset();
        if (m_conCount <= 0)
            isNotCos = true;
    }

    /// <summary>
    /// 排序 将已开启的排在前边  将为开启的使用默认排序
    /// </summary>
    private void SortListData()
    {
        List<HerocloneTemplate> _openList = new List<HerocloneTemplate>();
        List<HerocloneTemplate> _notOpenList = new List<HerocloneTemplate>();
        for (int i = 0; i < m_HeroCloneList.Count; i++)
        {
            if (ObjectSelf.GetInstance().IsGetTheHeroBlood(m_HeroCloneList[i].getId()) == true)
                _openList.Add(m_HeroCloneList[i]);
            else
                _notOpenList.Add(m_HeroCloneList[i]);
        }
        m_HeroCloneList.Clear();
        for (int j = 0; j < _openList.Count; j++)
        {
            m_HeroCloneList.Add(_openList[j]);
        }
        for (int k = 0; k < _notOpenList.Count; k++)
        {
            m_HeroCloneList.Add(_notOpenList[k]);
        }
        _openList.Clear();
        _notOpenList.Clear();
    }

    /// <summary>
    /// 初始化英雄克隆Item
    /// </summary>
    void UpdateHeroItem(int index, RectTransform cell)
    {
        if(index < m_HeroCloneList.Count)
        {
            UI_HeroCloneItem item = cell.gameObject.GetComponent<UI_HeroCloneItem>();
            if (item == null)
            {
                item = cell.gameObject.AddComponent<UI_HeroCloneItem>();
            }
            item.index = index;
            item.InitHeroCloneItemData(m_HeroCloneList[index]);
        }        
    }

    /// <summary>
    /// 初始化消耗品显示
    /// </summary>
    public int GetHeroCloneConAssetNum(int cosItemId)
    {
       int _cosCount = 0;
        List<BaseItem> _itemList = ObjectSelf.GetInstance().CommonItemContainer.GetItemList(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON);
        int _itemCount = _itemList.Count;
        for (int i = 0; i < _itemCount; ++i)
        {
            int _itemId = _itemList[i].GetItemTableID();
            if (_itemId == cosItemId)
            {
                int _tempNum = _itemList[i].GetItemCount();
                _cosCount += _tempNum;
            }
        }

        return _cosCount;
    }

    public void InitHeroCloneConAsset()
    {
        m_conCount = GetHeroCloneConAssetNum(m_ConAssetId);
        ItemTemplate _item = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(m_ConAssetId);
        m_ConAssetIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _item.getIcon());
        m_ConNUmTxt.text = m_conCount.ToString();
    }

    /// <summary>
    /// 用于模拟数值减少
    /// </summary>
    public void AnalogCos(int reduceNum)
    {
        if (m_conCount == 0)
        {
            isNotCos = true;
            return;
        }
        m_conCount --;
        m_ConNUmTxt.text = m_conCount.ToString();
    }

    /// <summary>
    /// 初始化英雄克隆数据
    /// </summary>
    private void ParseHeroCloneXmlData()
    {
        m_HeroCloneList.Clear();
        Dictionary<int, IExcelBean> _heroCloneXmlData = DataTemplate.GetInstance().m_HerocloneTable.getData();
        foreach (var item in _heroCloneXmlData)
        {
            HerocloneTemplate _heroCloneData = (HerocloneTemplate)DataTemplate.GetInstance().m_HerocloneTable.getTableData(item.Key);
            m_HeroCloneList.Add(_heroCloneData);
        }
    }

    /// <summary>
    /// 刷新消耗显示
    /// </summary>
    public void UpdateHeroCloneDataUI(GameEvent e)
    {
        m_MakAnim.gameObject.SetActive(true);
        m_MakAnim.SetBool("isMak",true);
        m_HeroCloneAnim.CrossFade("Boom1",0.3f);
        List<RectTransform> cellList = m_HeroLayout.cellList;
        for(int i =0; i < cellList.Count; ++i)
        {
            UI_HeroCloneItem item = cellList[i].GetComponent<UI_HeroCloneItem>();
            if(item.GetHeroId() == (int)e.data)
            {
                item.UpdateCosData();
            }
        }

        StartCoroutine(OnWaitAnimEnd(m_HeroCloneAnim,e));
    }
    /// <summary>
    /// 等待动画片段结束
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    private IEnumerator OnWaitAnimEnd(Animation anim,GameEvent e)
    {
        yield return new WaitForSeconds(anim.clip.length + 2f);
        if (anim != null)
        {
            anim.CrossFade("Idle1", 0.3f);            
        }
        //UI_HomeControler.Inst.ReMoveUI(gameObject);

        m_MakAnim.gameObject.SetActive(false);
        m_HeroCloneAnim["Idle1"].time = 0;

        ShowGetHeroUI(e.data);
    }

    /// <summary>
    /// 显示获得英雄弹窗
    /// </summary>
    private void ShowGetHeroUI(object heroId)
    {
        string _text = GameUtils.getString("clone_window1");
        UI_HomeControler.Inst.AddUI(HeroInfoPop.UI_ResPath);
        HeroTemplate _heroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData((int)heroId);
        //HeroInfoPop.inst.SetIsClone(true);
        HeroInfoPop.inst.SetShowData(_heroData);
        HeroInfoPop.inst.SetTitleText(_text);
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    protected override void OnClickbackBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
        UI_MainHome.m_CamClone.SetActive(false);
    }

    void OnDestroy()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.Release(M_CapPos);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_HeroCloneInject, UpdateHeroCloneDataUI);
    }






}
