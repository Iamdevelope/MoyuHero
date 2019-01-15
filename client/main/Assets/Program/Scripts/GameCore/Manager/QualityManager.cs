using UnityEngine;
using System.Collections;
using DreamFaction.GameEventSystem;

namespace DreamFaction.GameCore
{
    /// <summary>
    /// 游戏品质
    /// </summary>
    public enum GameQuality
    {
        Low = 6,    // 低
        Hight = 7, // 高
    }
    public class QualityManager : BaseControler
    {
        // 单例
        public static QualityManager Inst;
        private GameQuality m_GameQuality;

        // =================== 继承 ============================
        protected override void InitData()
        {
            Inst = this;
            //初始化配置
            string quality = ConfigsManager.Inst.GetClientConfig(ClientConfigs.Quality);
            if (quality == string.Empty)
                SetGameQuality(GameQuality.Hight); // 默认画质 ：高
            else
                SetGameQuality((GameQuality)int.Parse(quality));
            
        }

        //  ======================= 公共接口 ===========================
        // 1:设置当前游戏品质
        public void SetGameQuality(GameQuality quality)
        {
            if (m_GameQuality == quality)
                return;
            m_GameQuality = quality;
            QualitySettings.SetQualityLevel((int)m_GameQuality);
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.Quality, ((int)quality).ToString());
            //Debug.Log("游戏品质发生改变：" + quality);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_GameQualityChanged);
            // 开启/关闭 其他渲染优化操作！
            // TODO....

        }

        // 2: 获取当前游戏品质
        public GameQuality GetGameQuality()
        {
            return m_GameQuality;
        }
    }
}
