using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskManager : MonoBehaviour
{
    public GameObject prefab;
    GameObject obj;
    Animator anim;
    Vector3 offset;



    public void SetDisk(State state)
    {
        offset = this.transform.position - Vector3.Scale(this.transform.position , Vector3.forward);
        obj = Instantiate(prefab, offset, Quaternion.identity, this.transform);
        anim =obj.GetComponent<Animator>();
        if (state == State.BLACK)
        {
            anim.SetFloat("Color", 0);
        }
        else
        {
            anim.SetFloat("Color", 1);
        }
    }
  
    public void ChangeDisk(State state)
    {
        if (state == State.BLACK)
        {
            anim.Play("ToBlack");
        }
        else
        {
            anim.Play("ToWhite");

        }
    }

    [ContextMenu("TestSetDiskWhite")]
    public void TestSetDiskWhite()
    {
        SetDisk(State.WHITE);
      
    }

    [ContextMenu("TestSetDiskBlack")]
    public void TestSetDiskBlack()
    {
        SetDisk(State.BLACK);
      
    }
    [ContextMenu("TestChangeDiskWhite")]
    public void TestChangeDiskWhite()
    {
        ChangeDisk(State.WHITE);

    }
    [ContextMenu("TestChangeDiskBLACK")]
    public void TestChangeDiskBlack()
    {
        ChangeDisk(State.BLACK);
 
    }




}
