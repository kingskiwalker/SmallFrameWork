using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController:BaseController<MainController>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
