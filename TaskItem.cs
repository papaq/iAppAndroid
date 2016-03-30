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
    class TaskItem
    {
        public int TaskId { get; set; }

        public Coordinate Sit { get; set; }

        public DateTime Time { get; set; }

        public int Kind { get; set; }

        public bool Done { get; set; }

        private string GetStrByInt()
        {
            switch (Kind)
            {
                case 0:
                    return "bell";
                case 1:
                    return "bill";
                case 2:
                    return "fork";
                default:
                    return "taxi";
            }
        }

        public int GetIdByKind()
        {
            return (int)typeof(Resource.Drawable).GetField(GetStrByInt()).GetValue(null);
        }
    }
}