using Cinemachine;
using System.Linq;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private Blackscreen _blackScreen;
    [SerializeField] private ShopContent _shopContent;
    [SerializeField] private Transform _playerSpawnPoint;

    [Space(8)]
    [SerializeField] private CinemachineVirtualCamera _mainCmCamera;

    private void Awake()
    {
        Movement _playerToSpawn = _shopContent.CharacterSkinItems
            .ElementAt(PlayerPrefs.GetInt(ShopPrefsNames.SelectedCharacterID)).Player;

        Movement _newPlayer = Instantiate(_playerToSpawn, _playerSpawnPoint.transform.position, Quaternion.identity);

        _mainCmCamera.Follow = _newPlayer.transform;
        _mainCmCamera.LookAt = _newPlayer.transform;

        GameplaySpawner _roadSpawner = FindObjectOfType<GameplaySpawner>();
        _roadSpawner.Init(_newPlayer.GetComponent<Player>());

        _blackScreen.CloseBlackScreen();
    }
}