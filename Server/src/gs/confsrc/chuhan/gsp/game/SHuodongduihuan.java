package chuhan.gsp.game;


public class SHuodongduihuan implements mytools.ConvMain.Checkable ,Comparable<SHuodongduihuan>{

	public int compareTo(SHuodongduihuan o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SHuodongduihuan(){
		super();
	}
	public SHuodongduihuan(SHuodongduihuan arg){
		this.id=arg.id ;
		this.HuodongType=arg.HuodongType ;
		this.jianglibiaoid=arg.jianglibiaoid ;
		this.Jiangli_ID=arg.Jiangli_ID ;
		this.Jiangli_num=arg.Jiangli_num ;
		this.Tiaojian=arg.Tiaojian ;
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
	public int HuodongType  = 0  ;
	
	public int getHuodongType(){
		return this.HuodongType;
	}
	
	public void setHuodongType(int v){
		this.HuodongType=v;
	}
	
	/**
	 * 
	 */
	public int jianglibiaoid  = 0  ;
	
	public int getJianglibiaoid(){
		return this.jianglibiaoid;
	}
	
	public void setJianglibiaoid(int v){
		this.jianglibiaoid=v;
	}
	
	/**
	 * 
	 */
	public int Jiangli_ID  = 0  ;
	
	public int getJiangli_ID(){
		return this.Jiangli_ID;
	}
	
	public void setJiangli_ID(int v){
		this.Jiangli_ID=v;
	}
	
	/**
	 * 
	 */
	public int Jiangli_num  = 0  ;
	
	public int getJiangli_num(){
		return this.Jiangli_num;
	}
	
	public void setJiangli_num(int v){
		this.Jiangli_num=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<chuhan.gsp.game.condition> Tiaojian  ;
	
	public java.util.ArrayList<chuhan.gsp.game.condition> getTiaojian(){
		return this.Tiaojian;
	}
	
	public void setTiaojian(java.util.ArrayList<chuhan.gsp.game.condition> v){
		this.Tiaojian=v;
	}
	
	
};