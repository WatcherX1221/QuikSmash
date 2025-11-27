using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityFunctions : MonoBehaviour
{
    [SerializeReference]
    public TestClass YOLO;
    public GameObject player;
    public GameObject defaultHitbox;
    MainMovement mainmovement;
    public enum HitboxTypes
    {
        Blast,
        Grab
    }
    public enum FunctionNames
    {
        HitboxSpawn
    }
    public enum Ability
    {
        SolarPunch
    }

    // Timers variables

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainmovement = player.GetComponent<MainMovement>();
    }

    public void CallFunction(FunctionNames functionName)
    {
        Resources.Load<TextAsset>("HitboxData" + functionName);
        if(functionName == FunctionNames.HitboxSpawn)
        {
           // HitboxSpawn();
        }
    }

    public void HitboxSpawn(HitboxTypes hitboxType, float time, Vector2 position, Vector2 size, GameObject hitboxShape = null)
    {
        /*
        if (hitboxShape == null)
        {
            hitboxShape = defaultHitbox;
        }
        GameObject newHitbox = Instantiate(hitboxShape, new Vector2(player.transform.position.x + position.x, player.transform.position.y + position.y), transform.rotation, transform.parent);
        newHitbox.transform.localScale = size;
        newHitbox.tag = hitboxType.ToString();
        */
    }
}
