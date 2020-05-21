using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Camera cam;

    public System.Action OnJump;
    public System.Action OnDodge;
    public System.Action OnLand;
    public System.Action OnAttack;
    public System.Action OnHeavyAttack;
    public System.Action OnGetHit;
    public System.Action<Targetable> OnCastFire;
    public System.Action<Targetable> OnRetarget;

    private bool isAttacking;

    public bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }

    [SerializeField]
    private float maxHp;

    private float hP;

    public float HP { get { return hP; } set { hP = value; } }

    private bool isInvincible = false;

    [SerializeField]
    private float maxMp;

    private float mP;

    public float MP { get { return mP; } set { mP = value; } }

    [SerializeField]
    private float moveSpeed = 7f;

    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    private float accelerationValue = 500f;

    private float speedMultiplier = 1.0f;

    public float SpeedMultiplier { get { return speedMultiplier; } set { speedMultiplier = value; } }

    [SerializeField]
    float jumpHeight = 10f;

    [SerializeField]
    float gravity = 9.8f;

    [SerializeField]
    List<Targetable> targets = new List<Targetable>();

    private int currentTargetIndex = 0;

    private Targetable currentTarget = null;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cam = FindObjectOfType<Camera>();

        hP = maxHp;
        mP = maxMp;

        foreach (Targetable target in FindObjectsOfType<Targetable>())
        {
            targets.Add(target);
        }
    }
    
    private void Update()
    {
        // Run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMultiplier = 2.0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 1.0f;
        }

        if (characterController.isGrounded)
        {
            Vector3 forward = cam.transform.forward.normalized;
            forward.y = 0f;
            Vector3 right = cam.transform.right.normalized;
            right.y = 0f;

            moveDirection = Vector3.zero;

            // Acceleration
            float acceleration = accelerationValue * Time.deltaTime;
            if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0)
            {
                acceleration = -acceleration;
            }

            if (Input.GetButtonDown("Jump"))
            {
                OnJump?.Invoke();
                moveDirection.y = jumpHeight;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                OnDodge?.Invoke();
                speedMultiplier = 1.0f;
            }

            if (!isAttacking)
            {
                moveDirection = (forward * Input.GetAxis("Vertical")) + (right * Input.GetAxis("Horizontal"));
                moveDirection *= moveSpeed * speedMultiplier + acceleration;
            }

            if (Input.GetButtonDown("Attack"))
            {
                OnAttack?.Invoke();
            }

            if (Input.GetButtonDown("HeavyAttack"))
            {
                OnHeavyAttack?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (currentTarget != null)
                {
                    OnCastFire?.Invoke(targets[currentTargetIndex]);
                }
                else
                {
                    // TODO:: Implement a No Target Message
                }
            }

            // Target
            if (Input.GetButtonDown("Target"))
            {
                int stopIndex = currentTargetIndex == 0 ? targets.Count - 1 : currentTargetIndex - 1;
                Targetable stopTargeting = targets[stopIndex];
                int newTarget = currentTargetIndex;

                while ((targets[newTarget] != stopTargeting) || (targets.Count == 1))
                {
                    if (Vector3.Distance(transform.position, targets[(newTarget + 1) % targets.Count].transform.position) < 15.0f)
                    {
                        newTarget = (newTarget + 1) % targets.Count;
                        currentTargetIndex = newTarget;
                        currentTarget = targets[currentTargetIndex];
                        OnRetarget?.Invoke(currentTarget);
                        break;
                    }
                    newTarget = (newTarget + 1) % targets.Count;

                    // If only one target, break out after setting
                    if (targets.Count == 1)
                    {
                        break;
                    }
                }
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerStay(Collider collider)
    {
        Traps trap = collider.GetComponentInParent<Traps>();

        if (trap != null)
        {
            if (!isInvincible)
            {
                hP -= 10.0f;
                OnGetHit?.Invoke();
                isInvincible = true;
                StartCoroutine(InvincibilityFrames());
            }
        }
    }

    IEnumerator InvincibilityFrames()
    {
        yield return new WaitForSeconds(2);
        isInvincible = false;
    }
}
