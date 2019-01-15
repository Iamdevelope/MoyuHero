package chuhan.gsp.hotdeploy;

import java.lang.reflect.Constructor;
import java.util.HashMap;
import java.util.Map;

import xdb.Procedure;

import chuhan.gsp.HotdeployClass;
import chuhan.gsp.gm.ExtraCommandClassLoader;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.ModuleInterface;
import chuhan.gsp.main.ReloadResult;


public class Module implements ModuleInterface {

	static boolean hasNewClass = false;
    static Map<String, Class<? extends xdb.Procedure>> classes = new HashMap<String, Class<? extends xdb.Procedure>>();
    @Override
    public void exit() {
        //To change body of implemented methods use File | Settings | File Templates.
    }

    @Override
    public void init() throws Exception {
        //起服务器的时候不用做任何事情
    }

    @SuppressWarnings("unchecked")
	@Override
    public ReloadResult reload() throws Exception {
    	classes.clear();

        String jarfilename = ConfigManager.getInstance().getPropConf("sys").getProperty("sys.hotdeploy.jarfile");
        ExtraCommandClassLoader clzloader = new ExtraCommandClassLoader(".", jarfilename);

        Map<Integer,HotdeployClass> confs = ConfigManager.getInstance().getConf(HotdeployClass.class);
        for (HotdeployClass sHotdeployClass : confs.values()){
            if (clzloader.loadByMe(sHotdeployClass.newClassName)) {
                classes.put(sHotdeployClass.oldClassName, (Class<? extends Procedure>) clzloader.loadClass(sHotdeployClass.newClassName));
            }else{

                return new ReloadResult(false);
            }
        }
        hasNewClass = true;
        return new ReloadResult(true);
    }

    public static Procedure getHotdeployProcedure(String className, Procedure oldProc) throws Exception {
        Class<? extends xdb.Procedure> newProcedureClass = classes.get(className);
        if(newProcedureClass != null){
            Constructor<? extends xdb.Procedure> constructor = newProcedureClass.getConstructor(Procedure.class);
            return constructor.newInstance(oldProc);
        }
        return null;
    }
    
    public static boolean hasNewClass(){
    	return hasNewClass;
    }
}
