package chuhan.gsp.task;


public class cqiandaoconfig implements mytools.ConvMain.Checkable ,Comparable<cqiandaoconfig>{

	public int compareTo(cqiandaoconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cqiandaoconfig(){
		super();
	}
	public cqiandaoconfig(cqiandaoconfig arg){
		this.id=arg.id ;
		this.item1=arg.item1 ;
		this.item2=arg.item2 ;
		this.item3=arg.item3 ;
		this.num1=arg.num1 ;
		this.num2=arg.num2 ;
		this.num3=arg.num3 ;
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
	public int item1  = 0  ;
	
	public int getItem1(){
		return this.item1;
	}
	
	public void setItem1(int v){
		this.item1=v;
	}
	
	/**
	 * 
	 */
	public int item2  = 0  ;
	
	public int getItem2(){
		return this.item2;
	}
	
	public void setItem2(int v){
		this.item2=v;
	}
	
	/**
	 * 
	 */
	public int item3  = 0  ;
	
	public int getItem3(){
		return this.item3;
	}
	
	public void setItem3(int v){
		this.item3=v;
	}
	
	/**
	 * 
	 */
	public int num1  = 0  ;
	
	public int getNum1(){
		return this.num1;
	}
	
	public void setNum1(int v){
		this.num1=v;
	}
	
	/**
	 * 
	 */
	public int num2  = 0  ;
	
	public int getNum2(){
		return this.num2;
	}
	
	public void setNum2(int v){
		this.num2=v;
	}
	
	/**
	 * 
	 */
	public int num3  = 0  ;
	
	public int getNum3(){
		return this.num3;
	}
	
	public void setNum3(int v){
		this.num3=v;
	}
	
	
};