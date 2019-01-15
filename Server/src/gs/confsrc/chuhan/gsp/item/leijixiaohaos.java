package chuhan.gsp.item;


public class leijixiaohaos implements mytools.ConvMain.Checkable ,Comparable<leijixiaohaos>{

	public int compareTo(leijixiaohaos o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public leijixiaohaos(){
		super();
	}
	public leijixiaohaos(leijixiaohaos arg){
		this.id=arg.id ;
		this.huodong=arg.huodong ;
		this.starttime=arg.starttime ;
		this.endtime=arg.endtime ;
		this.yuanbao=arg.yuanbao ;
		this.itemnum=arg.itemnum ;
		this.reward=arg.reward ;
		this.num=arg.num ;
		this.post=arg.post ;
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
	public int huodong  = 0  ;
	
	public int getHuodong(){
		return this.huodong;
	}
	
	public void setHuodong(int v){
		this.huodong=v;
	}
	
	/**
	 * 
	 */
	public String starttime  = null  ;
	
	public String getStarttime(){
		return this.starttime;
	}
	
	public void setStarttime(String v){
		this.starttime=v;
	}
	
	/**
	 * 
	 */
	public String endtime  = null  ;
	
	public String getEndtime(){
		return this.endtime;
	}
	
	public void setEndtime(String v){
		this.endtime=v;
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
	public String itemnum  = null  ;
	
	public String getItemnum(){
		return this.itemnum;
	}
	
	public void setItemnum(String v){
		this.itemnum=v;
	}
	
	/**
	 * 
	 */
	public String reward  = null  ;
	
	public String getReward(){
		return this.reward;
	}
	
	public void setReward(String v){
		this.reward=v;
	}
	
	/**
	 * 
	 */
	public int num  = 0  ;
	
	public int getNum(){
		return this.num;
	}
	
	public void setNum(int v){
		this.num=v;
	}
	
	/**
	 * 
	 */
	public int post  = 0  ;
	
	public int getPost(){
		return this.post;
	}
	
	public void setPost(int v){
		this.post=v;
	}
	
	
};