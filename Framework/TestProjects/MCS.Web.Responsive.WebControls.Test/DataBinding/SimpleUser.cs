using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Web.Responsive.WebControls.Test.DataBinding
{
    [Serializable]
    public class SimpleUser
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    [Serializable]
    public class SimpleUserCollection : EditableDataObjectCollectionBase<SimpleUser>
    {
    }

    [Serializable]
    public class SimpleUserContainer
    {
        public SimpleUserCollection Users
        {
            get;
            set;
        }

        public string CandidateID
        {
            get;
            set;
        }
    }

    public static class SimpleUserAdapter
    {
        public static SimpleUserContainer PrepareUsers()
        {
            SimpleUserContainer container = new SimpleUserContainer();

            SimpleUserCollection users = new SimpleUserCollection();

            users.Add(new SimpleUser() { ID = "sz", Name = "沈峥" });
            users.Add(new SimpleUser() { ID = "sr", Name = "沈嵘" });
            users.Add(new SimpleUser() { ID = "za", Name = "张岸" });
            users.Add(new SimpleUser() { ID = "sjb", Name = "史江波" });

            container.Users = users;

            return container;
        }
    }
}