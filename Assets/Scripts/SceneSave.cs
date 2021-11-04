using UnityEngine;
using UnityEngine.UI;

public class SceneSave : MonoBehaviour
{
    [SerializeField]
    private Text TextScore;

    void Start()
    {
        TextScore.text = "" + GameData.MaxScore;
    }
}
