package chuhan.gsp.gm;


public class Cmd_setgmon extends GMCommand {

	@Override
	boolean exec(final String[] args) {
		new xdb.Procedure() {
			@Override
			protected boolean process() throws Exception {
				GMInterface.setGMOn(true);
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

