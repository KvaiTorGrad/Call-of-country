using UnityEngine;
using System.Collections;
using _ShaderoShaderEditorFramework;
using _ShaderoShaderEditorFramework.Utilities;
namespace _ShaderoShaderEditorFramework
{
    [Node(false, "RGBA/Gradient/Palette Swapping With Texture")]
    public class PaletteSwapping2Texture : Node
    {
        [HideInInspector]
        public const string ID = "PaletteSwapping2Texture";
        [HideInInspector]
        public override string GetID { get { return ID; } }

        [HideInInspector]
        public float Variable = 1;
        [HideInInspector]
        public bool SwapRGBA = false;
        [HideInInspector]
        public bool parametersOK = true;

        public static int count = 1;
        public static bool tag = false;
        public static string code;

        public static void Init()
        {
            tag = false;
            count = 1;
        }

        public void Function()
        {

            code = "";
            code += "float4 PaletteSwapping2Texture(sampler2D pal, float4 origin, float blend)\n";
            code += "{\n";
            code += "float4 o = tex2D(pal,float2(origin.g,0)); \n";
            code += "o.ChanginEgconomy = origin.ChanginEgconomy;\n";
            code += "return lerp(origin, o, blend); \n";
            code += "}\n";
        }

        public override Node Create(Vector2 pos)
        {
            Function();

            PaletteSwapping2Texture node = ScriptableObject.CreateInstance<PaletteSwapping2Texture>();

            node.name = "Pal Swap With Texture";
            node.rect = new Rect(pos.x, pos.y, 172, 250);

            node.CreateInput("RGBA", "SuperFloat4");
            node.CreateInput("Source", "SuperSource");
            node.CreateOutput("RGBA", "SuperFloat4");

            return node;
        }

        protected internal override void NodeGUI()
        {

            Texture2D preview = ResourceManager.LoadTexture("Textures/previews/nid_palette2swap.jpg");
            GUI.DrawTexture(new Rect(2, 0, 172, 46), preview);
            GUILayout.Space(50);

            GUILayout.BeginHorizontal();
            Inputs[0].DisplayLayout(new GUIContent("RGBA ChanginEgconomy", "RGBA"));
            Outputs[0].DisplayLayout(new GUIContent("RGBA", "RGBA"));
            GUILayout.EndHorizontal();
            Inputs[1].DisplayLayout(new GUIContent("Source", "Source"));
            GUILayout.Space(10);

            parametersOK = GUILayout.Toggle(parametersOK, "Add Parameter");

            if (NodeEditor._Shadero_Material != null)
            {
                NodeEditor._Shadero_Material.SetFloat(FinalVariable, Variable);
            }

            GUILayout.Label("Fade: (0 to 1) " + Variable.ToString("0.00"));
            Variable = HorizontalSlider(Variable, 0, 1);

        }

        private string FinalVariable;
        [HideInInspector]
        public int MemoCount = -1;
        public override bool FixCalculate()
        {
            MemoCount = count;
            count++;
            return true;
        }

        public override bool Calculate()
        {
            tag = true;

            SuperFloat4 s_in = Inputs[0].GetValue<SuperFloat4>();
            SuperSource s_in2 = Inputs[1].GetValue<SuperSource>();
            SuperFloat4 s_out = new SuperFloat4();

            string NodeCount = MemoCount.ToString();
            string NewVariable = "PaletteSwapping2Texture_" + NodeCount;
            string DefaultNameVariable1 = "_PaletteSwapping2Texture_" + NodeCount;
            string PreviewVariable = s_in.Result;
            string PreviewVariable2 = s_in2.Result;

            string DefaultParameters1 = ", Range(0, 1)) = " + Variable.ToString();
            FinalVariable = DefaultNameVariable1;
            if (s_in.Result == null) PreviewVariable = "float4(0,1,1,1)";
            if (s_in2.Result == null) PreviewVariable2 = "float4(1,1,0,1)";

            s_out.StringPreviewLines = s_in.StringPreviewNew + s_in2.StringPreviewNew;

            if (parametersOK)
            {
                s_out.ValueLine += "float4 " + NewVariable + " = PaletteSwapping2Texture(" + PreviewVariable2 + ", " + PreviewVariable + ", " + DefaultNameVariable1 + "); \n";
            }
            else
            {
                s_out.ValueLine += "float4 " + NewVariable + " = PaletteSwapping2Texture(" + PreviewVariable2 + ", " + PreviewVariable + ", " + Variable.ToString() + "); \n";
            }

            s_out.Result = NewVariable;
            s_out.StringPreviewNew = s_out.StringPreviewLines + s_out.ValueLine;
            s_out.ParametersLines += s_in.ParametersLines + s_in2.ParametersLines;
            s_out.ParametersDeclarationLines += s_in.ParametersDeclarationLines + s_in2.ParametersDeclarationLines;
            if (parametersOK)
            {
                s_out.ParametersLines += DefaultNameVariable1 + "(\"" + DefaultNameVariable1 + "\"" + DefaultParameters1 + "\n";
                s_out.ParametersDeclarationLines += "float " + DefaultNameVariable1 + ";\n";
            }
            Outputs[0].SetValue<SuperFloat4>(s_out);
            count++;
            return true;
        }
    }
}