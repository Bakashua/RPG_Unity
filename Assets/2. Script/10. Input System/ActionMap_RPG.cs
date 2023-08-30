// GENERATED AUTOMATICALLY FROM 'Assets/ActionMap_RPG.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ActionMap_RPG : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ActionMap_RPG()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ActionMap_RPG"",
    ""maps"": [
        {
            ""name"": ""UI_Combat"",
            ""id"": ""c04c5d50-6ef6-467c-b695-71571526fafb"",
            ""actions"": [
                {
                    ""name"": ""North BTN"",
                    ""type"": ""PassThrough"",
                    ""id"": ""febe34dc-0464-4910-a31d-4a14b19bb2a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""West BTN"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c5fabbc4-2ed7-4ada-98ab-e4b95658656e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""East BTN"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7c569fc8-566e-466e-8823-6843c3990841"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""South BTN"",
                    ""type"": ""PassThrough"",
                    ""id"": ""17dd404c-fbe0-4547-b330-4018ceef9bc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spe_L"",
                    ""type"": ""Button"",
                    ""id"": ""ef4c2d2f-03bf-4fba-9f89-b71d06f313e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spe_R"",
                    ""type"": ""Button"",
                    ""id"": ""bab41668-befb-4f88-ba2f-57be236b0ed5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch_GiveTurn"",
                    ""type"": ""Button"",
                    ""id"": ""854c3195-84e0-405d-910f-51f5c2bd2b94"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch_Change_Character"",
                    ""type"": ""Button"",
                    ""id"": ""8a897860-25ae-4297-8f96-f43b42a86051"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ultimate"",
                    ""type"": ""Button"",
                    ""id"": ""013c4c2c-76b8-4bed-a3fe-1c13ab60a8aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause_Menu"",
                    ""type"": ""Button"",
                    ""id"": ""d3dfa21c-b7fc-430e-916e-df4764f0c4d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scan_Enemy"",
                    ""type"": ""Button"",
                    ""id"": ""9ab4fc38-968a-4983-950a-3677c1a610fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6d4bd58e-82d8-46e3-9898-5bb4d056462c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""North BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d51bb572-9b5f-4461-8574-c63dabd337aa"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""North BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10a91bf7-5462-468d-b5f2-6b07f71d41d5"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""North BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4d01053-5b8d-4a62-b319-1fd360467edc"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""West BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d5b0664-6945-4046-9991-9d0e65f741d4"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""West BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08a22887-6d18-4096-ba7e-d75122e60943"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""West BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3251aad3-6944-4a87-9eec-a9cabce8d87e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""East BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25f9c5dd-6479-45a0-b72b-642eaffda0fb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""East BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34517797-eb63-4fbf-b42f-2397c45450b8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""East BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07078bb4-fa12-403c-9497-5a7047179bf4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""South BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d801ac12-8e9d-4eb7-8faa-035cdf3fe45f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""South BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6efbb931-7d13-4d46-9b3d-ea010d509cf9"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""South BTN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbfafe4a-83e0-40a5-8eb7-4dc0cdbd629e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spe_L"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8afa5495-1f13-4592-855a-cf689a140ec3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spe_L"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6dc289b6-e6a0-4a2c-a572-def0b1b29e3b"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spe_R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""074bb898-5d90-4203-aa5d-efe538673df2"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spe_R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc861b48-68f1-4a52-9ae5-7159edc03c96"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch_GiveTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9f49fcc-b547-4c26-9ec7-8736d83e8a1d"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": ""Hold(duration=0.8)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch_Change_Character"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e40d8602-0539-4d07-8368-e746ea691770"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Hold(duration=0.4)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ultimate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6162ca63-bbdd-46dc-82e8-5117fb8bbe10"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause_Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8817c337-5796-4d96-a3b0-7bc8dbc0808e"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scan_Enemy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""55e8662b-b3d1-485e-8b6f-4c23304a3fc7"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""7983a850-6dba-4169-a287-26630f05724d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""4dff886a-0780-4317-979a-58f4c3555236"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""da299b23-9437-4e67-bb60-657bcfa04148"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""edb61ca5-b165-4135-aabc-afdcbc21a3b3"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""2ab13ebc-43b7-40a3-8848-afb2fe93ff0b"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""97eb6f43-824e-487a-a105-e6146c9cb8bc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7f15a4ff-b876-4e5e-a863-aed5b7a58b59"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""346df4ce-5ad0-4c55-ac7c-a198f1363874"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""da01037a-50fc-43ef-b220-c90be5b6864b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""02c5e521-8b6d-4aef-84f9-ca0a504568be"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5ff711c8-e228-4fcb-94fa-eb060869a270"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""468c078e-c09a-4583-bf6f-5561724bc6a4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""260bce06-511a-4de7-be11-74f0c05fde66"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5901a4a9-fcf6-4828-82e3-c47392983919"",
                    ""path"": ""<XRController>/{Primary2DAxis}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a844b7b-2af2-464e-a5d1-9353d938b9d6"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1ddb9c5-40b8-43a0-b42e-c648e7f7bc2c"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""792c3afd-8394-483d-adc1-f2f23da2c615"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse;Touch"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""663d89de-38ad-4c7a-80eb-95070e4b0412"",
                    ""path"": ""<Joystick>/{Hatswitch}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59a13b8c-84cf-4abd-91e7-63d70a1a5baf"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea530d9d-7990-4475-8247-b19365c807e9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16f97318-ccbd-418d-a52f-55373645830d"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b737060-f7cf-45b0-9679-e3ff75337983"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d49d1b2-8ae9-4904-99cd-6bd9315afba2"",
                    ""path"": ""<XRController>/{PrimaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI_Gen"",
            ""id"": ""88b22845-8817-4532-8a89-79b795d434d8"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f66b1c64-2653-4512-b59d-bd3ad9b1e98e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""b7ae46a4-f532-4cde-987b-f4e17c713f64"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""f2927f32-48e8-43ca-afeb-8afc18321c5d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6bbe43d7-9a89-43de-b6df-aab1fb063de3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""136b47ee-7bac-4127-8230-c48774bbf5eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""00b837fd-efe7-4445-9908-ef734289b495"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""afda2989-522e-462d-9c42-68df8ab9759b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""32f32d54-1a77-4096-943b-6494a155b152"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDevicePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""280768ca-10c2-4f0e-bfe7-828884d34ddd"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDeviceOrientation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6efa8365-eca1-4f03-b411-3f02d9feb383"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""b235cd97-7682-4cc9-8426-e19c31040aa1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""949c928b-ad68-4dd8-aec6-860f779f02c7"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6a7f4369-15b4-46a8-9e00-69689e976132"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""74646d60-7eb5-4149-a3fa-748f16f1d277"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""489de5cc-f1a8-4a19-b175-cbb49ae633fa"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d6337e94-a62b-4530-953c-abc044c9d8dc"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0b918fc6-3e57-4196-8456-57facefb8ebe"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""28da8497-a2c6-4e51-be47-c6d79a0ded44"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5334ff18-6ae5-4bfb-90c8-45270bc2f3fa"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a576ee95-fca2-4730-87eb-30b9e6bf3d81"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""06052081-9bdf-44b2-86e7-598dd62bb189"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5a2c22df-9a7b-44b8-8fb7-fcbe127a4db2"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""44065317-b19f-4221-bf1f-b2926f79eb6d"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6d29bab7-961e-4467-8188-98807c7fe85c"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d16101bd-a4bb-4ca3-a3cf-25a27fc94977"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""63cd635a-38d3-43b3-b559-abf9ff54f385"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""72a96775-87a1-4d70-8348-280fd9a7bb46"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e7496b6b-bf67-4c00-bc07-78073935e9ea"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6a000021-c60e-4a07-98f5-0c3d615fe856"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""66a0d6ae-f2df-47b8-852f-08f6cdd1ef5b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""001dcc03-ea9f-40c3-9db4-8963bc87ee9b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4a974ee6-3990-4e0e-9904-abf72c7eaf74"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""edf4207a-355b-4d7b-bb52-76b5a5150fba"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b8fbe5e4-4359-43fd-be31-45481b913b10"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""91661ed4-f4a2-4de1-ab01-054d4a53a2a5"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba6f6f03-a2ff-4f55-b9f8-2b28e7a1346d"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d6d643f-6a62-4032-8c8f-00921f33915b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba63e1c2-2b9d-4ce4-bf4a-924b9fbc6752"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da7c0f38-afb1-450a-9672-85b537a62861"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41e4404b-ba01-4af1-91ff-5c91393e2bac"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88b0b388-9960-4c5b-82b9-addebbbac865"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2e1d94e-a34f-4a83-9890-f16ac23e79ba"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f2f5aa0-f7fe-4818-b085-8a6f95a06612"",
                    ""path"": ""<XRController>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80a550a1-e013-4814-a247-a2598b49726c"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59e3db06-566e-4a6e-a830-21c0c0630de2"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88f43fd3-4184-4fe4-bfc7-d302f30564cd"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13f2abbe-ae35-4336-bb56-5140d5362983"",
                    ""path"": ""<XRController>/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDevicePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48dcb49d-70ca-4abb-a7c3-0653208014d9"",
                    ""path"": ""<XRController>/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDeviceOrientation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UI_Combat
        m_UI_Combat = asset.FindActionMap("UI_Combat", throwIfNotFound: true);
        m_UI_Combat_NorthBTN = m_UI_Combat.FindAction("North BTN", throwIfNotFound: true);
        m_UI_Combat_WestBTN = m_UI_Combat.FindAction("West BTN", throwIfNotFound: true);
        m_UI_Combat_EastBTN = m_UI_Combat.FindAction("East BTN", throwIfNotFound: true);
        m_UI_Combat_SouthBTN = m_UI_Combat.FindAction("South BTN", throwIfNotFound: true);
        m_UI_Combat_Spe_L = m_UI_Combat.FindAction("Spe_L", throwIfNotFound: true);
        m_UI_Combat_Spe_R = m_UI_Combat.FindAction("Spe_R", throwIfNotFound: true);
        m_UI_Combat_Switch_GiveTurn = m_UI_Combat.FindAction("Switch_GiveTurn", throwIfNotFound: true);
        m_UI_Combat_Switch_Change_Character = m_UI_Combat.FindAction("Switch_Change_Character", throwIfNotFound: true);
        m_UI_Combat_Ultimate = m_UI_Combat.FindAction("Ultimate", throwIfNotFound: true);
        m_UI_Combat_Pause_Menu = m_UI_Combat.FindAction("Pause_Menu", throwIfNotFound: true);
        m_UI_Combat_Scan_Enemy = m_UI_Combat.FindAction("Scan_Enemy", throwIfNotFound: true);
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
        // UI_Gen
        m_UI_Gen = asset.FindActionMap("UI_Gen", throwIfNotFound: true);
        m_UI_Gen_Navigate = m_UI_Gen.FindAction("Navigate", throwIfNotFound: true);
        m_UI_Gen_Submit = m_UI_Gen.FindAction("Submit", throwIfNotFound: true);
        m_UI_Gen_Cancel = m_UI_Gen.FindAction("Cancel", throwIfNotFound: true);
        m_UI_Gen_Point = m_UI_Gen.FindAction("Point", throwIfNotFound: true);
        m_UI_Gen_Click = m_UI_Gen.FindAction("Click", throwIfNotFound: true);
        m_UI_Gen_ScrollWheel = m_UI_Gen.FindAction("ScrollWheel", throwIfNotFound: true);
        m_UI_Gen_MiddleClick = m_UI_Gen.FindAction("MiddleClick", throwIfNotFound: true);
        m_UI_Gen_RightClick = m_UI_Gen.FindAction("RightClick", throwIfNotFound: true);
        m_UI_Gen_TrackedDevicePosition = m_UI_Gen.FindAction("TrackedDevicePosition", throwIfNotFound: true);
        m_UI_Gen_TrackedDeviceOrientation = m_UI_Gen.FindAction("TrackedDeviceOrientation", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // UI_Combat
    private readonly InputActionMap m_UI_Combat;
    private IUI_CombatActions m_UI_CombatActionsCallbackInterface;
    private readonly InputAction m_UI_Combat_NorthBTN;
    private readonly InputAction m_UI_Combat_WestBTN;
    private readonly InputAction m_UI_Combat_EastBTN;
    private readonly InputAction m_UI_Combat_SouthBTN;
    private readonly InputAction m_UI_Combat_Spe_L;
    private readonly InputAction m_UI_Combat_Spe_R;
    private readonly InputAction m_UI_Combat_Switch_GiveTurn;
    private readonly InputAction m_UI_Combat_Switch_Change_Character;
    private readonly InputAction m_UI_Combat_Ultimate;
    private readonly InputAction m_UI_Combat_Pause_Menu;
    private readonly InputAction m_UI_Combat_Scan_Enemy;
    public struct UI_CombatActions
    {
        private @ActionMap_RPG m_Wrapper;
        public UI_CombatActions(@ActionMap_RPG wrapper) { m_Wrapper = wrapper; }
        public InputAction @NorthBTN => m_Wrapper.m_UI_Combat_NorthBTN;
        public InputAction @WestBTN => m_Wrapper.m_UI_Combat_WestBTN;
        public InputAction @EastBTN => m_Wrapper.m_UI_Combat_EastBTN;
        public InputAction @SouthBTN => m_Wrapper.m_UI_Combat_SouthBTN;
        public InputAction @Spe_L => m_Wrapper.m_UI_Combat_Spe_L;
        public InputAction @Spe_R => m_Wrapper.m_UI_Combat_Spe_R;
        public InputAction @Switch_GiveTurn => m_Wrapper.m_UI_Combat_Switch_GiveTurn;
        public InputAction @Switch_Change_Character => m_Wrapper.m_UI_Combat_Switch_Change_Character;
        public InputAction @Ultimate => m_Wrapper.m_UI_Combat_Ultimate;
        public InputAction @Pause_Menu => m_Wrapper.m_UI_Combat_Pause_Menu;
        public InputAction @Scan_Enemy => m_Wrapper.m_UI_Combat_Scan_Enemy;
        public InputActionMap Get() { return m_Wrapper.m_UI_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UI_CombatActions set) { return set.Get(); }
        public void SetCallbacks(IUI_CombatActions instance)
        {
            if (m_Wrapper.m_UI_CombatActionsCallbackInterface != null)
            {
                @NorthBTN.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnNorthBTN;
                @NorthBTN.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnNorthBTN;
                @NorthBTN.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnNorthBTN;
                @WestBTN.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnWestBTN;
                @WestBTN.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnWestBTN;
                @WestBTN.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnWestBTN;
                @EastBTN.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnEastBTN;
                @EastBTN.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnEastBTN;
                @EastBTN.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnEastBTN;
                @SouthBTN.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSouthBTN;
                @SouthBTN.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSouthBTN;
                @SouthBTN.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSouthBTN;
                @Spe_L.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSpe_L;
                @Spe_L.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSpe_L;
                @Spe_L.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSpe_L;
                @Spe_R.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSpe_R;
                @Spe_R.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSpe_R;
                @Spe_R.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSpe_R;
                @Switch_GiveTurn.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSwitch_GiveTurn;
                @Switch_GiveTurn.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSwitch_GiveTurn;
                @Switch_GiveTurn.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSwitch_GiveTurn;
                @Switch_Change_Character.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSwitch_Change_Character;
                @Switch_Change_Character.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSwitch_Change_Character;
                @Switch_Change_Character.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnSwitch_Change_Character;
                @Ultimate.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnUltimate;
                @Ultimate.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnUltimate;
                @Ultimate.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnUltimate;
                @Pause_Menu.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnPause_Menu;
                @Pause_Menu.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnPause_Menu;
                @Pause_Menu.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnPause_Menu;
                @Scan_Enemy.started -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnScan_Enemy;
                @Scan_Enemy.performed -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnScan_Enemy;
                @Scan_Enemy.canceled -= m_Wrapper.m_UI_CombatActionsCallbackInterface.OnScan_Enemy;
            }
            m_Wrapper.m_UI_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @NorthBTN.started += instance.OnNorthBTN;
                @NorthBTN.performed += instance.OnNorthBTN;
                @NorthBTN.canceled += instance.OnNorthBTN;
                @WestBTN.started += instance.OnWestBTN;
                @WestBTN.performed += instance.OnWestBTN;
                @WestBTN.canceled += instance.OnWestBTN;
                @EastBTN.started += instance.OnEastBTN;
                @EastBTN.performed += instance.OnEastBTN;
                @EastBTN.canceled += instance.OnEastBTN;
                @SouthBTN.started += instance.OnSouthBTN;
                @SouthBTN.performed += instance.OnSouthBTN;
                @SouthBTN.canceled += instance.OnSouthBTN;
                @Spe_L.started += instance.OnSpe_L;
                @Spe_L.performed += instance.OnSpe_L;
                @Spe_L.canceled += instance.OnSpe_L;
                @Spe_R.started += instance.OnSpe_R;
                @Spe_R.performed += instance.OnSpe_R;
                @Spe_R.canceled += instance.OnSpe_R;
                @Switch_GiveTurn.started += instance.OnSwitch_GiveTurn;
                @Switch_GiveTurn.performed += instance.OnSwitch_GiveTurn;
                @Switch_GiveTurn.canceled += instance.OnSwitch_GiveTurn;
                @Switch_Change_Character.started += instance.OnSwitch_Change_Character;
                @Switch_Change_Character.performed += instance.OnSwitch_Change_Character;
                @Switch_Change_Character.canceled += instance.OnSwitch_Change_Character;
                @Ultimate.started += instance.OnUltimate;
                @Ultimate.performed += instance.OnUltimate;
                @Ultimate.canceled += instance.OnUltimate;
                @Pause_Menu.started += instance.OnPause_Menu;
                @Pause_Menu.performed += instance.OnPause_Menu;
                @Pause_Menu.canceled += instance.OnPause_Menu;
                @Scan_Enemy.started += instance.OnScan_Enemy;
                @Scan_Enemy.performed += instance.OnScan_Enemy;
                @Scan_Enemy.canceled += instance.OnScan_Enemy;
            }
        }
    }
    public UI_CombatActions @UI_Combat => new UI_CombatActions(this);

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Fire;
    public struct PlayerActions
    {
        private @ActionMap_RPG m_Wrapper;
        public PlayerActions(@ActionMap_RPG wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Fire => m_Wrapper.m_Player_Fire;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Fire.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI_Gen
    private readonly InputActionMap m_UI_Gen;
    private IUI_GenActions m_UI_GenActionsCallbackInterface;
    private readonly InputAction m_UI_Gen_Navigate;
    private readonly InputAction m_UI_Gen_Submit;
    private readonly InputAction m_UI_Gen_Cancel;
    private readonly InputAction m_UI_Gen_Point;
    private readonly InputAction m_UI_Gen_Click;
    private readonly InputAction m_UI_Gen_ScrollWheel;
    private readonly InputAction m_UI_Gen_MiddleClick;
    private readonly InputAction m_UI_Gen_RightClick;
    private readonly InputAction m_UI_Gen_TrackedDevicePosition;
    private readonly InputAction m_UI_Gen_TrackedDeviceOrientation;
    public struct UI_GenActions
    {
        private @ActionMap_RPG m_Wrapper;
        public UI_GenActions(@ActionMap_RPG wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_UI_Gen_Navigate;
        public InputAction @Submit => m_Wrapper.m_UI_Gen_Submit;
        public InputAction @Cancel => m_Wrapper.m_UI_Gen_Cancel;
        public InputAction @Point => m_Wrapper.m_UI_Gen_Point;
        public InputAction @Click => m_Wrapper.m_UI_Gen_Click;
        public InputAction @ScrollWheel => m_Wrapper.m_UI_Gen_ScrollWheel;
        public InputAction @MiddleClick => m_Wrapper.m_UI_Gen_MiddleClick;
        public InputAction @RightClick => m_Wrapper.m_UI_Gen_RightClick;
        public InputAction @TrackedDevicePosition => m_Wrapper.m_UI_Gen_TrackedDevicePosition;
        public InputAction @TrackedDeviceOrientation => m_Wrapper.m_UI_Gen_TrackedDeviceOrientation;
        public InputActionMap Get() { return m_Wrapper.m_UI_Gen; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UI_GenActions set) { return set.Get(); }
        public void SetCallbacks(IUI_GenActions instance)
        {
            if (m_Wrapper.m_UI_GenActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnNavigate;
                @Submit.started -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnSubmit;
                @Cancel.started -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnCancel;
                @Point.started -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnPoint;
                @Click.started -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnClick;
                @ScrollWheel.started -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnScrollWheel;
                @MiddleClick.started -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.performed -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.canceled -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnMiddleClick;
                @RightClick.started -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnRightClick;
                @TrackedDevicePosition.started -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled -= m_Wrapper.m_UI_GenActionsCallbackInterface.OnTrackedDeviceOrientation;
            }
            m_Wrapper.m_UI_GenActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @MiddleClick.started += instance.OnMiddleClick;
                @MiddleClick.performed += instance.OnMiddleClick;
                @MiddleClick.canceled += instance.OnMiddleClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
            }
        }
    }
    public UI_GenActions @UI_Gen => new UI_GenActions(this);
    public interface IUI_CombatActions
    {
        void OnNorthBTN(InputAction.CallbackContext context);
        void OnWestBTN(InputAction.CallbackContext context);
        void OnEastBTN(InputAction.CallbackContext context);
        void OnSouthBTN(InputAction.CallbackContext context);
        void OnSpe_L(InputAction.CallbackContext context);
        void OnSpe_R(InputAction.CallbackContext context);
        void OnSwitch_GiveTurn(InputAction.CallbackContext context);
        void OnSwitch_Change_Character(InputAction.CallbackContext context);
        void OnUltimate(InputAction.CallbackContext context);
        void OnPause_Menu(InputAction.CallbackContext context);
        void OnScan_Enemy(InputAction.CallbackContext context);
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
    }
    public interface IUI_GenActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnMiddleClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnTrackedDevicePosition(InputAction.CallbackContext context);
        void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
    }
}
