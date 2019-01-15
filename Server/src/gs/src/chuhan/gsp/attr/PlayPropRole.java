package chuhan.gsp.attr;

public class PlayPropRole {
//	private final long roleId;
//	private final PlayProperties playProperties;
	
/*	private PlayPropRole(long roleId, PlayProperties playProperties) {
		this.roleId = roleId;
		this.playProperties = playProperties;
	}*/
	
	public static PlayPropRole getPlayPropRole(long roleId, boolean readOnly) {
		/*PlayProperties playProperties;
		if(readOnly) {
			playProperties = xtable.Playproperties.select(roleId);
		} else {
			playProperties = xtable.Playproperties.get(roleId);
		}
		if(null == playProperties) {
			if(readOnly) {
				playProperties = xbean.Pod.newPlayPropertiesData();
			} else {
				playProperties = xbean.Pod.newPlayProperties();
				xtable.Playproperties.insert(roleId, playProperties);
			}
		}*/
		return null;
	}
	
/*	public PlayProperties getProp() {
		return null;
	}*/
	
	public boolean addPlayJiFen(int jifen) {
/*		int current = playProperties.getPlayjifen() + jifen;
		if(current < 0) {
			return false;
		}
		playProperties.setPlayjifen(current);*/
		return true;
	}
}
