using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    private static bool shuttingDown = false;
    private readonly static object locker = new object();
    public static T Instance
    {
        get
        {
            if (shuttingDown)
            {
                Debug.Log("[Singleton] Instance '" + typeof(T) + "' already CHOIed. Returning null.");
                return null;
            }

            lock (locker)
            {
                if(instance == null)
                {
                    instance = (T)FindObjectOfType((typeof(T)));
                    if(instance == null)
                    {
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                        DontDestroyOnLoad(instance);
                    }
                }
            }

            return instance;
        }
    }

    private void OnApplicationQuit()
    {
        shuttingDown = true;
    }

    private void OnDestroy()
    {
        shuttingDown = true;
    }
}
