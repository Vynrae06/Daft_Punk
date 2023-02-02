using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryText : MonoBehaviour
{
    public void ShowText()
    {
        GetComponent<TextMeshProUGUI>().text = "TEXTE DE FELICITATION ALLEZ CASSEZ VOUS \n- BISOUX, DAFT PUNK";
    }
}
