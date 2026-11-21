using System;
using System.Collections.Generic;
using UnityEngine;

namespace Konzit.GameSystem
{
    [CreateAssetMenu(fileName = "GameDataPathConfig", menuName = "Game/GameSystems/Data/GameDataPathConfig")]
    public class GameDataPathConfig : ScriptableObject
    {
        public string FolderName;
        public List<GameDataPathDefinition> AllDefinition;
    }

}
