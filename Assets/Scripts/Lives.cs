using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lives : MonoBehaviour {

    public int _pLives = 7;
    public bool _respawn = false;
    public Player _player;

    public Text _livesText;

	// Use this for initialization
	void Start () {

        _livesText.text = " ";
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        _livesText.text = "Lives Remaining" + _pLives.ToString();

        if (_player.alive == false)
        {
            _pLives = _pLives - 1;
            _respawn = true;

        }
    }
}
