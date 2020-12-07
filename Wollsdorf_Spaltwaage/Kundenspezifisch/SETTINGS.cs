namespace Wollsdorf_Spaltwaage.Kundenspezifisch
{
    internal static class SETTINGS
    {
        public static int SQLTestCounter { get; set; }
        public static string ApplikationName { get; set; }
        public static string LizenzName { get; set; }
        public static string UserCode { get; set; }
        public static bool bISEntwicklungsTerminal { get; set; }

        public static System.Drawing.Color colButtonUnSel = System.Drawing.Color.DarkGray;
        public static System.Drawing.Color colMerkmalSelektiert = System.Drawing.Color.Olive;
        public static System.Drawing.Color colStufeSelektiert = System.Drawing.Color.Brown;
        public static System.Drawing.Color colTierklasseSelektiert = System.Drawing.Color.Chocolate;

        public static bool IS_EntwicklungsPC()
        {            
            return bISEntwicklungsTerminal;
        }
    }
}
