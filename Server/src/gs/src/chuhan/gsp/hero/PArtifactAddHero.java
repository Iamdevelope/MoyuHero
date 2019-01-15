package chuhan.gsp.hero;

import chuhan.gsp.DataInit;
import chuhan.gsp.msg.Message;



public class PArtifactAddHero extends xdb.Procedure{
	private final long roleid;
	public final int artifacttype; // 神器类型（key）
	public final java.util.LinkedList<Integer> herokeylist;

	
	public PArtifactAddHero(long roleid, int artifacttype,java.util.LinkedList<Integer> herokeylist) {
		this.roleid = roleid;
		this.artifacttype = artifacttype;
		this.herokeylist = herokeylist;


	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
		}
		HeroArtifactColumn artifacecol = HeroArtifactColumn.getHeroArtifactColumn(roleid, false);
		
		boolean result = artifacecol.addHeroToArtifact(artifacttype, herokeylist);

		SArtifactAddHero snd = new SArtifactAddHero();
		if(result){
			snd.result = SArtifactAddHero.END_OK;
		}
		else{
			snd.result = SArtifactAddHero.END_ERROR;
		}
		xdb.Procedure.psend(roleid, snd);
		
		return result;
	}
	
}
