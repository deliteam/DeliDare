using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MainMenu:MonoBehaviour{
    public GameObject CreditsPanel;    
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
    public void OnCreditButtonClicked(){
        CreditsPanel.SetActive(true);
    }
    public void OnCreditCloseButton(){
        CreditsPanel.SetActive(false);
    }
    }
}