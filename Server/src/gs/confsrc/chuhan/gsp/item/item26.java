package chuhan.gsp.item;


public class item26 implements mytools.ConvMain.Checkable ,Comparable<item26>{

	public int compareTo(item26 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public item26(){
		super();
	}
	public item26(item26 arg){
		this.id=arg.id ;
		this.name=arg.name ;
		this.des=arg.des ;
		this.Icon=arg.Icon ;
		this.bag=arg.bag ;
		this.classname=arg.classname ;
		this.type=arg.type ;
		this.quality=arg.quality ;
		this.ifSell=arg.ifSell ;
		this.sellPrice=arg.sellPrice ;
		this.stackNum=arg.stackNum ;
		this.Usenumber=arg.Usenumber ;
		this.dropPackId=arg.dropPackId ;
		this.systemBroadcasts=arg.systemBroadcasts ;
		this.rune_type=arg.rune_type ;
		this.rune_specialHeroId=arg.rune_specialHeroId ;
		this.rune_quality=arg.rune_quality ;
		this.rune_baseAttri1=arg.rune_baseAttri1 ;
		this.rune_baseAttri2=arg.rune_baseAttri2 ;
		this.rune_baseAttri3=arg.rune_baseAttri3 ;
		this.rune_addAttri1=arg.rune_addAttri1 ;
		this.rune_addAttri2=arg.rune_addAttri2 ;
		this.rune_addAttri3=arg.rune_addAttri3 ;
		this.rune_addAttri4=arg.rune_addAttri4 ;
		this.rune_strengthenId=arg.rune_strengthenId ;
		this.rune_smelt=arg.rune_smelt ;
		this.rune_exposeCostType1=arg.rune_exposeCostType1 ;
		this.rune_exposeCostValue1=arg.rune_exposeCostValue1 ;
		this.rune_exposeCostType2=arg.rune_exposeCostType2 ;
		this.rune_exposeCostValue2=arg.rune_exposeCostValue2 ;
		this.rune_exposeCostType3=arg.rune_exposeCostType3 ;
		this.rune_exposeCostValue3=arg.rune_exposeCostValue3 ;
		this.rune_exposeCostType4=arg.rune_exposeCostType4 ;
		this.rune_exposeCostValue4=arg.rune_exposeCostValue4 ;
		this.specialHeroDes=arg.specialHeroDes ;
		this.improvexperience=arg.improvexperience ;
		this.HeroExp=arg.HeroExp ;
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
	public String name  = null  ;
	
	public String getName(){
		return this.name;
	}
	
	public void setName(String v){
		this.name=v;
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
	
	/**
	 * 
	 */
	public String Icon  = null  ;
	
	public String getIcon(){
		return this.Icon;
	}
	
	public void setIcon(String v){
		this.Icon=v;
	}
	
	/**
	 * 
	 */
	public int bag  = 0  ;
	
	public int getBag(){
		return this.bag;
	}
	
	public void setBag(int v){
		this.bag=v;
	}
	
	/**
	 * 
	 */
	public String classname  = null  ;
	
	public String getClassname(){
		return this.classname;
	}
	
	public void setClassname(String v){
		this.classname=v;
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
	public int quality  = 0  ;
	
	public int getQuality(){
		return this.quality;
	}
	
	public void setQuality(int v){
		this.quality=v;
	}
	
	/**
	 * 
	 */
	public int ifSell  = 0  ;
	
	public int getIfSell(){
		return this.ifSell;
	}
	
	public void setIfSell(int v){
		this.ifSell=v;
	}
	
	/**
	 * 
	 */
	public int sellPrice  = 0  ;
	
	public int getSellPrice(){
		return this.sellPrice;
	}
	
	public void setSellPrice(int v){
		this.sellPrice=v;
	}
	
	/**
	 * 
	 */
	public int stackNum  = 0  ;
	
	public int getStackNum(){
		return this.stackNum;
	}
	
	public void setStackNum(int v){
		this.stackNum=v;
	}
	
	/**
	 * 
	 */
	public int Usenumber  = 0  ;
	
	public int getUsenumber(){
		return this.Usenumber;
	}
	
	public void setUsenumber(int v){
		this.Usenumber=v;
	}
	
	/**
	 * 
	 */
	public int dropPackId  = 0  ;
	
	public int getDropPackId(){
		return this.dropPackId;
	}
	
	public void setDropPackId(int v){
		this.dropPackId=v;
	}
	
	/**
	 * 
	 */
	public int systemBroadcasts  = 0  ;
	
	public int getSystemBroadcasts(){
		return this.systemBroadcasts;
	}
	
	public void setSystemBroadcasts(int v){
		this.systemBroadcasts=v;
	}
	
	/**
	 * 
	 */
	public int rune_type  = 0  ;
	
	public int getRune_type(){
		return this.rune_type;
	}
	
	public void setRune_type(int v){
		this.rune_type=v;
	}
	
	/**
	 * 
	 */
	public String rune_specialHeroId  = null  ;
	
	public String getRune_specialHeroId(){
		return this.rune_specialHeroId;
	}
	
	public void setRune_specialHeroId(String v){
		this.rune_specialHeroId=v;
	}
	
	/**
	 * 
	 */
	public int rune_quality  = 0  ;
	
	public int getRune_quality(){
		return this.rune_quality;
	}
	
	public void setRune_quality(int v){
		this.rune_quality=v;
	}
	
	/**
	 * 
	 */
	public int rune_baseAttri1  = 0  ;
	
	public int getRune_baseAttri1(){
		return this.rune_baseAttri1;
	}
	
	public void setRune_baseAttri1(int v){
		this.rune_baseAttri1=v;
	}
	
	/**
	 * 
	 */
	public int rune_baseAttri2  = 0  ;
	
	public int getRune_baseAttri2(){
		return this.rune_baseAttri2;
	}
	
	public void setRune_baseAttri2(int v){
		this.rune_baseAttri2=v;
	}
	
	/**
	 * 
	 */
	public int rune_baseAttri3  = 0  ;
	
	public int getRune_baseAttri3(){
		return this.rune_baseAttri3;
	}
	
	public void setRune_baseAttri3(int v){
		this.rune_baseAttri3=v;
	}
	
	/**
	 * 
	 */
	public int rune_addAttri1  = 0  ;
	
	public int getRune_addAttri1(){
		return this.rune_addAttri1;
	}
	
	public void setRune_addAttri1(int v){
		this.rune_addAttri1=v;
	}
	
	/**
	 * 
	 */
	public int rune_addAttri2  = 0  ;
	
	public int getRune_addAttri2(){
		return this.rune_addAttri2;
	}
	
	public void setRune_addAttri2(int v){
		this.rune_addAttri2=v;
	}
	
	/**
	 * 
	 */
	public int rune_addAttri3  = 0  ;
	
	public int getRune_addAttri3(){
		return this.rune_addAttri3;
	}
	
	public void setRune_addAttri3(int v){
		this.rune_addAttri3=v;
	}
	
	/**
	 * 
	 */
	public int rune_addAttri4  = 0  ;
	
	public int getRune_addAttri4(){
		return this.rune_addAttri4;
	}
	
	public void setRune_addAttri4(int v){
		this.rune_addAttri4=v;
	}
	
	/**
	 * 
	 */
	public int rune_strengthenId  = 0  ;
	
	public int getRune_strengthenId(){
		return this.rune_strengthenId;
	}
	
	public void setRune_strengthenId(int v){
		this.rune_strengthenId=v;
	}
	
	/**
	 * 
	 */
	public int rune_smelt  = 0  ;
	
	public int getRune_smelt(){
		return this.rune_smelt;
	}
	
	public void setRune_smelt(int v){
		this.rune_smelt=v;
	}
	
	/**
	 * 
	 */
	public int rune_exposeCostType1  = 0  ;
	
	public int getRune_exposeCostType1(){
		return this.rune_exposeCostType1;
	}
	
	public void setRune_exposeCostType1(int v){
		this.rune_exposeCostType1=v;
	}
	
	/**
	 * 
	 */
	public int rune_exposeCostValue1  = 0  ;
	
	public int getRune_exposeCostValue1(){
		return this.rune_exposeCostValue1;
	}
	
	public void setRune_exposeCostValue1(int v){
		this.rune_exposeCostValue1=v;
	}
	
	/**
	 * 
	 */
	public int rune_exposeCostType2  = 0  ;
	
	public int getRune_exposeCostType2(){
		return this.rune_exposeCostType2;
	}
	
	public void setRune_exposeCostType2(int v){
		this.rune_exposeCostType2=v;
	}
	
	/**
	 * 
	 */
	public int rune_exposeCostValue2  = 0  ;
	
	public int getRune_exposeCostValue2(){
		return this.rune_exposeCostValue2;
	}
	
	public void setRune_exposeCostValue2(int v){
		this.rune_exposeCostValue2=v;
	}
	
	/**
	 * 
	 */
	public int rune_exposeCostType3  = 0  ;
	
	public int getRune_exposeCostType3(){
		return this.rune_exposeCostType3;
	}
	
	public void setRune_exposeCostType3(int v){
		this.rune_exposeCostType3=v;
	}
	
	/**
	 * 
	 */
	public int rune_exposeCostValue3  = 0  ;
	
	public int getRune_exposeCostValue3(){
		return this.rune_exposeCostValue3;
	}
	
	public void setRune_exposeCostValue3(int v){
		this.rune_exposeCostValue3=v;
	}
	
	/**
	 * 
	 */
	public int rune_exposeCostType4  = 0  ;
	
	public int getRune_exposeCostType4(){
		return this.rune_exposeCostType4;
	}
	
	public void setRune_exposeCostType4(int v){
		this.rune_exposeCostType4=v;
	}
	
	/**
	 * 
	 */
	public int rune_exposeCostValue4  = 0  ;
	
	public int getRune_exposeCostValue4(){
		return this.rune_exposeCostValue4;
	}
	
	public void setRune_exposeCostValue4(int v){
		this.rune_exposeCostValue4=v;
	}
	
	/**
	 * 
	 */
	public String specialHeroDes  = null  ;
	
	public String getSpecialHeroDes(){
		return this.specialHeroDes;
	}
	
	public void setSpecialHeroDes(String v){
		this.specialHeroDes=v;
	}
	
	/**
	 * 
	 */
	public int improvexperience  = 0  ;
	
	public int getImprovexperience(){
		return this.improvexperience;
	}
	
	public void setImprovexperience(int v){
		this.improvexperience=v;
	}
	
	/**
	 * 
	 */
	public int HeroExp  = 0  ;
	
	public int getHeroExp(){
		return this.HeroExp;
	}
	
	public void setHeroExp(int v){
		this.HeroExp=v;
	}
	
	
};