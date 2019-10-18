using System;  
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class DocumentStripper {

        //String[] userText;
        private String[] dividedUserInput;

        public DocumentStripper(String rawUserText, FormatData fd, String codeType) {
            SplitInputText(rawUserText);
            TrimUserInput();
            if (codeType.Equals("Java") || codeType.Equals("C#")) {
                SetJavaClass(fd);
                StoreJavaConstructorsAndFunctions(fd);
            }
            if (codeType.Equals("Python")) {

            }
            if (codeType.Equals("Ruby")) {

            }

        }

        public void SplitInputText(String rawUserText) { // Divides each line by newline, and places the data in a string array.
            dividedUserInput = rawUserText.Split('\n');
        }

        private void TrimUserInput() {
            for (int i = 0; i < dividedUserInput.Length; i++) { // Iterates through string array and trims blank spaces.
                dividedUserInput[i] = dividedUserInput[i].Trim();
            }
        }

        private void SetJavaClass(FormatData fd) {
            String classRegex = @"^(class)[ ](\w+)[ ]\{.*";
            Regex rgx = new Regex(classRegex);

            for (int p = 0; p < dividedUserInput.Length; p++) {
                Match match = rgx.Match(dividedUserInput[p]);
                if (match.Success) {
                    if (dividedUserInput[p].Contains("//")) {
                        String[] splitClassTitleComment = dividedUserInput[p].Split(new string[] { "//" }, StringSplitOptions.None);
                        fd.SetClassContainerNameAndDescription(dividedUserInput[0], dividedUserInput[1]);
                    } else {
                        fd.SetClassContainerName(dividedUserInput[p].ToString());
                    }

                }
            }
        }

        private void SetPythonClass(FormatData fd) {
            String classRegex = @"^(class)[ ](\w+)[ ]\{.*";
            Regex rgx = new Regex(classRegex);

            for (int p = 0; p < dividedUserInput.Length; p++) {
                Match match = rgx.Match(dividedUserInput[p]);
                if (match.Success) {
                    if (dividedUserInput[p].Contains("//")) {
                        String[] splitClassTitleComment = dividedUserInput[p].Split(new string[] { "//" }, StringSplitOptions.None);
                        fd.SetClassContainerNameAndDescription(dividedUserInput[0], dividedUserInput[1]);
                    } else {
                        fd.SetClassContainerName(dividedUserInput[p].ToString());
                    }

                }
            }
        }

        private void StoreJavaConstructorsAndFunctions(FormatData fd) {
            
            for (int p = 0; p < dividedUserInput.Length; p++) {
                String constructorMatch = @"^(public|private)[ ](\S*)\(.*$";
                String functionMatch = @"^(public|private).\w+\s\w+\(";
                String functionOrConstructor = @"^(public|private)\s\w+.*$";
                foreach (Match m in Regex.Matches(dividedUserInput[p].ToString(), functionOrConstructor)) {
                    if (!m.Value.Contains('=') && Regex.IsMatch(m.Value, constructorMatch)) {
                        fd.SplitConstructorComment(m.Value);
                    }
                    else if (!m.Value.Contains('=') && Regex.IsMatch(m.Value, functionMatch)) {
                        fd.SplitFunctionComment(m.Value);
                    }
                }
            }

        }
    }

    

}
