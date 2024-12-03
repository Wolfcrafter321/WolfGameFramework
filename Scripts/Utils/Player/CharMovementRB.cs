using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[AddComponentMenu("Wolf/Player/CharMovementRB")]
public class CharMovementRB : MonoBehaviour
{
    [Header("----------")]
    [Header("groundCheckは、足元にエンプティを配置")]
    [Header("----------")]
    public Rigidbody rb;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float speed = 12f;
    public float maxFallSpeed = -30f;
    public float jumpHeight = 3f;

    [Space]
    public string Horizontal = "Horizontal";
    public string Vertical = "Vertical";
    public string Jump = "Jump";

    Vector3 velocity;
    Vector3 dir;
    bool isGrounded;

    void Start()
    {
        if(rb == null)
            {
            rb = gameObject.AddComponent<Rigidbody>() as Rigidbody;
            SetRBDefault();
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //◆平面的に、wasdを取得
        float x = Input.GetAxis(Horizontal);
        float z = Input.GetAxis(Vertical);
        dir = (transform.forward * z + transform.right * x) * speed;

        //◆坂道を下るときの処理。
        if (isGrounded && velocity.y < 0)
        {
            dir.y = -5f;
        }

        //◆ジャンプ
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        //rb.AddForce( dir , ForceMode.VelocityChange);
        rb.linearVelocity = new Vector3(dir.x, rb.linearVelocity.y, dir.z);


    }

    //void FixedUpdate()
    //{
    //rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    //}

    // Update is called once per frame
    void OnDrawGizmos()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();

            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>() as Rigidbody;
                SetRBDefault();
            }
        }
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }

    [ContextMenu("SetRBDefault")]
    void SetRBDefault()
    {
        rb.linearDamping = 8;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }
    
}
