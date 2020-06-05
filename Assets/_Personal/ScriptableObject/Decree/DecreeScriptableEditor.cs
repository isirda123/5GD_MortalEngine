


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


#if UNITY_EDITOR
[CustomEditor(typeof(DecreeScriptable))]
public class DecreeScriptableEditor : Editor
{
    private bool typeOfDecreeFolder,
        titleFolder,
        flavorTextFolder,
        maxMouffluFlatFolder,
        maxRockFlatFolder,
        maxWoodFlatFolder,
        maxBerryFlatFolder,
        consumptionFoodFlatFolder,
        consumptionEnergyFlatFolder,
        consumptionBuildFlatFolder,
        collectRangeMaxFolder,
        giveMoufluFolder,
        giveRockFolder,
        giveWoodFolder,
        giveBerryFolder,
        collectQuantityMoufluFolder,
        collectQuantityRockFolder,
        collectQuantityWoodFolder,
        collectQuantityBerryFolder,
        numberOfMoveFolder,
        flyFolder,
        roundBetweenDecreeFolder = true;


    void OnEnable()
    {
        typeOfDecreeFolder=
        titleFolder=
        flavorTextFolder=
        maxMouffluFlatFolder=
        maxRockFlatFolder=
        maxWoodFlatFolder=
        maxBerryFlatFolder=
        consumptionFoodFlatFolder =
        consumptionEnergyFlatFolder=
        consumptionBuildFlatFolder=
        collectRangeMaxFolder=
        giveMoufluFolder=
        giveRockFolder=
        giveWoodFolder=
        giveBerryFolder=
        collectQuantityMoufluFolder=
        collectQuantityRockFolder=
        collectQuantityWoodFolder=
        collectQuantityBerryFolder=
        numberOfMoveFolder=
        flyFolder=
        roundBetweenDecreeFolder=true;
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

        if (manager.CheckField(DecreeScriptable.TargetFields.maxMouffluFlat))
        {
            maxMouffluFlatFolder = EditorGUILayout.BeginFoldoutHeaderGroup(maxMouffluFlatFolder, "Max Moufflu");
            if (maxMouffluFlatFolder)
            {
                manager.decretsInfos.maxMouffluFlat = EditorGUILayout.IntField("Flat", manager.decretsInfos.maxMouffluFlat);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.maxRockFlat))
        {
            maxRockFlatFolder = EditorGUILayout.BeginFoldoutHeaderGroup(maxRockFlatFolder, "Max Rock");
            if (maxRockFlatFolder)
            {
                manager.decretsInfos.maxRockFlat = EditorGUILayout.IntField("Flat", manager.decretsInfos.maxRockFlat);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.maxWoodFlat))
        {
            maxWoodFlatFolder = EditorGUILayout.BeginFoldoutHeaderGroup(maxWoodFlatFolder, "Max Wood");
            if (maxWoodFlatFolder)
            {
                manager.decretsInfos.maxWoodFlat = EditorGUILayout.IntField("Flat", manager.decretsInfos.maxWoodFlat);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.maxBerryFlat))
        {
            maxBerryFlatFolder = EditorGUILayout.BeginFoldoutHeaderGroup(maxBerryFlatFolder, "Max Berry");
            if (maxBerryFlatFolder)
            {
                manager.decretsInfos.maxBerryFlat = EditorGUILayout.IntField("Flat", manager.decretsInfos.maxBerryFlat);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.consumptionFoodFlat))
        {
            consumptionFoodFlatFolder = EditorGUILayout.BeginFoldoutHeaderGroup(consumptionFoodFlatFolder, "Consumption Food");
            if (consumptionFoodFlatFolder)
            {
                manager.decretsInfos.consumptionFoodFlat = EditorGUILayout.IntField("Flat", manager.decretsInfos.consumptionFoodFlat);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.consumptionEnergyFlat))
        {
            consumptionEnergyFlatFolder = EditorGUILayout.BeginFoldoutHeaderGroup(consumptionEnergyFlatFolder, "Consumption Energy");
            if (consumptionEnergyFlatFolder)
            {
                manager.decretsInfos.consumptionEnergyFlat = EditorGUILayout.IntField("Flat", manager.decretsInfos.consumptionEnergyFlat);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.consumptionBuildFlat))
        {
            consumptionBuildFlatFolder = EditorGUILayout.BeginFoldoutHeaderGroup(consumptionBuildFlatFolder, "Consumption Build");
            if (consumptionBuildFlatFolder)
            {
                manager.decretsInfos.consumptionBuildFlat = EditorGUILayout.IntField("Flat", manager.decretsInfos.consumptionBuildFlat);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.collectRangeMax))
        {
            collectRangeMaxFolder = EditorGUILayout.BeginFoldoutHeaderGroup(collectRangeMaxFolder, "Range Of Collect");
            if (collectRangeMaxFolder)
            {
                manager.decretsInfos.collectRangeMax = EditorGUILayout.IntField("Flat", manager.decretsInfos.collectRangeMax);
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

        if (manager.CheckField(DecreeScriptable.TargetFields.numberOfMove))
        {
            numberOfMoveFolder = EditorGUILayout.BeginFoldoutHeaderGroup(numberOfMoveFolder, "Number of Move per round");
            if (numberOfMoveFolder)
            {
                manager.decretsInfos.numberOfMove = EditorGUILayout.IntField("Flat", manager.decretsInfos.numberOfMove);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.fly))
        {
            flyFolder = EditorGUILayout.BeginFoldoutHeaderGroup(flyFolder, "Fly to avoid Blockers");
            if (numberOfMoveFolder)
            {
                manager.decretsInfos.fly = EditorGUILayout.IntField("0 false 1 true", manager.decretsInfos.fly);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }

        if (manager.CheckField(DecreeScriptable.TargetFields.roundBetweenDecree))
        {
            roundBetweenDecreeFolder = EditorGUILayout.BeginFoldoutHeaderGroup(roundBetweenDecreeFolder, "Decrease time between Decree");
            if (roundBetweenDecreeFolder)
            {
                manager.decretsInfos.roundBetweenDecree = EditorGUILayout.IntField("0 false 1 true", manager.decretsInfos.roundBetweenDecree);
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
#endif
