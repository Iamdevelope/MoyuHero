using DreamFaction.SkillCore;
using DreamFaction.GameNetWork;

namespace DreamFaction.GameEventSystem
{
    public class EventRequestSkillPackage{
        /// <summary>
        /// 技能拥有者
        /// </summary>
        public readonly X_GUID mOwner = new X_GUID();
        /// <summary>
        /// 技能id
        /// </summary>
        public readonly int mSkillID;
        /// <summary>
        /// 目标对象
        /// </summary>
        public readonly ObjectCreature mTarget;
        /// <summary>
        /// 是否对我方有效
        /// </summary>
        public readonly bool isForSelf;
        /// <summary>
        /// 是否对自己有效
        /// </summary>
        public readonly bool isMyEff;

        public EventRequestSkillPackage(X_GUID uid, int skillid, bool isSelf,bool isMy = true)
        {
            mOwner.Copy(uid);
            mSkillID = skillid;
            isForSelf = isSelf;
            isMyEff = isMy;
        }
        public EventRequestSkillPackage(X_GUID uid, int skillid, ObjectCreature obj)
        {
            mOwner.Copy(uid);
            mSkillID = skillid;
            mTarget = obj;
        }
    }

    /// <summary>
    /// buff 更新事件包
    /// </summary>
    public class BuffUpdatePackage
    {
        public bool isAdd;  // 是否是添加buf
        public ObjectCreature creature; // 目标角色
        public Impact pImpact;  // 当前buf
    }

    public class SkillShowNamePackage
    {
        public X_GUID pOwner = new X_GUID();
        public string strName;

        public SkillShowNamePackage(X_GUID uid, string name)
        {
            pOwner.Copy(uid);
            strName = name;
        }
    }
}
