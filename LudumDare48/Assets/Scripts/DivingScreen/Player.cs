using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 currentAcceleration = new Vector2();

    private float maxSpeed = 200;
    private float accelSpeed = 200;

    private float cameraXMovementTotal = 0;

    private void FixedUpdate()
    {
        Vector2 accel = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (accel.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if(accel.x > 0)
            GetComponent<SpriteRenderer>().flipX = false;

        if (accel.x != 0)
        {
            //Accelerate in the direction the input is accelerating, then check to make sure you're not above the "acceleration" cap
            if(currentAcceleration.x > 0 && accel.x > 0 || currentAcceleration.x < 0 && accel.x < 0)
                currentAcceleration.x += accel.x * Time.deltaTime * accelSpeed;
            else
                currentAcceleration.x += accel.x * Time.deltaTime * accelSpeed * 1.25f;

            if (currentAcceleration.x > maxSpeed)
                currentAcceleration.x = maxSpeed;
            else if (currentAcceleration.x < -maxSpeed)
                currentAcceleration.x = -maxSpeed;
        }
        else if (currentAcceleration.x != 0)
        {
            //Accelerate towards 0
            if (currentAcceleration.x > 0)
                currentAcceleration.x -= (accelSpeed * Time.deltaTime * 0.75f);
            if (currentAcceleration.x < 0)
                currentAcceleration.x += (accelSpeed * Time.deltaTime * 0.75f);

            if (Math.Abs(currentAcceleration.x) < 0.05f)
                currentAcceleration.x = 0;
        }

        if (accel.y != 0)
        {
            //Accelerate in the direction the input is accelerating, then check to make sure you're not above the "acceleration" cap
            if (currentAcceleration.y > 0 && accel.y > 0 || currentAcceleration.y < 0 && accel.y < 0)
                currentAcceleration.y += accel.y * Time.deltaTime * accelSpeed;
            else
                currentAcceleration.y += accel.y * Time.deltaTime * accelSpeed * 1.25f;

            if (currentAcceleration.y > maxSpeed)
                currentAcceleration.y = maxSpeed;
            else if (currentAcceleration.y < -maxSpeed)
                currentAcceleration.y = -maxSpeed;
        }
        else
        {
            //Accelerate towards 0
            if (currentAcceleration.y > 0)
                currentAcceleration.y -= (accelSpeed * Time.deltaTime * 0.75f);
            if (currentAcceleration.y < 0)
                currentAcceleration.y += (accelSpeed * Time.deltaTime * 0.75f);

            if (Math.Abs(currentAcceleration.y) < 0.05f)
                currentAcceleration.y = 0;
        }

        GetComponent<Rigidbody2D>().MovePosition(transform.position + ((Vector3)currentAcceleration * Time.deltaTime));

        Camera.main.transform.SetPosition(y: transform.position.y);
    }

    private void PotentiallyMoveCameraHorizontal()
    {
        if (cameraXMovementTotal <= 50)
            return;
        else
        {
            if(transform.position.x < Camera.main.transform.position.x && Camera.main.transform.position.x > -(cameraXMovementTotal * 0.5f) ||
               transform.position.x > Camera.main.transform.position.x && Camera.main.transform.position.x < +(cameraXMovementTotal * 0.5f))
            {
                Camera.main.transform.SetPosition(x: transform.position.x);
            }
        }
    }

    public void SetCurrentAreaWidth(float width)
    {
        cameraXMovementTotal = Math.Max(0, width - 1280);
    }
}
