package chuhan.gsp.msg;

import java.util.List;

import chuhan.gsp.award.CompenseConfig;
import chuhan.gsp.award.CompenseRole;

public class PAddCompense extends xdb.Procedure{
	
	private final long roleId;
	private final List<CompenseConfig> compenses;
	public PAddCompense(long roleId, List<CompenseConfig> compenses) {
		this.roleId = roleId;
		this.compenses = compenses;
	}
	
	@Override
	protected boolean process() throws Exception {
		CompenseRole crole = CompenseRole.getCompenseRole(roleId, false); 
		crole.giveCompenses(compenses, true);
		return true;
	}

}
