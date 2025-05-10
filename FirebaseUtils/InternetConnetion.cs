using UnityEngine;
public static class InternetConnetion 
{
    public static bool IsAvailable()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}
