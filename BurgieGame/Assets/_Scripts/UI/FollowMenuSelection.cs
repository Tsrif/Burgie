using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FollowMenuSelection : MonoBehaviour, IPointerEnterHandler,ISelectHandler
{
    public GameObject icon;
    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    private Button button { get { return GetComponent<Button>(); } }

    private void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        source.volume = 0.2f;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //do stuff when highlighted
        //icon.transform.position = new Vector2(icon.transform.position.x, this.transform.position.y);
    }
    public void OnSelect(BaseEventData eventData)
    {
        //do stuff when selected
        icon.transform.position = new Vector2(icon.transform.position.x, this.transform.position.y);
        PlaySound();
    }

    void PlaySound()
    {
        source.PlayOneShot(sound);
    }


}