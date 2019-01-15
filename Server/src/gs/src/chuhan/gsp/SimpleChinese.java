package chuhan.gsp;

import chuhan.gsp.util.CheckName;

public class SimpleChinese extends LangueVersion {

	@Override
	public int checkName(String name) {
		return CheckName.checkValid(name);
	}

	@Override
	protected String langueName() {
		return "简体中文";
	}

	@Override
	public int getMaxNameLen() {
		return 14;
	}

	@Override
	public int getMinNameLen() {
		return 2;
	}

	@Override
	public String getUnicodeLimit() {
		return "[\u4e00-\u9fa5]";
	}

}
