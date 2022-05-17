using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotiTimeView : MonoBehaviour
{
  //  [SerializeField]
   // private CustomProperties properties;

    [SerializeField]
    private Button motiTimeDecidedButton = default;

    private int motiTime=5;

    private void Start()
    {
        motiTimeDecidedButton.onClick.AddListener(OnMotiTimeDecidedButtonClick);
    }
    private void OnMotiTimeDecidedButtonClick()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SetMotiTime>().SetTime(motiTime);
        gameObject.SetActive(false);
    }

    public void SetMotiTime(int motiTime)
    {
        this.motiTime = motiTime;
    }
}
