using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

 

    public AudioClip _cantMove;
    public AudioClip _Footsteps;

    public Lives _lives;

    public bool alive = true;

    AudioSource audio;

    private MazeCell currentCell;

    void Start ()
    {
        audio = GetComponent<AudioSource>();
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
            audio.PlayOneShot(_Footsteps, 1f);
        }
        
        else
        {
            audio.Stop();
            audio.PlayOneShot(_cantMove, 1f);
        }    
        }

    // Update is called once per frame
    void Update()
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
    void FixedUpdate()
    {
        if (_lives._respawn = true)
        {

        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "MinotaurFollower")
        {
            alive = false;
            Destroy(gameObject);
        } 

    }
}
