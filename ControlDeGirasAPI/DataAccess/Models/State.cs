﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class State
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //Relations

        public List<Process> Processes { get; set; }
    }
}
