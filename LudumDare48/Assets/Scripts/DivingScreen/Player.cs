using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Vector2 currentAcceleration = new Vector2();

    private float maxSpeed = 200;
    private float accelSpeed = 200;

    private float cameraXMovementTotal = 0;
    private RectTransform _rectTransform;
    private Rect currentBounds;

    private void Awake()
    {
        WorldManager.Instance.player = this;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        Vector2 accel = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (accel.x < 0 && _rectTransform.localScale.x > 0)
            _rectTransform.localScale = new Vector3(_rectTransform.localScale.x * -1, _rectTransform.localScale.y, 1);
        else if (accel.x > 0 && _rectTransform.localScale.x < 0)
            _rectTransform.localScale = new Vector3(_rectTransform.localScale.x * -1, _rectTransform.localScale.y, 1);

        if (accel.x != 0)
        {
            //Accelerate in the direction the input is accelerating, then check to make sure you're not above the "acceleration" cap
            if (currentAcceleration.x > 0 && accel.x > 0 || currentAcceleration.x < 0 && accel.x < 0)
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

        if (currentBounds.height > 0)
        {
            if (transform.position.y < currentBounds.y - currentBounds.height * 0.5f)
            {
                WorldManager.Instance.GetNextZone(1);
            }
            else if (transform.position.y > currentBounds.y + currentBounds.height * 0.5f)
            {
                WorldManager.Instance.GetNextZone(-1);
            }
        }

        Camera.main.transform.SetPosition(y: transform.position.y);
        PotentiallyMoveCameraHorizontal();
    }

    private void PotentiallyMoveCameraHorizontal()
    {
        float targetx = (cameraXMovementTotal == 0 ? currentBounds.x : transform.position.x);
        targetx = Math.Min((cameraXMovementTotal * 0.5f) + currentBounds.x, targetx);
        targetx = Math.Max((-cameraXMovementTotal * 0.5f) + currentBounds.x, targetx);

        float xDiff = targetx - Camera.main.transform.position.x;

        if (Math.Abs(xDiff) < 10f)
            Camera.main.transform.SetPosition(x: targetx);
        else if (xDiff > 0)
            Camera.main.transform.SetPosition(x: Math.Min((cameraXMovementTotal * 0.5f) + currentBounds.x, Camera.main.transform.position.x + Time.deltaTime * 200));
        else if (xDiff < 0)
            Camera.main.transform.SetPosition(x: Math.Max((-cameraXMovementTotal * 0.5f) + currentBounds.x, Camera.main.transform.position.x - Time.deltaTime * 200));

        //if (currentBounds.width <= 1280)
        //    return;
        //else
        //{
        //    if (transform.position.x < Camera.main.transform.position.x && Camera.main.transform.position.x > -(cameraXMovementTotal * 0.5f) + currentBounds.x ||
        //       transform.position.x > Camera.main.transform.position.x && Camera.main.transform.position.x < +(cameraXMovementTotal * 0.5f) + currentBounds.x)
        //    {
        //        float xDiff = transform.position.x - Camera.main.transform.position.x;
        //        if (Math.Abs(xDiff) < 10f)
        //            Camera.main.transform.SetPosition(x: transform.position.x);
        //        else if (xDiff > 0)
        //            Camera.main.transform.SetPosition(x: Math.Min((cameraXMovementTotal * 0.5f) + currentBounds.x, Camera.main.transform.position.x + Time.deltaTime * 200));
        //        else if (xDiff < 0)
        //            Camera.main.transform.SetPosition(x: Math.Max((-cameraXMovementTotal * 0.5f) + currentBounds.x, Camera.main.transform.position.x - Time.deltaTime * 200));
        //    }
        //}
    }

    public void SetCurrentAreaRect(Rect area)
    {
        currentBounds = area;
        cameraXMovementTotal = Math.Max(0, area.width - 1280);
    }
}
