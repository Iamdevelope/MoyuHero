package chuhan.gsp.item;


public class SItemList implements mytools.ConvMain.Checkable ,Comparable<SItemList>{

	public int compareTo(SItemList o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SItemList(){
		super();
	}
	public SItemList(SItemList arg){
		this.id=arg.id ;
		this.items=arg.items ;
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
	public java.util.ArrayList<Integer> items  ;
	
	public java.util.ArrayList<Integer> getItems(){
		return this.items;
	}
	
	public void setItems(java.util.ArrayList<Integer> v){
		this.items=v;
	}
	
	
};