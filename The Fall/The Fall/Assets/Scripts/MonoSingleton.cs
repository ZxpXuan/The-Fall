using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance) return _instance;

            _instance = FindObjectOfType<T>();
            if (_instance) return _instance;

            _instance = new GameObject(typeof(T).Name).AddComponent<T>();
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
