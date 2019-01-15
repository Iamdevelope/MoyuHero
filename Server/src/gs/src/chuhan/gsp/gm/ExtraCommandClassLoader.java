package chuhan.gsp.gm;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.util.Enumeration;
import java.util.HashSet;
import java.util.Set;
import java.util.jar.JarEntry;
import java.util.jar.JarFile;

import org.apache.log4j.Logger;

public class ExtraCommandClassLoader extends ClassLoader 
{
	private static Logger logger = Logger.getLogger(ExtraCommandClassLoader.class);

	private String basedir; // 需要该类加载器直接加载的jar文件的基目录

	private Set<String> dynaclazns; // 需要由该类加载器直接加载的类名

	public ExtraCommandClassLoader(String basedir, String clazns) {

		super(null); // 指定父类加载器为 null
		this.basedir = basedir;
		dynaclazns = new HashSet<String>();
		loadClassByMe(clazns);
	}

	private void loadClassByMe(String clazns) {

			loadDirectly(clazns);
			
		
	}

	private Class<?> loadDirectly(String name) {

		Class<?> cls = null;
		StringBuffer sb = new StringBuffer(basedir);
		sb.append(File.separator + name);
		JarFile jarFile = null;
		try {
			jarFile = new JarFile(sb.toString());
			JarEntry entry ;
			Enumeration<JarEntry> enums = jarFile.entries();
			while (enums.hasMoreElements()) {
				entry = enums.nextElement();
				if (entry.getName().endsWith(".class")){
					String className = entry.getName().replaceAll("/", ".");
					className = className.substring(0,className.lastIndexOf('.'));
					InputStream inputStream = jarFile.getInputStream(entry);
					cls = instantiateClass(className, inputStream, inputStream.available());
					dynaclazns.add(className);
				}
			}
			jarFile.close();
		} catch (Exception e) {
			try{
				if(jarFile != null)
					jarFile.close();
			} catch (Exception e1){
				e1.printStackTrace();
			}
			logger.error("hotdeploy class error", e);
		}
		return cls;
	}

	private Class<?> instantiateClass(String name, InputStream fin, long len) {

		byte[] raw = new byte[(int) len];
		try {
			fin.read(raw);
			fin.close();
		} catch (IOException e) {
			logger.error("hotdeploy class error", e);
		}
		return defineClass(name, raw, 0, raw.length);
	}

	public synchronized Class<?> loadClass(String name, boolean resolve) throws ClassNotFoundException {

		Class<?> cls = null;
		cls = findLoadedClass(name);
		if (!this.dynaclazns.contains(name) && cls == null)
			cls = getSystemClassLoader().loadClass(name);
		if (cls == null)
			throw new ClassNotFoundException(name);
		if (resolve)
			resolveClass(cls);
		return cls;
	}

    public boolean loadByMe(String className){
        return this.dynaclazns.contains(className);
    }
}
