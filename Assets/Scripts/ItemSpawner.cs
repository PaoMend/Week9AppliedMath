using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Item itemToSpawn;
    public Transform parent;
    public float spawnRate = 1.5f;
    public float spawnZ = 60f;
    public float spawnY = -2f;
    public float laneOffset = 2f;
    public float speed = 20f;
    public float despawnZ = -5f;
    public float collisionZ = 2f;
    public float collisionRange = 1.5f;

    public List<Item> items = new List<Item>();

    private PlayerMovement player;
    private PlayerHP playerHP;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        playerHP = FindObjectOfType<PlayerHP>();

        InvokeRepeating(nameof(BeginSpawn), 1f, spawnRate);
    }

    private void BeginSpawn()
    {
        int randomLane = Random.Range(0, 3);
        var spawnedItem = Instantiate(itemToSpawn, parent);

        spawnedItem.lane = randomLane;

        float xPosition = (randomLane - 1) * laneOffset;

        spawnedItem.itemPosition = new Vector3(xPosition,spawnY, spawnZ);
        spawnedItem.hasHit = false;

        items.Add(spawnedItem);
    }

    private void Update()
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            Item item = items[i];

            if (item == null)
            {
                items.RemoveAt(i);
                continue;
            }

            ItemMover(item);
            CheckCollision(item);

            if (item.itemPosition.z <= despawnZ)
            {
                Destroy(item.gameObject);
                items.RemoveAt(i);
            }
        }
    }

    private void ItemMover(Item item)
    {
        item.itemPosition.z -= speed * Time.deltaTime;
    }

    private void CheckCollision(Item item)
    {
        if (item.hasHit)
            return;

        float distanceZ = Mathf.Abs(item.itemPosition.z - collisionZ);

        if (distanceZ <= collisionRange)
        {
            if (item.lane == player.CurrentLane)
            {
                if (player.jumpHeight < item.requiredJumpHeight)
                {
                    playerHP.TakeDamage();
                }
            }

            item.hasHit = true;
        }
    }
}
