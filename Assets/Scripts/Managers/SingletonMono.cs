using System;
using UnityEngine;

/// <summary>
///     A safe singleton pattern for MonoBehaviour classes.
/// </summary>
/// <typeparam name="T"></typeparam>
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
