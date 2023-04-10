using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject AboutProject;
    public GameObject MemoObject;
    public GameManager GM;

    public void PlayBtn()
    {
        gameObject.SetActive(false);
        MemoObject.SetActive(true);
        //GM.StartGame();
    }

    public void ClsMemoBtn()
    {
        MemoObject.SetActive(false);
        GM.StartGame();
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void AboutProjectButton()
    {
        AboutProject.SetActive(true);
    }

    public void QuitBtn()
    {
        Application.Quit();
        Debug.Log("Вы вышли из игры");
    }

}
