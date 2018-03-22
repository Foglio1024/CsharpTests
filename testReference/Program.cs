using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceValueExample
{
    class ReferenceValueExample
    {
        class Object1
        {
            public Property Prop { get; set; }
        }
        class Object2
        {
            public Property Prop { get; set; }
        }
        class Property
        {
            public int Value { get; set; }
            public Property(Property other)
            {
                Value = other.Value;
            }
            public Property() { }
        }

        static void SetPropSameRef(Object1 js, Object2 pc)
        {
            //it just assigns reference; changing js.Prop.Value later will change js.Prop.Value too
            pc.Prop = js.Prop;
        }
        static void SetProp(Object1 js, Object2 pc)
        {
            //it instantiates a new property using js.Prop.Value for it
            pc.Prop = new Property(js.Prop);
        }
        static void Main(string[] args)
        {
            var src = new Object1();
            var dst = new Object2();

            src.Prop = new Property();
            src.Prop.Value = 1;

            bool valueOnly = true;

            if (valueOnly)
            {
                SetProp(src, dst);
                src.Prop.Value = 5;
                // -> src.Prop.Value : 5
                // -> dst.Prop.Value : 1
            }
            else
            {
                SetPropSameRef(src, dst);
                src.Prop.Value = 5;
                // -> src.Prop.Value : 5
                // -> dst.Prop.Value : 5
            }

        }

    }
}
