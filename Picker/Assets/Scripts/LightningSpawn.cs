using UnityEngine;

public class LightningSpawn : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    float RandX;
    Vector2 whereToSpawn;
    [SerializeField] private float spawnRate = 2f;
    float nextSpawn = 0.0f;

    private void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            RandX = Random.Range(-7f, 7f);
            whereToSpawn = new Vector2(RandX, transform.position.y);
            Instantiate(lightning, whereToSpawn, Quaternion.identity);
        }
    }
}
