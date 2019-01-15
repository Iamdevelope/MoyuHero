package chuhan.gsp.game;


public class caddcashconfig implements mytools.ConvMain.Checkable ,Comparable<caddcashconfig>{

	public int compareTo(caddcashconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public caddcashconfig(){
		super();
	}
	public caddcashconfig(caddcashconfig arg){
		this.id=arg.id ;
		this.price=arg.price ;
		this.yuanbao=arg.yuanbao ;
		this.present=arg.present ;
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
	public double price  = 0.0  ;
	
	public double getPrice(){
		return this.price;
	}
	
	public void setPrice(double v){
		this.price=v;
	}
	
	/**
	 * 
	 */
	public int yuanbao  = 0  ;
	
	public int getYuanbao(){
		return this.yuanbao;
	}
	
	public void setYuanbao(int v){
		this.yuanbao=v;
	}
	
	/**
	 * 
	 */
	public int present  = 0  ;
	
	public int getPresent(){
		return this.present;
	}
	
	public void setPresent(int v){
		this.present=v;
	}
	
	
};