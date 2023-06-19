using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpp : MonoBehaviour
{
    [Header("Rifle")]
    [SerializeField] private GameObject playerRifle;
    [SerializeField] private GameObject pickUpRifle;
    [SerializeField] private Player player;

    public UnityEvent WeaponPickedUp;

    private float radius = 2.5f;

    private void Awake()
    {
        playerRifle.SetActive(false);   
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerRifle.SetActive(true);
                pickUpRifle.SetActive(false);

                WeaponPickedUp.Invoke();
            }
        }        
    }
}
