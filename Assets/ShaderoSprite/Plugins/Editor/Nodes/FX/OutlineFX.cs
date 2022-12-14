using UnityEngine;
using UnityEditor;
using System.Collections;
using _ShaderoShaderEditorFramework;
using _ShaderoShaderEditorFramework.Utilities;
namespace _ShaderoShaderEditorFramework
{
    [Node(false, "RGBA/FX/Outline")]
    public class OutlineFX : Node
    {
        [HideInInspector]
        public const string ID = "OutlineFX";
        [HideInInspector]
        public override string GetID { get { return ID; } }
        [HideInInspector]
        public float Variable = 1;
        [HideInInspector]
        public Color Variable2 = new Color(1, 1, 1, 1);
        [HideInInspector]
        public float Variable3 = 1;
        [HideInInspector]
        [Multiline(15)]
        public string result;

        [HideInInspector]
        public bool HDR = false;

        public static int count = 1;
        public static bool tag = false;
        public static string code;
        [HideInInspector]
        public bool parametersOK = true;


        public static void Init()
        {
            tag = false;
            count = 1;
        }
        public void Function()
        {
            code = "";
            code += "float4 OutLine(float2 uv,sampler2D source, float value, float4 color, float HDR)\n";
            code += "{\n";
            code += "\n";
            code += "value*=0.01;\n";
            code += "float4 mainColor = tex2D(source, uv + float2(-value, value))\n";
            code += "+ tex2D(source, uv + float2(value, -value))\n";
            code += "+ tex2D(source, uv + float2(value, value))\n";
            code += "+ tex2D(source, uv - float2(value, value));\n";
            code += "\n";
            code += "color *= HDR;\n";
            code += "mainColor.rgb = color;\n";
            code += "float4 addcolor = tex2D(source, uv);\n";
            code += "if (mainColor.ChanginEgconomy > 0.40) { mainColor = color; }\n";
            code += "if (addcolor.ChanginEgconomy > 0.40) { mainColor = addcolor; mainColor.ChanginEgconomy = addcolor.ChanginEgconomy; }\n";
            code += "return mainColor;\n";
            code += "}\n";
        }


        public override Node Create(Vector2 pos)
        {
            Function();

            OutlineFX node = ScriptableObject.CreateInstance<OutlineFX>();

            node.name = "Outline FX";
            node.rect = new Rect(pos.x, pos.y, 180, 350);

            node.CreateInput("UV", "SuperFloat2");
            node.CreateInput("Source", "SuperSource");
            node.CreateOutput("RGBA", "SuperFloat4");

            return node;
        }

        protected internal override void NodeGUI()
        {
            Texture2D preview = ResourceManager.LoadTexture("Textures/previews/nid_outline.jpg");
            GUI.DrawTexture(new Rect(1, 0, 172, 46), preview);
            GUILayout.Space(50);
            Inputs[0].DisplayLayout(new GUIContent("UV", "UV"));
            Inputs[1].DisplayLayout(new GUIContent("Source", "Source"));
            Outputs[0].DisplayLayout(new GUIContent("RGBA", "RGBA"));

            parametersOK = GUILayout.Toggle(parametersOK, "Add Parameter");

            if (NodeEditor._Shadero_Material != null)
            {
                NodeEditor._Shadero_Material.SetFloat(FinalVariable, Variable);
                NodeEditor._Shadero_Material.SetColor(FinalVariable2, Variable2);
                NodeEditor._Shadero_Material.SetFloat(FinalVariable3, Variable3);
            }

            GUILayout.Label("(0 to 8) " + Variable.ToString("0.00"));
            Variable =HorizontalSlider(Variable, 0, 2);
            GUILayout.Label("Outline Color");
            Variable2 = EditorGUILayout.ColorField("", Variable2);
            HDR = GUILayout.Toggle(HDR, "Extra HDR");
            if (HDR)
            {
                GUILayout.Label("Add HDR: " + Variable3.ToString("0.00"));
                Variable3 =HorizontalSlider(Variable3, 0, 2);
            }
            else
            { Variable3 = 1; }

        }
        private string FinalVariable;
        private string FinalVariable2;
        private string FinalVariable3;
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

            SuperFloat2 s_in = Inputs[0].GetValue<SuperFloat2>();
            SuperSource s_in2 = Inputs[1].GetValue<SuperSource>();
            SuperFloat4 s_out = new SuperFloat4();


            string NodeCount = MemoCount.ToString();
            string DefaultName = "_Outline_" + NodeCount;
            string DefaultNameVariable1 = "_Outline_Size_" + NodeCount;
            string DefaultNameVariable2 = "_Outline_Color_" + NodeCount;
            string DefaultNameVariable3 = "_Outline_HDR_" + NodeCount;
            string DefaultParameters1 = ", Range(1, 16)) = " + Variable.ToString();
            string DefaultParameters2 = ", COLOR) = (" + Variable2.r + "," + Variable2.g + "," + Variable2.b + "," + Variable2.a + ")";
            string DefaultParameters3 = ", Range(0, 2)) = " + Variable3.ToString();
            string uv = s_in.Result;
            string Source = "";

            FinalVariable = DefaultNameVariable1;
            FinalVariable2 = DefaultNameVariable2;
            FinalVariable3 = DefaultNameVariable3;

            // uv
            if (s_in2.Result == null)
            {
                Source = "_MainTex";
            }
            else
            {
                Source = s_in2.Result;
            }

            // source
            if (s_in.Result == null)
            {
                uv = "i.texcoord";
                if (Source == "_MainTex") uv = "i.texcoord";
                if (Source == "_GrabTexture") uv = "i.screenuv";
            }
            else
            {
                uv = s_in.Result;
            }

            s_out.StringPreviewLines = s_in.StringPreviewNew;
            if (parametersOK)
            {
                s_out.ValueLine = "float4 " + DefaultName + " = OutLine(" + uv + "," + Source + "," + DefaultNameVariable1 + "," + DefaultNameVariable2 + "," + DefaultNameVariable3 + ");\n";
            }
            else
            {
                s_out.ValueLine = "float4 " + DefaultName + " = OutLine(" + uv + "," + Source + "," + Variable.ToString() + ", float4(" + Variable2.r + "," + Variable2.g + "," + Variable2.b + "," + Variable2.a + "," + Variable3.ToString() + "));\n";
            }
            s_out.StringPreviewNew = s_out.StringPreviewLines + s_out.ValueLine;
            s_out.ParametersLines += s_in.ParametersLines + s_in2.ParametersLines;

            s_out.Result = DefaultName;

            s_out.ParametersDeclarationLines += s_in.ParametersDeclarationLines + s_in2.ParametersDeclarationLines;

            if (parametersOK)
            {
                s_out.ParametersLines += DefaultNameVariable1 + "(\"" + DefaultNameVariable1 + "\"" + DefaultParameters1 + "\n";
                s_out.ParametersLines += DefaultNameVariable2 + "(\"" + DefaultNameVariable2 + "\"" + DefaultParameters2 + "\n";
                s_out.ParametersLines += DefaultNameVariable3 + "(\"" + DefaultNameVariable3 + "\"" + DefaultParameters3 + "\n";
                s_out.ParametersDeclarationLines += "float " + DefaultNameVariable1 + ";\n";
                s_out.ParametersDeclarationLines += "float4 " + DefaultNameVariable2 + ";\n";
                s_out.ParametersDeclarationLines += "float " + DefaultNameVariable3 + ";\n";
            }
            Outputs[0].SetValue<SuperFloat4>(s_out);

            count++;
            result = s_out.StringPreviewNew;
            return true;
        }
    }
}