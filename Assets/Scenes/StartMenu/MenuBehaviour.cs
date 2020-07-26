using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    public string dstSceneName;
    public bool isQuit;

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
        text.color = new Color(0.8f, 0.8f, 0.8f, 1f);
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
