using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public GameObject hearthContainer;
    private float fillValue;

    void Update(){
        fillValue = (float) GameController.Health;
        fillValue = fillValue / GameController.MaxHealth;
        hearthContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
