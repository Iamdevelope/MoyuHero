package chuhan.gsp.task;


public class monstergroup12 implements mytools.ConvMain.Checkable ,Comparable<monstergroup12>{

	public int compareTo(monstergroup12 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public monstergroup12(){
		super();
	}
	public monstergroup12(monstergroup12 arg){
		this.id=arg.id ;
		this.grouptype=arg.grouptype ;
		this.monsterid=arg.monsterid ;
		this.monsterprobability=arg.monsterprobability ;
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
	public int grouptype  = 0  ;
	
	public int getGrouptype(){
		return this.grouptype;
	}
	
	public void setGrouptype(int v){
		this.grouptype=v;
	}
	
	/**
	 * 
	 */
	public String monsterid  = null  ;
	
	public String getMonsterid(){
		return this.monsterid;
	}
	
	public void setMonsterid(String v){
		this.monsterid=v;
	}
	
	/**
	 * 
	 */
	public String monsterprobability  = null  ;
	
	public String getMonsterprobability(){
		return this.monsterprobability;
	}
	
	public void setMonsterprobability(String v){
		this.monsterprobability=v;
	}
	
	
};