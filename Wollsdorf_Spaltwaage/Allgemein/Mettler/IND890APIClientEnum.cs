namespace Wollsdorf_Spaltwaage.Allgemein.Mettler
{
    internal class IND890APIClientEnum
    {
        //Scale
        public enum enumTareType
        {
            TARENORMAL = 0,
            PRETARE = 1
        }
        public enum enumTareResult
        {
            FALSE = 0,
            TRUE = 1
        }
        public enum enumZeroResult
        {
            FALSE = 0,
            TRUE = 1
        }
        public enum enumSwitchScaleResult
        {
            FALSE = 0,
            TRUE = 1
        }
        public enum ScaleNo
        {
            CURRENTSCALE = 0,
            SCALE1 = 1,
            SCALE2 = 2,
            SCALE3 = 3,
            SCALE4 = 4,
            SUMSCALE = 5
        }
        public enum InterfaceNo
        {
            X1 = 1,
            X2 = 2,
            X3 = 3,
            X4 = 4,
            X5 = 5,
            X6 = 6,
            X7 = 7
        }
        //Interface
        public enum enumConnectionPort
        {
            X1 = 1,
            X2 = 2,
            X3 = 3,
            X4 = 4,
            X5 = 5,
            X6 = 6,
            ETHERNET = 7
        }
        public enum enumTemplateNumber
        {
            TEMPLATE1 = 1,
            TEMPLATE2 = 2,
            TEMPLATE3 = 3,
            TEMPLATE4 = 4,
            TEMPLATE5 = 5,
            TEMPLATE6 = 6,
            TEMPLATE7 = 7,
            TEMPLATE8 = 8,
            TEMPLATE9 = 9,
            TEMPLATE10 = 10
        }
    }
}
