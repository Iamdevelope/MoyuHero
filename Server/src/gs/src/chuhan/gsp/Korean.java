package chuhan.gsp;

public class Korean extends LangueVersion {

	@Override
	public int getMaxNameLen() {
		return 7;
	}

	@Override
	public int getMinNameLen() {
		return 2;
	}

	@Override
	protected String langueName() {
		return "韩文";
	}

	@Override
	public String getUnicodeLimit() {
		return "[\uac00-\ud79f]";
	}

}
