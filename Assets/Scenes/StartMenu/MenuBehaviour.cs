using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    public string dstSceneName;
    public bool isQuit;
    public Color activeColor;
    public Color inactiveColor;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void Activate()
    {
        text.color = activeColor;
    }

    public void Inactivate()
    {
        text.color = inactiveColor;
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
