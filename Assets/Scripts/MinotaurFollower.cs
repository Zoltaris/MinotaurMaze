﻿using UnityEngine;
using System.Collections;

public class MinotaurFollower : MonoBehaviour {


    public AudioClip _Dead;

	AudioSource audio;
	bool inputActive = true;

	private MazeCell currentCell;
    public Player _player;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		inputActive = true;
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
			
		}
		else
		{
			Move(MazeDirections.RandomValue);
		}
	   
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            audio.PlayOneShot(_Dead, 1f);
			inputActive = false;
        }
    }

    // Update is called once per frame
    void Update () {

		if(inputActive)
		{
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
}
