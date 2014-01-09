﻿using Jurassic;
using Jurassic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cb0t.Scripting.ObjectPrototypes
{
    [JSEmbed(Name = "HashlinkResult")]
    class JSHashlinkResult : ClrFunction
    {
        public JSHashlinkResult(ScriptEngine eng)
            : base(eng.Function.InstancePrototype, "HashlinkResult", new Objects.JSHashlinkResult(eng))
        {

        }

        [JSCallFunction(Flags = JSFunctionFlags.ConvertNullReturnValueToUndefined)]
        public Objects.JSHashlinkResult Call(object a)
        {
            return null;
        }

        [JSConstructorFunction(Flags = JSFunctionFlags.ConvertNullReturnValueToUndefined)]
        public Objects.JSHashlinkResult Construct(object a)
        {
            return null;
        }
    }
}
