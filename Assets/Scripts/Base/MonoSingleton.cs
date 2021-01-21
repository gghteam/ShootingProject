using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    private readonly static object locker = new object();
    public static T Instance
    {
        get
        {
            lock (locker)
            {
                if(instance == null)
                {
                    instance = (T)FindObjectOfType((typeof(T)));
                    if(instance == null)
                    {
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                    }
                }
            }

            return instance;
        }
    }
}
