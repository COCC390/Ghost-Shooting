using UnityEngine;

namespace Konzit.GameSystem
{
    [CreateAssetMenu(fileName = "GameSystemConfigs", menuName = "Game/GameSystems/GameSystemConfigs")]
    public class GameSystemConfigs : ScriptableObject
    {
        public GameDataPathConfig DataPathConfig;
        public string DefaultCharacterInventoryID;

        public void OnValidate()
        {
            if(string.IsNullOrEmpty(DefaultCharacterInventoryID))
                DefaultCharacterInventoryID = "character_inv";
        }
    }
}
