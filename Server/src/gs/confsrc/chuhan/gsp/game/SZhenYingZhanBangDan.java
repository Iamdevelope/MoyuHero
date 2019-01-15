package chuhan.gsp.game;


public class SZhenYingZhanBangDan implements mytools.ConvMain.Checkable ,Comparable<SZhenYingZhanBangDan>{

	public int compareTo(SZhenYingZhanBangDan o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SZhenYingZhanBangDan(){
		super();
	}
	public SZhenYingZhanBangDan(SZhenYingZhanBangDan arg){
		this.id=arg.id ;
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