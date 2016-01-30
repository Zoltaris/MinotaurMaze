using UnityEngine;
using System.Collections;

public class ExitPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	public void SetLocation (MazeCell cell)
	{
		transform.localPosition = cell.transform.localPosition;
	}
}
