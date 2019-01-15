using UnityEngine;
using System.Collections;

public class HUDFPS : MonoBehaviour
{
	
	// Attach this to a GUIText to make a frames/second indicator.
	//
	// It calculates frames/second over each updateInterval,
	// so the display does not keep changing wildly.
	//
	// It is also fairly accurate at very low FPS counts (<10).
	// We do this not by simply counting frames per interval, but
	// by accumulating FPS for each frame. This way we end up with
	// correct overall FPS even if the interval renders something like
	// 5.5 frames.
     //UILabel FPS;
	public static string finalFPS;
	public  float updateInterval = 0.5F;
	
	private float accum   = 0; // FPS accumulated over the interval
	private int   frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	private Rect _window_rect;

    void Awake()
    {
     
    }

	void Start()
    {
        //if (AppManager.Inst.appType == AppType.OutSideA || AppManager.Inst.appType == AppType.OutSideR)
        //{
        //    GameObject.Destroy(this);
        //}
        //FPS = gameObject.GetComponent<UILabel>();
		timeleft = updateInterval;  
		_window_rect = new Rect(10, 10, 300, 32);
         
        // 只能在编辑器模式下打印
        //Profiler.logFile = "mylog.log";
        //Profiler.enableBinaryLog = false;
        //Profiler.enabled = true;
   
	}
	
	void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
        //if (FPS == null)
            //return;
		// Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0 )
		{
			// display two fractional digits (f2 format)
			float fps = accum/frames;
			string format = System.String.Format("{0:F2}",fps);
			//FPS.text= format.ToString();
			finalFPS = format.ToString();
			
			//  DebugConsole.Log(format,level);
			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
		}
	}

	void OnGUI()
	{
		_window_rect = GUILayout.Window(0, _window_rect, DemoWindow, "Test Demo");
		//GUILayout.Label("FPS : " + HUDFPS.finalFPS);
	}
	private void DemoWindow(int id)
	{
		GUILayout.Label("FPS : " + HUDFPS.finalFPS);
        //GUILayout.Label("MonoHeapSize: " + Profiler.GetMonoHeapSize() / 1024 / 1024 + " MB");
        //GUILayout.Label("MonoUsedSize: " + Profiler.GetMonoUsedSize() / 1024 / 1024 + " MB");
        //GUILayout.Label("UsedHeapSize: " + Profiler.usedHeapSize / 1024 / 1024 + " MB");
        //GUILayout.Label("TotalAllocatedMemory: " + Profiler.GetTotalAllocatedMemory() / 1024 / 1024 + " MB");
        //GUILayout.Label("TotalReservedMemory: " + Profiler.GetTotalReservedMemory() / 1024 / 1024 + " MB");
        //GUILayout.Label("TotalUnusedReservedMemory: " + Profiler.GetTotalUnusedReservedMemory() / 1024 / 1024 + " MB");
		GUI.DragWindow(new Rect(0, 0, 300, 10000));
	}

}