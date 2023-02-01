using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText()
    {
        GetComponent<TextMeshProUGUI>().text = "TEXTE DE FELICITATION ALLEZ CASSEZ VOUS \n- BISOUX, DAFT PUNK";
    }
}
