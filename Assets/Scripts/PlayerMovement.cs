using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 10f;
    [SerializeField] float pushingSpeed = 8f;

    private Animator animator;
    private bool isWalking = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        // TODO do i need it?
    }

    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        RotatePlayer(horizontalInput, verticalInput);

        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        {
            transform.Translate(
             walkingSpeed * Time.deltaTime * new Vector3(horizontalInput, 0, Mathf.Abs(verticalInput)));
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        SetAnimatorVariables();
    }

    private void RotatePlayer(float horizontalInput, float verticalInput)
    {
        float rotation = Mathf.Abs(horizontalInput) > 0 ? Mathf.Sign(horizontalInput) * 90 : 0;

        if (verticalInput < 0)
        {
            rotation = -180;
        }

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            rotation,
            transform.localEulerAngles.z
        );
    }

    private void SetAnimatorVariables()
    {
        animator.SetBool("walking", isWalking);
    }
}
