using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceSetup : MonoBehaviour {
	
	public Transform playerPosition;
	public Transform opponentPosition;
	
	public RuntimeAnimatorController playerDance;
	public RuntimeAnimatorController opponentDance;

	GameObject playerPrefab;
	GameObject opponentPrefab;

	private enum PLAYER
	{
		PLAYER_0 = 0,
		PLAYER_1 = 1,
		PLAYER_2 = 2,
		PLAYER_3 = 3,
		PLAYER_4 = 4,
		PLAYER_5 = 5,
		PLAYER_6 = 6,
		PLAYER_7 = 7
	}

	void Awake(){
		PLAYER character = (PLAYER)PlayerPrefs.GetInt("Player");
		switch (character)
		{
			case PLAYER.PLAYER_0:
				playerPrefab = Resources.Load<GameObject>("Character prefabs/Player_0");
				break;
			case PLAYER.PLAYER_1:
				playerPrefab = Resources.Load<GameObject>("Character prefabs/Player_1");
				break;
			case PLAYER.PLAYER_2:
				playerPrefab = Resources.Load<GameObject>("Character prefabs/Player_2");
				break;
			case PLAYER.PLAYER_3:
				playerPrefab = Resources.Load<GameObject>("Character prefabs/Player_3");
				break;
			case PLAYER.PLAYER_4:
				playerPrefab = Resources.Load<GameObject>("Character prefabs/Player_4");
				break;
			case PLAYER.PLAYER_5:
				playerPrefab = Resources.Load<GameObject>("Character prefabs/Player_5");
				break;
			case PLAYER.PLAYER_6:
				playerPrefab = Resources.Load<GameObject>("Character prefabs/Player_6");
				break;
			case PLAYER.PLAYER_7:
				playerPrefab = Resources.Load<GameObject>("Character prefabs/Player_7");
				break;
		}
		opponentPrefab = Resources.Load<GameObject>("Character prefabs/Opponent base prefab");
		
		if(playerPrefab == null || opponentPrefab == null){
			Debug.LogWarning("No player/opponent prefab in resources");
			
			return;
		}
		
		GameObject newOpponent = Instantiate(opponentPrefab, opponentPosition.position, opponentPosition.rotation);
		newOpponent.GetComponent<Opponent>().enabled = false;
			
		GameObject newPlayer = Instantiate(playerPrefab, playerPosition.position, playerPosition.rotation);
		newPlayer.GetComponent<Player>().enabled = false;
		
		newOpponent.GetComponent<Animator>().runtimeAnimatorController = opponentDance;
		newPlayer.GetComponent<Animator>().runtimeAnimatorController = playerDance;
		
		DanceScene danceScene = FindObjectOfType<DanceScene>();
		danceScene.player = newPlayer.GetComponent<Animator>();
		danceScene.opponent = newOpponent.GetComponent<Animator>();
		
		newPlayer.GetComponentInChildren<ParticleSystem>().Stop();
		newOpponent.GetComponentInChildren<ParticleSystem>().Stop();
	}
}
