using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameObject slot1;

    private GoStop gostop;
    private Selectable selectable;

    RaycastHit2D[] hits;
    public bool CardSelected = false;
    public bool Player1CardSelected = false;
    public bool Player2CardSelected = false;

    public int PlayerTurn = 0;

    private void Awake()
    {
        gostop = FindObjectOfType<GoStop>();
        selectable = FindObjectOfType<Selectable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        slot1 = this.gameObject;
        Check();
    }

    // Update is called once per frame
    void Update()
    {        
        GetMouseClick();
        Check();
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
                            Card(hit.collider.gameObject);
                            CardSelected = true;                          
                        }

                        if (hit.collider.CompareTag("CardPlacePlayer1"))
                        {
                            Debug.Log("Clicked on Player1");
                            Player1CardSelected = true;
                        }


                        if (hit.collider.CompareTag("CardPlacePlayer2"))
                        {
                            Debug.Log("Clicked on Player 2");
                            Player2CardSelected = true;
                        }
                   
                }

            }
            //Debug.Log(mousePosition);
        }
    }

    void Card(GameObject selected)
    {
        if(slot1 = this.gameObject)
        {
            slot1 = selected; 
        }
    }

    void CardAction()
    {
        if (Player1CardSelected == true)
        {
            if (CardSelected == true)
            {
                int MonthCount = 0;
                int MonthPlace = 0;
                Selectable s1 = slot1.GetComponent<Selectable>();
                GameObject[] FloorPlaceUserinput = gostop.FloorPlace;
                
                foreach (int m in gostop.FloorCardsMonth)
                {
                    if (s1.month == m)
                    {
                        MonthCount++;
                        if (MonthCount >= 2)
                        {
                            print("What will you choose?");
                        }
                        slot1.transform.position = new Vector3(FloorPlaceUserinput[MonthPlace].transform.position.x + 1f, FloorPlaceUserinput[MonthPlace].transform.position.y, FloorPlaceUserinput[MonthPlace].transform.position.z + 0.5f);
                        //print("You have " + MonthCount + " " + m + "Month");
                    }
                    else
                    {
                        //print("Not Found");
                    }

                    MonthPlace++;
                }
            }
            Debug.Log("No problem");// 조건 실행문으로 교체
            PlayerTurn = 2;
            Player1CardSelected = !Player1CardSelected;
        }
            
        if (Player2CardSelected == true)
        {
            Debug.Log("No problem either");
            PlayerTurn = 1;
            Player2CardSelected = !Player2CardSelected; 
        }

        if (CardSelected == true)
        {
            CardSelected = !CardSelected;
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

    void Check()
    {
    }
}
