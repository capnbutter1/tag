using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;


    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVel;


    public float jumpHeight = 5f;
    public float gravity = -9f;
    Vector3 velocity;


    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    bool isGrounded;



    void Update()
    {


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);



        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0, z).normalized;


        if (direction.magnitude >= 0.1f)
        {
            //movement smooth
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 MoveDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            controller.Move(MoveDir.normalized * speed * Time.deltaTime);


        }

        Jumping();
        Gravity();




        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }

    void Gravity()
    {
        if (isGrounded && velocity.y < 0)
        {

            velocity.y = -2f;

        }
    }


    void Jumping()
    {

        if (isGrounded && Input.GetButtonDown("Jump"))
        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }
    }



}

