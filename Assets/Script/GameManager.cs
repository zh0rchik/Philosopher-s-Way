using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ResultObj;
    public PlayerMovement PM;
    public RoadSpawner RS;

    public Text PointsTxt;
    float Points;

    public bool CanPlay = true;

    public float MoveSpeed;
    public float PointBaseValue, PointsMultiplier;

    public void StartGame()
    {
        PM.ac.SetBool("death", false);
        ResultObj.SetActive(false);
        RS.StartGame();
        CanPlay = true;
        PM.ac.SetTrigger("respawn");

        Points = 0;
    }

    private void Update()
    {
        if (CanPlay)
        {
            Points += Time.deltaTime * PointBaseValue * PointsMultiplier;

            PointsMultiplier += .05f * Time.deltaTime;
            PointsMultiplier = Mathf.Clamp(PointsMultiplier, 1, 10);

            MoveSpeed += .1f * Time.deltaTime;
            MoveSpeed = Mathf.Clamp(MoveSpeed, 1, 20);
        }

        PointsTxt.text = "Score: " + ((int)Points).ToString();
    }

    public void ShowResult()
    {
        ResultObj.SetActive(true);

    }
}
