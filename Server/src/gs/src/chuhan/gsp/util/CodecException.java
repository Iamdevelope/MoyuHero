package chuhan.gsp.util;

public class CodecException extends Exception {
	private static final long serialVersionUID = 1L;
	
	private String exceptionInfo = null;

	public CodecException(){
		super();
	}
	
	public CodecException(String errorMessage){
		super(errorMessage);
		this.exceptionInfo = errorMessage;
	}
	
	public CodecException(String errorMessage,Throwable e){
		super(errorMessage,e);
		this.exceptionInfo = errorMessage;
		e.printStackTrace();
	}
	
	public CodecException(Throwable e){
		super(e);
		e.printStackTrace();
	}
	
	public String getExceptionMessage(){
		return exceptionInfo;
	}
	
	public String toString() {
		return this.getClass().getName()+"{"
				+ ", exceptionInfo='"
				+ exceptionInfo
				+ "'}";
	}


}
