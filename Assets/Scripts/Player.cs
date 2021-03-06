﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

 

    public AudioClip[] _cantMove;
    public AudioClip _Footsteps;
    public AudioClip _Dead;
    public AudioClip _Escape;
	public AudioClip _Reset;

    public GameManager _GM;

    public bool inputActive = true;

    AudioSource audio;

    private MazeCell currentCell;

    void Start ()
    {
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
            audio.Stop();
            audio.PlayOneShot(_Footsteps, 0.5f);
        }
        
        else
        {
            audio.Stop();
			audio.PlayOneShot(_cantMove[Random.Range(0, _cantMove.Length)], 1f);
        }    
        }

    // Update is called once per frame
    void Update()
    {
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
    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Minotaur")
        {
            audio.PlayOneShot(_Dead, 1f);
			inputActive = false;
			StartCoroutine(Exit());
		} 
		if (col.gameObject.tag == "End") {
            audio.PlayOneShot(_Escape, 1f);
			inputActive = false;
			StartCoroutine(Exit());
		}
    }

	IEnumerator Exit() {
		Debug.Log(Time.time);
		yield return new WaitForSeconds(3);
		audio.PlayOneShot(_Reset, 1f);
	}
}
