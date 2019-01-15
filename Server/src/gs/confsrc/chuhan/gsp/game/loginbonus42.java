package chuhan.gsp.game;


public class loginbonus42 implements mytools.ConvMain.Checkable ,Comparable<loginbonus42>{

	public int compareTo(loginbonus42 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public loginbonus42(){
		super();
	}
	public loginbonus42(loginbonus42 arg){
		this.id=arg.id ;
		this.day=arg.day ;
		this.type=arg.type ;
		this.room=arg.room ;
		this.rewardAndNum=arg.rewardAndNum ;
		this.showNum=arg.showNum ;
		this.nextRoom=arg.nextRoom ;
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
	public int day  = 0  ;
	
	public int getDay(){
		return this.day;
	}
	
	public void setDay(int v){
		this.day=v;
	}
	
	/**
	 * 
	 */
	public int type  = 0  ;
	
	public int getType(){
		return this.type;
	}
	
	public void setType(int v){
		this.type=v;
	}
	
	/**
	 * 
	 */
	public int room  = 0  ;
	
	public int getRoom(){
		return this.room;
	}
	
	public void setRoom(int v){
		this.room=v;
	}
	
	/**
	 * 
	 */
	public String rewardAndNum  = null  ;
	
	public String getRewardAndNum(){
		return this.rewardAndNum;
	}
	
	public void setRewardAndNum(String v){
		this.rewardAndNum=v;
	}
	
	/**
	 * 
	 */
	public int showNum  = 0  ;
	
	public int getShowNum(){
		return this.showNum;
	}
	
	public void setShowNum(int v){
		this.showNum=v;
	}
	
	/**
	 * 
	 */
	public int nextRoom  = 0  ;
	
	public int getNextRoom(){
		return this.nextRoom;
	}
	
	public void setNextRoom(int v){
		this.nextRoom=v;
	}
	
	
};