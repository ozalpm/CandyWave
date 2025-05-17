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
            // Zaman� s�f�rdan ba�lat ve sahneyi yeniden y�kle
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnNoButton()
        {
            // Zaman� s�f�rla ve ana men� sahnesine d�n
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }

    }
}
