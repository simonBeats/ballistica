using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static int cash;

    public Text cashDisplay;

    private void Start()
    {
        cash = 1000;
    }

    private void Update()
    {
        cashDisplay.text = cash.ToString();
    }
}
