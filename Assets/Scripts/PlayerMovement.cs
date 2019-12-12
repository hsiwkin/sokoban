using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 10f;
    [SerializeField] float pushingSpeed = 8f;

    private Animator animator;
    private bool walking = false;

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
    }
}
