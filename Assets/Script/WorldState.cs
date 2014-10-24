using UnityEngine;
using System.Collections;

public class WorldState:MonoBehaviour{
	public delegate void OnGameOver();
	public static OnGameOver gameOver;
	public enum GameState{
		PlayerAlive,
		PlayerDead
	}
	public static GameState state = GameState.PlayerAlive;

	public static GameObject requestPlayer(){
		var plr = GameObject.FindWithTag ("Player");
		if (plr != null) {
			return plr;
		}
		else{
			var mockPlr = new GameObject();
			mockPlr.transform.position = Vector2.zero;
			return mockPlr;
		}
	}
	public static bool hasPlayer(){
		return GameObject.FindWithTag ("Player") != null;
	}

	public void OnGUI(){
		if (state == GameState.PlayerDead) {
				if (GUI.Button (new Rect (Screen.width/2-150/2, Screen.height/2-150/2, 150, 150), "Restart!")) {
					Application.LoadLevel (Application.loadedLevelName);
					state = GameState.PlayerAlive;
			}
		}
	}



}
