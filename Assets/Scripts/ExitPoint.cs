using UnityEngine;
using System.Collections;

public class ExitPoint : MonoBehaviour {

	public GameManager _GM;

	// Use this for initialization
	void Start () {

	}

	public void SetLocation (MazeCell cell)
	{
		transform.localPosition = cell.transform.localPosition;
	}

	void OnTriggerEnter (Collider other) 
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Winner!!");
			_GM.winState = true;
			_GM.gameOver = true;
		}
	}
}
