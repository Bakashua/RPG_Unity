using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMapGenerator : MonoBehaviour
{

    /*
     *  1 on prend le script de map
     *  2 on genere des points
     * 
     */

    public SO_DungeonMapConfig MapConfig;
    public GameObject Parent;
    public GameObject Node;

    private void Start()
    {
        GenerateMap();
    }


    public void GenerateMap()
    {
        for (int f = 0; f < MapConfig.NumberOfprebossNode; f++)
        {
            for (int i = 0; i < MapConfig.NumberOfStartingNode; i++)
            {
                GenerateButton(i, f);
            }
        }
    }

    void GenerateButton(int i, int f)
    {
        float randomVarx = Random.Range(1, 10);
        float randomVary = Random.Range(1, 10);
        Vector3 newPos = new Vector3(Parent.transform.position.x + (f * 200 + randomVarx), Parent.transform.position.y + (i * 300 + randomVary), Parent.transform.position.z);
        GameObject obj = Instantiate(Node, newPos, Parent.transform.rotation);

        obj.transform.parent = Parent.transform;

        DungeonNodeObj newnode = obj.GetComponent<DungeonNodeObj>();
        int t = Random.Range(0, MapConfig.Nodes.Count);
        newnode.Type = GiveNodeType(t);
        newnode.Encounter = GiveEmcounterGroup(newnode.Type);


    }

    NodeType GiveNodeType(int i)
    {
        return MapConfig.Nodes[i].node;
        // should apply according to map config
        ////switch (i)
        ////{
        ////    case 0:
        ////        return NodeType.MinorEnemy;

        ////    case 1:
        ////        return NodeType.Boss;

        ////    case 2:
        ////        return NodeType.EliteEnemy;

        ////    case 3:
        ////        return NodeType.Mystery;

        ////    case 4:
        ////        return NodeType.RestSite;

        ////    case 5:
        ////        return NodeType.Store;

        ////    case 6:
        ////        return NodeType.Treasure;
        ////}
        ////return NodeType.MinorEnemy;
    }

    // should apply accord to map config
    SO_Encounter GiveEmcounterGroup(NodeType nt)
    {
        switch (nt)
        {
            case (NodeType.Boss):
                int random = Random.Range(0, MapConfig.Encounters_Boss.Count);
                return MapConfig.Encounters_Boss[random];

            case (NodeType.EliteEnemy):
                int random1 = Random.Range(0, MapConfig.Encounters_Elite.Count);
                return MapConfig.Encounters_Elite[random1];

            case (NodeType.MinorEnemy):
                int random2 = Random.Range(0, MapConfig.Encounters_Normal.Count);
                return MapConfig.Encounters_Normal[random2];
        }

        return MapConfig.Encounters_Normal[0];
    }


}
