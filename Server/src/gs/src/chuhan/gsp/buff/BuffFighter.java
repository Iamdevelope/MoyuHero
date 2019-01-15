package chuhan.gsp.buff;

import chuhan.gsp.attr.AttrAgent;
import chuhan.gsp.attr.AttrFighter;
import chuhan.gsp.attr.SBuffChangeResult;

public class BuffFighter extends BuffAgent{
	
//	xbean.FighterInfo fighterInfo;
	
//	public BuffFighter(xbean.BuffAgent fighterInfo) {
//		super(fighterInfo.getBuffagent(),false);
//		this.fighterInfo = fighterInfo;
//	}
	
	public BuffFighter(xbean.BuffAgent agent, boolean readonly) {
		super(agent, readonly);
		// TODO Auto-generated constructor stub
	}

	@Override
	public boolean existState(int buffTypeId) {
		return false;
	}

	@Override
	public boolean psendSBuffChangeResult(BuffResult result) {
		return false;
	}

	@Override
	protected SBuffChangeResult getSBuffChangeResult(BuffResult result) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public AttrAgent getAttrAgent() {
		return null;//new AttrFighter(fighterInfo);
	}

}
