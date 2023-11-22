using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    #region --------- Serialized Variables ---------
    [SerializeField] private int buttonIndex;
    #endregion ------------------

    #region --------- Monobehavior Methods ---------
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(AddScene);
    }
    #endregion ------------------

    #region --------- Private Methods ---------
    private void AddScene()
    {
        GameManager.Instance.LoadScene(buttonIndex);
    }
    #endregion ------------------
}
