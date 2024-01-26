using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("_scn_gameplay", LoadSceneMode.Single);
    }
}