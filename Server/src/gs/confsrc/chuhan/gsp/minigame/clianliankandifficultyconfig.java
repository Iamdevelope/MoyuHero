package chuhan.gsp.minigame;


public class clianliankandifficultyconfig implements mytools.ConvMain.Checkable ,Comparable<clianliankandifficultyconfig>{

	public int compareTo(clianliankandifficultyconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public clianliankandifficultyconfig(){
		super();
	}
	public clianliankandifficultyconfig(clianliankandifficultyconfig arg){
		this.id=arg.id ;
		this.time=arg.time ;
		this.iconcount=arg.iconcount ;
		this.result=arg.result ;
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
	public int time  = 0  ;
	
	public int getTime(){
		return this.time;
	}
	
	public void setTime(int v){
		this.time=v;
	}
	
	/**
	 * 
	 */
	public int iconcount  = 0  ;
	
	public int getIconcount(){
		return this.iconcount;
	}
	
	public void setIconcount(int v){
		this.iconcount=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<Integer> result  ;
	
	public java.util.ArrayList<Integer> getResult(){
		return this.result;
	}
	
	public void setResult(java.util.ArrayList<Integer> v){
		this.result=v;
	}
	
	
};