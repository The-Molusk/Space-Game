using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneySystem : MonoBehaviour
{
    int currentMoney;
    [SerializeField] GameObject moneyText;
    // Start is called before the first frame update
    void Start()
    {
        //access external file and read money amount
    }
    private void Update()
    {
        moneyText.GetComponent<TMPro.TextMeshProUGUI>().text = ("£" + currentMoney.ToString());
    }

    public void addMoney(int money)
    {
        currentMoney = currentMoney + money;
    }
}
