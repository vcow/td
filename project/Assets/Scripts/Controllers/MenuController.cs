using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class MenuController : MonoBehaviour
    {
        private void OnDestroy()
        {
            foreach (var button in GetComponentsInChildren<Button>())
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}