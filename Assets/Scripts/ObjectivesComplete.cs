using TMPro;
using UnityEngine;


public class ObjectivesComplete : MonoBehaviour
{
    [Header("Objectives to Complete")]

    public TMP_Text objectives1;
    public TMP_Text objectives2;
    public TMP_Text objectives3;
    public TMP_Text objectives4;   

    public static ObjectivesComplete occurrence;

    private void Awake()
    {
        occurrence = this;
    }
    //public void GetObjectiveDone(bool obj1, bool obj2, bool obj3, bool obj4)
    //{
    //    Mission1(obj1);
    //    Mission2(obj2);
    //    Mission3(obj3);
    //    Mission4(obj4);

    //}

    public void Mission4(bool obj4)
    {
        if (obj4 == true)
        {
            objectives4.text = "4. Complete";
            objectives4.color = Color.green;
        }
        else
        {
            objectives4.text = "4. Take people to a safe place";
            objectives4.color = Color.red;
        }
    }

    public void Mission3(bool obj3)
    {
        if (obj3 == true)
        {
            objectives3.text = "3. Complete";
            objectives3.color = Color.green;
        }
        else
        {
            objectives3.text = "3. Find Car";
            objectives3.color = Color.red;
        }
    }

    public void Mission2(bool obj2)
    {
        if (obj2 == true)
        {
            objectives2.text = "2. Complete";
            objectives2.color = Color.green;
        }
        else
        {
            objectives2.text = "2. Locate the villagers";
            objectives2.color = Color.red;
        }
    }

    public void Mission1(bool obj1)
    {
        if (obj1 == true)
        {
            objectives1.text = "1. Complete";
            objectives1.color = Color.green;
        }
        else
        {
            objectives1.text = "1. Find the Rifle";
            objectives1.color = Color.red;
        }
    }
}
