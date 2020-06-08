using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> laser_ = new List<GameObject>();

    private AudioSource source;

    private bool active = true;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (GameManager.gmInstance_) GameManager.gmInstance_.AddButton(this);
    }

    public List<GameObject> GetLaser()
    {
        return laser_;
    }
    public bool IsActive()
    {
        return active;
    }

    public void SetButton(List<GameObject> laser , bool activeIn)
    { 
        laser_ = laser;
        active = activeIn;
        if (!active) GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController detectorJugador = collision.GetComponent<PlayerController>();
        if (detectorJugador && Input.GetButtonDown("Use") && active)
        {
            source.Play();
            active = false;
            GetComponent<SpriteRenderer>().color = Color.green;
            foreach (GameObject obj in laser_)
            {
                obj.SetActive(false);
            }
        }
    }
    private void OnDestroy()
    {
        if (GameManager.gmInstance_) GameManager.gmInstance_.RemoveButton(this);
    }
}
