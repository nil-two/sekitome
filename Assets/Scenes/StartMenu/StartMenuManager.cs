﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    private const int MOVE_MENU_TYPE_UP     = 0;
    private const int MOVE_MENU_TYPE_DOWN   = 1;
    private const int MOVE_MENU_TYPE_ESCAPE = 2;

    public Animator fade;
    public MenuBehaviour startMenu;
    public MenuBehaviour resultMenu;
    public MenuBehaviour quitMenu;
    public float sceneTransitionSec;

    private MenuBehaviour[] menus;
    private int menuI;

    void Start()
    {
        menus = new MenuBehaviour[] {startMenu, resultMenu, quitMenu};
        menuI = 0;
        UpdateMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveMenu(MOVE_MENU_TYPE_UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveMenu(MOVE_MENU_TYPE_DOWN);
        }       
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
        {
            MoveMenu(MOVE_MENU_TYPE_ESCAPE);
        }       
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            SelectMenu();
        }       
    }

    void MoveMenu(int moveMenuType)
    {
        if (moveMenuType == MOVE_MENU_TYPE_UP)
        {
            menuI--;
        }
        else if (moveMenuType == MOVE_MENU_TYPE_DOWN)
        {
            menuI++;
        }
        else if (moveMenuType == MOVE_MENU_TYPE_ESCAPE)
        {
            if (menuI == menus.Length - 1)
            {
                SelectMenu();
                return;
            }
            else
            {
                menuI = menus.Length - 1;
            }
        }

        if (menuI < 0)
        {
            menuI = menus.Length + (menuI % menus.Length);
        }
        else if (menuI >= menus.Length)
        {
            menuI = menuI % menus.Length;
        }

        UpdateMenu();
    }

    void UpdateMenu()
    {
        foreach (var menu in menus)
        {
            menu.Inactivate();
        }
        menus[menuI].Activate();
    }

    void SelectMenu()
    {
        var menu = menus[menuI];
        if (menu.IsQuit())
        {
            Quit();
            return;
        }

        var dstSceneName = menu.GetDstSceneName();
        if (dstSceneName != null)
        {
            MoveScene(dstSceneName);
            return;
        }
    }

    void MoveScene(string dstSceneName)
    {
        fade.SetBool("fade", true);
        StartCoroutine(WaitLoadScene(dstSceneName, sceneTransitionSec));
    }

    IEnumerator WaitLoadScene(string dstSceneName, float waitSec)
    {
        yield return new WaitForSeconds(waitSec);
        SceneManager.LoadScene(dstSceneName);
    }

    void Quit()
    {
        fade.SetBool("fade", true);
        StartCoroutine(WaitQuit(sceneTransitionSec));
    }

    IEnumerator WaitQuit(float waitSec)
    {
        yield return new WaitForSeconds(waitSec);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        #endif
    }
}
