using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class DisplayHeaders {

        private static String origVertIcon = "|";
        private static String origHoriIcon = "-";
        private static String origCrosIcon = "+";
        private static int origTypeWidth = 20;
        private static int origMethodWidth = 60;

        public static String GetMethodMessage() {
            return "Modifier and Type";
        }

        public static String GetConstructorMessage() {
            return "Constructor and Description";
        }

        public static String GetMethodDescriptionMessage() {
            return "Method and Description";
        }

        public static String GetOriginalVertIcon() {
            return origVertIcon;
        }
        public static String GetOriginalHoriIcon() {
            return origHoriIcon;
        }

        public static String GetOriginalCrosIcon() {
            return origCrosIcon;
        }

        public static int GetOriginalTypeWidth() {
            return origTypeWidth;
        }

        public static int GetOriginalMethodWidth() {
            return origMethodWidth;
        }

        public static String GetCurrentDate() {
            return DateTime.Now.ToString("MM/dd/yyyy -- hh:mm:ss tt");
        }

    }
}
