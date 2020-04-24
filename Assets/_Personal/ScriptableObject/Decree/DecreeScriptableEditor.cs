


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


[CustomEditor(typeof(DecreeScriptable))]

public class DecreeScriptableEditor : Editor
{
    private bool typeOfDecreeFolder,
        titleFolder,
        flavorTextFolder,
        maxFoodPercentFolder,
        maxEnergyPercentFolder,
        maxConstructionPercentFolder,
        consumptionFoodPercentFolder,
        consumptionEnergyPercentFolder,
        consumptionBuildPercentFolder,
        speedPercentFolder,
        collectSpeedPercentFolder,
        collectRangeMaxFolder,
        giveMoufluFolder,
        giveRockFolder,
        giveWoodFolder,
        giveBerryFolder,
        collectQuantityMoufluFolder,
        collectQuantityRockFolder,
        collectQuantityWoodFolder,
        collectQuantityBerryFolder,
        speedRespawnMoufluPercentFolder,
        speedRespawnRockPercentFolder,
        speedRespawnWoodPercentFolder,
        speedRespawnBerryPercentFolder = true;


    void OnEnable()
    {
        typeOfDecreeFolder=
        titleFolder=
        flavorTextFolder=
        maxFoodPercentFolder=
        maxEnergyPercentFolder=
        maxConstructionPercentFolder=
        consumptionFoodPercentFolder=
        consumptionEnergyPercentFolder=
        consumptionBuildPercentFolder=
        speedPercentFolder=
        collectSpeedPercentFolder=
        collectRangeMaxFolder=
        giveMoufluFolder=
        giveRockFolder=
        giveWoodFolder=
        giveBerryFolder=
        collectQuantityMoufluFolder=
        collectQuantityRockFolder=
        collectQuantityWoodFolder=
        collectQuantityBerryFolder=
        speedRespawnMoufluPercentFolder=
        speedRespawnRockPercentFolder=
        speedRespawnWoodPercentFolder=
        speedRespawnBerryPercentFolder = true;
    }

    public override void OnInspectorGUI()
    {
        var manager = (DecreeScriptable)target;
        serializedObject.Update();

        manager.targetField = (DecreeScriptable.TargetFields)EditorGUILayout.EnumFlagsField("Decree Infos", manager.targetField);

        if (manager.CheckField(DecreeScriptable.TargetFields.typeOfDecree))
        {
            typeOfDecreeFolder = EditorGUILayout.BeginFoldoutHeaderGroup(typeOfDecreeFolder, "Type Of Decree");
            if (typeOfDecreeFolder)
            {
                manager.decretsInfos.myTypeOfDecree = (DecretsInfos.typeOfDecree)EditorGUILayout.EnumPopup("Type Of Decree", manager.decretsInfos.myTypeOfDecree);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }


        if (manager.CheckField(DecreeScriptable.TargetFields.title))
        {
            titleFolder = EditorGUILayout.BeginFoldoutHeaderGroup(titleFolder, "Title");
            if (titleFolder)
            {
                manager.decretsInfos.title = EditorGUILayout.TextField("Title", manager.decretsInfos.title);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.flavorText))
        {
            flavorTextFolder = EditorGUILayout.BeginFoldoutHeaderGroup(flavorTextFolder, "Flavor Text");
            if (flavorTextFolder)
            {
                manager.decretsInfos.flavorText = EditorGUILayout.TextField("Flavor Text", manager.decretsInfos.flavorText);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.maxFoodPercent))
        {
            maxFoodPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(maxFoodPercentFolder, "Max Food");
            if (maxFoodPercentFolder)
            {
                manager.decretsInfos.maxFoodPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.maxFoodPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.maxEnergyPercent))
        {
            maxEnergyPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(maxEnergyPercentFolder, "Max Energy");
            if (maxEnergyPercentFolder)
            {
                manager.decretsInfos.maxEnergyPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.maxEnergyPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.maxConstructionPercent))
        {
            maxConstructionPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(maxConstructionPercentFolder, "Max Construction");
            if (maxConstructionPercentFolder)
            {
                manager.decretsInfos.maxConstructionPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.maxConstructionPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.consumptionFoodPercent))
        {
            consumptionFoodPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(consumptionFoodPercentFolder, "Consumption Food");
            if (consumptionFoodPercentFolder)
            {
                manager.decretsInfos.consumptionFoodPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.consumptionFoodPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.consumptionEnergyPercent))
        {
            consumptionEnergyPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(consumptionEnergyPercentFolder, "Consumption Energy");
            if (consumptionEnergyPercentFolder)
            {
                manager.decretsInfos.consumptionEnergyPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.consumptionEnergyPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.consumptionBuildPercent))
        {
            consumptionBuildPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(consumptionBuildPercentFolder, "Consumption Build");
            if (consumptionBuildPercentFolder)
            {
                manager.decretsInfos.consumptionBuildPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.consumptionBuildPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.speedPercent))
        {
            speedPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(speedPercentFolder, "Speed Of Avatar");
            if (speedPercentFolder)
            {
                manager.decretsInfos.speedPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.speedPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.collectSpeedPercent))
        {
            collectSpeedPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(collectSpeedPercentFolder, "Speed Of Collect");
            if (collectSpeedPercentFolder)
            {
                manager.decretsInfos.collectSpeedPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.collectSpeedPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.collectRangeMax))
        {
            collectRangeMaxFolder = EditorGUILayout.BeginFoldoutHeaderGroup(collectRangeMaxFolder, "Range Of Collect");
            if (collectRangeMaxFolder)
            {
                manager.decretsInfos.collectRangeMax = EditorGUILayout.IntField("Percent", manager.decretsInfos.collectRangeMax);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.giveMouflu))
        {
            giveMoufluFolder = EditorGUILayout.BeginFoldoutHeaderGroup(giveMoufluFolder, "Give Mouflu");
            if (giveMoufluFolder)
            {
                manager.decretsInfos.giveMouflu = EditorGUILayout.IntField("Flat", manager.decretsInfos.giveMouflu);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.giveRock))
        {
            giveRockFolder = EditorGUILayout.BeginFoldoutHeaderGroup(giveRockFolder, "Give Rock");
            if (giveRockFolder)
            {
                manager.decretsInfos.giveRock = EditorGUILayout.IntField("Flat", manager.decretsInfos.giveRock);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.giveWood))
        {
            giveWoodFolder = EditorGUILayout.BeginFoldoutHeaderGroup(giveWoodFolder, "Give Wood");
            if (giveWoodFolder)
            {
                manager.decretsInfos.giveWood = EditorGUILayout.IntField("Flat", manager.decretsInfos.giveWood);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.giveBerry))
        {
            giveBerryFolder = EditorGUILayout.BeginFoldoutHeaderGroup(giveBerryFolder, "Give Berry");
            if (giveBerryFolder)
            {
                manager.decretsInfos.giveBerry = EditorGUILayout.IntField("Flat", manager.decretsInfos.giveBerry);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.collectQuantityMouflu))
        {
            collectQuantityMoufluFolder = EditorGUILayout.BeginFoldoutHeaderGroup(collectQuantityMoufluFolder, "Collect Mouflu per square");
            if (collectQuantityMoufluFolder)
            {
                manager.decretsInfos.collectQuantityMouflu = EditorGUILayout.IntField("Flat", manager.decretsInfos.collectQuantityMouflu);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.collectQuantityRock))
        {
            collectQuantityRockFolder = EditorGUILayout.BeginFoldoutHeaderGroup(collectQuantityRockFolder, "Collect Rock per square");
            if (collectQuantityRockFolder)
            {
                manager.decretsInfos.collectQuantityRock = EditorGUILayout.IntField("Flat", manager.decretsInfos.collectQuantityRock);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.collectQuantityWood))
        {
            collectQuantityWoodFolder = EditorGUILayout.BeginFoldoutHeaderGroup(collectQuantityWoodFolder, "Collect Wood per square");
            if (collectQuantityWoodFolder)
            {
                manager.decretsInfos.collectQuantityWood = EditorGUILayout.IntField("Flat", manager.decretsInfos.collectQuantityWood);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.collectQuantityBerry))
        {
            collectQuantityBerryFolder = EditorGUILayout.BeginFoldoutHeaderGroup(collectQuantityBerryFolder, "Collect Berry per square");
            if (collectQuantityBerryFolder)
            {
                manager.decretsInfos.collectQuantityBerry = EditorGUILayout.IntField("Flat", manager.decretsInfos.collectQuantityBerry);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.speedRespawnMoufluPercent))
        {
            speedRespawnMoufluPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(speedRespawnMoufluPercentFolder, "Respawn Speed Mouflu");
            if (speedRespawnMoufluPercentFolder)
            {
                manager.decretsInfos.speedRespawnMoufluPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.speedRespawnMoufluPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.speedRespawnRockPercent))
        {
            speedRespawnRockPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(speedRespawnRockPercentFolder, "Respawn Speed Rock");
            if (speedRespawnRockPercentFolder)
            {
                manager.decretsInfos.speedRespawnRockPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.speedRespawnRockPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.speedRespawnWoodPercent))
        {
            speedRespawnWoodPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(speedRespawnWoodPercentFolder, "Respawn Speed Wood");
            if (speedRespawnWoodPercentFolder)
            {
                manager.decretsInfos.speedRespawnWoodPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.speedRespawnWoodPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.speedRespawnBerryPercent))
        {
            speedRespawnBerryPercentFolder = EditorGUILayout.BeginFoldoutHeaderGroup(speedRespawnBerryPercentFolder, "Respawn Speed Berry");
            if (speedRespawnBerryPercentFolder)
            {
                manager.decretsInfos.speedRespawnBerryPercent = EditorGUILayout.IntField("Percent", manager.decretsInfos.speedRespawnBerryPercent);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }



        serializedObject.ApplyModifiedProperties();
        AssetDatabase.SaveAssets();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(manager);
        }

    }
}

