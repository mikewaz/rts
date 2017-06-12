using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{

    const float MoveSpeed = 0.2f;
    const float ZoomSpeed = 0.5f;
    float _clickPose;

    // Use this for initialization
    void Start()
    {
        this._clickPose = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        KeybordControl();
        MouseControl();
    }

    void KeybordControl()
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

    void MouseControl()
    {
        #region Camera rotation
        if (Input.GetMouseButtonDown(2))
            this._clickPose = Input.mousePosition.x;
        if (Input.GetMouseButtonUp(2))
            this._clickPose = 0.0F;
        if (this._clickPose != 0.0F)
            TurnCamera();
        #endregion

        #region Camera zooming
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            ZoomCamera(false);
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            ZoomCamera(true);
        #endregion
    }

    void TurnCamera()
    {
        var clickDelta = Input.mousePosition.x - this._clickPose;
        var minClickDelta = Screen.width / 10;
        var turnSpeed = CalculateSpeedOfRotation(minClickDelta, clickDelta);

        if (clickDelta > minClickDelta)
            this.transform.RotateAround(this.transform.position, Vector3.up, turnSpeed * Time.deltaTime);
        if (clickDelta < -1 * minClickDelta)
            this.transform.RotateAround(this.transform.position, Vector3.up, -1 * turnSpeed * Time.deltaTime);
    }

    void MoveCamera(Vector2 side)
    {
        this.transform.position += new Vector3(side.x * MoveSpeed, 0, side.y * MoveSpeed);
    }

    void ZoomCamera(bool isDistancing)
    {
        if (isDistancing)
        {
            this.transform.position += new Vector3(0, ZoomSpeed, -1 * ZoomSpeed);
        }
        else
        {
            this.transform.position -= new Vector3(0, ZoomSpeed, -1 * ZoomSpeed);
        }
    }

    static float CalculateSpeedOfRotation(float step, float clickDelta)
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
