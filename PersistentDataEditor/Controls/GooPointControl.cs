﻿using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;

namespace PersistentDataEditor
{
    internal class GooPointControl : GooHorizonalControlBase<GH_Point>
    {
        public override Guid AddCompnentGuid => new Guid("3581f42a-9592-4549-bd6b-1c0fc39d067b");


        public GooPointControl(Func<GH_Point> valueGetter, Func<bool> isNull, string name) : base(valueGetter, isNull, name)
        {

        }

        protected override BaseControlItem[] SetControlItems()
        {
            switch ((Point_Control)Instances.Settings.GetValue(typeof(Point_Control).FullName, 0))
            {
                default:
                    return new BaseControlItem[]
                    {
                        new GooInputBoxStringControl<GH_Point>(()=> ShowValue, _isNull),
                    };
                case Point_Control.XYZ:
                    return new BaseControlItem[]
                    {
                        new StringRender("X"),

                        new GooInputBoxStringControl<GH_Number>(()=> ShowValue == null ? null : new GH_Number(ShowValue.Value.X), _isNull),

                        new StringRender("Y"),

                        new GooInputBoxStringControl<GH_Number>(()=> ShowValue == null ? null : new GH_Number(ShowValue.Value.Y), _isNull),

                        new StringRender("Z"),

                        new GooInputBoxStringControl<GH_Number>(()=> ShowValue == null ? null : new GH_Number(ShowValue.Value.Z), _isNull),
                    };
            }


        }

        protected override GH_Point SetValue(IGH_Goo[] values)
        {
            switch ((Point_Control)Instances.Settings.GetValue(typeof(Point_Control).FullName, 0))
            {
                default:
                    return (GH_Point)values[0];

                case Point_Control.XYZ:
                    return new GH_Point(new Point3d(
                ((GH_Number)values[0]).Value,
                ((GH_Number)values[1]).Value,
                ((GH_Number)values[2]).Value));
            }
        }

        public override void DosomethingWhenCreate(IGH_DocumentObject obj)
        {
            if (obj == null) return;
            GH_Component com = (GH_Component)obj;

            if (com.Params.Input.Count < 3) return;

            if (com.Params.Input[0] is Param_Number param0 && _values[0].SaveValue is GH_Number Value0)
            {
                param0.PersistentData.Clear();
                param0.PersistentData.Append(Value0);
            }

            if (com.Params.Input[1] is Param_Number param1 && _values[1].SaveValue is GH_Number Value1)
            {
                param1.PersistentData.Clear();
                param1.PersistentData.Append(Value1);
            }

            if (com.Params.Input[2] is Param_Number param2 && _values[2].SaveValue is GH_Number Value2)
            {
                param2.PersistentData.Clear();
                param2.PersistentData.Append(Value2);
            }
        }
    }
}
