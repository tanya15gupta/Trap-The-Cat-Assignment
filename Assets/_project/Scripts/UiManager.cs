using UnityEngine;

namespace TrapTheCat.Ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerWin;
        [SerializeField] private GameObject catWin;

        public void Init()
        {
            playerWin.SetActive(false);
            catWin.SetActive(false);
        }

        public void ActivatePlayerWin()
        {
            playerWin.SetActive(true);
        }

        public void ActivateCatWin()
        {
            catWin.SetActive(true);
        }
    }
}