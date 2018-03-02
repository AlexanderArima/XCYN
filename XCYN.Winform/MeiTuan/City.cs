using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Winform.MeiTuan
{
    public class City : IEquatable<City>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string MeiShiURL { get; set; }

        public string URL { get; set; }

        public bool State { get; set; } = true;

        public string AddTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        public bool Equals(City other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return Name.Equals(other.Name) && URL.Equals(other.URL);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashProductName = Name == null ? 0 : Name.GetHashCode();

            //Get hash code for the Code field.
            int hashProductURL = URL.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductURL;
        }
    }
}
