// DO NOT MODIFY THIS FILE!  This is a default script for Glider.  If you want to
// change it, copy it into the Scripts folder and change it there.  Glider does
// not read this file.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Glider.Common.Objects
{
    public class DoAutoLog : GScript
    {
        public override void Execute()
        {
            Thread.Sleep(3000);
            Context.DoAutoLogin();
        }
    }
}