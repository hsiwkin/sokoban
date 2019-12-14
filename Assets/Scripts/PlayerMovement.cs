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

        RotatePlayer();
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

    private void RotatePlayer()
    {
        // horizontal movement
        if (Mathf.Abs(transform.position.x - target.x) > 0)
        {
            float directionSign = -1 *
                Mathf.Sign(transform.position.x - target.x);

            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                directionSign * 90,
                transform.eulerAngles.z
            );
        }
        else // vertical movement
        {
            float directionSign = -1 *
                Mathf.Sign(transform.position.x - target.x);

            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                directionSign > 0 ? 0 : 180,
                transform.eulerAngles.z
            );
        }
    }
}
