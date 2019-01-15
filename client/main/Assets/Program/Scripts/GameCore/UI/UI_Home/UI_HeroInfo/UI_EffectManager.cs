using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameSceneEditor;
using DreamFaction.Utils;
public class UI_EffectManager : BaseControler
{
    public static UI_EffectManager _instance;
    List<UI_EffectStatic> m_EffectStatic=new List<UI_EffectStatic>();
   
   protected override void InitData()
   {
       base.InitData();

           _instance = this;
     
   }

   protected override void UpdateState()
   {
       base.UpdateState();
       for (int i = 0; i < m_EffectStatic.Count; ++i)
       {
           if (m_EffectStatic[i].GetGameObject() == null)
           {
               m_EffectStatic.RemoveAt(i);
               continue;
           }
           if (m_EffectStatic[i].GetIsActivation())
           {
               m_EffectStatic[i].OnUpdateState();
           }
       }
   }

    public UI_EffectStatic GetUI_EffectStatic(string name)
   {
       for (int i = 0; i < m_EffectStatic.Count; ++i)
       {
           if (m_EffectStatic[i].GetGameObject() != null && m_EffectStatic[i].GetEffectName().Equals(name) && m_EffectStatic[i].GetIsActivation() == false)
           {
               return m_EffectStatic[i];
           }

       }
       return null;
   }

    public UI_EffectStatic GetUI_EnableEffect(string name)
    {
        for (int i = 0; i < m_EffectStatic.Count; ++i)
        {
            if (m_EffectStatic[i].GetGameObject() != null && m_EffectStatic[i].GetEffectName().Equals(name) && m_EffectStatic[i].GetIsActivation() == true)
            {
                return m_EffectStatic[i];
            }

        }
        return null;
    }


    public void DisableEffect(string effectName)
    {
        for (int i = m_EffectStatic.Count-1; i >= 0 ; i--)
        {
            if (m_EffectStatic[i].GetGameObject() != null && m_EffectStatic[i].GetEffectName().Equals(effectName))
            {
                GameObject.Destroy(m_EffectStatic[i].GetGameObject());
                m_EffectStatic.RemoveAt(i);
            }

        }
    }

    public void InstanceEffect_Link(string effectName, Transform pos)
   {
      UI_EffectStatic effect=GetUI_EffectStatic(effectName);
      if (effect!=null&& effect.GetGameObject() != null)
      {
          effect.ResetTime();
          effect.SetActivation(true);
      }
       else
       {
           GameObject obj = Instantiate(Resources.Load(common.EffectPath + effectName), pos.position, pos.rotation) as GameObject;
           if (obj != null)
           {
               UI_EffectStatic newEffect = new UI_EffectStatic();
               obj.transform.parent = pos;
               //obj.l = "Effects";
               newEffect.ResetTime();
               newEffect.SetGameObject(obj);
               newEffect.SetEffectName(effectName);
               //newEffect.SetActivation(true);
               m_EffectStatic.Add(newEffect);
           }
       }
       

   }

}
