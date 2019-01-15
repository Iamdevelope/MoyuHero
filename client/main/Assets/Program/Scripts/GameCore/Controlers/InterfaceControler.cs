using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.UI;
using DreamFaction.UI.Core;
using DreamFaction.LogSystem;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;

namespace DreamFaction.GameCore
{
    /// <summary>
    /// 提供通用接口类
    /// </summary>
    public class InterfaceControler : BaseControler
    {
        public float speed = 300;
        private static InterfaceControler Inst;
        public List<MessageItem> MsgBoxList = new List<MessageItem>();                     //冒泡盒子组
        List<int> delIndex = new List<int>();   //冒泡提示要删除的元素index
        Vector3 tempPos = Vector3.zero;
        protected override void InitData()
        {
            if (Inst == null)
                Inst = this;
        }
        protected override void DestroyData()
        {
            Inst = null;
        }
        public static InterfaceControler GetInst()
        {
            return Inst;
        }
        //显示空框体(单行个数，总共显示个数，单页最多显示个数,需要显示空物体的PRB地址,父节点)
        public void ShowKong(int OneRowCount, int MaxCount, int OnePageCount, Transform par, string url)
        {
            int _kcount = 0;
            if (OnePageCount == 0 || OneRowCount == 0)
                return;
            if (MaxCount <= OnePageCount)
            {
                _kcount = OnePageCount - MaxCount;
            }
            else
            {
                _kcount = OneRowCount - (MaxCount % OneRowCount);
                if (_kcount == OneRowCount)
                    _kcount = 0;
            }
            for (int i = 0; i < _kcount; ++i)
            {
                GameObject go = Instantiate(Resources.Load(url)) as GameObject;
                go.transform.SetParent(par, false);
            }
        }
        //添加提示框
        /// <summary>
        /// 通用冒泡提示框 ，建议使用此接口。
        /// 不要再使用自定义的tranform位置 统一显示位置
        /// </summary>
        /// <param name="text"></param>
        /// <param name="par"></param>
        /// <param name="MaxCount"></param>
        public void AddMsgBox(string text, Transform par = null, int MaxCount = 1)
        {
            if (MsgBoxList == null)
                MsgBoxList = new List<MessageItem>();

            //如果是图文混排
            if (text.Contains("Image"))
            {
                RichText textRich = RichText.GetRichText(text);
                AddMsgBox(textRich.transform,par);
            }
            else
            {
                GameObject temp = Instantiate(Resources.Load("UI/Prefabs/UI_Home/UI_MsgBox")) as GameObject;
                temp.transform.SetParent(par == null ? UI_HomeControler.Inst.GetTopTransform() : par, false);
                temp.transform.FindChild("Text").GetComponent<Text>().text = text;
                AddMsgBox(temp.transform, par);
            }
        }
        ///  通用冒泡提示框 
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        /// <param name="MaxCount"></param>
        public void AddMsgBox(Transform child,Transform parent = null,int MaxCount = 1)
        {
            if (MsgBoxList == null)
                MsgBoxList = new List<MessageItem>();

            child.transform.SetParent(parent == null ? UI_HomeControler.Inst.GetTopTransform() : parent, false);
            MessageItem item = new MessageItem();
            if (child.name.Contains( "Rich"))
            {
                item.gameobject = child.gameObject;
                item.time = 1;
                for (int i = 0; i < child.childCount; i++)
                {
                    if (child.GetChild(i).gameObject.name == "Text")
                    {
                        item.TextList.Add(child.GetChild(i).gameObject.GetComponent<Text>());
                    }
                    if (child.GetChild(i).gameObject.name == "Image")
                    {
                        item.ImageList.Add(child.GetChild(i).gameObject.GetComponent<Image>());
                    }
                }
            }
            else
            {               
                item.gameobject = child.gameObject;
                item.time = 1;
                item.ImageList.Add(child.gameObject.GetComponent<Image>());
                item.TextList.Add(child.gameObject.transform.FindChild("Text").GetComponent<Text>());
            }
            if (MsgBoxList.Count > 0)
            {
                
                Destroy(MsgBoxList[0].gameobject);
                MsgBoxList.Remove(MsgBoxList[0]);
            }
            MsgBoxList.Add(item);
            //for (int i = MsgBoxList.Count - 1; i > 0; --i)
            //{
            //    GameObject bottom = MsgBoxList[i].gameobject;
            //    GameObject up = MsgBoxList[i - 1].gameobject;
            //    up.transform.localPosition = new Vector3(up.transform.localPosition.x, Mathf.Max(up.transform.localPosition.y, bottom.transform.localPosition.y + 120, up.transform.localPosition.z));
            //}
        }
        public class MessageItem
        {
            public GameObject gameobject;
            public float time;
            public float bgAlpha = 1;
            public float textAlpha = 1f;
            public List<Image> ImageList = new List<Image>();
            public List<Text> TextList = new List<Text>();
        }

        protected override void UpdateView ()
        {
            if (MsgBoxList.Count == 0)
                return;
            try
            {
                if (MsgBoxList.Count >= 2)
                {
                    MsgBoxList.Remove(MsgBoxList[0]);
                }
                else
                {
                    if (MsgBoxList[0].gameobject != null)
                    {
                        Transform child = MsgBoxList[0].gameobject.transform;
                        MsgBoxList[0].time -= 1 * Time.deltaTime;
                        if (MsgBoxList[0].time <= 0)
                        {
                            for (int i = 0; i < MsgBoxList[0].ImageList.Count;i++ )
                            {
                                MsgBoxList[0].ImageList[i].color = new Color(1, 1, 1, MsgBoxList[0].bgAlpha -= 1f * Time.deltaTime);
                            }
                            for (int i = 0; i < MsgBoxList[0].TextList.Count; i++)
                            {
                                MsgBoxList[0].TextList[i].color = new Color(1, 1, 1, MsgBoxList[0].textAlpha -= 1f * Time.deltaTime);
                            }
                            
                            if (MsgBoxList[0].bgAlpha <= 0)
                            {
                                MsgBoxList.RemoveAt(0);
                                Destroy(child.gameObject);
                            }                          
                        }
                    }
                }

             
                //for (int i = MsgBoxList.Count; i > 0; --i)
                //{
                //    if (MsgBoxList[i - 1].gameobject != null)
                //    {
                //        Transform child = MsgBoxList[i - 1].gameobject.transform;
                //        tempPos = child.localPosition;
                //        tempPos.y += MsgBoxList[i - 1].speed * Time.deltaTime;
                //        child.localPosition = tempPos;
                //        if (tempPos.y >= 450)
                //        {
                //            MsgBoxList.RemoveAt(i - 1);
                //            Destroy(child.gameObject);
                //        }
                //    }
                //    else
                //    {
                //        MsgBoxList.RemoveAt(i - 1);
                //        // Debug.LogError("冒泡提示GameObject已经消耗，但是集合内的对象没有删除");
                //    }
                //}
            }
            catch (System.Exception e)
            {
                //造成空引用异常的原因就是  当提示还在移动时 没有销毁
                //切换场景 会立刻销毁掉还是显示提示条的场景资源 ，此时 list 的引用地址还是存在的，只是资源已经释放，所以不用理睬。
                //foreach (MessageItem item in MsgBoxList)
                //{
                //    Destroy(item.gameobject);
                //}
                Debug.LogError(e.Message);
                // MsgBoxList.Clear();

            }
        }

        /// <summary>
        /// 公用的魔钻不足提示窗;
        /// </summary>
        public void ShowGoldNotEnougth(Transform parent = null)
        {
            UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();

            if (box == null)
            {
                LogSystem.LogManager.LogError("提示窗is null");
                return;
            }

            box.SetDescription_text(GameUtils.getString("common_diamondenough_content"));
            box.SetIsNeedDescription(false);
            box.SetLeftBtn_text(GameUtils.getString("shop_content4"));
            box.SetLeftClick(() =>
            {
                //InterfaceControler.GetInst().AddMsgBox("打开快速充值界面", parent);
                UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
                box.OnCloes();
            });

            box.SetRightBtn_text(GameUtils.getString("common_button_close"));
        }

        private void RemoveMsgBoxList(GameObject temp)
        {
        //    MsgBoxList.Remove(temp);
        //    Destroy(temp);
        }
        /// <summary>
        /// 显示星级  [Lyq]
        /// </summary>
        public void AddSharLevel(Transform par, HeroTemplate hero)
        {
            int m_heroStar = hero.getQuality();
            int maxStar = hero.getMaxQuality();
            for (int i = 0; i < 5; i++)
            {
                Image temp = par.GetChild(i).GetComponent<Image>();
                temp.enabled = i < maxStar;

                temp = par.GetChild(i + 5).GetComponent<Image>();
                temp.enabled = i < m_heroStar;
            }
        }

        //等级图片排序
        public void AddLevelNum ( string level, Transform par, string url )
        {
            if ( level.Length > par.childCount )
            {
                int childCount = par.childCount;
                for ( int i = 0; i < level.Length - childCount; ++i )
                {
                    GameObject _obj = new GameObject ( "Num" );
                    _obj.transform.SetParent ( par, false );
                    _obj.transform.localScale = Vector3.one;
                    _obj.AddComponent<Image> ().enabled = false;
                }
            }

            for ( int i = 0; i < par.childCount; ++i )
            {
                if ( i < level.Length )
                {
                    string temp = level.Substring ( i, 1 );
                    Image image = par.GetChild ( i ).GetComponent<Image> ();
                    image.preserveAspect = true;
                    image.sprite = UIResourceMgr.LoadSprite ( url + temp );
                    image.enabled = true;
                    par.GetChild ( i ).gameObject.SetActive ( true );
                }
                else
                {
                    par.GetChild ( i ).gameObject.SetActive ( false );
                }
            }
        }
        /// <summary>
        /// 等级排序 [从左到右排序  Lyq]
        /// </summary>
        /// <param name="level"></param>
        /// <param name="par"></param>
        public static void AddLevelNum(string level, Transform par,bool isVip = false)
        {
            for (int i = 0; i < par.childCount; ++i)
            {
                Destroy(par.GetChild(i).gameObject);
            }
            string url = "UI/Number/card_number/";
            if (isVip)
            {
                url = "UI/Number/vip_number/vip";
            }  
          
            char[] _charAry = level.ToCharArray();
            for (int i = 0; i < _charAry.Length; i++)
            {
                GameObject _obj = new GameObject("Num");
                _obj.transform.SetParent(par, false);
                _obj.transform.localScale = Vector3.one;
                Image _img = _obj.AddComponent<Image>();
                _img.sprite = UIResourceMgr.LoadSprite(url + _charAry[i]);
                _img.preserveAspect = true;
            }
        }

        /// <summary>
        /// 初始化英雄类型图标 [Lyq]
        /// </summary>
        /// <param name="heroData">英雄表数据</param>
        /// <param name="attackTypeImg">攻击类型</param>
        /// <param name="jobTypeImg">职业类型</param>
        /// <param name="raceTypeImg">种族类型</param>
        public void ShowHeroImg(HeroTemplate heroData, Image attackTypeImg, Image jobTypeImg, Image raceTypeImg)
        {
            if (heroData.getClientSignType()[0] == 0 && heroData.getClientSignType()[1] == 0)//近战物理
            {
                attackTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_06");
            }
            else if (heroData.getClientSignType()[0] == 0 && heroData.getClientSignType()[1] == 1)//近战法术
            {
                attackTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_05");
            }
            else if (heroData.getClientSignType()[0] == 1 && heroData.getClientSignType()[1] == 0)//远程物理
            {
                attackTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_04");
            }
            else if (heroData.getClientSignType()[0] == 1 && heroData.getClientSignType()[1] == 1)//远程法术
            {
                attackTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_07");
            }

            if (heroData.getClientSignType()[2] == 0)//肉盾
            {
                jobTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_02");
            }
            else if (heroData.getClientSignType()[2] == 1)//输出
            {
                jobTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_03");
            }
            else if (heroData.getClientSignType()[2] == 2)//辅助
            {
                jobTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_01");
            }

            if (heroData.getCamp() == 1)//生灵
            {
                raceTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_01");
            }
            else if (heroData.getCamp() == 2)//神抵
            {
                raceTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_03");
            }
            else if (heroData.getCamp() == 3)//恶魔
            {
                raceTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_02");
            }
        }

        /// <summary>
        /// 返回英雄种族类型图标 [Lyq]
        /// </summary>
        /// <param name="heroData">英雄T</param>
        /// <returns></returns>
        public Sprite GetHeroRaceTypeIcon(HeroTemplate heroT)
        {
            switch (heroT.getCamp())
            {
                case 1://生灵
                    return UIResourceMgr.LoadSprite(common.defaultPath + "icon_TY_0005");
                case 2://神抵
                    return UIResourceMgr.LoadSprite(common.defaultPath + "icon_TY_0009");
                case 3://恶
                    return UIResourceMgr.LoadSprite(common.defaultPath + "icon_TY_0006");
                default:
                    LogManager.Log("Fack  又填错表");
                    return null;
            }
        }

        /// <summary>
        /// 根据定位显示显示类型图标 文本 [Lyq]
        /// </summary>
        /// <param name="heroT"></param>
        /// <param name="typeIcon"></param>
        public void ShowTypeIcon(HeroTemplate heroT,Image typeIcon,Text typeTxt = null)
        {
            int temp = heroT.getQosition();
            switch (temp)
            {
                case 1:
                    typeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "icon_TY_0001");
                    if (typeTxt != null)
                        typeTxt.text = GameUtils.getString("ui_yingxiongbeibao8");
                    break;
                case 2:
                    typeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "icon_TY_0004");
                    if (typeTxt != null)
                        typeTxt.text = GameUtils.getString("ui_yingxiongbeibao7");
                    break;
                case 3:
                    typeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "icon_TY_0000");
                    if (typeTxt != null)
                        typeTxt.text = GameUtils.getString("ui_yingxiongbeibao10");
                    break;
                case 4:
                    typeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "icon_TY_0008");
                    if (typeTxt != null)
                        typeTxt.text = GameUtils.getString("ui_yingxiongbeibao9");
                    break;
                default:
                    LogManager.Log("类型不对");
                    break;
            }
        }

        public void ShowSkillTypeIcon(SkillTemplate skillT, Image typeIcon)
        {
            switch (skillT.getSkillType())
            {
                case 1:
                    typeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0065");
                    break;
                case 2:
                    typeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0064");
                    break;
                default:
                    LogManager.Log("找数值对表");
                    break;
            }
        }

        /// <summary>
        /// 返回技能是否开启 [Lyq]   重载
        /// </summary>
        /// <param name="objHero"></param>
        /// <param name="skillNo"></param>
        /// <returns></returns>
        public bool IsOpenSkill(ObjectHero objHero,int skillNo)
        {
            HeroTemplate heroData = objHero.GetHeroRow();
            return IsOpenSkill(heroData,skillNo);
        }

        /// <summary>
        /// 返回技能是否开启 [Lyq]
        /// </summary>
        /// <param name="heroData">英雄表数据</param>
        /// <param name="skillNo">技能编号</param>
        /// <returns></returns>
        public bool IsOpenSkill(HeroTemplate heroData,int skillNo)
        {
            int starLevel = heroData.getQuality();
            switch (skillNo)
            {
                case 1:
                    if (starLevel <= 1) return false;
                    break;
                case 2:
                    if (starLevel <= 2) return false;
                    break;
                case 3:
                    if (starLevel <= 3) return false;
                    break;
            }
            return true;
        }


        /// <summary>
        /// 返回当前消耗品的个数 [Lyq]
        /// </summary>
        /// <param name="id">消耗品ID</param>
        /// <returns>拥有消耗品数量</returns>
        public int ReturnItemCount(int id)
        {
            int _tempNum = 0;
            List<BaseItem> _itemList = ObjectSelf.GetInstance().CommonItemContainer.GetItemList(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON);
            int baseItemCount = _itemList.Count;
            for (int i = 0; i < baseItemCount; ++i)
            {
                if (_itemList[i].GetItemTableID() == id)
                {
                    int _count = _itemList[i].GetItemCount();
                    _tempNum += _count;
                }
            }
            return _tempNum;
        }
        /// <summary>
        /// 根据英雄表英雄id获取品质头像外框
        /// </summary>
        /// <param name="heroId">英雄表英雄id</param>
        /// <returns>白绿蓝紫橙红</returns>
        public Sprite ReturnHeroQuailty(int quality)
        {
            switch (quality)
            {
                case 1:
                    return UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0109");
                case 2:
                    return UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0112");
                case 3:
                    return UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0113");
                case 4:
                    return UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0110");
                case 5:
                    return UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0200");
                case 6:
                    return UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0111");
                default:
                    LogManager.Log("只能支持1-6");
                    return null;
            }
        }
        /// <summary>
        ///  返回英雄资质Sprite
        /// </summary>
        /// <param name="heroT"></param>
        /// <returns></returns>
        public Sprite GetHeroAptImg(HeroTemplate heroT)
        {
            switch (heroT.getBorn())
            {
                case 1:
                    return UIResourceMgr.LoadSprite(common.defaultPath + "icon_YXQH_0003");
                case 2:
                    return UIResourceMgr.LoadSprite(common.defaultPath + "icon_YXQH_0002");
                case 3:
                    return UIResourceMgr.LoadSprite(common.defaultPath + "icon_YXQH_0001");
                case 4:
                    return UIResourceMgr.LoadSprite(common.defaultPath + "icon_YXQH_0000");
//                 case 5:
//                     return UIResourceMgr.LoadSprite(common.defaultPath + "");
//                 case 6:
//                     return UIResourceMgr.LoadSprite(common.defaultPath + "");
                default:
                    LogManager.Log("坑");
                    return UIResourceMgr.LoadSprite(common.defaultPath + "");
            }


        }

    }
}

