using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float mSpeed = 12.0f;

    private CharacterController controller;
    private readonly float gravity = -9.81f;
    private readonly float mJumpHeight = 3.0f;

    private Vector3 mVelocity;

    public Transform mGroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool isGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(mGroundCheck.position, groundDistance, groundMask);
        if (isGrounded && mVelocity.y < 0.0f)
        {
            mVelocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * mSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            mVelocity.y = Mathf.Sqrt(mJumpHeight * -2.0f * gravity);
        }

        mVelocity.y += gravity * Time.deltaTime;
        controller.Move(mVelocity * Time.deltaTime);
    }

}
