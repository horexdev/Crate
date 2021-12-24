using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTypeHandler : MonoBehaviour
{
    /*
     * CollectLevel ID = 1
     * SurviveLevel ID = 2
     * FinishLevel ID = 3
    */

    public static int TypeOfLevel = 0; // Тип уровня

    private void Awake() {
        if (GameObject.Find("CollectLevel") != null) {
            TypeOfLevel = 1;
            Debug.Log("Тип уровня: Collect");
        }
        else if (GameObject.Find("SurviveLevel") != null) {
            TypeOfLevel = 2;
            Debug.Log("Тип уровня: Survive");
        } else {
            TypeOfLevel = 3;
            Debug.Log("Тип уровня: Finish");
        }
    }
}
