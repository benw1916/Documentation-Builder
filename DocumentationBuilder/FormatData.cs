using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;

namespace DocumentationBuilder {
    class FormatData { // This class does all the heavy lifting, it parses the data and places it in the appropriate arrays.
        private ClassContainer classContain;
        private ConstructorData constructData;
        //        private ArrayList rawConstructorsorMethods;

        private FunctionData functionData;

        private ArrayList Variables;

        public FormatData() { // Default constructor; this initializes all the arraylists.
            this.classContain = new ClassContainer();
       //     this.rawConstructorsorMethods = new ArrayList();

            this.constructData = new ConstructorData();

            this.functionData = new FunctionData();

            this.Variables = new ArrayList();

        }

        public void SplitConstructorComment(String passedRawData) { // Splits the passed type from the name of the function.
            if (Regex.IsMatch(passedRawData, @"//.*$")) {
                String[] constructorAndComment = passedRawData.Split(new string[] { "//" }, StringSplitOptions.None);
                SetConstructor(constructorAndComment[1].Replace("{", ""), constructorAndComment[1]);
            } else {
                SetConstructor(passedRawData.Replace("{", ""));
            }
        }

        public void SplitFunctionComment(String passedRawData) { // This function splits the name of the function from the comment.
            String[] visibilityAndFunction = passedRawData.Split(new[] { ' ' }, 3);
            if (Regex.IsMatch(passedRawData, @"//.*$")) {
                String[] functionAndComment = visibilityAndFunction[2].Split(new string[] { "//" }, StringSplitOptions.None);
                this.functionData.SetDataLine(visibilityAndFunction[1], functionAndComment[0], functionAndComment[1]);
            } else {
                String[] removeParentheses = visibilityAndFunction[2].Split('{');
                this.functionData.SetDataLine(visibilityAndFunction[1], removeParentheses[0]);
            }
        }

        public void SetConstructor(String passedConstruct) { // Saves passed data to constructor dataset.  If only one argument is passed, constructor comment is blank.
            SetConstructorTitle(passedConstruct.Trim());
            SetConstructorComment("");
        }

        public void SetConstructor(String passedConstruct, String passedComment) { // Similar to the method above, but with a passed comment variable.
            SetConstructorTitle(passedConstruct.Trim());
            SetConstructorComment(passedComment.Trim());
        }

        public void SetVariables(String passedVariable) { // I'm not sure whether I'm going to implement this or not.  This will save passed variables to an arraylist.
            this.Variables.Add(passedVariable.Trim());
        }

        public String[] GetVariables() {
            String[] variableList = { };
            for (int i = 0; i < this.Variables.Count; i++) {
                variableList[i] = this.Variables[i].ToString();
            }
            return variableList;
        }

        public String GetVariable(int passedCount) {
            return this.Variables[passedCount].ToString();
        }

        // ********* Function Data ********

        public void SetDataLine(String passedType, String passedMethod) {
            this.functionData.SetDataLine(passedType, passedMethod, "");
        }

        public void SetDataLine(String passedType, String passedMethod, String passedComment) {
            this.functionData.SetDataLine(passedType, passedMethod, passedComment);
        }

        public void SetDataLine(String passedMethod) {
            this.functionData.SetDataLine("", passedMethod, "");
        }

        public int GetFunctionCount() { // Another assistance function, that returns the size of the arraylist.
            return this.functionData.GetCount();
        }

        public String GetFunctionType(int positionValue) { // Function that gets a type saved at a specific location.
            return this.functionData.GetType(positionValue);
        }

        public String GetFunctionTitle(int positionValue) { // Function that gets a method saved at a specific location.
            return this.functionData.GetTitle(positionValue);
        }

        public String GetFunctionComment(int positionValue) {
            return this.functionData.GetComment(positionValue);
        }

        // ********* Constructor Data **********
        public String ReturnConstructor(int positionValue) {
            return GetConstructorTitle(positionValue) + " -- " + GetConstructorComment(positionValue);
        }

        public int ConstructorCount() { // This is an assistance function, this is for a loop or something.
            return this.constructData.GetCount();
        }

        public String GetConstructorTitle(int positionValue) { // Function that gets a constructor saved a specific location.
            return this.constructData.GetTitle(positionValue);
        }

        public String GetConstructorComment(int positionValue) {
            return this.constructData.GetComment(positionValue);
        }

        public void SetConstructorTitle(String passedTitle) {
            this.constructData.AddTitle(passedTitle);
        }

        public void SetConstructorComment(String passedComment) {
            this.constructData.AddComment(passedComment);
        }

        public Boolean IsCommentSet(int positionValue) {
            if (String.IsNullOrEmpty(GetConstructorComment(positionValue)) || String.IsNullOrWhiteSpace(GetConstructorComment(positionValue))) {
                return false;
            }
            return true;
        }

        public void SetConstructorTitleAndComment(String passedTitle, String passedComment) {
            this.constructData.SetTitleAndComment(passedTitle, passedComment);
        }

        // ********* Class Container ***********
        public void SetClassContainerName(String passedTitle) {
            this.classContain.SetName(passedTitle);
        }

        public void SetClassContainerDescription(String passedDescription) {
            this.classContain.SetDescription(passedDescription);
        }

        public void SetClassContainerNameAndDescription(String passedTitle, String passedDescription) {
            this.classContain.SetNameAndDescription(passedTitle, passedDescription);
        }

        public String GetClassContainerName() {
            return this.classContain.GetName();
        }

        public String ReturnClassContainerName() {
            return this.classContain.ReturnClassName();
        }

        public String GetClassContainerDescription() {
            return this.classContain.GetDescription();
        }

        public Boolean IsClassContainerDescriptionSet() {
            return this.classContain.IsDescriptionSet();
        }

    }

    class FunctionData {
        private ArrayList type;
        private ArrayList title;
        private ArrayList comment;

        public FunctionData() {
            this.type = new ArrayList();
            this.title = new ArrayList();
            this.comment = new ArrayList();
        }

        public void SetDataLine(String passedType, String passedMethod) {
            this.type.Add(passedType.Trim());
            this.title.Add(passedMethod.Trim());
            this.comment.Add("");
        }

        public void SetDataLine(String passedType, String passedMethod, String passedComment) {
            this.type.Add(passedType.Trim());
            this.title.Add(passedMethod.Trim());
            this.comment.Add(passedComment.Trim());
        }

        public void SetDataLine(String passedMethod) {
            this.type.Add("");
            this.title.Add(passedMethod.Trim());
            this.comment.Add("");
        }

        public int GetCount() { // Another assistance function, that returns the size of the arraylist.
            return this.title.Count;
        }

        public String GetType(int positionValue) { // Function that gets a type saved at a specific location.
            return this.type[positionValue].ToString();
        }

        public String GetTitle(int positionValue) { // Function that gets a method saved at a specific location.
            return this.title[positionValue].ToString();
        }

        public String GetComment(int positionValue) {
            return this.comment[positionValue].ToString();
        }

    }

}