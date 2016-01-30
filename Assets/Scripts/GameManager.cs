using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	public Maze mazePrefab;



    public Player playerPrefab;
	public MinotaurFollower minotaurPrefab;

    private Player playerInstance;
	private MinotaurFollower minotaurInstance;

    public int Lives;
    public bool alive = true;
    public bool gameOver = false;
    public bool restart = false;

    public Text _gameOver;
    public Text _restart;
    public Text _lives;

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
        SpawnPlayer();
        SpawnMinotaur();
        Lives = 1;
        alive = true;
        gameOver = false;
        restart = false;
        _restart.text = " ";
        _gameOver.text = " ";

		
	}


void FixedUpdate ()
    {
        _lives.text = "Lives Remaining: " + Lives.ToString();
        if (Lives == 0)
        {
            alive = false;
            gameOver = true;
        }

        if (gameOver)
        {
            GameOver();
            restart = true;
        }

        if (restart)
        {
            RestartGame();
        }
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
        StartCoroutine(BeginGame());
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
        //SpawnPlayer();
    }

    public void GameOver()
    {
    _gameOver.text = "The Minotaur has devoured you";
        _restart.text = "Press 'r' to Restart";

    }
}