using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryText : MonoBehaviour
{
    public void ShowText()
    {
        GetComponent<TextMeshProUGUI>().text = "Bravo, vous avez gagne !\nGrace a vous, le bug a ete resolu et l’amnesie generale n’est plus qu’un mauvais souvenir…\nMaintenant, depechez-vous de retourner a la machine !\nBISOUS, DAFT PUNK";
    }
}
