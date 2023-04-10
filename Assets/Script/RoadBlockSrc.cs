using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlockSrc : MonoBehaviour
{
    GameManager GM;
    Vector3 moveVec;

    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        moveVec = new Vector3(-1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.CanPlay)
            transform.Translate(moveVec * Time.deltaTime * GM.CurrentMoveSpeed);
    }
}
