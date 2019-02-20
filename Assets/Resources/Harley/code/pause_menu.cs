using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class pause_menu : MonoBehaviour {
    public GameObject me;
    public ThirdPerson look;
    public Slider xlook;
    public Slider ylook;
    // Use this for initialization
    void Start () {
        xlook.onValueChanged.AddListener(delegate { xlooknow(); });
        ylook.onValueChanged.AddListener(delegate { ylooknow(); });
    }

    // Update is called once per frame
    public void manu()
    {
        gamemanger.GM.pause = true;
        SceneManager.LoadScene("MainMenu");
    }
    public void game()
    {
        me.SetActive(false);
        gamemanger.GM.pause = false;
    }
    public void quit()
    {
        Application.Quit();
    }
    public void xlooknow()
    {
        PlayerPrefs.SetFloat("xlook", xlook.value);
        look.SensivityX = PlayerPrefs.GetFloat("xlook");
    }
    public void ylooknow()
    {
        PlayerPrefs.SetFloat("ylook", ylook.value);
        look.SensivityY= PlayerPrefs.GetFloat("ylook");
    }
}
