package chuhan.gsp.util;

public class CollectionUtil {
	public static boolean isEmpty(java.util.Collection c) {
		return null == c || c.size() == 0;
	}
	
	public static boolean isNotEmpty(java.util.Collection c) {
		return null != c && c.size() > 0;
	}
	
	public static boolean isEmpty(String s) {
		return null == s || s.isEmpty();
	}
	
	public static boolean isNotEmpty(String s) {
		return (null != s) && (!s.isEmpty());
	}
}
