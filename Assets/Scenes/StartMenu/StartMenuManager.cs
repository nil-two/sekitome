using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    private const int MOVE_MENU_TYPE_UP     = 0;
    private const int MOVE_MENU_TYPE_DOWN   = 1;
    private const int MOVE_MENU_TYPE_ESCAPE = 2;

    public GameObject startMenu;
    public GameObject resultMenu;
    public GameObject quitMenu;

    private MenuBehavior[] menus;
    private int menuI;

    void Start()
    {
        menus = new MenuBehavior[] {
            startMenu.GetComponent<MenuBehavior>(),
            resultMenu.GetComponent<MenuBehavior>(),
            quitMenu.GetComponent<MenuBehavior>(),
        };
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
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            MoveMenu(MOVE_MENU_TYPE_ESCAPE);
        }       
        else if (Input.GetKeyDown(KeyCode.Return))
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
        if (menus[menuI].IsQuit())
        {
            QuitGame();
            return;
        }

        var dstSceneName = menus[menuI].GetDstSceneName();
        if (dstSceneName != null)
        {
            SceneManager.LoadScene(dstSceneName);
            return;
        }
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        #endif
    }
}
