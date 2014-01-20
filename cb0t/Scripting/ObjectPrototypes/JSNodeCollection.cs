﻿using Jurassic;
using Jurassic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cb0t.Scripting.ObjectPrototypes
{
    [JSEmbed(Name = "NodeCollection")]
    class JSNodeCollection : ClrFunction
    {
        public JSNodeCollection(ScriptEngine eng)
            : base(eng.Function.InstancePrototype, "NodeCollection", new Objects.JSNodeCollection(eng))
        {

        }

        [JSCallFunction(Flags = JSFunctionFlags.ConvertNullReturnValueToUndefined)]
        public Objects.JSNodeCollection Call(object a)
        {
            return null;
        }

        [JSConstructorFunction(Flags = JSFunctionFlags.ConvertNullReturnValueToUndefined)]
        public Objects.JSNodeCollection Construct(object a)
        {
            return null;
        }
    }
}
