using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGameManager : Singleton<ShipGameManager>
{
    public string gameType = "easy";
    public GameObject finalTrail;

    public List<bool> game1 = new List<bool>(new bool[2]);
    public string game1Operation = "and";
    // Start is called before the first frame update
    protected override void Awake()
    {
        IsPersistentBetweenScenes = false;
        base.Awake();
    }

    public void SetGameArray(bool value, int position) {
        if (gameType == "easy") { 
            game1[position] = value;
        }
    }

    public void SetGameOperation(string operation) {
        game1Operation = operation;
    }

    public void Send() {
        if (gameType == "easy") {
            bool winner = game1Operation == "and" ? game1[0] && game1[1] : game1[0] || game1[1];
            if (!winner) {
                // losthp
                Debug.Log("Wrong");
            } else {
                Debug.Log("Correct");
                // add point, generate new game
            }
        }
    }
}
