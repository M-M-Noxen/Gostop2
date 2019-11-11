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
        //Check();
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
        //Check();
        CardAction();
        TurnCounter();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, Mathf.Infinity);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                if (hit)
                {
                    if (hit.collider.CompareTag("Card"))
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
        if (slot1 = this.gameObject)
        {
            slot1 = selected;
        }
    }

    void CardAction()
    {
        if (Player1CardSelected == true)
        {
            CardRule();
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

    void CardRule()
    {
        if (CardSelected == true)
        {
            int MonthPlace = 0;
            List<int> MonthtempNum = new List<int>();
            List<int> MonthtempPlace = new List<int>();
            Selectable s1 = slot1.GetComponent<Selectable>();
            GameObject[] FloorPlaceUserinput = gostop.FloorPlace;

            foreach (int m in gostop.FloorCardsMonth)
            {
                if (m == s1.month) 
                {
                    MonthtempNum.Add(m);
                    MonthtempPlace.Add(MonthPlace);
                }
                MonthPlace++;
            }
            //깔린카드 검사 -> MonthtempNum에 일치하는 month를, MonthtempPlace 에 month의 위치를
            if (MonthtempNum.Count == 0)
            {
                //일치하는 카드가 없다면
                gostop.FloorCards.Add(slot1.name);
                gostop.FloorCardsMonth.Add(s1.month);
                //선택한 카드를 장판 패 List에 추가
                string[] tempArr = gostop.FloorCards.ToArray();
                int temp = 0;
                foreach(string m in tempArr)
                {
                    if(m == slot1.name)
                    {
                        slot1.transform.position = new Vector3(gostop.FloorPlace[temp].transform.position.x, gostop.FloorPlace[temp].transform.position.y, gostop.FloorPlace[temp].transform.position.z);
                        gostop.Player1Cards.Remove(m);
                        gostop.Player1CardsMonth.Remove(s1.month);
                        //임의의 리스트(tempArr) 안에서 몇번째(temp)에 속하는지 찾아서 카드 배치(transformposition)
                        //이후 손패에서 제거(Remove)
                    }
                    temp++;
                }
                FlipCard();
                MonthtempNum.Clear();
                MonthtempPlace.Clear();
                //다음 시행을 위해 List 초기화(Clear)
            }

            if (MonthtempNum.Count == 1)
            {
                /*slot1.transform.position = new Vector3(FloorPlaceUserinput[MonthtempPlace[0]].transform.position.x + 1f, FloorPlaceUserinput[MonthtempPlace[0]].transform.position.y, FloorPlaceUserinput[MonthtempPlace[0]].transform.position.z + 0.5f);
                deckUserinput.RemoveAt(deckUserinput.Count - 1);
                GameObject newCard = Instantiate(cardPrefabs, new Vector3(FloorPlace[i - 20].transform.position.x, FloorPlace[i - 20].transform.position.y, FloorPlace[i - 20].transform.position.z), Quaternion.identity, FloorPlace[i - 20].transform);
                newCard.name = card;
                newCard.GetComponent<Selectable>().faceUp = true;*/
                
            }

            /*if (Monthtemp.Count == 2)
            {
                //UI Canvas 활성화 -> 2개의 버튼으로 구성, List 에 있는 카드들을 불러온다 -> 선택 -> 그 카드를 다시 함수에 반환,이동
                slot1.transform.position = new Vector3(FloorPlaceUserinput[Monthtemp[0]].transform.position.x + 1f, FloorPlaceUserinput[Monthtemp[0]].transform.position.y, FloorPlaceUserinput[Monthtemp[0]].transform.position.z + 0.5f);
            }*/
        }
    }

    void FlipCard()
    {
        int MonthPlace = 0;
        List<int> Reserve_Month_After_Flip = new List<int>();
        List<int> Reserve_Place_After_Flip = new List<int>();
        //덱에서 꺼낸 카드(deck.Peek())를 장판 패에 비교(if문)
            //같을 경우 월과 장소를 List에 저장
        foreach (int k in gostop.FloorCardsMonth)
        {
            if(k == gostop.monthdeck.Peek())
            {
                Reserve_Month_After_Flip.Add(k);
                Reserve_Place_After_Flip.Add(MonthPlace);
            }

            MonthPlace++;
        }
        if(Reserve_Month_After_Flip.Count == 0)
        {
            //뒤집었는데 같은 카드가 없는 경우
            string tempsave = gostop.deck.Peek();
            int tempPlace = 0;
            gostop.FloorCardsMonth.Add(gostop.monthdeck.Pop());
            gostop.FloorCards.Add(gostop.deck.Pop());
            //덱에 있던 카드를 장판 패에 추가

            foreach (string m in gostop.FloorCards)
            {
                if(m == tempsave)
                {
                    GameObject newCard = Instantiate(gostop.cardPrefabs, new Vector3(gostop.FloorPlace[tempPlace].transform.position.x, gostop.FloorPlace[tempPlace].transform.position.y, transform.position.z), Quaternion.identity, gostop.FloorPlace[tempPlace].transform);
                    newCard.name = gostop.FloorCards[tempPlace];
                    newCard.GetComponent<Selectable>().faceUp = true;
                }
                tempPlace++;
            }
            Reserve_Month_After_Flip.Clear();
            Reserve_Place_After_Flip.Clear();
            //몇번쨰에 추가 됐는지 확인(tempPlace),배치(Instantiate)
        }
    }

    void Check()
    {

    }
}
