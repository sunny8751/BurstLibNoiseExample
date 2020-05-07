using UnityEngine;
using BurstLibNoise;
using BurstLibNoise.Operator;
using NodeEditorFramework.Utilities;
using UnityEditor;

namespace NodeEditorFramework.BurstLibNoiseEditor
{
	[Node(false, "Operator/Power")]
	public class PowerNode : BurstLibNoiseModule
	{
		public const string ID = "powerNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Power"; } }
		
		[ValueConnectionKnob("", Direction.In, "BurstModuleBase")]
		public ValueConnectionKnob inputModule1Knob;
		[ValueConnectionKnob("", Direction.In, "BurstModuleBase")]
		public ValueConnectionKnob inputModule2Knob;
		
		[ValueConnectionKnob("", Direction.Out, "BurstModuleBase")]
		public ValueConnectionKnob outputModuleKnob;

		public override void BurstLibNoiseNodeGUI()
		{
            inputModule1Knob.DisplayLayout();
            inputModule2Knob.DisplayLayout();
			outputModuleKnob.DisplayLayout();

			DrawTexture();
		}

		public override bool Calculate()
		{
            BurstModuleBase inputModule1 = inputModule1Knob.connected() ? inputModule1Knob.GetValue<BurstModuleBase>() : null;
            BurstModuleBase inputModule2 = inputModule2Knob.connected() ? inputModule2Knob.GetValue<BurstModuleBase>() : null;
            if (inputModule1 != null && inputModule2 != null) {
                Power module = new Power(inputModule1, inputModule2);
                outputModuleKnob.SetValue(module);
    			tex = GenerateTex(module);
            } else {
                tex = defaultTex;
            }
			return true;
		}

	}
}