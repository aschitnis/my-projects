﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.Generic.Template
{
    public interface IDocument
    {
        string Title { get; set; }
        string Format { get; set; }
    }
}
