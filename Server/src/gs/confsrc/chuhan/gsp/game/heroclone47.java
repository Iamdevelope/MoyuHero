package chuhan.gsp.game;


public class heroclone47 implements mytools.ConvMain.Checkable ,Comparable<heroclone47>{

	public int compareTo(heroclone47 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public heroclone47(){
		super();
	}
	public heroclone47(heroclone47 arg){
		this.id=arg.id ;
		this.sortID=arg.sortID ;
		this.cloneCostId=arg.cloneCostId ;
		this.cloneCostValue=arg.cloneCostValue ;
		this.openCondition=arg.openCondition ;
		this.openConditionDes=arg.openConditionDes ;
		this.is_had=arg.is_had ;
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
	public int sortID  = 0  ;
	
	public int getSortID(){
		return this.sortID;
	}
	
	public void setSortID(int v){
		this.sortID=v;
	}
	
	/**
	 * 
	 */
	public int cloneCostId  = 0  ;
	
	public int getCloneCostId(){
		return this.cloneCostId;
	}
	
	public void setCloneCostId(int v){
		this.cloneCostId=v;
	}
	
	/**
	 * 
	 */
	public int cloneCostValue  = 0  ;
	
	public int getCloneCostValue(){
		return this.cloneCostValue;
	}
	
	public void setCloneCostValue(int v){
		this.cloneCostValue=v;
	}
	
	/**
	 * 
	 */
	public int openCondition  = 0  ;
	
	public int getOpenCondition(){
		return this.openCondition;
	}
	
	public void setOpenCondition(int v){
		this.openCondition=v;
	}
	
	/**
	 * 
	 */
	public String openConditionDes  = null  ;
	
	public String getOpenConditionDes(){
		return this.openConditionDes;
	}
	
	public void setOpenConditionDes(String v){
		this.openConditionDes=v;
	}
	
	/**
	 * 
	 */
	public int is_had  = 0  ;
	
	public int getIs_had(){
		return this.is_had;
	}
	
	public void setIs_had(int v){
		this.is_had=v;
	}
	
	
};