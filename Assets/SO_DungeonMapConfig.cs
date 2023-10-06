using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Dungeon Map", menuName = "My Game/Dungeon/New DungeonMap")]
public class SO_DungeonMapConfig : ScriptableObject
{
    [Header("Basic")]

    public GameObject DungeonNode;
    [Range(0f, 30f)] public int NumberOfprebossNode = 5;
    [Range(0f, 10f)] public int NumberOfStartingNode = 2;
    [Range(0f, 10f)] public int NumberOfLevels = 1;


    [Header("Nodes")]
    public List<DungeonNode> Nodes = new();


    [Header("Dungon specific")]
    public List<SO_Encounter> Encounters_Normal = new();
    public List<SO_Encounter> Encounters_Elite = new();
    public List<SO_Encounter> Encounters_Boss = new();
    public List<string> Mistery = new();
    public string Chest;
    public string Shop;
    public string Rest;


}

[System.Serializable]
public class DungeonNode
{
    [Tooltip("Default node for this map layer. If Randomize Nodes is 0, you will get this node 100% of the time")]
    public NodeType node;
    public float DistanceFromPreviousNode = 1;
    public float DistanceVariance = 1;[Tooltip("If this is set to 0, nodes on this layer will appear in a straight line. Closer to 1f = more position randomization")]
    [Range(0f, 1f)] public float randomizePosition;
    [Tooltip("Chance to get a random node that is different from the default node on this layer")]
    [Range(0f, 1f)] public float randomizeNodes;

}
public enum NodeType
{
    MinorEnemy,
    EliteEnemy,
    Boss,
    RestSite,
    Treasure,
    Store,
    Mystery
}