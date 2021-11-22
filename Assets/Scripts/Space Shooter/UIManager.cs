using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Text _GameOver;
   
    [SerializeField]
    private Text _RestartGame;
    [SerializeField]
    private GameManager gameManager;
   


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = " Score: " + 0;
      
        _GameOver.gameObject.SetActive(false);
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(gameManager==null) Debug.LogError("Game_Manager::GameManager is NULL!!!");
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = " Score: " + playerScore.ToString();
    }

    public void UpdateAmmo(int playerAmmo)
    {
        _ammoText.text = "Ammo: " + playerAmmo.ToString();
    }

    public void UpdateLives(int currentLives)
    {

        
        _LivesImg.sprite = _liveSprites[currentLives];
        if (currentLives < 1)
        {
            GameOver();
        }
    }

     private void GameOver()
    {
        StartCoroutine(BlinkRoutine());
        gameManager.PlayerIsDead();
        Debug.Log("BlinkRoutine Called");
        _GameOver.gameObject.SetActive(true);
        _RestartGame.gameObject.SetActive(true);

     }


    IEnumerator BlinkRoutine()
    {
        while(true)
        {
                   
            _GameOver.gameObject.SetActive(true);
            _RestartGame.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
           
            _GameOver.gameObject.SetActive(false);
            //_RestartGame.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);

            
        }

    }
    
}
