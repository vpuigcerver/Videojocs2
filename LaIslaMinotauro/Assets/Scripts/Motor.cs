using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

    public static Motor Instance;

    public float MoveSpeed = 10f;

    public Vector3 MoveVector { get; set; }
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

        MoveVector *= MoveSpeed;

        MoveVector *= Time.deltaTime;

        Controller.CharacterController.Move(MoveVector);

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
}
