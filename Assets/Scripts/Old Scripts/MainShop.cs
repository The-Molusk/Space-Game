using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShop : MonoBehaviour
{
    #region Vars
    mouseLook LookScript;

    [SerializeField, Range(0f,10f)] float interactRadius = 10f;

    [SerializeField] GameObject player;
    [SerializeField] GameObject openText;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject playerCameraHolder;
    #endregion
    private void Start()
    {
        LookScript = playerCameraHolder.GetComponent<mouseLook>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < interactRadius)
        {
            openText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                //opens shop menu
                Debug.Log("Open Shop");
                showShop();
                
            }  
        }
        else
        {
            openText.SetActive(false);
        }
        if(shopUI.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            hideShop();
        }
    }
    void hideShop()
    {
        LookScript.enableLook();
        shopUI.SetActive(false);
        gameUI.SetActive(true);
    }
    void showShop()
    {
        LookScript.disableLook();
        shopUI.SetActive(true);
        gameUI.SetActive(false);
    }
}
