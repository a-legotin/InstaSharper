using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaUserShortList : List<InstaUserShort>, IInstaBaseList
    {
        public string NextId { get; set; }
    }
}