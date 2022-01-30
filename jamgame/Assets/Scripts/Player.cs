using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(TopDownController))]
public class Player : MonoBehaviour
{

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public Animator animator;
	public SpriteRenderer sprite;

	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller controller;
	TopDownController topDownController;

	public float layer = 0;
	bool sideView = true;

	void Start()
	{
		controller = GetComponent<Controller>();
		topDownController = GetComponent<TopDownController>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	void Update()
	{
		if (controller.collisions.below)
		{
			animator.SetBool("isJumping", false);
		}

		Camera.main.orthographicSize = 10 / Camera.main.aspect;
		// print(Camera.main.orthographicSize);
		//height of screen is size *2, width of screen is 10

		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			ShiftView(ref sideView);
		}
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (sideView)
		{
			if (controller.collisions.above || controller.collisions.below)
			{
				velocity.y = 0;
			}
			GiveToAryan(input);


			if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
			{
				velocity.y = jumpVelocity;
				animator.SetBool("isJumping", true);
			}

			float targetVelocityX = input.x * moveSpeed;
			velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			velocity.y += gravity * Time.deltaTime;
			controller.Move(velocity * Time.deltaTime);
		}
		else
		{
			topDownController.Move(input);
		}
	}
	void ShiftView(ref bool sideView)
	{
		if (sideView)
		{
			velocity = new Vector3(0, 0, 0);
			topDownController.ConvertPosition();
			Camera.main.transform.position = new Vector3(0, -20, -10);
		}
		else
		{
			Camera.main.transform.position = new Vector3(0, 0, -10);
		}
		sideView = !sideView;

	}

	void GiveToAryan(Vector2 inputDirection)
	{
		if (controller.collisions.below)
		{
			animator.SetFloat("speed", Mathf.Abs(inputDirection.x));
		}
		if (inputDirection.x < 0)
		{
			sprite.flipX = true;
		}
		else if (inputDirection.x > 0)
		{
			sprite.flipX = false;
		}
	}
}
