﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

    public Player playerPrefab;

    private Player playerInstance;

	private Maze mazeInstance;

	private void Start () {
		StartCoroutine(BeginGame());
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	private IEnumerator BeginGame () {
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));

	}

	private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
            {
            Destroy(playerInstance.gameObject);
        }
        StartCoroutine(BeginGame());
	}
}