//using UnityEngine;
//using UnityEngine.UI;
//using DreamFaction.UI.Core;
//using System.Collections;
//using System.Collections.Generic;
//using GNET;
//using DreamFaction.Utils;
//using DreamFaction.GameNetWork;
//using DG.Tweening;
//using DreamFaction.LogSystem;
//using DreamFaction.UI;
//public class AwareIconItem : CellItem
//{

//    private Button m_Btn;//自身

//    private Image m_SpriteImage; // 物品圖片
//    private Text m_NameText;
//    private Text m_NumText;//數量

//    public TableReader m_RunecostTable;

//    InnerdropTemplate item = new InnerdropTemplate();

//    public override void InitUIData()
//    {
//        base.InitUIData();

//        m_SpriteImage = selfTransform.FindChild("Good_Image").GetComponent<Image>();
//        m_NameText = selfTransform.FindChild("nameText").GetComponent<Text>();
//        m_NumText = selfTransform.FindChild("BG_Image/Num").GetComponent<Text>();
        

//        //IsReceiveImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Shenqi_04", typeof(Sprite)) as Sprite;
//        //m_ReceivedText.text = GameUtils.getString("sign_content4");
//    }
//    public void SetIconDate(int _key, GameactivityTemplate _GameactivityTemplate)
//    {

//        int[] dropdestypeArray = _GameactivityTemplate.getDropdestype();
//        string[] DropdesArray = _GameactivityTemplate.getDropdes().Split('#');
//        int[] numdesArray = _GameactivityTemplate.getNumdes(); ;
//        string[] TextdesArray = _GameactivityTemplate.getTextdes().Split('#');

//        if (numdesArray.Length != 0)
//        {
//            if (numdesArray[0] != -1)
//            {
//                m_NumText.text = "x" + numdesArray[_key];
//            }
            
//        }

//        if (dropdestypeArray[_key] == 1)
//        {
//            int id = int.Parse(DropdesArray[_key]);
//            //InnerdropTemplate item = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(id);

//            int itemid = id / 1000000;

//            switch (itemid)
//            {
//                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES: //资源
//                    ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(id);
//                    if (_temp_res != null)
//                    {
//                        string _tempIconNam_1 = _temp_res.getIcon3();
//                        m_NameText.text = GameUtils.getString(_temp_res.getName());
//                        m_SpriteImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_1);
//                    }
//                    break;
//                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE://符文
//                    ItemTemplate _temp_rune = (ItemTemplate)DataTemplate.GetInstance().GetItemTemplateById(id);
//                    if (_temp_rune != null)
//                    {
//                        string _tempIconNam_2 = _temp_rune.getDes();
//                        m_NameText.text = GameUtils.getString(_temp_rune.getName());
//                        m_SpriteImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_2);
//                    }
//                    break;
//                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON: //道具
//                    ItemTemplate _temp_common = (ItemTemplate)DataTemplate.GetInstance().GetItemTemplateById(id);
//                    if (_temp_common != null)
//                    {
//                        string _tempIconNam_3 = _temp_common.getIcon_s();
//                        m_NameText.text = GameUtils.getString(_temp_common.getName());
//                        m_SpriteImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_3);
//                    }
//                    break;
//                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO: //英雄
//                    HeroTemplate _temp_hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(id);
//                    if (_temp_hero != null)
//                    {
//                        int _tempIconNam_4 = _temp_hero.getArtresources();
//                        m_NameText.text = GameUtils.getString(_temp_hero.getNameID());
//                        ArtresourceTemplate _temp_Art = (ArtresourceTemplate)DataTemplate.GetInstance().GetArtResourceTemplate(_tempIconNam_4);
//                        string _tempIconNam_5 = _temp_Art.getHeadiconresource();
//                        m_SpriteImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_5);
//                    }
//                    break;
//                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN: //皮肤
//                    ArtresourceTemplate _temp_Art_2 = (ArtresourceTemplate)DataTemplate.GetInstance().GetArtResourceTemplate(id);
//                    if (_temp_Art_2 != null)
//                    {
//                        m_NameText.text = GameUtils.getString(_temp_Art_2.getNameID());
//                        string _tempIconNam_6 = _temp_Art_2.getHeadiconresource();
//                        m_SpriteImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_6);
//                    }
//                    break;
//            }
//        }
//        if (dropdestypeArray[_key] == 0)
//        {
//            m_NameText.text = GameUtils.getString(TextdesArray[_key]);
//            m_SpriteImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + TextdesArray[_key]);
//        }
//    }
//}
