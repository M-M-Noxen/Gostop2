using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameObject slot1;
    private GoStop gostop;

    RaycastHit2D[] hits;
    public bool CardSelected = false;
    public bool Player1CardSelected = false;
    public bool Player2CardSelected = false;

    public int PlayerTurn = 0;


    // Start is called before the first frame update
    void Start()
    {
        gostop = FindObjectOfType<GoStop>();
    }

    // Update is called once per frame
    void Update()
    {
        /*MouseClickCheck();*/
        GetMouseClick();
        CardAction();
        TurnCounter();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            hits  = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, Mathf.Infinity);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                
                if (hit)
                {
                        if (hit.collider.CompareTag("Card") )
                        {
                            Debug.Log("Clicked on Card");                            
                            CardSelected = true;                          
                        }

                        if (hit.collider.CompareTag("CardPlacePlayer1"))
                        {
                            Debug.Log("Clicked on Player1");
                            Player1CardSelected = true;
                        }


                        else if (hit.collider.CompareTag("CardPlacePlayer2"))
                        {
                            Debug.Log("Clicked on Player 2");
                            Player2CardSelected = true;
                        }
                   
                }

            }
            //Debug.Log(mousePosition);
        }
    }

    /*void MouseClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse is clicked");
        }
    }*/

    void CardAction()
    {
        if (CardSelected == true & Player1CardSelected == true)
        {
            Debug.Log("No problem");// 조건 실행문으로 교체
            PlayerTurn = 2;
            CardSelected = !CardSelected;
            Player1CardSelected = !Player1CardSelected;
        }
            //카드가 선택되면 OtherPlace 중에서 월(month)을 비교해본다.
               //if (Other Place Card Month = Selected Card Month) 
                    //카드들을 충돌 시키고 덱에서 카드 한장을 뒤집는다.
                        // if( Other Place Card Month = Flipped Card Month)
                            //카드들을 충돌 시키고 해당 플레이어 밑으로 가져온다.
                                //if (Selected Card Month = Other Card Place Month = Flipped Card Month)
                                    //뻑이요
                        // if( Other Place Card Month != Flipped Card Month)
                            //Flipped Card 를 Other Card Place 에 내려놓는다.
               //if (Other Place Card Month != Selected Card Month)
                    // Selected Card 를 Other Card Place 에 내려놓는다
                        // if( Other Place Card Month = Flipped Card Month)
                            // 카드들을 충돌 시키고 해당 플레이어 밑으로 가져온다,
                        // if( Other Place Card Month != Flipped Card Month)
                            // Flipped Card 를 Other Card Place 에 내려놓는다.

        //이 실행을 하고 나면 턴이 종료되고 Player 2 에게 조종권이 넘어간다.

        if (CardSelected == true & Player2CardSelected == true)
        {
            Debug.Log("No problem either");
            PlayerTurn = 1;
            CardSelected = !CardSelected;
            Player2CardSelected = !Player2CardSelected; 
        }

    }




    void TurnCounter()
    {

        GameObject[] TagFinderTemp1 = GameObject.FindGameObjectsWithTag("NotSelectable1");
        GameObject[] TagFinderTemp2 = GameObject.FindGameObjectsWithTag("NotSelectable2");
        GameObject[] TagFinder1 = GameObject.FindGameObjectsWithTag("CardPlacePlayer1");
        GameObject[] TagFinder2 = GameObject.FindGameObjectsWithTag("CardPlacePlayer2");
        if (PlayerTurn == 0)
        {
            foreach (GameObject CardTag in TagFinder2)
            {
                CardTag.gameObject.tag = "NotSelectable2";
                //print("Cartag0:" + CardTag0);
            }

            //시작보정
        }

        else if (PlayerTurn == 1)
        {
            foreach (GameObject CardTag in TagFinderTemp1)
            {
                CardTag.gameObject.tag = "CardPlacePlayer1";
                //print("Cartag1:" + CardTag1);
            }

            foreach (GameObject CardTag in TagFinder2)
            {
                CardTag.gameObject.tag = "NotSelectable2";
                //print("Cartag2:" + CardTag2);
            }
            //Player1 차례
        }

        else if (PlayerTurn == 2)
        {
                foreach (GameObject CardTag in TagFinderTemp2)
                {
                    CardTag.gameObject.tag = "CardPlacePlayer2";
                    //print("Cartag3:" + CardTag3);
                }
                foreach (GameObject CardTag in TagFinder1)
                {
                    CardTag.gameObject.tag = "NotSelectable1";
                    //print("Cartag4:" + CardTag4.tag);                   
                }                
                //Player2 차례           
        }

    }


}
