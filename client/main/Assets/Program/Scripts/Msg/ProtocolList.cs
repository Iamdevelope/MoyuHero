using System;
using Platform;

namespace GNET
{
	public class ProtocolList
	{
		public static void RegisterProtocols()
		{

            // 连接激活
            Protocol.Register(KeepAlive.PROTOCOL_TYPE, new KeepAlive());
            Protocol.Register(UserLogin.PROTOCOL_TYPE, new UserLogin());
            Protocol.Register(ErrorInfo.PROTOCOL_TYPE, new ErrorInfo());
            Protocol.Register(Challenge.PROTOCOL_TYPE, new Challenge());
            Protocol.Register(Response.PROTOCOL_TYPE, new Response());

            Protocol.Register(Dispatch.PROTOCOL_TYPE, new Dispatch());
            Protocol.Register(Send.PROTOCOL_TYPE, new Send());
            Protocol.Register(CRoleList.PROTOCOL_TYPE, new CRoleList());
            Protocol.Register(CCreateRole.PROTOCOL_TYPE, new CCreateRole());
            Protocol.Register(SSendStages.PROTOCOL_TYPE, new SSendStages());
            Protocol.Register(SRefreshStage.PROTOCOL_TYPE, new SRefreshStage());
            Protocol.Register(SRefreshStageBattle.PROTOCOL_TYPE, new SRefreshStageBattle());

            Protocol.Register(SPlayStatus.PROTOCOL_TYPE, new SPlayStatus());
            Protocol.Register(CEnterWorld.PROTOCOL_TYPE, new CEnterWorld());
            Protocol.Register(SRoleList.PROTOCOL_TYPE, new SRoleList());
            Protocol.Register(SCreateRole.PROTOCOL_TYPE, new SCreateRole());
            Protocol.Register(SEnterWorld.PROTOCOL_TYPE, new SEnterWorld());

            Protocol.Register(CSendCommand.PROTOCOL_TYPE, new CSendCommand());

            ////hero文件夹下
            //Protocol.Register(CAddTroop.PROTOCOL_TYPE, new CAddTroop());
            //Protocol.Register(CBuySeeStarUpHero.PROTOCOL_TYPE, new CBuySeeStarUpHero());
            //Protocol.Register(CHeroOutAllTroop.PROTOCOL_TYPE, new CHeroOutAllTroop());
            //Protocol.Register(CQianghuaHero.PROTOCOL_TYPE, new CQianghuaHero());
            //Protocol.Register(CSaveStarUpHero.PROTOCOL_TYPE, new CSaveStarUpHero());
            //Protocol.Register(CSellHero.PROTOCOL_TYPE, new CSellHero());

            Protocol.Register(CHeroEquipUp.PROTOCOL_TYPE, new CHeroEquipUp());
            Protocol.Register(SHeroEquipUp.PROTOCOL_TYPE, new SHeroEquipUp());
            Protocol.Register(CHeroCompose.PROTOCOL_TYPE, new CHeroCompose());
            Protocol.Register(SHeroCompose.PROTOCOL_TYPE, new SHeroCompose());
            Protocol.Register(CHeroMSExp.PROTOCOL_TYPE, new CHeroMSExp());
            Protocol.Register(SHeroMSExp.PROTOCOL_TYPE, new SHeroMSExp());
            Protocol.Register(CHeroJinjie.PROTOCOL_TYPE, new CHeroJinjie());
            Protocol.Register(SHeroJinjie.PROTOCOL_TYPE, new SHeroJinjie());
            Protocol.Register(CArtifactAddHero.PROTOCOL_TYPE, new CArtifactAddHero());
            Protocol.Register(CChangeSkin.PROTOCOL_TYPE, new CChangeSkin());
            Protocol.Register(CSplitHero.PROTOCOL_TYPE, new CSplitHero());
            Protocol.Register(CStarUpHero.PROTOCOL_TYPE, new CStarUpHero());
            //Protocol.Register(SBuySeeStarUpHero.PROTOCOL_TYPE, new SBuySeeStarUpHero());
            //Protocol.Register(SQianghuaHero.PROTOCOL_TYPE, new SQianghuaHero());
            Protocol.Register(SArtifactAddHero.PROTOCOL_TYPE, new SArtifactAddHero());
            Protocol.Register(SAddSkin.PROTOCOL_TYPE, new SAddSkin());   
            Protocol.Register(SChangeSkin.PROTOCOL_TYPE, new SChangeSkin());

            Protocol.Register(SArtifactLevelUp.PROTOCOL_TYPE, new SArtifactLevelUp());
            Protocol.Register(SRefreshArtifact.PROTOCOL_TYPE, new SRefreshArtifact());
            Protocol.Register(SRefreshHero.PROTOCOL_TYPE, new SRefreshHero());
            Protocol.Register(SRefreshTroops.PROTOCOL_TYPE, new SRefreshTroops());
            Protocol.Register(SRemoveHero.PROTOCOL_TYPE, new SRemoveHero());
            Protocol.Register(SRefreshSweep.PROTOCOL_TYPE, new SRefreshSweep());  
            
            //Protocol.Register(SSaveStarUpHero.PROTOCOL_TYPE, new SSaveStarUpHero());
            //Protocol.Register(SSellHero.PROTOCOL_TYPE, new SSellHero());
            Protocol.Register(SSplitHero.PROTOCOL_TYPE, new SSplitHero());
            Protocol.Register(SStarUpHero.PROTOCOL_TYPE, new SStarUpHero());
            Protocol.Register(CTuJianBox.PROTOCOL_TYPE, new CTuJianBox());
            Protocol.Register(STuJianBox.PROTOCOL_TYPE, new STuJianBox());
            Protocol.Register(CTuJianHeros.PROTOCOL_TYPE, new CTuJianHeros());
            Protocol.Register(STuJianHeros.PROTOCOL_TYPE, new STuJianHeros());
            Protocol.Register(SPeiyangHero.PROTOCOL_TYPE, new SPeiyangHero());
            Protocol.Register(SHeroSkillup.PROTOCOL_TYPE, new SHeroSkillup());
            Protocol.Register(SHeroLevelUpSpeed.PROTOCOL_TYPE, new SHeroLevelUpSpeed());  
            

            //item文件夹下
            Protocol.Register(CUseItem.PROTOCOL_TYPE, new CUseItem());
            Protocol.Register(CRefineEquip.PROTOCOL_TYPE, new CRefineEquip());
            Protocol.Register(CSellItem.PROTOCOL_TYPE, new CSellItem());
            Protocol.Register(CSplitEquip.PROTOCOL_TYPE, new CSplitEquip());
            Protocol.Register(CIdentifyEquip.PROTOCOL_TYPE, new CIdentifyEquip());
            Protocol.Register(CBagExpansion.PROTOCOL_TYPE, new CBagExpansion());
            Protocol.Register(SAddItem.PROTOCOL_TYPE, new SAddItem());
            Protocol.Register(SRefreshItem.PROTOCOL_TYPE, new SRefreshItem());
            Protocol.Register(SRemoveItem.PROTOCOL_TYPE, new SRemoveItem());
            Protocol.Register(SModItemNum.PROTOCOL_TYPE, new SModItemNum());
            Protocol.Register(SShowGiftItem.PROTOCOL_TYPE, new SShowGiftItem());
            Protocol.Register(SRefineEquip.PROTOCOL_TYPE, new SRefineEquip());
            Protocol.Register(SIdentifyEquip.PROTOCOL_TYPE, new SIdentifyEquip());
            Protocol.Register(SSplitEquip.PROTOCOL_TYPE, new SSplitEquip());
            
     
            
            ////battle文件夹下
            Protocol.Register(CBeginBattle.PROTOCOL_TYPE, new CBeginBattle());
            Protocol.Register(CBuyStateBattleNum.PROTOCOL_TYPE, new CBuyStateBattleNum());
            Protocol.Register(CEndBattle.PROTOCOL_TYPE, new CEndBattle());
            Protocol.Register(SBeginBattle.PROTOCOL_TYPE, new SBeginBattle());
            Protocol.Register(SEndBattle.PROTOCOL_TYPE, new SEndBattle());
            Protocol.Register(CGetStageReward.PROTOCOL_TYPE, new CGetStageReward());
            Protocol.Register(SGetStageReward.PROTOCOL_TYPE, new SGetStageReward());
            Protocol.Register(COpenMohe.PROTOCOL_TYPE, new COpenMohe());
            Protocol.Register(SOpenMohe.PROTOCOL_TYPE, new SOpenMohe());
            Protocol.Register(CBuySmShop.PROTOCOL_TYPE, new CBuySmShop());
            Protocol.Register(SBuySmShop.PROTOCOL_TYPE, new SBuySmShop());
            
            Protocol.Register(CSweepBattle.PROTOCOL_TYPE, new CSweepBattle());
            Protocol.Register(SSweepBattle.PROTOCOL_TYPE, new SSweepBattle());
            Protocol.Register(SBuyStateBattleNum.PROTOCOL_TYPE, new SBuyStateBattleNum());
            

   
            ////attr文件夹下
            Protocol.Register(SRefreshBattleNum.PROTOCOL_TYPE, new SRefreshBattleNum());
            Protocol.Register(SRefreshGold.PROTOCOL_TYPE, new SRefreshGold());
            Protocol.Register(SRefreshZiYuan.PROTOCOL_TYPE, new SRefreshZiYuan());
            Protocol.Register(SRefreshLevel.PROTOCOL_TYPE, new SRefreshLevel());
            Protocol.Register(SRefreshRoleExp.PROTOCOL_TYPE, new SRefreshRoleExp());
            Protocol.Register(SRefreshTili.PROTOCOL_TYPE, new SRefreshTili());
            Protocol.Register(SRefreshVipLevel.PROTOCOL_TYPE, new SRefreshVipLevel());
            Protocol.Register(SRefreshYuanBao.PROTOCOL_TYPE, new SRefreshYuanBao());
            Protocol.Register(SYuanbaoNotEnough.PROTOCOL_TYPE, new SYuanbaoNotEnough());
            Protocol.Register(SRefreshBagExp.PROTOCOL_TYPE, new SRefreshBagExp());
            Protocol.Register(SRefreshChargeSum.PROTOCOL_TYPE, new SRefreshChargeSum());

            //mail
            Protocol.Register(CGetMailList.PROTOCOL_TYPE, new CGetMailList());
            Protocol.Register(CReceiveMail.PROTOCOL_TYPE, new CReceiveMail());
            Protocol.Register(CRemoveMailList.PROTOCOL_TYPE, new CRemoveMailList());
            Protocol.Register(SGetMailList.PROTOCOL_TYPE, new SGetMailList());
            Protocol.Register(SReceiveMail.PROTOCOL_TYPE, new SReceiveMail());
            Protocol.Register(SIsHaveNotOpen.PROTOCOL_TYPE, new SIsHaveNotOpen());
            Protocol.Register(SRefreshMail.PROTOCOL_TYPE, new SRefreshMail());

            Protocol.Register(SSendMsgNotify.PROTOCOL_TYPE, new SSendMsgNotify());

            Protocol.Register(CChangeName.PROTOCOL_TYPE, new CChangeName());
            Protocol.Register(SChangeName.PROTOCOL_TYPE, new SChangeName());
            Protocol.Register(SGameTime.PROTOCOL_TYPE, new SGameTime());

            //play-lottery
            Protocol.Register(CLottery.PROTOCOL_TYPE, new CLottery());
            Protocol.Register(SLottery.PROTOCOL_TYPE, new SLottery());
            Protocol.Register(CChangeDream.PROTOCOL_TYPE, new CChangeDream());
            Protocol.Register(CGetDream.PROTOCOL_TYPE, new CGetDream());
            Protocol.Register(SGetDream.PROTOCOL_TYPE, new SGetDream());
            Protocol.Register(SChangeDream.PROTOCOL_TYPE, new SChangeDream());
            Protocol.Register(SRefreshLotty.PROTOCOL_TYPE, new SRefreshLotty());

            //play-lotteryitem
            Protocol.Register(CLotteryItem.PROTOCOL_TYPE, new CLotteryItem());
            Protocol.Register(SLotteryItem.PROTOCOL_TYPE, new SLotteryItem());
            Protocol.Register(SRefreshLottyItem.PROTOCOL_TYPE, new SRefreshLottyItem());

            //play-huoyuedu
            Protocol.Register(CGetHuoYue.PROTOCOL_TYPE, new CGetHuoYue());
            Protocol.Register(CGetHuoYueBox.PROTOCOL_TYPE, new CGetHuoYueBox());
            Protocol.Register(SGetHuoYueBox.PROTOCOL_TYPE, new SGetHuoYueBox());
            Protocol.Register(SRefreshHuoYue.PROTOCOL_TYPE, new SRefreshHuoYue());

            //play-shop
            Protocol.Register(CShopBuy.PROTOCOL_TYPE, new CShopBuy());
            //Protocol.Register(CSignIn.PROTOCOL_TYPE, new CSignIn());
            Protocol.Register(SShopBuy.PROTOCOL_TYPE, new SShopBuy());
            Protocol.Register(SRefreshShopBuy.PROTOCOL_TYPE, new SRefreshShopBuy());    
            //Protocol.Register(SSignIn.PROTOCOL_TYPE, new SSignIn());
            Protocol.Register(CGetNewShop.PROTOCOL_TYPE, new CGetNewShop());
            Protocol.Register(SRefreshNewShop.PROTOCOL_TYPE, new SRefreshNewShop());
            Protocol.Register(CBuyNewShop.PROTOCOL_TYPE, new CBuyNewShop());
            Protocol.Register(SBuyNewShop.PROTOCOL_TYPE, new SBuyNewShop());
            Protocol.Register(CGetNewShopItem.PROTOCOL_TYPE, new CGetNewShopItem());
            Protocol.Register(SGetNewShopItem.PROTOCOL_TYPE, new SGetNewShopItem()); 

            //play-endlessbattle
            Protocol.Register(CBeginEndless.PROTOCOL_TYPE, new CBeginEndless());
            Protocol.Register(CBuyPact.PROTOCOL_TYPE, new CBuyPact());
            Protocol.Register(CEndlessBuyadd.PROTOCOL_TYPE, new CEndlessBuyadd());
            Protocol.Register(CEndlessPass.PROTOCOL_TYPE, new CEndlessPass());
            Protocol.Register(CTodayEndless.PROTOCOL_TYPE, new CTodayEndless());
            Protocol.Register(CGetEndlessRank.PROTOCOL_TYPE, new CGetEndlessRank());
            Protocol.Register(CEndlessEnd.PROTOCOL_TYPE, new CEndlessEnd());
            
            Protocol.Register(SBeginEndless.PROTOCOL_TYPE, new SBeginEndless());
            Protocol.Register(SBuyPact.PROTOCOL_TYPE, new SBuyPact());
            Protocol.Register(SEndlessBuyadd.PROTOCOL_TYPE, new SEndlessBuyadd());
            Protocol.Register(SEndlessEnd.PROTOCOL_TYPE, new SEndlessEnd());
            Protocol.Register(SEndlessPass.PROTOCOL_TYPE, new SEndlessPass());
            Protocol.Register(STodayEndless.PROTOCOL_TYPE, new STodayEndless());
            Protocol.Register(SGetEndlessRank.PROTOCOL_TYPE, new SGetEndlessRank());

            //play-activity
            Protocol.Register(CGetMSZQ.PROTOCOL_TYPE, new CGetMSZQ());
            Protocol.Register(CHeroClone.PROTOCOL_TYPE, new CHeroClone());
            Protocol.Register(SGetMSZQ.PROTOCOL_TYPE, new SGetMSZQ());
            Protocol.Register(SHeroClone.PROTOCOL_TYPE, new SHeroClone());
            Protocol.Register(SRefreshHeroClone.PROTOCOL_TYPE, new SRefreshHeroClone());
            Protocol.Register(CGetQiYuan.PROTOCOL_TYPE, new CGetQiYuan());
            Protocol.Register(SGetQiYuan.PROTOCOL_TYPE, new SGetQiYuan());
            Protocol.Register(CMonthCard.PROTOCOL_TYPE, new CMonthCard());
            Protocol.Register(SMonthCard.PROTOCOL_TYPE, new SMonthCard());
            Protocol.Register(SRefreshMonthCard.PROTOCOL_TYPE, new SRefreshMonthCard());
            Protocol.Register(SRefreshLogin.PROTOCOL_TYPE, new SRefreshLogin());
            Protocol.Register(CNewyindao.PROTOCOL_TYPE, new CNewyindao());
            Protocol.Register(SNewyindao.PROTOCOL_TYPE, new SNewyindao());
            Protocol.Register(CDuihuanlb.PROTOCOL_TYPE, new CDuihuanlb());
            Protocol.Register(SDuihuanlb.PROTOCOL_TYPE, new SDuihuanlb());
            Protocol.Register(SRefreshGameAct.PROTOCOL_TYPE, new SRefreshGameAct());
            Protocol.Register(SAddHeroClone.PROTOCOL_TYPE, new SAddHeroClone());
            Protocol.Register(CGetGameAct.PROTOCOL_TYPE, new CGetGameAct());
            Protocol.Register(SGetGameAct.PROTOCOL_TYPE, new SGetGameAct());
            Protocol.Register(SRefreshSingleGameAct.PROTOCOL_TYPE, new SRefreshSingleGameAct());
            Protocol.Register(CSeeGameAct.PROTOCOL_TYPE, new CSeeGameAct());
            Protocol.Register(SSeeGameAct.PROTOCOL_TYPE, new SSeeGameAct());
            
            
            

            //play-tanxian
            Protocol.Register(CTanxianBegin.PROTOCOL_TYPE, new CTanxianBegin());
            Protocol.Register(STanxianBegin.PROTOCOL_TYPE, new STanxianBegin());
            Protocol.Register(SRefreshTanXian.PROTOCOL_TYPE, new SRefreshTanXian());
            Protocol.Register(CTanXianOther.PROTOCOL_TYPE, new CTanXianOther());
            Protocol.Register(STanXianOther.PROTOCOL_TYPE, new STanXianOther());

            //play-wordboss
            Protocol.Register(CBeginBoss.PROTOCOL_TYPE, new CBeginBoss());
            Protocol.Register(SBeginBoss.PROTOCOL_TYPE, new SBeginBoss());
            Protocol.Register(CBossPass.PROTOCOL_TYPE, new CBossPass());
            Protocol.Register(SBossPass.PROTOCOL_TYPE, new SBossPass());
            Protocol.Register(CGetMyWordBoss.PROTOCOL_TYPE, new CGetMyWordBoss());
            Protocol.Register(SGetMyWordBoss.PROTOCOL_TYPE, new SGetMyWordBoss());
            Protocol.Register(CGetWordBoss.PROTOCOL_TYPE, new CGetWordBoss());
            Protocol.Register(SGetWordBoss.PROTOCOL_TYPE, new SGetWordBoss());
            Protocol.Register(CBossShop.PROTOCOL_TYPE, new CBossShop());
            Protocol.Register(SBossShop.PROTOCOL_TYPE, new SBossShop());
            Protocol.Register(CBuyBossShop.PROTOCOL_TYPE, new CBuyBossShop());
            Protocol.Register(SBuyBossShop.PROTOCOL_TYPE, new SBuyBossShop());
            Protocol.Register(CBossBuyZhufu.PROTOCOL_TYPE, new CBossBuyZhufu());
            Protocol.Register(SBossBuyZhufu.PROTOCOL_TYPE, new SBossBuyZhufu());
            Protocol.Register(CBuyShouwangzl.PROTOCOL_TYPE, new CBuyShouwangzl());
            Protocol.Register(SBuyShouwangzl.PROTOCOL_TYPE, new SBuyShouwangzl());
            Protocol.Register(CGetBossRank.PROTOCOL_TYPE, new CGetBossRank());
            Protocol.Register(SGetBossRank.PROTOCOL_TYPE, new SGetBossRank());

            //exchange
            Protocol.Register(CRequestExchangeBill.PROTOCOL_TYPE, new CRequestExchangeBill());
            Protocol.Register(SReplyExchangeBill.PROTOCOL_TYPE, new SReplyExchangeBill());
            

		}
	}
}

	

