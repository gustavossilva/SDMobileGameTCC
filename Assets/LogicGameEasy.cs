using UnityEngine;

public class LogicGameEasy : MonoBehaviour
{
    public ButtonSwitch firstSwitch;
    public ButtonSwitch secondSwitch;
    public LogicOperator logicOperator;
    public GameObject finalTrailOn;

    public void GenerateNewGame() {
        firstSwitch.GenerateRandomSwitch();
        secondSwitch.GenerateRandomSwitch();
        logicOperator.GenerateRandomLogicOperator();
        finalTrailOn.SetActive(false);
    }
}
