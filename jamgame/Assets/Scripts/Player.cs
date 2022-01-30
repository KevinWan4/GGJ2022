using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(TopDownController))]
public class Player : MonoBehaviour
{

	[SerializeField] string newLevel;

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	bool doorKey = false;
	int doorLayer = 0;
	int keyLayer = 0;

	public Animator animator;
	public SpriteRenderer sprite;

	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller controller;
	TopDownController topDownController;

	public int layer;
	bool sideView = true;

	void Start() {
		layer = 1;
		controller = GetComponent<Controller>();
		topDownController = GetComponent<TopDownController>();
		controller.updateLayer(layer);

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	void Update() {
		//key
		if ((Vector3.Distance(transform.position, GameObject.Find("id_card_side").transform.position) < 1 && layer == keyLayer) || Vector3.Distance(transform.position, GameObject.Find("id_card_top").transform.position) < 1 ) {
			GameObject.Find("id_card_side").transform.position = new Vector3(1000, 1000, 0);
			GameObject.Find("id_card_top").transform.position = new Vector3(1000, 1000, 0);
			doorKey = true;
		}
		//door
		if (doorKey && ((Vector3.Distance(transform.position, GameObject.Find("door_side").transform.position) < 1 && layer == doorLayer) || Vector3.Distance(transform.position, GameObject.Find("door_top").transform.position) < 1 )) {
			topDownController.nextLevel(SceneManager.GetActiveScene().name);
			SceneManager.LoadScene(newLevel);
		}
		if (controller.collisions.below) {
			animator.SetBool("isJumping", false);
		}

		Camera.main.orthographicSize = 10 / Camera.main.aspect;
		// print(Camera.main.orthographicSize);
		//height of screen is size *2, width of screen is 10

		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			ShiftView(ref sideView);
		}
		
		if (sideView) {
			if (controller.collisions.above || controller.collisions.below) {
				velocity.y = 0;
			}
			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			GiveToAryan(input);


			if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below) {
				velocity.y = jumpVelocity;
				animator.SetBool("isJumping", true);
			}

			float targetVelocityX = input.x * moveSpeed;
			velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			velocity.y += gravity * Time.deltaTime;
			controller.Move(velocity * Time.deltaTime);
		}
		else {
			Vector2 input = new Vector2(0, 0);
			if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) input.y = -1;
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) input.y = 1;
			if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) input.x = -1;
			if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) input.x = 1;
			if (input.x != 0 || input.y != 0) topDownController.Move(input);
			input.x = 0;
			input.y = 0;
		}
	}
	void ShiftView(ref bool sideView) {
		if (sideView) {
			velocity = new Vector3(0, 0, 0);
			topDownController.ConvertPosition();
			sprite.sortingOrder = 5;
			Camera.main.transform.position = new Vector3(0, -20, -10);
		}
		else {
			layer = topDownController.ConvertBack();
			controller.updateLayer(layer);
			Camera.main.transform.position = new Vector3(0, 0, -10);
			sprite.sortingOrder = layer;
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
