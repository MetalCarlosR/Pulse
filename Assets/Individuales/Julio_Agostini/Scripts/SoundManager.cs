using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager smInstance_;

    [SerializeField]
    private AudioSource shooting, door, walking, reload;



    void Awake()
    {
        {
            if (smInstance_ == null)
            {
                smInstance_ = this;
                Debug.Log("SoundManager Set");
            }
            else if (smInstance_ != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this);
        }
    }


    public void PlayShooting()
    {
        shooting.Play();
    }

    public void PlayDoor()
    {
        door.Play();
    }
    public void Playwalking()
    {
        walking.Play();
    }

    public void StopWalking()
    {
        walking.Stop();
    }

}
