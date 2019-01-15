
package chuhan.gsp.gm;

import java.util.Collections;
import java.util.Comparator;
import java.util.LinkedList;
import java.util.List;

import xdb.TTable.IWalk;

public class Cmd_refreshladder extends GMCommand {
	
	public static class RankRoleId
	{
		public int rank;
		public long roleId;
		public RankRoleId(int rank, long roleId) {
			this.rank = rank;
			this.roleId = roleId;
		}
	}
	@Override
	boolean exec(final String[] args) {
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				final List<RankRoleId> calranks = new LinkedList<Cmd_refreshladder.RankRoleId>();
				xtable.Ladderroles.getTable().walk(new IWalk<Long, xbean.LadderRole>() {
					@Override
					public boolean onRecord(Long k, xbean.LadderRole v) {
						calranks.add(new RankRoleId(v.getLadderrank(), k));
						return true;
					}
				});
				
				Collections.sort(calranks, new Comparator<RankRoleId>() {
					@Override
					public int compare(RankRoleId arg0, RankRoleId arg1) {
						if(arg0.rank <= 0 || arg0.rank > 20000)
							return 1;
						else if(arg1.rank <= 0 || arg1.rank > 20000)
							return -1;
						return arg0.rank - arg1.rank;
					}
				});
				
				int currank = 0;
				for(RankRoleId rankrole : calranks)
				{
					if(rankrole.rank > currank)
						currank = rankrole.rank;
					else
					{
						currank++;
						if(currank > 20000)
							rankrole.rank = 20001;
						else
							rankrole.rank = currank;
					}
				}
				
				for(final RankRoleId rankrole : calranks)
				{
					pexecute(new xdb.Procedure() {
						protected boolean process() throws Exception {
							xtable.Ladderroles.get(rankrole.roleId).setLadderrank(rankrole.rank);
							if(rankrole.rank <=0 || rankrole.rank > 20000)
								return true;
							xbean.LadderInfo xladder = xtable.Pvpladder.get(rankrole.rank);
							if(xladder == null)
							{
								xladder = xbean.Pod.newLadderInfo();
								xtable.Pvpladder.insert(rankrole.rank, xladder);
							}
							xladder.setRoleid(rankrole.roleId);
							return true;
						};
					});
				}
				System.out.println("refresh : "+ calranks.size());
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {

		return "//refreshladder";
	}

}

