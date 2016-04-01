using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public enum State
    {
        LOOK_FOR,
        GOTO,
        ATTACK,
    }
    public State CurState;
    public float Speed = 3.5f;
    public float GotoDistance = 13;
    public float AttackDistance = 4;
    public float AttackTimer = 2;
    public Transform target;
    public string PlayerTag = "Player";
    private float CurTime;
    private Player PlayerScript;
	// Use this for initialization
	IEnumerator Start () {
        //El Start
        target = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        CurTime = AttackTimer;
        if(target != null)
        {
            PlayerScript = target.GetComponent<Player>();
        }
	    while (true)
        {
            // El update
            switch (CurState)
            {
                case State.LOOK_FOR:
                    LookFor();
                    break;
                case State.GOTO:
                    GoTo();
                    break;
                case State.ATTACK:
                    Attack();
                    break;
            }
            yield return 0;
        }
	}
	
	// Update is called once per frame
	void LookFor()
    {
        if(Vector3.Distance(target.position, transform.position) < GotoDistance)
        {
            CurState = State.GOTO;
        }
    }
    void GoTo()
    {
        transform.LookAt(target);

        Vector3 fwd = transform.TransformDirection(Vector3.forward);//unitario
        RaycastHit Buddy;

        if(Physics.Raycast(transform.position, fwd,out Buddy))
        {
            if(Buddy.transform.tag != PlayerTag)
            {
                CurState = State.LOOK_FOR;
                return;
            }
        }
        

        if (Vector3.Distance(target.position, transform.position) > AttackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }
        else
        {
            CurState = State.ATTACK;
        }
    }
    void Attack()
    {
        transform.LookAt(target);
        CurTime = CurTime - Time.deltaTime;
        
        if(CurTime < 0)
        {
            PlayerScript.health--;
            CurTime = AttackTimer;
        }
        if (Vector3.Distance(target.position, transform.position) > AttackDistance)
        {
            CurState = State.GOTO;
        }
    }
}
