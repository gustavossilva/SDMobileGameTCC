using UnityEngine;

public class LogicOperator : MonoBehaviour
{
    public GameObject and;
    public GameObject or;
    // Start is called before the first frame update
    void Start()
    {
        ShipGameManager.Instance.SetGameOperation("and");
    }

    public void SwitchOperator() {
        string operation = this.and.activeSelf ? "or" : "and";
        ShipGameManager.Instance.SetGameOperation(operation);
        this.and.SetActive(!this.and.activeSelf);
        this.or.SetActive(!this.or.activeSelf);
        // tells gameManager the change
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
