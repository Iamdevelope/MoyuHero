package chuhan.gsp.game;


public class sZhuzhanConfig implements mytools.ConvMain.Checkable ,Comparable<sZhuzhanConfig>{

	public int compareTo(sZhuzhanConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public sZhuzhanConfig(){
		super();
	}
	public sZhuzhanConfig(sZhuzhanConfig arg){
		this.id=arg.id ;
		this.XiaohaoYuanbao_PVE=arg.XiaohaoYuanbao_PVE ;
		this.GongxunJiangli_PVE=arg.GongxunJiangli_PVE ;
		this.XiaohaoYuanbao_PVP=arg.XiaohaoYuanbao_PVP ;
		this.GongxunJiangli_PVP=arg.GongxunJiangli_PVP ;
	}
	public void checkValid(java.util.Map<String,java.util.Map<Integer,? extends Object> > objs){
	}
	/**
	 * 
	 */
	public int id  = 0  ;
	
	public int getId(){
		return this.id;
	}
	
	public void setId(int v){
		this.id=v;
	}
	
	/**
	 * 
	 */
	public int XiaohaoYuanbao_PVE  = 0  ;
	
	public int getXiaohaoYuanbao_PVE(){
		return this.XiaohaoYuanbao_PVE;
	}
	
	public void setXiaohaoYuanbao_PVE(int v){
		this.XiaohaoYuanbao_PVE=v;
	}
	
	/**
	 * 
	 */
	public int GongxunJiangli_PVE  = 0  ;
	
	public int getGongxunJiangli_PVE(){
		return this.GongxunJiangli_PVE;
	}
	
	public void setGongxunJiangli_PVE(int v){
		this.GongxunJiangli_PVE=v;
	}
	
	/**
	 * 
	 */
	public int XiaohaoYuanbao_PVP  = 0  ;
	
	public int getXiaohaoYuanbao_PVP(){
		return this.XiaohaoYuanbao_PVP;
	}
	
	public void setXiaohaoYuanbao_PVP(int v){
		this.XiaohaoYuanbao_PVP=v;
	}
	
	/**
	 * 
	 */
	public int GongxunJiangli_PVP  = 0  ;
	
	public int getGongxunJiangli_PVP(){
		return this.GongxunJiangli_PVP;
	}
	
	public void setGongxunJiangli_PVP(int v){
		this.GongxunJiangli_PVP=v;
	}
	
	
};