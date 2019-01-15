using System;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using GNET;
/// <summary>
///   神器管理器。用一个map管理所有的客户端已有的神器。key为神器的类型，有且仅为唯一。value为数据结构[5/26/2015 Zmy]
/// </summary>
public class ArtifactContainer
{
    private Dictionary<int, Artifact> m_ArtifactMap = new Dictionary<int, Artifact>();// PS:因为key的特殊性起始从1开始。遍历逻辑要防止遗漏最后一个key为map.count的value [5/27/2015 Zmy]

    public ArtifactContainer()
    {
        m_ArtifactMap.Clear();
    }
    public Dictionary<int, Artifact> GetArtifactMap() { return m_ArtifactMap; }
    public int GetArtifactMapCount() { return m_ArtifactMap.Count; }
    public Artifact GetArtifactData(int nKey)
    {
        if (m_ArtifactMap.ContainsKey(nKey))
        {
            return m_ArtifactMap[nKey];
        }
        return null;
    }
    public void InitArtifactMap(int nType, GNET.Artifact _info)
    {
        if (_info == null)
        {
            LogManager.LogError("!!!!!Error:InitArtifactMap is null ,The ArtifactType:" + nType);
            LogManager.LogToFile("!!!!!Error:InitArtifactMap is null ,The ArtifactType:" + nType);
            return;
        }
        Artifact _data = new Artifact();
        _data.InitArtifactData(_info);
        m_ArtifactMap.Add(nType, _data);
    }

    public void ClearArtifactMap()
    {
        m_ArtifactMap.Clear();
    }
	
    public void RefeashArtifact(GNET.Artifact _info)
    {
        if (m_ArtifactMap.ContainsKey(_info.artifacttype))
        {
            m_ArtifactMap[_info.artifacttype].InitArtifactData(_info);
        }
    }

    // 刷新神器激活状态 [5/27/2015 Zmy]
    public bool UpdateArtifactActivateState()
    {
        bool _isRefeash = false;    //是否需要刷新
        bool _isChange  = false;    //是否有改变
        for (int i = 0; i <= m_ArtifactMap.Count; i++)
        {
            if (m_ArtifactMap.ContainsKey(i))
            {
                _isChange = m_ArtifactMap[i].UpdateActivateState();
                if (_isChange)
                    _isRefeash = true;
                
            }
        }

        return _isRefeash;
    }
    //获取全部神器的某一属性加成值总和（包括已升级的属性加成） [5/27/2015 Zmy]
    public int GetAttributeTotal(EM_ARTIFACT_ATTRIBUTE_TYPE _type)
    {
        int nSum = 0;
        switch (_type)
        {
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAXHP:
                for (int i = 0; i <= m_ArtifactMap.Count;i++ )
                {
                    if (m_ArtifactMap.ContainsKey(i))
                    {
                        nSum += m_ArtifactMap[i].GetMaxHP();
                    }
                }
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_PHYSICALATTACK:
                for (int i = 0; i <= m_ArtifactMap.Count; i++)
                {
                    if (m_ArtifactMap.ContainsKey(i))
                    {
                        nSum += m_ArtifactMap[i].GetPhysicalAttack();
                    }
                }
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_PHYSICALDEFENCE:
                for (int i = 0; i <= m_ArtifactMap.Count; i++)
                {
                    if (m_ArtifactMap.ContainsKey(i))
                    {
                        nSum += m_ArtifactMap[i].GetPhysicalDefence();
                    }
                }
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAGICATTACK:
                for (int i = 0; i <= m_ArtifactMap.Count; i++)
                {
                    if (m_ArtifactMap.ContainsKey(i))
                    {
                        nSum += m_ArtifactMap[i].GetMagicAttack();
                    }
                }
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAGICDEFENCE:
                for (int i = 0; i <= m_ArtifactMap.Count; i++)
                {
                    if (m_ArtifactMap.ContainsKey(i))
                    {
                        nSum += m_ArtifactMap[i].GetMagicDefence();
                    }
                }
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_HIT:
                for (int i = 0; i <= m_ArtifactMap.Count; i++)
                {
                    if (m_ArtifactMap.ContainsKey(i))
                    {
                        nSum += m_ArtifactMap[i].GetHit();
                    }
                }
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_DODGE:
                for (int i = 0; i <= m_ArtifactMap.Count; i++)
                {
                    if (m_ArtifactMap.ContainsKey(i))
                    {
                        nSum += m_ArtifactMap[i].GetDodge();
                    }
                }
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_CRITICAL:
                for (int i = 0; i <= m_ArtifactMap.Count; i++)
                {
                    if (m_ArtifactMap.ContainsKey(i))
                    {
                        nSum += m_ArtifactMap[i].GetCritical();
                    }
                }
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_TENACITY:
                for (int i = 0; i <= m_ArtifactMap.Count; i++)
                {
                    if (m_ArtifactMap.ContainsKey(i))   
                    {
                        nSum += m_ArtifactMap[i].GetTenacity();
                    }
                }
                break;
            default:
                break;
        }

        return nSum;
    }
}
