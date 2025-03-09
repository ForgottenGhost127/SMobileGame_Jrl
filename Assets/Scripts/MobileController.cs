using System;
using UnityEngine;
using TMPro;

public class MobileController : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private TextMeshProUGUI _testText;
    private float _initialTime;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
        
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                _initialTime = Time.realtimeSinceStartup;

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                _testText.text = "Moving";

            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                _testText.text = "Stationary";

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                float finalTime = Time.realtimeSinceStartup - _initialTime;
                _testText.text = Input.GetTouch(0).position.ToString() + "\n" + finalTime;
            }
                
        }

        
            

    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
	
}
