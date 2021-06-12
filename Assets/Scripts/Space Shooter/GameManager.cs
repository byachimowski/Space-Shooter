using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _playerIsDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _playerIsDead == true)
        {
            SceneManager.LoadScene(0); // Load "Main Menu" Scene
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); //allows us to quit the application while in Full screen Mode
        }

    }

    public void PlayerIsDead()
    {
        _playerIsDead = true;
    }

}
