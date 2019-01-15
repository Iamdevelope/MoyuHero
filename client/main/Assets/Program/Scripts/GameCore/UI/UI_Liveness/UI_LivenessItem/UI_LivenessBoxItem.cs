using UnityEngine;
using System.Collections;
using DreamFaction.Utils;
using UnityEngine.UI;


public class UI_LivenessBoxItem : UI_LivenessBoxItemBase {

    private int m_ActiveNum;
    public int Index;
    public void Data(object data,int index)
    {
        m_ActiveNum = (int)data;
        Index = index;

        this.m_ActiveNumText.text = m_ActiveNum.ToString() + GameUtils.getString("liveness_content2");
        if (canOpen())
        {
            this.m_ActiveNumText.color = Color.white;
        }
        else
        {
            this.m_ActiveNumText.color = new Color(0.54f,0.51f,0.61f);
        }
        

        bool f = m_ActiveNum == 150;
        if (f)
        {
            m_NormalBox.gameObject.SetActive(false);
            m_AdvancedBox.gameObject.SetActive(true);
            if (canOpen())
            {
                m_PointImageHui.gameObject.SetActive(false);
                m_PointImageLight.gameObject.SetActive(true);
                if (isOpend())
                {
                    m_NormalLight.gameObject.SetActive(false);
                    m_AdvancedBox.gameObject.SetActive(false);
                    m_AdvancedOpen.gameObject.SetActive(true);
                }
                else
                {
                    m_NormalLight.gameObject.SetActive(true);
                }
            }
            else
            {
                m_PointImageHui.gameObject.SetActive(true);
                m_PointImageLight.gameObject.SetActive(false);
                m_NormalLight.gameObject.SetActive(false);
            }  
        }
        else
        {
            m_NormalBox.gameObject.SetActive(true);
            m_AdvancedBox.gameObject.SetActive(false);
            if (canOpen())
            {
                m_PointImageHui.gameObject.SetActive(false);
                m_PointImageLight.gameObject.SetActive(true);
                if (isOpend())
                {
                    m_NormalLight.gameObject.SetActive(false);
                    m_NormalBox.gameObject.SetActive(false);
                    m_Opened.gameObject.SetActive(true);
                }
                else
                {
                    m_NormalLight.gameObject.SetActive(true);
                }
            }
            else
            {
                m_PointImageHui.gameObject.SetActive(true);
                m_PointImageLight.gameObject.SetActive(false);
                m_NormalLight.gameObject.SetActive(false);
            }
        }
    }

    public bool canOpen()
    {
        if (UI_Liveness._instance.m_LivenessNum >= m_ActiveNum)
        {
            return true;
        }
        return false;
    }

    //开没开？
    public bool isOpend()
    {
        int _result = 0;
        _result = UI_Liveness._instance.isLivenessBox % (int)Mathf.Pow(10, Index + 1);        
        return _result == 0 ? false : _result / Mathf.Pow(10, Index) >= 1.0f;
        
    }
}
