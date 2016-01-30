using UnityEngine;
using System.Collections;

public class ExitPoint : MonoBehaviour {

	private MazeCell currentCell;

	// Use this for initialization
	void Start () {

	}

	public void SetLocation (MazeCell cell)
	{
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
	}
}
