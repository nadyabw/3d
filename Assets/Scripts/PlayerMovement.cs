using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController controller;

    [Header("Movement")]
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;

    [Header("Jump")]
    [SerializeField] private float jumpHeight;

    [Header("Gravity")]
    [SerializeField] private float gravityScale = 2f;

    [Header("Rotation")]
    [SerializeField] private float rotateSpeed;

    [Header("Resetting")]
    [SerializeField] private float toStartDuration;

    [Header("Teleportation")]
    [SerializeField] private float teleportationTime = 0.75f;
    [SerializeField] private float teleportationDelay = 0.5f;

    private Transform cameraTransform;

    private float gravity;
    private Vector3 startPosition;
    private bool isMoving;

    private void Awake()
    {
        startPosition = transform.position;
        isMoving = true;
    }

    private void OnDisable()
    {
        animator.SetBool("IsRunning", false);
    }

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (Game.Instance.IsPaused || !isMoving) return;

        //Rotate();
        Move();
    }

    private void Rotate()
    {
        var horizontal = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up, rotateSpeed * horizontal * Time.deltaTime);
    }

    private void Move()
    {
        var forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();
        var right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var moveDirection = forward * vertical + right * horizontal;

        bool isRunning = IsRunning();
        if(isRunning)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        animator.SetBool("IsRunning", isRunning);

        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        var moveDelta = moveDirection * (moveSpeed * Time.deltaTime);

        if (controller.isGrounded)
        {
            gravity = -0.1f;

            if (Input.GetButtonDown("Jump"))
            {
                gravity = jumpHeight;
                AudioManager.Instance.PlaySFX(SFXType.Jump, transform);
            }
        }
        else
        {
            gravity += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        moveDelta.y = gravity;

        controller.Move(moveDelta);

        bool IsRunning()
        {
            return (Mathf.Abs(vertical) + Mathf.Abs(horizontal)) > 0;
        }
    }

    public void ResetPosition()
    {
        isMoving = false;

        transform
               .DOMove(startPosition, toStartDuration)
               .SetEase(Ease.Flash)
               .OnComplete(() => isMoving = true);
    }

    public void TeleportTo(Vector3 pos)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(teleportationDelay);
        sequence.Append(transform.DOMove(pos, teleportationTime).SetEase(Ease.Linear));
    }

}
