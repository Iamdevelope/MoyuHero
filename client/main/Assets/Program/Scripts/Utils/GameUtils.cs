using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.LogSystem;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork.Data;
using System.Text;
using DreamFaction.GameNetWork;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.NetworkInformation;
using DreamFaction.UI;
using DreamFaction.UI.Core;
using GNET;

namespace DreamFaction.Utils
{
    public  class GameUtils 
    {
        // 遍历Transform查找所有子节点
        public static void AttachParticleCS(Transform trans)
        {
            int childCount = trans.childCount;
            for (int k = 0; k < childCount; ++k)
            {
                Transform tt = trans.GetChild(k);
                if (tt.childCount > 0)
                {
                    AttachParticleCS(tt);
                }
                else
                {
                    AttachCS(tt);
                }
            }
            AttachCS(trans);
        }

        // 绑定脚本
        private static void AttachCS(Transform trans)
        {
            if (trans.GetComponent<ParticleSystem>() != null || trans.GetComponent<ParticleEmitter>() != null)
            {
                if (trans.GetComponent<ParticleHelp>() != null)
                {
                    //Debug.LogWarning(trans.name + "已经添加过了<ParticleHelp>");
                    //GameObject.DestroyImmediate(trans.GetComponent<ParticleHelp>());
                    GameObject.Destroy(trans.GetComponent<ParticleHelp>());
//                     if (trans.GetComponent<ParticleHelp>() == null)
//                         Debug.Log(trans.name + " 拥有 Particle,已删除<ParticleHelp>");
//                     else
//                         Debug.Log(trans.name + " 拥有 Particle,删除<ParticleHelp>失败");
                }
                else
                {
                    //Debug.Log(trans.name + " 拥有 Particle,准备添加<ParticleHelp>");
                    trans.gameObject.AddComponent<ParticleHelp>();//添加一个脚本
                }
            }
            else
            {
                
                //Debug.Log(trans.name  + " 没有Particle，跳过！");
            }
        }
        /// <summary>
        /// 英雄相关解析
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hero">英雄信息类</param>
        /// <returns></returns>
        public static string getHeroString(string key, ObjectCard hero)
        {
            string[] strHeroList = getString(key).Split('|');
            if (strHeroList.Length>2)
            {
                StringBuilder heroer = new StringBuilder();
                for (int i = 0; i < strHeroList.Length; i++)
                {
                    string[] str = strHeroList[i].Split(',');
                    switch (str[0])
                    {
                        case"0":
                            switch(str[1])
                            {
                                case"HERO_AD":
                                    heroer.Append(hero.GetPhysicalAttack());
                                    break;
                                case"HERO_AP":
                                    heroer.Append(hero.GetMagicAttack());
                                    break;
                                case"HERO_HP":
                                    heroer.Append(hero.GetMaxHP());
                                    break;
                                case"HERO_LEVEL":
                                    heroer.Append(hero.GetHeroData().Level);
                                    break;
                                //case "HERO_TITLE":
                                //    heroer.Append("");
                                //    break;
                                    //拓展
                                default:
                                    break;
                            }
                            break;
                        case"1":
                            switch (str[1])
                            {
                                case "HERO_AD":
                                    int numAD = (int)(hero.GetPhysicalAttack() * float.Parse(str[2]));
                                    heroer.Append(numAD);
                                    break;
                                case "HERO_AP":
                                    int numAP = (int)(hero.GetMagicAttack() * float.Parse(str[2]));
                                    heroer.Append(numAP);
                                    break;
                                case "HERO_HP":
                                    int numHP = (int)(hero.GetMaxHP() * float.Parse(str[2]));
                                    heroer.Append(numHP);
                                    break;
                                case "HERO_LEVEL":
                                    int numLEVEL = (int)(hero.GetHeroData().Level * float.Parse(str[2]));
                                     heroer.Append(numLEVEL);
                                    break;
                                //case "HERO_TITLE":
                                //    heroer.Append("");
                                //    break;
                                    //拓展
                                default:
                                    break;
                            }
                            break;
                        default:
                            heroer.Append(strHeroList[i]);
                            break;
                    }
                }
                return heroer.ToString();
            }
            return getString(key);
        }
        /// <summary>
        /// 玩家信息相关解析
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getPlayerString(string key)
        {
            string[] strPlayerList = getString(key).Split('|');
            if (strPlayerList.Length>2)
            {
                StringBuilder player = new StringBuilder();
                for (int i = 0; i < strPlayerList.Length; i++)
                {
                    string[] str = strPlayerList[i].Split(',');
                    switch (str[0])
                    {
                        case "0":
                            switch (str[1])
	                        {
                                case "PLAYER_NAME":
                                    player.Append(ObjectSelf.GetInstance().Name);
                                    break;
                                    //拓展
		                        default:
                                    break;
	                        }
                            break;
                        case "1":
                            //TODO
                            break;
                        default:
                            player.Append(strPlayerList[i]);
                            break;
                    }
                }
                return player.ToString();
            }
             return getString(key);  
        }

        // 获取字符串
        public static string getString(string key, bool replaceBr = false)
        {
            var table = DataTemplate.GetInstance().m_ChsTextTable;
            if (table.tableContainsKey(key))
            {
                var data = (ChsTextTemplate)table.getTableData(key);
                
                if (replaceBr)
                    return data.languageMap[AppManager.Inst.GameLanguage].Replace("\\n", "\n").Trim();
                else
                    return data.languageMap[AppManager.Inst.GameLanguage].Trim();
            }
            //LogManager.Log("!!!!!!!!!!!warning：getString not has Table Key :" + key);
            return string.Empty;
        }

        //是否是屏蔽字
        public static bool IsShieldCharacter(string key,string type)
        {
            var table = DataTemplate.GetInstance().m_ShieldCharacterTable;
            if (table.tableContainsKey(key))
            {
                var data = (ShieldcharacterTemplate)table.getTableData(key);
                if (type == "name" )
                {
                    if (data.getName() == 1)
                    {
                        return true;
                    }
                }
                if (type == "chat")
                {
                    if (data.getChat() == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string getString(string key, TEXT_COLOR color)
        {
            return StringWithColor(getString(key), color);
        }

        public static string StringWithColor(string content, TEXT_COLOR color)
        {
            return StringWithColor(content, color.ToString().ToLower());
        }
           
        public static string StringWithGameColor(string content, GAME_TXT_COLOR color)
        {
            string c = "";
            switch (color)
            {
                case GAME_TXT_COLOR.BLUE:
                    c = "66EBFF";
                    break;
                case GAME_TXT_COLOR.PURPLE:
                    c = "FF7BED";
                    break;
                case GAME_TXT_COLOR.ORANGE:
                    c = "FFA48E";
                    break;
                case GAME_TXT_COLOR.YELLOW:
                    c = "FFF58E";
                    break;
                case GAME_TXT_COLOR.GREEN:
                    c = "7EFF8A";
                    break;
                case GAME_TXT_COLOR.LIGHTGREEN:
                    c = "E6FFCC";
                    break;
                case GAME_TXT_COLOR.LIGHTORANGE:
                    c = "FFF2CC";
                    break;
                case GAME_TXT_COLOR.LIGHTBLUE:
                    c = "D9FFF4";
                    break;
                default:
                    break;
            }

            return StringWithColor(content, c);
        }

        public static string StringWithColor(string content, string color)
        {
            return "<color=" + color + ">" + content + "</color>";
        }

        public static string StringWithGrayColor(string content)
        {
            return "<color=#A1A1A1>" + content + "</color>";
        }

        public static string GetAttriName(int attriType)
        {
            switch (attriType)
            {
                case 1://生命值
                    {
                        return getString( "attribute3name" );
                    }                    
                case 3://攻击
                    {
                        return getString( "attribute1name" );
                    }

                case 5://物理防御
                    {
                        return getString( "attribute2name" );
                    }
                    
                case 23://命中率
                    {
                       
                        return getString( "attribute4name" );
                    }
                    
                case 24://闪避率
                    {
                        return getString( "attribute5name" );
                    }
                    
                case 25://暴击率
                    {
                        return getString( "attribute6name" );
                    }
                    
                case 26://韧性率
                    {
                        return getString( "attribute7name" );
                    }
                    
                case 31://伤害加成率
                    {
                        return getString( "attribute10name" );
                    }
                    
                case 32://伤害减免率
                    {
                        return getString( "attribute11name" );
                    }
                    
                case 33://暴击伤害率
                    {
                        return getString( "attribute12name" );
                    }
                    
                case 56://格挡率

                    {
                        return getString( "attribute8name" );
                    }
                    
                case 57://破甲率
                    {
                        return getString( "attribute9name" );
                    }
                    
                case 58://吸血率
                    {
                        return getString("baseattribute13des");
                    }
                    
                case 14://物理伤害加深率
                    {
                        return getString("baseattribute16des");
                    }
                    
                case 15://法术伤害加深率
                    {
                        return getString("baseattribute18des");
                    }
                    
                case 16://暴击率
                    {
                        return getString("baseattribute16des");
                    }
                    
                case 17://物理伤害加成率
                    {
                        return getString("baseattribute17des");
                    }
                    
                case 18://物理伤害减免率
                    {
                        return getString("baseattribute18des");
                    }

                case 19://法伤加成率
                    {
                        return getString("baseattribute19des");
                    }
                case 21: //附加伤害值
                    {
                        return getString("baseattribute21des");
                    }
                case 00: //初始怒值
                    {
                        return getString("baseattribute25des");
                    }
                case 20://法伤减免率
                    {
                        return getString("baseattribute20des");
                    }
                case 22://伤害减免值
                    {
                        return getString("baseattribute22des");
                    }
                    
            }

            return "";
        }

        public static bool TryGetString(string key, out string val)
        {
            var data = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData(key);
            if(data == null)
            {
                val = "";
                return false;
            }

            val = data.languageMap[AppManager.Inst.GameLanguage];
            return true;
        }

        //解析技能升级描述
        public static string SetUpShow(ObjectCard _card, SkillupcostTemplate _skill)
        {
            if (_card == null || _skill == null)
                return string.Empty;
            string _text = GameUtils.getHeroString(_skill.getUpgradeDes(), _card).Replace("\\n", "\n");
            return _text;
        }

        public static int GetKeyByIdx(Dictionary<int, Pair> dic, int idx)
        {
            if (dic == null || dic.Count == 0 || idx < 0 || idx >= dic.Count)
                return default(int);

            int i = 0;
            foreach(int k in dic.Keys)
            {
                if (idx == i)
                    return k;

                i++;
            }

            return default(int);
        }

        public static void DestroyChildsObj(GameObject parent, bool isImmediately = false)
        {
            if (parent == null) return;

            int count = parent.transform.childCount;

            if (count == 0) return;

            while (--count >= 0)
            {
                if (isImmediately)
                    GameObject.DestroyImmediate(parent.transform.GetChild(count).gameObject);
                else
                    GameObject.Destroy(parent.transform.GetChild(count).gameObject);
            }
        }

        /// <summary>
        /// EM_OBJECT_CLASS_INVALID = -1,
        /// EM_OBJECT_CLASS_SPELL = 1000,	    //1000000000 - 1099999999	技能
        /// EM_OBJECT_CLASS_BUFF = 1100,	    //1100000001 - 1199999999	BUFF
        /// EM_OBJECT_CLASS_DROPBOX = 1200,	    //1200000000 - 1299999999	掉落包
        /// EM_OBJECT_CLASS_MONSTER = 1300,	    //1300000000 - 1399999999	关卡与怪物
        /// EM_OBJECT_CLASS_RES = 1400,         //1400000001 - 1400999999	资源
        /// EM_OBJECT_CLASS_RUNE = 1401,        //1401000001 - 1401999999	符文
        /// EM_OBJECT_CLASS_COMMON = 1402,	    //1402000001 - 1402999999	道具
        /// EM_OBJECT_CLASS_HERO = 1403,	    //1403000001 - 1403999999	英雄
        /// EM_OBJECT_CLASS_SKIN = 1404,        //1404000001 - 1404999999	皮肤
        /// EM_OBJECT_CLASS_BOX = 1405,      	//1405000001 - 1405999999	宝箱
        /// EM_OBJECT_CLASS_ARTIFACT = 1406,    //1406000001 - 1406999999	神器
        /// </summary>
        /// <param name="id"></param>
        /// <returns>获取道具的类型</returns>
        public static EM_OBJECT_CLASS GetObjectClassById(int id)
        {
            int result = id / 1000000;
            if (result < 1400)
            {
                result /= 100;
                result *= 100;
            }
            return (EM_OBJECT_CLASS)result;
        }

        /// <summary>
        /// 获得物品的类型；不包含英雄、怪物;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EM_ITEM_TYPE GetItemTypeById(int id)
        {
            ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(id);
            if (itemT == null)
            {
                return EM_ITEM_TYPE.EM_ITEM_TYPE_INVALID;
            }

            return (EM_ITEM_TYPE)id;
        }

        public static int StringToInt(string str)
        {
            try
            {
                return (int.Parse(str));
            }catch(Exception e)
            {
                LogManager.LogError(e.ToString());
                return -1;
            }
        }

        public static void SetImageGrayState(Image img, bool isGray)
        {
            if (isGray)
            {
                Shader s = Shader.Find("UI/ColorfulToGray01");
                if (s == null)
                {
                    LogManager.LogError("Shader Not Found");
                    return;
                }

                Material mat = new Material(s);
                mat.SetFloat("_Gray", 1f);
                //mat.mainTexture = img.sprite.texture;

                img.material = mat;
            }
            else
            {
                if (img.material != null)
                {
                    img.material.SetFloat("_Gray", 0f);
                }
            }
        }

        public static void SetBtnSpriteGrayState(Button bt, bool isGray)
        {
            Image img = bt.GetComponent<Image>();
                
            if (img == null)
            {
                LogManager.LogError("Image Component Not Found");
                return;
            }

            SetImageGrayState(img, isGray);
        }

        public static Sprite GetSpriteByResourceType(EM_RESOURCE_TYPE resourceType)
        {
            return GetSpriteByResourceType((int)resourceType);
        }

        /// <summary>
        /// 根据资源类型获得sprite;
        /// 魔钻	1400 000001
        /// 金币	1400 000002
        /// 圣灵之泉	1400 000003
        /// 熔炼点	1400000004
        /// 黄金勋章	1400000005
        /// 白银勋章	1400000006
        /// 青铜勋章	1400000007
        /// 赤铁勋章	1400000008
        /// 经验结晶	1400000009
        /// 活力     1400010001
        /// </summary>
        /// <returns></returns>
        public static Sprite GetSpriteByResourceType(int resourceID)
        {
            string spritePath = "";
            switch(resourceID)
            {
                case 1400000001:
                    ResourceindexTemplate _temp_res_1 = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(1400000001);
                    if (_temp_res_1 != null)
                    {
                        spritePath = _temp_res_1.getIcon3();
                    }
                    break;
                case 1400000002:
                    ResourceindexTemplate _temp_res_2 = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(1400000002);
                    if (_temp_res_2 != null)
                    {
                        spritePath = _temp_res_2.getIcon3();
                    }
                    break;
                case 1400000003:
                    ResourceindexTemplate _temp_res_3 = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(1400000003);
                    if (_temp_res_3 != null)
                    {
                        spritePath = _temp_res_3.getIcon3();
                    }
                    break;
                case 1400000004:
                    ResourceindexTemplate _temp_res_4 = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(1400000004);
                    if (_temp_res_4 != null)
                    {
                        spritePath = _temp_res_4.getIcon3();
                    }
                    break;
                case 1400000005:
                    ResourceindexTemplate _temp_res_5 = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(1400000005);
                    if (_temp_res_5 != null)
                    {
                        spritePath = _temp_res_5.getIcon3();
                    }
                    break;
                case 1400000006:
                    ResourceindexTemplate _temp_res_6 = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(1400000006);
                    if (_temp_res_6 != null)
                    {
                        spritePath = _temp_res_6.getIcon3();
                    }
                    break;
                case 1400000007:
                    ResourceindexTemplate _temp_res_7 = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(1400000007);
                    if (_temp_res_7 != null)
                    {
                        spritePath = _temp_res_7.getIcon3();
                    }
                    break;
                case 1400000008:
                    ResourceindexTemplate _temp_res_8 = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(1400000008);
                    if (_temp_res_8 != null)
                    {
                        spritePath = _temp_res_8.getIcon3();
                    }
                    break;
                case 1400000009:
                    ResourceindexTemplate _temp_res_9 = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(1400000009);
                    if (_temp_res_9 != null)
                    {
                        spritePath = _temp_res_9.getIcon3();
                    }
                    break;
                case 1400010001:
                    //spritePath = "UI_Home_04";
                    ResourceindexTemplate _temp_res_10001 = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(1400010001);
                    if (_temp_res_10001 != null)
                    {
                        spritePath = _temp_res_10001.getIcon3();
                    }
                    break;
            }

            return UIResourceMgr.LoadSprite(common.defaultPath + spritePath);
        }

        /// <summary>
        /// 根据资源类型获得sprite;
        /// 魔钻	1400 000001
        /// 金币	1400 000002
        /// 圣灵之泉	1400 000003
        /// 熔炼点	1400000004
        /// 黄金勋章	1400000005
        /// 白银勋章	1400000006
        /// 青铜勋章	1400000007
        /// 赤铁勋章	1400000008
        /// 经验结晶	1400000009
        /// 活力     1400010001
        /// </summary>
        /// <returns></returns>
        public static long GetResourceCountByID(int resourceID)
        {
            var self = ObjectSelf.GetInstance();
            if (self == null)
                return -1;
            long count = -1;

            switch (resourceID)
            {
                case 1400000001:
                    count = self.Gold;
                    break;
                case 1400000002:
                    count = self.Money;
                    break;
                case 1400000003:
                    count = self.HeroMoney;
                    break;
                case 1400000004:
                    count = self.RuneMoney;
                    break;
                case 1400000005:
                    count = self.HuangjinXZ;
                    break;
                case 1400000006:
                    count = self.BaiJinXZ;
                    break;
                case 1400000007:
                    count = self.QingTongXZ;
                    break;
                case 1400000008:
                    count = self.ChiTieXZ;
                    break;
                case 1400000009:
                    count = self.ExpFruit;
                    break;
                case 1400010001:
                    count = self.ActionPoint;
                    break;
            }

            return count;
        }

        /// <summary>
        /// 获取英雄ID减去最后一位
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetHeroIDNum(int num)
        {
            string str = num.ToString();
            string s = str.Substring(0, str.Length - 1);
            return int.Parse(s);        
        }
        
        //获得本机IP
        public static int GetLocalIp()  
        {  
            string hostname = Dns.GetHostName();//得到本机名    
            IPHostEntry localhost = Dns.GetHostEntry(hostname);  
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.GetHashCode();  
        }

        // 获得本机MAC地址
        public static string GetMac()
        {
            string macAddress = "";
            try
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    if (!ni.GetPhysicalAddress().ToString().Equals(""))
                    {
                        macAddress = ni.GetPhysicalAddress().ToString();
                        break;
                    }
                }
            }
            catch
            {
                
            }

            return macAddress;
        }

        public static float MaxValue(float[] num)
        {
            float _tmp = num[0];
            for (int i = 0; i < num.Length;i++ )
            {
                if (_tmp < num[i])
                {
                    _tmp = num[i];
                }
            }
            return _tmp;
        }


		/// <summary>
        /// 将字符串转为DateTime;
        /// str事件表示为年(4)+月(2)+日(2)+时(2)+分(2)+秒(2)
        /// </summary>
        /// <returns></returns>
        public static DateTime ConvertStringToDateTime(string timeStr)
        {
            if (string.IsNullOrEmpty(timeStr) || timeStr.Length != 14)
                return DateTime.MaxValue;

            int year, month, day, hour, minute, sec;
            year = int.Parse(timeStr.Substring(0, 4));
            month = int.Parse(timeStr.Substring(4, 2));
            day = int.Parse(timeStr.Substring(6, 2));
            hour = int.Parse(timeStr.Substring(8, 2));
            minute = int.Parse(timeStr.Substring(10, 2));
            sec = int.Parse(timeStr.Substring(12, 2));

            return new DateTime(year, month, day, hour, minute, sec);
        }

        public static TimeSpan ConverToTimeSpan(int sec)
        {
            int hour = sec / 3600;
            int minute = (sec - hour * 3600) / 60;
            int s = sec - minute * 60 - hour * 3600;
            
            return new TimeSpan(hour, minute, s);
        }

        /// <summary>
        /// 将字符串转为 str
        /// str 表示为年(4)+月(2)+日(2)+时(2)+分(2)+秒(2)
        /// </summary>
        /// <returns></returns>
        public static string ConvertStringToDate(string timeStr)
        {
            string year, month, day, hour, minute, sec,newStr;
            year = timeStr.Substring(0, 4);
            month = timeStr.Substring(4, 2);
            day = timeStr.Substring(6, 2);
            //hour = timeStr.Substring(8, 2);
            //minute = timeStr.Substring(10, 2);
            //sec = timeStr.Substring(12, 2);
            newStr = year + "/" + month + "/" + day;// +" " + hour + ":" + minute + ":" + sec;

            return newStr;
        }


        /// <summary>
        /// 格式对齐;
        /// </summary>
        /// <param name="num"></param>
        /// <param name="length"></param>
        /// <param name="fillChar"></param>
        /// <returns></returns>
        public static string FillWithChar(int num, int length, char fillChar)
        {
            if (num.ToString().Length >= length)
                return num.ToString();

            StringBuilder sb = new StringBuilder();

            for(int i =0, j = length - num.ToString().Length; i < j; i++)
            {
                sb.Append(fillChar);
            }

            sb.Append(num);

            return sb.ToString();
        }

        // 消息包里不允许写成员变量值拷贝函数，封装到外层处理协议数据 [6/16/2015 Zmy]
        public static void CopyFightInfo(ref GNET.fightInfo _Srcinfo, FightInfo _DstInfo)
        {
            LogManager.LogAssert(_Srcinfo);
            LogManager.LogAssert(_DstInfo);

            _Srcinfo.m_attacker = _DstInfo.m_Attacker;
            _Srcinfo.m_nimpactcount = _DstInfo.m_nImpactCount;
            for (int nindex = _DstInfo.m_Impact.Length - 1; nindex >= 0; nindex--)
            {
                if (_DstInfo.m_Impact[nindex] == int.MaxValue)
                    continue;

                _Srcinfo.m_impact.AddLast(_DstInfo.m_Impact[nindex]);
            }
            _Srcinfo.m_spellid = _DstInfo.m_SpellID;
            _Srcinfo.m_ncount = _DstInfo.m_nCount;

            for (int i = _DstInfo.m_DefenceInfo.Length - 1; i >= 0; i--)
            {
                if (_DstInfo.m_DefenceInfo[i].m_Defencer == 0)
                    continue;

                defenceInfo _v_ = new defenceInfo();

                _v_.m_defencer = _DstInfo.m_DefenceInfo[i].m_Defencer;
                _v_.m_hit = _DstInfo.m_DefenceInfo[i].m_Hit;
                _v_.m_nimpactcount = _DstInfo.m_DefenceInfo[i].m_nImpactCount;
                for (int nindex = _DstInfo.m_DefenceInfo[i].m_Impact.Length - 1; nindex >= 0; nindex--)
                {
                    if (_DstInfo.m_DefenceInfo[i].m_Impact[nindex] == int.MaxValue)
                        continue;

                    _v_.m_impact.AddLast(_DstInfo.m_DefenceInfo[i].m_Impact[nindex]);
                }
                _v_.m_remainhp = _DstInfo.m_DefenceInfo[i].m_RemainHP;

                _Srcinfo.m_defenceinfo.AddLast(_v_);
            }
            
        }


        /// <summary>
        /// 设置gameobject及所有gameobject的子物体的layer;
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="newLayer"></param>
        public static void SetLayerRecursively(GameObject obj, int newLayer)
        {
            obj.layer = newLayer;
   
            for( int i = 0, j = obj.transform.childCount; i < j; i++)
            {
                SetLayerRecursively( obj.transform.GetChild(i).gameObject, newLayer);
            }
        }
        

        public static int GetLithCount(ObjectCard card)
        {
            HeroTemplate temp = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card.GetHeroData().TableID);
            int lithCount = temp.getReturnBack();
            int expNum = temp.getExpNum();
            int level = card.GetHeroData().Level;
            int allExp = 0;
            for (int i = 1; i < level; i++)
            {
                HeroexpTemplate heroExpTemp = (HeroexpTemplate)DataTemplate.GetInstance().m_HeroExpTable.getTableData(i);
                allExp += heroExpTemp.getExp()[expNum - 1];
            }

            lithCount += (int)((allExp + card.GetHeroData().Exp) * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

            return lithCount;
        }

        /// <summary>
        /// 返回当前资质，定位的最大星级数
        /// </summary>
        /// <param name="bron"></param>
        /// <param name="qosition"></param>
        /// <param name="quality"></param>
        /// <param name="halosPn"></param>
        public static int GetCurStarMax(int bron, int qosition, int quality)
        {
            List<HeroaddstageTemplate> AdvancedData = new List<HeroaddstageTemplate>();
            AdvancedData.Clear();

            //资质
            foreach (var item in DataTemplate.GetInstance().m_HeroaddstageTable.getData())
            {
                if (item.Value == null)
                {
                    continue;
                }

                HeroaddstageTemplate heroaddstageT = item.Value as HeroaddstageTemplate;
                if (heroaddstageT.getBorn() == bron)
                {
                    AdvancedData.Add(heroaddstageT);
                }
            }

            //定位
            for (int i = AdvancedData.Count - 1; i >= 0; i--)
            {
                if (AdvancedData[i].getQosition() != qosition)
                {
                    AdvancedData.Remove(AdvancedData[i]);
                }
            }

            int _star = -1;
            //星级
            for (int j = AdvancedData.Count - 1; j >= 0; j--)
            {
                if (AdvancedData[j].getQuality() > _star)
                {
                    _star = AdvancedData[j].getQuality();
                }
            }
            return _star;
        }

        /// <summary>
        /// 返回当前星级的最大阶数； 资质，定位，星级
        /// </summary>
        /// <param name="bron"></param>
        /// <param name="qosition"></param>
        /// <param name="quality"></param>
        /// <param name="halosPn"></param>
        public static int GetCurStarMaxHalosPn(int bron, int qosition, int quality)
        {
            List<HeroaddstageTemplate> AdvancedData = new List<HeroaddstageTemplate>();
            AdvancedData.Clear();

            //资质
            foreach (var item in DataTemplate.GetInstance().m_HeroaddstageTable.getData())
            {
                if (item.Value == null)
                {
                    continue;
                }

                HeroaddstageTemplate heroaddstageT = item.Value as HeroaddstageTemplate;
                if (heroaddstageT.getBorn() == bron)
                {
                    AdvancedData.Add(heroaddstageT);
                }
            }

            //定位
            for (int i = AdvancedData.Count - 1; i >= 0; i--)
            {
                if (AdvancedData[i].getQosition() != qosition)
                {
                    AdvancedData.Remove(AdvancedData[i]);
                }
            }

            //星级
            for (int j = AdvancedData.Count - 1; j >= 0; j--)
            {
                if (AdvancedData[j].getQuality() != quality)
                {
                    AdvancedData.Remove(AdvancedData[j]);
                }
            }

            int _halosPn = -1;
            //阶数
            for (int k = AdvancedData.Count - 1; k >= 0; k--)
            {
                if (AdvancedData[k].getHalosPn() > _halosPn)
                {
                    _halosPn = AdvancedData[k].getHalosPn();
                }
            }
            return _halosPn;
        }

        /// <summary>
        /// 根据指定的 资质，定位，星级，阶数，获取指定数据
        /// </summary>
        /// <param name="bron"></param>
        /// <param name="qosition"></param>
        /// <param name="quality"></param>
        /// <param name="halosPn"></param>
        public static HeroaddstageTemplate GetCurAdvancedData(int bron, int qosition, int quality, int halosPn)
        {
            List<HeroaddstageTemplate> AdvancedData = new List<HeroaddstageTemplate>();
            AdvancedData.Clear();

            //资质
            foreach (var item in DataTemplate.GetInstance().m_HeroaddstageTable.getData())
            {
                if (item.Value == null)
                {
                    continue;
                }

                HeroaddstageTemplate heroaddstageT = item.Value as HeroaddstageTemplate;
                if (heroaddstageT.getBorn() == bron)
                {
                    AdvancedData.Add(heroaddstageT);
                }
            }

            //定位
            for (int i = AdvancedData.Count - 1; i >= 0; i--)
            {
                if (AdvancedData[i].getQosition() != qosition)
                {
                    AdvancedData.Remove(AdvancedData[i]);
                }
            }

            //星级
            for (int j = AdvancedData.Count - 1; j >= 0; j--)
            {
                if (AdvancedData[j].getQuality() != quality)
                {
                    AdvancedData.Remove(AdvancedData[j]);
                }
            }

            //阶数
            for (int k = AdvancedData.Count - 1; k >= 0; k--)
            {
                if (AdvancedData[k].getHalosPn() == halosPn)
                {
                    return AdvancedData[k];
                }
            }
            return null;
        }

        /// <summary>
        /// 返回下一级进阶的数据
        /// </summary>
        /// <param name="herodata"></param>
        public static HeroaddstageTemplate GetHeroNextAdvancedData(ObjectCard objectCard)
        {
            HeroTemplate _HeroDataT = objectCard.GetHeroRow();//里面有资质，定位
            HeroData _HeroData = objectCard.GetHeroData();//里面有星级，阶数

            int _maxStage = GetCurStarMaxHalosPn(_HeroDataT.getBorn(), _HeroDataT.getQosition(), _HeroData.StarLevel);
            int _maxStar = GetCurStarMax(_HeroDataT.getBorn(), _HeroDataT.getQosition(), _HeroData.StarLevel);

            List<HeroaddstageTemplate> AdvancedData = new List<HeroaddstageTemplate>();
            AdvancedData.Clear();

            //资质
            foreach (var item in DataTemplate.GetInstance().m_HeroaddstageTable.getData())
            {
                if (item.Value == null)
                {
                    continue;
                }

                HeroaddstageTemplate heroaddstageT = item.Value as HeroaddstageTemplate;
                if (heroaddstageT.getBorn() == _HeroDataT.getBorn())
                {
                    AdvancedData.Add(heroaddstageT);
                }
            }

            //定位
            for (int i = AdvancedData.Count - 1; i >= 0; i--)
            {
                if (AdvancedData[i].getQosition() != _HeroDataT.getQosition())
                {
                    AdvancedData.Remove(AdvancedData[i]);
                }
            }

            //星级
            for (int j = AdvancedData.Count - 1; j >= 0; j--)
            {
                if (AdvancedData[j].getQuality() != _HeroData.StarLevel)
                {
                    AdvancedData.Remove(AdvancedData[j]);
                }
            }

            if (_HeroData.CurStage + 1 > _maxStage)//下一阶大于了最大阶数 返回下一星级,0阶  攻击
            {
                if (_HeroData.StarLevel + 1 > _maxStar)
                {
                     return GetCurAdvancedData(_HeroDataT.getBorn(), _HeroDataT.getQosition(), _HeroData.StarLevel, _HeroData.CurStage);
                }
                return GetCurAdvancedData(_HeroDataT.getBorn(), _HeroDataT.getQosition(), _HeroData.StarLevel + 1, 0);
            }

            //阶数
            for (int k = AdvancedData.Count - 1; k >= 0; k--)
            {                               
                if (AdvancedData[k].getHalosPn() == _HeroData.CurStage + 1)
                {
                        return AdvancedData[k];
                }
            }
            return null;
        }

        /// <summary>
        /// 返回当前资质，定位,元素类型，元素等级的 表数据
        /// </summary>
        /// <param name="bron"></param>
        /// <param name="qosition"></param>
        /// <param name="Element"></param>
        /// <param name="ElementLeve"></param>
        /// <returns></returns>
        public static HerocultureTemplate GetCurCultureTData(int bron, int qosition, int Element, int ElementLeve)
        {
            List<HerocultureTemplate> herocultureData = new List<HerocultureTemplate>();
            herocultureData.Clear();

            //资质
            foreach (var item in DataTemplate.GetInstance().m_HerocultureTable.getData())
            {
                if (item.Value == null)
                {
                    continue;
                }

                HerocultureTemplate herocultureT = item.Value as HerocultureTemplate;
                if (herocultureT.getBorn() == bron)
                {
                    herocultureData.Add(herocultureT);
                }
            }

            //定位
            for (int i = herocultureData.Count - 1; i >= 0; i--)
            {
                if (herocultureData[i].getQosition() != qosition)
                {
                    herocultureData.Remove(herocultureData[i]);
                }
            }

            //元素类型
            for (int i = herocultureData.Count - 1; i >= 0; i--)
            {
                if (herocultureData[i].getElement() != Element)
                {
                    herocultureData.Remove(herocultureData[i]);
                }
            }

            if (ElementLeve == 0)
                return null;

            //元素等级
            for (int i = herocultureData.Count - 1; i >= 0; i--)
            {
                if (herocultureData[i].getElementLeve() == ElementLeve)
                {
                    return(herocultureData[i]);
                }
            }

            return null;
        }

        /// <summary>
        /// 返回当前资质，定位,元素类型，的最大等级
        /// </summary>
        /// <param name="bron"></param>
        /// <param name="qosition"></param>
        /// <param name="Element"></param>
        /// <param name="ElementLeve"></param>
        /// <returns></returns>
        public static int GetCurCultureTData(int bron, int qosition, int Element)
        {
            List<HerocultureTemplate> herocultureData = new List<HerocultureTemplate>();
            herocultureData.Clear();

            //资质
            foreach (var item in DataTemplate.GetInstance().m_HerocultureTable.getData())
            {
                if (item.Value == null)
                {
                    continue;
                }

                HerocultureTemplate herocultureT = item.Value as HerocultureTemplate;
                if (herocultureT.getBorn() == bron)
                {
                    herocultureData.Add(herocultureT);
                }
            }

            //定位
            for (int i = herocultureData.Count - 1; i >= 0; i--)
            {
                if (herocultureData[i].getQosition() != qosition)
                {
                    herocultureData.Remove(herocultureData[i]);
                }
            }

            //元素类型
            for (int i = herocultureData.Count - 1; i >= 0; i--)
            {
                if (herocultureData[i].getElement() != Element)
                {
                    herocultureData.Remove(herocultureData[i]);
                }
            }

            int ElementMaxLeve = -1;
            //元素等级
            for (int i = herocultureData.Count - 1; i >= 0; i--)
            {
                if (herocultureData[i].getElementLeve() > ElementMaxLeve)
                {
                    ElementMaxLeve = herocultureData[i].getElementLeve();
                }
            }

            return ElementMaxLeve;
        }

        /// <summary>
        /// 返回类型的最大等级
        /// </summary>
        private int GetFixedLevelMsT(int type)
        {
            MsTemplate _MsDataT = null;
            //查找对应类型的表数据
            foreach (var item in DataTemplate.GetInstance().m_MsTable.getData())
            {
                if (item.Value == null)
                {
                    continue;
                }

                MsTemplate MsDataT = item.Value as MsTemplate;
                if (MsDataT.getId() == type + 1)
                {
                    _MsDataT = MsDataT;
                }
            }

            int _MaxLevel = -1;
            for (int i = 0; i < _MsDataT.getLevel().Length;i++ )
            {
                if (_MsDataT.getLevel()[i] > _MaxLevel)
                    _MaxLevel = _MsDataT.getLevel()[i];
            }
            return _MaxLevel;
        }

        public static char[] unitChar = new char[]{
            ' ','十','百','千','万'
        };
        /// <summary>
        /// 数字变成汉语数字（最多支持5位数，不支持负数）;
        /// 1024 => 一千零二十四;
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ConverIntToString(int num)
        {
            char[] numChar = num.ToString().ToCharArray();

            StringBuilder sb = new StringBuilder();
            for (int i = 0, j = numChar.Length; i < j; i++)
            {
                sb.Append(ConverCharToWord(numChar[i]));
                if (numChar[i] != '0')
                {
                    sb.Append(unitChar[j - i - 1]);
                }
            }

            //去掉后面连续为'零'的字符;
            for (int m = sb.Length - 1; m >= 0; m--)
            {
                if (sb[m].Equals('零'))
                {
                    sb.Remove(m, 1);
                }
                else
                    break;
            }

            if (numChar.Length == 2 && numChar[0] == '1')
            {
                sb.Remove(0, 1);
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// 数字变成汉语数字;
        /// 1 => 一
        /// 2 => 二
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static char ConverCharToWord(char num)
        {
            switch (num)
            {
                case '0': return GameUtils.getString("shuzi0").ToCharArray()[0];
                case '1': return GameUtils.getString("shuzi1").ToCharArray()[0];
                case '2': return GameUtils.getString("shuzi2").ToCharArray()[0];
                case '3': return GameUtils.getString("shuzi3").ToCharArray()[0];
                case '4': return GameUtils.getString("shuzi4").ToCharArray()[0];
                case '5': return GameUtils.getString("shuzi5").ToCharArray()[0];
                case '6': return GameUtils.getString("shuzi6").ToCharArray()[0];
                case '7': return GameUtils.getString("shuzi7").ToCharArray()[0];
                case '8': return GameUtils.getString("shuzi8").ToCharArray()[0];
                case '9': return GameUtils.getString("shuzi9").ToCharArray()[0];
                default:
                    Debug.Log("只支持1-9个数");
                    return ' ';
            }
        }

        /// <summary>
        /// 数字变成汉语数字;
        /// 1 => 一
        /// 2 => 二
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
		public static char ConverIntToWord(int num)
        {
            switch(num)
            {
                case 0: return GameUtils.getString("shuzi0").ToCharArray()[0];
                case 1: return GameUtils.getString("shuzi1").ToCharArray()[0];
                case 2: return GameUtils.getString("shuzi2").ToCharArray()[0];
                case 3: return GameUtils.getString("shuzi3").ToCharArray()[0];
                case 4: return GameUtils.getString("shuzi4").ToCharArray()[0];
                case 5: return GameUtils.getString("shuzi5").ToCharArray()[0];
                case 6: return GameUtils.getString("shuzi6").ToCharArray()[0];
                case 7: return GameUtils.getString("shuzi7").ToCharArray()[0];
                case 8: return GameUtils.getString("shuzi8").ToCharArray()[0];
                case 9: return GameUtils.getString("shuzi9").ToCharArray()[0];
                default:
                    Debug.Log("只支持1-9个数");
                    return ' ';
            }
        }

        public static string ConverQualityToStr(int quality)
        {
            switch (quality)
            {
                case 1: return GameUtils.getString("yingxiongpinzhi01");
                case 2: return GameUtils.getString("yingxiongpinzhi02");
                case 3: return GameUtils.getString("yingxiongpinzhi03");
                case 4: return GameUtils.getString("yingxiongpinzhi04");
                case 5: return GameUtils.getString("yingxiongpinzhi05");
                case 6: return GameUtils.getString("yingxiongpinzhi06");
                default:
                    Debug.Log("只支持1-6个品质");
                    return string.Empty;
            }
        }


        /// <summary>
        /// 获取装备的背景 
        /// 传入装备的表ID
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static string GetEquipBgColor ( int tableid )
        {
            EquipmentqualityTemplate temp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( tableid );
            int color = temp.getQialityColor ();
            string ret = "";
            switch ( color )
            {
                case 1:
                    ret = "img_TY_0039";
                    break;
                case 2:
                    ret = "img_TY_0026";
                    break;
                case 3:
                    ret = "img_TY_0025";
                    break;
                case 4:
                    ret = "img_TY_0030";
                    break;
                case 5:
                    ret = "img_TY_0021";
                    break;
                case 6:
                    ret = "img_TY_0031";
                    break;
                default:
                    break;
            }

            return ret;
        }

        /// <summary>
        /// 获取装备的颜色
        /// 传入装备的表ID
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static Color GetEquipNameColor ( int tableid )
        {
            EquipmentqualityTemplate temp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( tableid );
            int id = temp.getQialityColor ();
            Color color = Color.white;
            switch ( id )
            {
                case 1:
                    color = Color.white;
                    break;
                case 2:
                    color = Color.green;
                    break;
                case 3:
                    color = Color.blue;
                    break;
                case 4:
                    color = Color.gray;
                    break;
                case 5:
                    color = Color.white;
                    break;
                case 6:
                    color = Color.red;
                    break;
                default:
                    break;
            }

            return color;
        }

        public static Color GetEquipNameAttr ( int tableid )
        {
            EquipmentqualityTemplate temp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( tableid );
            int id = temp.getQialityColor ();
            Color color = Color.white;
            switch ( id )
            {
                case 1:
                    color = Color.white;
                    break;
                case 2:
                    color = Color.green;
                    break;
                case 3:
                    color = Color.blue;
                    break;
                case 4:
                    color = Color.gray;
                    break;
                case 5:
                    color = Color.white;
                    break;
                case 6:
                    color = Color.red;
                    break;
                default:
                    break;
            }

            return color;
        }
        /// <summary>
        /// 根据道具id获得道具背景
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static Sprite GetItemQualitySprite(int tableid)
        {
            ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(tableid);
            Sprite _sprite = null;
            if (itemT != null)
            {
                switch (itemT.getQuality())
                {
                    case -1:
                        _sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0024");
                        break;
                    case 1:
                        _sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0073");
                        break;
                    case 2:
                        _sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0075");
                        break;
                    case 3:
                        _sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0077");
                        break;
                    case 4:
                        _sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0081");
                        break;
                    case 5:
                        _sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0079");
                        break;
                }
            }
            return _sprite;
        }

        public static string GetHeroNameFontColor(int quality)
        {
            switch (quality)
            {
                case 1:
                    return "<color=#FFFFFF>{0}</color>";//白
                case 2:
                    return "<color=#0fcf00>{0}</color>";//绿
                case 3:
                    return "<color=#1697d1>{0}</color>";//蓝
                case 4:
                    return "<color=#A600FB>{0}</color>";//紫
                case 5:
                    return "<color=#ee981d>{0}</color>";//橙
                case 6:
                    return "<color=#FF0000>{0}</color>";//红
                default:
                    return "<color=#FFFFFF>{0}</color>";
            }
        }

        public static string GetSkillColorClear(int index)
        {
            string str = "";
            switch (index)
            {
                case 1:
                    str = GameUtils.getString("yingxiongpinzhi01");
                    break;
                case 2:
                    str = GameUtils.getString("yingxiongpinzhi02");
                    break;
                case 3:
                    str = GameUtils.getString("yingxiongpinzhi03");
                    break;
                case 4:
                    str = GameUtils.getString("yingxiongpinzhi04");
                    break;
                case 5:
                    str = GameUtils.getString("yingxiongpinzhi05");
                    break;
                case 6:
                    str = GameUtils.getString("yingxiongpinzhi06");
                    break;
                default:
                    break;
            }
            return string.Format(getString("skillunlockquality"),str);
        }
    }

}