using System;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private const float MoveSpeed = 0.2f;
    private const float ZoomSpeed = 0.5f;
    private float ClickPose = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        KeybordControl();
        MouseControl();
    }

    private void KeybordControl()
    {
        if (Input.GetKey(KeyCode.W))
            MoveCamera(Vector2.up);
        if (Input.GetKey(KeyCode.A))
            MoveCamera(Vector2.left);
        if (Input.GetKey(KeyCode.S))
            MoveCamera(Vector2.down);
        if (Input.GetKey(KeyCode.D))
            MoveCamera(Vector2.right);
    }

    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(2))
            this.ClickPose = Input.mousePosition.x;
        if (Input.GetMouseButtonUp(2))
            this.ClickPose = 0;
        if (this.ClickPose != 0)
            TurnCamera();
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            ZoomCamera(false);
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            ZoomCamera(true);

    }

    private void TurnCamera()
    {
        var clickDelta = Input.mousePosition.x - this.ClickPose;
        var minClickDelta = Screen.width / 10;
        var TurnSpeed = CalculateSpeedOfRotation(minClickDelta, clickDelta);

        if (clickDelta > minClickDelta)
            this.transform.RotateAround(this.transform.position, Vector3.up, TurnSpeed * Time.deltaTime);
        if (clickDelta < -1 * minClickDelta)
            this.transform.RotateAround(this.transform.position, Vector3.up, -1 * TurnSpeed * Time.deltaTime);
    }

    private void MoveCamera(Vector2 side)
    {
        this.transform.position += new Vector3(side.x * MoveSpeed, 0, side.y * MoveSpeed);
    }

    private void ZoomCamera(bool isDistancing)
    {
        var currentPos = this.transform.position;
        if (isDistancing)
        {
            this.transform.position += new Vector3(0, ZoomSpeed, -1 * ZoomSpeed);
        }
        else
        {
            this.transform.position -= new Vector3(0, ZoomSpeed, -1 * ZoomSpeed);
        }
    }

    private float CalculateSpeedOfRotation(float step, float clickDelta)
    {
        var speedOfRotation = 0f;
        var currentStep = 0f;
        while (currentStep <= Math.Abs(clickDelta))
        {
            currentStep += step;
            speedOfRotation += 10f;
        }
        return speedOfRotation;
    }
}
