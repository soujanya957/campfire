using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{


    [SerializeField] private float spawnRate = 10.0f;

    [SerializeField] private GameObject[] bugPrefabs;
    [SerializeField] private bool canSpawn = true;
    private Vector2 screenBounds;

    private void Start() {
        StartCoroutine(Spawner());
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }


    private IEnumerator Spawner() {

        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn) {

            int rand = Random.Range(0, bugPrefabs.Length);
            GameObject bugsToSpawn = bugPrefabs[rand];

            Instantiate(bugsToSpawn, transform.position, Quaternion.identity);

            yield return wait;

        }

    }

    private void Update() {
        if (transform.position.x > 4.6 || transform.position.x < -5.5 || transform.position.y > 7.1 || transform.position.y < -4.7) {
            Destroy(this.gameObject);
        }
    }



}
