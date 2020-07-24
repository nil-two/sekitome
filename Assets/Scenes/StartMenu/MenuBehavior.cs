using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehavior : MonoBehaviour
{
    public string dstSceneName = null;
    public bool isQuit         = false;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void Activate()
    {
        text.color = new Color(1f, 1f, 1f, 1f);
    }

    public void Inactivate()
    {
        text.color = new Color(0.6f, 0.6f, 0.6f, 1f);
    }

    public bool IsQuit()
    {
        return isQuit;
    }

    public string GetDstSceneName()
    {
        return dstSceneName;
    }
}
