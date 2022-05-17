using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
  
    public const int length=8;
   // [SerializeField]
 //   public CellController[,] cells=new CellController[length,length];
  [SerializeField]
    private GameObject cell_Prefab;
    [SerializeField]
    private Vector3 startPosition;

    public OthelloManager manager;

    private void Start()
    {
        SetBoard();
    }

    [ContextMenu("CreateBoard")]
    public void SetBoard()
    {
        for (int tate = 0; tate < length; tate++)
        {
            for (int yoko = 0; yoko < length; yoko++)
            {
                GameObject obj = Instantiate(cell_Prefab, startPosition + Vector3.right * yoko + Vector3.down * tate, Quaternion.identity, this.gameObject.transform);
                CellController cellController= obj.GetComponent<CellController>();
                cellController.Init(new Vector2Int(tate, yoko));
                //cells[tate, yoko] = cellController;
                manager.SetCellObject(new Vector2Int(tate, yoko), obj);
            }
        }
        SetStart();
    }

    private void SetStart()

    {
        manager.CellsObject[3, 3].GetComponent<DiskManager>().SetDisk(State.BLACK);
        manager.CellsObject[3, 4].GetComponent<DiskManager>().SetDisk(State.WHITE);
        manager.CellsObject[4, 3].GetComponent<DiskManager>().SetDisk(State.WHITE);
        manager.CellsObject[4, 4].GetComponent<DiskManager>().SetDisk(State.BLACK);

    }

}
