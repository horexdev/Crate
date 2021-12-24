using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMoney : MonoBehaviour
{
    public Text CoinsCount;

    void Update()
    {
        PlayerData data = SaveManager.LoadPlayer();
        CoinsCount.text = data.money.ToString();
    }
}
