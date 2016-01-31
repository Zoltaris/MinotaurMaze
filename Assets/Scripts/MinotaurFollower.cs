using UnityEngine;
using System.Collections;

public class MinotaurFollower : MonoBehaviour {

	public AudioClip _cantMove;
	public AudioClip _Footsteps;

	AudioSource audioSource;

	private MazeCell currentCell;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	public void SetLocation (MazeCell cell)
	{
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
	}

	private void Move (MazeDirection direction)
	{
		MazeCellEdge edge = currentCell.GetEdge(direction);
		if (edge is MazePassage)
		{
			SetLocation(edge.otherCell);
			GetComponent<AudioSource>().PlayOneShot(_Footsteps, 1f);
		}
		else
		{ 
			GetComponent<AudioSource>().PlayOneShot(_cantMove, 1f); 
		}    
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Move(MazeDirection.North);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Move(MazeDirection.East);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			Move(MazeDirection.South);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Move(MazeDirection.West);
		}
	}
}
