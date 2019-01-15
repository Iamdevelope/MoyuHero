package chuhan.gsp;

public class Thai extends LangueVersion {

	@Override
	protected String langueName() {
		return "泰文";
	}

	@Override
	public int getMaxNameLen() {
		return 12;
	}

	@Override
	public int getMinNameLen() {
		return 2;
	}

	@Override
	public String getUnicodeLimit() {
		return "[\u0e00-\u0e7f]";
	}

}
