using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Wolf/Player/CharMovement")]
public class CharMovement : MonoBehaviour
{
    [Header("----------")]
    [Header("groundCheckは、足元にエンプティを配置")]
    [Header("----------")]
    public CharacterController characterController;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float maxFallSpeed = -30f;
    public float jumpHeight = 3f;

    [Space]
    public string Horizontal="Horizontal";
    public string Vertical="Vertical";
    public string Jump="Jump";

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis(Horizontal);
        float z = Input.GetAxis(Vertical);

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown(Jump) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, maxFallSpeed, 5000);
        characterController.Move(velocity * Time.deltaTime);

    }

    void OnDrawGizmosSelected()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();

            if(characterController == null)
            {
                characterController = gameObject.AddComponent<CharacterController>() as CharacterController;
            }
        }
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }

}
