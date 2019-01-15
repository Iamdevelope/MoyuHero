package chuhan.gsp.item;


public class Chongzhi implements mytools.ConvMain.Checkable ,Comparable<Chongzhi>{

	public int compareTo(Chongzhi o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public Chongzhi(){
		super();
	}
	public Chongzhi(Chongzhi arg){
		this.id=arg.id ;
		this.times=arg.times ;
		this.yinliang=arg.yinliang ;
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
	public int times  = 0  ;
	
	public int getTimes(){
		return this.times;
	}
	
	public void setTimes(int v){
		this.times=v;
	}
	
	/**
	 * 
	 */
	public int yinliang  = 0  ;
	
	public int getYinliang(){
		return this.yinliang;
	}
	
	public void setYinliang(int v){
		this.yinliang=v;
	}
	
	
};