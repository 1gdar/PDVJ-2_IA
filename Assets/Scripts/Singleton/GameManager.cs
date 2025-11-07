using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]    
    private GameObject[] objectsGM;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
      
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(gameObject); // Persiste entre escenas
    }

    private void Start()
    {
       Cursor.lockState = CursorLockMode.Confined;


    }

    public void GameOver()
    {
        objectsGM[0].SetActive(true);
    }
}
