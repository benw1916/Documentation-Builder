using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class ClassContainer {
        private String name;
        private String description;
        private Boolean isSet = false;

        public void SetName(String passedTitle) { // This is a one-time-use method, it runs once, and is set forever.  I'd probably need to extend it more if I wanted multiple classes.
            if (isSet == false) {
                this.name = passedTitle;
                this.description = " ";
                isSet = true;
            }
        }

        public void SetDescription(String passedDescription) {
            this.description = passedDescription;
        }

        public void SetNameAndDescription(String passedName, String passedDescription) {
            SetName(passedName);
            SetDescription(passedDescription);
        }

        public String GetName() { // This returns the class name, less attractive version.
            if (isSet == true) {
                // String[] headerData = this.name.ToString().Split(' ');
                return this.name;//[1];
            }
            return "Error: No Class Name Set";
        }

        public String ReturnClassName() { // This is the more attractive version of GetClassname.
            return "Class: " + GetName() + "\n\n";
        }

        public Boolean IsDescriptionSet() {
            if (String.IsNullOrWhiteSpace(this.description) || String.IsNullOrEmpty(this.description)) {
                return false;
            }
            return true;
        }

        public String GetDescription() {
            return this.description;
        }

    }
}
