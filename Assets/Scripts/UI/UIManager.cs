using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class UIManager : MonoBehaviour
{
    public static UIManager me;

    [SerializeField] List<GameObject> menus = new List<GameObject>();
    [SerializeField] Dropdown territoryTypeDropdown;
    [SerializeField] InputField newModNameInputField;
    [SerializeField] InputField openModNameInputField;
    [SerializeField] Text modNameText;


    [SerializeField] Menu _currentMenu = Menu.NewMod;
    ShipType shipType;
    FactionType factionType;

    [SerializeField] public PrefabMenu prefabMenu = new PrefabMenu();
    [SerializeField] public SpawnGroupMenu spawnGroupMenu = new SpawnGroupMenu();
    [SerializeField] public TerritoryMenu territoryMenu = new TerritoryMenu();
    [SerializeField] public DescriptionMenu descriptionMenu = new DescriptionMenu(); 


    //const string ME = "Modular Encounters ";


    #region enums
    public enum Menu
    {
        Main = 0,
        NewMod,
        OpenMod,
        Mod,
        NewPrefab,
        NewShip,
        NewShip2,
        SpaceCargoShip,
        PlanetaryInstallation,
        SpaceRandomEncounter,// 10 = 9
        Faction,
        PlanetRequirements,
        ThreatLevel,
        PCU,
        PlayerCount,
        PlayerCredits,
        PlayerReputation,
        ModStorageComponent,
        KnownPlayerLocation,
        Territory,// 20 = 19
        Mix,
        Mix2,
        NewTerritory,
        PlanetaryTerritory,
        NewTerritory2 = 24,
    }

    public enum ShipType
    {
        SpaceCargoShip = 0,
        LunarCargoShip,
        AtmosphericCargoShip,
        PlanetaryInstallation,
        RandomSpaceEncounter,
    }

    public enum FactionType
    {
        Custom = 0,
        RandomMiner,
        RandomtBuilder,
        RadomTrader,
    }

    public enum TerritoryType
    {
        Static = 0,
        Planetary,
    }
    #endregion


    private void Awake()
    {
        me = this;

        Logging.LogWrn($"Starting menu is set to  Menu.{_currentMenu}");
        UpdateUI();

        XMLHandler.Test();
    }


    #region MenuFunctions
    public void NextMenu()
    {
        switch (_currentMenu)
        {
            case Menu.NewShip:
                _currentMenu = Menu.NewShip2;
                break;

            case Menu.SpaceCargoShip:
                _currentMenu = Menu.Faction;
                break;

            case Menu.PlanetaryInstallation:
                _currentMenu = Menu.Faction;
                break;

            case Menu.SpaceRandomEncounter:
                _currentMenu = Menu.Faction;
                break;

            case Menu.PlanetRequirements:
                _currentMenu = Menu.ThreatLevel;
                break;

            case Menu.ThreatLevel:
                _currentMenu = Menu.PCU;
                break;

            case Menu.PCU:
                _currentMenu = Menu.PlayerCount;
                break;

            case Menu.PlayerCount:
                _currentMenu = Menu.PlayerCredits;
                break;

            case Menu.PlayerCredits:
                _currentMenu = Menu.PlayerReputation;
                break;

            case Menu.PlayerReputation:
                _currentMenu = Menu.ModStorageComponent;
                break;

            case Menu.ModStorageComponent:
                _currentMenu = Menu.KnownPlayerLocation;
                break;

            case Menu.KnownPlayerLocation:
                _currentMenu = Menu.Territory;
                break;

            case Menu.Territory:
                _currentMenu = Menu.Mix;
                break;

            case Menu.Mix:
                _currentMenu = Menu.Mix2;
                break;

            case Menu.NewTerritory:
                if (territoryTypeDropdown.value == 0)
                {
                    _currentMenu = Menu.NewTerritory2;
                }
                else if (territoryTypeDropdown.value == 1)
                {
                    _currentMenu = Menu.PlanetaryTerritory;
                }
                break;

            case Menu.PlanetaryTerritory:
                _currentMenu = Menu.NewTerritory2;
                break;
        }

        UpdateUI();
    }

    public void LastMenu()
    {
        switch(_currentMenu)
        {
            case Menu.OpenMod:
                _currentMenu = Menu.Main;
                break;

            case Menu.NewMod:
                _currentMenu = Menu.Main;
                break;

            case Menu.NewPrefab:
                _currentMenu = Menu.Mod;
                ModMenuManager.me.EditingFlag = false;
                break;

            case Menu.NewShip:
                _currentMenu = Menu.Mod;
                break;

            case Menu.NewShip2:
                _currentMenu = Menu.NewShip;
                break;

            case Menu.SpaceCargoShip:
                _currentMenu = Menu.NewShip2;
                break;

            case Menu.PlanetaryInstallation:
                _currentMenu = Menu.NewShip2;
                break;

            case Menu.SpaceRandomEncounter:
                _currentMenu = Menu.NewShip2;
                break;

            case Menu.Faction:
                switch(shipType)
                {
                    case ShipType.SpaceCargoShip:
                        _currentMenu = Menu.SpaceCargoShip;
                        break;

                    case ShipType.PlanetaryInstallation:
                        _currentMenu = Menu.PlanetaryInstallation;
                        break;

                    case ShipType.RandomSpaceEncounter:
                        _currentMenu = Menu.SpaceRandomEncounter;
                        break;

                    default:
                        _currentMenu = Menu.NewShip2;
                        break;
                }
                break;

            case Menu.PlanetRequirements:
                _currentMenu = Menu.Faction;
                break;

            case Menu.ThreatLevel:
                _currentMenu = Menu.PlanetRequirements;
                break;

            case Menu.PCU:
                _currentMenu = Menu.ThreatLevel;
                break;

            case Menu.PlayerCount:
                _currentMenu = Menu.PCU;
                break;

            case Menu.PlayerCredits:
                _currentMenu = Menu.PlayerCount;
                break;

            case Menu.PlayerReputation:
                _currentMenu = Menu.PlayerCredits;
                break;

            case Menu.ModStorageComponent:
                _currentMenu = Menu.PlayerReputation;
                break;

            case Menu.KnownPlayerLocation:
                _currentMenu = Menu.ModStorageComponent;
                break;

            case Menu.Territory:
                _currentMenu = Menu.KnownPlayerLocation;
                break;

            case Menu.Mix:
                _currentMenu = Menu.Territory;
                break;

            case Menu.Mix2:
                _currentMenu = Menu.Mix;
                break;

            case Menu.NewTerritory:
                _currentMenu = Menu.Mod;
                break;

            case Menu.PlanetaryTerritory:
                _currentMenu = Menu.NewTerritory;
                break;

            case Menu.NewTerritory2:
                if (territoryTypeDropdown.value == 0)
                    _currentMenu = Menu.NewTerritory;
                else
                    _currentMenu = Menu.PlanetaryTerritory;
                break;
        }

        UpdateUI();
    }

    public void CreateButton()
    {
        switch (_currentMenu)
        {
            case Menu.NewMod:
                CreateMod();
                _currentMenu = Menu.Mod;
                break;

            case Menu.NewPrefab:
                CreatePrefab();
                _currentMenu = Menu.Mod;
                break;

            case Menu.Mix2:
                CreateSpawngroup();
                _currentMenu = Menu.Mod;
                break;

            case Menu.NewTerritory2:
                CreateTerritory();
                _currentMenu = Menu.Mod;
                break;
        }
        UpdateUI();

        ModMenuManager.me.CloseNewPanel();
        ModMenuManager.me.UpdateVisuals();

        ClearInputFields();
    }

    public void NewModMenu()
    {
        _currentMenu = Menu.NewMod;
        UpdateUI();
    }

    public void OpenModMenu()
    {
        _currentMenu = Menu.OpenMod;
        UpdateUI();
    }

    public void NewPrefabMenu()
    {
        _currentMenu = Menu.NewPrefab;
        UpdateUI();
    }

    public void NewShipMenu()
    {
        _currentMenu = Menu.NewShip;
        UpdateUI();
    }

    public void NewTerritoryMenu()
    {
        _currentMenu = Menu.NewTerritory;
        UpdateUI();
    }

    public void SetShipType(int type)
    {
        shipType = (ShipType)type;
        
        switch((ShipType)type)
        {
            case ShipType.SpaceCargoShip:
                _currentMenu = Menu.SpaceCargoShip;
                break;

            case ShipType.PlanetaryInstallation:
                _currentMenu = Menu.PlanetaryInstallation;
                break;

            case ShipType.RandomSpaceEncounter:
                _currentMenu = Menu.SpaceRandomEncounter;
                break;

            default:
                _currentMenu = Menu.Faction;
                break;
        }

        UpdateUI();
    }

    public void SetFactionType(int type)
    {
        factionType = (FactionType)type;

        _currentMenu = Menu.PlanetRequirements;
        UpdateUI();
    }

    public void Close()
    {
        if(!Save())
        {
            Logging.LogErr("Error Saving Mod");
            return;
        }

        _currentMenu = Menu.Main;
        UpdateUI();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ClearInputFields()
    {
        //prefabs
        prefabMenu.FileName.text = "";
        prefabMenu.BeaconText.text = "";
        prefabMenu.Speed.text = "";

        //territories
        territoryMenu.Name.text = "";
        territoryMenu.Type.value = 0;
        territoryMenu.Active.isOn = true;
        territoryMenu.Radius.text = "";
        territoryMenu.CoordsX.text = "";
        territoryMenu.CoordsY.text = "";
        territoryMenu.CoordsZ.text = "";
        territoryMenu.ScaleWithPlanetSize.isOn = true;
        territoryMenu.PlanetGeneratorName.text = "";
        territoryMenu.AnnounceArriveDepart.isOn = false;
        territoryMenu.CustomArriveMessage.text = "";
        territoryMenu.CustomDepartMessage.text = "";

        //spawngroups
        spawnGroupMenu.Name.text = "";
        spawnGroupMenu.Frequency.text = "";

        //description
        descriptionMenu.UseAutopilotInSpace.isOn = false;
        descriptionMenu.PlanetaryInstallationType.text = "";
        descriptionMenu.CutVoxelsAtAirtightCels.isOn = true;
        descriptionMenu.ForceStaticGrid.isOn = true;
        descriptionMenu.RemoveVoxelsIfGridRemoved.isOn = true;
        descriptionMenu.UniqueEncounter.isOn = true;
        descriptionMenu.FactionOwner.text = "";
        descriptionMenu.PlanetMinimumSize.text = "";
        descriptionMenu.PlanetMaximumSize.text = "";
        descriptionMenu.PlanetRequiresAtmo.isOn = false;
        descriptionMenu.PlanetRequiresOxygen.isOn = false;
        descriptionMenu.PlanetRequiresVacuum.isOn = false;
        descriptionMenu.UseThreatLevelCheck.isOn = false;
        descriptionMenu.ThreatLevelCheckRange.text = "";
        descriptionMenu.ThreatScoreMinimum.text = "";
        descriptionMenu.ThreatScoreMaximum.text = "";
        descriptionMenu.ThreatLevelCheckOtherNPCOwners.isOn = false;
        descriptionMenu.UsePCUCheck.isOn = false;
        descriptionMenu.PCUCheckRadius.text = "";
        descriptionMenu.PCUMinimum.text = "";
        descriptionMenu.PCUMaximum.text = "";
        descriptionMenu.UsePlayerCountCheck.isOn = false;
        descriptionMenu.PlayerCountCheckRadius.text = "";
        descriptionMenu.MinimumPlayers.text = "";
        descriptionMenu.MaximumPlayers.text = "";
        descriptionMenu.UsePlayerCredits.isOn = false;
        descriptionMenu.IncludeAllPlayersInRadius.isOn = false;
        descriptionMenu.IncludeFactionBalance.isOn = false;
        descriptionMenu.PlayerCreditCheckRadius.text = "";
        descriptionMenu.MinimumPlayerCredits.text = "";
        descriptionMenu.MaximumPlayerCredits.text = "";
        descriptionMenu.UsePlayerFactionReputation.isOn = false;
        descriptionMenu.CheckReputationAgainstOtherNPCFaction.text = "";
        descriptionMenu.PlayerReputationCheckRadius.text = "";
        descriptionMenu.MinimumReputation.text = "";
        descriptionMenu.MaximumReputation.text = "";
        descriptionMenu.AttachModComponentToGrid.isOn = false;
        descriptionMenu.StorageKey.text = "";
        descriptionMenu.StorageValue.text = "";
        descriptionMenu.UseKnownPlayerLocation.isOn = false;
        descriptionMenu.KnownPlayerLocationMustMatchFaction.isOn = false;
        descriptionMenu.KnownPlayerLocationMinSpawnedEncounters.text = "";
        descriptionMenu.KnownPlayerLocationMinSpawnedEncounters.text = "";
        descriptionMenu.TerritoryName.text = "";
        descriptionMenu.MaxDistanceFromTerritoryCenter.text = "";
        descriptionMenu.MinDistanceFromTerritoryCenter.text = "";
        descriptionMenu.ReplenishSystems.isOn = true;
        descriptionMenu.InitializeStoreBlocks.isOn = true;
        descriptionMenu.RotateFirstCockpitToForward.isOn = false;
        descriptionMenu.DisableDampners.isOn = false;
        descriptionMenu.IgnoreCleanupRules.isOn = false;
        descriptionMenu.AdminSpawnOnly.isOn = false;
        descriptionMenu.SpawnRandomCargo.isOn = false;
        descriptionMenu.ReactorsOn.isOn = true;
        descriptionMenu.PauseAutopilotAtDistance.text = "";
        descriptionMenu.MinSpawnFromWorldCenter.text = "";
        descriptionMenu.MaxSpawnFromWorldCenter.text = "";
    }
    #endregion


    #region UtilityFunctions
    public void OpenMod()
    {
        ModDataHolder.mod.Name = openModNameInputField.text;
        modNameText.text = ModDataHolder.mod.Name;

        if(!FileHandler.OpenMod())
        {
            Logging.LogErr($"Error Loading Mod {modNameText.text}");
            return;
        }

        _currentMenu = Menu.Mod;
        UpdateUI();
    }

    public void CreateMod()
    {
        ModDataHolder.mod.Name = newModNameInputField.text;
        modNameText.text = ModDataHolder.mod.Name;

        if(!FileHandler.CreateMod())
        {
            Logging.LogErr($"Error Creating Mod {modNameText.text}");
            return;
        }

        _currentMenu = Menu.Mod;
        UpdateUI();
    }

    public void CreatePrefab()
    {
        Prefab prefab = new Prefab()
        {
            SubtypeId = prefabMenu.FileName.text,
            BeaconText = prefabMenu.BeaconText.text,
        };
        double.TryParse(prefabMenu.Speed.text, out prefab.Speed);

        if(ModMenuManager.me.EditingFlag)
        {
            ModDataHolder.AllPrefabs[ModMenuManager.me.currentlySelected.GetComponent<PrefabRef>().MyPrefabId] = prefab;
        }
        else
        {
            ModDataHolder.AllPrefabs.Add(prefab);
        }
    }

    public void CreateSpawngroup()
    {
        SpawnGroup sp = new SpawnGroup()
        {
            SubtypeId = spawnGroupMenu.Name.text,
            Description = CreateDescription(false)
        };
        double.TryParse(spawnGroupMenu.Frequency.text, out sp.Frequency);

        if (ModMenuManager.me.EditingFlag)
        {
            ModDataHolder.AllSpawnGroups[ModMenuManager.me.currentlySelected.GetComponent<SpawnGroupRef>().MySpawnGroupId] = sp;
        }
        else
        {
            ModDataHolder.AllSpawnGroups.Add(sp);
        }
    }

    public void CreateTerritory()
    {
        SpawnGroup territory = new SpawnGroup()
        {
            SubtypeId = territoryMenu.Name.text,
            Frequency = 5.0,
            Description = CreateDescription(true)
        };

        territory.Prefabs.Add(new Prefab("Fleeting_Rival_mk.1","",20.0));


        if(ModMenuManager.me.EditingFlag)
        {
            ModDataHolder.AllTerritories[ModMenuManager.me.currentlySelected.GetComponent<TerritoryRef>().MyTerritoryId] = territory;
        }
        else
        {
            ModDataHolder.AllTerritories.Add(territory);
        }
    }

    public string CreateDescription(bool IsTerritory)
    {
        MyDescription desc = new MyDescription();

        desc.isTerritory = IsTerritory;

        if(IsTerritory)
        {
            desc.name = spawnGroupMenu.Name.text;
            if (territoryMenu.Type.value == 0)
            {
                desc.type = "Static";
            }
            else
            {
                desc.type = "Planetary";
                desc.planetGeneratorName = territoryMenu.PlanetGeneratorName.text;
            }
            desc.active = territoryMenu.Active.isOn;
            double.TryParse(territoryMenu.Radius.text, out desc.radius);
            desc.scaleRadiusWithPlanetSize = territoryMenu.ScaleWithPlanetSize.isOn;
            double.TryParse(territoryMenu.CoordsX.text, out desc.coordsX);
            double.TryParse(territoryMenu.CoordsY.text, out desc.coordsY);
            double.TryParse(territoryMenu.CoordsZ.text, out desc.coordsZ);
            desc.announceArriveDepart = territoryMenu.AnnounceArriveDepart.isOn;
            desc.customArriveMessage = territoryMenu.CustomArriveMessage.text;
            desc.customDepartMessage = territoryMenu.CustomDepartMessage.text;
        }
        else
        {
            if (shipType == ShipType.SpaceCargoShip)
                desc.spaceCargoShip = true;
            else
                desc.spaceCargoShip = false;

            if (shipType == ShipType.LunarCargoShip)
                desc.lunarCargoShip = true;
            else
                desc.lunarCargoShip = false;

            if (shipType == ShipType.AtmosphericCargoShip)
                desc.atmosphericCargoShip = true;
            else
                desc.atmosphericCargoShip = false;

            if (shipType == ShipType.PlanetaryInstallation)
                desc.planetaryInstallation = true;
            else
                desc.planetaryInstallation = false;

            if (shipType == ShipType.RandomSpaceEncounter)
                desc.spaceRandomEncounter = true;
            else
                desc.spaceRandomEncounter = false;

            desc.useAutoPilotInSpace = descriptionMenu.UseAutopilotInSpace.isOn;
            desc.planetaryInstallationType = descriptionMenu.PlanetaryInstallationType.text;
            desc.cutVoxelsAtAirtightCells = descriptionMenu.CutVoxelsAtAirtightCels.isOn;
            desc.factionOwner = descriptionMenu.FactionOwner.text;
            desc.replenishSystems = descriptionMenu.ReplenishSystems;
            desc.uniqueEncounter = descriptionMenu.UniqueEncounter.isOn;

            if (factionType == FactionType.RadomTrader)
                desc.useRandomTraderFaction = true;
            else
                desc.useRandomTraderFaction = false;

            if (factionType == FactionType.RandomMiner)
                desc.useRandomMinerFaction = true;
            else
                desc.useRandomMinerFaction = false;

            if (factionType == FactionType.RandomtBuilder)
                desc.useRandomBuilderFaction = true;
            else
                desc.useRandomBuilderFaction = false;

            desc.ignoreCleanupRules = descriptionMenu.IgnoreCleanupRules.isOn;
            desc.initializeStoreBlocks = descriptionMenu.InitializeStoreBlocks.isOn;
            double.TryParse(descriptionMenu.PauseAutopilotAtDistance.text, out desc.pauseAutopilotAtPlayerDistance);
            desc.forceStaticGrid = descriptionMenu.ForceStaticGrid.isOn;
            desc.adminSpawnOnly = descriptionMenu.AdminSpawnOnly.isOn;
            double.TryParse(descriptionMenu.MinSpawnFromWorldCenter.text, out desc.minSpawnFromWorldCenter);
            double.TryParse(descriptionMenu.MaxSpawnFromWorldCenter.text, out desc.maxSpawnFromWorldCenter);
            desc.planetRequiresVacuum = descriptionMenu.PlanetRequiresVacuum.isOn;
            desc.planetRequiresOxygen = descriptionMenu.PlanetRequiresOxygen.isOn;
            desc.planetRequiresAtmo = descriptionMenu.PlanetRequiresAtmo.isOn;
            double.TryParse(descriptionMenu.PlanetMinimumSize.text, out desc.planetMinimumSize);
            double.TryParse(descriptionMenu.PlanetMaximumSize.text, out desc.planetMaximumSize);
            desc.useThreatLevelCheck = descriptionMenu.UseThreatLevelCheck.isOn;
            double.TryParse(descriptionMenu.ThreatLevelCheckRange.text, out desc.threatLevelCheckRange);
            desc.threatLevelCheckOtherNPCOwners = descriptionMenu.ThreatLevelCheckOtherNPCOwners.isOn;
            double.TryParse(descriptionMenu.ThreatScoreMinimum.text, out desc.threatScoreMinimum);
            double.TryParse(descriptionMenu.ThreatScoreMaximum.text, out desc.threatScoreMaximum);
            desc.usePCUCheck = descriptionMenu.UsePCUCheck.isOn;
            double.TryParse(descriptionMenu.PCUCheckRadius.text, out desc.PCUCheckRadius);
            double.TryParse(descriptionMenu.PCUMinimum.text, out desc.PCUMinimum);
            double.TryParse(descriptionMenu.PCUMaximum.text, out desc.PCUMaximum);
            desc.usePlayerCountCheck = descriptionMenu.UsePlayerCountCheck.isOn;
            double.TryParse(descriptionMenu.PlayerCountCheckRadius.text, out desc.playerCountCheckRadius);
            int.TryParse(descriptionMenu.MinimumPlayers.text, out desc.minimumPlayers);
            int.TryParse(descriptionMenu.MaximumPlayers.text, out desc.maximumPlayers);
            desc.usePlayerCredits = descriptionMenu.UsePlayerCredits.isOn;
            desc.includeAllPlayersInRadius = descriptionMenu.IncludeAllPlayersInRadius.isOn;
            desc.includeFactionBalance = descriptionMenu.IncludeFactionBalance.isOn;
            double.TryParse(descriptionMenu.PlayerCreditCheckRadius.text, out desc.playerCreditCheckRadius);
            int.TryParse(descriptionMenu.MinimumPlayerCredits.text, out desc.minimumPlayerCredits);
            int.TryParse(descriptionMenu.MaximumPlayerCredits.text, out desc.maximumPlayerCredits);
            desc.usePlayerFactionReputation = descriptionMenu.UsePlayerFactionReputation.isOn;
            double.TryParse(descriptionMenu.PlayerReputationCheckRadius.text, out desc.playerReputationCheckRadius);
            desc.checkReputationAgainstOtherNPCFaction = descriptionMenu.CheckReputationAgainstOtherNPCFaction.text;
            double.TryParse(descriptionMenu.MinimumReputation.text, out desc.minimumReputation);
            double.TryParse(descriptionMenu.MaximumReputation.text, out desc.maximumReputation);
            desc.attachModStorageComponentToGrid = descriptionMenu.AttachModComponentToGrid.isOn;
            desc.storageKey = descriptionMenu.StorageKey.text;
            desc.storageValue = descriptionMenu.StorageValue.text;
            desc.useKnowPlayerLocations = descriptionMenu.UseKnownPlayerLocation.isOn;
            desc.knownPlayerLocationMustMatchFaction = descriptionMenu.KnownPlayerLocationMustMatchFaction.isOn;
            double.TryParse(descriptionMenu.KnownPlayerLocationMinSpawnedEncounters.text, out desc.knownPlayerLocationMinSpawnedEncounters);
            double.TryParse(descriptionMenu.KnownPlayerLocationMaxSpawnedEncounters.text, out desc.knownPlayerLocationMaxSpawnedEncounters);
            desc.territory = descriptionMenu.TerritoryName.text;
            double.TryParse(descriptionMenu.MinDistanceFromTerritoryCenter.text, out desc.minDistanceFromTerritoryCenter);
            double.TryParse(descriptionMenu.MaxDistanceFromTerritoryCenter.text, out desc.maxDistanceFromTerritoryCenter);
            desc.rotateFirstCockpitToForward = descriptionMenu.RotateFirstCockpitToForward.isOn;
            desc.spawnRandomCargo = descriptionMenu.SpawnRandomCargo.isOn;
            desc.disableDampeners = descriptionMenu.DisableDampners.isOn;
            desc.reactorsOn = descriptionMenu.ReactorsOn.isOn;
            desc.removeVoxelsIfGridRemoved = descriptionMenu.RemoveVoxelsIfGridRemoved.isOn;
        }

        return desc.ToString();
    }

    public bool Save()
    {
        if (!FileHandler.SaveMod())
        {
            Logging.LogErr($"Error saving mod {ModDataHolder.mod.Name}");
            return false;
        }

        return true;
    }
    #endregion


    void UpdateUI()
    {
        for (int i = 0; i < menus.Count; i++)
        {
            if(i == (int)_currentMenu)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }
}


#region MenuStructs
[Serializable]
public struct PrefabMenu
{
    public InputField FileName;
    public InputField BeaconText;
    public InputField Speed;
}

[Serializable]
public struct SpawnGroupMenu
{
    public InputField Name;
    public InputField Frequency;
}

[Serializable]
public struct DescriptionMenu
{
    public Toggle UseAutopilotInSpace;
    public InputField PlanetaryInstallationType;
    public Toggle CutVoxelsAtAirtightCels, ForceStaticGrid, RemoveVoxelsIfGridRemoved;
    public Toggle UniqueEncounter;
    public InputField FactionOwner;
    public Toggle PlanetRequiresAtmo, PlanetRequiresOxygen, PlanetRequiresVacuum;
    public InputField PlanetMinimumSize, PlanetMaximumSize;
    public Toggle UseThreatLevelCheck;
    public InputField ThreatLevelCheckRange, ThreatScoreMinimum, ThreatScoreMaximum;
    public Toggle ThreatLevelCheckOtherNPCOwners;
    public Toggle UsePCUCheck;
    public InputField PCUCheckRadius, PCUMinimum, PCUMaximum;
    public Toggle UsePlayerCountCheck;
    public InputField PlayerCountCheckRadius, MinimumPlayers, MaximumPlayers;
    public Toggle UsePlayerCredits, IncludeAllPlayersInRadius, IncludeFactionBalance;
    public InputField PlayerCreditCheckRadius, MinimumPlayerCredits, MaximumPlayerCredits;
    public Toggle UsePlayerFactionReputation;
    public InputField CheckReputationAgainstOtherNPCFaction, PlayerReputationCheckRadius, MinimumReputation, MaximumReputation;
    public Toggle AttachModComponentToGrid;
    public InputField StorageKey, StorageValue;
    public Toggle UseKnownPlayerLocation, KnownPlayerLocationMustMatchFaction;
    public InputField KnownPlayerLocationMinSpawnedEncounters, KnownPlayerLocationMaxSpawnedEncounters;
    public InputField TerritoryName, MinDistanceFromTerritoryCenter, MaxDistanceFromTerritoryCenter;
    public Toggle ReplenishSystems, IgnoreCleanupRules, InitializeStoreBlocks, AdminSpawnOnly, RotateFirstCockpitToForward,
        SpawnRandomCargo, DisableDampners, ReactorsOn;
    public InputField PauseAutopilotAtDistance, MinSpawnFromWorldCenter, MaxSpawnFromWorldCenter;
}

[Serializable]
public struct TerritoryMenu
{
    public InputField Name;
    public Dropdown Type;
    public Toggle Active;
    public InputField Radius;
    public InputField CoordsX, CoordsY, CoordsZ;
    public Toggle ScaleWithPlanetSize;
    public InputField PlanetGeneratorName;
    public Toggle AnnounceArriveDepart;
    public InputField CustomArriveMessage;
    public InputField CustomDepartMessage;
}
#endregion
