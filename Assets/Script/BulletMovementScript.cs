using UnityEngine;
using System.Collections;

public class BulletMovementScript : MonoBehaviour {
	public static readonly int IS_ANGLED_TO_PLAYER = 0x1 << 0;

	public delegate Vector2 MoveDelegate(GameObject obj);
	public float speed;
	public MoveDelegate moveScript;
	public double timeAlive;
	public int flags;

	// Use this for initialization
	void Start () {
		if (moveScript == null) {
			moveScript = DefaultMove;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeAlive += Time.deltaTime;
		if (moveScript != null) {
						transform.position = moveScript (this.gameObject);
		}
		Rect bounds = new Rect (-6, -10, 12, 20);
		if (!bounds.Contains (transform.position)) {
			moveScript=null;
			Destroy(gameObject);
		}
		//Debug.Log (Vector3.Distance (WorldState.requestPlayer ().transform.position, this.transform.position));
		if (WorldState.hasPlayer() && Vector2.Distance (WorldState.requestPlayer().transform.position, this.transform.position) <0.15) {
			Debug.Log ("Kill!");
			WorldState.requestPlayer().GetComponent<PlayerInputScript>().kill();
		}

				
	}
	Vector2 DefaultMove(GameObject obj){
		Vector2 newPos;
		newPos = obj.transform.position;
		Vector2 delt = new Vector2 (Mathf.Cos(obj.transform.eulerAngles.z),Mathf.Sin(obj.transform.eulerAngles.z));
		delt.Scale(new Vector2(speed,speed));

		newPos += delt;
		return newPos;
	}

	public void facePlayer(){
		flags |= IS_ANGLED_TO_PLAYER;
		if (WorldState.hasPlayer ()) {
			GameObject plr=WorldState.requestPlayer ();
			Vector2 diff = plr.transform.position - this.transform.position;
			float angl = Mathf.Atan2(diff.y,diff.x);
			if(angl < 0){
				angl+=Mathf.PI*2;
			}
			Debug.DrawRay(transform.position,new Vector3(Mathf.Cos(angl),Mathf.Sin (angl),0));
			Debug.DrawRay(transform.position,diff);
			transform.rotation=Quaternion.Euler(new Vector3(0,0,angl));
		}
	}

	public bool hasFlag(int flag){
		return (flags & flag) > 0;
	}

}
