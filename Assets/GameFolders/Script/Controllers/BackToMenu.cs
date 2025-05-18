using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CandyWave.Controllers
{



public class BackToMenu : MonoBehaviour
{
        public void GoBackToMenu()
        {
            
            SceneManager.LoadScene("Menu"); 
        }

    }
}
