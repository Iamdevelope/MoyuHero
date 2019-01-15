package chuhan.gsp.main;

/**
 * 模块的初始化写在init函数里
 * 模块的清理操作写在exit函数里
 *
 */
public interface ModuleInterface {
	public void exit();
	public void init() throws Exception;
	public ReloadResult reload() throws Exception;
}
