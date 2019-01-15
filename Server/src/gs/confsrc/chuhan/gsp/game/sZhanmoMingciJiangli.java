package chuhan.gsp.game;


public class sZhanmoMingciJiangli implements mytools.ConvMain.Checkable ,Comparable<sZhanmoMingciJiangli>{

	public int compareTo(sZhanmoMingciJiangli o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public sZhanmoMingciJiangli(){
		super();
	}
	public sZhanmoMingciJiangli(sZhanmoMingciJiangli arg){
		this.id=arg.id ;
		this.Rank=arg.Rank ;
		this.Item_ID=arg.Item_ID ;
		this.Item_num=arg.Item_num ;
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
	public int Rank  = 0  ;
	
	public int getRank(){
		return this.Rank;
	}
	
	public void setRank(int v){
		this.Rank=v;
	}
	
	/**
	 * 
	 */
	public int Item_ID  = 0  ;
	
	public int getItem_ID(){
		return this.Item_ID;
	}
	
	public void setItem_ID(int v){
		this.Item_ID=v;
	}
	
	/**
	 * 
	 */
	public int Item_num  = 0  ;
	
	public int getItem_num(){
		return this.Item_num;
	}
	
	public void setItem_num(int v){
		this.Item_num=v;
	}
	
	
};