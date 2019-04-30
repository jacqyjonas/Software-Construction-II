/// <summary>
/// This class holds the names read in
/// from the names list by NameSorting.cs
/// </summary>
///
/// <author>
/// Jacqlyne Mba-Jonas
/// </author>
/// <date>04/25/2019</date>

namespace NameSorting
{
    class Name
    {
        //A name shall not change after initialization
        public readonly string FirstName;
        public readonly string LastName;

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
