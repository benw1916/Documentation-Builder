using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class UserDefinedIcons {
        private String vertIcon;
        private String horiIcon;
        private String crosIcon;
        private int typeWidth;
        private int methodWidth;

        public UserDefinedIcons() {

        }

        public UserDefinedIcons(String userDefinedVert, String userDefinedHori, String userDefinedCros, int userDefinedType, int userDefinedMethod) {
            SetVertIcon(userDefinedVert);
            SetHoriIcon(userDefinedHori);
            SetCrosIcon(userDefinedCros);
            SetTypeWidth(userDefinedType);
            SetMethodWidth(userDefinedMethod);
        }

        public void SetVertIcon(String passedVertIcon) {
            this.vertIcon = passedVertIcon;
        }

        public void SetHoriIcon(String passedHoriIcon) {
            this.horiIcon = passedHoriIcon;
        }

        public void SetCrosIcon(String passedCrosIcon) {
            this.crosIcon = passedCrosIcon;
        }

        public void SetTypeWidth(int passedTypeWidth) {
            this.typeWidth = passedTypeWidth;
        }

        public void SetMethodWidth(int passedMethodWidth) {
            this.methodWidth = passedMethodWidth;
        }

        public String GetVertIcon() {
            return this.vertIcon;
        }

        public String GetHoriIcon() {
            return this.horiIcon;
        }

        public String GetCrosIcon() {
            return this.crosIcon;
        }

        public int GetTypeWidth() {
            return this.typeWidth;
        }

        public int GetMethodWidth() {
            return this.methodWidth;
        }



    }
}
