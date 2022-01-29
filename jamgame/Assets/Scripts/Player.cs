using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller))]
[RequireComponent (typeof(TopDownController))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller controller;
	TopDownController topDownController;

	float layers = 0;
	bool sideView = true;

	void Start() {
		controller = GetComponent<Controller> ();
		topDownController = GetComponent<TopDownController>();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
			ShiftView(ref sideView);
		}
		if (sideView) {
			if (controller.collisions.above || controller.collisions.below) {
				velocity.y = 0;
			}

			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			GiveToAryan(input);
			

			if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below) {
				velocity.y = jumpVelocity;
			}

			float targetVelocityX = input.x * moveSpeed;
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
			velocity.y += gravity * Time.deltaTime;
			controller.Move (velocity * Time.deltaTime);
		} else {
			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			// TopDownController.Move(input);
		}
	}
	void ShiftView(ref bool sideView) {
		sideView = !sideView;
		
	}

	void GiveToAryan(Vector2 inputDirection) {
		if (inputDirection.x != 0 && controller.collisions.below) {
		}
	}
}