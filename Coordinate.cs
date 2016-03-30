using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace iAppAndroid
{
    class Coordinate
    {
        public int Table { set; get; }

        public int Place { set; get; }


        public string GetPlaceLetter()
        {
            switch (Place)
            {
                case 0:
                    return "A";
                case 1:
                    return "B";
                case 2:
                    return "C";
                default:
                    return "D";
            }
        }
    }
}