using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class TextFramework {

        UserDefinedIcons udi;

        public TextFramework() {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(DisplayHeaders.GetOriginalVertIcon());
            this.udi.SetHoriIcon(DisplayHeaders.GetOriginalHoriIcon());
            this.udi.SetCrosIcon(DisplayHeaders.GetOriginalCrosIcon());
            this.udi.SetTypeWidth(DisplayHeaders.GetOriginalTypeWidth());
            this.udi.SetMethodWidth(DisplayHeaders.GetOriginalMethodWidth());
        }

        public TextFramework(String passedVertIcon) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(passedVertIcon);
            this.udi.SetHoriIcon(DisplayHeaders.GetOriginalHoriIcon());
            this.udi.SetCrosIcon(DisplayHeaders.GetOriginalCrosIcon());
            this.udi.SetTypeWidth(DisplayHeaders.GetOriginalTypeWidth());
            this.udi.SetMethodWidth(DisplayHeaders.GetOriginalMethodWidth());
        }

        public TextFramework(String passedVertIcon, String passedHoriIcon) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(passedVertIcon);
            this.udi.SetHoriIcon(passedHoriIcon);
            this.udi.SetCrosIcon(DisplayHeaders.GetOriginalCrosIcon());
            this.udi.SetTypeWidth(DisplayHeaders.GetOriginalTypeWidth());
            this.udi.SetMethodWidth(DisplayHeaders.GetOriginalMethodWidth());
        }

        public TextFramework(String passedVertIcon, String passedHoriIcon, String passedCrosIcon) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(passedVertIcon);
            this.udi.SetHoriIcon(passedHoriIcon);
            this.udi.SetCrosIcon(passedCrosIcon);
            this.udi.SetTypeWidth(DisplayHeaders.GetOriginalTypeWidth());
            this.udi.SetMethodWidth(DisplayHeaders.GetOriginalMethodWidth());
        }

        public TextFramework(String passedVertIcon, String passedHoriIcon, String passedCrosIcon, int passedTypeWidth, int passedMethodWidth) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(passedVertIcon);
            this.udi.SetHoriIcon(passedHoriIcon);
            this.udi.SetCrosIcon(passedCrosIcon);
            this.udi.SetTypeWidth(passedTypeWidth);
            this.udi.SetMethodWidth(passedMethodWidth);
        }

        public TextFramework(int passedTypeWidth) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(DisplayHeaders.GetOriginalVertIcon());
            this.udi.SetHoriIcon(DisplayHeaders.GetOriginalHoriIcon());
            this.udi.SetCrosIcon(DisplayHeaders.GetOriginalCrosIcon());
            this.udi.SetTypeWidth(passedTypeWidth);
            this.udi.SetMethodWidth(DisplayHeaders.GetOriginalMethodWidth());
        }

        public TextFramework(int passedTypeWidth, int passedMethodWidth) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(DisplayHeaders.GetOriginalVertIcon());
            this.udi.SetHoriIcon(DisplayHeaders.GetOriginalHoriIcon());
            this.udi.SetCrosIcon(DisplayHeaders.GetOriginalCrosIcon());
            this.udi.SetTypeWidth(passedTypeWidth);
            this.udi.SetMethodWidth(passedMethodWidth);
        }

        public static String LeftAlignmentTextWithPadding(String passedText, int TypeOrMethod) { // This function checks the length of the passed text, and adds padding to the end.
            StringBuilder leftAlign = new StringBuilder();
            leftAlign.Append(passedText);
            for (int i = 1; i <= TypeOrMethod - passedText.Length; i++) {
                leftAlign.Append(" ");
            }
            return leftAlign.ToString();
        }

        public static String LeftAlignmentTextWithPadding(String passedText, int TypeOrMethod, String endIcons) { // THis function checks the length of the passed text, adds padding to the end, and also adds icons to the beginning and end.  
            StringBuilder leftAlign = new StringBuilder();
            leftAlign.Append(endIcons);
            leftAlign.Append(passedText);
            for (int i = 1; i <= TypeOrMethod - passedText.Length; i++) {
                leftAlign.Append(" ");
            }
            leftAlign.Append(endIcons);
            return leftAlign.ToString();
        }

        public ArrayList FormatMethodText(String passedComment, int typeOrMethodWidth) { // This function takes input text, splits it by the width passed, and places it in to an Array.  
            StringBuilder textBlocks = new StringBuilder();
            ArrayList outputArray = new ArrayList();
            if (passedComment.Length % typeOrMethodWidth != 0) {
                do {
                    passedComment += " ";
                } while (passedComment.Length % typeOrMethodWidth != 0);
            }
            int character = 0;
            int multiplier = 1;
            for (int row = 0; row < passedComment.Length / typeOrMethodWidth; row++) {
                do {
                    textBlocks.Append(passedComment[character]);
                    character++;
                } while (character != (typeOrMethodWidth * multiplier));
                outputArray.Add(textBlocks.ToString());
                textBlocks.Clear();
                multiplier++;
            }

            return outputArray;
        }

        public String AssembleConstructorRow(String displayString) { // This is one of the heavy lifters in the library. This function builds the constructor output; it takes the output string, and formats properly, and adds icons to both ends.
            StringBuilder constructionRow = new StringBuilder();

            ArrayList constructionText = new ArrayList(FormatMethodText(displayString, this.udi.GetMethodWidth()));
         //   constructionRow.Append(Environment.NewLine);
            for (int p = 0; p < constructionText.Count; p++) {
                constructionRow.Append(this.udi.GetVertIcon());
                constructionRow.Append(constructionText[p]);
                constructionRow.Append(this.udi.GetVertIcon() + Environment.NewLine);
            }
            constructionRow.Append(CreateLine(this.udi.GetMethodWidth(), this.udi.GetHoriIcon(), this.udi.GetCrosIcon()) + Environment.NewLine);
            return constructionRow.ToString();
        }

        public String AssembleFunctionRow(String passedType, String passedMethod) { // This is one of the heavy lifters in the library.  This function builds a function row, without a comment.  It sends the passed data to be formatted, and returns it formatted.
            StringBuilder functionRow = new StringBuilder();
            ArrayList typeText = new ArrayList(FormatMethodText(passedType, this.udi.GetTypeWidth()));
            ArrayList outputText = new ArrayList(FormatMethodText(passedMethod.Replace('{', ' ').Trim(), this.udi.GetMethodWidth()));

            functionRow.Append(Environment.NewLine);
            for (int i = 0; i < outputText.Count; i++) {
                if (i == 0) {
                    functionRow.Append(this.udi.GetVertIcon() + LeftAlignmentTextWithPadding(typeText[i].ToString(), this.udi.GetTypeWidth()));
                } else {
                    functionRow.Append(this.udi.GetVertIcon() + LeftAlignmentTextWithPadding(" ", this.udi.GetTypeWidth()));
                }
                functionRow.Append(LeftAlignmentTextWithPadding(this.udi.GetVertIcon() + outputText[i].ToString(), this.udi.GetMethodWidth()) + this.udi.GetVertIcon() + Environment.NewLine);
            }
            functionRow.Append(GetHorizontalDivider());
            return functionRow.ToString();
        }

        public String AssembleFunctionRow(String passedType, String passedMethod, String passedComment) {// This is one of the heavy lifters in the library.  This function builds a function row, with a comment.  It sends the passed data to be formatted, and returns it formatted.
            StringBuilder functionRow = new StringBuilder();
            ArrayList typeText = new ArrayList(FormatMethodText(passedType, this.udi.GetTypeWidth()));
            ArrayList outputText = new ArrayList(FormatMethodText(passedMethod.Replace('{', ' ').Trim(), this.udi.GetMethodWidth()));
            ArrayList commentText = new ArrayList(FormatMethodText(passedComment, this.udi.GetMethodWidth()));

            functionRow.Append(Environment.NewLine + this.udi.GetVertIcon());
            for (int i = 0; i < outputText.Count; i++) {
                if (i == 0) {
                    functionRow.Append(LeftAlignmentTextWithPadding(typeText[i].ToString(), this.udi.GetTypeWidth()));
                } else {
                    functionRow.Append(this.udi.GetVertIcon() + LeftAlignmentTextWithPadding(" ", this.udi.GetTypeWidth()));
                }
                functionRow.Append(LeftAlignmentTextWithPadding(this.udi.GetVertIcon() + outputText[i].ToString(),this.udi.GetMethodWidth()) + this.udi.GetVertIcon() + Environment.NewLine);
            }

            for (int a = 0; a < commentText.Count; a++) {
                functionRow.Append(LeftAlignmentTextWithPadding(" ", this.udi.GetTypeWidth(), this.udi.GetVertIcon()));
                functionRow.Append(commentText[a].ToString() + this.udi.GetVertIcon() + Environment.NewLine);
            }
            functionRow.Append(GetHorizontalDivider());
            return functionRow.ToString();
        }

        public String GetConstructorSummaryHeader() {
            StringBuilder constructorHeader = new StringBuilder();
            constructorHeader.Append(CreateLine(this.udi.GetMethodWidth(), this.udi.GetHoriIcon(), this.udi.GetCrosIcon()) + Environment.NewLine);
            constructorHeader.Append(LeftAlignmentTextWithPadding(DisplayHeaders.GetConstructorMessage(), this.udi.GetMethodWidth(), this.udi.GetVertIcon()) + Environment.NewLine);
            constructorHeader.Append(CreateLine(this.udi.GetMethodWidth(), this.udi.GetHoriIcon(), this.udi.GetCrosIcon()));
            return constructorHeader.ToString();
        }

        public String GetMethodSummaryHeader() {
            StringBuilder headerMessage = new StringBuilder();
            headerMessage.Append(GetHorizontalDivider() + Environment.NewLine);
            headerMessage.Append(LeftAlignmentTextWithPadding(DisplayHeaders.GetMethodMessage(), this.udi.GetTypeWidth(), this.udi.GetVertIcon()));
            headerMessage.Append(LeftAlignmentTextWithPadding(DisplayHeaders.GetMethodDescriptionMessage(), this.udi.GetMethodWidth()));
            headerMessage.Append(this.udi.GetVertIcon() + Environment.NewLine);
            headerMessage.Append(GetHorizontalDivider());
            return headerMessage.ToString();
        }

        public String GetHorizontalDivider() {
            return this.udi.GetCrosIcon() + DevelopLine(this.udi.GetHoriIcon(), this.udi.GetTypeWidth()) + this.udi.GetCrosIcon() + DevelopLine(this.udi.GetHoriIcon(), this.udi.GetMethodWidth()) + this.udi.GetCrosIcon();
        }

        private String DevelopLine(String passedIcon, int TypeOrMethodLength) {
            StringBuilder typeLine = new StringBuilder();
            for (int i = 0; i < TypeOrMethodLength; i++) {
                typeLine.Append(passedIcon);
            }
            return typeLine.ToString();
        }

        private String CreateLine(int lineLength) { // The second of three "CreateLine" functions.  Pass a length value, and it will return the default icons in a line form.
            StringBuilder methodLine = new StringBuilder();
            methodLine.Append(this.udi.GetCrosIcon());
            for (int i = 0; i < lineLength; i++) {
                methodLine.Append(this.udi.GetHoriIcon());
            }
            methodLine.Append(this.udi.GetCrosIcon());
            return methodLine.ToString();
        }

        private String CreateLine(int lineLength, String printIcon, String endIcons) { // The second of three "CreateLine" functions.  This takes a passed length, icon to print, and end icon, and returns the formatted text.
            StringBuilder methodLine = new StringBuilder();
            methodLine.Append(endIcons);
            for (int i = 0; i < lineLength; i++) {
                methodLine.Append(printIcon);
            }
            methodLine.Append(endIcons);
            return methodLine.ToString();
        }

        private String CreateLine(int lineLength, String printIcon, String endIcons, Boolean beforeOrAfter) { // The second of three "CreateLine" functions.  I know this isn't optimal, but I'm using a boolean.  True is Before, and False is After.
            StringBuilder methodLine = new StringBuilder();
            if (beforeOrAfter) {
                methodLine.Append(endIcons);
            }
            for (int i = 0; i < lineLength; i++) {
                methodLine.Append(printIcon);
            }
            if (!beforeOrAfter) {
                methodLine.Append(endIcons);
            }
            return methodLine.ToString();
        }

    }

}
