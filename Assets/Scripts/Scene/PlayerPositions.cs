using UnityEngine;

public class PlayerPositions : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GetComponent<Transform>();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SavePosition();
        }
        else
        {
            LoadPosition();
        }
    }

    private void SavePosition()
    {
        PlayerPrefs.SetFloat("playerX", player.position.x);
        PlayerPrefs.SetFloat("playerY", player.position.y);
    }

    private void LoadPosition()
    {
        player.position = new Vector2(
            PlayerPrefs.GetFloat("playerX", player.position.x),
            PlayerPrefs.GetFloat("playerY", player.position.y));
    }
}
