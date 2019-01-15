////////////////////////////////////////////////////////////////////////////////
//  
// @module �¼�ϵͳ
// @author ����
////////////////////////////////////////////////////////////////////////////////
/// <namespace>
/// <summary>�¼�ϵͳ</summary>
/// <remarks>���Զ�����¼�ϵͳ������AS3д�ģ�</remarks>
/// </namespace>
namespace DreamFaction.GameEventSystem
{
    /// <summary>
    /// ��Ϸ�¼�ϵͳ���е��¼�ID���嶼�����Struct��<br/> 
    /// DreamFaction.NetWork ���������¼�    ID���� 100000 - 199999  "Net_"��ͷ��ʾ NetEvent<br/> 
    /// DreamFaction.UI UIģ�鴥�����¼�     ID���� 200000 - 299999  "U_"��ͷ��ʾ UserInputEvent<br/> 
    /// DreamFaction.GameCore ��Ϸ�ڴ����¼� ID���� 300000 - 399999  "G_"��ͷ��ʾ GameEvent<br/> 
    /// DreamFaction.GameSceneEditor �ؿ��༭�������¼� ID���� 400000 - 499999 "SE_"��ͷ��ʾ SceneEditor<br/> 
    /// ע�⣺�¼�ID��UI��LUA��Ҫ���¶��岢ʹ�ã�Ϊ�˱���������ﶼҪ��ʾ��д��IDֵ��<br/> 
    /// </summary>
    public enum GameEventID
    {
        //--------------------------------------
        // DreamFaction.NetWork ���������¼����¼�ID��UI��Ҫ���¶���Ϊ�˱���������ﶼҪ��ʾ��д��IDֵ
        // ID���� 100000 - 199999
        // "N_"��ͷ��ʾ NetEvent
        //--------------------------------------
        #region WWW����web�˺ŷ���������֤�˺���Ϣ��صĻص��¼��� ��ʼID��100000
        /// <summary>
        /// �¼����˺�ע��ɹ�
        /// </summary>
        Net_RegistOK                = 100000,
        /// <summary>
        /// �¼����˺�ע��ʧ�ܣ�ʧ��ԭ�����¼�������!
        /// </summary>
        Net_RegistUnOK              = 100001,
        /// <summary>
        /// �¼��������˺�ע���������ַʧ�ܣ�ʧ��ԭ�����¼������У�
        /// </summary>
        Net_RegistError             = 100002,
        /// <summary>
        /// �¼�����½�˺ŷ������ɹ���
        /// </summary>
        Net_LoginOK                 = 100003,
        /// <summary>
        /// �¼�����½�˺ŷ�����ʧ�ܣ�ʧ��ԭ�����¼�������!
        /// </summary>
        Net_LoginUnOK               = 100004,
        /// <summary>
        /// �¼��������˺ŵ�½��������ַʧ�ܣ�ʧ��ԭ�����¼������У�
        /// </summary>
        Net_LoginError              = 100005,
        /// <summary>
        /// �¼��������豸ID�˺���Ϣ���ִ��� <br/>
        /// 1 �����Ѱ��˻� 0����û�а󶨹��˻� -1����û��ע��� -2���������쳣 -3����������ǿ�
        /// </summary>
        Net_RequestError            = 100006,
        /// <summary>
        /// �¼��������豸ID�˺���ϢOK�� <br/>
        /// 1 �����Ѱ��˻� 0����û�а󶨹��˻� -1����û��ע��� -2���������쳣 -3����������ǿ�
        /// </summary>
        Net_RequestOK               = 100007,
        #endregion

        #region ����������صĻص��¼�����ʼID��100010
        /// <summary>
        /// ������Ϸ������OK
        /// </summary>
        Net_ConnectGameServerOK     = 100010,
        /// <summary>
        /// �������ӳ����Ͽ����ӣ�
        /// </summary>
        Net_ConnectGameServerUnOK   = 100011,
        /// <summary>
        /// ������Ϸ������ʱ����
        /// </summary>
        Net_ConnectGameServerError  = 100012,
        /// <summary>
        /// ��½����Ϸ�������ɹ���
        /// </summary>
        Net_LoginGameServerOK       = 100013,
        /// <summary>
        /// ��½����Ϸ������ʧ�ܣ��¼��������о����ʧ��ԭ��
        /// </summary>
        Net_LoginGameServerUnOK     = 100014,
        #endregion

        #region ������ԣ����ݷ����仯��֪ͨ�¼� MCHumanDetailAttribute ��ʼID��100020
        /// <summary>
        /// ��ҡ������������仯
        /// </summary>
        Net_MCHumanDetailAttribute_Name = 100020,
        /// <summary>
        /// ��ҡ��ȼ��������仯
        /// </summary>
        Net_MCHumanDetailAttribute_Level = 100021,
        /// <summary>
        /// ��ҡ����顱�����仯
        /// </summary>
        Net_MCHumanDetailAttribute_Exp  = 100022,
        /// <summary>
        /// ��ҡ��ж���/�����������仯
        /// </summary>
        Net_MCHumanDetailAttribute_ActionPoint  = 100023,
        /// <summary>
        /// ��ҡ���ҡ������仯
        /// </summary>
        Net_MCHumanDetailAttribute_Money        = 100024,
        /// <summary>
        /// ��ҡ���ֵ�ҡ������仯
        /// </summary>
        Net_MCHumanDetailAttribute_Gold         = 100025,
        /// <summary>
        /// ��ҡ�VIP�ȼ��������仯
        /// </summary>
        Net_MCHumanDetailAttribute_VipLevel     = 100026,
        /// <summary>
        /// ��ҡ�VIP���顱�����仯
        /// </summary>
        Net_MCHumanDetailAttribute_VipExp       = 100027,
        /// <summary>
        /// ������Ϣ��ȡ���׼��������Ϸ
        /// </summary>
        Net_MCHumanDetailAttribute_OK           = 100028,
        /// <summary>
        /// ��ҡ������㡱�����仯
        /// </summary>
        Net_MCHumanDetailAttribute_RuneMoney    = 100029,
        /// <summary>
        /// ��ҡ�����㡱�����仯
        /// </summary>
        Net_MCHumanDetailAttribute_HeroMoney    = 100030,
        /// <summary>
        /// ��ҡ�����ᾧ�������仯
        /// </summary>
        Net_MCHumanDetailAttribute_ExpFruit     = 100031,
        /// <summary>
        /// ��ҡ������ᾧ�������仯
        /// </summary>
        Net_MCHumanDetailAttribute_HeroFruit    = 100032,
        /// <summary>
        /// ��ҡ�������������������仯
        /// </summary>
        Net_MCHumanDetailAttribute_BagBuyCount  = 100033,
        /// <summary>
        /// ��ҡ�Ӣ�۹�������������仯
        /// </summary>
        Net_MCHumanDetailAttribute_HeroBuyCount = 100034,
        
        #endregion

        #region ��������ʱ��ѡ�Ľ�ɫ�б� MCNotifySelectRolePacket ��ʼID��100050
        Net_MCNotifySelectRolePacket_SourceName = 100050, 
        #endregion

        #region Ӣ�� �����ϢID MCNewHeroListPacket MCHeroDetailAttributePacket MCHeroDeletePacket��ʼID��100080
        /// <summary>
        /// Ӣ���б��ʼ������OK
        /// </summary>
        Net_MCNewHeroListPacket_OK              = 100080,
        #endregion

        #region ��Ʒ/���� �����ϢID MCNewItemListPacket MCItemInfoPacket MCItemDeletePacket ��ʼID��100280
        /// <summary>
        /// ��Ʒ�б��ʼ������OK
        /// </summary>
        Net_MCNewItemListPacket_OK              = 100280,
        #endregion

        #region ����/�� �����ϢID MCTeamInfoPacket MCSetTeamObjectRetPacket ��ʼID��100480
        /// <summary>
        /// ����/�� ��ʼ������OK
        /// </summary>
        Net_MCTeamInfoPacket_OK             = 100480,
        /// <summary>
        /// ��������OK
        /// </summary>
        Net_MCSetTeamObjectRetPacket_OK     = 100481, 
        #endregion

        #region ս������ �����ϢID MCDetailCampaignPacket MCCampaignAwardPacket ��ʼID��100680
        /// <summary>
        /// ս������ ��ʼ������OK
        /// </summary>
        Net_MCDetailCampaignPacket_OK       = 100680, 
        #endregion

        #region ��������Ϣ֪ͨ ��ʼID��101000
        /// <summary>
        /// ս������ ��ʼ������OK
        /// </summary>
        Net_MCGameResultPacket = 101000, 
        #endregion

        // ======= java�������¶����������Ϣ ============
        /// <summary>
        /// ��������ʾ��Ϣ
        /// </summary>
        U_NetTips = 101003,
        /// <summary>
        /// ���¼����½��ť
        /// </summary>
        U_ReActive = 10104,
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        U_MsgNotify = 10105,
        /// <summary>
        /// ������Ӣ�����ݸı�;
        /// </summary>
        Net_RefreshHero = 10106,
        /// <summary>
        /// ��������Ʒ���ݸı�---KE_KnapsackUpdateShow;
        /// </summary>
        Net_RefreshItem = 10107,

        /// <summary>
        /// ͨ����ʾ
        /// </summary>
        U_GameTips = 10108,

        /// <summary>
        /// �Ƴ�Ӣ��;
        /// </summary>
        Net_RemoveHero = 10109,

        //--------------------------------------
        // DreamFaction.UI UIģ�鴥�����¼�
        // ID���� 200000 - 299999
        // "U_"��ͷ��ʾ UserInputEvent
        //--------------------------------------
        /// <summary>
        /// �û������½
        /// </summary>
        U_Login             = 200000,
        /// <summary>
        /// �л���Ϸ����
        /// </summary>
        U_Localize          = 200001,
        /// <summary>
        /// �����ı�
        /// </summary>
        U_MessageAlert      = 200002,
        /// <summary>
        /// ��������
        /// </summary>
        U_MessageConnecting = 200003,
        /// <summary>
        /// �յ�Ӧ��
        /// </summary>
        U_MessageConnectOK  = 200004,
        /// <summary>
        /// ������Դ�������
        /// </summary>
        U_EternalSpriteLoaded = 200005,
        /// <summary>
        /// �л�ѡ�е�Ӣ��
        /// </summary>
        U_HeroChangeTarget = 200006,
        /// <summary>
        /// ������Ϣ
        /// </summary>
        U_MessageBox = 200007,
        /// <summary>
        /// ͨ��������ѡ����棬ѡ��������¼�
        /// </summary>
        U_SelectedServer = 200008,
        /// <summary>
        /// ����UICanvas
        /// </summary>
        U_HidUICanvas = 200009,
        /// <summary>
        /// ��ʾUICanvas
        /// </summary>
        U_ShowUICanvas = 200010,
        /// <summary>
        /// UICanvas����������
        /// </summary>
        U_BlockCanvasRaycasts = 200011,
        /// <summary>
        /// UICanvas������������
        /// </summary>
        U_UnBlockCanvasRaycasts = 200012,
        /// <summary>
        /// ��UI
        /// </summary>
        U_OpenUI = 200013,
        /// <summary>
        /// �ر�UI
        /// </summary>
        U_CloseUI = 200014,
        /// <summary>
        /// �򿪻��߹ر�UI����������������ǰUI����رգ������ǰUI�ر����
        /// </summary>
        U_OpenOrCloseUI = 200015,
        /// <summary>
        /// �̳���Ʒˢ��;
        /// </summary>
        U_RefreshShopInfo = 200016,

        /// <summary>
        /// ɨ�����ݷ����¼�
        /// </summary>
        U_RapidClearRespond = 200017,

        /// <summary>
        /// ������ʾ�򵯳��¼������������̵�
        /// </summary>
        UI_MysteriousShopSpecialTips = 200018,
        /// <summary>
        /// ������ʾ�򵯳��¼�����������ؿ�
        /// </summary>
        UI_SpecialStageTips = 200019,
        /// <summary>
        /// �����̵깺��ظ��¼�
        /// </summary>
        UI_MysteriousShopBuyReplay = 200020,
        /// <summary>
        /// ������ȡ�¼�
        /// </summary>
        UI_GetPower                = 200021,
        /// <summary>
        /// �̳ǹ����Դ������ɻص�;
        /// </summary>
        UI_ShopAdAssetDownload     = 200022,
        /// <summary>
        /// ��Ը�ɹ���
        /// </summary>
        UI_SacredAltarSuccend      = 200023,
        /// <summary>
        /// ��Ը����
        /// </summary>
        UI_SacredAltarTips         = 200024,
        /// <summary>
        /// ����UI��ʾ
        /// </summary>
        UI_SacredAltarUIShow       = 200025,
        /// <summary>
        /// ˢ���¿���Ϣ;
        /// </summary>
        UI_RefreshMonthCard        = 200026,
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        UI_GetLivenessBox          = 200027,
        /// <summary>
        /// ���»�������
        /// </summary>
        UI_GetLiveness             = 200028,
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        UI_ChangeName              = 200029,

        /// <summary>
        /// UI���淢���ı�ʱ
        /// </summary>
        UI_InterfaceChange          = 200030,

        /// <summary>
        /// �ؿ�ɨ�����ݸı�ʱ��;
        /// </summary>
        UI_StageSweepDataChange     = 200031,

        /// <summary>
        /// �յ����������������Ϣ
        /// </summary>
        UI_ReceiveCaptionMessage = 200032,

        /// <summary>
        /// �ؿ����ݸı���Ϣ;
        /// </summary>
        UI_StageDataRefresh = 200033,

        /// <summary>
        /// �С������ʾ
        /// </summary>
        UI_ActivityPointShow = 200034,

        /// <summary>
        /// �ˢ�µ�������
        /// </summary>
        UI_ActivityRefreshSingle = 200035,

        /// <summary>
        /// ���ֵ�����
        /// </summary>
        UI_ActivityMoneyChange = 200036,

        /// <summary>
        /// �ʼ���ˢ��
        /// </summary>
        UI_MailRefresh = 200037,

        /// <summary>
        /// ���ʼ� ��ȡ�����б�
        /// </summary>
        UI_MailReceiveListData = 200038,

        /// <summary>
        /// ��ȡ�����ʼ�
        /// </summary>
        UI_MailReceiveMore = 200039,

        /// <summary>
        /// ɾ���ʼ�
        /// </summary>
        UI_MailDel = 200040,

        /// <summary>
        /// Ӣ�۽��׳ɹ�
        /// </summary>
        UI_AdvancedSuccess = 200041,

        /// <summary>
        /// Ӣ�����������ɹ�
        /// </summary>
        UI_MysticSuccess = 200042,
        /// <summary>
        /// ��Ƭ�ϳ�Ӣ�۳ɹ�
        /// </summary>
        UI_FragmentComposeHeroSuccess =200043,
        /// <summary>
        /// �ؿ�������ȡ;
        /// </summary>
        UI_ChapterBoxGot = 200044,
        /// <summary>
        /// �ƹ����ݸı�;
        /// </summary>
        UI_JiuGuanDataUpdate = 200045,
        //--------------------------------------
        // DreamFaction.GameCore ��Ϸ�ڴ����¼� : ��Ϸ����Դ�����¼�
        // ID���� 300000 - 300499
        // "G_"��ͷ��ʾ GameEvent
        //--------------------------------------
        /// <summary>
        /// �ͻ��˳�ʼ��Դ׼������
        /// </summary>
        G_Clent_ResOK       = 300000,
        /// <summary>
        /// Ŀ�곡����Դ׼������ 
        /// </summary>
        G_Scene_ResOK       = 300001,
        /// <summary>
        /// �����л����
        /// </summary>
        G_ChangeScene_Over  = 300002,
		/// <summary>
		///  Ŀ�곡���¼���ɣ�׼��ɾ��loading�������� [1/20/2015 Zmy]
		/// </summary>
        G_DestroyLoadingObj = 300003,
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        G_ActionPoint_Update = 300004,
        /// <summary>
        /// �ؿ���¼����
        /// </summary>
        G_BattleNum_Update = 300005, 
        /// <summary>
        /// ��Ҹ���
        /// </summary>
        G_Money_Update = 300006,
        /// <summary>
        /// ���ǵȼ�����
        /// </summary>
        G_HumanLevel_Update = 300007,
        /// <summary>
        /// ���Ǿ������
        /// </summary>
        G_HumanExp_Update = 300008,
        /// <summary>
        /// VIP�ȼ�����
        /// </summary>
        G_VipLevel_Update = 300009,
        /// <summary>
        /// Ԫ������
        /// </summary>
        G_Gold_Update = 300010,
        /// <summary>
        /// ���͸���
        /// </summary>
        G_Formation_Update=300011,
        /// <summary>
        /// ��ʾƷ�ʷ����仯
        /// </summary>
        G_GameQualityChanged = 300012,
        /// <summary>
        /// ������Դ����
        /// </summary>
        G_MoneyResource_Update = 300013,
        /// <summary>
        /// ���������µ�����
        /// </summary>
        G_Aritfact_Enable = 300014,

		/// <summary>
		/// ����֮��Ӣ����������
		/// </summary>
		G_Lith_Hero_Update = 3000015,

		/// <summary>
		/// ����֮���Ӣ����������
		/// </summary>
		G_Soul_Hero_Update = 3000038,
        /// <summary>
        /// ��ս��������ɹ���
        /// </summary>
        G_FightNumSucceed  = 3000016,

        /// <summary>
        /// VIP�ȼ������������¼�
        /// </summary>
        G_VipLevelUp = 300017,

        /// <summary>
        /// ̽�������¼�;
        /// </summary>
        G_ExplorePoint_Update = 300018,

        /// <summary>
        /// ˢ������BOSS����
        /// </summary>
        G_GetWorldBoss = 300019,

		/// <summary>
        /// ̽���������ݸı�;
        /// </summary>
        G_ExploreData_Update = 300020,
        /// <summary>
        /// �յ�SGetMyWordBoss
        /// </summary>
        G_GetMyWorldBoss = 300021,
        /// <summary>
        /// �յ���������������BOSS�̵���Ʒ������Ϣ
        /// </summary>
        G_SBuyBossShop = 300022,
        /// <summary>
        /// �յ���������������BOSSף��������Ϣ
        /// </summary>
        G_SBuyBossBlessing = 300023,
        /// <summary>
        /// �յ���������������BOSS������֮�鷵����Ϣ
        /// </summary>
        G_SBuyWatcherSoul = 300024,
        /// <summary>
        /// �յ�����������BOSS���а񷵻���Ϣ
        /// </summary>
        G_SGetBossRank = 300025,
        /// <summary>
        /// �յ�SBossShop
        /// </summary>
        G_SBossShop = 300026,
        /// <summary>
        /// ���̽�ն����ٻ�;
        /// </summary>
        G_ExploreTeamCallBack = 300027,
        /// <summary>
        /// ���̽������ʱ�����;
        /// </summary>
        G_ExploreTeamTimeUp = 300028,
        /// <summary>
        /// ���̽����ȡ����;
        /// </summary>
        G_ExploreTeamGetReward = 300029,
        /// <summary>
        /// ���̽������ˢ������;
        /// </summary>
        G_ExploreTeamRefreshTasks = 300030,
        /// <summary>
        /// ���̽������--��ʼ̽�ճɹ�;
        /// </summary>
        G_ExploreTeamBeginTasks = 300031,
        //--------------------------------------
        // DreamFaction.GameCore ��Ϸ�ڴ����¼� : ����ս����
        // ID���� 300500 - 300999
        // "G_"��ͷ��ʾ GameEvent
        //--------------------------------------
        /// <summary>
        /// Ӣ�۱��˺�
        /// </summary>
        F_HeroBeHurt        = 300500,
        /// <summary>
        /// �з����˺�
        /// </summary>
        F_EnemyBeHurt       = 300501,
        /// <summary>
        /// Ӣ������
        /// </summary>
        F_HeroOnDie         = 300502,
        /// <summary>
        /// �з�����
        /// </summary>
        F_EnemyOnDie        = 300503,
        /// <summary>
        /// ��ǰս���غϽ���
        /// </summary>
        F_BattleRoundOver   = 300504,
        /// <summary>
        /// ��ǰ����ս������
        /// </summary>
        F_BattleOver        = 300505,
        /// <summary>
        /// Ӣ��ȫ��������ս��ʧ�ܣ�
        /// </summary>
        F_BattleFail        = 300506,
        /// <summary>
        /// ս��״̬�ı�
        /// </summary>
        F_FightStateUpdate  = 300507,
        /// <summary>
        /// ������[3/9/2015 Zmy]
        /// </summary>
        F_OnBeHeal_Hero          = 300508,
        F_OnBeHeal_Monster          = 300509,
        /// <summary>
        /// ֧ԮҰ�����Ѫ��[3/23/2015 zcd]
        /// </summary>
        F_OnSupportMonstorBlood = 300510,
        /// <summary>
        /// ��ʾ����flag
        /// </summary>
        SE_ShowSkillTarget = 300511,
        /// <summary>
        /// �����ͷż���
        /// </summary>
        SE_RequestReleaseSkill = 300512,
        /// <summary>
        /// �����ͷ����,���¶���
        /// </summary>
        SE_ResetSkillCD = 300513,
        /// <summary>
        /// ս������ʱ������ս��ʧ��
        /// </summary>
        F_CountDownOver = 300514,
        /// <summary>
        /// Ѫ���仯
        /// </summary>
        F_UI_ChangeHP = 300515,
        /// <summary>
        /// δ����
        /// </summary>
        F_UI_Dodge = 300516,
        /// <summary>
        /// ѡ�񼯻�Ŀ��
        /// </summary>
        F_Select_FireSign = 300517,
        /// <summary>
        /// buff���£���ʾUI
        /// </summary>
        F_BuffEvent_ShowUI = 300518,
        /// <summary>
        /// ŭ������
        /// </summary>
        F_Anger_Update = 300519,
        /// <summary>
        /// �����ͷ���ʾ������
        /// </summary>
        F_ShowSkillName = 300520,
        /// <summary>
        /// ˢ�±�����ʾ
        /// </summary>
        F_ShowBox       =300521,
        /// <summary>
        /// ���ܵ��¼�;
        /// </summary>
        G_SkillPoint_Update = 300522,
        /// <summary>
        /// ��ȡ�̵�
        /// </summary>
        G_SGetStore = 300523,
        //--------------------------------------
        // DreamFaction.GameSceneEditor �ؿ��༭�������¼�
        // ID���� 400000 - 499999
        // "SE_"��ͷ��ʾ SceneEditor
        //--------------------------------------
        /// <summary>
        /// ����ս���¼�׼����������(�ڼ�������)
        /// </summary>
		SE_PrepareEnemy     = 400000,
        /// <summary>
        /// ��������ս��״̬�¼�������ֹͣ�ƶ�(�ڼ�������)
        /// </summary>
        SE_EnterFightState  = 400001,
        /// <summary>
        /// ���������¼�(�ڼ�������)
        /// </summary>
        SE_StoryEnter       = 400002,
        /// <summary>
        /// ���������¼�����(����ID Int)
        /// </summary>
        SE_StoryEnd         = 400003,
        /// <summary>
        /// Ӣ��˲���ƶ�����
        /// </summary>
        SE_HeroPathMomentMoveEnter  = 400004,
        /// <summary>
        /// Ӣ��˲���ƶ��˳�
        /// </summary>
        SE_HeroPathMomentMoveExit   = 400005,
        /// <summary>
        /// Ӣ�������ƶ�
        /// </summary>
        SE_HeroPathNormalMove       = 400006,
        /// <summary>
        /// Ӣ��ԭ�ش���
        /// </summary>
        SE_HeroPathIdle             = 400007,
        /// <summary>
        /// ս�������༭���������
        /// </summary>
        SE_FightEditorLoadDone      = 400008,
        /// <summary>
        /// �����ƶ���ս�����������ӵ�
        /// </summary>
        SE_LineUpReady              = 400009,
        /// <summary>
        /// ֹͣ�����������
        /// </summary>
        SE_StopMonsterBirth         = 400010,
        /// <summary>
        /// Ӣ��˲���ƶ���
        /// </summary>
        SE_HeroPathMomentMoveIng    = 400011,
        /// <summary>
        /// ս��ʤ��
        /// </summary>
        SE_FightWin                 = 400012,

        /// <summary>
        /// ����BOSSս����
        /// </summary>
        SE_BossPass                 = 400013,
        
        /// <summary>
        /// ׼�����ؾ�;
        /// </summary>
        SE_PrepareBoard = 400021,
        /// <summary>
        /// ���ؾ߽���--ȫ�����ؾ���;
        /// </summary>
        SE_PrepareBoardOver = 400022,
        /// <summary>
        /// �ؾ��ƶ���;
        /// </summary>
        SE_Boarding = 400023,
        /// <summary>
        /// �ؾ��ƶ�����;
        /// </summary>
        SE_BoardingOver = 400024,
        /// <summary>
        /// ���������������ڿ���
        /// </summary>
        SE_StoryCameraEnter = 400025,
        /// <summary>
        /// �������������
        /// </summary>
        SE_StoryCameraEnd = 400026,
        //--------------------------------------
        // DreamFaction.KnapsackEvent ����
        // ID���� 500000-599999
        // "KE_"��ͷ��ʾ KnapsackEvent
        //--------------------------------------
        KE_KnapsackUpdateShow       = 500000,
        KE_KnapsackGigtShow         = 500001,
        KE_BagItemSizeShow          = 500002,
        /// <summary>
        /// Ӣ�۱����������
        /// </summary>
        KE_HeroBagItemSizeShow      = 500003,
        KE_KnapsackAdd              = 500004,
        KE_ModItemNum               = 500005,
        KE_ShowGift                 = 500006,
        //--------------------------------------
        // DreamFaction.HeroMassage Ӣ����Ϣ����
        // ID���� 600000-699999
        // "HE_"��ͷ��ʾ HeroMassage
        //--------------------------------------
        HE_HeroLevelUpSucceed     =6000001,
        HE_PeiyangUp              =6000002,
        HE_BeginnerUp             =6000003,
        HE_HeroLevelUpDefeat      =6000004,
        HE_HeroBeginnerUpdateShow =6000005,
        HE_HeroCloneInject        =6000006,
        HE_HeroSkin               =6000007,//Ӣ�ۻ�װ�ɹ�
        HE_GetHeroHp              =6000008,//���Ӣ��֮Ѫ
        /// <summary>
        /// ͼ�����
        /// </summary>
        HB_BoxUpdate                = 7000001,//��ȡ����
        HB_GetMedalPop              = 7000003,//���ڻ�õ�ѫ�µ���
        HB_GetSTuJianHeros          = 7000004,//�յ�������STuJianHerosЭ��
        /// <summary>
        /// ħ��
        /// </summary>
        F_SealBox                   = 8000001,
        F_IsOpenSealBox             = 8000002,//�Ƿ���Դ�ħ��
        /// <summary>
        /// �����������
        /// </summary>
        F_LimitBoutEnd              = 9000001,//�غϽ���
        F_LimitAddEnd               = 9000002,//�������Լӳ�
        F_LimitClearing             = 9000003,//����
        F_LimitPactOk               = 9000004,//ԤԼ�ɹ�
        F_LimitFightEnd             = 9000005,//ս������
        F_LimitRankUpdate           = 9000006,//���а����
        
        // װ��ǿ�� and ��Ʒ
        I_EquipStrengthen           = 11000000, // װ��ǿ��
        I_EquipLetGood              = 11000001, // װ����Ʒ


        // �����������
        G_Guide_Stop_Type = 10000001,
        G_Guide_Fighting = 10000002,
        G_Guide_Continue = 10000003,
    }
}