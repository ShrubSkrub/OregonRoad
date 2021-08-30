using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private bool active;
 
    [HideInInspector]
    public string objectID;

    [SerializeField] bool hideWhenNotOnScene;
    [SerializeField] string sceneNameToUnhideOn;

    private void Awake()
    {
        objectID = name;
        //+ transform.position.ToString() + transform.eulerAngles.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if(Object.FindObjectsOfType<DontDestroy>()[i] != this)
            {
                if(Object.FindObjectsOfType<DontDestroy>()[i].objectID.Equals(this.objectID))
                {
                    Destroy(this.gameObject);
                }
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (hideWhenNotOnScene && !active && SceneManager.GetActiveScene().name.Equals(sceneNameToUnhideOn)) //unhide
        {
            GetComponent<Canvas>().enabled = true;
            active = true;
            GetComponent<Canvas>().worldCamera = GameObject.Find("Camera").GetComponent<Camera>();
        }

        else if (hideWhenNotOnScene && active && !SceneManager.GetActiveScene().name.Equals(sceneNameToUnhideOn)) //hide
        {
            GetComponent<Canvas>().enabled = false;
            active = false;
        }
    }
}
