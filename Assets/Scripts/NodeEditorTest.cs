using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using BurstLibNoise;
using BurstLibNoise.Generator;
using BurstLibNoise.Operator;

public class NodeEditorTest : MonoBehaviour
{
	[SerializeField] int Width = 0;

	[SerializeField] int Height = 0;
    
    // Get noise settings from node editor
    public NoiseSettings noiseSettings;

    private void Start() {
        if (noiseSettings == null) {
            Debug.LogError("Set noiseSettings to be the generated output of Window->Node Editor's Ouput node!");
            return;
        }
        Debug.Log(noiseSettings.moduleData[0].type);

        float start = Time.realtimeSinceStartup;
        RenderAndSetImage(noiseSettings);
        Debug.Log("BurstLibNoise runtime: " + (Time.realtimeSinceStartup - start));
    }

	void RenderAndSetImage(NoiseSettings noiseSettings)
	{
		var heightMapBuilder = new Noise2D(Width, Height, noiseSettings);
        heightMapBuilder.GeneratePlanar(Noise2D.Left, Noise2D.Right, Noise2D.Top, Noise2D.Bottom);
		// heightMapBuilder.GenerateSpherical(90, -90, -180, 180);
        // heightMapBuilder.GenerateCylindrical(-180, 180, -1, 1);
		var image = heightMapBuilder.GetTexture();
		GetComponent<Renderer>().material.mainTexture = image;

        heightMapBuilder.Dispose();
	}
}
