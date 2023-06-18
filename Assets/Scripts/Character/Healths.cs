using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healths : MonoBehaviour
{
    [SerializeField] private float health;


    public void Takedamage(int damage)
    {
        health -= damage;
        if(health > 0)
        {

        }
        else
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
