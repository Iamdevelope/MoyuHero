package chuhan.gsp.game;


public class svipconfig implements mytools.ConvMain.Checkable ,Comparable<svipconfig>{

	public int compareTo(svipconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public svipconfig(){
		super();
	}
	public svipconfig(svipconfig arg){
		this.id=arg.id ;
		this.rmb=arg.rmb ;
		this.baoji=arg.baoji ;
		this.buytimes=arg.buytimes ;
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
	public int rmb  = 0  ;
	
	public int getRmb(){
		return this.rmb;
	}
	
	public void setRmb(int v){
		this.rmb=v;
	}
	
	/**
	 * 
	 */
	public int baoji  = 0  ;
	
	public int getBaoji(){
		return this.baoji;
	}
	
	public void setBaoji(int v){
		this.baoji=v;
	}
	
	/**
	 * 
	 */
	public int buytimes  = 0  ;
	
	public int getBuytimes(){
		return this.buytimes;
	}
	
	public void setBuytimes(int v){
		this.buytimes=v;
	}
	
	
};