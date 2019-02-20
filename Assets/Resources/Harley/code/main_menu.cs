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
        SceneManager.LoadScene("");
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("");
    }
    public void exit()
    {
        Application.Quit();
    }
}
