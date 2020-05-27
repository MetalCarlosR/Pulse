using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioArma : MonoBehaviour
{
    [SerializeField]
    private Daga daga = null;
    [SerializeField]
    private Pistola pistola = null;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        pistola.enabled = false;
        GameManager.gmInstance_.SetWeapon(daga.enabled);
    }

    void Update()
    {
        if (Input.GetButtonDown("WeaponChange") && !GameManager.gmInstance_.IsGamePaused())
        {
            animator.SetTrigger("Cambio arma");
            daga.gameObject.SetActive(!daga.gameObject.activeSelf);
            daga.enabled = !daga.enabled;
            pistola.enabled = !pistola.enabled;
            GameManager.gmInstance_.SetWeapon(daga.enabled);
        }
    }
}
