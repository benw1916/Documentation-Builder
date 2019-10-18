using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class ConstructorData {
        private ArrayList title;
        private ArrayList comment;

        public ConstructorData() {
            this.title = new ArrayList();
            this.comment = new ArrayList();
        }

        public void AddTitle(String passedTitle) {
            this.title.Add(passedTitle);
        }

        public void AddComment(String passedComment) {
            this.comment.Add(passedComment);
        }

        public void SetTitleAndComment(String passedTitle, String passedComment) {
            AddTitle(passedTitle);
            AddComment(passedComment);
        }

        public int GetCount() {
            return this.title.Count;
        }

        public String GetTitle(int passedValue) {
            return this.title[passedValue].ToString();
        }

        public String GetComment(int passedValue) {
            return this.comment[passedValue].ToString();
        }

    }
}
