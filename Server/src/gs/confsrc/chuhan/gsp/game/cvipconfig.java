package chuhan.gsp.game;


public class cvipconfig implements mytools.ConvMain.Checkable ,Comparable<cvipconfig>{

	public int compareTo(cvipconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cvipconfig(){
		super();
	}
	public cvipconfig(cvipconfig arg){
		this.id=arg.id ;
		this.rmb=arg.rmb ;
		this.name=arg.name ;
		this.text=arg.text ;
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
	public String name  = null  ;
	
	public String getName(){
		return this.name;
	}
	
	public void setName(String v){
		this.name=v;
	}
	
	/**
	 * 
	 */
	public String text  = null  ;
	
	public String getText(){
		return this.text;
	}
	
	public void setText(String v){
		this.text=v;
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