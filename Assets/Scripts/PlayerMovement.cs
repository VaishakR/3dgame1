using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Vector3 velocity;
	private bool grounded;

	[SerializeField]
	private float speed = 2.0f;
	[SerializeField]
	private float jumpForce = 1.0f;
	private Rigidbody rb;

    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
		rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);
		if (Input.GetKeyDown(KeyCode.Space))
			rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }
}
