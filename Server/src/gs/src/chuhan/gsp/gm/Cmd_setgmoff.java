package chuhan.gsp.gm;


public class Cmd_setgmoff extends GMCommand {

	@Override
	boolean exec(final String[] args) {
		new xdb.Procedure() {
			@Override
			protected boolean process() throws Exception {
				GMInterface.setGMOn(false);
				return true;
			}

		}.submit();
		return true;
	}

	@Override
	String usage() {

		return "//setgmon";
	}

}

