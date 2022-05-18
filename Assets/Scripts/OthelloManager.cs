using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State { EMPTY, BLACK, WHITE }
public class OthelloManager : MonoBehaviour
{
    [SerializeField]
    private EndGame endGame;
    [SerializeField]
    private TMPro.TMP_Text Textview;
    State nowState = State.BLACK;
    
    //置ける場所リスト
    List<Vector2Int>[,] ReverseLists = new List<Vector2Int>[8, 8];

    //おける座標のリスト
    List<Vector2Int> ReversablePositions = new List<Vector2Int>();
    
    private bool preSkip = false;
    //セルの情報初期は空
    //public State myState { get; private set; } = State.EMPTY;

    //セルの情報だけ
    int[,] cellsState = new int[8, 8]{
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,1,2,0,0,0},
        {0,0,0,2,1,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0}};


    //セルたちのこんとろーらー
  public   GameObject[,] CellsObject = new GameObject[8, 8] ;

    private void Start()
    {
        SetReverceLists(nowState);
    }

    //セルの情報セット
    public void SetState(Vector2Int position , State state)
    {
        cellsState[position.x,position.y] = (int)state;
    }
    public void SetCellObject(Vector2Int position, GameObject cell)
    {
        CellsObject[position.x, position.y] = cell;
    }


    //探索方向
    Vector2Int[] Directions = new Vector2Int[8]
    {
        new Vector2Int(-1,-1),
    new Vector2Int(-1,0),
    new Vector2Int(-1,1),
    new Vector2Int(0,-1),
    new Vector2Int(0,1),
    new Vector2Int(1,-1),
    new Vector2Int(1,0),
    new Vector2Int(1,1)
    };

    //
    //State state;
    //Vector2Int position;
    List< Vector2Int> ReverseList(State state , Vector2Int position  )
    {
        //this.state = state;
        //this.position = position;
        List<Vector2Int> positions=new List<Vector2Int>();


        //指定した座標がクリック可能か、可能ならひっくり返る座標たちを返す
        //すでに石があればクリック不可
        if (cellsState[position.x, position.y] != 0) return new List<Vector2Int>();

        //いちますとなり
        for (int i_D = 0; i_D < Directions.Length; i_D++)
        {
            // Debug.Log(Directions[i_D]);

            //おわりになるまでやる。最大８回ループ
            //何ことなりに同じ色があるか
        
            for (int j = 1; j < 8; j++)
            {
                int sameColor_index = 0;
                Vector2Int checkPosition = position + Directions[i_D] * j;
               // Debug.Log(checkPosition);
                //マス目を超えたらおわり
                if (Mathf.Min(checkPosition.x, checkPosition.y) < 0 || Mathf.Max(checkPosition.x, checkPosition.y) >= 8) break;
                
                //空欄だったら終わり
                if (cellsState[checkPosition.x, checkPosition.y] == 0) break;
                //同じ色があったら終わり
                if (cellsState[checkPosition.x, checkPosition.y] == (int)state)
                {
                    if (j == 1) break;
                    sameColor_index = j;


                    for (int num = 1; num < sameColor_index; num++)
                    {
                        positions.Add(position + Directions[i_D] * num);
                       
                    }
                    break;
                }
            }
        
        }
        return positions;
    }

    public void ChangeBoard(State state, Vector2Int position)
    {
        cellsState[position.x, position.y] = (int)state;
    }

    public Vector2Int testPosition;
    public State testState;
    [ContextMenu("test")]
    public void Reverse(State state,Vector2Int position)
    {
        this.testPosition = position;
        this.testState = state;
       // var test = ReverseList(testState, testPosition);↓とおなじいみのはず
        var test = ReverseLists[testPosition.x, testPosition.y];
            // for(int i = 0; i< test.Count; i  ++ )
            //  if (test.Count > 0)
            //    {
            //Debug.Log(test[0]);
            CellsObject[testPosition.x, testPosition.y].GetComponent<DiskManager>().SetDisk(testState);
            ChangeBoard(testState, testPosition);
        for (int i = 0; i < test.Count; i++)
        {
            CellsObject[test[i].x, test[i].y].GetComponent<DiskManager>().ChangeDisk(testState);
            ChangeBoard(testState, test[i]);

            //  }
        }
        ChangeState( ( (int)nowState == 1) ? State.WHITE : State.BLACK);
    }

    //
    Vector2Int score;
    public Vector2Int GetScore()
    {
        score = Vector2Int.zero;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (cellsState[i, j] == 1)
                {
                    score.x++;
                }
                else if(cellsState[i,j]==2)
                {
                    score.y++;
                }     
            }
        }
        return score;
    }
  
 
    //リスト作ると同時にスキップチェックする
    //ReversablePositionsにもいれる
    public void SetReverceLists(State colorState)
    {
        bool nowSkip = true;
        ReverseLists = new List<Vector2Int>[8, 8];
        ReversablePositions = new List<Vector2Int>();
        for (int i = 0; i< 8; i ++)
        {
            for(int j = 0; j < 8; j++)
            {
                ReverseLists[i, j] = ReverseList(colorState, new Vector2Int(i, j));
                
                if (ReverseLists[i, j].Count > 0) 
                { 
                    //おける
                    nowSkip = false;
                    ReversablePositions.Add(new Vector2Int(i, j));
                }
            

            }
        }
  
        if (nowSkip && preSkip　)
        {      
            //おわり
            StartCoroutine(GoEnding());
       
           // EndGame();
         
          
        }
        else if (nowSkip && !preSkip)
        {
            //スキップ一回目
            preSkip = true;
            //スキップ？
            ChangeState(((int)nowState == 1) ? State.WHITE : State.BLACK);
            
        
        }else if(!nowSkip )
        {
            preSkip = false;
        }
       
    }

    IEnumerator GoEnding()
    {
        yield return new WaitForSeconds(1f);
        Vector2Int score = GetScore();
        endGame.gameObject.SetActive(true);
        endGame.EndState(score);
    }

    public void ChangeState(State state)
    {
        nowState = state;
        SetReverceLists(state);
        StateView();
    }
    private void StateView()
    {
        switch (nowState)
        {
         
            case State.BLACK:
                Textview.text = "黒の番です";
                break;
            case State.WHITE:
                Textview.text = "白の番です";
                break;
        }
      //  string stateString = nowState == State.BLACK ? "黒" : "白";
       // Textview.text = stateString + "の番です";
    }
    public State GetState()
    {
        return nowState;
    }

    //リバースリストがからじゃなかったらTRUE
    public bool PositionCanReverse(Vector2Int position)
    {
bool canReverse =        ReverseLists[position.x, position.y].Count > 0;
        return canReverse;
    }

    public void AutoReverse()
    {
        if (ReversablePositions.Count == 0) { Debug.Log("おくとこないよ"); return; }
        
        int RandomIndex = Random.Range(0, ReversablePositions.Count);
        Vector2Int ReversePosition = ReversablePositions[RandomIndex];
        Reverse(nowState, ReversePosition);
    }


    public State GetNowState()
    {
        return nowState;
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 150, 100), "Auto"))
        {
            AutoReverse();
        }
    }
  
}
