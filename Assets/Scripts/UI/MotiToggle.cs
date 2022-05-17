using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MotiToggle : MonoBehaviour
{
    [SerializeField]
    private int myMotiTime;
   
    private Toggle motiTimeToggle = default;
    [SerializeField]
    private MotiTimeView motiTimeView;
    private void Start()
    {
        motiTimeToggle = GetComponent<Toggle>();
        motiTimeToggle.onValueChanged.AddListener(OnMotiTimeToggleValueChanged);
    }
    private void OnMotiTimeToggleValueChanged(bool value)
    {
        if (value)
        {
            motiTimeView.SetMotiTime(myMotiTime);
        }
    }
}
