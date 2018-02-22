using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityWeb.Common.Interfaces;

namespace UniversityWeb.Common.Classes
{
    public abstract class ViewModelBase: IAngularView
    {
        public string NgAppName
        {
            get { return "UniversityApp"; }
        }
    }
}