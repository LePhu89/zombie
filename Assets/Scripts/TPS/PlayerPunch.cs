using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header("Player Punch")]
    [SerializeField] private Camera cam;
    [SerializeField] private int giveDamageOf = 1;
    [SerializeField] private float punchingRange = 5f;

    [Header("Punch Effect")]
    [SerializeField] private GameObject punchEffect;
    [SerializeField] private GameObject woodEffect;


    public void Punch()
    {
        RaycastHit hitinfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitinfo, punchingRange))
        {

            Debug.Log(hitinfo.transform.name);
            Healths healths = hitinfo.transform.GetComponent<Healths>();
            Enemy1 enemy1 = hitinfo.transform.GetComponent<Enemy1>();
            
            if (healths != null)
            {
                healths.Takedamage(giveDamageOf);
                GameObject effect = Instantiate(woodEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(effect, 1f);
            }
            else if (enemy1 != null)
            {
                enemy1.ZombieHitDame(giveDamageOf);
                GameObject gore = Instantiate(punchEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(gore, 1f);
            }           
        }
    }
}
