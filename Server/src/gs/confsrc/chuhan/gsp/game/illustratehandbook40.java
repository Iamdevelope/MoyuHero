package chuhan.gsp.game;


public class illustratehandbook40 implements mytools.ConvMain.Checkable ,Comparable<illustratehandbook40>{

	public int compareTo(illustratehandbook40 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public illustratehandbook40(){
		super();
	}
	public illustratehandbook40(illustratehandbook40 arg){
		this.id=arg.id ;
		this.type=arg.type ;
		this.contentId=arg.contentId ;
		this.reward=arg.reward ;
		this.sortingId=arg.sortingId ;
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
	public int contentId  = 0  ;
	
	public int getContentId(){
		return this.contentId;
	}
	
	public void setContentId(int v){
		this.contentId=v;
	}
	
	/**
	 * 
	 */
	public int reward  = 0  ;
	
	public int getReward(){
		return this.reward;
	}
	
	public void setReward(int v){
		this.reward=v;
	}
	
	/**
	 * 
	 */
	public int sortingId  = 0  ;
	
	public int getSortingId(){
		return this.sortingId;
	}
	
	public void setSortingId(int v){
		this.sortingId=v;
	}
	
	
};