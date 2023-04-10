using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject AboutProject;
    public GameManager GM;

    public void PlayBtn()
    {
        gameObject.SetActive(false);
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
        Debug.Log("�� ����� �� ����");
    }

}
