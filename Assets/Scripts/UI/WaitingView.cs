using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaitingView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI waiatingText;
    [SerializeField]
    private int interval = 50;
    private int count=0;
    private int length;
    // Start is called before the first frame update
    void Start()
    {
        length = waiatingText.text.Length;
        waiatingText.maxVisibleCharacters = length - 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (count>interval)
        {
            count = 0;
            waiatingText.maxVisibleCharacters++;
            if (waiatingText.maxVisibleCharacters > length)
            {
                waiatingText.maxVisibleCharacters = length - 3;
            }
        }
        count++;
    }
}
