using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityFunctions : MonoBehaviour
{
    public GameObject player;
    public GameObject defaultHitbox;
    MainMovement mainmovement;
    public enum HitboxTypes
    {
        Blast,
        Grab
    }

    // Timers variables

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainmovement = player.GetComponent<MainMovement>();
    }
    private void Update()
    {
        HitboxSpawn(HitboxTypes.Grab, 0.25f, new Vector2(2f, 1f), new Vector2(1f, 1f));
    }

    public void HitboxSpawn(HitboxTypes hitboxType, float time, Vector2 position, Vector2 size, GameObject hitboxShape = null)
    {
        if (hitboxShape == null)
        {
            hitboxShape = defaultHitbox;
        }
        GameObject newHitbox = Instantiate(hitboxShape, new Vector2(player.transform.position.x + position.x, player.transform.position.y + position.y), transform.rotation, transform.parent);
        newHitbox.transform.localScale = size;
        newHitbox.tag = hitboxType.ToString();
    }
}
