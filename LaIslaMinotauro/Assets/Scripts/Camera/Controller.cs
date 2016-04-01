using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public static CharacterController CharacterController;
    public static Controller Instance;
	// Use this for initialization
	void Awake () {
        CharacterController = GetComponent("CharacterController") as CharacterController;
        Instance = this;
        CameraMovement.UseExistingOrCreateNewMainCamera();
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main == null)
            return;
        GetLocomotionInput();
        HandleActionInput();
        Motor.Instance.UpdateMotor();
	}

    void GetLocomotionInput()
    {
        var deadZone = 0.1f;
        Motor.Instance.VerticalVelocity = Motor.Instance.MoveVector.y;
        Motor.Instance.MoveVector = Vector3.zero;

        if(Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
            Motor.Instance.MoveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
            Motor.Instance.MoveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        CtmAnimation.Instance.DetermineCurrentMoveDirection();
    }

    void HandleActionInput()
    {
        if (Input.GetButton("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        Motor.Instance.Jump();
    }
}
