using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PowerUpPickUp : MonoBehaviour
{

    public int speedModifier;
    // Start is called before the first frame update
    void Awake()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //only exectue OnPlayerEnter if the player collides with this token.
        var player = other.gameObject.GetComponent<PlayerController2D>();
        if (player != null) OnPlayerEnter(player);
    }

    void OnPlayerEnter(PlayerController2D player)
    {
        switch (gameObject.name) { 
            case "Run(Clone)": player.runSpeed += speedModifier;  break;
            case "DoubleJump(Clone)": player.canDoubleJump = true;     break;
            default: Debug.Log("default"); break;
        }
        Destroy(gameObject);
       /* if (collected) return;
        //disable the gameObject and remove it from the controller update list.
        frame = 0;
        sprites = collectedAnimation;
        if (controller != null)
            collected = true;
        //send an event into the gameplay system to perform some behaviour.
        var ev = Schedule<PlayerTokenCollision>();
        ev.token = this;
        ev.player = player;*/
    }
}
