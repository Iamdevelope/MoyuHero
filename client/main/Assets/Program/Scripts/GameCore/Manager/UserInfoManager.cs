using UnityEngine;
using System.Collections;
/// <summary>
/// 用户信息管理器，负责加载账号信息，保存账号信息，删除账号信息，注册账号，接收反馈，发送账号信息给游戏服务器等等！
/// 账号注册暂定为通过第三方账号系统获取！
/// </summary>
namespace DreamFaction.GameCore
{
    class UserInfoManager : BaseControler
    {
        public static UserInfoManager Inst;  // 单例

        // ==================================================================
        // 初始化，更新，删除
        // ==================================================================
        // 游戏初始化操作 -- 其他系统的初始化参见脚本执行顺序（MonoManager）
        protected override void InitData()
        {
            Inst = this;
            // 初始化第三方账号管理系统
            // TODO....
        }
        // 删除操作
        protected override void DestroyData()
        {
            Inst = null;
        }
    } 
}
