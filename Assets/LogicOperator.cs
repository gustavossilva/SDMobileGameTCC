using UnityEngine;

public class LogicOperator : MonoBehaviour
{
    public GameObject and;
    public GameObject or;

    public int position = 0;
    // Start is called before the first frame update
    void Start()
    {
        bool startWithOr = Random.Range(0.0f, 1.0f) > 0.5;
        if (startWithOr) {
            and.SetActive(false);
            or.SetActive(true);
            ShipGameManager.Instance.SetGameOperation("or", position);
        }
        else {
            ShipGameManager.Instance.SetGameOperation("and", position);
        }
    }

    public void SwitchOperator() {
        if (ShipGameManager.Instance.lockLogicOperators) {
            return;
        }
        string operation = this.and.activeSelf ? "or" : "and";
        ShipGameManager.Instance.SetGameOperation(operation, position);
        this.and.SetActive(!this.and.activeSelf);
        this.or.SetActive(!this.or.activeSelf);
        // tells gameManager the change
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
