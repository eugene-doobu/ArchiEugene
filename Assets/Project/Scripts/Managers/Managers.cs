using System.Collections;
using System.Collections.Generic;
using ArchiEugene.Azure;
using ArchiEugene.Communication;
using UnityEngine;
using ArchiEugene.UI;
using ArchiEugene.Resources;
using ArchiEugene.Scene;
using ArchiEugene.UserProp;
using ArchiEugene.XRToolkit;

namespace ArchiEugene
{
	public class Managers : MonoBehaviour
	{
	    static Managers s_instance; // 유일성이 보장된다
	    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

		#region Contents
		private CommunicationManager _communication = new CommunicationManager();
		private UserPropManager _userProp = new UserPropManager();

		public static CommunicationManager Communication => Instance._communication;
		public static UserPropManager UserProp => Instance._userProp;
		#endregion

		#region Core
		private DataManager _data = new DataManager();
		private PoolManager _pool = new PoolManager();
	    private ResourceManager _resource = new ResourceManager();
	    private SceneManagerEx _scene = new SceneManagerEx();
	    private SoundManager _sound = new SoundManager();
	    private UIManager _ui = new UIManager();
	    private XRManager _xr = new XRManager();
	    private AzureManager _azure = new AzureManager();

	    public static DataManager Data => Instance._data;
	    public static PoolManager Pool => Instance._pool;
	    public static ResourceManager Resource => Instance._resource;
	    public static SceneManagerEx Scene => Instance._scene;
	    public static SoundManager Sound => Instance._sound;
	    public static UIManager UI => Instance._ui;
	    public static XRManager XR => Instance._xr;
	    public static AzureManager Azure = Instance._azure;
		#endregion

	    public static void Init()
	    {
	        if (s_instance == null)
	        {
				GameObject go = GameObject.Find("@Managers");
	            if (go == null)
	            {
	                go = new GameObject { name = "@Managers" };
	                go.AddComponent<Managers>();
	            }

	            DontDestroyOnLoad(go);
	            s_instance = go.GetComponent<Managers>();
	            
	            CoreManagerInit();
	            ContentManagerInit();
	        }		
		}

	    public static void Clear()
	    {
	        Sound.Clear();
	        Scene.Clear();
	        UI.Clear();
	        Pool.Clear();
	    }

	    private static void CoreManagerInit()
	    {
		    s_instance._data.Init();
		    s_instance._pool.Init();
		    s_instance._sound.Init();
		    s_instance._ui.Init();
		    s_instance._xr.Init();
		    s_instance._azure.Init();
	    }

	    private static void ContentManagerInit()
	    {
		    s_instance._communication.Init();
		    s_instance._userProp.Init();
	    }
	}
}
