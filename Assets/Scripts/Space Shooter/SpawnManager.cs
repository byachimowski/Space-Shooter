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


    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

   
    IEnumerator SpawnRoutine()
    {
                       
        while (_enableSpawn)
        {
            
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 7f, 0);

            GameObject newEnemy=Instantiate(_enemyPrefab,posToSpawn ,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);

        }
                       
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(_enableSpawn)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 7f, 0);
           
            Instantiate(Powerups[Random.Range(0,3)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }


    }


    public void OnPlayerDeath()
    {
        _enableSpawn = false;

    }


}
