using UnityEngine;
using System.Collections;

public class PlayerInputScript : MonoBehaviour {
	public float playerSpeed;
	Vector2 movement;
	bool alive;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		handleInput();
		transform.Translate (movement);
	}
	void handleInput(){
		movement = Vector2.zero;
		if (Input.GetKey (KeyCode.W)) {
			movement+=Vector2.up;
		}
		if (Input.GetKey (KeyCode.S)) {
			movement-=Vector2.up;
		}
		if (Input.GetKey (KeyCode.A)) {
			movement-=Vector2.right;
		}
		if (Input.GetKey (KeyCode.D)) {
			movement+=Vector2.right;
		}
		movement.Normalize ();
		if(Input.GetKey(KeyCode.LeftShift)){
			movement.Scale(new Vector2(0.5f,0.5f));
		}
		movement.Scale (new Vector2(playerSpeed, playerSpeed));
	}
	public void kill(){
		WorldState.gameOver = delegate() {
			WorldState.state = WorldState.GameState.PlayerDead;
		};
		WorldState.gameOver();
		Destroy(this.gameObject);
		alive = false;
	}

}
