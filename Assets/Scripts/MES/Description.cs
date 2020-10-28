using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Utils;

public class MyDescription//Unfinished
{
    static char[] TagElements = new char[] { '[', ']', ':' , '\t' , '\n' };
    static char[] VectorElements = new char[] { 'X' , 'Y' , 'Z' };

    public const string spawngroupHeader = "[Modular Encounters SpawnGroup]";
    public const string territoryHeader = "[Modular Encounters Territory]";

    public bool isTerritory = false;

    #region TerritoryOnly
    //territory only
    public string name = "";
    public string type = "";//Static or Planetary
    public bool active = true;
    public double radius = 0;
    public bool scaleRadiusWithPlanetSize = false;
    public double coordsX = 0, coordsY = 0, coordsZ = 0;

    public bool announceArriveDepart = false;
    public string customArriveMessage = "";
    public string customDepartMessage = "";

    public string planetGeneratorName = "";//EarthLike, Mars, Titan, other planet generator name
    #endregion
    //10

    #region SpawnGroup
    //spawngroup only
    public bool spaceCargoShip = true;
    public bool lunarCargoShip = false;
    public bool spaceRandomEncounter = false;
    public bool atmosphericCargoShip = false;
    public bool bossEncounterSpace = false;
    public bool bossEncounterAtmo = false;
    public bool bossEncounterAny = false;

    public bool planetaryInstallation = false;
    public string planetaryInstallationType = "";//Small, Medium or Large. default is Small
    public List<Vector3> rotateInstallations;
    public List<bool> reverseForwardDirections;
    public bool cutVoxelsAtAirtightCells = true;
    public bool removeVoxelsIfGridRemoved = true;

    public bool bossCustomAnnounceEnable = false;
    public string bossCustomAnnounceAuthor = "";
    public string bossCustomAnnounceMessage = "";
    public string bossCustomGPSLabel = "";

    public bool replenishSystems = true;
    public bool uniqueEncounter = false;
    public bool useAutoPilotInSpace = false;

    public string factionOwner = "SPRT";
    public bool useRandomMinerFaction = false;
    public bool useRandomBuilderFaction = false;
    public bool useRandomTraderFaction = false;

    public bool ignoreCleanupRules = false;

    public bool initializeStoreBlocks = true;
    public List<string> containerTypesForStoreOrders;

    public List<string> planetBlacklist;
    public List<string> planetWhitelist;
    public bool planetRequiresVacuum = false;
    public bool planetRequiresAtmo = false;
    public bool planetRequiresOxygen = false;
    public double planetMinimumSize = -1;
    public double planetMaximumSize = -1;

    public bool useThreatLevelCheck = false;
    public double threatLevelCheckRange = -1;
    public bool threatLevelCheckOtherNPCOwners = false;
    public double threatScoreMinimum = -1;
    public double threatScoreMaximum = -1;

    public bool usePCUCheck = false;
    public double PCUCheckRadius = 5000;
    public double PCUMinimum = -1;
    public double PCUMaximum = -1;

    public bool usePlayerCountCheck = false;
    public double playerCountCheckRadius = -1;
    public int minimumPlayers = 0;
    public int maximumPlayers = 0;

    public bool usePlayerCredits = false;
    public bool includeAllPlayersInRadius = true;
    public bool includeFactionBalance = true;
    public double playerCreditCheckRadius = 5000;
    public int minimumPlayerCredits = 0;
    public int maximumPlayerCredits = 0;

    public bool usePlayerFactionReputation = false;
    public double playerReputationCheckRadius = 0;
    public string checkReputationAgainstOtherNPCFaction = "";
    public double minimumReputation = -1500;
    public double maximumReputation = 1500;

    public bool attachModStorageComponentToGrid = false;
    public string storageKey = "";
    public string storageValue = "";

    public List<string> requireAllMods;
    public List<string> requireAnyMods;
    public List<string> excludeAnyMods;
    public List<string> excludeAllMods;
    public List<string> modBlockExists;
    public List<int> requiredPlayersOnline;

    public bool useKnowPlayerLocations = false;
    public bool knownPlayerLocationMustMatchFaction = true;
    public double knownPlayerLocationMinSpawnedEncounters = 0;
    public double knownPlayerLocationMaxSpawnedEncounters = 0;

    public string territory = "";
    public double minDistanceFromTerritoryCenter = -1;
    public double maxDistanceFromTerritoryCenter = -1;

    public double pauseAutopilotAtPlayerDistance = -1;//-1 for ignore
    public bool forceStaticGrid = false;
    public bool adminSpawnOnly = false;
    public List<string> sandboxVariables = new List<string>();//add to ParseFromString
    public double minAirDensity = -1;//add to ParseFromString
    public double maxAirDensity = -1;//add to ParseFromString
    public double minGravity = -1;//add to ParseFromString
    public double maxGravity = -1;//add to ParseFromString
    public double minSpawnFromWorldCenter = -1;
    public double maxSpawnFromWorldCenter = -1;
    public double minSpawnFromPlanetSurface = -1;//add to ParseFromString
    public double maxSpawnFromPlanetSurface = -1;//add to ParseFromString
    public bool useDayOrNightOnly = false;//add to ParseFromString
    public bool spawnOnlyAtNight = false;//add to ParseFromString
    public bool useWeatherSpawning = false;//add to ParseFromString
    public List<string> allowedWeatherSystems = new List<string>();//add to ParseFromString
    public bool useTerrainTypeValidation = false;//add to ParseFromString
    public List<string> allowedTerrainTypes = new List<string>();//add to ParseFromString
    public bool rotateFirstCockpitToForward = false;
    public bool spawnRandomCargo = true;
    public bool disableDampeners = false;
    public bool reactorsOn = true;
    #endregion
    //88+10=98

    //add to ParseFromString
    #region PrefabManip
    public bool randomizeWeapons = false;
    public bool ignoreWeaponRandomizerMod = false;
    public List<string> weaponRandomizerBlacklist = new List<string>();
    public List<string> weaponRandomizerWhitelist = new List<string>();
    public bool ignoreWeaponRandomizerTargetGlobalBlacklist = false;
    public bool ignoreWeaponRandomizerTargetGlobalWhitelist = false;
    public List<string> weaponRandomizerTargetBlacklist = new List<string>();
    public List<string> weaponRandomizerTargetWhitelist = new List<string>();
    public bool addDefenseShieldBlocks = false;
    public bool ignoreShieldProviderMod = false;
    public bool useBlockReplacer = false;
    public List<string> replaceBlockReference = new List<string>();
    public bool useBlockReplacerProfile = false;
    public List<string> blockReplacerProfileNames = new List<string>();
    public bool relaxReplacedBlocksSize = false;
    public bool alwaysRemoveBlock = false;
    public bool ignoreGlobalBlockReplacer = false;
    public bool convertToHeavyArmour = false;
    public bool overrideBlockDamageModifier = false;
    public double blockDamageModifier = 100;
    public bool gridsAreEditable = true;
    public bool gridsAreDestructible = true;
    public bool shiftBlockColorsHue = false;
    public bool randomHueShift = false;
    public double shiftBlockColorAmount = 0;
    public List<string> assignGridSkin = new List<string>();
    public bool recolorGrid = false;
    public List<ColorReferencePair> colorReferencePairs = new List<ColorReferencePair>();
    public List<ColorSkinReferencePair> colorSkinReferencePairs = new List<ColorSkinReferencePair>();
    public bool reduceBlockBuildStates = false;
    public double minimumBlocksPercent = 10;
    public double maximumBlocksPercent = 40;
    public double minimumBuildPercent = 10;
    public double maximumBuildPercent = 75;
    public bool ereaseIngameScripts = false;
    public bool disableTimerBlocks = false;
    public bool disableSensorBlocks = false;
    public bool disableWarheads = false;
    public bool disableThrustOverride = false;
    public bool disableGyroOverride = false;
    public List<string> enableBlocksWithName = new List<string>();
    public List<string> disableBlocksWithName = new List<string>();
    public bool allowPartialNames = false;
    public bool useRandomNameGenerator = false;
    public string randomGridNamePrefix = "";
    public string randomGridNamePattern = "";
    public bool useBlockNameReplacer = false;
    public List<NamePair> blockNameReplacerReference = new List<NamePair>();
    public List<string> assignContainerTypesToAllCargo = new List<string>();
    public bool useContainerTypeAssignment = false;
    public List<NamePair> containerTypeAssignmentReference = new List<NamePair>();
    public bool changeTurretSettings = false;
    public double turrentRange = 800;
    public bool turretIdleRotation = false;
    public bool turretTargetMeteors = true;
    public bool turretTargetMissiles = true;
    public bool turretTargetCharacters = true;
    public bool turretTargetSmallGrids = true;
    public bool turretTargetLargeGrids = true;
    public bool turretTargetStations = true;
    public bool turretTargetNeutrals = true;
    public bool clearAuthorship = false;
    #endregion
    //61+98=159

    public override string ToString()
    {
        string temp = string.Empty;
        string output = "\t\t\t<Description>\n";

        if (isTerritory)
        {
            output += $"\t\t\t[Modular Encounters Territory]\n";
            output += $"\t\t\t[Name:{name}]\n";
            output += $"\t\t\t[Type:{type}]\n";
            output += $"\t\t\t[Active:{active.ToString().ToLower()}]\n";
            output += $"\t\t\t[Radius:{radius}]\n";
            if (scaleRadiusWithPlanetSize)
                output += $"\t\t\t[ScaleRadiusWithPlanetSize:{scaleRadiusWithPlanetSize.ToString().ToLower()}]\n";
            output += $"\t\t\t[CoordsX:{coordsX}]\n";
            output += $"\t\t\t[CoordsY:{coordsY}]\n";
            output += $"\t\t\t[CoordsZ:{coordsZ}]\n";
            if (announceArriveDepart)
            {
                output += $"\t\t\t[AnnounceArriveDepart:{announceArriveDepart.ToString().ToLower()}]\n";
                output += $"\t\t\t[CustomArriveMessage:{customArriveMessage}]\n";
                output += $"\t\t\t[CustomDepartMessage:{customDepartMessage}]\n";
            }
            if (!string.IsNullOrEmpty(planetGeneratorName))
                output += $"\t\t\t[PlanetGeneratorName:{planetGeneratorName}]\n";
        }
        else
        {
            output += "\t\t\t[Modular Encounters SpawnGroup]\n";
            if (spaceCargoShip)
            {
                output += $"\t\t\t[SpaceCargoShip:{spaceCargoShip.ToString().ToLower()}]\n";
                if (useAutoPilotInSpace)
                    output += $"\t\t\t[UseAutoPilotInSpace:{useAutoPilotInSpace.ToString().ToLower()}]\n";
            }
                
            if (lunarCargoShip)
                output += $"\t\t\t[LunarCargoShip:{lunarCargoShip.ToString().ToLower()}]\n";

            if (spaceRandomEncounter)
            {
                output += $"\t\t\t[SpaceRandomEncounter:{spaceRandomEncounter.ToString().ToLower()}]\n";
                if (useAutoPilotInSpace)
                    output += $"\t\t\t[UseAutoPilotInSpace:{useAutoPilotInSpace.ToString().ToLower()}]\n";

                output += $"\t\t\t[UniqueEncounter:{uniqueEncounter.ToString().ToLower()}]\n";
            }

            if (atmosphericCargoShip)
                output += $"\t\t\t[AtmosphericCargoShip:{atmosphericCargoShip.ToString().ToLower()}]\n";

            if (planetaryInstallation)
            {
                output += $"\t\t\t[PlanetaryInstallation:{planetaryInstallation.ToString().ToLower()}]\n";
                output += $"\t\t\t[PlanetaryInstallationType:{planetaryInstallationType}]\n";
                output += $"\t\t\t[CutVoxelsAtAirtightCells:{cutVoxelsAtAirtightCells.ToString().ToLower()}]\n";
                output += $"\t\t\t[RemoveVoxelsIfGridRemoved:{removeVoxelsIfGridRemoved.ToString().ToLower()}]\n";

                if(rotateInstallations.Count != 0)
                {
                    foreach (Vector3 vec in rotateInstallations)
                    {
                        temp += $"{Vector3ToMESFormat(vec)},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[RotateInstallations:{temp}]\n";

                    temp = string.Empty;
                }
                
                if(reverseForwardDirections.Count != 0)
                {
                    foreach (bool boolean in reverseForwardDirections)
                    {
                        temp += $"{boolean.ToString().ToLower()},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[ReverseForwardDirections:{temp}]\n";

                    temp = string.Empty;
                }
            }

            if (bossEncounterSpace)
            {
                output += $"\t\t\t[BossEncounterSpace:{bossEncounterSpace.ToString().ToLower()}]\n";
            }

            if (bossEncounterAtmo)
            {
                output += $"\t\t\t[BossEncounterAtmo:{bossEncounterAtmo.ToString().ToLower()}]\n";
            }

            if (bossEncounterAny)
            {
                output += $"\t\t\t[BossEncounterAny:{bossEncounterAny.ToString().ToLower()}]\n";
            }

            if(bossEncounterSpace || bossEncounterAtmo || bossEncounterAny)
            {
                output += $"\t\t\t[BossCustomAnnounceEnable:{bossCustomAnnounceEnable.ToString().ToLower()}]\n";
                if(bossCustomAnnounceEnable)
                {
                    output += $"\t\t\t[BossCustomAnnounceAuthor:{bossCustomAnnounceAuthor}]\n";
                    output += $"\t\t\t[BossCustomAnnounceMessage:{bossCustomAnnounceMessage}]\n";
                }
                if(!string.IsNullOrEmpty(bossCustomGPSLabel))
                    output += $"\t\t\t[BossCustomGPSLabel:{bossCustomGPSLabel}]\n";
            }

            if(!string.IsNullOrEmpty(factionOwner))
                output += $"\t\t\t[FactionOwner:{factionOwner}]\n";
            else
            {
                if (useRandomMinerFaction)
                    output += $"\t\t\t[UseRandomMinerFaction:{useRandomMinerFaction.ToString().ToLower()}]\n";
                if (useRandomBuilderFaction)
                    output += $"\t\t\t[UseRandomBuilderFaction:{useRandomBuilderFaction.ToString().ToLower()}]\n";
                if (useRandomTraderFaction)
                    output += $"\t\t\t[UseRandomTraderFaction:{useRandomBuilderFaction.ToString().ToLower()}]\n";
            }

            if (ignoreCleanupRules)
                output += $"\t\t\t[IgnoreCleanupRules:{ignoreCleanupRules.ToString().ToLower()}]\n";

            if (replenishSystems)
                output += $"\t\t\t[ReplenishSystems:{replenishSystems.ToString().ToLower()}]\n";

            if(initializeStoreBlocks)
                output += $"\t\t\t[InitializeStoreBlocks:{initializeStoreBlocks.ToString().ToLower()}]\n";

            if(containerTypesForStoreOrders.Count != 0)
            {
                foreach(string s in containerTypesForStoreOrders)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[ContainerTypesForStoreOreders:{temp}]\n";
                temp = string.Empty;
            }

            if(planetBlacklist.Count != 0)
            {
                foreach(string s in planetBlacklist)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[PlanetBlacklist:{temp}]\n";

                temp = string.Empty;
            }

            if (planetWhitelist.Count != 0)
            {
                foreach (string s in planetWhitelist)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[PlanetWhitelist:{temp}]\n";

                temp = string.Empty;
            }

            if (pauseAutopilotAtPlayerDistance != -1)
                output += $"\t\t\t[PauseAutopilotAtPlayerDistance:{pauseAutopilotAtPlayerDistance.ToString().ToLower()}]\n";

            if (forceStaticGrid)
                output += $"\t\t\t[ForceStaticGrid:{forceStaticGrid.ToString().ToLower()}]\n";

            if (adminSpawnOnly)
                output += $"\t\t\t[AdminSpawnOnly:{adminSpawnOnly.ToString().ToLower()}]\n";

            if(sandboxVariables.Count != 0)
            {
                foreach(string s in sandboxVariables)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[SandboxVariables:{temp}]\n";

                temp = string.Empty;
            }

            if (minAirDensity != -1)
                output += $"\t\t\t[MinAirDensity:{minAirDensity}]\n";

            if(maxAirDensity != -1)
                output += $"\t\t\t[MaxAirDensity:{maxAirDensity}]\n";

            if (minGravity != -1)
                output += $"\t\t\t[MinGravity:{minGravity}]\n";

            if (maxGravity != -1)
                output += $"\t\t\t[MaxGravity:{maxGravity}]\n";

            if (minSpawnFromPlanetSurface != -1)
                output += $"\t\t\t[MinSpawnFromPlanetSurface:{minSpawnFromPlanetSurface}]\n";

            if (maxSpawnFromPlanetSurface != -1)
                output += $"\t\t\t[MaxSpawnFromPlanetSurface:{maxSpawnFromPlanetSurface}]\n";

            if (useDayOrNightOnly)
            {
                output += $"\t\t\t[UseDayOrNighOnly:{useDayOrNightOnly.ToString().ToLower()}]\n";
                output += $"\t\t\t[SpawnOnlyAtNight:{spawnOnlyAtNight.ToString().ToLower()}]\n";
            }

            if (useWeatherSpawning)
            {
                output += $"\t\t\t[UseWeatherSpawning:{useWeatherSpawning.ToString().ToLower()}]\n";
                foreach(string s in allowedWeatherSystems)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[AllowedWeatherSystems:{temp}]\n";

                temp = string.Empty;
            }

            if(useTerrainTypeValidation)
            {
                output += $"\t\t\t[UseTerrainTypeValidation:{useTerrainTypeValidation.ToString().ToLower()}]\n";

                foreach(string s in allowedTerrainTypes)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[AllowedTerrainTypes:{temp}]\n";

                temp = string.Empty;
            }

            if (minSpawnFromWorldCenter != -1)
                output += $"\t\t\t[MinSpawnFromWorldCenter:{minSpawnFromWorldCenter}]\n";

            if (maxSpawnFromWorldCenter != -1)
                output += $"\t\t\t[MaxSpawnFromWorldCenter:{maxSpawnFromWorldCenter}]\n";

            if (planetRequiresVacuum)
                output += $"\t\t\t[PlanetRequiresVacuum:{planetRequiresVacuum.ToString().ToLower()}]\n";

            if (planetRequiresAtmo)
                output += $"\t\t\t[PlanetRequiresAtmo:{planetRequiresAtmo.ToString().ToLower()}]\n";

            if (planetRequiresOxygen)
                output += $"\t\t\t[PlanetRequiresOxygen:{planetRequiresOxygen.ToString().ToLower()}]\n";

            if (planetMinimumSize != -1)
                output += $"\t\t\t[PlanetMinimumSize:{planetMinimumSize}]\n";

            if (planetMaximumSize != -1)
                output += $"\t\t\t[PlanetMaximumSize:{planetMaximumSize}]\n";

            if (useThreatLevelCheck)
            {
                output += $"\t\t\t[UseThreatLevelCheck:{useThreatLevelCheck.ToString().ToLower()}]\n";
                output += $"\t\t\t[ThreatLevelCheckRange:{threatLevelCheckRange}]\n";
                output += $"\t\t\t[ThreatIncludeOtherNpcOwners:{threatLevelCheckOtherNPCOwners.ToString().ToLower()}]\n";
                output += $"\t\t\t[ThreatScoreMinimum:{threatScoreMinimum}]\n";
                output += $"\t\t\t[ThreatScoreMaximum:{threatScoreMaximum}]\n";
            }

            if (usePCUCheck)
            {
                output += $"\t\t\t[UsePCUCheck:{usePCUCheck.ToString().ToLower()}]\n";
                output += $"\t\t\t[PCUCheckRadius:{PCUCheckRadius}]\n";
                output += $"\t\t\t[PCUMinimum:{PCUMinimum}]\n";
                output += $"\t\t\t[PCUMaximum:{PCUMaximum}]\n";
            }

            if (usePlayerCountCheck)
            {
                output += $"\t\t\t[UsePlayerCountCheck:{usePlayerCountCheck.ToString().ToLower()}]\n";
                output += $"\t\t\t[PlayerCountCheckRadius:{playerCountCheckRadius}]\n";
                output += $"\t\t\t[MinimumPlayers:{minimumPlayers}]\n";
                output += $"\t\t\t[MaximumPlayers:{maximumPlayers}]\n";
            }

            if (usePlayerCredits)
            {
                output += $"\t\t\t[UsePlayerCredits:{usePlayerCredits.ToString().ToLower()}]\n";
                output += $"\t\t\t[IncludeAllPlayersInRadius:{includeAllPlayersInRadius.ToString().ToLower()}]\n";
                output += $"\t\t\t[IncludeFactionBalance:{includeFactionBalance.ToString().ToLower()}]\n";
                output += $"\t\t\t[PlayerCreditsCheckRadius:{playerCreditCheckRadius}]\n";
                output += $"\t\t\t[MinimumPlayerCredits:{minimumPlayerCredits}]\n";
                output += $"\t\t\t[MaximumPlayerCredits:{maximumPlayerCredits}]\n";
            }

            if (usePlayerFactionReputation)
            {
                output += $"\t\t\t[UsePlayerFactionReputation:{usePlayerFactionReputation.ToString().ToLower()}]\n";
                output += $"\t\t\t[PlayerReputationCheckRadius:{playerReputationCheckRadius}]\n";
                output += $"\t\t\t[CheckReputationAgainstOtherNPCFaction:{checkReputationAgainstOtherNPCFaction}]\n";
                output += $"\t\t\t[MinimumReputation:{minimumReputation}]\n";
                output += $"\t\t\t[MaximumReputation:{maximumReputation}]\n";
            }

            if (attachModStorageComponentToGrid)
            {
                output += $"\t\t\t[AttachModStorageComponentToGrid:{attachModStorageComponentToGrid.ToString().ToLower()}]\n";
                output += $"\t\t\t[StorageKey:{storageKey}]\n";
                output += $"\t\t\t[StorageValue:{storageValue}]\n";
            }

            if (requireAllMods.Count != 0)
            {
                foreach (string s in requireAllMods)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[RequireAllMods:{temp}]\n";

                temp = string.Empty;
            }

            if (requireAnyMods.Count != 0)
            {
                foreach (string s in requireAnyMods)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[RequireAnyMods:{temp}]\n";

                temp = string.Empty;
            }

            if (excludeAnyMods.Count != 0)
            {
                foreach (string s in excludeAnyMods)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[ExcludeAnyMods:{temp}]\n";

                temp = string.Empty;
            }

            if (excludeAllMods.Count != 0)
            {
                foreach (string s in excludeAllMods)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[ExcludeAllMods:{temp}]\n";

                temp = string.Empty;
            }

            if (modBlockExists.Count != 0)
            {
                foreach (string s in modBlockExists)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[ModBlockExists:{temp}]\n";

                temp = string.Empty;
            }

            if (requiredPlayersOnline.Count != 0)
            {
                foreach (int s in requiredPlayersOnline)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[RequiredPlayersOnline:{temp}]\n";

                temp = string.Empty;
            }

            if (useKnowPlayerLocations)
            {
                output += $"\t\t\t[UseKnownPlayerLocations:{useKnowPlayerLocations.ToString().ToLower()}]\n";
                output += $"\t\t\t[KnownPlayerLocationMustMatchFaction:{knownPlayerLocationMustMatchFaction.ToString().ToLower()}]\n";
                output += $"\t\t\t[KnownPlayerLocationMinSpawnedEncounters:{knownPlayerLocationMinSpawnedEncounters}]\n";
                output += $"\t\t\t[KnownPlayerLocationMaxSpawnedEncounters:{knownPlayerLocationMaxSpawnedEncounters}]\n";
            }

            if (!string.IsNullOrEmpty(territory))
            {
                output += $"\t\t\t[Territory:{territory}]\n";
                output += $"\t\t\t[MinDistanceFromTerritoryCenter:{minDistanceFromTerritoryCenter}]\n";
                output += $"\t\t\t[MaxDistanceFromTerritoryCenter:{maxDistanceFromTerritoryCenter}]\n";
            }

            if (rotateFirstCockpitToForward)
                output += $"\t\t\t[RotateFirstCockpitToForward:{rotateFirstCockpitToForward.ToString().ToLower()}]\n";

            if(!spawnRandomCargo)
                output += $"\t\t\t[SpawnRandomCargo:{spawnRandomCargo.ToString().ToLower()}]\n";

            if(!disableDampeners)
                output += $"\t\t\t[DisableDampeners:{disableDampeners.ToString().ToLower()}]\n";

            if(!reactorsOn)
                output += $"\t\t\t[ReactorsOn:{reactorsOn.ToString().ToLower()}]\n";

            //prefab manip

            if (randomizeWeapons)
            {
                output += $"\t\t\t[RandomizeWeapons:{randomizeWeapons.ToString().ToLower()}]\n";

                if (ignoreWeaponRandomizerMod)
                    output += $"\t\t\t[IgnoreWeaponRandomizerMod:{ignoreWeaponRandomizerMod.ToString().ToLower()}]\n";

                if (weaponRandomizerBlacklist.Count != 0)
                {
                    foreach (string s in weaponRandomizerBlacklist)
                    {
                        temp += $"{s},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[WeaponRandomizerBlacklist:{temp}]\n";

                    temp = string.Empty;
                }

                if (weaponRandomizerWhitelist.Count != 0)
                {
                    foreach (string s in weaponRandomizerWhitelist)
                    {
                        temp += $"{s},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[WeaponRandomizerWhitelist:{temp}]\n";

                    temp = string.Empty;
                }

                if (ignoreWeaponRandomizerTargetGlobalBlacklist)
                    output += $"\t\t\t[IgnoreWeaponRandomizerTargetGlobalBlacklist:{ignoreWeaponRandomizerTargetGlobalBlacklist.ToString().ToLower()}]\n";

                if (ignoreWeaponRandomizerTargetGlobalWhitelist)
                    output += $"\t\t\t[IgnoreWeaponRandomizerTargetGlobalWhitelist:{ignoreWeaponRandomizerTargetGlobalWhitelist.ToString().ToLower()}]\n";

                if (weaponRandomizerTargetBlacklist.Count != 0)
                {
                    foreach (string s in weaponRandomizerTargetBlacklist)
                    {
                        temp += $"{s},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[WeaponRandomizerTargetBlacklist:{temp}]\n";

                    temp = string.Empty;
                }

                if (weaponRandomizerTargetWhitelist.Count != 0)
                {
                    foreach (string s in weaponRandomizerTargetWhitelist)
                    {
                        temp += $"{s},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[WeaponRandomizerTargetWhitelist:{temp}]\n";

                    temp = string.Empty;
                }

                if (addDefenseShieldBlocks)
                    output += $"\t\t\t[AddDefenseShieldBlocks:{addDefenseShieldBlocks.ToString().ToLower()}]\n";

                if (ignoreShieldProviderMod)
                    output += $"\t\t\t[IgnoreShieldProviderMod:{ignoreShieldProviderMod.ToString().ToLower()}]\n";
            }

            if (useBlockReplacer)
            {
                output += $"\t\t\t[UseBlockReplacer:{useBlockReplacer.ToString().ToLower()}]\n";

                if (replaceBlockReference.Count != 0)
                {
                    foreach (string s in replaceBlockReference)
                    {
                        temp += $"{s},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[ReplaceBlockReference:{temp}]\n";

                    temp = string.Empty;
                }

                if (useBlockReplacerProfile)
                    output += $"\t\t\t[UseBlockReplacerProfile:{useBlockReplacerProfile.ToString().ToLower()}]\n";

                if (blockReplacerProfileNames.Count != 0)
                {
                    foreach (string s in blockReplacerProfileNames)
                    {
                        temp += $"{s},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[BlockReplacerProfileNames:{temp}]\n";

                    temp = string.Empty;
                }

                if (relaxReplacedBlocksSize)
                    output += $"\t\t\t[RelaxReplacedBlocksSize:{relaxReplacedBlocksSize.ToString().ToLower()}]\n";

                if (alwaysRemoveBlock)
                    output += $"\t\t\t[AlwaysRemoveBlock:{alwaysRemoveBlock.ToString().ToLower()}]\n";

                if (ignoreGlobalBlockReplacer)
                    output += $"\t\t\t[IgnoreGlobalBlockReplacer:{ignoreGlobalBlockReplacer.ToString().ToLower()}]\n";

                if (convertToHeavyArmour)
                    output += $"\t\t\t[ConvertToHeavyArmour:{convertToHeavyArmour.ToString().ToLower()}]\n";
            }

            if (overrideBlockDamageModifier)
            {
                output += $"\t\t\t[OverrideBlockDamageModifier:{overrideBlockDamageModifier.ToString().ToLower()}]\n";
                output += $"\t\t\t[BlockDamageModifier:{blockDamageModifier}]\n";
            }

            if (!gridsAreEditable)
                output += $"\t\t\t[GridsAreEditable:{gridsAreEditable.ToString().ToLower()}]\n";

            if (!gridsAreDestructible)
                output += $"\t\t\t[GridsAreDestructible:{gridsAreDestructible.ToString().ToLower()}]\n";

            if (shiftBlockColorsHue)
            {
                output += $"\t\t\t[ShiftBlockColorsHue:{shiftBlockColorsHue.ToString().ToLower()}]\n";

                if (randomHueShift)
                    output += $"\t\t\t[RandomHueShift:{randomHueShift.ToString().ToLower()}]\n";
                else
                    output += $"\t\t\t[ShiftBlockColourAmount:{shiftBlockColorAmount}]\n";
            }

            if (assignGridSkin.Count != 0)
            {
                foreach (string s in assignGridSkin)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[AssignGridSkin:{temp}]\n";

                temp = string.Empty;
            }

            if (recolorGrid)
            {
                output += $"\t\t\t[RecolorGrid:{recolorGrid.ToString().ToLower()}]\n";

                if (colorReferencePairs.Count != 0)
                {
                    foreach (ColorReferencePair s in colorReferencePairs)
                    {
                        temp += $"{s},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[ColorReferencePairs:{temp}]\n";

                    temp = string.Empty;
                }

                if (colorReferencePairs.Count != 0)
                {
                    foreach (ColorSkinReferencePair s in colorSkinReferencePairs)
                    {
                        temp += $"{s},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[ColorReferencePairs:{temp}]\n";

                    temp = string.Empty;
                }
            }

            if (reduceBlockBuildStates)
            {
                output += $"\t\t\t[ReduceBlockBuildStates:{reduceBlockBuildStates.ToString().ToLower()}]\n";

                output += $"\t\t\t[MinimumBlocksPercent:{minimumBlocksPercent}]\n";
                output += $"\t\t\t[MaximumBlocksPercent:{maximumBlocksPercent}]\n";
                output += $"\t\t\t[MinimumBuildPercent:{minimumBuildPercent}]\n";
                output += $"\t\t\t[MaximumBuildPercent:{maximumBuildPercent}]\n";
            }

            if (ereaseIngameScripts)
                output += $"\t\t\t[EreaseIngameScripts:{ereaseIngameScripts.ToString().ToLower()}]\n";

            if (disableTimerBlocks)
                output += $"\t\t\t[DisableTimerBlocks:{disableTimerBlocks.ToString().ToLower()}]\n";

            if (disableSensorBlocks)
                output += $"\t\t\t[DisableSensorBlocks:{disableSensorBlocks.ToString().ToLower()}]\n";

            if (disableWarheads)
                output += $"\t\t\t[DisableWarheads:{disableWarheads.ToString().ToLower()}]\n";

            if (disableThrustOverride)
                output += $"\t\t\t[DisableThrustOverride:{disableThrustOverride.ToString().ToLower()}]\n";

            if (disableGyroOverride)
                output += $"\t\t\t[DisableGyroOverride:{disableGyroOverride.ToString().ToLower()}]\n";

            if (enableBlocksWithName.Count != 0)
            {
                foreach (string s in enableBlocksWithName)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[EnableBlocksWithName:{temp}]\n";

                temp = string.Empty;
            }

            if (disableBlocksWithName.Count != 0)
            {
                foreach (string s in disableBlocksWithName)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[DisableBlocksWithName:{temp}]\n";

                temp = string.Empty;
            }

            if (allowPartialNames)
                output += $"\t\t\t[AllowPartialNames:{allowPartialNames.ToString().ToLower()}]\n";

            if (useRandomNameGenerator)
            {
                output += $"\t\t\t[UseRandomNameGenerator:{useRandomNameGenerator.ToString().ToLower()}]\n";

                output += $"\t\t\t[RandomGridNamePrefix:{randomGridNamePrefix}]\n";
                output += $"\t\t\t[RandomGridNamePattern:{randomGridNamePattern}]\n";
            }

            if (useBlockNameReplacer)
            {
                output += $"\t\t\t[UseBlockNameReplacer:{useBlockNameReplacer.ToString().ToLower()}]\n";

                if (blockNameReplacerReference.Count != 0)
                {
                    foreach (NamePair s in blockNameReplacerReference)
                    {
                        temp += $"{s},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[BlockNameReplacerReference:{temp}]\n";

                    temp = string.Empty;
                }
            }

            if (assignContainerTypesToAllCargo.Count != 0)
            {
                foreach (string s in assignContainerTypesToAllCargo)
                {
                    temp += $"{s},";
                }
                temp.TrimEnd(',');

                output += $"\t\t\t[AssignContainerTypesToAllCargo:{temp}]\n";

                temp = string.Empty;
            }

            if (useContainerTypeAssignment)
            {
                output += $"\t\t\t[UseContainerTypeAssignment:{useContainerTypeAssignment.ToString().ToLower()}]\n";

                if (containerTypeAssignmentReference.Count != 0)
                {
                    foreach (NamePair s in containerTypeAssignmentReference)
                    {
                        temp += $"{s},";
                    }
                    temp.TrimEnd(',');

                    output += $"\t\t\t[ContainerTypeAssignmentReference:{temp}]\n";

                    temp = string.Empty;
                }
            }

            if (changeTurretSettings)
            {
                output += $"\t\t\t[ChangeTurretSettings:{changeTurretSettings.ToString().ToLower()}]\n";

                output += $"\t\t\t[TurretRange:{turrentRange}]\n";
                output += $"\t\t\t[TurretIdleRotation:{turretIdleRotation.ToString().ToLower()}]\n";
                output += $"\t\t\t[TurretTargetMeteors:{turretTargetMeteors.ToString().ToLower()}]\n";
                output += $"\t\t\t[TurretTargetMissiles:{turretTargetMissiles.ToString().ToLower()}]\n";
                output += $"\t\t\t[TurretTargetCharacters:{turretTargetCharacters.ToString().ToLower()}]\n";
                output += $"\t\t\t[TurretTargetSmallGrids:{turretTargetSmallGrids.ToString().ToLower()}]\n";
                output += $"\t\t\t[TurretTargetLargeGrids:{turretTargetLargeGrids.ToString().ToLower()}]\n";
                output += $"\t\t\t[TurretTargetStations:{turretTargetStations.ToString().ToLower()}]\n";
                output += $"\t\t\t[TurretTargetNeutrals:{turretTargetNeutrals.ToString().ToLower()}]\n";
            }

            if (clearAuthorship)
                output += $"\t\t\t[ClearAuthorship:{clearAuthorship.ToString().ToLower()}]\n";
        }

        //

        output += $"\t\t\t</Description>\n";
        return output;
    }

    public static bool ParseFromString(string String, out MyDescription desc)
    {
        Logging.Log($"Attempting to read description from string: \n {String}");
        desc = new MyDescription();

        if (String.Contains("Modular Encounters Territory"))
            desc.isTerritory = true;
        else
            desc.isTerritory = false;

        string[] splitDescprition = String.Split('\n');

        foreach (string tag in splitDescprition)
        {
            Logging.Log($"Iteration for tag {tag}");

            //if (!CheckTagBool("", tag, ref desc.))
            //  return false;

            #region Territory
            if (!CheckTagString("Name", tag, ref desc.name))
                return false;
            if (!CheckTagString("Type", tag, ref desc.type))
                return false;
            if (!CheckTagBool("Active", tag, ref desc.active))
                return false;
            if (!CheckTagDouble("Radius", tag, ref desc.radius))
                return false;
            if (!CheckTagBool("ScaleRadiusWithPlanetSize", tag, ref desc.scaleRadiusWithPlanetSize))
                return false;
            if (!CheckTagDouble("\tCoordsX", tag, ref desc.coordsX))
                return false;
            if (!CheckTagDouble("\tCoordsY", tag, ref desc.coordsY))
                return false;
            if (!CheckTagDouble("\tCoordsZ", tag, ref desc.coordsZ))
                return false;
            if (!CheckTagBool("AnnounceArriveDepart", tag, ref desc.announceArriveDepart))
                return false;
            if (!CheckTagString("CustomArriveMessage", tag, ref desc.customArriveMessage))
                return false;
            if (!CheckTagString("CustomDepartMessage", tag, ref desc.customDepartMessage))
                return false;
            if (!CheckTagString("PlanetGeneratorName", tag, ref desc.planetGeneratorName))
                return false;
            #endregion

            #region SpawnGroup
            if (!CheckTagBool("SpaceCargoShip", tag, ref desc.spaceCargoShip))
                return false;
            if (!CheckTagBool("LunarCargoShip", tag, ref desc.lunarCargoShip))
                return false;
            if (!CheckTagBool("SpaceRandomEncounter", tag, ref desc.spaceRandomEncounter))
                return false;
            if (!CheckTagBool("AtmosphericCargoShip", tag, ref desc.atmosphericCargoShip))
                return false;
            if (!CheckTagBool("BossEncounterSpace", tag, ref desc.bossEncounterSpace))
                return false;
            if (!CheckTagBool("BossEncounterAtmo", tag, ref desc.bossEncounterAtmo))
                return false;
            if (!CheckTagBool("BossEncounterAny", tag, ref desc.bossEncounterAny))
                return false;
            if (!CheckTagBool("UseAutoPilotInSpace", tag, ref desc.useAutoPilotInSpace))
                return false;
            if (!CheckTagBool("PlanetaryInstallation", tag, ref desc.planetaryInstallation))
                return false;
            if (!CheckTagString("PlanetaryInstallationType", tag, ref desc.planetaryInstallationType))
                return false;
            if (!CheckTagListVector3("RotateInstallations", tag, ref desc.rotateInstallations))
                return false;
            if (!CheckTagListBool("ReverseForwardDirections", tag, ref desc.reverseForwardDirections))
                return false;
            if (!CheckTagBool("CutVoxelsAtAirtightCells", tag, ref desc.cutVoxelsAtAirtightCells))
                return false;
            if (!CheckTagBool("RemoveVoxelsIfGridRemoved", tag, ref desc.removeVoxelsIfGridRemoved))
                return false;
            if (!CheckTagBool("BossCustomAnnounceEnable", tag, ref desc.bossCustomAnnounceEnable))
                return false;
            if (!CheckTagString("BossCustomAnnounceAuthor", tag, ref desc.bossCustomAnnounceAuthor))
                return false;
            if (!CheckTagString("BossCustomAnnounceMessage", tag, ref desc.bossCustomAnnounceMessage))
                return false;
            if (!CheckTagString("BossCustomGPSLabel", tag, ref desc.bossCustomGPSLabel))
                return false;
            if (!CheckTagBool("UniqueEncounter", tag, ref desc.uniqueEncounter))
                return false;
            if (!CheckTagString("FactionOwner", tag, ref desc.factionOwner))
                return false;
            if (!CheckTagBool("UseRandomMinerFaction", tag, ref desc.useRandomMinerFaction))
                return false;
            if (!CheckTagBool("UseRandomBuilderFaction", tag, ref desc.useRandomBuilderFaction))
                return false;
            if (!CheckTagBool("UseRandomTraderFaction", tag, ref desc.useRandomTraderFaction))
                return false;
            if (!CheckTagBool("IgnoreCleanupRules", tag, ref desc.ignoreCleanupRules))
                return false;
            if (!CheckTagBool("ReplenishSystems", tag, ref desc.replenishSystems))
                return false;
            if (!CheckTagBool("InitializeStoreBlocks", tag, ref desc.initializeStoreBlocks))
                return false;
            if (!CheckTagListString("ContainerTypesForStoreOrders", tag, ref desc.containerTypesForStoreOrders))
                return false;
            if (!CheckTagListString("PlanetBlackList", tag, ref desc.planetBlacklist))
                return false;
            if (!CheckTagListString("PlanetWhiteList", tag, ref desc.planetWhitelist))
                return false;
            if (!CheckTagDouble("PauseAutopilotAtPlayerDistance", tag, ref desc.pauseAutopilotAtPlayerDistance))
                return false;
            if (!CheckTagBool("ForceStaticGrid", tag, ref desc.forceStaticGrid))
                return false;
            if (!CheckTagBool("AdminSpawnOnly", tag, ref desc.adminSpawnOnly))
                return false;
            if (!CheckTagListString("SandboxVariables", tag, ref desc.sandboxVariables))
                return false;
            if (!CheckTagDouble("MinSpawnFromWorldCenter", tag, ref desc.minSpawnFromWorldCenter))
                return false;
            if (!CheckTagDouble("MaxSpawnFromWorldCenter", tag, ref desc.maxSpawnFromWorldCenter))
                return false;
            if (!CheckTagBool("PlanetRequiresVacuum", tag, ref desc.planetRequiresVacuum))
                return false;
            if (!CheckTagBool("PlanetRequiresAtmo", tag, ref desc.planetRequiresAtmo))
                return false;
            if (!CheckTagBool("PlanetRequiresOxygen", tag, ref desc.planetRequiresOxygen))
                return false;
            if (!CheckTagDouble("PlanetMinimumSize", tag, ref desc.planetMinimumSize))
                return false;
            if (!CheckTagDouble("PlanetMaximumSize", tag, ref desc.planetMaximumSize))
                return false;
            if (!CheckTagBool("UseThreatLevelCheck", tag, ref desc.useThreatLevelCheck))
                return false;
            if (!CheckTagDouble("ThreatLevelCheckRange", tag, ref desc.threatLevelCheckRange))
                return false;
            if (!CheckTagBool("ThreatIncludeOtherNpcOwners", tag, ref desc.threatLevelCheckOtherNPCOwners))
                return false;
            if (!CheckTagDouble("ThreatScoreMinimum", tag, ref desc.threatScoreMinimum))
                return false;
            if (!CheckTagDouble("ThreatScoreMaximum", tag, ref desc.threatScoreMaximum))
                return false;
            if (!CheckTagBool("UsePCUCheck", tag, ref desc.usePCUCheck))
                return false;
            if (!CheckTagDouble("PCUCheckRadius", tag, ref desc.PCUCheckRadius))
                return false;
            if (!CheckTagDouble("PCUMinimum", tag, ref desc.PCUMinimum))
                return false;
            if (!CheckTagDouble("PCUMaximum", tag, ref desc.PCUMaximum))
                return false;
            if (!CheckTagBool("UsePlayerCountCheck", tag, ref desc.usePlayerCountCheck))
                return false;
            if (!CheckTagDouble("PlayerCountCheckRadius", tag, ref desc.playerCountCheckRadius))
                return false;
            if (!CheckTagInt("MinimumPlayers", tag, ref desc.minimumPlayers))
                return false;
            if (!CheckTagInt("MaximumPlayers", tag, ref desc.maximumPlayers))
                return false;
            if (!CheckTagBool("UsePlayerCredits", tag, ref desc.usePlayerCredits))
                return false;
            if (!CheckTagBool("IncludeAllPlayersInRadius", tag, ref desc.includeAllPlayersInRadius))
                return false;
            if (!CheckTagBool("IncludeFactionBalance", tag, ref desc.includeFactionBalance))
                return false;
            if (!CheckTagDouble("PlayerCreditsCheckRadius", tag, ref desc.playerCreditCheckRadius))
                return false;
            if (!CheckTagInt("MinimumPlayerCredits", tag, ref desc.minimumPlayerCredits))
                return false;
            if (!CheckTagInt("MaximumPlayerCredits", tag, ref desc.maximumPlayerCredits))
                return false;
            if (!CheckTagBool("UsePlayerFactionReputation", tag, ref desc.usePlayerFactionReputation))
                return false;
            if (!CheckTagDouble("PlayerReputationCheckRadius", tag, ref desc.playerReputationCheckRadius))
                return false;
            if (!CheckTagString("CheckReputationAgainstOtherNPCFaction", tag, ref desc.checkReputationAgainstOtherNPCFaction))
                return false;
            if (!CheckTagDouble("MinimumReputation", tag, ref desc.minimumReputation))
                return false;
            if (!CheckTagDouble("MaximumReputation", tag, ref desc.maximumReputation))
                return false;
            if (!CheckTagBool("AttachModStorageComponentToGrid", tag, ref desc.attachModStorageComponentToGrid))
                return false;
            if (!CheckTagString("StorageKey", tag, ref desc.storageKey))
                return false;
            if (!CheckTagString("StorageValue", tag, ref desc.storageValue))
                return false;
            if (!CheckTagListString("RequireAllMods", tag, ref desc.requireAllMods))
                return false;
            if (!CheckTagListString("RequireAnyMods", tag, ref desc.requireAnyMods))
                return false;
            if (!CheckTagListString("ExcludeAllMods", tag, ref desc.excludeAllMods))
                return false;
            if (!CheckTagListString("ExcludeAnyMods", tag, ref desc.excludeAnyMods))
                return false;
            if (!CheckTagListString("ModBlockExists", tag, ref desc.modBlockExists))
                return false;
            if (!CheckTagListInt("RequiredPlayersOnline", tag, ref desc.requiredPlayersOnline))
                return false;
            if (!CheckTagBool("UseKnownPlayerLocations", tag, ref desc.useKnowPlayerLocations))
                return false;
            if (!CheckTagBool("KnownPlayerLocationMustMatchFaction", tag, ref desc.knownPlayerLocationMustMatchFaction))
                return false;
            if (!CheckTagDouble("KnownPlayerLocationMinSpawnedEncounters", tag, ref desc.knownPlayerLocationMinSpawnedEncounters))
                return false;
            if (!CheckTagDouble("KnownPlayerLocationMaxSpawnedEncounters", tag, ref desc.knownPlayerLocationMaxSpawnedEncounters))
                return false;
            if (!CheckTagString("Territory", tag, ref desc.territory))
                return false;
            if (!CheckTagDouble("MinDistanceFromTerritoryCenter", tag, ref desc.minDistanceFromTerritoryCenter))
                return false;
            if (!CheckTagDouble("MaxDistanceFromTerritoryCenter", tag, ref desc.maxDistanceFromTerritoryCenter))
                return false;
            if (!CheckTagBool("RotateFirstCockpitToForward", tag, ref desc.rotateFirstCockpitToForward))
                return false;
            if (!CheckTagBool("SpawnRandomCargo", tag, ref desc.spawnRandomCargo))
                return false;
            if (!CheckTagBool("DisableDampeners", tag, ref desc.disableDampeners))
                return false;
            if (!CheckTagBool("ReactorsOn", tag, ref desc.reactorsOn))
                return false;
            #endregion
        }

        return true;
    }

    static bool CheckTagString(string name, string tag, ref string variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (!GetTagValueString(tag, name, out variable))
            {
                return false;
            }
        }

        return true;
    }
    static bool CheckTagInt(string name, string tag, ref int variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (!GetTagValueInt(tag, name, out variable))
            {
                return false;
            }
        }

        return true;
    }
    static bool CheckTagDouble(string name, string tag, ref double variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (!GetTagValueDouble(tag, name, out variable))
            {
                return false;
            }
        }
        
        return true;
    }
    static bool CheckTagBool(string name, string tag, ref bool variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (tag.Contains("true"))
                variable = true;
            else if (tag.Contains("false"))
                variable = false;
            else
            {
                return false;
            }
        }
        
        return true;
    }
    static bool CheckTagListBool(string name, string tag, ref List<bool> variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (!GetTagValueListBool(tag, name, out variable))
                return false;
        }

        return true;
    }
    static bool CheckTagListInt(string name, string tag, ref List<int> variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (!GetTagValueListInt(tag, name, out variable))
                return false;
        }

        return true;
    }
    static bool CheckTagListVector3(string name, string tag, ref List<Vector3> variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (!GetTagValueListVector3(tag, name, out variable))
                return false;
        }

        return true;
    }
    static bool CheckTagListString(string name, string tag, ref List<string> variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (!GetTagValueListString(tag, name, out variable))
                return false;
        }

        return true;
    }
    static bool CheckTagListColorReferencePair(string name, string tag, ref List<ColorReferencePair> variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (!GetTagValueListColorReferencePair(tag, name, out variable))
                return false;
        }

        return true;
    }
    static bool CheckTagListColorSkinReferencePair(string name, string tag, ref List<ColorSkinReferencePair> variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (!GetTagValueListColorSkinReferencePair(tag, name, out variable))
                return false;
        }

        return true;
    }
    static bool CheckTagListNamePair(string name, string tag, ref List<NamePair> variable)
    {
        if (tag.StartsWith($"\t\t\t[{name}"))
        {
            if (!GetTagValueListNamePair(tag, name, out variable))
                return false;
        }

        return true;
    }

    public static bool GetTagValueString(string Tag, string ValueName, out string Value)
    {
        Value = Tag.Replace(ValueName, "").Trim(TagElements);
        return true;
    }
    public static bool GetTagValueInt(string Tag, string ValueName, out int Value)
    {
        if (int.TryParse(Tag.Replace(ValueName, "").Trim(TagElements), out Value))
            return true;
        else
            return false;
    }
    public static bool GetTagValueDouble(string Tag, string ValueName, out double Value)
    {
        if (double.TryParse(Tag.Replace(ValueName, "").Trim(TagElements), out Value))
            return true;
        else
            return false;
    }

    public static bool GetTagValueListBool(string Tag, string ValueName, out List<bool> Value)
    {
        string[] values = Tag.Trim(TagElements).Replace(ValueName, "").Split(',');

        Value = new List<bool>();

        bool temp;

        foreach (string s in values)
        {
            if (!bool.TryParse(s, out temp))
                return false;
            else
                Value.Add(temp);
        }

        return true;
    }
    public static bool GetTagValueListVector3(string Tag, string ValueName, out List<Vector3> Value)
    {
        string[] values = Tag.Trim(TagElements).Replace(ValueName, "").Split(',');

        Value = new List<Vector3>();

        Vector3 temp;

        foreach (string s in values)
        {
            if (!ParseVector3(s, out temp))
                return false;
            else
                Value.Add(temp);
        }

        return true;
    }
    public static bool GetTagValueListInt(string Tag, string ValueName, out List<int> Value)
    {
        string[] values = Tag.Trim(TagElements).Replace(ValueName, "").Split(',');

        Value = new List<int>();

        int temp;

        foreach (string s in values)
        {
            if (!int.TryParse(s, out temp))
                return false;
            else
                Value.Add(temp);
        }

        return true;
    }
    public static bool GetTagValueListString(string Tag, string ValueName, out List<string> Value)
    {
        string[] values = Tag.Trim(TagElements).Replace(ValueName, "").Split(',');

        Value = new List<string>();

        foreach (string s in values)
        {
            Value.Add(s);
        }

        return true;
    }
    public static bool GetTagValueListColorReferencePair(string Tag, string ValueName, out List<ColorReferencePair> Value)
    {
        string[] values = Tag.Trim(TagElements).Replace(ValueName, "").Split(',');

        Value = new List<ColorReferencePair>();

        ColorReferencePair temp;

        foreach (string s in values)
        {
            if (!ParseColorReferencePair(s, out temp))
                return false;
            else
                Value.Add(temp);
        }

        return true;
    }
    public static bool GetTagValueListColorSkinReferencePair(string Tag, string ValueName, out List<ColorSkinReferencePair> Value)
    {
        string[] values = Tag.Trim(TagElements).Replace(ValueName, "").Split(',');

        Value = new List<ColorSkinReferencePair>();

        ColorSkinReferencePair temp;

        foreach (string s in values)
        {
            if (!ParseColorSkinReferencePair(s, out temp))
                return false;
            else
                Value.Add(temp);
        }

        return true;
    }
    public static bool GetTagValueListNamePair(string Tag, string ValueName, out List<NamePair> Value)
    {
        string[] values = Tag.Trim(TagElements).Replace(ValueName, "").Split(',');

        Value = new List<NamePair>();

        NamePair temp;

        foreach (string s in values)
        {
            if (!ParseNamePair(s, out temp))
                return false;
            else
                Value.Add(temp);
        }

        return true;
    }

    public static string Vector3ToMESFormat(Vector3 vector)
    {
        return "{X:" + vector.x + " Y:" + vector.y + " Z:" + vector.z + "}";
    }

    public static bool ParseVector3(string value, out Vector3 result)
    {
        result = new Vector3();

        string[] values = value.Trim(VectorElements).Split(':');

        if (!float.TryParse(values[0], out result.x))
            return false;

        if (!float.TryParse(values[1], out result.y))
            return false;

        if (!float.TryParse(values[2], out result.z))
            return false;

        return true;
    }
    public static bool ParseColorReferencePair(string value, out ColorReferencePair result)
    {
        result = new ColorReferencePair();

        string[] values = value.Split('|');

        if (!ParseVector3(values[0], out result.first))
            return false;
        if (!ParseVector3(values[1], out result.second))
            return false;


        return true;
    }
    public static bool ParseColorSkinReferencePair(string value, out ColorSkinReferencePair result)
    {
        result = new ColorSkinReferencePair();

        string[] values = value.Split('|');

        if (!ParseVector3(values[0], out result.color))
            return false;
        result.skin = values[1];


        return true;
    }
    public static bool ParseNamePair(string value, out NamePair result)
    {
        result = new NamePair();

        string[] values = value.Split('|');

        result.first = values[0];
        result.second = values[1];


        return true;
    }
}

public struct ColorReferencePair
{
    public Vector3 first;
    public Vector3 second;

    public override string ToString()
    {
        return "{"+$"X:{first.x} Y:{first.y} Z:{first.z}"+"}|{"+$"X:{second.x} Y:{second.y} Z:{second.z}"+"}";
    }
}
public struct ColorSkinReferencePair
{
    public Vector3 color;
    public string skin;

    public override string ToString()
    {
        return "{" + $"X:{color.x} Y:{color.y} Z:{color.z}" + "}|{" + skin + "}";
    }
}
public struct NamePair
{
    public string first;
    public string second;

    public override string ToString()
    {
        return $"{first}|{second}";
    }
}
