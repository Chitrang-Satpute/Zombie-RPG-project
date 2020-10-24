using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDropItem : MonoBehaviour
{
    
    public List<GameObject> drops;
    
    public int[] table =
        { 
        60, //Drop cube 
        30, //Drop sphere
        10  //Drop Capsule
    };

    public int total;
    public int randomNumber;

    public void DropItems()
        {

            foreach (var item in table)
            {
                total += item;
            }

            //Generate Random Number
            randomNumber = Random.Range(0, total);

            for (int i = 0; i < table.Length; i++)
            {

                //Comparing is my random number <= to the current weight?
                if (randomNumber <= table[i])
                {
                    Debug.Log(randomNumber + "" + drops[i].name);
                    //Award the drop
                    
                    Instantiate(drops[i], transform.position, Quaternion.identity);
                break;
                }
                else
                {
                    randomNumber -= table[i];
                }
            }
        }
}

