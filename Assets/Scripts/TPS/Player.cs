using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] private float playerHealth;
    public float presentHealth;
    [SerializeField] private GameObject playerDamage;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject endGamePanal;

    [Header("Player follow camera")]
    [SerializeField] private Transform playerCamera;


    [Header("Amination and Gravity")]
    [SerializeField] private CharacterController cC;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] Animator animator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = playerHealth;
        healthBar.GiveFullHealth(playerHealth);
    }

    public void PlayerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        StartCoroutine(PlayerDamage());
        healthBar.SetHealth(presentHealth);
        if (presentHealth <= 0)
        {           
            PlayerDie();
        }
    }
    private void PlayerDie()
    {
        print("dieeeeeeeeeee");
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        endGamePanal.SetActive(true);
        Time.timeScale = 0f;
    }
    private IEnumerator PlayerDamage()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(2.18f);
        playerDamage.SetActive(false);
    }    
}
