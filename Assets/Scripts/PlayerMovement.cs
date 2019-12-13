using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public delegate void MovementFinishEventHandler(object source, EventArgs args);
    public event MovementFinishEventHandler MovementFinish;

    private float speed = 1.0f;
    private bool performingAction = false;
    Vector3Int target;

    private Animator animator;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if (performingAction)
        {
            Move();
        }
    }

    public void StartMovement(Vector3Int target)
    {
        this.target = target;
        performingAction = true;
        animator.SetBool("walking", true);
    }

    public void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            performingAction = false;
            OnMovementFinish();
            animator.SetBool("walking", false);
        }
    }

    protected virtual void OnMovementFinish()
    {
        MovementFinish?.Invoke(this, EventArgs.Empty);
    }
}
