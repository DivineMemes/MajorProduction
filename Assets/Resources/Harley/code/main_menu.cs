using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class main_menu : MonoBehaviour
{
    public void start()
    {
        SceneManager.LoadScene(1);
        gamemanger.GM.once = 0;
        gamemanger.GM.KeyCount = 0;
        gamemanger.GM.win = false;
        gamemanger.GM.thrownumber = 0;
        gamemanger.GM.throwme = false;
    }
    public void settings()
    {
        SceneManager.LoadScene(1);
        gamemanger.GM.once = 0;
        gamemanger.GM.thrownumber = 0;
    }
    public void mainmenu()
    {
        SceneManager.LoadScene(0);
    }
    public void exit()
    {
        Application.Quit();
    }
}
