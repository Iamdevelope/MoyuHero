using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using DG.Tweening;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;
public class UI_GiftMassage : BaseUI
{
    public List<GameObject> itemList = new List<GameObject>();
    public List<int> giftList;
    private Button closeBtn;
    public Transform mGrid;
    public override void InitUIData()
    {
        base.InitUIData();
        closeBtn = selfTransform.FindChild("UI_Btn_Close").GetComponent<Button>();
        closeBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnCloseClick));
    }
    
    public void GiftShows()
    {
       // GameEventDispatcher.Inst.dispatchEvent(GameEventID.KE_KnapsackUpdateShow);
        for (int i = 0; i < itemList.Count; i++)
        {
            Destroy(itemList[i]);

        }
        itemList.Clear();
        giftList = UI_ItemsManage._instance.giftIDList;
        for (int i = 0; i < giftList.Count; i++)
        {

            InnerdropTemplate inner = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(giftList[i]);
            int itemid= inner.getObjectid() / 1000000;
            switch (itemid)
            {
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                    if (itemid == 1400)
                    {
                        GameObject item = Instantiate(Resources.Load("UI/Prefabs/UI_Bag/item"), transform.position, transform.rotation) as GameObject;
                        item.transform.parent = mGrid;
                        item.transform.localScale = Vector3.one;
                        UI_RewardsItemManage uifigt = item.GetComponent<UI_RewardsItemManage>();
                        ResourceindexTemplate itemT = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(inner.getObjectid());
                        uifigt.mName.text = GameUtils.getString(itemT.getName());
                        itemT.getIcon3();
                        uifigt.mIcon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + itemT.getIcon3());
                        uifigt.mNum.text = "x" + inner.getDropnum();
                        itemList.Add(item);

                    }
                    break;
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                    {
                        GameObject item = Instantiate(Resources.Load("UI/Prefabs/UI_Bag/rune"), transform.position, transform.rotation) as GameObject;
                        item.transform.parent = mGrid;
                        item.transform.localScale = Vector3.one;
                        ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(inner.getObjectid());
                        UI_RewardsItemManage uifigt = item.GetComponent<UI_RewardsItemManage>();
                        uifigt.mName.text = GameUtils.getString(itemTable.getName());
                        uifigt.typeNum = 1;
                        uifigt.id = inner.getObjectid();
                        uifigt.mIcon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + itemTable.getIcon());
                        uifigt.mNum.text = "x" + inner.getDropnum().ToString();
                        itemList.Add(item);
                    }
                    break;
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                    {
                        GameObject item = Instantiate(Resources.Load("UI/Prefabs/UI_Bag/item"), transform.position, transform.rotation) as GameObject;
                        item.transform.parent = mGrid;
                        item.transform.localScale = Vector3.one;
                        ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(inner.getObjectid());
                        UI_RewardsItemManage uifigt = item.GetComponent<UI_RewardsItemManage>();
                        uifigt.id = inner.getObjectid();
                        uifigt.typeNum = 2;
                        uifigt.mName.text = GameUtils.getString(itemTable.getName());
                        uifigt.mIcon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + itemTable.getIcon());
                        uifigt.mNum.text = "x" + inner.getDropnum().ToString();
                        itemList.Add(item);
                    }
                    break;
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                    {
                        GameObject item = Instantiate(Resources.Load("UI/Prefabs/UI_Bag/hero"), transform.position, transform.rotation) as GameObject;
                        item.transform.parent = mGrid;
                        item.transform.localScale = Vector3.one;
                        HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(inner.getObjectid());
                        UI_RewardsItemManage uihero = item.GetComponent<UI_RewardsItemManage>();
                        uihero.id = inner.getObjectid();
                        uihero.typeNum = 3;
                        uihero.mName.text = GameUtils.getString(hero.getTitleID());//"英雄";
                        ArtresourceTemplate art = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(hero.getArtresources());
                        uihero.mIcon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + art.getHeadiconresource());
                        int star = hero.getQuality();
                        int maxStar = hero.getMaxQuality();
                        for (int j = 0; j < 5; j++)
                        {
                            uihero.starList[j].SetActive(j < star);
                            uihero.starList[j + 5].SetActive(j < maxStar);
                        }

                        itemList.Add(item);
                    }
                    break;

                default:
                    break;
            }
            
        }

    }

    public void OnCloseClick()
    { 
        this.gameObject.SetActive(false);
        //GameEventDispatcher.Inst.dispatchEvent(GameEventID.KE_KnapsackAdd);
    }

}
