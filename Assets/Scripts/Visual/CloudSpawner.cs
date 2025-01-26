using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private float xSpawnLocations;
    [SerializeField] private float yMinSpawnLocation;
    [SerializeField] private float yMaxSpawnLocation;
    [SerializeField] private float zMinSpawnLocation;
    [SerializeField] private float zMaxSpawnLocation;
    [SerializeField] private Cloud cloudPrefab;
    [SerializeField] private List<Cloud> cloudList;
    [SerializeField] private int cloudAmount;
    [SerializeField] private Sprite[] cloudSprites;


    private Timer timer;

    private void Awake()
    { 
        timer = GetComponent<Timer>();

        timer.timerFinishedEvent.AddListener(OnTimerFinished);
        
    }

    private void OnDestroy()
    {
        timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
    }

    public void SpawnCloud()
    {
        if(cloudList.Count >= cloudAmount)
        {
            bool cloudCleared = false;
            List<Cloud> cloudListCopy = cloudList;
            foreach(Cloud listedCloud in cloudListCopy)
            {
                if(Vector2.Distance(Camera.main.transform.position, listedCloud.transform.position) > xSpawnLocations)
                {
                    cloudList.Remove(listedCloud);
                    Destroy(listedCloud.gameObject);
                    cloudCleared = true;
                    return;
                }
            }
            if (!cloudCleared)
                return;
        }

        

        int cloudDirection = 1;

        if (Random.value > 0.5)
        {
            cloudDirection *= -1;
        }

        Cloud cloud = Instantiate(cloudPrefab, new Vector3(Camera.main.transform.position.x + xSpawnLocations * cloudDirection, Random.Range(yMinSpawnLocation, yMaxSpawnLocation),
            Random.Range(zMinSpawnLocation, zMaxSpawnLocation)), Quaternion.identity, this.transform);
        cloud.SetDirection(-cloudDirection);
        cloud.MultiplySpeed(Random.Range(1, 3));
        cloud.SetSprite(cloudSprites[Random.Range(0, cloudSprites.Length-1)]);
        cloudList.Add(cloud);
    }

    void OnTimerFinished()
    {
        SpawnCloud();

        timer.SetDuration(Random.Range(4, 40));
        timer.StartTimer();
    }
}
