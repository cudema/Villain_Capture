using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData player
    {
        private set; get;
    }

    private void Awake()
    {
        if (player == null)
        {
            player = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
