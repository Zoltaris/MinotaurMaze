using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	public AudioClip _Introduction;
	public AudioClip _NorthWest;
	public AudioClip _NorthEast;
	public AudioClip _SouthEast;
	public AudioClip _SouthWest;

    public Player playerPrefab;
	public MinotaurFollower minotaurPrefab;
	public ExitPoint exitPrefab;

    private Player playerInstance;
	private MinotaurFollower minotaurInstance;
	private ExitPoint exitInstance;

    public int Lives;
    public bool alive = true;
	public bool winState = false;
    public bool gameOver = false;
    public bool restart = false;

    public Text _gameOver;
    public Text _restart;
    public Text _lives;

	private Maze mazeInstance;

	AudioSource audio;

	private void Start () {
		audio = GetComponent<AudioSource>();
		StartCoroutine(BeginGame());
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }

		if (Input.GetKeyDown(KeyCode.Return))
		{
			CheckLocation();
		}

        /*    _lives.text = "Lives Remaining: " + Lives.ToString();

            if (Lives == 0)
            {
                alive = false;
                gameOver = true;
            }

            if (gameOver)
            {
                GameOver();
                //restart = true;
            }

            if (restart)
            {
                RestartGame();
            }

            if (winState)
            {

            }

        Debug.Log(_lives);*/
        
	}

	private IEnumerator BeginGame () {
		audio.PlayOneShot(_Introduction, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
        SpawnPlayer();
        SpawnMinotaur();
		exitInstance = Instantiate(exitPrefab) as ExitPoint;
		exitInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        Lives = 1;
        alive = true;
		winState = false;
        gameOver = false;
        restart = false;

		
	}
    
	private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
		{
            Destroy(playerInstance.gameObject);
        }
		if (minotaurInstance != null)
		{
			Destroy(minotaurInstance.gameObject);
		}
		if (exitInstance != null)
		{
			Destroy(exitInstance.gameObject);
		}
        StartCoroutine(BeginGame());
	}

	private void CheckLocation() {
		Debug.Log("Checking direction");		
		// var distance = Vector3.Distance(playerInstance.gameObject.transform.position, exitInstance.gameObject.transform.position);
		Vector3 direction = playerInstance.gameObject.transform.position - exitInstance.gameObject.transform.position;

		if (direction.x < 0 && direction.z < 0) {
			Debug.Log("To the North East!");
			audio.PlayOneShot(_NorthEast, 1f);
		} else if (direction.x < 0 && direction.z > 0) {
			Debug.Log("To the South East!");
			audio.PlayOneShot(_SouthEast, 1f);
		} else if (direction.x > 0 && direction.z < 0) {
			Debug.Log("To the North West!");
			audio.PlayOneShot(_NorthWest, 1f);		
		} else if (direction.x > 0 && direction.z > 0) {
			Debug.Log("To the South West!");
			audio.PlayOneShot(_SouthWest, 1f);
		}
	}

    void SpawnPlayer ()
    {
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
    }

    void SpawnMinotaur()
    {
        minotaurInstance = Instantiate(minotaurPrefab) as MinotaurFollower;
        minotaurInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
    }

    public void LivesDecrease()
    {
        Lives = Lives - 1;
       // _lives.text = "Lives Remaining: " + Lives.ToString();
        if (Lives == 0)
        {
            alive = false;
            gameOver = true;
        }
        //SpawnPlayer();
        Debug.Log(Lives);
        Debug.Log(_lives);
    }

    /*public void GameOver()
    {
		if (winState) {
			_gameOver.text = "Woohoo! You found the exit!";
		} else {
    		_gameOver.text = "The Minotaur has devoured you";
		}
        _restart.text = "Press 'r' to Restart";
        Debug.Log(winState);*/
    }
