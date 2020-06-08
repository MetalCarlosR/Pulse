using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 LookDir;
    Vector3 ControllerDir;

    private int speed_ = 7;

    private bool pulse_ = false;
    private bool laser = false;
    private FieldOfView fov;
    private float fovSet = 360, limit = 50, cd = 0.15f, timeCD;

    [SerializeField]
    private GameObject black = null;
    private Pistola gun;
    private Daga daga;
    private AudioSource walking;
    private float lastDir = 0;
    
    void Start()
    {
        gun = GetComponent<Pistola>();
        rb = GetComponent<Rigidbody2D>();
        daga = GetComponentInChildren<Daga>();
        walking = GetComponent<AudioSource>();
        black.SetActive(true);
        timeCD = Time.time;
        if (GameManager.gmInstance_ != null)
        {
            GameManager.gmInstance_.SetPlayer(gameObject);
            fov = GameManager.gmInstance_.createFieldofView();
            fov.name = "FieldOfView" + name;
            fov.SetInstance(limit, fovSet);
        }
        else
        {
            Debug.LogError("Warnign GameManager was null when trying to access it from " + this);
        }


    }
    void Update()
    {
        if (!GameManager.gmInstance_.IsGamePaused())
        {
            if (!SettingsManager.smInstance_.GetSettings().controller_) ControlesTR();
            else ControlesMando();
            if (rb.velocity.y != 0 || rb.velocity.x != 0)
            {
                PlayWalking();
            }
            else
            {
                walking.Stop();
            }
            if (fov != null)
            {
                if (fovSet != 360)
                {
                    fov.SetAngle(-transform.right);
                }
                fov.SetOrigin(transform.position);
            }

        }
    }

    private void PlayWalking()
    {
        if (!walking.isPlaying)
        {
            walking.Play();
        }
    }

    void ControlesTR()
    {
        LookAtMouse();
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed_;
        if (Input.GetButtonDown("Fire1") && !pulse_ && Time.time > (timeCD + cd))
        {
            daga.Attack();
            gun.Shoot();
            timeCD = Time.time + cd;
        }
        if (Input.GetButtonDown("Fire2") && !pulse_ && !laser)
        {
            gun.Laser(true);
            laser = true;
        }
        else if (Input.GetButtonUp("Fire2") && laser)
        {
            gun.Laser(false);
            laser = false;
        }
    }
    void LookAtMouse()
    {
        LookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        transform.up = LookDir;
    }

    void ControlesMando()
    {
        LookAtController();
        rb.velocity = new Vector2(Input.GetAxis("HorizontalMando"), Input.GetAxis("VerticalMando")).normalized * speed_;
        if (Input.GetAxisRaw("Fire1Mando") == 1 && !pulse_ && Time.time > (timeCD + cd))
        {
            daga.Attack();
            gun.Shoot();
            timeCD = Time.time + cd;
        }
        if (Input.GetAxisRaw("Fire2Mando") == 1 && !pulse_ && !laser)
        {
            gun.Laser(true);
            laser = true;
        }
        else if (Input.GetAxisRaw("Fire2Mando") == 0 && laser)
        {
            gun.Laser(false);
            laser = false;
        }
    }
    void LookAtController()
    {
        float dir = Mathf.Atan2(Input.GetAxis("HorizontalRight"), Input.GetAxis("VerticalRight")) * Mathf.Rad2Deg;
        if (dir == 0) dir = lastDir;
        ControllerDir = new Vector3(0, 0, dir);
        transform.rotation = Quaternion.Euler(ControllerDir);
        lastDir = dir;
    }
    public void Die()
    {
        if (!GameManager.gmInstance_.TGM)
        {
            Pistola pistola = GetComponent<Pistola>();
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            walking.Stop();
            if (pistola)
            {
                pistola.enabled = false;
            }
            enabled = false;
            walking.loop = false;
            walking.clip = SoundManager.smInstance_.GetClip(SoundManager.FXSounds.PLAYER_DEATH);
            StartCoroutine(PlayerDeathSound());
        }
    }

    private IEnumerator PlayerDeathSound()
    {
        walking.Play();
        GameManager.gmInstance_.PlayerDeath();
        yield return new WaitForSeconds(3.5f);
        SoundManager.smInstance_.PublicSetFxVolume(false);

    }

    public void UsePulse(bool pulse)
    {
        pulse_ = pulse;
        if (!pulse) speed_ = 10;
        else
        {
            speed_ = 0;
            if (gun.enabled)
            {
                gun.Laser(false);
                SetLaser(false);
            }
        }

    }
    public int GetSpeed()
    {
        return speed_;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Puerta door = collision.GetComponent<Puerta>();
        if (door && Input.GetButtonDown("Use"))
        {
            door.MovPuerta(transform);
        }
    }
    public void SetLaser(bool laser_)
    {
        laser = laser_;
    }
}
