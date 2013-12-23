﻿using Jurassic;
using Jurassic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cb0t.Scripting.ObjectPrototypes
{
    [JSEmbed(Name = "User")]
    class JSUser : ClrFunction
    {
        public JSUser(ScriptEngine eng)
            : base(eng.Function.InstancePrototype, "User", new Objects.JSUser(eng))
        {

        }

        [JSCallFunction(Flags = JSFunctionFlags.ConvertNullReturnValueToUndefined)]
        public Objects.JSUser Call(object a)
        {
            return null;
        }

        [JSConstructorFunction(Flags = JSFunctionFlags.ConvertNullReturnValueToUndefined)]
        public Objects.JSUser Construct(object a)
        {
            return null;
        }
    }
}
