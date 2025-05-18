using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CandyWave.Controllers
{


public class GameOverController : MonoBehaviour
{
        public void OnYesButton()
        {
            
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnNoButton()
        {
           
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }

    }
}
