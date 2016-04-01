using UnityEngine;
using System.Collections;

public class CtmAnimation : MonoBehaviour {

    public enum Direction
    {
        Stationary, Forward, Backward, Left, Rigth,
        LeftForward, RigthForward, LeftBackward, RigthBackward
    }

    public static CtmAnimation Instance;
    public Direction MoverDirection { get; set; }
	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void DetermineCurrentMoveDirection()
    {
        var forward = false;
        var backward = false;
        var left = false;
        var rigth = false;

        if (Motor.Instance.MoveVector.z > 0)
            forward = true;
        if (Motor.Instance.MoveVector.z < 0)
            backward = true;
        if (Motor.Instance.MoveVector.x > 0)
            rigth = true;
        if (Motor.Instance.MoveVector.x < 0)
            left = true;

        if (forward)
        {
            if (left)
                MoverDirection = Direction.LeftForward;
            else if (rigth)
                MoverDirection = Direction.RigthForward;
            else
                MoverDirection = Direction.Forward;
        }
        else if (backward)
        {
            if (left)
                MoverDirection = Direction.LeftBackward;
            else if (rigth)
                MoverDirection = Direction.RigthBackward;
            else
                MoverDirection = Direction.Backward;
        }
        else if (left)
            MoverDirection = Direction.Left;
        else if (rigth)
            MoverDirection = Direction.Rigth;
        else
            MoverDirection = Direction.Stationary;
    }
}
