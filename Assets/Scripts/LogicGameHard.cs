using UnityEngine;

public class LogicGameHard : MonoBehaviour
{
    public ButtonSwitch firstSwitch;
    public ButtonSwitch secondSwitch;
    public ButtonSwitch thirdSwitch;
    public ButtonSwitch fourthSwitch;
    public LogicOperator logicOperator;
    public LogicOperator logicOperator2;
    public LogicOperator logicOperator3;
    public GameObject midTrail1;
    public GameObject midTrail2;
    public GameObject finalTrailOn;

    public void GenerateNewGame() {
        midTrail1.SetActive(false);
        midTrail2.SetActive(false);
        firstSwitch.GenerateRandomSwitch();
        secondSwitch.GenerateRandomSwitch();
        thirdSwitch.GenerateRandomSwitch();
        fourthSwitch.GenerateRandomSwitch();
        logicOperator.GenerateRandomLogicOperator();
        logicOperator2.GenerateRandomLogicOperator();
        logicOperator3.GenerateRandomLogicOperator();
        finalTrailOn.SetActive(false);
    }
}
