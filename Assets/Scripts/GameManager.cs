using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public GameObject canvas;
    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        //create singleton
        if (GameManager.Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;


        gameState = GameState.AreaA; 
    }

    // Update is called once per frame
    void Update()
    {
		//if (Input.GetKeyDown(KeyCode.Tab))
		//{
		//	canvas.GetComponentInChildren<InventoryManager>().ShowToggleInventory();
		//}
    }

    public void OnChangeGameState(GameState gameState)
    {
        print("changing game state to:" + gameState.ToString());
        this.gameState = gameState;
    }

    public void OnButtonPressed(string key)
    {
        if(gameState == GameState.AreaA)
        {
            //deal with the inventory system
            switch (key)
            {
                case "TAB":
                   canvas.GetComponentInChildren<InventoryManager>().ShowToggleInventory();
                   break;
                case "J":
                    canvas.GetComponentInChildren<InventoryManager>().ChangeSelection(true);
                    break;
                case "K":
                    canvas.GetComponentInChildren<InventoryManager>().ChangeSelection(false);
                    break;
                case "RETURN":
                    canvas.GetComponentInChildren<InventoryManager>().ConfirmSelection();
                    break;

            }

        }
        else if(gameState== GameState.AreaB)
        {
            //deal with the coin
        }
    }



    public enum GameState
    {
        AreaA,
        AreaB
    }
}
