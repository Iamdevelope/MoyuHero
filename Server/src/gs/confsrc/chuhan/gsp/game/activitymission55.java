package chuhan.gsp.game;


public class activitymission55 implements mytools.ConvMain.Checkable ,Comparable<activitymission55>{

	public int compareTo(activitymission55 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public activitymission55(){
		super();
	}
	public activitymission55(activitymission55 arg){
		this.id=arg.id ;
		this.stagecondition=arg.stagecondition ;
		this.levelcondition=arg.levelcondition ;
		this.selecttype=arg.selecttype ;
		this.type=arg.type ;
		this.parameter=arg.parameter ;
		this.times=arg.times ;
		this.activitydegree=arg.activitydegree ;
		this.paytype=arg.paytype ;
		this.des=arg.des ;
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
	public int stagecondition  = 0  ;
	
	public int getStagecondition(){
		return this.stagecondition;
	}
	
	public void setStagecondition(int v){
		this.stagecondition=v;
	}
	
	/**
	 * 
	 */
	public int levelcondition  = 0  ;
	
	public int getLevelcondition(){
		return this.levelcondition;
	}
	
	public void setLevelcondition(int v){
		this.levelcondition=v;
	}
	
	/**
	 * 
	 */
	public int selecttype  = 0  ;
	
	public int getSelecttype(){
		return this.selecttype;
	}
	
	public void setSelecttype(int v){
		this.selecttype=v;
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
	public String parameter  = null  ;
	
	public String getParameter(){
		return this.parameter;
	}
	
	public void setParameter(String v){
		this.parameter=v;
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
	public int activitydegree  = 0  ;
	
	public int getActivitydegree(){
		return this.activitydegree;
	}
	
	public void setActivitydegree(int v){
		this.activitydegree=v;
	}
	
	/**
	 * 
	 */
	public int paytype  = 0  ;
	
	public int getPaytype(){
		return this.paytype;
	}
	
	public void setPaytype(int v){
		this.paytype=v;
	}
	
	/**
	 * 
	 */
	public String des  = null  ;
	
	public String getDes(){
		return this.des;
	}
	
	public void setDes(String v){
		this.des=v;
	}
	
	
};