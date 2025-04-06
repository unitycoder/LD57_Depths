using System.Collections;
using UnityEngine;

public class Musioc : MonoBehaviour
{
    public AudioClip[] notes;

    bool played1 = false;

    void Update()
    {
        float p1 = Mathf.PerlinNoise1D(Time.time * 4.5f);
        if (p1>0.75f)
        {
            if (!played1)
            {
                played1 = true;
                AudioSource.PlayClipAtPoint(notes[0], transform.position);
            }
        }
        else
        {
            played1 = false;

        }

        float p2 = Mathf.PerlinNoise1D(Time.time * 4.5f + 0.5f);
        if (p2 > 0.75f)
        {
            if (!played1)
            {
                played1 = true;
                AudioSource.PlayClipAtPoint(notes[1], transform.position);
            }
        }
        else
        {
            played1 = false;
        }

        float p3 = Mathf.PerlinNoise1D(Time.time * 24.5f + 1.0f);
        if (p3 > 0.75f)
        {
            if (!played1)
            {
                played1 = true;
                AudioSource.PlayClipAtPoint(notes[2], transform.position);
            }
        }
        else
        {
            played1 = false;
        }

        float p4 = Mathf.PerlinNoise1D(Time.time * 14.5f + 1.5f);
        if (p4 > 0.75f)
        {
            if (!played1)
            {
                played1 = true;
                AudioSource.PlayClipAtPoint(notes[3], transform.position);
            }
        }
        else
        {
            played1 = false;
        }
    }


}
