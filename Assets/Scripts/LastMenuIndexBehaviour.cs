using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMenuIndexBehaviour : MonoBehaviour
{
    private static bool dontDestroyed = false;
    private static int lastMenuIndex  = 0;

    void Start()
    {
        if (dontDestroyed)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        dontDestroyed = true;
        lastMenuIndex = 0;
    }

    public static LastMenuIndexBehaviour FindInstance()
    {
        return GameObject.Find("LastMenuIndex").GetComponent<LastMenuIndexBehaviour>();
    }

    public int GetIndex()
    {
        return lastMenuIndex;
    }

    public void SetIndex(int i)
    {
        lastMenuIndex = i;
    }
}
