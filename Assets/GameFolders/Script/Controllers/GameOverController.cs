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
            // Zamaný sýfýrdan baþlat ve sahneyi yeniden yükle
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnNoButton()
        {
            // Zamaný sýfýrla ve ana menü sahnesine dön
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }

    }
}
