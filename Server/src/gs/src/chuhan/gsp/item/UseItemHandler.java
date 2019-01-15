package chuhan.gsp.item;

public interface UseItemHandler {
	public UseResult onUse(final long roleid, final BasicItem bi, final int usednum);
}
