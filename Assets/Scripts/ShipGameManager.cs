using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShipGameManager : Singleton<ShipGameManager>
{
    public bool lockLogicOperators = false;
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

    public int points = 0;
    public int maxPoints = 10;
    public int lifes = 2;
    public int qtdEasy = 4;
    public int qtdHard = 5;

    public TextMeshProUGUI pointText;
    public TextMeshProUGUI lifesText;

    public GameObject game1;
    public GameObject game2;
    public LogicGameEasy gameEasy;
    public LogicGameHard gameHard;
    public Animator oxygenAnimation;
    public AudioSource audioSource;
    public AudioClip failSFX;
    public AudioClip successClip;
    public Button sendButton;
    // Start is called before the first frame update
    protected override void Awake()
    {
        IsPersistentBetweenScenes = false;
        base.Awake();
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        lifesText.text = "Limite de erros: " + lifes.ToString();
        pointText.text = "Circuitos funcionando: " + points.ToString() + "/" + maxPoints.ToString();
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
        }
    }

    public void GenerateNewGame() {
        sendButton.interactable = true;
        bool isHard = qtdHard > 0 && (qtdEasy == 0 || Random.Range(0.0f, 1.0f) > 0.5);
        if (!isHard) {
            qtdEasy --;
            game1.SetActive(true);
            game2.SetActive(false);
            gameEasy.GenerateNewGame();
            gameType = "easy";
            return;
        }
        gameType = "hard";
        qtdHard--;
        game2.SetActive(true);
        game1.SetActive(false);
        gameHard.GenerateNewGame();
    }

    public void Send() {
        if (gameType == "easy") {
            bool winner = gameOperations[0] == "and" ? gameSwitches[0] && gameSwitches[1] : gameSwitches[0] || gameSwitches[1];
            if (!winner) {
                audioSource.clip = failSFX;
                audioSource.Play();
                lifes--;
                lifesText.text = "Limite de erros: " + lifes.ToString();
                if (lifes < 0) {
                    SceneManager.LoadScene("GameOverLogic");
                }
            } else {
                audioSource.clip = successClip;
                audioSource.Play();
                points++;
                pointText.text = "Circuitos funcionando: " + points.ToString() + "/" + maxPoints.ToString();
                if (points > maxPoints) {
                    PlayerPrefs.SetInt("finished2", 1);
                    if (PlayerPrefs.GetInt("finished1", 0) > 0) {
                        SceneManager.LoadScene("Victory");
                    } else {
                        SceneManager.LoadScene("CompleteLogic");
                    }
                }
                finalTrail.SetActive(true);
                oxygenAnimation.SetTrigger("InitOxygen");
                sendButton.interactable = false;
            }
        }
        if (gameType == "hard") {
            bool winner = gameOperations[2] == "and" ? midTrail1Active.activeSelf && midTrail2Active.activeSelf : midTrail1Active.activeSelf || midTrail2Active.activeSelf;
            if (!winner) {
                audioSource.clip = failSFX;
                audioSource.Play();
                lifes--;
                lifesText.text = "Limite de erros: " + lifes.ToString();
                if (lifes < 0) {
                    SceneManager.LoadScene("GameOverLogic");
                }
            } else {
                audioSource.clip = successClip;
                audioSource.Play();
                points++;
                pointText.text = "Circuitos funcionando: " + points.ToString() + "/" + maxPoints.ToString();
                if (points > maxPoints) {
                    PlayerPrefs.SetInt("finished2", 1);
                    if (PlayerPrefs.GetInt("finished1", 0) > 0) {
                        SceneManager.LoadScene("Victory");
                    } else {
                        SceneManager.LoadScene("CompleteLogic");
                    }                    
                }
                finalTrailGame2.SetActive(true);
                oxygenAnimation.SetTrigger("InitOxygen");
                sendButton.interactable = false;
            }      
        }
    }
}
