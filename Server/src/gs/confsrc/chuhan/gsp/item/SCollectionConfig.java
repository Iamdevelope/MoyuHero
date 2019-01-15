package chuhan.gsp.item;


public class SCollectionConfig implements mytools.ConvMain.Checkable ,Comparable<SCollectionConfig>{

	public int compareTo(SCollectionConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SCollectionConfig(){
		super();
	}
	public SCollectionConfig(SCollectionConfig arg){
		this.id=arg.id ;
		this.collectionitem=arg.collectionitem ;
		this.reward=arg.reward ;
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
	public java.util.ArrayList<Integer> collectionitem  ;
	
	public java.util.ArrayList<Integer> getCollectionitem(){
		return this.collectionitem;
	}
	
	public void setCollectionitem(java.util.ArrayList<Integer> v){
		this.collectionitem=v;
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
	
	
};