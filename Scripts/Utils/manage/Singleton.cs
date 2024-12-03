// using Unity.Netcode;
using UnityEngine;

namespace Wolf
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T singleton { get; private set; }

        public virtual void Awake()
        {
            if (singleton == null)
            {
                singleton = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public class SingletonDontDestroy<T> : MonoBehaviour where T : Component
    {
        public static T singleton { get; private set; }

        public virtual void Awake()
        {
            if (singleton == null)
            {
                singleton = this as T;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    //public class NetworkBehaviourSingleton<T> : NetworkBehaviour where T : Component
    //{
    //    public static T Instance { get; private set; }

    //    public virtual void Awake()
    //    {
    //        if (Instance == null)
    //        {
    //            Instance = this as T;
    //        }
    //        else
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    //public class NetworkBehaviourSingletonDontDestroy<T> : NetworkBehaviour where T : Component
    //{
    //    public static T Instance { get; private set; }

    //    public virtual void Awake()
    //    {
    //        if (Instance == null)
    //        {
    //            Instance = this as T;
    //            DontDestroyOnLoad(this);
    //        }
    //        else
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}