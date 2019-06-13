using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : MonoBehaviour
{
    ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = this.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particle != null) {
            Color color = Color.Lerp(new Color(0.2f, 0.2f, 0.2f), Color.white, SceneController.CONTEXT.TimeCtrl.LightIntensity);

            color.a = 0.8f;
            particle.GetComponent<ParticleSystemRenderer>().material.color = color;
        }
    }
}
