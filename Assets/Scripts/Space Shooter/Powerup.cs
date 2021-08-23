using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    //PowerupID     0=Triple_Shot   1=Speed_Powerup   2=Shields   3=Ammo   4= Health 
    [SerializeField]
    private int _powerupID =0;
    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null) Debug.LogError("The _uiManager = null!!!");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();//created abreviated function label "player" 
           
            switch (_powerupID)
            {

                case 0: // Triple Shot Collected

                    player.TripleShotActive();
                    Destroy(this.gameObject);
                    break;

                case 1: // Speed Boost Collected

                   player.PowerBoostActive();
                    Destroy(this.gameObject);
                    break;

                case 2:// Shield Collected

                    player.ShieldsActive();
                    Destroy(this.gameObject);
                    break;

                case 3:// Ammo Collected

                    player.AmmoCollected();
                    Destroy(this.gameObject);
                    break;

                case 4:// Health Collected

                    player.HealthCollected();
                    Destroy(this.gameObject);
                    break;


                default: Debug.Log(" switch (_powerupID) exited as DEFAULT");
                    return;
            }
          
        }
     
    }
    
}
