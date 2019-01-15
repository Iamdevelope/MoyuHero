package chuhan.gsp.game;


public class chuodongduihuan implements mytools.ConvMain.Checkable ,Comparable<chuodongduihuan>{

	public int compareTo(chuodongduihuan o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public chuodongduihuan(){
		super();
	}
	public chuodongduihuan(chuodongduihuan arg){
		this.id=arg.id ;
		this.huodongtype=arg.huodongtype ;
		this.jiangliid=arg.jiangliid ;
		this.jianglinum=arg.jianglinum ;
		this.tiaojianleixing=arg.tiaojianleixing ;
		this.tiaojianshuliang=arg.tiaojianshuliang ;
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
	public int huodongtype  = 0  ;
	
	public int getHuodongtype(){
		return this.huodongtype;
	}
	
	public void setHuodongtype(int v){
		this.huodongtype=v;
	}
	
	/**
	 * 
	 */
	public int jiangliid  = 0  ;
	
	public int getJiangliid(){
		return this.jiangliid;
	}
	
	public void setJiangliid(int v){
		this.jiangliid=v;
	}
	
	/**
	 * 
	 */
	public int jianglinum  = 0  ;
	
	public int getJianglinum(){
		return this.jianglinum;
	}
	
	public void setJianglinum(int v){
		this.jianglinum=v;
	}
	
	/**
	 * 
	 */
	public int tiaojianleixing  = 0  ;
	
	public int getTiaojianleixing(){
		return this.tiaojianleixing;
	}
	
	public void setTiaojianleixing(int v){
		this.tiaojianleixing=v;
	}
	
	/**
	 * 
	 */
	public int tiaojianshuliang  = 0  ;
	
	public int getTiaojianshuliang(){
		return this.tiaojianshuliang;
	}
	
	public void setTiaojianshuliang(int v){
		this.tiaojianshuliang=v;
	}
	
	
};