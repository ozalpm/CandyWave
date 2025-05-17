using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CandyWave.Controllers
{


public class MenuController : MonoBehaviour
{
        public void PlayGame()
        {
            SceneManager.LoadScene("SampleScene"); 
        }

        public void ExitGame()
        {
            Application.Quit();
            
        }
    }
 }
