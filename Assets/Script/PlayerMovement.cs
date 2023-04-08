using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public Animator ac;
    public GameManager GM;
    CapsuleCollider selfCollider;
    Rigidbody rb;

    public float JumpSpeed = 12;

    int laneNumber = 1,
        lanesCount = 2;

    public float FirstLanePos,
                 LaneDistance,
                 SideSpeed;

    bool isRolling = false;

    Vector3 ccCenterNorm = new Vector3(0, 0.96f, 0),
            ccCenterRoll = new Vector3(0, .4f, .85f),
            ccCenterJump = new Vector3(0, 1.5f, 0);

    float ccHeightNorm = 2,
          ccHeightRoll = .4f,
          ccHeightJump = 1;

    bool wannaJump = false;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        ac = GetComponent<Animator>();
        selfCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, Physics.gravity.y * 4, 0), ForceMode.Acceleration);

        if (wannaJump && isGrounded())
        {
            ac.SetTrigger("jumping");

            selfCollider.center = ccCenterJump;
            selfCollider.height = ccHeightJump;

            rb.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
            wannaJump = false;
        }
        else if (rb.velocity.y < -8.9f)
        {
            StartCoroutine(DoFall());
        }
           
    }

    IEnumerator DoFall()
    {
        ac.SetTrigger("falling");
        yield return new WaitForSeconds(0.05f);
        selfCollider.center = ccCenterNorm;
        selfCollider.height = ccHeightNorm;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded())
        {
            ac.ResetTrigger("falling");

            if (GM.CanPlay)
            {
                if (!isRolling)
                {
                    if (Input.GetAxisRaw("Vertical") > 0)
                        wannaJump = true;
                    else if (Input.GetAxisRaw("Vertical") < 0)
                        StartCoroutine(DoRoll());
                }
            }
        }

        CheckInput();

        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(newPos.z, FirstLanePos + (laneNumber * LaneDistance), Time.deltaTime * SideSpeed);
        transform.position = newPos;
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.05f);
    }

    void CheckInput()
    {
        int sign = 0;

        if (!GM.CanPlay || isRolling)
            return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Push A or Left");
            sign = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Push D or Right");
            sign = 1;
        }
        else
            return;

        laneNumber += sign;
        laneNumber = Mathf.Clamp(laneNumber, 0, lanesCount);
    }

    IEnumerator DoRoll()
    {
        Debug.Log("rolling");
        isRolling = true;
        ac.SetBool("rolling", true);

        selfCollider.center = ccCenterRoll;
        selfCollider.height = ccHeightRoll;

        yield return new WaitForSeconds(1.5f);
        ac.SetBool("rolling", false);
        Debug.Log("not rolling");

        selfCollider.center = ccCenterNorm;
        selfCollider.height = ccHeightNorm;

        yield return new WaitForSeconds(0.3f);
        isRolling = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Trap") || !GM.CanPlay)
        {

            return;
        }
            

        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        GM.CanPlay = false;

        ac.SetBool("death", true);

        yield return new WaitForSeconds(3);

        GM.ShowResult();
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        laneNumber = 1;
    }
}
