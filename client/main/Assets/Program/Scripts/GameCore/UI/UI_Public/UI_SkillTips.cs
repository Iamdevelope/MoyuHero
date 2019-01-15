using UnityEngine;
using System.Collections;
using DreamFaction.Utils;
using System.Text;
using System.Text.RegularExpressions;
namespace DreamFaction.UI
{
    /// <summary>
    /// 技能描述Tips
    /// </summary>
    public class UI_SkillTips : BaseUITips
    {
        private ObjectCard m_card;
        private SkillTemplate m_skill;
        public UI_SkillTips(ObjectCard _card,SkillTemplate _skill)
        {
            m_card = _card;
            m_skill = _skill;
        }


        public override string SetShow()
        {
            if (m_card == null || m_skill == null)
                return string.Empty;
            string _text = GameUtils.getHeroString(m_skill.getSkillDes(), m_card).Replace("\\n", "\n");
            return _text;
        }
        /// <summary>
        /// 解析出技能描述 只有描述 没有技能消耗和冷却时间
        /// </summary>
        /// <returns></returns>
        public string GetDesc()
        {
          string  strHero=  GameUtils.getHeroString(m_skill.getSkillDes(), m_card).Replace("\\n", "\n").Replace(" ","");
            //分割出标题和描述部分
          string strDesc = strHero.Split('\n')[1];
          try
          {
              if (strDesc.Length > 0)
              {
                  //获取<size=14>的index
                  string temp = strDesc;
                  Regex resize = new Regex("<size=14>");
                  Regex resizeend = new Regex("</size>");
                  Regex recolor = new Regex("color");
                  Regex recolorend = new Regex("</color>");
                  Regex xiaohao=new Regex("技能消耗");
                  for (int i = 1; i > 0; i++)
                  {
                      if (resize.IsMatch(temp))  //先删除 <size=14>
                      {
                          int deleteIndex = resize.Match(temp).Index;
                          int deleteLength = resize.Match(temp).Length;
                          temp = temp.Remove(deleteIndex, deleteLength);//得到一个新的字符串
                      }
                      else if (resizeend.IsMatch(temp))   //删除</size>
                      {
                          int deleteIndex = resizeend.Match(temp).Index;
                          int deleteLength = resizeend.Match(temp).Length;
                          temp = temp.Remove(deleteIndex, deleteLength);//得到一个新的字符串
                      }
                      else if (recolorend.IsMatch(temp))   //删除</color>
                      {
                          int deleteIndex = recolorend.Match(temp).Index;
                          int deleteLength = recolorend.Match(temp).Length;
                          temp = temp.Remove(deleteIndex, deleteLength);//得到一个新的字符串
                      }
                      else if (recolor.IsMatch(temp))  //删除<color=#sssss>
                      {
                          int deleteIndex = recolor.Match(temp).Index - 1;
                          int deleteLength = recolor.Match(temp).Length + 10;
                          temp = temp.Remove(deleteIndex, deleteLength);//得到一个新的字符串
                      }
                      else if (xiaohao.IsMatch(temp))   //删除技能消耗 后面的所有字符
                      {
                          int deleteIndex = xiaohao.Match(temp).Index;
                          temp = temp.Remove(deleteIndex);
                          temp = temp.Trim(); //去除尾部的空白字符
                          string endChar = temp.Substring(temp.Length - 1, 1);
                          if (endChar == "," || endChar == ";" || endChar == "。" ||endChar=="，")
                          {
                                  temp = temp.Remove(temp.Length - 1);
                            
                          }
                      }
                      else
                      {
                          break;
                      }
                  }
                  return temp;
              }
              else
              {
                  return "";
              }
          }
          catch (System.Exception)
          {
              return "";
          }
        
        }
    }
}

