using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    private GoStop gostop;

    public bool faceUp = false;

    public string value;
    public int month;

    private string monthString;


    // Start is called before the first frame update
    private void Awake()
    {
        gostop = FindObjectOfType<GoStop>();
    }

    void Start()
    {
        if (CompareTag("Card"))
        {
            value = transform.name[0].ToString();

            for(int i=1; i<transform.name.Length; i++)
            {
                char m = transform.name[i];
                monthString = monthString + m.ToString();
                //Debug.Log("C =" + m + " i="+ i);
                //Debug.Log("VS ="+ valueString);
            }

            if(monthString == "1")
            {
                month = 1;
            }
            if (monthString == "2")
            {
                month = 2;
            }
            if (monthString == "3")
            {
                month = 3;
            }
            if (monthString == "4")
            {
                month = 4;
            }
            if (monthString == "5")
            {
                month = 5;
            }
            if (monthString == "6")
            {
                month = 6;
            }
            if (monthString == "7")
            {
                month = 7;
            }
            if (monthString == "8")
            {
                month = 8;
            }
            if (monthString == "9")
            {
                month = 9;
            }
            if (monthString == "10")
            {
                month = 10;
            }
            if (monthString == "11")
            {
                month = 11;
            }
            if (monthString == "12")
            {
                month = 12;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
