using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab=null;
    [SerializeField] 
    private GameObject _enemyContainer=null;
   [SerializeField]
    private bool _enableSpawn = true;
    [SerializeField]
    private GameObject[]Powerups;
    private GameObject _newEnemy;
    private UIManager _uiManager;
    [SerializeField]
    private int PwrUp,PwrUpCtn =0;// used to limit Secondary Fire Powerup spawning
     
    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

   
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_enableSpawn)
        {
            
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 7f, 0);

            _newEnemy=Instantiate(_enemyPrefab,posToSpawn ,Quaternion.identity);
            _newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);

        }
                       
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(_enableSpawn)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 7f, 0);
            PwrUp = GetPowerUpID();
            Instantiate(Powerups[Random.Range(0,6)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }


    }
   
    private int GetPowerUpID()
    {
        int PUID = Random.Range(0, 6);
        if(PUID ==5) //Player_Missile_Powerup
        {
            PwrUpCtn++;
            if (PwrUpCtn >= 3) // limits the spawning of the Missile Power Up to once every 3 times it is selected
            {
                PwrUpCtn = 0;
                return PUID; // return Id 5 to spawn a missile
            }
            else
            {
                PUID = Random.Range(0, 5);// only power ups 1-5 can spawn Not the Missile_Powerup
                return PUID;
            }

            
        }
        return PUID;
    }

    public void OnPlayerDeath()
    {
        _enableSpawn = false;
       

    }


}
