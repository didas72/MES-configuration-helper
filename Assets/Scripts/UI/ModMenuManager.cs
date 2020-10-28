using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class ModMenuManager : MonoBehaviour
{
    public static ModMenuManager me;

    public bool EditingFlag;

    private GameObject InfoScrollPanel;
    public GameObject currentlySelected;

    private GameObject PrefabHolder;
    private GameObject SpawngroupHolder;

    [SerializeField] GameObject NewOverlay;
    [SerializeField] GameObject SelectOverlay;
    [SerializeField] GameObject OptionsHolder;

    [SerializeField] GameObject PrefabObject;
    [SerializeField] GameObject SpawnGroupObject;
    [SerializeField] GameObject TerritoryObject;
    [SerializeField] GameObject InfoLineObject;

    Dictionary<Prefab, GameObject> displayedPrefabs = new Dictionary<Prefab, GameObject>();
    Dictionary<SpawnGroup, GameObject> displayedSpawngroups = new Dictionary<SpawnGroup, GameObject>();


    private void Awake()
    {
        me = this;

        currentlySelected = null;

        InfoScrollPanel = GameObject.Find("InfoHolder");

        PrefabHolder = GameObject.Find("PrefabHolder");
        SpawngroupHolder = GameObject.Find("SpawngroupHolder");

        //Test();

        UpdateLists();
    }

    void Test()
    {
        Logging.Log(true.ToString().ToLower());
        ModDataHolder.AllPrefabs.Add(new Prefab("T_PRE", "BeaconText", 20.0));
        ModDataHolder.AllSpawnGroups.Add(new SpawnGroup("T_SP", "", 5.0, new List<Prefab>()));
        ModDataHolder.AllTerritories.Add(new SpawnGroup("T_TERR", "[Modular Encounters Territory]\n\t\t\t[Name:TestName]\n\t\t\t[Type:Planetary]\n\t\t\t[Active:true]\n\t\t\t[Radius:10000]\n\t\t\t[ScaleRadiusWithPlanetSize:true]\n\t\t\t[CoordsX:0]\n\t\t\t[CoordsY:0]\n\t\t\t[CoordsZ:0]\n\t\t\t[AnnounceArriveDepart:false]\n\t\t\t[PlanetGeneratorName:PlanetGenName]", 5.0, new List<Prefab>()));
    }

    public void UpdateVisuals()
    {
        EditingFlag = false;

        if(currentlySelected == null)
            ShowInfo();
        
        UpdateLists();
    }

    public void SelectModElement(GameObject go)
    {
        if (currentlySelected != null)
            currentlySelected.transform.GetChild(0).GetComponent<Image>().color = new Color((float)0.1640625, (float)0.12890625, (float)0.5);
        currentlySelected = go;

        Common.ClearChildren(InfoScrollPanel);

        go.transform.GetChild(0).GetComponent<Image>().color = new Color((float)0.1640625, (float)0.12890625, (float)0.5, (float)0.5);

        if (go.TryGetComponent(out PrefabRef prefabRef))
        {
            Prefab prefab = prefabRef.GetMyPrefab();

            Logging.Log("Loaded Prefab from selected Gameobject.");

            ShowInfo(prefab);
        }
        else if (go.TryGetComponent(out SpawnGroupRef spawnGroupRef))
        {
            SpawnGroup spawnGroup = spawnGroupRef.GetMySpawnGroup();

            Logging.Log("Loaded Spawngroup from selected Gameobject.");

            ShowInfo(spawnGroup);
        }
        else if (go.TryGetComponent(out TerritoryRef territoryRef))
        {
            SpawnGroup territory = territoryRef.GetMyTerritory();

            Logging.Log("Loaded Territory from selected Gameobject.");

            ShowInfo(territory);
        }
        else
        {
            Logging.LogErr("Failed to load mod component from selected Gameobject.");
        }
    }

    void ShowInfo(Prefab prefab)
    {
        Logging.Log("Showing info for Prefab.");

        GameObject[] lines = new GameObject[4];

        for(int i = 0; i < lines.Length; i++)//Instantiate all the lines
        {
            lines[i] = Instantiate(InfoLineObject, InfoScrollPanel.transform);
            lines[i].transform.localPosition = new Vector3(0, i * -90 + 260, 0);
        }

        lines[0].GetComponentInChildren<Text>().text = $"File name: {prefab.SubtypeId}";
        lines[1].GetComponentInChildren<Text>().text = $"Position: (x:{prefab.Position.x} y:{prefab.Position.y} z:{prefab.Position.z})";
        lines[2].GetComponentInChildren<Text>().text = $"Beacon text: {prefab.BeaconText}";
        lines[3].GetComponentInChildren<Text>().text = $"Speed: {prefab.Speed}";
    }
    void ShowInfo(SpawnGroup spawnGroup)
    {
        Logging.Log("Showing info for Territory/Spawngroup.");
        MyDescription desc;
        if (!MyDescription.ParseFromString(spawnGroup.Description, out desc))
        {
            Logging.LogErr($"Error loading description from string to show information for Territory/Spawngroup {spawnGroup.SubtypeId}");
            return;
        }
        
        if (desc.isTerritory)
        {
            //is territory
            short lineCount = 5;
            if (desc.scaleRadiusWithPlanetSize)
                lineCount += 1;
            if (desc.announceArriveDepart)
                lineCount += 3;
            if (desc.type == "Planetary")
                lineCount += 1;


            GameObject[] lines = new GameObject[lineCount];
            
            for (short i = 0; i < lines.Length; i++)//Instantiate all the lines
            {
                lines[i] = Instantiate(InfoLineObject, InfoScrollPanel.transform);
                lines[i].transform.localPosition = new Vector3(0, i * -90 + 260, 0);
            }

            short index = 5;
            lines[0].GetComponentInChildren<Text>().text = $"Territory name: {desc.name}";
            lines[1].GetComponentInChildren<Text>().text = $"Territory type: {desc.type}";
            lines[2].GetComponentInChildren<Text>().text = $"Active: {desc.active}";
            lines[3].GetComponentInChildren<Text>().text = $"Radius: {desc.radius}";
            lines[4].GetComponentInChildren<Text>().text = $"Coordinates: (x:{desc.coordsX} y:{desc.coordsY} z:{desc.coordsZ})";
            if(desc.scaleRadiusWithPlanetSize)
            {
                lines[index].GetComponentInChildren<Text>().text = $"Scale Radius With Planet Size: {desc.scaleRadiusWithPlanetSize}";
                index++;
            }
            if(desc.announceArriveDepart)
            {
                lines[index].GetComponentInChildren<Text>().text = $"Announce Arrive Depart: {desc.announceArriveDepart}";
                lines[index + 1].GetComponentInChildren<Text>().text = $"Custom Arrive Message: {desc.customArriveMessage}";
                lines[index + 2].GetComponentInChildren<Text>().text = $"Custom Depart Message: {desc.customDepartMessage}";
                index += 3;
            }
            if(desc.type == "Planetary")
            {
                lines[index].GetComponentInChildren<Text>().text = $"Planet Generator Name: {desc.planetGeneratorName}";
            }
        }
        else
        {
            //is spawngroup
            short lineCount = 5;

            /*
             * 1-SubtypeId-
             * 
             * 1-Type-
             * 
             * 1-FactionOwner/random...faction-
             * 
             * 1 foreach valued thing-
             * 
             * 1-frequency-
             * 
             * 1-Prefabs-
             */

            if (desc.planetaryInstallation)
                lineCount += 3;
            if (desc.spaceCargoShip || desc.spaceRandomEncounter)
                lineCount += 1;
            if (desc.spaceRandomEncounter)
                lineCount += 1;
            if (desc.ignoreCleanupRules)
                lineCount += 1;
            if (desc.replenishSystems)
                lineCount += 1;
            if (desc.initializeStoreBlocks)
                lineCount += 1;
            if (desc.pauseAutopilotAtPlayerDistance != 1)
                lineCount += 1;
            if (desc.forceStaticGrid)
                lineCount += 1;
            if (desc.adminSpawnOnly)
                lineCount += 1;
            if (desc.minSpawnFromWorldCenter != -1)
                lineCount += 1;
            if (desc.maxSpawnFromWorldCenter != -1)
                lineCount += 1;
            if (desc.planetRequiresVacuum)
                lineCount += 1;
            if (desc.planetRequiresAtmo)
                lineCount += 1;
            if (desc.planetRequiresOxygen)
                lineCount += 1;
            if (desc.planetMinimumSize != -1)
                lineCount += 1;
            if (desc.planetMaximumSize != -1)
                lineCount += 1;
            if (desc.useThreatLevelCheck)
                lineCount += 5;
            if (desc.usePCUCheck)
                lineCount += 4;
            if (desc.usePlayerCountCheck)
                lineCount += 4;
            if (desc.usePlayerCredits)
                lineCount += 6;
            if (desc.usePlayerFactionReputation)
                lineCount += 5;
            if (desc.attachModStorageComponentToGrid)
                lineCount += 3;
            if (desc.useKnowPlayerLocations)
                lineCount += 4;
            if (!string.IsNullOrEmpty(desc.territory))
                lineCount += 3;
            if (desc.rotateFirstCockpitToForward)
                lineCount += 1;
            if (desc.spawnRandomCargo)
                lineCount += 1;
            if (desc.disableDampeners)
                lineCount += 1;
            if (desc.reactorsOn)
                lineCount += 1;
            

            GameObject[] lines = new GameObject[lineCount];//Set length

            for (int a = 0; a < lines.Length; a++)//Instantiate all the lines
            {
                lines[a] = Instantiate(InfoLineObject, InfoScrollPanel.transform);
                lines[a].transform.localPosition = new Vector3(0, a * -90 + 260, 0);
            }

            short i = 5;

            lines[0].GetComponentInChildren<Text>().text = $"Name: {spawnGroup.SubtypeId}";
            lines[1].GetComponentInChildren<Text>().text = $"Frequency: {spawnGroup.Frequency}";

            if (desc.spaceCargoShip)
                lines[3].GetComponentInChildren<Text>().text = $"Space Cargo Ship";
            else if (desc.lunarCargoShip)
                lines[3].GetComponentInChildren<Text>().text = $"Lunar Cargo Ship";
            else if(desc.atmosphericCargoShip)
                lines[3].GetComponentInChildren<Text>().text = $"Atmospheric Cargo Ship";
            else if(desc.spaceRandomEncounter)
                lines[3].GetComponentInChildren<Text>().text = $"Space Random Encounter";
            else
                lines[3].GetComponentInChildren<Text>().text = $"Planetary Installation";

            if(desc.useRandomBuilderFaction)
                lines[2].GetComponentInChildren<Text>().text = $"Random Builder Faction";
            else if(desc.useRandomMinerFaction)
                lines[2].GetComponentInChildren<Text>().text = $"Random Miner Faction";
            else if(desc.useRandomTraderFaction)
                lines[2].GetComponentInChildren<Text>().text = $"Random Trader Faction";
            else
                lines[2].GetComponentInChildren<Text>().text = $"Faction Owner: {desc.factionOwner}";

            string s = string.Empty;

            foreach(Prefab pre in spawnGroup.Prefabs)
            {
                s += $"{pre.SubtypeId};";
            }

            lines[4].GetComponentInChildren<Text>().text = $"Prefabs: {s}";

            if(desc.spaceCargoShip || desc.spaceRandomEncounter)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Use Autopilot In Space: {desc.useAutoPilotInSpace}";
                i += 1;
            }

            if(desc.spaceRandomEncounter)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Unique Encounter: {desc.uniqueEncounter}";
                i += 1;
            }

            if(desc.planetaryInstallation)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Installation Type: {desc.planetaryInstallationType}";
                lines[i+1].GetComponentInChildren<Text>().text = $"Cut Voxels At Airtight Cells: {desc.cutVoxelsAtAirtightCells}";
                lines[i+2].GetComponentInChildren<Text>().text = $"Remove Voxels If Grid Remove: {desc.removeVoxelsIfGridRemoved}";
                i += 3;
            }

            if(desc.ignoreCleanupRules)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Ignore Cleanup Rules";
                i += 1;
            }

            if(desc.replenishSystems)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Replenish Systems";
                i += 1;
            }

            if(desc.initializeStoreBlocks)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Initialize Store Blocks";
                i += 1;
            }

            if(desc.pauseAutopilotAtPlayerDistance != -1)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Pause Autopilot At Player Distance: {desc.pauseAutopilotAtPlayerDistance}";
                i += 1;
            }

            if(desc.forceStaticGrid)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Force Static Grid";
                i += 1;
            }

            if(desc.adminSpawnOnly)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Admin Spawn Only";
                i += 1;
            }

            if(desc.minSpawnFromWorldCenter != -1)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Min Spawn From World Center: {desc.minSpawnFromWorldCenter}";
                i += 1;
            }

            if(desc.maxSpawnFromWorldCenter != -1)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Max Spawn From World Center: {desc.maxSpawnFromWorldCenter}";
                i += 1;
            }

            if(desc.planetRequiresVacuum)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Planet Requires Vacuum";
                i += 1;
            }

            if(desc.planetRequiresAtmo)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Planet Requires Atmosphere";
                i += 1;
            }

            if(desc.planetRequiresOxygen)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Planet Requires Oxygen";
                i += 1;
            }

            if(desc.planetMinimumSize != -1)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Planet Minimum Size: {desc.planetMinimumSize}";
                i += 1;
            }

            if(desc.planetMaximumSize != -1)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Planet Maximum Size: {desc.planetMaximumSize}";
                i += 1;
            }

            if(desc.useThreatLevelCheck)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Use Threat Level Check:";
                lines[i+1].GetComponentInChildren<Text>().text = $" -Check Radius: {desc.threatLevelCheckRange}";
                lines[i+2].GetComponentInChildren<Text>().text = $" -Include Other NPC Owners: {desc.threatLevelCheckOtherNPCOwners}";
                lines[i+3].GetComponentInChildren<Text>().text = $" -Minimum Score: {desc.threatScoreMinimum}";
                lines[i+4].GetComponentInChildren<Text>().text = $" -Maximum Score: {desc.threatScoreMaximum}";
                i += 5;
            }

            if(desc.usePCUCheck)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Use PCU Check:";
                lines[i+1].GetComponentInChildren<Text>().text = $" -Check Radius: {desc.PCUCheckRadius}";
                lines[i+2].GetComponentInChildren<Text>().text = $" -Minimum: {desc.PCUMinimum}";
                lines[i+3].GetComponentInChildren<Text>().text = $" -Maximum: {desc.PCUMaximum}";
                i += 4;
            }

            if(desc.usePlayerCountCheck)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Use Player Count Check:";
                lines[i+1].GetComponentInChildren<Text>().text = $" -Check Radius: {desc.playerCountCheckRadius}";
                lines[i+2].GetComponentInChildren<Text>().text = $" -Minimum: {desc.minimumPlayers}";
                lines[i+3].GetComponentInChildren<Text>().text = $" -Maximum: {desc.maximumPlayers}";
                i += 4;
            }

            if(desc.usePlayerCredits)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Use Player Credits:";
                lines[i+1].GetComponentInChildren<Text>().text = $" -Include All Players In Radius: {desc.includeAllPlayersInRadius}";
                lines[i+2].GetComponentInChildren<Text>().text = $" -Inclued Faction Balance: {desc.includeFactionBalance}";
                lines[i+3].GetComponentInChildren<Text>().text = $" -Check Radius: {desc.playerCreditCheckRadius}";
                lines[i+4].GetComponentInChildren<Text>().text = $" -Minimum: {desc.minimumPlayerCredits}";
                lines[i+5].GetComponentInChildren<Text>().text = $" -Maximum: {desc.maximumPlayerCredits}";
                i += 6;
            }

            if(desc.usePlayerFactionReputation)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Use Player Faction Reputation:";
                lines[i+1].GetComponentInChildren<Text>().text = $" -Check Radius: {desc.playerReputationCheckRadius}";
                lines[i+2].GetComponentInChildren<Text>().text = $" -Check Reputation Agains Other NPC Factions: {desc.checkReputationAgainstOtherNPCFaction}";
                lines[i+3].GetComponentInChildren<Text>().text = $" -Minimum: {desc.minimumReputation}";
                lines[i+4].GetComponentInChildren<Text>().text = $" -Maximum: {desc.maximumReputation}";
                i += 5;
            }

            if(desc.attachModStorageComponentToGrid)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Attach Mod Storage To Grid:";
                lines[i+1].GetComponentInChildren<Text>().text = $" -Key: {desc.storageKey}";
                lines[i+2].GetComponentInChildren<Text>().text = $" -Value: {desc.storageValue}";
                i += 3;
            }

            if(desc.useKnowPlayerLocations)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Use Known Player Locations:";
                lines[i+1].GetComponentInChildren<Text>().text = $" -Known Player Location Must Match Faction: {desc.knownPlayerLocationMustMatchFaction}";
                lines[i+2].GetComponentInChildren<Text>().text = $" -Min Spawned Encounters: {desc.knownPlayerLocationMinSpawnedEncounters}";
                lines[i+3].GetComponentInChildren<Text>().text = $" -Max Spawned Encounters: {desc.knownPlayerLocationMaxSpawnedEncounters}";
                i += 4;
            }

            if(!string.IsNullOrEmpty(desc.territory))
            {
                lines[i].GetComponentInChildren<Text>().text = $"Territory: {desc.territory}";
                lines[i+1].GetComponentInChildren<Text>().text = $"Min Distance From Territory Center: {desc.minDistanceFromTerritoryCenter}";
                lines[i+2].GetComponentInChildren<Text>().text = $"Max Distance From Territory Center: {desc.maxDistanceFromTerritoryCenter}";
                i += 3;
            }

            if(desc.rotateFirstCockpitToForward)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Rotate First Cockpit To Forward";
                i += 1;
            }

            if(desc.spawnRandomCargo)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Spawn Random Cargo";
                i += 1;
            }

            if(desc.disableDampeners)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Disable Dampners";
                i += 1;
            }

            if(desc.reactorsOn)
            {
                lines[i].GetComponentInChildren<Text>().text = $"Reactors On";
                i += 1;
            }
        }
    }
    void ShowInfo()
    {
        Common.ClearChildren(InfoScrollPanel);
    }

    public void NewButton()
    {
        NewOverlay.SetActive(true);
    }

    public void CloseNewPanel()
    {
        NewOverlay.SetActive(false);
    }

    public void UpdateLists()
    {
        Common.ClearChildren(PrefabHolder);
        displayedPrefabs.Clear();

        for(int i = 0; i < ModDataHolder.AllPrefabs.Count; i++)
        {
            AddPrefabToList(ModDataHolder.AllPrefabs[i], i);
        }

        Common.ClearChildren(SpawngroupHolder);
        Common.ClearChildren(OptionsHolder);
        displayedSpawngroups.Clear();

        for (int i = 0; i < ModDataHolder.AllSpawnGroups.Count; i++)
        {
            AddSpawngroupToList(ModDataHolder.AllSpawnGroups[i], i);
            AddSpawngroupToSelectList(ModDataHolder.AllSpawnGroups[i], i);
        }

        for(int i = 0; i< ModDataHolder.AllTerritories.Count; i++)
        {
            AddSpawngroupToList(ModDataHolder.AllTerritories[i], i);
        }
    }

    void AddPrefabToList(Prefab prefab, int index)
    {
        Logging.Log($"Adding Prefab {prefab.SubtypeId} to PrefabHolder.");

        GameObject go = Instantiate(PrefabObject, PrefabHolder.transform);
        go.transform.localPosition = new Vector3(0, 310 - displayedPrefabs.Count * PrefabHolder.GetComponent<ScrollHandlerComponent>().ElementHeight, 0);

        go.GetComponentInChildren<Text>().text = prefab.SubtypeId;

        go.GetComponent<PrefabRef>().MyPrefabId = index;

        displayedPrefabs.Add(prefab, go);
    }

    void AddSpawngroupToList(SpawnGroup spawnGroup, int index)
    {
        GameObject go;

        if (!spawnGroup.Description.Contains("Modular Encounters Territory"))
        {
            go = Instantiate(SpawnGroupObject, SpawngroupHolder.transform);
            Logging.Log($"Adding Spawngroup {spawnGroup.SubtypeId} to SpawngroupHolder.");
        }
        else
        {
            go = Instantiate(TerritoryObject, SpawngroupHolder.transform);
            Logging.Log($"Adding Territory {spawnGroup.SubtypeId} to SpawngroupHolder.");
        }

        go.transform.localPosition = new Vector3(0, 310 - displayedSpawngroups.Count * SpawngroupHolder.GetComponent<ScrollHandlerComponent>().ElementHeight, 0);

        go.GetComponentInChildren<Text>().text = spawnGroup.SubtypeId;

        if(go.TryGetComponent(out SpawnGroupRef spr))
        {
            spr.MySpawnGroupId = index;
            displayedSpawngroups.Add(spr.GetMySpawnGroup(), go);
        }
        else if(go.TryGetComponent(out TerritoryRef tr))
        {
            tr.MyTerritoryId = index;
            displayedSpawngroups.Add(tr.GetMyTerritory(), go);
        }
        else
        {
            Logging.LogErr("Failed to get Reference component from instantiated selectable territory/spawngroup.");
        }
    }

    void AddSpawngroupToSelectList(SpawnGroup spawnGroup, int index)
    {
        GameObject go;

        if (spawnGroup.Description.Contains("Modular Encounters Territory"))
        {
            return;
        }

        Logging.Log($"Adding Spawngroup {spawnGroup.SubtypeId} to 'Add To' list.");

        go = Instantiate(SpawnGroupObject, OptionsHolder.transform);

        go.transform.localPosition = new Vector3(0, 310 - displayedSpawngroups.Count * SpawngroupHolder.GetComponent<ScrollHandlerComponent>().ElementHeight, 0);

        go.GetComponentInChildren<Text>().text = spawnGroup.SubtypeId;
        go.GetComponent<SelectableComponent>().IsSelectMenu = true;

        if (go.TryGetComponent(out SpawnGroupRef spr))
        {
            spr.MySpawnGroupId = index;
        }
    }

    public void DeleteSelectedModComponent()
    {
        if(currentlySelected.TryGetComponent(out PrefabRef pre))
        {
            Logging.Log($"Deleting Prefab {pre.GetMyPrefab().SubtypeId}.");
            Prefab pref = pre.GetMyPrefab();
            if(displayedPrefabs.ContainsKey(pref))
            {
                if (displayedPrefabs[pref] == pre.gameObject)
                {
                    Common.ClearChildren(OptionsHolder);

                    ModDataHolder.AllPrefabs.Remove(pref);

                    displayedPrefabs.Remove(pref);

                    RemovePrefabFromSpawnGroups(pref);

                    Common.ClearChildren(currentlySelected);

                    Destroy(currentlySelected);
                    Destroy(pre);
                }
            }
        }
        else if(currentlySelected.TryGetComponent(out SpawnGroupRef sp))
        {
            Logging.Log($"Deleting Prefab {sp.GetMySpawnGroup().SubtypeId}.");
            SpawnGroup spg = sp.GetMySpawnGroup();
            
            if(displayedSpawngroups.ContainsKey(spg))
            {
                if (displayedSpawngroups[spg] == sp.gameObject)
                {
                    Common.ClearChildren(currentlySelected);

                    Destroy(currentlySelected);
                    Destroy(sp);
                }
            }
        }
        else if(currentlySelected.TryGetComponent(out TerritoryRef terr))
        {
            Logging.Log($"Deleting Territory {terr.GetMyTerritory().SubtypeId}.");
            SpawnGroup spg = terr.GetMyTerritory();

            if(displayedSpawngroups.ContainsKey(spg))
            {
                if (displayedSpawngroups[spg] == terr.gameObject)
                {
                    ModDataHolder.AllTerritories.Remove(spg);

                    displayedSpawngroups.Remove(spg);

                    Common.ClearChildren(currentlySelected);

                    Destroy(currentlySelected);
                    Destroy(terr);
                }
            }
        }
        else
        {
            Logging.LogErr($"Failed to load mod element from selected GameObject to be deleted.");
        }

        currentlySelected = null;

        UpdateVisuals();
    }

    public void EditSelectedModComponent()
    {
        if(currentlySelected.TryGetComponent(out SpawnGroupRef spawnGroupRef))
        {
            Logging.Log($"Loading Spawngroup {spawnGroupRef.GetMySpawnGroup().SubtypeId} to edit.");
            EditingFlag = true;
            UIManager.me.NewShipMenu();

            SpawnGroupMenu spMenu = UIManager.me.spawnGroupMenu;
            DescriptionMenu menu = UIManager.me.descriptionMenu;
            SpawnGroup spawnGroup = spawnGroupRef.GetMySpawnGroup();
            MyDescription desc;
            if (!MyDescription.ParseFromString(spawnGroup.Description, out desc))
            {
                Logging.LogErr($"Failed to load description from spawngroup {spawnGroup.SubtypeId} to edit.");
            }

            spMenu.Name.text = spawnGroup.SubtypeId;
            spMenu.Frequency.text = spawnGroup.Frequency.ToString();

            menu.UseAutopilotInSpace.isOn = desc.useAutoPilotInSpace;
            menu.PlanetaryInstallationType.text = desc.planetaryInstallationType;
            menu.CutVoxelsAtAirtightCels.isOn = desc.cutVoxelsAtAirtightCells;
            menu.ForceStaticGrid.isOn = desc.forceStaticGrid;
            menu.RemoveVoxelsIfGridRemoved.isOn = desc.removeVoxelsIfGridRemoved;
            menu.UniqueEncounter.isOn = desc.uniqueEncounter;
            menu.FactionOwner.text = desc.factionOwner;
            menu.PlanetRequiresAtmo.isOn = desc.planetRequiresAtmo;
            menu.PlanetRequiresOxygen.isOn = desc.planetRequiresOxygen;
            menu.PlanetRequiresVacuum.isOn = desc.planetRequiresVacuum;
            menu.PlanetMinimumSize.text = desc.planetMinimumSize.ToString();
            menu.PlanetMaximumSize.text = desc.planetMinimumSize.ToString();
            menu.UseThreatLevelCheck.isOn = desc.useThreatLevelCheck;
            menu.ThreatLevelCheckRange.text = desc.threatLevelCheckRange.ToString();
            menu.ThreatScoreMinimum.text = desc.threatScoreMinimum.ToString();
            menu.ThreatScoreMaximum.text = desc.threatScoreMaximum.ToString();
            menu.ThreatLevelCheckOtherNPCOwners.isOn = desc.threatLevelCheckOtherNPCOwners;
            menu.UsePCUCheck.isOn = desc.usePCUCheck;
            menu.PCUCheckRadius.text = desc.PCUCheckRadius.ToString();
            menu.PCUMaximum.text = desc.PCUMaximum.ToString();
            menu.PCUMinimum.text = desc.PCUMinimum.ToString();
            menu.UsePlayerCountCheck.isOn = desc.usePlayerCountCheck;
            menu.PlayerCountCheckRadius.text = desc.usePlayerCountCheck.ToString();
            menu.MinimumPlayers.text = desc.minimumPlayers.ToString();
            menu.MaximumPlayers.text = desc.maximumPlayers.ToString();
            menu.UsePlayerCredits.isOn = desc.usePlayerCredits;
            menu.IncludeAllPlayersInRadius.isOn = desc.includeAllPlayersInRadius;
            menu.IncludeFactionBalance.isOn = desc.includeFactionBalance;
            menu.PlayerCreditCheckRadius.text = desc.playerCreditCheckRadius.ToString();
            menu.MinimumPlayerCredits.text = desc.minimumPlayerCredits.ToString();
            menu.MaximumPlayerCredits.text = desc.maximumPlayerCredits.ToString();
            menu.UsePlayerFactionReputation.isOn = desc.usePlayerFactionReputation;
            menu.CheckReputationAgainstOtherNPCFaction.text = desc.checkReputationAgainstOtherNPCFaction;
            menu.PlayerReputationCheckRadius.text = desc.playerReputationCheckRadius.ToString();
            menu.MinimumReputation.text = desc.minimumReputation.ToString();
            menu.MaximumReputation.text = desc.maximumReputation.ToString();
            menu.AttachModComponentToGrid.isOn = desc.attachModStorageComponentToGrid;
            menu.StorageKey.text = desc.storageKey;
            menu.StorageValue.text = desc.storageValue;
            menu.UseKnownPlayerLocation.isOn = desc.useKnowPlayerLocations;
            menu.KnownPlayerLocationMustMatchFaction.isOn = desc.knownPlayerLocationMustMatchFaction;
            menu.KnownPlayerLocationMinSpawnedEncounters.text = desc.knownPlayerLocationMinSpawnedEncounters.ToString();
            menu.KnownPlayerLocationMaxSpawnedEncounters.text = desc.knownPlayerLocationMaxSpawnedEncounters.ToString();
            menu.TerritoryName.text = desc.territory;
            menu.MinDistanceFromTerritoryCenter.text = desc.minDistanceFromTerritoryCenter.ToString();
            menu.MaxDistanceFromTerritoryCenter.text = desc.maxDistanceFromTerritoryCenter.ToString();
            menu.ReplenishSystems.isOn = desc.replenishSystems;
            menu.IgnoreCleanupRules.isOn = desc.ignoreCleanupRules;
            menu.InitializeStoreBlocks.isOn = desc.initializeStoreBlocks;
            menu.AdminSpawnOnly.isOn = desc.adminSpawnOnly;
            menu.RotateFirstCockpitToForward.isOn = desc.rotateFirstCockpitToForward;
            menu.SpawnRandomCargo.isOn = desc.spawnRandomCargo;
            menu.DisableDampners.isOn = desc.disableDampeners;
            menu.ReactorsOn.isOn = desc.reactorsOn;
            menu.PauseAutopilotAtDistance.text = desc.pauseAutopilotAtPlayerDistance.ToString();
            menu.MinSpawnFromWorldCenter.text = desc.minSpawnFromWorldCenter.ToString();
            menu.MaxSpawnFromWorldCenter.text = desc.maxSpawnFromWorldCenter.ToString();
        }
        else if(currentlySelected.TryGetComponent(out PrefabRef prefabRef))
        {
            Logging.Log($"Loading Prefab {prefabRef.GetMyPrefab().SubtypeId} to edit.");
            EditingFlag = true;
            UIManager.me.NewPrefabMenu();

            PrefabMenu menu = UIManager.me.prefabMenu;
            Prefab prefab = prefabRef.GetMyPrefab();

            menu.FileName.text = prefab.SubtypeId;
            menu.BeaconText.text = prefab.BeaconText;
            menu.Speed.text = prefab.Speed.ToString();
        }
        else if(currentlySelected.TryGetComponent(out TerritoryRef territoryRef))
        {
            Logging.Log($"Loading Territory {territoryRef.GetMyTerritory().SubtypeId} to edit.");
            EditingFlag = true;
            UIManager.me.NewTerritoryMenu();

            TerritoryMenu menu = UIManager.me.territoryMenu;
            SpawnGroup territory = territoryRef.GetMyTerritory();
            MyDescription desc;
            if(!MyDescription.ParseFromString(territory.Description, out desc))
            {
                Logging.LogErr("Error loading description for edit."); return;
            }

            menu.Name.text = territory.SubtypeId;
            if (desc.type.Contains("tatic"))
                menu.Type.value = 0;
            else
                menu.Type.value = 1;
            menu.Active.isOn = desc.active;
            menu.Radius.text = desc.radius.ToString();
            menu.CoordsX.text = desc.coordsX.ToString();
            menu.CoordsY.text = desc.coordsY.ToString();
            menu.CoordsZ.text = desc.coordsZ.ToString();
            menu.ScaleWithPlanetSize.isOn = desc.scaleRadiusWithPlanetSize;
            menu.PlanetGeneratorName.text = desc.planetGeneratorName;
            menu.AnnounceArriveDepart.isOn = desc.announceArriveDepart;
            menu.CustomArriveMessage.text = desc.customArriveMessage;
            menu.CustomDepartMessage.text = desc.customDepartMessage;
        }
        else
        {
            Logging.LogErr($"Failed to load mod element from selected Gameobject to be edited.");
        }
    }

    public void OpenSelectOverlay()
    {
        if(currentlySelected.TryGetComponent(out PrefabRef prefabRef))
        {
            SelectOverlay.SetActive(true);
        }
    }

    public void CloseSelectOverlay()
    {
        SelectOverlay.SetActive(false);
    }

    public void SelectSP(GameObject go)
    {
        ModDataHolder.AllSpawnGroups[go.GetComponent<SpawnGroupRef>().MySpawnGroupId].AddPrefab(currentlySelected.GetComponent<PrefabRef>().GetMyPrefab());

        Logging.Log($"Added Prefab {currentlySelected.GetComponent<PrefabRef>().GetMyPrefab().SubtypeId} to Spawngroup {ModDataHolder.AllSpawnGroups[go.GetComponent<SpawnGroupRef>().MySpawnGroupId].SubtypeId}.");

        CloseSelectOverlay();

        UpdateVisuals();
    }

    public void RemovePrefabFromSpawnGroups(Prefab pre)
    {
        int count = 0;

        for(int i = 0; i < ModDataHolder.AllSpawnGroups.Count; i++)
        {
            if(ModDataHolder.AllSpawnGroups[i].Prefabs.Contains(pre))
            {
                ModDataHolder.AllSpawnGroups[i].Prefabs.Remove(pre);
                count++;
            }
        }

        Logging.Log($"Removed Prefab from {count} Spawngroups.");
    }
}
