using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLevel : MonoBehaviour
{
    public int id;

    private Transform m_levelPlane;

    private void Awake() {
        m_levelPlane = this.transform.Find("LevelPlane");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show() {
        MeshRenderer[] meshs = this.transform.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshs)
            mesh.enabled = true;

        ParticleSystem[] particles = this.transform.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles) {
            particle.Play();
            // var emission = particle.emission;
            // emission.enabled = true;

        }
        m_levelPlane.gameObject.SetActive(false);
    }

    public void Select() {
        Show();
        m_levelPlane.gameObject.SetActive(true);
    }

    public void Hide() {
        MeshRenderer[] meshs = this.transform.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshs)
            mesh.enabled = false;

        ParticleSystem[] particles = this.transform.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles) {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            // var emission = particle.emission;
            // emission.enabled = false;
        }

        m_levelPlane.gameObject.SetActive(false);
    }
}
