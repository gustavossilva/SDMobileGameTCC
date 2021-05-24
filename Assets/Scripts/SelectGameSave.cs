using UnityEngine;

public class SelectGameSave : MonoBehaviour
{
    public GameObject executed1;
    public GameObject executed2;
    public GameObject executed3;
    public GameObject congrats;
    void Start()
    {
        bool finishedFirst = PlayerPrefs.GetInt("finished1", 0) > 0;
        bool finishedSecond = PlayerPrefs.GetInt("finished2", 0) > 0;
        bool finishedThird = PlayerPrefs.GetInt("finished3", 0) > 0;
        bool finishedGame = finishedFirst && finishedSecond && finishedThird;
        if (finishedFirst) {
            executed1.SetActive(true);
        }
        if (finishedSecond) {
            executed2.SetActive(true);
        }
        if (finishedThird) {
            executed3.SetActive(true);
        }
        if (finishedGame) {
            congrats.SetActive(true);
        }
    }

}
