using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

    public static Motor Instance;

    public float ForwardSpeed = 10f;
    public float BackwardSpeed = 2f;
    public float StrafingSpeed = 5f;
    public float JumpSpeed = 8f;
    public float Gravity = 10f;
    public float TerminaVelocity = 20f;


    public Vector3 MoveVector { get; set; }
	public float VerticalVelocity { get; set; }

    // Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	public void UpdateMotor () {
        ProcessMotion();
        SnapAlignCharacterWithCamera();
	}

    void ProcessMotion()
    {
        MoveVector = transform.TransformDirection(MoveVector);

        if (MoveVector.magnitude > 10)
            MoveVector = Vector3.Normalize(MoveVector);

        MoveVector *= MoveSpeed();

        //Reaplicar Velocidad Vertical MoveVector y
        MoveVector = new Vector3(MoveVector.x, VerticalVelocity, MoveVector.z);

        //Aplicar Gravedad
        ApplyGravity();

        Controller.CharacterController.Move(MoveVector * Time.deltaTime);

    }

    void ApplyGravity()
    {
        if (MoveVector.y > -TerminaVelocity)
            MoveVector = new Vector3(MoveVector.x, MoveVector.y - Gravity * Time.deltaTime, MoveVector.z);

        if(Controller.CharacterController.isGrounded && MoveVector.y < -1)
            MoveVector = new Vector3(MoveVector.x, -1, MoveVector.z);

    }

    public void Jump()
    {
        if (Controller.CharacterController.isGrounded)
            VerticalVelocity = JumpSpeed;
    }

    void SnapAlignCharacterWithCamera()
    {
        if(MoveVector.x != 0 || MoveVector.z != 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                Camera.main.transform.eulerAngles.y,
                transform.eulerAngles.z);
        }

    }

    float MoveSpeed()
    {
        var moveSpeed = 0f;

        switch(CtmAnimation.Instance.MoverDirection)
        {
            case CtmAnimation.Direction.Stationary:
                moveSpeed = 0;
                break;
            case CtmAnimation.Direction.Forward:
                moveSpeed = ForwardSpeed;
                break;
            case CtmAnimation.Direction.Backward:
                moveSpeed = BackwardSpeed;
                break;
            case CtmAnimation.Direction.Left:
                moveSpeed = StrafingSpeed;
                break;
            case CtmAnimation.Direction.Rigth:
                moveSpeed = StrafingSpeed;
                break;
            case CtmAnimation.Direction.RigthForward:
                moveSpeed = ForwardSpeed;
                break;
            case CtmAnimation.Direction.LeftForward:
                moveSpeed = ForwardSpeed;
                break;
            case CtmAnimation.Direction.RigthBackward:
                moveSpeed = BackwardSpeed;
                break;
            case CtmAnimation.Direction.LeftBackward:
                moveSpeed = BackwardSpeed;
                break;

        }

        return moveSpeed;
    }
}
