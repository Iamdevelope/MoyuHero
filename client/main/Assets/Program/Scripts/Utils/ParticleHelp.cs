using UnityEngine;
using System.Collections;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;

namespace DreamFaction.Utils
{
    public class ParticleHelp : MonoBehaviour
    {
        private float lowPoint = 0.5f;
        private float start_emissionRate = 0.0f; // 起始粒子发射速率
        private float start_minEmission = 0.0f;  // 最小发射数
        private float start_maxEmission = 0.0f; // 最大发射数

        void Awake()
        {
            Init();
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_GameQualityChanged, OnQualityChanged);
        }

        void OnDestroy() 
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_GameQualityChanged, OnQualityChanged);
        }

        // Use this for initialization
        void Start()
        {
            ResetParticle();
        }

        void Init()
        {
            if (particleSystem)
            {
                start_emissionRate = particleSystem.emissionRate; // 初始粒子发射速率
            }
            else if (particleEmitter)
            {
                start_minEmission = particleEmitter.minEmission;  // 初始最小发射数
                start_maxEmission = particleEmitter.maxEmission; // 初始最大发射数
            }
        }

        void ResetParticle()
        {
            if (ConfigsManager.Inst.GetClientConfig(ClientConfigs.Quality) == ((int)GameQuality.Low).ToString())
            {
                if (particleSystem)
                {
                    particleSystem.emissionRate *= lowPoint; // 粒子发射速率减半
                }
                else if (particleEmitter)
                {
                    particleEmitter.minEmission *= lowPoint;
                    particleEmitter.maxEmission *= lowPoint;
                }
            }
            else
            {
                if (particleSystem)
                {
                    particleSystem.emissionRate = start_emissionRate; 
                }
                else if (particleEmitter)
                {
                    particleEmitter.minEmission = start_minEmission;
                    particleEmitter.maxEmission = start_maxEmission;
                }
            }
        }


        // =========== 事件回调 =========
        private void OnQualityChanged()
        {
            //Debug.Log("粒子系统：" + this.name + " 收到Quality改变的消息！");
            ResetParticle();
        }
    }
}
