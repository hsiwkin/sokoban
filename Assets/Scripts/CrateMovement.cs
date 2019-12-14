using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateMovement : MonoBehaviour
{
    public delegate void MovementFinishEventHandler(object source, EventArgs args);
    public event MovementFinishEventHandler MovementFinish;

    private float speed = 1.0f;
    private bool performingAction = false;
    private Vector3 target;

    void Update()
    {
        if (performingAction)
        {
            Move();
        }
    }

    public void StartMovement(Vector2Int target)
    {
        this.target = new Vector3(
            target[1],
            transform.position.y,
            target[0]
        );

        performingAction = true;
    }

    public void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            performingAction = false;
            OnMovementFinish();
        }
    }

    protected virtual void OnMovementFinish()
    {
        MovementFinish?.Invoke(this, EventArgs.Empty);
    }

}
