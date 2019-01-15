package chuhan.gsp.exchange;

import java.util.Map;

import chuhan.gsp.game.SAddCashConfig;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.util.DateUtil;

public class PGetRoleBills extends xdb.Procedure{
	private final long queryRoleId;
	private final long roleId;
	private final int state ;
	private final String platbillid;
	private final long gamebillid;
	public PGetRoleBills(long queryRoleId, long roleId, int state, String platbillid,
			long gamebillid) {
		this.queryRoleId = queryRoleId;
		this.roleId = roleId;
		this.state = state;
		this.platbillid = platbillid;
		this.gamebillid = gamebillid;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		ChargeRole crole = ChargeRole.getChargeRole(roleId, false);
		if(crole == null)
		{
			Message.sendPopMsg(queryRoleId, Message.getMessage(2));
			return true;
		}
		Map<Long,xbean.BillData> bills = crole.getBillsByState(state);
		if(bills.isEmpty())
		{
			Message.sendPopMsg(queryRoleId,Message.getMessage(3));
			return true;
		}
		MsgRole mrole = MsgRole.getMsgRole(queryRoleId, false);
		for(xbean.BillData xbill : bills.values())
		{
			if(platbillid != null && !platbillid.equals("0") && !xbill.getPlatbillid().equals(platbillid))
				continue;
			if(gamebillid > 0 && xbill.getBillid() != gamebillid)
				continue;
			StringBuilder sb = new StringBuilder();
			sb.append("Create:").append(DateUtil.getStringFormat2Second(xbill.getCreatetime()))
			.append(",State:").append(getStateStr(xbill.getState()))
			.append(",Platid:").append(xbill.getPlatbillid())
			.append(",Billid:").append(xbill.getBillid())
			.append(",Good:").append(getGoodName(xbill.getGoodid()))
			.append(",Price:").append(xbill.getPrice());
			if(mrole != null)
				mrole.addSysMsgWithSP(0, null, sb.toString(), 0, MsgRole.MST_TYPE_SYS);
		}
			
		return true;
	}
	
	private String getStateStr(int state)
	{
		switch(state)
		{
		case xbean.BillData.STATE_SENDED:
				return Message.getMessage(4);
		case xbean.BillData.STATE_CONFIRMED:
			return Message.getMessage(5);
		case xbean.BillData.STATE_FAILED:
			return Message.getMessage(6);
		}
		return Message.getMessage(7);
	}
	private String getGoodName(int goodid)
	{
		SAddCashConfig goodcfg = ConfigManager.getInstance().getConf(SAddCashConfig.class).get(goodid);
		if(goodcfg != null)
			return goodcfg.name;
		else
			return Message.getMessage(8);
	}
}
