using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityFunctions : MonoBehaviour
{
    public GameObject player;
    MainMovement mainmovement;
    // Timers variables
    [SerializeField]
    float solarPunchWindup = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainmovement = player.GetComponent<MainMovement>();
        solarPunchWindup = 1f;
    }

    public void SolarPunch(int playerDirection, Transform playerPosition)
    {
         
        if(solarPunchWindup > 0f)
        {
            AbilityTimerInfo("SolarPunch", playerDirection, playerPosition);
            player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, 0f);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            mainmovement.move.Disable();
            if(mainmovement.jump.IsPressed())
            {
                solarPunchWindup = 1f;
                mainmovement.move.Enable();
            }
        }
        else
        {
            GameObject hitboxObject = Resources.Load<GameObject>("Hitboxes/CircleHitbox");
            float hitboxDistance = 2.5f;
            Vector2 hitboxPosition = new Vector2(playerPosition.position.x + (playerDirection * hitboxDistance), playerPosition.position.y);
            GameObject hitbox = Instantiate(hitboxObject, hitboxPosition, transform.rotation);
            Destroy(hitbox, 0.25f);
            solarPunchWindup = 1f;
        }
        
    }

    public void AbilityTimerInfo(string abilityName, int playerDirection, Transform playerPosition)
    {
        Debug.Log("AbilityTimerInfo Called");
        if(abilityName == "SolarPunch")
        {
            Debug.Log("Attack is Solar Punch");
            if(solarPunchWindup > 0)
            {
                Debug.Log("Started changing value");
                solarPunchWindup -= Time.deltaTime;
                SolarPunch(playerDirection, playerPosition);
                Debug.Log("New time Solar Punch" + solarPunchWindup);
            }
        }
    }
}
