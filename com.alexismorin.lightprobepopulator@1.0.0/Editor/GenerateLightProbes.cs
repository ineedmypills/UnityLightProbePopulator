#if !COMPILER_UDONSHARP && UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LightProbePopulation
{
    public class GenerateLightProbes
    {
        private const string PREFIX = "[LightProbePopulation]";

        #region Settings
        private static bool CalculateStaticOnly = true;

        [MenuItem("Tools/LightProbePopulation/Settings/DisableStaticOnly")]
        private static void DisableStaticOnly()
        {
            Debug.Log($"{PREFIX} DisableStaticOnly");
            CalculateStaticOnly = false;
        }

        [MenuItem("Tools/LightProbePopulation/Settings/EnableStaticOnly")]
        private static void EnableStaticOnly()
        {
            Debug.Log($"{PREFIX} EnableStaticOnly");
            CalculateStaticOnly = false;
        }

        #endregion

        #region Generator Options

        [MenuItem("Tools/LightProbePopulation/Generate/Low Resolution")]
        private static void Generate_Low()
        {
            Debug.Log($"{PREFIX} Generate_Low");
            CleanupProbes();
            List<Vector3> probeLocations = new List<Vector3>();
            LightProbeGroup lightProbeGroup = CreateProbe();
            GameObject[] objectsInScene = CollectObjectsInScene(probeLocations, false);
            lightProbeGroup.GetComponent<LightProbeGroup>().probePositions = probeLocations.ToArray();
        }

        [MenuItem("Tools/LightProbePopulation/Generate/Medium Resolution")]
        private static void Generate_Medium()
        {
            Debug.Log($"{PREFIX} Generate_Medium");
            CleanupProbes();
            List<Vector3> probeLocations = new List<Vector3>();
            LightProbeGroup lightProbeGroup = CreateProbe();
            GameObject[] objectsInScene = CollectObjectsInScene(probeLocations);
            lightProbeGroup.GetComponent<LightProbeGroup>().probePositions = probeLocations.ToArray();
        }

        [MenuItem("Tools/LightProbePopulation/Generate/High Resolution")]
        private static void Generate_High()
        {
            Debug.Log($"{PREFIX} Generate_High");
            CleanupProbes();
            List<Vector3> probeLocations = new List<Vector3>();
            LightProbeGroup lightProbeGroup = CreateProbe();
            GameObject[] objectsInScene = CollectObjectsInScene(probeLocations);
            probeLocations = SubdivideProbes(probeLocations, 2);
            lightProbeGroup.GetComponent<LightProbeGroup>().probePositions = probeLocations.ToArray();
        }

        [MenuItem("Tools/LightProbePopulation/Generate/Very High Resolution")]
        private static void Generate_VeryHigh()
        {
            Debug.Log($"{PREFIX} Generate_VeryHigh");
            CleanupProbes();
            List<Vector3> probeLocations = new List<Vector3>();
            LightProbeGroup lightProbeGroup = CreateProbe();
            GameObject[] objectsInScene = CollectObjectsInScene(probeLocations);
            probeLocations = SubdivideProbes(probeLocations, 4);
            lightProbeGroup.probePositions = probeLocations.ToArray();
        }

        #endregion

        #region Generator Core
        private static List<Vector3> SubdivideProbes(List<Vector3> probeLocations, int multiplier)
        {
            Debug.Log($"{PREFIX} SubdivideProbes. in '{probeLocations.Count}' with multiplier '{multiplier}'");
            int boundProbes = probeLocations.Count * multiplier;
            for (int i = 0; i < boundProbes; i++)
            {
                probeLocations.Add(Vector3.Lerp(
                        probeLocations[Random.Range(0, boundProbes / multiplier)],
                        probeLocations[Random.Range(0, boundProbes / multiplier)],
                        0.5f));
            }

            return probeLocations;
        }

        private static void CleanupProbes()
        {
            var components = GameObject.FindObjectsByType<LightProbeGroup>(FindObjectsSortMode.None);
            Debug.Log($"{PREFIX} CleanupProbes. Found '{components.Length}' probes in scene.");

            foreach (var component in components)
            {
                component.enabled = false;
                Debug.Log($"{PREFIX} Disabled probe '{component.gameObject.name}' with '{component.probePositions.Length}' positions");
                // prob delete it
            }
        }

        private static LightProbeGroup CreateProbe()
        {
            Debug.Log($"{PREFIX} CreateProbe");
            var lightProbes = new GameObject("Light Probe Group");
            var component = lightProbes.AddComponent<LightProbeGroup>();
            return component;
        }

        private static GameObject[] CollectObjectsInScene(List<Vector3> probeLocations, bool min = true, bool max = true)
        {
            GameObject[] objectsInScene = Object.FindObjectsOfType<GameObject>();
            Renderer temp;
            foreach (GameObject obj in objectsInScene)
            {
                if (CalculateStaticOnly && !obj.isStatic) continue;
                temp = obj.GetComponent<Renderer>();
                if (!temp) continue;
                if (min) probeLocations.Add(temp.bounds.max);
                if (max) probeLocations.Add(temp.bounds.min);
            }

            Debug.Log($"{PREFIX} CollectObjectsInScene '{objectsInScene.Length}'");
            return objectsInScene;
        }

        #endregion
    }
#endif
}