using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    public GameObject handUI;         
    public AudioClip soundFX;         
    public AudioSource audioSource;   

    private bool inReach = false;    
    private bool isOpen = false;      
    private Animator animator;      
    private Coroutine autoCloseCoroutine;

    void Start()
    {
        animator = GetComponent<Animator>();
        handUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            handUI.SetActive(true);

            
            if (autoCloseCoroutine != null)
            {
                StopCoroutine(autoCloseCoroutine);
                autoCloseCoroutine = null;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            handUI.SetActive(false);

            
            if (isOpen && autoCloseCoroutine == null)
            {
                autoCloseCoroutine = StartCoroutine(AutoCloseAfterDelay(2f));
            }
        }
    }

    void Update()
    {
        if (inReach && Input.GetMouseButtonDown(0)) 
        {
            if (!isOpen)
            {
                Open();
            }
        }
    }

    void Open()
    {
        isOpen = true;
        animator.SetBool("open", true);
        PlaySoundFX();
    }

    void Close()
    {
        isOpen = false;
        animator.SetBool("open", false);
        PlaySoundFX();
    }

    void PlaySoundFX()
    {
        if (audioSource && soundFX)
        {
            audioSource.PlayOneShot(soundFX);
        }
    }

    IEnumerator AutoCloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

       
        if (!inReach && isOpen)
        {
            Close();
        }

        autoCloseCoroutine = null;
    }
}

