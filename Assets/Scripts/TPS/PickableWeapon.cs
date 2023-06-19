using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickableWeapon : MonoBehaviour
{
    [Header("Rifle")]
    [SerializeField] private WeaponSwitcher _playerWeaponSwitcher;
    [SerializeField] private Player player;

    public UnityEvent WeaponPickedUp;

    private float radius = 2.5f;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _playerWeaponSwitcher.EquipRifle();
                gameObject.SetActive(false);
                WeaponPickedUp.Invoke();
            }
        }        
    }
}
