package chuhan.gsp.game;


public class cweekdaysconfig implements mytools.ConvMain.Checkable ,Comparable<cweekdaysconfig>{

	public int compareTo(cweekdaysconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cweekdaysconfig(){
		super();
	}
	public cweekdaysconfig(cweekdaysconfig arg){
		this.id=arg.id ;
		this.day=arg.day ;
		this.text=arg.text ;
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
	public String day  = null  ;
	
	public String getDay(){
		return this.day;
	}
	
	public void setDay(String v){
		this.day=v;
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
	
	
};