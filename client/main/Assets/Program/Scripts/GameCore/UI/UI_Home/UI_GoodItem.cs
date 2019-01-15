using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
namespace DreamFaction.UI
{
    public class UI_GoodItem : BaseUI
    {     
        private Image GoodImage;                                //掉落物品图片
        private Text GoodName;                                  //掉落物品名称
        private Text GoodNums;                                  //掉落物品数量  
        private GameObject Stars;                               //星级
        private GameObject Bg_Image;                            //战斗大地图掉落背景框
        private Text Num;                                       //战斗大地图掉落物品数量
        private GameObject Rand_Image;                          //战斗大地图掉落几率图片
        private Text CardDetailedDataName;                      //卡牌详细信息名称
        private Text CardDetailedDataBaseValue;                 //卡牌详细信息基础数值
        private Text CardDetailedDataBuffValue;                 //卡牌详细信息BUFF增益值
        private Button mSelfBtn;
        private ItemTemplate mItem;
        public override void InitUIData()
        {
            //mSelfBtn = transform.GetComponent<Button>();
            //if (mSelfBtn!=null)
            //{
            //    mSelfBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSelf));
            //}
            
        }
       
        //初始化掉落符文和道具(不叠加)
        public void InitRunes(ItemTemplate item)
        {
            mItem = item;
            GoodImage = selfTransform.FindChild("Good_Image").GetComponent<Image>();
            GoodName = selfTransform.FindChild("Good_Name").GetComponent<Text>();
            GoodNums = selfTransform.FindChild("Good_Nums").GetComponent<Text>();
            //mSelfBtn = transform.GetComponent<Button>();
            //mSelfBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSelf));
            Stars = selfTransform.FindChild("Star_Image").gameObject;
            Stars.SetActive(false);
            GoodNums.enabled = false;
            GoodName.enabled = false;
            //Debug.Log(item.getIcon());
            Sprite temp = UIResourceMgr.LoadSprite(common.defaultPath + item.getIcon());
            GoodImage.sprite = temp;
        }
        //初始化掉落符文和道具(叠加)
        public void InitRunes(ItemTemplate item,int Num)
        {
            mItem = item;
            GoodImage = selfTransform.FindChild("Good_Image").GetComponent<Image>();
            GoodName = selfTransform.FindChild("Good_Name").GetComponent<Text>();
            GoodNums = selfTransform.FindChild("Good_Nums").GetComponent<Text>();
            //mSelfBtn = transform.GetComponent<Button>();
            //mSelfBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSelf));
            Stars = selfTransform.FindChild("Star_Image").gameObject;
            Stars.SetActive(false);
            GoodName.enabled = false;
            if(Num==1)
            {
                GoodNums.enabled = false;
            }
            else
            {
                GoodNums.enabled = true;
                GoodNums.text = "X" + Num.ToString();
            }
            //Debug.Log(item.getIcon());
            Sprite _img = UIResourceMgr.LoadSprite(common.defaultPath + item.getIcon());
            GoodImage.sprite = _img;
        }
        //初始化掉落英雄
        public void InitHero(HeroTemplate _herodata)
        {
            GoodImage = selfTransform.FindChild("Good_Image").GetComponent<Image>();
            GoodName = selfTransform.FindChild("Good_Name").GetComponent<Text>();
            GoodNums = selfTransform.FindChild("Good_Nums").GetComponent<Text>();
            Stars = selfTransform.FindChild("Star_Image").gameObject;
            Stars.SetActive(true);
            GoodName.enabled = true;
            GoodNums.enabled = false;
            ArtresourceTemplate _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_herodata.getArtresources());
            Sprite _img = UIResourceMgr.LoadSprite(common.defaultPath + _Artresourcedata.getHeadiconresource());
            GoodImage.sprite = _img;
            ChsTextTemplate chs = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData(_herodata.getTitleID());
            GoodName.text = chs.languageMap["Chinese"];
            int starNum = _herodata.getQuality();
            int maxStar = _herodata.getMaxQuality();
            //for (int i = 5; i < 5 + starNum; ++i)
            //{
            //    Stars.transform.GetChild(i).GetComponent<Image>().enabled = true;
            //}
            for (int i = 0; i < 5; i++)
            {
                Stars.transform.GetChild(i).GetComponent<Image>().enabled = i < maxStar;
                Stars.transform.GetChild(i + 5).GetComponent<Image>().enabled = i < starNum;
            }
        }
        //显示战斗掉落物品
        public void InitFightAreaGoodItem(string name,int num)
        {
            GoodImage = selfTransform.FindChild("Good_Image").GetComponent<Image>();
            Bg_Image = selfTransform.FindChild("BG_Image").gameObject;
            Num = selfTransform.FindChild("BG_Image/Num").GetComponent<Text>();
            Rand_Image = selfTransform.FindChild("Rand_Image").gameObject;
            Sprite _img = UIResourceMgr.LoadSprite(common.defaultPath + name);
            GoodImage.sprite = _img;
            if (num > 0)
            {
                Rand_Image.SetActive(false);
                Bg_Image.SetActive(true);
                Num.text= "x"+num.ToString();
            }
            else
            {
                Rand_Image.SetActive(true);
                Bg_Image.SetActive(false);
            }
        }

        //public void OnClickSelf()
        //{
        //    UI_HomeControler.Inst.AddUI("UI_Home/UI_ItemMessage_2_2");
        //    UI_Item._instance.UpdateShow(mItem);

        //}

        //显示英雄信息界面详细信息界面
        public void InitCardDetailedData(int num,ObjectCard _card)
        {
            CardDetailedDataName = selfTransform.FindChild("Name").GetComponent<Text>();
            CardDetailedDataBaseValue = selfTransform.FindChild("BaseValue").GetComponent<Text>();
            CardDetailedDataBuffValue = selfTransform.FindChild("BuffValue").GetComponent<Text>();
            switch(num)
            {
                case 1://生命值
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute1des");
                        CardDetailedDataBaseValue.text = _card.GetBaseMaxHP().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetMaxHP() - _card.GetBaseMaxHP()).ToString();
                    }
                    break;
                case 2://物理攻击力
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute2des");
                        CardDetailedDataBaseValue.text = _card.GetPhysicalBaseAttack().ToString();
                        CardDetailedDataBuffValue.text = "+"+(_card.GetPhysicalAttack() - _card.GetPhysicalBaseAttack()).ToString();
                    }
                    break;
                case 3://法术攻击力
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute4des");
                        CardDetailedDataBaseValue.text = _card.GetMagicBaseAttack().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetMagicAttack() - _card.GetMagicBaseAttack()).ToString();
                    }
                    break;
                case 4://物理防御力
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute3des");
                        CardDetailedDataBaseValue.text = _card.GetPhysicalBaseDefence().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetPhysicalDefence() - _card.GetPhysicalBaseDefence()).ToString();
                    }
                    break;
                case 5://法术防御力
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute5des");
                        CardDetailedDataBaseValue.text = _card.GetMagicBaseDefence().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetMagicDefence() - _card.GetMagicBaseDefence()).ToString();
                    }
                    break;
                case 6://命中
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute6des");
                        CardDetailedDataBaseValue.text = _card.GetBaseHit().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetHit() - _card.GetBaseHit()).ToString();
                    }
                    break;
                case 7://闪避
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute7des");
                        CardDetailedDataBaseValue.text = _card.GetBaseDodge().ToString();
                        CardDetailedDataBuffValue.text ="+"+ (_card.GetDodge() - _card.GetBaseDodge()).ToString();
                    }
                    break;
                case 8://暴击
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute8des");
                        CardDetailedDataBaseValue.text = _card.GetBaseCritical().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetCritical() - _card.GetBaseCritical()).ToString();
                    }
                    break;
                case 9://暴击伤害率
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute16des");
                        CardDetailedDataBaseValue.text = _card.GetCriticalHurtAddRate() * 100 +"%";
                        CardDetailedDataBuffValue.text = "+" + (_card.GetCriticalHurtAddRate() - _card.GetCriticalHurtAddRate()).ToString();
                    }
                    break;
                case 10://韧性
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute9des");
                        CardDetailedDataBaseValue.text = _card.GetBaseTenacity().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetTenacity() - _card.GetBaseTenacity()).ToString();
                    }
                    break;
                case 11://速度
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute10des");
                        CardDetailedDataBaseValue.text = _card.GetBaseSpeed().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetSpeed() - _card.GetBaseSpeed()).ToString();
                    }
                    break;
                case 12://生命恢复力
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute11des");
                        CardDetailedDataBaseValue.text = _card.GetHpRecover().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetHpRecover() - _card.GetHpRecover()).ToString();
                    }
                    break;
                case 13://初始怒气值
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute25des");
                        CardDetailedDataBaseValue.text = _card.GetInitPowerAddition().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetInitPowerAddition() - _card.GetInitPowerAddition()).ToString();
                    }
                    break;
                case 14://物理伤害加深率
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute20des");
                        CardDetailedDataBaseValue.text = _card.GetPhysicalHurtAddPermil() * 100 +"%";
                        CardDetailedDataBuffValue.text = "+" + (_card.GetPhysicalHurtAddPermil() - _card.GetPhysicalHurtAddPermil()).ToString();
                    }
                    break;
                case 15://法术伤害加深率
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute18des");
                        CardDetailedDataBaseValue.text = _card.GetMagicHurtAddPermil() * 100 +"%";
                        CardDetailedDataBuffValue.text = "+" + (_card.GetMagicHurtAddPermil() - _card.GetMagicHurtAddPermil()).ToString();
                    }
                    break;
                case 16://伤害附加值
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute21des");
                        CardDetailedDataBaseValue.text = _card.GetExtraHurt().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetExtraHurt() - _card.GetExtraHurt()).ToString();
                    }
                    break;
                case 17://物理伤害减免率
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute17des");
                        CardDetailedDataBaseValue.text = _card.GetPhysicalHurtReducePermil() * 100 +"%";
                        CardDetailedDataBuffValue.text = "+" + (_card.GetPhysicalHurtReducePermil() - _card.GetPhysicalHurtReducePermil()).ToString();
                    }
                    break;
                case 18://法术伤害减免率
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute19des");
                        CardDetailedDataBaseValue.text = _card.GetMagicHurtReducePermil() * 100 +"%";
                        CardDetailedDataBuffValue.text = "+" + (_card.GetMagicHurtReducePermil() - _card.GetMagicHurtReducePermil()).ToString();
                    }
                    break;
                case 19://伤害减免值
                    {
                        CardDetailedDataName.text = GameUtils.getString("baseattribute22des");
                        CardDetailedDataBaseValue.text = _card.GetReduceHurtPoint().ToString();
                        CardDetailedDataBuffValue.text = "+" + (_card.GetReduceHurtPoint() - _card.GetReduceHurtPoint()).ToString();
                    }
                    break;
            }
        }
    }

}

