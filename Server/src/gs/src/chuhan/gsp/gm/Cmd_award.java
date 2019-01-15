package chuhan.gsp.gm;

import chuhan.gsp.award.AwardManager;

public class Cmd_award extends GMCommand {

	@Override
	boolean exec(final String[] args) {
		if (args.length != 1)
			return false;
		long roleid = getGmroleid();
		if (args.length > 1) {
			roleid = GMInterface.getTargetRoleId(args[1]);
			if (roleid <= 0)
				roleid = getGmroleid();
		}
		final long rid = roleid;
		new xdb.Procedure() {
			@Override
			protected boolean process() throws Exception {
				Integer awardid = Integer.parseInt(args[0]);
				AwardManager.getInstance().distributeAllAward(rid, awardid,
						null, true);
				return true;
			}

		}.submit();
		return true;
	}

	@Override
	String usage() {

		return "//award [awardid] [account不填即自己]";
	}

}

