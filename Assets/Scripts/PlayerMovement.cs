using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController controller;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Jump")]
    [SerializeField] private float jumpHeight;

    [Header("Gravity")]
    [SerializeField] private float gravityScale = 2f;

    [Header("Rotation")]
    [SerializeField] private float rotateSpeed;

    [Header("Resetting")]
    [SerializeField] private float toStartDuration;

    private float gravity;
    private Vector3 startPosition;
    private bool isMoving;

    private void Awake()
    {
        startPosition = transform.position;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;

        Rotate();
        Move();
    }

    private void Rotate()
    {
        var horizontal = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up, rotateSpeed * horizontal * Time.deltaTime);
    }

    private void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var moveDirection = transform.forward * vertical + transform.right * horizontal;

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
            }
        }
        else
        {
            gravity += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        moveDelta.y = gravity;

        controller.Move(moveDelta);
    }

    public void ResetPosition()
    {
        isMoving = false;

        transform
               .DOMove(startPosition, toStartDuration)
               .SetEase(Ease.Flash)
               .OnComplete(() => isMoving = true);
    }
}
