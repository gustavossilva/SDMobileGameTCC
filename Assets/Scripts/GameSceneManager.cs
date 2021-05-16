using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public bool removeSound = false;

    public void StartGame(string game) {
        if (removeSound) {
            GameObject[] sounds = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject sound in sounds) {
                Destroy(sound);     
            }
        }
        SceneManager.LoadScene(game);
    }
}
