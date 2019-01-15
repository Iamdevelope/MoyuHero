package chuhan.gsp.game;


public class czhuzhanconfig implements mytools.ConvMain.Checkable ,Comparable<czhuzhanconfig>{

	public int compareTo(czhuzhanconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public czhuzhanconfig(){
		super();
	}
	public czhuzhanconfig(czhuzhanconfig arg){
		this.id=arg.id ;
		this.xiaohaoyuanbaopve=arg.xiaohaoyuanbaopve ;
		this.gongxunjianglipve=arg.gongxunjianglipve ;
		this.xiaohaoyuanbaopvp=arg.xiaohaoyuanbaopvp ;
		this.gongxunjianglipvp=arg.gongxunjianglipvp ;
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
	public int xiaohaoyuanbaopve  = 0  ;
	
	public int getXiaohaoyuanbaopve(){
		return this.xiaohaoyuanbaopve;
	}
	
	public void setXiaohaoyuanbaopve(int v){
		this.xiaohaoyuanbaopve=v;
	}
	
	/**
	 * 
	 */
	public int gongxunjianglipve  = 0  ;
	
	public int getGongxunjianglipve(){
		return this.gongxunjianglipve;
	}
	
	public void setGongxunjianglipve(int v){
		this.gongxunjianglipve=v;
	}
	
	/**
	 * 
	 */
	public int xiaohaoyuanbaopvp  = 0  ;
	
	public int getXiaohaoyuanbaopvp(){
		return this.xiaohaoyuanbaopvp;
	}
	
	public void setXiaohaoyuanbaopvp(int v){
		this.xiaohaoyuanbaopvp=v;
	}
	
	/**
	 * 
	 */
	public int gongxunjianglipvp  = 0  ;
	
	public int getGongxunjianglipvp(){
		return this.gongxunjianglipvp;
	}
	
	public void setGongxunjianglipvp(int v){
		this.gongxunjianglipvp=v;
	}
	
	
};