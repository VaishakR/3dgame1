using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
	public Vector3 velocity;

	[SerializeField] private float speed = 2.0f;
	[SerializeField] private float jumpForce = 1.0f;
	[SerializeField] private float rotateYSpeed;
	[SerializeField] private float rotateXSpeed;
	[SerializeField] private float drag;

	[SerializeField] private Camera camera;
	[SerializeField] private Transform orientation;

	[SerializeField] private LayerMask groundLayer;

	private bool isMoving = false;
	private bool grounded;

	float xRotation; float yRotation;
	float horizontalInput; float verticalInput;
	Vector3 moveDirection;

	private Rigidbody rb;
	public float playerHeight;
	

    void Start()
    {
		rb = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
		horizontalInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
		verticalInput = Input.GetAxisRaw("Vertical") * Time.deltaTime;

		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

		if (horizontalInput != 0 || verticalInput != 0) 
			isMoving = true;

		if (grounded) {
			rb.drag = drag;
			Debug.Log("grounded");
		}
		else {
			rb.drag = 0;
		}

		handleRotation();	
    }

	void FixedUpdate()
	{
		moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

		if (isMoving)
		{
			rb.AddForce(moveDirection * speed * 10f, ForceMode.Force);
		}

		if (Input.GetKeyDown(KeyCode.Space) && grounded)
			rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

	}

	void handleRotation()
	{
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * rotateXSpeed;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * rotateYSpeed;

		yRotation += mouseX;
		xRotation -= mouseY;

		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		orientation.rotation = Quaternion.Euler(0, yRotation, 0);
		camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
	}
}
