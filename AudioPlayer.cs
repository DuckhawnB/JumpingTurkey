using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip dyingClip;
    [SerializeField][Range(0f, 1f)] float dyingVolume = 1f;

    public void PlayDyingClip()
    {
        if (dyingClip != null)
        {
            AudioSource.PlayClipAtPoint(dyingClip, Camera.main.transform.position, dyingVolume);
        }
    }
}
