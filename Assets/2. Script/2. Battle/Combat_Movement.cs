using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Combat_Movement : MonoBehaviour, ISelectHandler
{
    [Header("CLASS")]
    // is assigned on character spawner
    public LevelDesignManager LDM;
    public Player_Camera_Combat playerCam;

    public Material Reset;
    public Material Walkable;
    public Material UnWalkable;
    public GameObject currTiles;

    public List<GameObject> AdjacentTiles = new List<GameObject>();
    List<GameObject> BtnToClear = new List<GameObject>();

    [Header("UI")]
    public GameObject AutoSelect;
    public GameObject TileBtn;
    public GameObject BtnParent;

    private void Start()
    {
        //GetAdjacentCells();
        LDM = LevelDesignManager.LDM;
    }

    public void PlayerMovement()
    {
        Battle_GUI_Manager.instance_BUIM.targetButton_Cancel.SetActive(true);
        BattleCamManager.instance_BCam.Activate_TacticalCam();
        //BattleCamManager.instance_BCam.Setup_TacticCam();
        UI_PlayerTurn.instance_PTM.DesactivateSpellBtn();
        Invoke("GetAdjacentCells", 0.5f);
        Invoke("CreateTileBtn", 0.75f);
    }

    // invoked in playermovement
    void GetAdjacentCells()
    {
        Combat_Tiles tileData = currTiles.GetComponent<Combat_Tiles>();

        foreach (GameObject tile in LDM.TilesAllied)
        {
            // Get the coordinates of the current cell
            int x = tile.GetComponent<Combat_Tiles>().x;
            int z = tile.GetComponent<Combat_Tiles>().z;

            // Check if the current cell is the specific cell
            if (x == tileData.x && z == tileData.z)
            {
                // The current cell is the specific cell, so skip it
                continue;
            }

            // Check if the current cell is adjacent to the specific cell
            //if (tileData.x - x <= 1 && tileData.z - z <= 1 && tileData.Walkable)
            if (tileData.x - x <= 1 && tileData.x - x >= -1
                && tileData.z - z <= 1 && tileData.z - z >= -1
                && tileData.Walkable == true)
            {
                tile.GetComponent<MeshRenderer>().material = Walkable;
                // The current cell is adjacent to the specific cell
                AdjacentTiles.Add(tile); // add the cell to the list of adjacent cells
            }
        }
    }

    // invoked in playermovement
    void CreateTileBtn()
    {
        BtnParent.SetActive(true);

        GameObject autoBtn = Instantiate(AutoSelect, BtnParent.transform, false);
        //clear_autoSelect = newButton2;
        BtnToClear.Add(autoBtn);

        foreach (GameObject tile in AdjacentTiles)
        {
            // Create a new button for the current cell
            GameObject button = Instantiate(TileBtn, BtnParent.transform);
            BtnToClear.Add(button);

            Ui_Combat_TileMove dataBtn = button.GetComponent<Ui_Combat_TileMove>();
            dataBtn.Tile = tile;

            // Get the Button component for the button
            Button btn = button.GetComponent<Button>();

            // Set the text of the button to the coordinates of the cell
            button.GetComponentInChildren<TextMeshProUGUI>().text = "(" + tile.GetComponent<Combat_Tiles>().x + ", " + tile.GetComponent<Combat_Tiles>().z + ")";

            // Add a click event handler to the button
            btn.onClick.AddListener(() => OnButtonClick(tile));
            //btn.onSelect.AddListener(OnSelect);
        }
    }

    // Event handler for when a button is clicked
    void OnButtonClick(GameObject tile)
    {
        Vector3 target = tile.transform.position;
        float animSpeed = 0.7f;

        transform.DOMove(target, animSpeed).SetEase(Ease.InOutSine);
        //StartCoroutine(LerpPosition(target, animSpeed));

        UI_PlayerTurn.instance_PTM.ClearHeroToManage();
        FinishCharacterTurn();
        ClearTileBtn();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("button 1 selected");
    }

    // also trigger on even cancel move in UI_PlayerTurn
    public void ClearTileBtn()
    {
        BtnParent.SetActive(false);
        foreach (GameObject tile in AdjacentTiles)
        {
            tile.GetComponent<MeshRenderer>().material = Reset;
        }
        AdjacentTiles.Clear();

        foreach (GameObject item in BtnToClear)
        {
            Destroy(item);
        }
    }

    void FinishCharacterTurn()
    {
        // reset cam
        BattleCamManager.instance_BCam.ResetPlayerCamera();
        BattleCamManager.instance_BCam.Desactivate_TacticalCam();
        playerCam.Desactivate_cam();
        //BattleCamManager.instance_BCam.Setup_TacticCam();

        GameObject character = BattleStateMachine.instance_BSM.herosToManage[0];

        character.GetComponent<HeroStateMachine>().curentCooldown = 0;
        character.GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.PROCESSING;

        character.GetComponent<HeroStateMachine>().STATEMACHINE();

        BattleStateMachine.instance_BSM.herosToManage.RemoveAt(0);
    }

    
}
