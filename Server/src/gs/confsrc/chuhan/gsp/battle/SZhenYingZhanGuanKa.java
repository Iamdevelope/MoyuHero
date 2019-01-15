package chuhan.gsp.battle;


public class SZhenYingZhanGuanKa implements mytools.ConvMain.Checkable ,Comparable<SZhenYingZhanGuanKa>{

	public int compareTo(SZhenYingZhanGuanKa o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SZhenYingZhanGuanKa(){
		super();
	}
	public SZhenYingZhanGuanKa(SZhenYingZhanGuanKa arg){
		this.id=arg.id ;
		this.bingli=arg.bingli ;
		this.gongji=arg.gongji ;
		this.fangyu=arg.fangyu ;
		this.jilue=arg.jilue ;
		this.award_id=arg.award_id ;
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
	public double bingli  = 0.0  ;
	
	public double getBingli(){
		return this.bingli;
	}
	
	public void setBingli(double v){
		this.bingli=v;
	}
	
	/**
	 * 
	 */
	public double gongji  = 0.0  ;
	
	public double getGongji(){
		return this.gongji;
	}
	
	public void setGongji(double v){
		this.gongji=v;
	}
	
	/**
	 * 
	 */
	public double fangyu  = 0.0  ;
	
	public double getFangyu(){
		return this.fangyu;
	}
	
	public void setFangyu(double v){
		this.fangyu=v;
	}
	
	/**
	 * 
	 */
	public double jilue  = 0.0  ;
	
	public double getJilue(){
		return this.jilue;
	}
	
	public void setJilue(double v){
		this.jilue=v;
	}
	
	/**
	 * 
	 */
	public int award_id  = 0  ;
	
	public int getAward_id(){
		return this.award_id;
	}
	
	public void setAward_id(int v){
		this.award_id=v;
	}
	
	
};