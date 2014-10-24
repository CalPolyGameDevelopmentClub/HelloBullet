using UnityEngine;
using System.Collections;

public class ExampleFiringScript : MonoBehaviour {
	public GameObject bullet;

	public float loopIntv;
	public float fireIntv;
	public float innerIntv;
	public float updateCount;
	float timer;
	float loopTimer;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		loopTimer += Time.deltaTime;
		if (loopTimer < fireIntv && timer > innerIntv){
			timer=0;
			updateCount++;
			for(int i = 0; i < 8; i++){
				var angle = (updateCount*3);
				var Bullet = Instantiate (bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, (float)angle + i*2*Mathf.PI/8))) as GameObject;
				var bScript = Bullet.GetComponent<BulletMovementScript> ();
				bScript.moveScript = expSpeed;
				var Bullet2 = Instantiate (bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, (float)angle + i*2*Mathf.PI/8))) as GameObject;
				var bScript2 = Bullet2.GetComponent<BulletMovementScript> ();
				bScript2.moveScript = expSpeed2;
			}
		}
		else if( loopTimer > loopIntv){
			loopTimer = 0;
		}

	}
	Vector2 expSpeed(GameObject obj){
		var bScript = obj.GetComponent<BulletMovementScript>();

		Vector2 newPos;
		newPos = obj.transform.position;
		Vector2 delt = new Vector2 (Mathf.Cos(obj.transform.eulerAngles.z),Mathf.Sin(obj.transform.eulerAngles.z));
		Debug.DrawRay(obj.transform.position,new Vector3(delt.x,delt.y,0));
		delt.Scale(new Vector2(bScript.speed,bScript.speed));
		if (!bScript.hasFlag (BulletMovementScript.IS_ANGLED_TO_PLAYER)) {
						bScript.speed = 0.01f + Mathf.Sin ((float)bScript.timeAlive) * 0.01f;
				} 
		else {
			bScript.speed = 0.05f;
		}

		newPos += delt;
		if (!bScript.hasFlag(BulletMovementScript.IS_ANGLED_TO_PLAYER) && bScript.timeAlive > Mathf.PI) {
			bScript.facePlayer();
		}

		return newPos;

	}
	Vector2 expSpeed2(GameObject obj){
		var bScript = obj.GetComponent<BulletMovementScript>();
		Vector2 newPos;
		newPos = obj.transform.position;
		Vector2 delt = new Vector2 (Mathf.Cos(obj.transform.eulerAngles.z),Mathf.Sin(obj.transform.eulerAngles.z));
		delt.Scale(new Vector2(bScript.speed,bScript.speed));
		bScript.speed = 0.02f + Mathf.Sin ((float)bScript.timeAlive*3) * 0.02f;
		newPos += delt;

		return newPos;
	
	}

				                                                                   
}
