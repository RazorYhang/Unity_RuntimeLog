using UnityEngine;
using System.Collections;

/// Runtime log.
/// Author: ZiZi
/// Birthday: 2015/7/23
/// 
/// To Use:
/// 	attach this stuff on a camera with GUIlayer, and done.
/// API:
/// 	RuntimeLog.Log("blablabla")					Add a normal log
///  	RuntimeLog.Error / Warning("blablabla")		Add a error / warning log	
/// 	RuntimeLog.Clear()							Clear all the logs in log stack.
///     messegeColor								Change color of the log
/// 	MessegeBoxRect								The position of the log box.
[RequireComponent(typeof (Camera))]		// Need one camera
[RequireComponent(typeof (GUILayer))]	// with GUI layer
[ExecuteInEditMode]						
[DisallowMultipleComponent]				
public class RuntimeLog : MonoBehaviour {

	#region Const
	static readonly int DEFAULT_BOX_WIDTH = 1024;
	static readonly int DEFAULT_BOX_HEIGHT = 800;
	static readonly int DEFAULT_BOX_TOP = 10;
	static readonly int DEFAULT_BOX_LEFT = 10;
	#endregion

	#region Static	// Statics
	static protected string 	m_logStack;				// log string
	static private int 			maxLogCount = 20;	// Max number of logs on screen
	static public int 			MaxLogCount { get { return maxLogCount; }
											  set { maxLogCount = value;
													RuntimeLog.ClearLog();}}

	static protected int 		logCount = 0;	// Number of logs in log stack.
	static public int 			LogCount 	{ get { return logCount; } }

	/// <summary>
	/// Add a log to the runtime log.
	/// </summary>
	/// <param name="msg">log.</param>
	static public void Log<T>( T msg ){
		// Add msg
		// Every msg end with a '\n'
		m_logStack += msg+"\n";

		if (logCount >= maxLogCount) {
			//TODO: delete first msg, then add msg.
			// Need more checking such as give a"\n\n\nmsg\n\n" input.
			int i = 0;
			while(m_logStack[i] != '\n' && i < m_logStack.Length){
				++i;
			}

			if(i < m_logStack.Length){
				m_logStack = m_logStack.Remove(0, i + 1); // move msg, inlcude '\n'
			}
		}

		else {
			logCount++;
		}
	}

	/// <summary>
	/// Add a warning log to the runtime log.
	/// </summary>
	/// <param name="msg">log.</param>
	static public void WarningLog(string msg){
		m_logStack += "[Warning]: ";
		RuntimeLog.Log (msg);
	}

	/// <summary>
	/// Add a error log to the runtime log.
	/// </summary>
	/// <param name="msg">log.</param>
	static public void ErrorLog(string msg){
		m_logStack += "[ERROR]: ";
		RuntimeLog.Log (msg);
	}

	/// <summary>
	/// Clear all the runtime log.
	/// </summary>
	static void ClearLog(){
		m_logStack = "";
		logCount = 0;
	}
	#endregion

	public Color   logColor = Color.red;	// color of the log on this camera.
	protected Rect logBoxRect;				// Log box of the log on this camera.
	public Rect    LogBoxRect 				{get {return logBoxRect;} set {logBoxRect = value;}}
	public bool    isDisplay = true;		// Display the log on screen?

	void Start() {
		Initialize ();
	}

/// Test Case:
/// 
//	void Update (){
//		if (Input.GetKeyUp (KeyCode.Space)) {
//			RuntimeLog.ClearLog();
//		}
//
//		if (Input.GetKeyUp (KeyCode.LeftControl)) {
//			RuntimeLog.Log("This is a messege.");
//		}
//
//		if (Input.GetKeyUp (KeyCode.LeftAlt)) {
//			RuntimeLog.ErrorLog("This is a error.");
//		}
//		if (Input.GetKeyUp (KeyCode.A)) {
//			RuntimeLog.Log("");
//		}
//		if (Input.GetKeyUp (KeyCode.B)) {
//			RuntimeLog.Log('\n');
//		}
//	}

	void OnGUI () 
	{	
		if (isDisplay) {
			GUI.contentColor = logColor;
			GUI.Label (logBoxRect, m_logStack);
		}
	}

	protected void Initialize () {
		logBoxRect = new Rect (DEFAULT_BOX_TOP, DEFAULT_BOX_LEFT, DEFAULT_BOX_WIDTH, DEFAULT_BOX_HEIGHT);
		m_logStack = "";
	}
}
