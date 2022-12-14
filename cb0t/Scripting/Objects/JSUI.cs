using Jurassic;
using Jurassic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cb0t.Scripting.Objects
{
    class JSUI : ObjectInstance
    {
        internal JSUI(ScriptEngine eng)
            : base(eng)
        {
            this.PopulateFunctions();
        }

        public JSUI(ObjectInstance prototype)
            : base(prototype.Engine, ((ClrFunction)prototype.Engine.Global["UI"]).InstancePrototype)
        {
            this.PopulateFunctions();
        }

        protected override string InternalClassName
        {
            get { return "UI"; }
        }

        // begin

        public CustomScriptSettings UIPanel { get; private set; }
        public bool CanCreate { get; set; }
        public bool CanAddControls { get; set; }

        [JSFunction(Name = "createUserListMenuOption", IsWritable = false, IsEnumerable = true)]
        public bool CreateUserListMenuOption(object a, object b)
        {
            if (this.CanAddControls || this.CanCreate)
                if (!(a is Undefined) && b is UserDefinedFunction)
                {
                    String text = a.ToString();
                    UserDefinedFunction callback = (UserDefinedFunction)b;

                    if (ScriptManager.UserListMenuOptions.Find(x => x.Text == text) == null)
                    {
                        ScriptManager.UserListMenuOptions.Add(new CustomJSMenuOption
                        {
                            Callback = callback,
                            Text = text
                        });

                        return true;
                    }
                }

            return false;
        }

        [JSFunction(Name = "createRoomMenuOption", IsWritable = false, IsEnumerable = true)]
        public bool CreateRoomMenuOption(object a, object b, object c)
        {
            if (this.CanAddControls || this.CanCreate)
                if (!(a is Undefined) && b is UserDefinedFunction)
                {
                    String text = a.ToString();
                    UserDefinedFunction callback = (UserDefinedFunction)b;

                    if (ScriptManager.RoomMenuOptions.Find(x => x.Text == text) == null)
                    {
                        JSCheckState cstate = JSCheckState.Unused;

                        if (c is bool)
                            cstate = ((bool)c) ? JSCheckState.Checked : JSCheckState.Unchecked;

                        ScriptManager.RoomMenuOptions.Add(new CustomJSMenuOption
                        {
                            Callback = callback,
                            Text = text,
                            CanCheck = cstate != JSCheckState.Unused,
                            IsChecked = cstate == JSCheckState.Checked
                        });

                        return true;
                    }
                }

            return false;
        }

        [JSFunction(Name = "createTextBox", IsWritable = false, IsEnumerable = true)]
        public JSUITextBox CreateTextBox()
        {
            if (this.CanAddControls)
                return new JSUITextBox(this.Engine.Object.InstancePrototype, this);
            else
                return null;
        }

        [JSFunction(Name = "createTextArea", IsWritable = false, IsEnumerable = true)]
        public JSUITextArea CreateTextArea()
        {
            if (this.CanAddControls)
                return new JSUITextArea(this.Engine.Object.InstancePrototype, this);
            else
                return null;
        }

        [JSFunction(Name = "createCheckBox", IsWritable = false, IsEnumerable = true)]
        public JSUICheckBox CreateCheckBox()
        {
            if (this.CanAddControls)
                return new JSUICheckBox(this.Engine.Object.InstancePrototype, this);
            else
                return null;
        }

        [JSFunction(Name = "createLabel", IsWritable = false, IsEnumerable = true)]
        public JSUILabel CreateLabel()
        {
            if (this.CanAddControls)
                return new JSUILabel(this.Engine.Object.InstancePrototype, this);
            else
                return null;
        }

        [JSFunction(Name = "createButton", IsWritable = false, IsEnumerable = true)]
        public JSUIButton CreateButton()
        {
            if (this.CanAddControls)
                return new JSUIButton(this.Engine.Object.InstancePrototype, this);
            else
                return null;
        }

        [JSFunction(Name = "createGroupBox", IsWritable = false, IsEnumerable = true)]
        public JSUIGroupBox CreateGroupBox()
        {
            if (this.CanAddControls)
                return new JSUIGroupBox(this.Engine.Object.InstancePrototype, this);
            else
                return null;
        }

        [JSFunction(Name = "createRadioButton", IsWritable = false, IsEnumerable = true)]
        public JSUIRadioButton CreateRadioButton(object a)
        {
            if (this.CanAddControls)
                if (!(a is Undefined))
                {
                    String str = a.ToString();

                    if (!String.IsNullOrEmpty(str))
                        return new JSUIRadioButton(this.Engine.Object.InstancePrototype, this, str);
                }

            return null;
        }

        [JSFunction(Name = "createListBox", IsWritable = false, IsEnumerable = true)]
        public JSUIListBox CreateListBox()
        {
            if (this.CanAddControls)
                return new JSUIListBox(this.Engine.Object.InstancePrototype, this);
            else
                return null;
        }

        [JSFunction(Name = "createComboBox", IsWritable = false, IsEnumerable = true)]
        public JSUIComboBox CreateComboBox()
        {
            if (this.CanAddControls)
                return new JSUIComboBox(this.Engine.Object.InstancePrototype, this);
            else
                return null;
        }

        [JSFunction(Name = "createImage", IsWritable = false, IsEnumerable = true)]
        public JSUIImage CreateImage()
        {
            if (this.CanAddControls)
                return new JSUIImage(this.Engine.Object.InstancePrototype, this);
            else
                return null;
        }

        [JSFunction(Name = "create", IsWritable = false, IsEnumerable = true)]
        public bool Create()
        {
            if (this.CanCreate)
            {
                this.UIPanel = Form1.SettingsContent.CreateCustomScriptSettings(this.Engine.ScriptName);
                this.CanCreate = false;
                this.CanAddControls = true;
                return true;
            }

            return false;
        }
    }
}
