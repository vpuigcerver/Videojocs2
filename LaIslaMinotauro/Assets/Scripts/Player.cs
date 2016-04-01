using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int health = 2;
    public float speed = 10.0f;
    public float gravity = 9.8f;
    public float maxVelocityChange = 10;
    public float jumpHeight = 2;
    private bool grounded;
    private bool dead;
    private Transform PlayerTransform;
    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //  PlayerTransform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = PlayerTransform.TransformDirection(targetVelocity);
        targetVelocity = targetVelocity * speed;

        Vector3 velocity = _rigidbody.velocity;
        Vector3 velocityChange = targetVelocity - velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        if (Input.GetButton("Jump")&& grounded)
        {
            _rigidbody.velocity = new Vector3(velocity.x, CalculateJump(), velocity.z);
        }
        _rigidbody.AddForce(new Vector3(0, -gravity * _rigidbody.mass, 0));
        grounded = false;
    }

    float CalculateJump()
    {
        float Jump = Mathf.Sqrt(2 * jumpHeight * gravity);
        return Jump;
    }

    void OnCollisionStay()
    {
        grounded = true;

    }
    void Update()
    {
        if(health< 1)
        {
            Application.LoadLevel("Test");
        }
    }
}
