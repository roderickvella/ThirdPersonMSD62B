using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //projectile
    public GameObject grenade;
    public Transform grenadeSpawnPosition;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane1")
            GameManager.Instance.OnChangeGameState(GameManager.GameState.AreaA);
        else if (collision.gameObject.name == "Plane2")
            GameManager.Instance.OnChangeGameState(GameManager.GameState.AreaB);
    }

    public void ThrowGrenade()
    {
        //launch the grenade
        Instantiate(grenade, grenadeSpawnPosition.position, grenadeSpawnPosition.rotation);
    }
}
