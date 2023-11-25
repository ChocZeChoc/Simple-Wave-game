using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private float timeBetweenWaves = 3.0f;

    [SerializeField] private float SpawnRateUp = -0.1f;
    [SerializeField] private int enemyUpRate = 3;

    [SerializeField] private int enemyCount;

    [SerializeField] GameObject enemy;

    public bool waveComplete = true;

    int waveCount = 0;

    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] GameObject player;
    private Vector3 playerPos;
    [SerializeField] private float protectedArea = 5f;

    [SerializeField] float xBound = 45f;
    [SerializeField] float zBound = 45f;

    float xPos;
    float zPos;

    private CameraFollow cameraFollow;

    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = GameObject.Find("CameraHolder").GetComponent<CameraFollow>();
         
    }

    // Update is called once per frame
    void Update()
    {
        bool playerRes = cameraFollow.playerRes;

        waveText.text = "Wave : " + waveCount.ToString();

        if (waveComplete && !playerRes && player != null)
        {
            StartCoroutine(waveSpawner());
        }

        if (player != null)
        {
            playerPos = player.transform.position;
        }
        
    }

    IEnumerator waveSpawner()
    {
        
        waveComplete = false;

       

        for (int i = 0; i < enemyCount; i++)
        {
            

            float xPosLow;
            float zPosLow;
            float xPosUp;
            float zPosUp;
            xPosLow = Random.Range(-xBound, playerPos.x - protectedArea);
            xPosUp = Random.Range(playerPos.x + protectedArea, xBound);
            zPosLow = Random.Range(-zBound, playerPos.z - protectedArea);
            zPosUp = Random.Range(playerPos.z + protectedArea, zBound);

            float n = Random.Range(0, 4);
            if (n == 0)
            {
                xPos = xPosLow;
                zPos = zPosLow;
            }
            else if (n == 1)
            {
                xPos = xPosUp;
                zPos = zPosUp;
            }
            else if (n == 2)
            {
                xPos = xPosLow;
                zPos = zPosUp;
            }
            else if (n == 3)
            {
                xPos = xPosUp;
                zPos = zPosLow;
            }

            GameObject enemyClone = Instantiate(enemy,new Vector3(xPos, 0f, zPos),Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
        }

        spawnRate += SpawnRateUp;
        enemyCount += enemyUpRate;
        
        yield return new WaitForSeconds(timeBetweenWaves);
        waveCount += 1;

        waveComplete = true;
    }
}
