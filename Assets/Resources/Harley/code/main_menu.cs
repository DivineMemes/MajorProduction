using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class main_menu : MonoBehaviour
{
    public void start()
    {
        SceneManager.LoadScene("");
    }
    public void settings()
    {
        SceneManager.LoadScene(1);
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
