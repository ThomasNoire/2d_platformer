using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSet : MonoBehaviour
{
    private void Update()
    {
        Menu();
    }

    private void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name.Equals("Sc2"))
            {
                SceneManager.LoadScene("Sc1");
            }
            else
            {
                SceneManager.LoadScene("Sc2");
            }
        }
    }
}