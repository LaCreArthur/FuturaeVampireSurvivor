using System;
using UnityEngine;

/// <summary>
///     A safe singleton pattern for MonoBehaviour classes. <br></br>
///     Access the instance through the Instance property.  <br></br>
///     Instance property is always safe and lazy-loaded.   <br></br>
///     - The instance will be created if it doesn't exist. <br></br>
///     - If an instance already exists, the new one will be destroyed. <br></br>
///     Override OnAwake() to add initialization code.  <br></br>
/// </summary>
/// <typeparam name="T">Use the name of your class</typeparam>
/// <example>
///     <c> public class PlayerTransform : SingletonMono&lt;PlayerTransform&gt;</c>
/// </example>
public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    static T s_instance;
    bool _isAwoken;

    public static T Instance
    {
        get {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Debug.LogError("Cannot call SingletonMB instance in Editor mode.");
                return null;
            }
#endif

            if (s_instance == null)
            {
                s_instance = FindAnyObjectByType<T>();
                if (s_instance == null)
                {
                    Type t = typeof(T);
                    s_instance = new GameObject(t.Name, t).GetComponent<T>();
                }

                s_instance.Init();
            }

            return s_instance;
        }
    }

    void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Debug.LogWarning($"An instance of \"{typeof(T).Name}\" already exists. Destroying duplicate one.",
                s_instance.gameObject);
            Destroy(this);
        }
        else
        {
            s_instance = this as T;
            Init();
        }
    }

    /// <summary>
    ///     Will be called only once when the instance is created.
    /// </summary>
    protected virtual void OnAwake() {}

    void Init()
    {
        if (_isAwoken)
            return;

        _isAwoken = true;
        DontDestroyOnLoad(transform.root.gameObject);
        OnAwake();
    }
}
