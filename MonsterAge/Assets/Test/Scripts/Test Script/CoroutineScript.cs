using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineScript : MonoBehaviour
{
    private MeshRenderer renderer;
    void Start()
    {
        renderer = this.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            //Debug.Log("Fade");
            StartCoroutine(TestCoroutine());

        }
        //StartCoroutine(Fade());

    }

    IEnumerator Fade()
    {
        Color color = renderer.material.color;
        for(float alpha = 1; alpha >= 0; alpha -= 0.1f)
        {
            Debug.Log(alpha);
            color.a = alpha;
            renderer.material.color = color;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator TestCoroutine()
    {
        Debug.Log("The First");

        yield return null;

        Debug.Log("The Second");

        yield return new WaitForSeconds(2);

        Debug.Log("The Third");

    }
}
