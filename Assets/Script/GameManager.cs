using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ResultObj;
    public MainMenuController MMC;
    public PlayerMovement PM;
    public RoadSpawner RS;

    public Text PointsTxt;
    float Points;

    public bool CanPlay = true;

    public float BaseMoveSpeed, CurrentMoveSpeed;
    public float PointBaseValue, PointsMultiplier;

    public void StartGame()
    {
        PM.ac.SetBool("death", false);
        ResultObj.SetActive(false);
        RS.StartGame();
        CanPlay = true;
        PM.ac.SetTrigger("respawn");

        CurrentMoveSpeed = BaseMoveSpeed;
        PointsMultiplier = 1;
        Points = 0;
    }

    public void MoveToMenu()
    {
        CanPlay = false;
        ResultObj.SetActive(false);

        MMC.OpenMenu();
    }

    private void Update()
    {
        if (CanPlay)
        {
            Points += Time.deltaTime * PointBaseValue * PointsMultiplier;

            PointsMultiplier += .05f * Time.deltaTime;
            PointsMultiplier = Mathf.Clamp(PointsMultiplier, 1, 10);

            CurrentMoveSpeed += .1f * Time.deltaTime;
            CurrentMoveSpeed = Mathf.Clamp(CurrentMoveSpeed, 1, 20);
        }

        PointsTxt.text = "Score: " + ((int)Points).ToString();
    }

    public void ShowResult()
    {
        ResultObj.SetActive(true);

    }
}
