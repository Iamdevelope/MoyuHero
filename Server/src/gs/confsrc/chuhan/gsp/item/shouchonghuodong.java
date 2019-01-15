package chuhan.gsp.item;


public class shouchonghuodong implements mytools.ConvMain.Checkable ,Comparable<shouchonghuodong>{

	public int compareTo(shouchonghuodong o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public shouchonghuodong(){
		super();
	}
	public shouchonghuodong(shouchonghuodong arg){
		this.id=arg.id ;
		this.chargenum=arg.chargenum ;
		this.getyb=arg.getyb ;
		this.everydayyb=arg.everydayyb ;
		this.times=arg.times ;
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
	public int chargenum  = 0  ;
	
	public int getChargenum(){
		return this.chargenum;
	}
	
	public void setChargenum(int v){
		this.chargenum=v;
	}
	
	/**
	 * 
	 */
	public int getyb  = 0  ;
	
	public int getGetyb(){
		return this.getyb;
	}
	
	public void setGetyb(int v){
		this.getyb=v;
	}
	
	/**
	 * 
	 */
	public int everydayyb  = 0  ;
	
	public int getEverydayyb(){
		return this.everydayyb;
	}
	
	public void setEverydayyb(int v){
		this.everydayyb=v;
	}
	
	/**
	 * 
	 */
	public int times  = 0  ;
	
	public int getTimes(){
		return this.times;
	}
	
	public void setTimes(int v){
		this.times=v;
	}
	
	
};