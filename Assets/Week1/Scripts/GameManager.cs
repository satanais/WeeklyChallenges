using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance {  get; private set; }

    [SerializeField] private GameObject[] m_crown;
    [SerializeField] private Transform[] m_hidingPlaces;
    public Action m_backInPlace;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this);
        }
        else
        {
            s_instance = this;
        }
    }
    void Start()
    {
        RandomCrown();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame()
    {
        RandomCrown();
        BackInPlace();
    }

    private void RandomCrown()
    {
        List<Transform> pos = new List<Transform>(m_hidingPlaces);
        foreach (var crown in m_crown)
        {
            int randIndex = UnityEngine.Random.Range(0, pos.Count);

            crown.transform.position = pos[randIndex].position;

            pos.RemoveAt(randIndex);
            
        }

    }

    public void BackInPlace()
    {
        m_backInPlace?.Invoke(); //add m_gameManager.m_backInPlace += resetplace; in objects's script
    }
}
