package chuhan.gsp.attr;

import java.util.Map;

import xbean.BasicFightProperties;

public class AttrFighter extends AttrCalcAgent{
	
//	private xbean.FighterInfo fighterInfo;
	
//	public AttrFighter(xbean.FighterInfo fighterInfo)
//	{
//		this.fighterInfo = fighterInfo;
//	}
	
	
	@Override
	protected Map<Integer, Float> getEffects() {
		return null;
	}

	@Override
	protected Map<Integer, Float> getFinalAttrs() {
		return null;
	}


	@Override
	public void addHp(int v) {
		
		int oldv = getHp();
		int newv = Math.min(Math.max(0, oldv+v), (int)getAttrById(AttrType.ARMY));
//		fighterInfo.setHp(newv);
	}


	@Override
	public int getHp() {
		
		return 0;
	}


	@Override
	protected BasicFightProperties getBfp() {
		return null;
	}
	
	@Override
	protected int getSpeed() {
		return 0;
	}

}
