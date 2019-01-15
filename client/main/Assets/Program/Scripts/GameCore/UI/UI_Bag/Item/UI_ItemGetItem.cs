using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;

public class UI_ItemGetItem :CellItem {

    [SerializeField] Image m_Icon;
    [SerializeField] Text name,count;
    [SerializeField] GameObject[] m_Stars=new GameObject[5];
    private GameObject m_Rune;
    private GameObject m_Rune_Common;
    private GameObject m_Rune_Special;
    private Image m_Rune_Common_icon;
    private Image m_Rune_Special_icon;
    private GameObject[] m_RuneCommonType = new GameObject[4];
    public override void InitUIData()
    {
        base.InitUIData();
        m_Rune = transform.FindChild("rune").gameObject;
        m_Rune_Common = transform.FindChild("rune/putong").gameObject;
        m_Rune_Special = transform.FindChild("rune/teshu").gameObject;
        m_Rune_Common_icon = transform.FindChild("rune/putong/icon").GetComponent<Image>();
        m_Rune_Special_icon = transform.FindChild("rune/teshu/icon").GetComponent<Image>();
        m_RuneCommonType[0] = transform.FindChild("rune/putong/type_1").gameObject;
        m_RuneCommonType[1] = transform.FindChild("rune/putong/type_2").gameObject;
        m_RuneCommonType[2] = transform.FindChild("rune/putong/type_3").gameObject;
        m_RuneCommonType[3] = transform.FindChild("rune/putong/type_4").gameObject;
    }
    /// <summary>
    /// 填充数据
    /// </summary>
    /// <param name="id">掉落包小包id</param>
    public void FillData(int id)
    { 
        //通过id 区分出是资源 物品 英雄 还是其他
        //1400000001 - 1400999999	资源 对应数据表53
        //1401000001 - 1401999999	符文 对应数据表26
        //1402000001 - 1402999999	道具 对应数据表26
        //1403000001 - 1403999999	英雄 对应数据表01
       InnerdropTemplate innerTemplate=  DataTemplate.GetInstance().GetInnerdropTemplateById(id);
       if (innerTemplate == null)
       {
           Debug.LogError("Innerdrop表中没有对应的id:"+id);
           return;
       }
        int ItemId = innerTemplate.getObjectid();
        if (ItemId >= 1400000001 && ItemId <= 1400999999)//资源
        {
            ResourceindexTemplate template = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(ItemId);
            if(template!=null)
            {
                 m_Icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + template.getIcon1());
                 name.text = GameUtils.getString(template.getName());
                 count.text = "x" + innerTemplate.getDropnum().ToString();
                 m_Stars[0].transform.parent.gameObject.SetActive(false);
                 m_Rune.SetActive(false);
                 m_Icon.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError(string.Format("找不到id{0}的为物品", ItemId));
            }
        }
        else if ( ItemId >= 1402000001 && ItemId <= 1402999999)//道具
        {
            ItemTemplate template = DataTemplate.GetInstance().GetItemTemplateById(ItemId);
            if (template != null)
            {
                m_Icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + template.getIcon_s());
                name.text = GameUtils.getString(template.getName());
                count.text = "x" + innerTemplate.getDropnum().ToString();
                m_Stars[0].transform.parent.gameObject.SetActive(false);
                m_Rune.SetActive(false);
                m_Icon.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError(string.Format("找不到id{0}的为物品", ItemId));
            }
        }
       else if(ItemId >= 1401000001 && ItemId <= 1401999999) //符文
       {
           ItemTemplate template = DataTemplate.GetInstance().GetItemTemplateById(ItemId);
           if (template != null)
           {
               name.text = GameUtils.getString(template.getName());
               count.text = "x" + innerTemplate.getDropnum().ToString();
               m_Stars[0].transform.parent.gameObject.SetActive(true);
               m_Rune.SetActive(true);
               m_Icon.gameObject.SetActive(false);
               //星级
               for (int i = 0; i < m_Stars.Length; i++)
               {
                   if (template.getRune_quality() >= i + 1)
                   {
                       m_Stars[i].SetActive(true);
                   }
                   else
                   {
                       m_Stars[i].SetActive(false);
                   }
               }
               //设置普通符文的类型显示
               for (int i = 0; i < m_RuneCommonType.Length; i++)
               {
                   if (template.getRune_type() == i + 1)
                   {
                       m_RuneCommonType[i].SetActive(true);
                   }
                   else
                   {
                       m_RuneCommonType[i].SetActive(false);
                   }
               }
               //普通符文
               if (template.getRune_type() < 5)
               {
                   m_Rune_Common.SetActive(true);
                   m_Rune_Special.SetActive(false);
                   m_Rune_Common_icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + template.getIcon_s());
               }
               else //特殊符文
               {
                   m_Rune_Common.SetActive(false);
                   m_Rune_Special.SetActive(true);
                   m_Rune_Special_icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + template.getIcon_s());
               }
             
           }
           else
           {
               Debug.LogError(string.Format("找不到id{0}的为物品", ItemId));
           }
       }
        else if (ItemId >= 1403000001 && ItemId <= 1403999999) //英雄
        {
             HeroTemplate template =DataTemplate.GetInstance().GetHeroTemplateById(ItemId);
             ArtresourceTemplate artTemplate=DataTemplate.GetInstance().GetArtResourceTemplate(template.getArtresources());
             if (template != null)
             {
                 m_Icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + artTemplate.getHeadiconresource());
                 name.text = GameUtils.getString(template.getTitleID());
                 count.text = "x" + innerTemplate.getDropnum().ToString();
                 m_Stars[0].transform.parent.gameObject.SetActive(true);
                 m_Rune.SetActive(false);
                 m_Icon.gameObject.SetActive(true);
                 for (int i = 0; i < m_Stars.Length; i++)
                 {
                     if (template.getQuality() >= i + 1)
                     {
                         m_Stars[i].SetActive(true);
                     }
                     else
                     {
                         m_Stars[i].SetActive(false);
                     }
                 }
             }
             else
             {
                 Debug.LogError(string.Format("找不到id{0}的为物品", ItemId));
             }
        }
      
    
    }
}
