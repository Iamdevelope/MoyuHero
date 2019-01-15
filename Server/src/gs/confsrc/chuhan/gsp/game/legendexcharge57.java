package chuhan.gsp.game;


public class legendexcharge57 implements mytools.ConvMain.Checkable ,Comparable<legendexcharge57>{

	public int compareTo(legendexcharge57 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public legendexcharge57(){
		super();
	}
	public legendexcharge57(legendexcharge57 arg){
		this.id=arg.id ;
		this.type=arg.type ;
		this.cost=arg.cost ;
		this.reward=arg.reward ;
		this.show=arg.show ;
		this.probability=arg.probability ;
		this.sort=arg.sort ;
		this.name=arg.name ;
		this.comment=arg.comment ;
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
	public int cost  = 0  ;
	
	public int getCost(){
		return this.cost;
	}
	
	public void setCost(int v){
		this.cost=v;
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
	public String show  = null  ;
	
	public String getShow(){
		return this.show;
	}
	
	public void setShow(String v){
		this.show=v;
	}
	
	/**
	 * 
	 */
	public int probability  = 0  ;
	
	public int getProbability(){
		return this.probability;
	}
	
	public void setProbability(int v){
		this.probability=v;
	}
	
	/**
	 * 
	 */
	public int sort  = 0  ;
	
	public int getSort(){
		return this.sort;
	}
	
	public void setSort(int v){
		this.sort=v;
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
	public String comment  = null  ;
	
	public String getComment(){
		return this.comment;
	}
	
	public void setComment(String v){
		this.comment=v;
	}
	
	
};