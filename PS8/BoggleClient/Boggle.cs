﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoggleClient
{
    public partial class Boggle : Form, IBoggleView
    {
        public Boggle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fired when user must be registered.
        /// Parameters are name and email.
        /// </summary>
        public event Action<string, string> RegisterPressed;
    }
}
