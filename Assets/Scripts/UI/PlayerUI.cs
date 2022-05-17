using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text userName_Text;
    [SerializeField]
    private TMP_Text timeLimit_Text;

    private string userName;
        

    public void SetUserName(string userName)
    {
        this.userName = userName;
        userName_Text.text = this.userName;
    }
    
}
