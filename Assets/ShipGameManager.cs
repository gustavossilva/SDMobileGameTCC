using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGameManager : Singleton<ShipGameManager>
{
    public bool lockLogicOperators = true;
    public string gameType = "easy";
    public GameObject finalTrail;
    public GameObject finalTrailGame2;
    public GameObject midTrail1;
    public GameObject midTrail2;
    public GameObject midTrail1Active;
    public GameObject midTrail2Active;
    public List<bool> gameSwitches = new List<bool>(new bool[4]);
    public List<string> gameOperations = new List<string>(new string[4]);
    public string game1Operation = "and";
    // Start is called before the first frame update
    protected override void Awake()
    {
        IsPersistentBetweenScenes = false;
        base.Awake();
    }

    public void SetGameArray(bool value, int position) {
        gameSwitches[position] = value;
        if (gameType == "hard") {
            if (position == 0 || position == 1) {
                bool active = gameOperations[0] == "and" ? gameSwitches[0] && gameSwitches[1] : gameSwitches[0] || gameSwitches[1];
                if (active) {
                    midTrail1.SetActive(false);
                    midTrail1Active.SetActive(true);
                } else {
                    midTrail1.SetActive(true);
                    midTrail1Active.SetActive(false);
                }
            }
            if (position == 2 || position == 3) {
                bool active = gameOperations[1] == "and" ? gameSwitches[2] && gameSwitches[3] : gameSwitches[2] || gameSwitches[3];
                if (active) {
                    midTrail2.SetActive(false);
                    midTrail2Active.SetActive(true);
                } else {
                    midTrail2.SetActive(true);
                    midTrail2Active.SetActive(false);
                }
            }
            /*if (position == 3) {
                bool active = gameOperations[2] == "and" ? midTrail1Active.activeSelf && midTrail2Active.activeSelf : midTrail1Active.activeSelf || midTrail2Active.activeSelf;
                if (active) {
                    midTrail2.SetActive(false);
                    midTrail2Active.SetActive(true);
                } else {
                    midTrail2.SetActive(true);
                    midTrail2Active.SetActive(false);
                }
            }*/
        }
    }

    public void SetGameOperation(string operation, int position) {
        gameOperations[position] = operation;
        if (gameType == "hard") {
            if (position == 0) {
                bool active = gameOperations[0] == "and" ? gameSwitches[0] && gameSwitches[1] : gameSwitches[0] || gameSwitches[1];
                if (active) {
                    midTrail1.SetActive(false);
                    midTrail1Active.SetActive(true);
                } else {
                    midTrail1.SetActive(true);
                    midTrail1Active.SetActive(false);
                }
            }
            if (position == 1) {
                bool active = gameOperations[1] == "and" ? gameSwitches[2] && gameSwitches[3] : gameSwitches[2] || gameSwitches[3];
                if (active) {
                    midTrail2.SetActive(false);
                    midTrail2Active.SetActive(true);
                } else {
                    midTrail2.SetActive(true);
                    midTrail2Active.SetActive(false);
                }
            }
            /*if (position == 3) {
                bool active = gameOperations[2] == "and" ? midTrail1Active.activeSelf && midTrail2Active.activeSelf : midTrail1Active.activeSelf || midTrail2Active.activeSelf;
                if (active) {
                    midTrail2.SetActive(false);
                    midTrail2Active.SetActive(true);
                } else {
                    midTrail2.SetActive(true);
                    midTrail2Active.SetActive(false);
                }
            }*/
        }
    }

    public void Send() {
        if (gameType == "easy") {
            bool winner = gameOperations[0] == "and" ? gameSwitches[0] && gameSwitches[1] : gameSwitches[0] || gameSwitches[1];
            if (!winner) {
                // losthp
                Debug.Log("Wrong");
            } else {
                Debug.Log("Correct");
                // add point, generate new game
            }
        }
        if (gameType == "hard") {
            bool winner = gameOperations[2] == "and" ? midTrail1Active.activeSelf && midTrail2Active.activeSelf : midTrail1Active.activeSelf || midTrail2Active.activeSelf;
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
