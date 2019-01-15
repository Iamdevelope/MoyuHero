package chuhan.gsp.task;


public class STiantiConfig implements mytools.ConvMain.Checkable ,Comparable<STiantiConfig>{

	public int compareTo(STiantiConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public STiantiConfig(){
		super();
	}
	public STiantiConfig(STiantiConfig arg){
		this.id=arg.id ;
		this.srartrank=arg.srartrank ;
		this.endrank=arg.endrank ;
		this.battleID=arg.battleID ;
		this.jifena=arg.jifena ;
		this.jifenb=arg.jifenb ;
		this.level=arg.level ;
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
	public int srartrank  = 0  ;
	
	public int getSrartrank(){
		return this.srartrank;
	}
	
	public void setSrartrank(int v){
		this.srartrank=v;
	}
	
	/**
	 * 
	 */
	public int endrank  = 0  ;
	
	public int getEndrank(){
		return this.endrank;
	}
	
	public void setEndrank(int v){
		this.endrank=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<Integer> battleID  ;
	
	public java.util.ArrayList<Integer> getBattleID(){
		return this.battleID;
	}
	
	public void setBattleID(java.util.ArrayList<Integer> v){
		this.battleID=v;
	}
	
	/**
	 * 
	 */
	public int jifena  = 0  ;
	
	public int getJifena(){
		return this.jifena;
	}
	
	public void setJifena(int v){
		this.jifena=v;
	}
	
	/**
	 * 
	 */
	public double jifenb  = 0.0  ;
	
	public double getJifenb(){
		return this.jifenb;
	}
	
	public void setJifenb(double v){
		this.jifenb=v;
	}
	
	/**
	 * 
	 */
	public int level  = 0  ;
	
	public int getLevel(){
		return this.level;
	}
	
	public void setLevel(int v){
		this.level=v;
	}
	
	
};