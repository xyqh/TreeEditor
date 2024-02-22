public class Singleton<T> where T : class, new()
{
    private static readonly object _instanceLock = new object();
    private static T _instance = null;

    public static T GetInstance()
    {
        if (_instance == null)
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
            }
        }

        return _instance;
    }

    public virtual void OnInit()
    {
        
    }

    public virtual void OnDestroyed()
    {
        
    }
}