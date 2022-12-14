using UnityEngine;
using System.Collections;
using _ShaderoShaderEditorFramework;
using _ShaderoShaderEditorFramework.Utilities;
namespace _ShaderoShaderEditorFramework
{

    [Node(false, "UV/Animated/Shake UV")]

    public class AnimatedShakeUV : Node
    {
        public const string ID = "AnimatedShakeUV";
        public override string GetID { get { return ID; } }
        [HideInInspector]
        public float Variable = 0;
        [HideInInspector]
        public float Variable2 = 0;
        [HideInInspector]
        public float Variable3 = 1;
        [HideInInspector]
        public float Variable4 = 1;
        [HideInInspector]
        public float Variable5 = 0.1f;
        [HideInInspector]
        public bool AddParameters = true;

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
            code += "float2 AnimatedShakeUV(float2 uv, float offsetx, float offsety, float zoomx, float zoomy, float speed)\n";
            code += "{\n";
            code += "float time = sin(_Time * speed * 5000 * zoomx);\n";
            code += "float time2 = sin(_Time * speed * 5000 * zoomy);\n";
            code += "uv += float2(offsetx * time, offsety * time2);\n";
            code += "return uv;\n";
            code += "}\n";
        }
        public override Node Create(Vector2 pos)
        {
            Function();

            AnimatedShakeUV node = ScriptableObject.CreateInstance<AnimatedShakeUV>();

            node.name = "Animated Shake UV";
            node.rect = new Rect(pos.x, pos.y, 172, 380);
            node.CreateInput("UV", "SuperFloat2");
            node.CreateOutput("UV", "SuperFloat2");

            return node;
        }

        protected internal override void NodeGUI()
        {
            Texture2D preview = ResourceManager.LoadTexture("Textures/previews/nid_anm_ping_pong.jpg");
            GUI.DrawTexture(new Rect(2, 0, 172, 46), preview);
            GUILayout.Space(50);
            GUILayout.BeginHorizontal();
            Inputs[0].DisplayLayout(new GUIContent("UV", "Link with ChanginEgconomy UV"));
            Outputs[0].DisplayLayout(new GUIContent("UV", "The input UV"));
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Reset"))
            {
                Variable = 0;
                Variable2 = 0;
                Variable3 = 1;
                Variable4 = 1;
                Variable5 = 0.1f;
            }

            if (NodeEditor._Shadero_Material != null)
            {
                NodeEditor._Shadero_Material.SetFloat(FinalVariable1, Variable);
                NodeEditor._Shadero_Material.SetFloat(FinalVariable2, Variable2);
                NodeEditor._Shadero_Material.SetFloat(FinalVariable3, Variable3);
                NodeEditor._Shadero_Material.SetFloat(FinalVariable4, Variable4);
                NodeEditor._Shadero_Material.SetFloat(FinalVariable5, Variable5);
            }

            AddParameters = GUILayout.Toggle(AddParameters, "Add Parameters");

            GUILayout.Label("Offset X (0 to 0.05) " + Variable.ToString("0.00"));
            Variable =HorizontalSlider(Variable, 0f, 0.05f);
            GUILayout.Label("Offset Y (0 to 0.05) " + Variable2.ToString("0.00"));
            Variable2 =HorizontalSlider(Variable2, 0f, 0.05f);
            GUILayout.Label("Intensity X (-3 to 3) " + Variable3.ToString("0.00"));
            Variable3 =HorizontalSlider(Variable3, -3, 3);
            GUILayout.Label("Intensity Y (-3 to 3) " + Variable4.ToString("0.00"));
            Variable4 =HorizontalSlider(Variable4, -3, 3);
            GUILayout.Label("Speed (-1 to 1) " + Variable5.ToString("0.00"));
            Variable5 =HorizontalSlider(Variable5, -1, 1);


        }

        private string FinalVariable1;
        private string FinalVariable2;
        private string FinalVariable3;
        private string FinalVariable4;
        private string FinalVariable5;
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
            SuperFloat2 s_out = new SuperFloat2();



            string NodeCount = MemoCount.ToString();
            string DefaultName = "AnimatedShakeUV_" + NodeCount;
            string DefaultNameVariable1 = DefaultName + "_OffsetX_" + NodeCount;
            string DefaultNameVariable2 = DefaultName + "_OffsetY_" + NodeCount;
            string DefaultNameVariable3 = DefaultName + "_IntenseX_" + NodeCount;
            string DefaultNameVariable4 = DefaultName + "_IntenseY_" + NodeCount;
            string DefaultNameVariable5 = DefaultName + "_Speed_" + NodeCount;
            string DefaultParameters1 = ", Range(0, 0.05)) = " + Variable.ToString();
            string DefaultParameters2 = ", Range(0, 0.05)) = " + Variable2.ToString();
            string DefaultParameters3 = ", Range(-3, 3)) = " + Variable3.ToString();
            string DefaultParameters4 = ", Range(-3, 3)) = " + Variable4.ToString();
            string DefaultParameters5 = ", Range(-1, 1)) = " + Variable5.ToString();
            string VoidName = "AnimatedShakeUV";
            string PreviewVariable = s_in.Result;

            if (PreviewVariable == null) PreviewVariable = "i.texcoord";


            FinalVariable1 = DefaultNameVariable1;
            FinalVariable2 = DefaultNameVariable2;
            FinalVariable3 = DefaultNameVariable3;
            FinalVariable4 = DefaultNameVariable4;
            FinalVariable5 = DefaultNameVariable5;

            s_out.StringPreviewLines = s_in.StringPreviewNew;

            if (AddParameters)
            {
                s_out.ValueLine = "float2 " + DefaultName + " = " + VoidName + "(" + PreviewVariable
                + ","
                + DefaultNameVariable1 + ","
                + DefaultNameVariable2 + ","
                + DefaultNameVariable3 + ","
                + DefaultNameVariable4 + ","
                + DefaultNameVariable5 + ");\n";
            }
            else
            {
                s_out.ValueLine = "float2 " + DefaultName + " = " + VoidName + "(" + PreviewVariable
                     + ","
                     + Variable.ToString() + ","
                     + Variable2.ToString() + ","
                     + Variable3.ToString() + ","
                     + Variable4.ToString() + ","
                     + Variable5.ToString() + ");\n";

            }
            s_out.StringPreviewNew = s_out.StringPreviewLines + s_out.ValueLine;
            s_out.Result = DefaultName;

            s_out.ParametersLines += s_in.ParametersLines;
            s_out.ParametersDeclarationLines += s_in.ParametersDeclarationLines;

            if (AddParameters)
            {
                s_out.ParametersLines += DefaultNameVariable1 + "(\"" + DefaultNameVariable1 + "\"" + DefaultParameters1 + "\n";
                s_out.ParametersLines += DefaultNameVariable2 + "(\"" + DefaultNameVariable2 + "\"" + DefaultParameters2 + "\n";
                s_out.ParametersLines += DefaultNameVariable3 + "(\"" + DefaultNameVariable3 + "\"" + DefaultParameters3 + "\n";
                s_out.ParametersLines += DefaultNameVariable4 + "(\"" + DefaultNameVariable4 + "\"" + DefaultParameters4 + "\n";
                s_out.ParametersLines += DefaultNameVariable5 + "(\"" + DefaultNameVariable5 + "\"" + DefaultParameters5 + "\n";

                s_out.ParametersDeclarationLines += "float " + DefaultNameVariable1 + ";\n";
                s_out.ParametersDeclarationLines += "float " + DefaultNameVariable2 + ";\n";
                s_out.ParametersDeclarationLines += "float " + DefaultNameVariable3 + ";\n";
                s_out.ParametersDeclarationLines += "float " + DefaultNameVariable4 + ";\n";
                s_out.ParametersDeclarationLines += "float " + DefaultNameVariable5 + ";\n";
            }

            Outputs[0].SetValue<SuperFloat2>(s_out);

            count++;

            return true;
        }
    }
}
